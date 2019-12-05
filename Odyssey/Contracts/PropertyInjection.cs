using System;
using Utilinator.Core;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Property injection.
    /// </summary>
    public class PropertyInjection : ICloneable
    {
        /// <summary>
        /// Name of property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        public PropertyInjection(string name, object value)
        {
            Argument.NotNull(nameof(name), name);
            Argument.NotNull(nameof(value), value);

            Value = value;
            Name = name;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>Cloned instance.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
