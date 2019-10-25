using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Property dependency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyDependency : Attribute
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interfaceType">Interface type.</param>
        public PropertyDependency(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }
}
