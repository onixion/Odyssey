using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Parameter injections.
    /// </summary>
    public class ParameterInjections
    {
        /// <summary>
        /// Try to resolve parameters.
        /// </summary>
        /// <remarks>
        /// When set to true, when resolving the parameters,
        /// missing parameters will be set to null.
        /// </remarks>
        public bool TryResolveParameters { get; }

        /// <summary>
        /// Injections.
        /// </summary>
        public IEnumerable<ParameterInjection> Injections { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tryResolveParameters"></param>
        /// <param name="injections"></param>
        public ParameterInjections(bool tryResolveParameters, IEnumerable<ParameterInjection> injections)
        {
            TryResolveParameters = tryResolveParameters;
            Injections = injections.ToArray();
        }
    }
}
