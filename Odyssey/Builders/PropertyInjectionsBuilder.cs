using Odyssey.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Builders
{
    /// <summary>
    /// Property injections builder.
    /// </summary>
    public class PropertyInjectionsBuilder
    {
        /// <summary>
        /// Property injections.
        /// </summary>
        public IEnumerable<PropertyInjection> Injections { get; set; }

        readonly IList<PropertyInjection> injections = new List<PropertyInjection>();

        /// <summary>
        /// Add property injection.
        /// </summary>
        /// <param name="propertyInjection">Property injection.</param>
        /// <returns>Property injection builder.</returns>
        public PropertyInjectionsBuilder AddPropertyInjection(PropertyInjection propertyInjection)
        {
            injections.Add(propertyInjection);
            return this;
        }

        /// <summary>
        /// Injection behaviour.
        /// </summary>
        public InjectionBehaviour InjectionBehaviour { get; set; }

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Property injections.</returns>
        public PropertyInjections Build()
        {
            return new PropertyInjections(
                Injections != null ? Injections.Concat(injections) : injections, 
                InjectionBehaviour);
        }
    }
}
