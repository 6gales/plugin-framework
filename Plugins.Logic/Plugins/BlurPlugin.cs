using System.Drawing;
using Plugins.Logic.Commands;

namespace Plugins.Logic.Plugins
{
    public class BlurPlugin
    {
        public const string Name = "Blur";

        public static Bitmap Process(Bitmap image, BlurCommand command)
        {
            var blurred = new Bitmap(image.Width, image.Height);
            var rectangle = new Rectangle(0, 0, image.Width, image.Height);

            using (var graphics = Graphics.FromImage(blurred))
            {
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }

            for (var xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (var yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    var blurPixelCount = 0;

                    for (var x = xx; (x < xx + command.Size && x < image.Width); x++)
                    {
                        for (var y = yy; (y < yy + command.Size && y < image.Height); y++)
                        {
                            var pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR /= blurPixelCount;
                    avgG /= blurPixelCount;
                    avgB /= blurPixelCount;

                    for (var x = xx; x < xx + command.Size && x < image.Width && x < rectangle.Width; x++)
                    {
                        for (var y = yy; y < yy + command.Size && y < image.Height && y < rectangle.Height; y++)
                        {
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                        }
                    }
                }
            }

            return blurred;
        }
    }
}
