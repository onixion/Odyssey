using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Registration builder.
    /// </summary>
    public class RegistrationBuilder
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RegistrationBuilder()
        {
        }

        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <returns>Registration builder.</returns>
        public static RegistrationBuilder New()
        {
            return new RegistrationBuilder();
        }

        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; set; }

        /// <summary>
        /// Implemetnation type.
        /// </summary>
        public Type ImplementationType { get; set; }

        /// <summary>
        /// Create on resolve.
        /// </summary>
        public bool CreateOnResolve { get; set; } = false;

        /// <summary>
        /// Instance.
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameter injections.
        /// </summary>
        IList<ParameterInjection> parameterInjections = new List<ParameterInjection>();

        /// <summary>
        /// Add parameter injection.
        /// </summary>
        /// <param name="parameterInjection">Parameter injection.</param>
        /// <returns>Registration builder.</returns>
        public RegistrationBuilder AddParameterInjection(ParameterInjection parameterInjection)
        {
            parameterInjections.Add(parameterInjection);
            return this;
        }

        /// <summary>
        /// Property injections.
        /// </summary>
        IList<PropertyInjection> propertyInjections = new List<PropertyInjection>();

        /// <summary>
        /// Property injection.
        /// </summary>
        /// <param name="propertyInjection">Property injection.</param>
        /// <returns>Registrations builder.</returns>
        public RegistrationBuilder AddPropertyInjection(PropertyInjection propertyInjection)
        {
            propertyInjections.Add(propertyInjection);
            return this;
        }

        /// <summary>
        /// Builds registration.
        /// </summary>
        /// <returns>Registration.</returns>
        public Registration Build()
        {
            return new Registration(
                InterfaceType, 
                ImplementationType, 
                CreateOnResolve, 
                Instance, 
                Name, 
                parameterInjections.ToArray(), 
                propertyInjections.ToArray());
        }
    }
}
