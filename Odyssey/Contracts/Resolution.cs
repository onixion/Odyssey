using System;
using Utilinator.Core;

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
        public Optional<string> Name { get; }

        /// <summary>
        /// Injections.
        /// </summary>
        public Optional<Injections> Injections { get; }

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
            Name = name;
            Injections = injections;
        }
    }
}
