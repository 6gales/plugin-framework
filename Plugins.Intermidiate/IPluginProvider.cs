using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Plugins.Intermediate
{
    public interface IPluginProvider
    {
        IReadOnlyCollection<string> Commands { get; }

        Bitmap Process(Bitmap image, string commandName, Dictionary<string, object> parameters);
    }
}
