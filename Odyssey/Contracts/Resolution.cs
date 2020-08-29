using GroundWork;
using GroundWork.Contracts;
using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Resolution.
    /// </summary>
    public class Resolution
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; }

        /// <summary>
        /// Name.
        /// </summary>
        public IOptional<string> Name { get; }

        /// <summary>
        /// Injections.
        /// </summary>
        public IOptional<Injections> Injections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="name"></param>
        /// <param name="injections"></param>
        public Resolution(
            Type interfaceType, 
            string name = null, 
            Injections injections = null)
        {
            Argument.NotNull(nameof(interfaceType), interfaceType);

            InterfaceType = interfaceType;
            Name = new Optional<string>(name);
            Injections = new Optional<Injections>(injections);
        }

        /// <summary>
        /// To string.
        /// </summary>
        public override string ToString()
        {
            return $"Resolution[Interface={InterfaceType},Name={Name.ValueOrDefault}]";
        }
    }
}
