using Odyssey.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Builders
{
    /// <summary>
    /// Constructor injection builder.
    /// </summary>
    public class ConstructorInjectionBuilder
    {
        /// <summary>
        /// Parameter injections.
        /// </summary>
        public IEnumerable<ParameterInjection> Injections { get; set; }

        readonly IList<ParameterInjection> injections = new List<ParameterInjection>();

        /// <summary>
        /// Add parameter injection.
        /// </summary>
        /// <param name="parameterInjection"></param>
        /// <returns>Constructor injection builder.</returns>
        public ConstructorInjectionBuilder AddParameterInjection(ParameterInjection parameterInjection)
        {
            injections.Add(parameterInjection);
            return this;
        }

        /// <summary>
        /// Injection behaviour.
        /// </summary>
        public InjectionBehaviour InjectionBehaviour { get; set; }

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Constructor injection.</returns>
        public ConstructorInjection Build()
        {
            return new ConstructorInjection(
                Injections != null ? Injections.Concat(injections) : injections,
                InjectionBehaviour);
        }
    }
}
