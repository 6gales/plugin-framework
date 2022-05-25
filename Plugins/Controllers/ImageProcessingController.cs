using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plugins.Intermediate;

namespace Plugins.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ImageProcessingController : ControllerBase
    {
        private readonly ILogger<ImageProcessingController> _logger;
        private readonly IPluginProvider _pluginProvider;
        private readonly Dictionary<Guid, Bitmap> _savedImages = new Dictionary<Guid, Bitmap>();

        public ImageProcessingController(ILogger<ImageProcessingController> logger, IPluginProvider pluginProvider)
        {
            _logger = logger;
            _pluginProvider = pluginProvider;
        }

        [HttpPost("image")]
        public IActionResult PostImageNews([FromForm(Name = "body")] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Please send a photo");
                }

                var id = Guid.NewGuid();
                using (var stream = file.OpenReadStream())
                {
                    var image = new Bitmap(stream);
                    _savedImages[id] = image;
                }

                return Ok(id);
            }
            catch
            {
                return BadRequest("error while uploading image");
            }
        }

        [HttpPost("process/{id}")]
        public IActionResult ProcessImage([FromQuery] Guid id, [FromBody] IReadOnlyList<Command> commands)
        {
            if (!_savedImages.TryGetValue(id, out var image))
            {
                return BadRequest("image was not loaded");
            }

            foreach (var command in commands)
            {
                image = _pluginProvider.Process(image, command.Name, command.Parameters);
            }

            var outputStream = new MemoryStream();
            image.Save(outputStream, ImageFormat.Jpeg);
            outputStream.Seek(0, SeekOrigin.Begin);
            return File(outputStream, "image/jpeg");
        }

        [HttpGet("commands")]
        public IEnumerable<string> Get()
        {
            return _pluginProvider.Commands;
        }
    }
}
