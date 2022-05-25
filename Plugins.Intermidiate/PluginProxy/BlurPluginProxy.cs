using System.Collections.Generic;
using System.Drawing;
using Plugins.Logic.Commands;
using Plugins.Logic.Plugins;

namespace Plugins.Intermediate.PluginProxy
{
    public class BlurPluginProxy : IPlugin
    {
        public string Name => BlurPlugin.Name;

        public Bitmap Process(Bitmap image, Dictionary<string, object> parameters)
        {
            return BlurPlugin.Process(image, parameters.ToCommand<BlurCommand>());
        }
    }
}
