using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application.Features.Auth.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old password is required");
        }
    }
}
