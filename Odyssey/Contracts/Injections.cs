using Utilinator.Core;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Injections.
    /// </summary>
    public class Injections
    {
        /// <summary>
        /// Constructor injection.
        /// </summary>
        public Optional<ConstructorInjection> ConstructorInjection { get; }

        /// <summary>
        /// Property injections.
        /// </summary>
        public Optional<PropertyInjections> PropertyInjections { get; }

        /// <summary>
        /// Deocrator injection.
        /// </summary>
        public Optional<DecoratorRegistration> DecoratorInjection { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="constructorInjection"></param>
        /// <param name="propertyInjections"></param>
        /// <param name="decoratorInjection"></param>
        public Injections(ConstructorInjection constructorInjection = null, PropertyInjections propertyInjections = null, DecoratorRegistration decoratorInjection = null)
        {
            ConstructorInjection = constructorInjection ?? new Optional<ConstructorInjection>();
            PropertyInjections = propertyInjections ?? new Optional<PropertyInjections>();
            DecoratorInjection = decoratorInjection ?? new Optional<DecoratorRegistration>();
        }
    }
}
