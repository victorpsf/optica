using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Input.PersonalModules;
using Shared.Models;

namespace Personal.Server.Controllers;

[Authorize(nameof (Authorization.PersonalPermission.PESSOAL_AUTORIZACAO))]
[AllowAnonymous]
public class PersonController: ControllerBase
{
    [Authorize(nameof (Authorization.PersonalPermission.PESSOAL_VISUALIZAR))]
    [HttpGet]
    public IActionResult Index(
        [FromQuery] PersonFilter filter
    ) {
        return Ok(filter);
    }
}