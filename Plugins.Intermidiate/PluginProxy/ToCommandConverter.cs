using System.Collections.Generic;
using Newtonsoft.Json;

namespace Plugins.Intermediate.PluginProxy
{
    internal static class ToCommandConverter
    {
        public static T ToCommand<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var json = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
