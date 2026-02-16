using LAIE.Application.Analytics.Queries;
using LAIE.Application.Decisions.Commands;
using LAIE.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAIE.Api;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // In production this validates hashed credentials with identity store.
        var accessToken = _tokenService.GenerateAccessToken(Guid.NewGuid(), request.Email, new[] { "User" });
        var refreshToken = _tokenService.GenerateRefreshToken();
        return Ok(new AuthResponse(accessToken, refreshToken));
    }
}

public sealed record LoginRequest(string Email, string Password);
public sealed record AuthResponse(string AccessToken, string RefreshToken);

[ApiController]
[Route("api/decisions")]
[Authorize]
public sealed class DecisionsController : ControllerBase
{
    private readonly ISender _sender;

    public DecisionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDecisionCommand command, CancellationToken cancellationToken)
    {
        var id = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id }, new { id });
    }
}

[ApiController]
[Route("api/analytics")]
[Authorize(Policy = "AnalystOrAdmin")]
public sealed class AnalyticsController : ControllerBase
{
    private readonly ISender _sender;

    public AnalyticsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("snapshot/{userId:guid}")]
    public async Task<IActionResult> Snapshot(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAnalyticsSnapshotQuery(userId), cancellationToken);
        return Ok(result);
    }
}

[ApiController]
[Route("api/admin")]
[Authorize(Policy = "AdminOnly")]
public sealed class AdminController : ControllerBase
{
    [HttpGet("audit-logs")]
    public IActionResult GetAuditLogs() => Ok(new { message = "Audit log stream endpoint placeholder" });
}
