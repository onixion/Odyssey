using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Builders
{
    /// <summary>
    /// Resolution builder.
    /// </summary>
    public class ResolutionBuilder 
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Injections.
        /// </summary>
        public Injections Injections { get; set; }

        /// <summary>
        /// Builds registration.
        /// </summary>
        /// <returns>Registration.</returns>
        public Resolution Build()
        {
            return new Resolution(InterfaceType, Name, Injections);
        }
    }
}
