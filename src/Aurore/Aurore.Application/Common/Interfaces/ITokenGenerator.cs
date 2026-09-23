using Aurore.Application.Common.DTOs;
using Aurore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Aurore.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        TokenResult GenerateToken(User user);
    }
}
