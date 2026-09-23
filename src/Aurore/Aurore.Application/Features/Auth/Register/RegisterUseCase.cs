using Aurore.Application.Common.Exceptions;
using Aurore.Application.Common.Interfaces;
using Aurore.Domain.Entities;
using Aurore.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application.Features.Auth.Register
{
    public class RegisterUseCase : IUseCase<RegisterRequest, RegisterResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IValidator<RegisterRequest> _validator;

        public RegisterUseCase(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IValidator<RegisterRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }

        public async Task<RegisterResponse> ExecuteAsync(RegisterRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var isEmailExist = await _unitOfWork.Users.ExistByEmail(request.Email, ct);
            if (isEmailExist)
                throw new AppValidationException(nameof(request.Email), "Email already registered");

            var passwordHash = _passwordHasher.HashPassword(request.Password);

            var user = new User(request.FullName, request.Email, passwordHash, UserRole.Customer);

            if (!string.IsNullOrEmpty(request.Phone))
                user.UpdateProfile(request.FullName, request.Phone);

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync(ct);

            return new RegisterResponse();
        }
    }
}
