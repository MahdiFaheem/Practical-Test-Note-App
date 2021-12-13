﻿using Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Interfaces.EncyptionInterfaces
{
    public interface IAuthenticationService
    {
        string CreateToken(User user);
        string CreateRefreshToken(User user);
        string VerifyToken(string token);
        List<Claim> GetClaimsFromToken(string token);
    }
}
