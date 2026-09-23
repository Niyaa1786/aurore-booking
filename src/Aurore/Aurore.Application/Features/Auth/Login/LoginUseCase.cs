using Aurore.Application.Common.Exceptions;
using Aurore.Application.Common.Interfaces;
using Aurore.Application.Features.Auth.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Aurore.Application.Features.Auth.Login
{
    public class LoginUseCase : IUseCase<LoginRequest, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IValidator<LoginRequest> _validator;

        public LoginUseCase(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IValidator<LoginRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _validator = validator;
        }

        public async Task<LoginResponse> ExecuteAsync(LoginRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email, ct);

            if (user is null)
                throw new AppValidationException(nameof(request.Email), "Invalid email or password.");

            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                throw new AppValidationException(nameof(request.Password), "Invalid email or password.");

            var tokenResult = _tokenGenerator.GenerateToken(user);

            user.SetRefreshToken(tokenResult.RefreshToken, tokenResult.RefreshTokenExpiration);
            await _unitOfWork.SaveChangesAsync(ct);

            return new LoginResponse
            {
                AccessToken = tokenResult.AccessToken,
                RefreshToken = tokenResult.RefreshToken,
                AccessTokenExpiration = tokenResult.AccessTokenExpiration,
                RefreshTokenExpiration = tokenResult.RefreshTokenExpiration,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToString()
                }
            };
        }
    }
}

