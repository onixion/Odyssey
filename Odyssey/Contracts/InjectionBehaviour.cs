using System;
using System.Collections.Generic;
using System.Text;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Injection behaviour.
    /// </summary>
    public enum InjectionBehaviour
    {
        /// <summary>
        /// Default behaviour.
        /// </summary>
        /// <remarks>
        /// Resolve all injections necessary. If one can't be resolved,
        /// injection fails as a whole.
        /// </remarks>
        Default,

        /// <summary>
        /// Ignore failed resolving.
        /// </summary>
        /// <remarks>
        /// Tries to resolve all injections. If one or more can't be 
        /// resolved, they are simply set to 'null' or default value.
        /// </remarks>
        IgnoreUnknownDependencies,
    }
}
