using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financial.Server.Controllers;

[AllowAnonymous]
public class MainController: ControllerBase
{
    public MainController()
    { }

    public IActionResult Index()
    {
        return Ok(new { query = 1 });
    }
}