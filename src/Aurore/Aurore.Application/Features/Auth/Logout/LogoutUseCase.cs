using Aurore.Application.Common.Exceptions;
using Aurore.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application.Features.Auth.Logout
{
    public class LogoutUseCase : IUseCase<LogoutRequest, LogoutResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogoutUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LogoutResponse> ExecuteAsync(LogoutRequest request, CancellationToken ct = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, ct);
            if (user == null)
                throw new NotFoundException("User not found.");

            user.RevokeRefreshToken();
            await _unitOfWork.SaveChangesAsync(ct);

            return new LogoutResponse();
        }
    }
}
