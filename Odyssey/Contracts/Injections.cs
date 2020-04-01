
using GroundWork.Core;

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
        /// Deocrator injection.
        /// </summary>
        public Optional<DecoratorRegistration> DecoratorInjection { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="constructorInjection"></param>
        /// <param name="decoratorInjection"></param>
        public Injections(ConstructorInjection constructorInjection = null, DecoratorRegistration decoratorInjection = null)
        {
            ConstructorInjection = new Optional<ConstructorInjection>(constructorInjection);
            DecoratorInjection = new Optional<DecoratorRegistration>(decoratorInjection);
        }
    }
}
