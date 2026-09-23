using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application.Features.Auth.Register
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Phone { get; set; }
    }
}
