
using GroundWork;
using GroundWork.Contracts;
using Odyssey.Core;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Injections.
    /// </summary>
    public class Injections : IInjections
    {
        /// <summary>
        /// Constructor injection.
        /// </summary>
        public IOptional<ConstructorInjection> ConstructorInjection { get; }

        /// <summary>
        /// Deocrator injection.
        /// </summary>
        public IOptional<DecoratorRegistration> DecoratorInjection { get; }

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
