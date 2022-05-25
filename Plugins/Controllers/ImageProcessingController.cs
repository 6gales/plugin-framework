using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plugins.Api.Dto;
using Plugins.Api.Services;

namespace Plugins.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ImageProcessingController : ControllerBase
    {
        private readonly ILogger<ImageProcessingController> _logger;
        private readonly ImageProcessingService _imageProcessingService;

        public ImageProcessingController(ILogger<ImageProcessingController> logger, ImageProcessingService imageProcessingService)
        {
            _logger = logger;
            _imageProcessingService = imageProcessingService;
        }

        [HttpPost("image")]
        [Consumes("multipart/form-data")]
        public IActionResult PostImage([FromForm] FileDto fileDto)
        {
            try
            {
                var file = fileDto.File;

                if (file == null || file.Length == 0)
                {
                    return BadRequest("please send a photo");
                }
                
                using var stream = file.OpenReadStream();
                var image = new Bitmap(stream);
                var id = _imageProcessingService.Save(image);

                return Ok(id);
            }
            catch
            {
                return BadRequest("error while uploading image");
            }
        }

        [HttpPost("process/{id}")]
        public IActionResult ProcessImage(Guid id, [FromBody] IReadOnlyList<Command> commands)
        {
            if (!_imageProcessingService.TryProcessImage(id, commands, out var image))
            {
                return BadRequest("image was not loaded");
            }

            var outputStream = new MemoryStream();
            image.Save(outputStream, ImageFormat.Jpeg);
            outputStream.Seek(0, SeekOrigin.Begin);
            return File(outputStream, "image/jpeg");
        }

        [HttpGet("commands")]
        public IEnumerable<string> Get()
        {
            return _imageProcessingService.Commands;
        }
    }
}
