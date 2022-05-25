using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using Plugins.Api.Dto;
using Plugins.Intermediate;

namespace Plugins.Api.Services
{
    public class ImageProcessingService
    {
        private readonly IPluginProvider _pluginProvider;
        private readonly ConcurrentDictionary<Guid, Bitmap> _savedImages = new ConcurrentDictionary<Guid, Bitmap>();

        public ImageProcessingService(IPluginProvider pluginProvider)
        {
            _pluginProvider = pluginProvider;
        }

        public IEnumerable<string> Commands => _pluginProvider.Commands;

        public Guid Save(Bitmap image)
        {
            var id = Guid.NewGuid();
            _savedImages[id] = image;
            return id;
        }

        public bool TryProcessImage(Guid id, IReadOnlyList<Command> commands, out Bitmap image)
        {
            if (!_savedImages.TryGetValue(id, out image))
            {
                return false;
            }

            foreach (var command in commands)
            {
                image = _pluginProvider.Process(image, command.Name, command.Parameters);
            }

            _savedImages.TryRemove(id, out _);
            return true;
        }
    }
}