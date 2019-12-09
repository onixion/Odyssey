using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Registrations builder extensions.
    /// </summary>
    public static class RegistrationsBuilderExtensions
    {
        /// <summary>
        /// Register.
        /// </summary>
        /// <typeparam name="TInterface">Interface type.</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="registrationsBuilder">Registrations builder.</param>
        /// <param name="name">Name.</param>
        /// <param name="parameterInjections">Parameter injections.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <returns>Registrations builder.</returns>
        public static RegistrationsBuilder Register<TInterface, TImplementation>(
            this RegistrationsBuilder registrationsBuilder,
            string name = null,
            object instance = null,
            IEnumerable<ParameterInjection> parameterInjections = null,
            IEnumerable<PropertyInjection> propertyInjections = null) where TImplementation : TInterface
        {
            var builder = new RegistrationBuilder()
            {
                InterfaceType = typeof(TInterface),
                ImplementationType = typeof(TImplementation),
                Name = name,
                Instance = instance,
            };

            if (parameterInjections != null)
                builder.AddParameterInjections(parameterInjections);

            if (propertyInjections != null)
                builder.AddPropertyInjections(propertyInjections);

            registrationsBuilder.Register(builder.Build());
            return registrationsBuilder;
        }
    }
}
