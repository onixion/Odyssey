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
        /// Type of parameter.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        public ParameterInjection(object value)
        {
            Value = value;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public ParameterInjection(string name, Type type, object value)
        {
            Name = name;
            Type = type;
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
