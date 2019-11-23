using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Registration builder extensions.
    /// </summary>
    public static class RegistrationBuilderExtensions
    {
        /// <summary>
        /// Add parameter injections.
        /// </summary>
        /// <param name="registrationBuilder">Registration builder.</param>
        /// <param name="parameterInjections">Parameter injections.</param>
        /// <returns>Registration builder.</returns>
        public static RegistrationBuilder AddParameterInjections(this RegistrationBuilder registrationBuilder, IEnumerable<ParameterInjection> parameterInjections)
        {
            foreach (ParameterInjection parameterInjection in parameterInjections)
                registrationBuilder.AddParameterInjection(parameterInjection);

            return registrationBuilder;
        }

        /// <summary>
        /// Add property injections.
        /// </summary>
        /// <param name="registrationBuilder">Registration builder.</param>
        /// <param name="propertyInjections">Parameter injections.</param>
        /// <returns>Registration builder.</returns>
        public static RegistrationBuilder AddPropertyInjections(this RegistrationBuilder registrationBuilder, IEnumerable<PropertyInjection> propertyInjections)
        {
            foreach (PropertyInjection propertyInjection in propertyInjections)
                registrationBuilder.AddPropertyInjection(propertyInjection);

            return registrationBuilder;
        }
    }
}
