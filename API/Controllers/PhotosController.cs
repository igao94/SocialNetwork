using Application.Photos.AddPhoto;
using Application.Photos.DeletePhoto;
using Application.Photos.SetMainPhoto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PhotosController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddPhoto([FromForm] AddPhotoCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpPut("setMainPhoto/{photoId}")]
    public async Task<IActionResult> SetMainPhoto(int photoId)
    {
        return HandleResult(await mediator.Send(new SetMainPhotoCommand(photoId)));
    }

    [HttpDelete("{photoId}")]
    public async Task<IActionResult> DeletePhoto(int photoId)
    {
        return HandleResult(await mediator.Send(new DeletePhotoCommand(photoId)));
    }
}
