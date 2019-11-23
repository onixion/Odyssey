using Odyssey.Utils;
using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Property injection.
    /// </summary>
    public class PropertyInjection : ICloneable
    {
        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Name of property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="name">Name.</param>
        public PropertyInjection(object value, string name = null)
        {
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
