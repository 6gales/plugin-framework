using System.Collections.Generic;
using System.Drawing;

namespace Plugins.Intermediate
{
    public interface IPlugin
    {
        public string Name { get; }

        public Bitmap Process(Bitmap image, Dictionary<string, object> parameters);
    }
}
