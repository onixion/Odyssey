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
        public Type InterfaceType { get; set; }

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
            return new Resolution(InterfaceType, Name, parameterInjections.ToArray(), propertyInjections.ToArray());
        }
    }
}
