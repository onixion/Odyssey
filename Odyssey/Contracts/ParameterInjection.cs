using GroundWork;
using GroundWork.Contracts;
using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Parameter injection.
    /// </summary>
    public class ParameterInjection : ICloneable
    {
        /// <summary>
        /// Name of parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value.
        /// </summary>
        public IOptional<object> Value { get; } = new Optional<object>();

        /// <summary>
        /// Resolution.
        /// </summary>
        public IOptional<Resolution> Resolution { get; } = new Optional<Resolution>();

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
            Value = new Optional<object>(value);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="resolution">Resolution.</param>
        public ParameterInjection(string name, Resolution resolution)
        {
            Argument.NotNull(nameof(name), name);
            Argument.NotNull(nameof(resolution), resolution);

            Name = name;
            Resolution = new Optional<Resolution>(resolution);
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
