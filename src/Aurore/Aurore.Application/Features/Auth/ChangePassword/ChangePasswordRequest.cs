using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Aurore.Application.Features.Auth.ChangePassword
{
    public class ChangePasswordRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
