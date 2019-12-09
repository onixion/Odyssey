using Odyssey.Contracts;
using Odyssey.Core.Builders;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Extensions
{
    /// <summary>
    /// Resolver extensions.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="container">Container.</param>
        /// <param name="registrationBuilders">Registration builders.</param>
        /// <returns></returns>
        public static IContainer CreateChild(this IContainer container, IEnumerable<RegistrationBuilder> registrationBuilders)
        {
            return container.CreateChild(registrationBuilders.Select(builder => builder.Build()));
        }

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <typeparam name="TInterface">Interface to resolve.</typeparam>
        /// <param name="container">Container.</param>
        /// <param name="name">Name.</param>
        /// <param name="parameterInjections">Parameter injections.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <returns>Service.</returns>
        public static TInterface Resolve<TInterface>(
            this IContainer container, 
            string name = null, 
            IEnumerable<ParameterInjection> parameterInjections = null,
            IEnumerable<PropertyInjection> propertyInjections = null)
        {
            var resolutionBuilder = new ResolutionBuilder
            {
                InterfaceType = typeof(TInterface),
            };

            if (name != null)
                resolutionBuilder.Name = name;

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

            return (TInterface) container.Resolve(resolutionBuilder.Build());
        }
    }
}
