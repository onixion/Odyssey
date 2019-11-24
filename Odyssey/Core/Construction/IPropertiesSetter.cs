using Odyssey.Contracts;
using System.Collections.Generic;
using System.Reflection;

namespace Odyssey.Core.Constructor
{
    /// <summary>
    /// Properties setter interface.
    /// </summary>
    public interface IPropertiesSetter
    {
        /// <summary>
        /// Set properties.
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <param name="properties">Properties.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <param name="container">Container.</param>
        void SetProperties(object instance, PropertyInfo[] properties, IEnumerable<PropertyInjection> propertyInjections, IContainer container);
    }
}
