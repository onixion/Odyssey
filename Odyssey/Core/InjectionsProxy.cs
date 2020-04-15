using GroundWork.Core;
using Odyssey.Contracts;

namespace Odyssey.Core
{
    /// <summary>
    /// Injections proxy.
    /// </summary>
    public class InjectionsProxy : IInjections
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
        /// <param name="registrationInjection">Registration injections.</param>
        /// <param name="resolutionInjection">Resolution injections.</param>
        public InjectionsProxy(IInjections registrationInjection = null, IInjections resolutionInjection = null)
        {
            
        }
    }
}
