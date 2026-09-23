using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application.Features.Auth.Login
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
