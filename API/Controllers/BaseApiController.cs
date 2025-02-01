using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected ActionResult HandleResult<T>(Result<T> result,
        string? actionName = null,
        object? routeValues = null)
    {
        if (result is null) return NotFound();

        if (!result.IsSuccess) return BadRequest(result.Error);

        if (typeof(T) == typeof(Unit)) return NoContent();

        if (!string.IsNullOrEmpty(actionName) && routeValues is not null)
            return CreatedAtAction(actionName, routeValues, result.Value);

        return Ok(result.Value);
    }
}
