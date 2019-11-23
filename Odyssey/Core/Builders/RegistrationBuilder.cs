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
        /// Create on resolve.
        /// </summary>
        bool createOnResolve;

        /// <summary>
        /// Set lifetime.
        /// </summary>
        /// <param name="createOnResolve">Create on resolve.</param>
        /// <returns>Registration builder.</returns>
        /// <remarks>
        /// Is optional and default is Lifetime.CreateOnce.
        /// </remarks>
        public RegistrationBuilder SetCreateOnResolve(bool createOnResolve)
        {
            this.createOnResolve = createOnResolve;
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
            return new Registration(interfaceType, implementationType, createOnResolve, instance, name, parameterInjections.ToArray(), propertyInjections.ToArray());
        }
    }
}
