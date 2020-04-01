using Odyssey.Builders;
using Odyssey.Contracts;
using System;
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
        /// <param name="registrationBuilder">Registration builder.</param>
        /// <returns></returns>
        public static IContainer CreateChild(this IContainer container, RegistrationsBuilder registrationBuilder)
        {
            return container.CreateChild(registrationBuilder.Build());
        }

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
            IEnumerable<ParameterInjection> parameterInjections = null)
        {
            var resolutionBuilder = new ResolutionBuilder
            {
                InterfaceType = typeof(TInterface),
            };

            if (name != null)
                resolutionBuilder.Name = name;

            if (parameterInjections != null)
            {
                // TODO
                //resolutionBuilder.AddParameterInjections(parameterInjections);
            }

            return (TInterface) container.Resolve(resolutionBuilder.Build());
        }

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="name">Name.</param>
        /// <param name="parameterInjections">Parameter injections.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <returns></returns>
        public static object Resolve(
            this IContainer container,
            Type interfaceType,
            string name = null,
            IEnumerable<ParameterInjection> parameterInjections = null)
        {
            var resolutionBuilder = new ResolutionBuilder
            {
                InterfaceType = interfaceType,
                Name = name,
            };

            if (parameterInjections != null)
            {
                //foreach (ParameterInjection parameterInjection in parameterInjections)
                //    resolutionBuilder.AddParameterInjection(parameterInjection);
            }

            return container.Resolve(resolutionBuilder);
        }

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="container">Container.</param>
        /// <param name="resolutionBuilder">Resolution builder.</param>
        /// <returns>Service.</returns>
        public static object Resolve(this IContainer container, ResolutionBuilder resolutionBuilder)
        {
            return container.Resolve(resolutionBuilder.Build());
        }
    }
}
