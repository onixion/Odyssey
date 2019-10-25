using SmartContainer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartContainer.Core.Builders
{
    /// <summary>
    /// Registration builder.
    /// </summary>
    public class RegistrationBuilder
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        Type interfaceType;

        /// <summary>
        /// Set interface type.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns>Registration builder.</returns>
        /// <remarks>
        /// Is required.
        /// </remarks>
        public RegistrationBuilder SetInterfaceType(Type interfaceType)
        {
            this.interfaceType = interfaceType;
            return this;
        }

        /// <summary>
        /// Implemetnation type.
        /// </summary>
        Type implementationType;

        /// <summary>
        /// Set implementation type.
        /// </summary>
        /// <param name="implementationType"></param>
        /// <returns>Registration builder.</returns>
        /// <remarks>
        /// Is required.
        /// </remarks>
        public RegistrationBuilder SetImplementationType(Type implementationType)
        {
            this.implementationType = implementationType;
            return this;
        }

        /// <summary>
        /// Life time.
        /// </summary>
        Lifetime? lifetime;

        /// <summary>
        /// Set lifetime.
        /// </summary>
        /// <param name="lifetime">Life time.</param>
        /// <returns>Registration builder.</returns>
        /// <remarks>
        /// Is optional and default is Lifetime.CreateOnce.
        /// </remarks>
        public RegistrationBuilder SetLifetime(Lifetime lifetime)
        {
            this.lifetime = lifetime;
            return this;
        }

        /// <summary>
        /// Instance.
        /// </summary>
        object instance;

        /// <summary>
        /// Set instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Registration builder.</returns>
        /// <remarks>
        /// Is optional and only used when lifetime is Lifetime.CreatedOnce.
        /// </remarks>
        public RegistrationBuilder SetInstance(object instance)
        {
            this.instance = instance;
            return this;
        }

        /// <summary>
        /// Name.
        /// </summary>
        string name;

        /// <summary>
        /// Set name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// Is optional and used to resolve services which have the same interface type.
        /// </remarks>
        public RegistrationBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Parameter injections.
        /// </summary>
        IList<ParameterInjection> parameterInjections = new List<ParameterInjection>();

        /// <summary>
        /// Add parameter injection.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        /// <param name="value">Value of parameter.</param>
        /// <returns></returns>
        /// <remarks>
        /// Is optional and only supported when life time is set to Lifetime.CreateOnce.
        /// </remarks>
        public RegistrationBuilder AddParameterInjection(string parameterName, object value)
        {
            parameterInjections.Add(new ParameterInjection(parameterName, null, value));
            return this;
        }

        /// <summary>
        /// Property injections.
        /// </summary>
        IList<PropertyInjection> propertyInjections = new List<PropertyInjection>();

        /// <summary>
        /// Add property injection.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Value of the property.</param>
        /// <returns></returns>
        /// <remarks>
        /// Is optional and only supported when life time is set to Lifetime.CreateOnce.
        /// </remarks>
        public RegistrationBuilder AddPropertyInjection(string propertyName, object value)
        {
            propertyInjections.Add(new PropertyInjection(propertyName, null, value));
            return this;
        }

        /// <summary>
        /// Builds registration.
        /// </summary>
        /// <returns>Registration.</returns>
        public Registration Build()
        {
            return new Registration(interfaceType, implementationType, lifetime, instance, name, parameterInjections.ToArray(), propertyInjections.ToArray());
        }
    }
}
