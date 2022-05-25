using System.Drawing;
using Plugins.Logic.Commands;

namespace Plugins.Logic.Plugins
{
    public class ResizePlugin
    {
        public const string Name = "Resize";

        public static Bitmap Process(Bitmap image, ResizeCommand command)
        {
            return new Bitmap(image, new Size(command.Width, command.Height));
        }
    }
}
