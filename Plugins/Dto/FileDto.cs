using Microsoft.AspNetCore.Http;

namespace Plugins.Api.Dto
{
    public class FileDto
    {
        public IFormFile File { get; set; }
    }
}