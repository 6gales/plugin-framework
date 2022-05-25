using System.Collections.Generic;

namespace Plugins.Api.Dto
{
    public class Command
    {
        public string Name { get; set; }

        public Dictionary<string, object> Parameters { get; set; }
    }
}
