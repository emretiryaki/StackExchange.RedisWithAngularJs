using System;
using System.Reflection;
using Castle.MicroKernel;

namespace Session.Redis
{
    public static class WindsorExtensions
    {
        public static void InjectProperties(this IKernel kernel, object target)
        {
            Type type = target.GetType();
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanWrite || !kernel.HasComponent(property.PropertyType)) continue;
                object value = kernel.Resolve(property.PropertyType);
                try
                {
                    property.SetValue(target, value, null);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Can't set property {0} on type {1}", property.Name, type.FullName);
                    throw new InvalidOperationException(message, ex);
                }
            }
        }
    }
}