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
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="name">Name.</param>
        public PropertyDependency(Type interfaceType = null, string name = null)
        {
            InterfaceType = interfaceType;
            Name = name;
        }
    }
}
