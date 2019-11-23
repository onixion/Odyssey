using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Resolution builder.
    /// </summary>
    public class ResolutionBuilder 
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        Type interfaceType;

        /// <summary>
        /// Set interface type.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns>Resolution builder.</returns>
        /// <remarks>
        /// Is required.
        /// </remarks>
        public ResolutionBuilder SetInterfaceType(Type interfaceType)
        {
            this.interfaceType = interfaceType;
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
        /// <returns>Resolution builder.</returns>
        /// <remarks>
        /// Is optional and used to resolve services which have the same interface type.
        /// </remarks>
        public ResolutionBuilder SetName(string name)
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
        /// <returns>Resolution builder.</returns>
        /// <remarks>
        /// Is optional and only supported when life time is set to Lifetime.CreateOnce.
        /// </remarks>
        public ResolutionBuilder AddParameterInjection(ParameterInjection parameterInjection)
        {
            parameterInjections.Add(parameterInjection);
            return this;
        }

        /// <summary>
        /// Property injections.
        /// </summary>
        IList<PropertyInjection> propertyInjections = new List<PropertyInjection>();

        /// <summary>
        /// Add property injection.
        /// </summary>
        /// <param name="propertyInjection">Property injection.</param>
        /// <returns>Resolution builder</returns>
        /// <remarks>
        /// Is optional and only supported when life time is set to Lifetime.CreateOnce.
        /// </remarks>
        public ResolutionBuilder AddPropertyInjection(PropertyInjection propertyInjection)
        {
            propertyInjections.Add(propertyInjection);
            return this;
        }

        /// <summary>
        /// Builds registration.
        /// </summary>
        /// <returns>Registration.</returns>
        public Resolution Build()
        {
            return new Resolution(interfaceType, name, parameterInjections.ToArray(), propertyInjections.ToArray());
        }
    }
}
