using Odyssey.Contracts;
using Odyssey.Core.Builders;
using System.Collections.Generic;

namespace Odyssey.Extensions
{
    /// <summary>
    /// Resolver extensions.
    /// </summary>
    public static class ResolverExtensions
    {
        /// <summary>
        /// Resolve.
        /// </summary>
        /// <typeparam name="TInterface">Interface to resolve.</typeparam>
        /// <param name="resolver">Resolver.</param>
        /// <param name="name">Name.</param>
        /// <param name="parameterInjections">Parameter injections.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <returns>Service.</returns>
        public static TInterface Resolve<TInterface>(
            this IResolver resolver, 
            string name = null, 
            IEnumerable<ParameterInjection> parameterInjections = null,
            IEnumerable<PropertyInjection> propertyInjections = null)
        {
            var resolutionBuilder = new ResolutionBuilder()
                .SetInterfaceType(typeof(TInterface))
                .SetName(name ?? "");

            if (parameterInjections != null)
            {
                foreach (ParameterInjection parameterInjection in parameterInjections)
                    resolutionBuilder.AddParameterInjection(parameterInjection);
            }

            if (propertyInjections != null)
            {
                foreach (PropertyInjection propertyInjection in propertyInjections)
                    resolutionBuilder.AddPropertyInjection(propertyInjection);
            }

            return (TInterface) resolver.Resolve(resolutionBuilder.Build());
        }
    }
}
