using Odyssey.Contracts;
using System;

namespace Odyssey.Builders
{
    /// <summary>
    /// Registration builder.
    /// </summary>
    public class RegistrationBuilder
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; set; }

        /// <summary>
        /// Implemetnation type.
        /// </summary>
        public Type ImplementationType { get; set; }

        /// <summary>
        /// Create on resolve.
        /// </summary>
        public bool CreateOnResolve { get; set; } = false;

        /// <summary>
        /// Instance.
        /// </summary>
        public object Instance { get; set; }

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
        public Registration Build()
        {
            return new Registration(
                InterfaceType, 
                ImplementationType, 
                CreateOnResolve, 
                Instance, 
                Name,
                Injections);
        }
    }
}
