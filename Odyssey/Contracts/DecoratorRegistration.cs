using System;
using Utilinator.Core;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Decorator registration.
    /// </summary>
    public class DecoratorRegistration
    {
        /// <summary>
        /// Implementation type.
        /// </summary>
        public Optional<Type> ImplementationType { get; }

        /// <summary>
        /// Injections.
        /// </summary>
        public Optional<Injections> Injections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DecoratorRegistration(Type implementationType = null, Injections injections = null)
        {
            ImplementationType = implementationType ?? new Optional<Type>();
            Injections = injections ?? new Optional<Injections>();
        }
    }
}
