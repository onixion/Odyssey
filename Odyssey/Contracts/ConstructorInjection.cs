using GroundWork.Core;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Constructor injections.
    /// </summary>
    public class ConstructorInjection
    {
        /// <summary>
        /// Injections.
        /// </summary>
        public IEnumerable<ParameterInjection> Injections { get; }

        /// <summary>
        /// Injection behaviour.
        /// </summary>
        public InjectionBehaviour InjectionBehaviour { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="injections">Parameter injections.</param>
        /// <param name="injectionBehaviour">Injections behaviour.</param>
        public ConstructorInjection(IEnumerable<ParameterInjection> injections, InjectionBehaviour injectionBehaviour = InjectionBehaviour.Default)
        {
            Argument.NotNull(nameof(injections), injections);

            Injections = injections.ToArray();
            InjectionBehaviour = injectionBehaviour;
        }
    }
}
