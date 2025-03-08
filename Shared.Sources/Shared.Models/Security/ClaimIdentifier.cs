﻿using System.Security.Claims;
using System.Text.Json;

namespace Shared.Models.Security;

public class ClaimIdentifier
{
    public int UserId { get; set; }
    public string[] Permissions { get; set; }
    public string[] Roles { get; set; }
    
    public static ClaimIdentifier GetInstance(IEnumerable<Claim> claims)
    {
        var uI = claims.FirstOrDefault(a => a.Type == "uI");
        var uR = claims.FirstOrDefault(a => a.Type == "uR");
        var uP = claims.FirstOrDefault(a => a.Type == "uP");

        if (uI == null)
            throw new NullReferenceException();

        return new ClaimIdentifier()
        {
            UserId = Convert.ToInt32(uI.Value),
            Permissions = uP is null ? new string[0]: JsonSerializer.Deserialize<List<string>>(uP.Value).ToArray(),
            Roles = uR is null ? new string[0]: JsonSerializer.Deserialize<List<string>>(uR.Value).ToArray(),
        };
    }
}