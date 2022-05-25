using System.Collections.Generic;
using System.Drawing;
using Plugins.Logic.Commands;
using Plugins.Logic.Plugins;

namespace Plugins.Intermediate.PluginProxy
{
    public class ResizePluginProxy : IPlugin
    {
        public string Name => ResizePlugin.Name;

        public Bitmap Process(Bitmap image, Dictionary<string, object> parameters)
        {
            return ResizePlugin.Process(image, parameters.ToCommand<ResizeCommand>());
        }
    }
}
