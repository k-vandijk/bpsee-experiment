using BPSEE.Experiment.Application.Dtos.User;
using BPSEE.Experiment.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BPSEE.Experiment.Microservices.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await _userService.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var user = await _userService.GetByIdAsync(id, ct);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        var user = await _userService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
}
