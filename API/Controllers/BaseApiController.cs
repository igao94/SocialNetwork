using API.Extensions;
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

        if (typeof(T) == typeof(byte[]) && result.Value is not null)
            return File((byte[])(object)result.Value, "application/zip", "UserData.zip");

        if (!string.IsNullOrEmpty(actionName) && routeValues is not null)
            return CreatedAtAction(actionName, routeValues, result.Value);

        return Ok(result.Value);
    }

    protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
    {
        if (!result.IsSuccess) return BadRequest(result.Error);

        if (result.Value is null) return NotFound();

        Response.AddPaginationHeader(result.Value.CurrentPage,
            result.Value.PageSize,
            result.Value.TotalCount,
            result.Value.TotalPages);

        return Ok(result.Value);
    }
}
