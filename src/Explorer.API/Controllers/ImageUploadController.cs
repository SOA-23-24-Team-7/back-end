using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/images")]
public class ImageUploadController : BaseApiController
{
    [HttpPost]
    public ActionResult<String> Upload(IFormFile image)
    {
        if (image == null || image.Length == 0) return CreateResponse(Result.Fail("No image sent"));

        var imageExtension = Path.GetExtension(image.FileName);
        var imageName = Guid.NewGuid().ToString() + imageExtension;
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", imageName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
                image.CopyTo(fileStream);
        }

        return CreateResponse(Result.Ok(imageName));
    }
}
