using Odyssey.Contracts;
using Odyssey.Core.Builders;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Registrations builder.
    /// </summary>
    public class RegistrationsBuilder
    {
        /// <summary>
        /// Registrations.
        /// </summary>
        readonly IList<Registration> registrations = new List<Registration>();

        /// <summary>
        /// Reverse order.
        /// </summary>
        readonly bool reverseOrder;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="reverseOrder">Reverse order.</param>
        public RegistrationsBuilder(bool reverseOrder = false)
        {
            this.reverseOrder = reverseOrder;
        }

        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <param name="reverseOrder">Reverse order.</param>
        /// <returns>Registration builder.</returns>
        public static RegistrationsBuilder New(bool reverseOrder = false)
        {
            return new RegistrationsBuilder(reverseOrder);
        }

        /// <summary>
        /// Add registration.
        /// </summary>
        /// <param name="registration">Registration.</param>
        /// <returns>Registrations builder.</returns>
        public RegistrationsBuilder Register(Registration registration)
        {
            registrations.Add(registration);
            return this;
        }

        /// <summary>
        /// Register.
        /// </summary>
        /// <typeparam name="TInterface">Interface type.</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="name">Name.</param>
        /// <param name="parameterInjections">Parameter injections.</param>
        /// <param name="propertyInjections">Property injections.</param>
        /// <returns>Registrations builder.</returns>
        public RegistrationsBuilder Register<TInterface, TImplementation>(
            string name = null,
            IEnumerable<ParameterInjection> parameterInjections = null,
            IEnumerable<PropertyInjection> propertyInjections = null) where TImplementation : TInterface
        {
            Registration registration = new RegistrationBuilder()
                .SetInterfaceType(typeof(TInterface))
                .SetImplementationType(typeof(TImplementation))
                .AddParameterInjections(parameterInjections)
                .AddPropertyInjections(propertyInjections)
                .Build();

            registrations.Add(registration);
            return this;
        }

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Registrations.</returns>
        public IEnumerable<Registration> Build()
        {
            return reverseOrder ? registrations.Reverse().ToArray() : registrations.ToArray();
        }
    }
}
