using System;

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
        /// Type of property.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public PropertyInjection(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public PropertyInjection(string name, Type type, object value)
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
