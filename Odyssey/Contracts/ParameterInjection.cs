using GroundWork.Core;
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
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        public ParameterInjection(string name, object value)
        {
            Argument.NotNull(nameof(name), name);
            Argument.NotNull(nameof(value), value);

            Name = name;
            Value = value;
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
