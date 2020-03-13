using Odyssey.Builders;
using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Extensions
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
            IEnumerable<ParameterInjection> parameterInjections = null,
            IEnumerable<PropertyInjection> propertyInjections = null) where TImplementation : TInterface
        {
            var builder = new RegistrationBuilder()
            {
                InterfaceType = typeof(TInterface),
                ImplementationType = typeof(TImplementation),
                Name = name,
                Injections = new InjectionsBuilder
                {
                    ConstructorInjection = new ConstructorInjectionBuilder
                    {
                        Injections = parameterInjections,
                    }
                    .Build(),

                    PropertyInjections = new PropertyInjectionsBuilder
                    {
                        Injections = propertyInjections,
                    }
                    .Build()
                }
                .Build()
            };

            registrationsBuilder.Register(builder.Build());
            return registrationsBuilder;
        }

        /// <summary>
        /// Register instance.
        /// </summary>
        /// <typeparam name="TInterface">Interface type.</typeparam>
        /// <param name="registrationsBuilder">Registrations builder.</param>
        /// <param name="instance">Instance.</param>
        /// <param name="name">Name.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <returns>Registrations builder.</returns>
        public static RegistrationsBuilder RegisterInstance<TInterface>(
            this RegistrationsBuilder registrationsBuilder,
            object instance,
            string name = null,
            IEnumerable<PropertyInjection> propertyInjections = null)
        {
            var builder = new RegistrationBuilder()
            {
                InterfaceType = typeof(TInterface),
                Name = name,
                Instance = instance,
                Injections = new InjectionsBuilder
                {
                    PropertyInjections = new PropertyInjectionsBuilder
                    {
                        Injections = propertyInjections,
                    }
                    .Build()
                }
                .Build()
            };

            registrationsBuilder.Register(builder);
            return registrationsBuilder;
        }

        /// <summary>
        /// Register service that will be created on resolve.
        /// </summary>
        /// <typeparam name="TInterface">Interface type.</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="registrationsBuilder">Registrations builder.</param>
        /// <returns>Registrations builder.</returns>
        public static RegistrationsBuilder RegisterOnResolve<TInterface, TImplementation>(this RegistrationsBuilder registrationsBuilder)
        {
            var builder = new RegistrationBuilder()
            {
                InterfaceType = typeof(TInterface),
                ImplementationType = typeof(TImplementation),
            };

            registrationsBuilder.Register(builder);
            return registrationsBuilder;
        }

        /// <summary>
        /// Registrate builder.
        /// </summary>
        /// <param name="registrationsBuilder">Registrations builder.</param>
        /// <param name="builder">Registration builder.</param>
        /// <returns>Registrations builder.</returns>
        public static RegistrationsBuilder Register(this RegistrationsBuilder registrationsBuilder, RegistrationBuilder builder)
        {
            return registrationsBuilder.Register(builder.Build());
        }

        /// <summary>
        /// Registrate registrations.
        /// </summary>
        /// <param name="registrationsBuilder">Registrations builder.</param>
        /// <param name="registrations">Registrations.</param>
        /// <returns>Registrations builder.</returns>
        public static RegistrationsBuilder Register(
            this RegistrationsBuilder registrationsBuilder, 
            IEnumerable<Registration> registrations)
        {
            foreach(Registration registration in registrations)
                return registrationsBuilder.Register(registration);

            return registrationsBuilder;
        }

        /// <summary>
        /// Registrate registrations.
        /// </summary>
        /// <param name="registrationsBuilder">Registrations builder.</param>
        /// <param name="registrationBuilders">Registration builders.</param>
        /// <returns>Registrations builder.</returns>
        public static RegistrationsBuilder Register(
            this RegistrationsBuilder registrationsBuilder, 
            IEnumerable<RegistrationBuilder> registrationBuilders)
        {
            foreach (RegistrationBuilder registrationBuilder in registrationBuilders)
                return registrationsBuilder.Register(registrationBuilder);

            return registrationsBuilder;
        }
    }
}
