using System.Collections.Generic;

namespace Plugins.Intermediate.PluginProxy
{
    internal static class ToCommandConverter
    {
        public static T ToCommand<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var (name, value) in source)
            {
                someObjectType
                    .GetProperty(name)
                    ?.SetValue(someObject, value, null);
            }

            return someObject;
        }
    }
}
