using Odyssey.Contracts;
using Odyssey.Core.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Odyssey.Core.Constructor
{
    /// <summary>
    /// Properties setter.
    /// </summary>
    public class PropertiesSetter : IPropertiesSetter
    {
        /// <summary>
        /// Set properties.
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <param name="properties">Properties.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <param name="container">Container.</param>
        public void SetProperties(object instance, PropertyInfo[] properties, IEnumerable<PropertyInjection> propertyInjections, IContainer container)
        {
            foreach (PropertyInfo propertyInfo in properties)
            {
                var propertyInjection = propertyInjections
                    .Where(injection => injection.Name != null)
                    .FirstOrDefault(injection => injection.Name == propertyInfo.Name);

                // If there is a property injection defined, set the value.
                if (propertyInjection != null)
                {
                    propertyInfo.SetValue(instance, propertyInjection.Value, null);
                    continue;
                }

                // If the property has the attribute "Resolve" then resolve it.
                if (propertyInfo.CustomAttributes.Any(customAttributeData => customAttributeData.AttributeType == typeof(Resolve)))
                {
                    Resolution resolution = new ResolutionBuilder
                    {
                        InterfaceType = propertyInfo.PropertyType,
                        // TODO Name = propertyInfo.Name,
                    }
                    .Build();

                    propertyInfo.SetValue(instance, container.Resolve(resolution), null);
                }
            }
        }
    }
}
