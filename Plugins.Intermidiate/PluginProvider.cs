using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Plugins.Intermediate
{
    public class PluginProvider : IPluginProvider
    {
        private readonly Dictionary<string, IPlugin> _plugins;

        public PluginProvider(IEnumerable<IPlugin> plugins)
        {
            _plugins = plugins.ToDictionary(p => p.Name, p => p);
        }

        public IReadOnlyCollection<string> Commands => _plugins.Keys;

        public Bitmap Process(Bitmap image, string commandName, Dictionary<string, object> parameters)
        {
            if (!_plugins.TryGetValue(commandName, out var plugin))
            {
                return image;
            }

            return plugin.Process(image, parameters);
        }
    }
}
