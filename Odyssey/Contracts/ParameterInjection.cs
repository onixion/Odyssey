using Odyssey.Utils;
using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Parameter injection.
    /// </summary>
    public class ParameterInjection : ICloneable
    {
        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Name of parameter.
        /// </summary>
        /// <remarks>
        /// Is optional. Just a hint for the container.
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="name">Name.</param>
        public ParameterInjection(object value, string name = null)
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
