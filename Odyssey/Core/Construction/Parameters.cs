using Odyssey.Contracts;
using Odyssey.Core.Builders;
using Odyssey.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Core.Construction.Parameters
{
    /// <summary>
    /// Registration parameters.
    /// </summary>
    public class Parameters : IParameters
    {
        /// <summary>
        /// Get parameters.
        /// </summary>
        /// <param name="typeRegistration">Type registration.</param>
        /// <param name="container">Container.</param>
        /// <param name="resolution">Resolution.</param>
        /// <param name="serviceInstance">Service instance.</param>
        /// <returns>Parameters.</returns>
        public object[] GetParameters(TypeRegistration typeRegistration, IContainer container, Resolution resolution = null, object serviceInstance = null)
        {
            var parameterInfos = serviceInstance == null ?
                typeRegistration.RuntimeMetaData.ConstructorInfo.GetParameters() :
                typeRegistration.RuntimeMetaData.DecoratorConstructorInfo.GetParameters();

            var parameters = new object[parameterInfos.Length];

            // Get parameter injections.
            var parameterInjections = resolution != null ? 
                resolution.ParameterInjections :
                typeRegistration.Registration.ParameterInjections;

            // This loop will first try to inject a named parameter value (if there is one).
            // If there is none, it will try to resolve the parameter if it has the resolve attribute attached.
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                var parameterInfo = parameterInfos[i];

                // If service instance is set and the parameter type is 
                // interface type, then set it (used for decorator injection).
                if (serviceInstance != null && typeRegistration.Registration.InterfaceType.IsAssignableFrom(parameterInfo.ParameterType))
                {
                    parameters[i] = serviceInstance;
                    continue;
                }

                // If a named parameter injection is found, use it.
                var namedParameterInjection = parameterInjections
                    .Where(injection => injection.Name != null)
                    .FirstOrDefault(injection => injection.Name == parameterInfo.Name);

                if (namedParameterInjection != null)
                {
                    parameters[i] = namedParameterInjection.Value;
                    // CHECK TYPE THEN THROW
                    continue;
                }

                // If the parameter has the attribute, resolve it.
                if (parameterInfo.CustomAttributes.Any(customAttributeData => customAttributeData.AttributeType == typeof(Resolve)))
                {
                    // If the parameter type is IContainer just set it directly.
                    if (typeof(IContainer).IsAssignableFrom(parameterInfo.ParameterType))
                    {
                        parameters[i] = container;
                    }
                    // Else resolve it.
                    else
                    {
                        parameters[i] = container.Resolve(new ResolutionBuilder
                        {
                            InterfaceType = parameterInfo.ParameterType,
                        }
                        .Build());
                    }

                    continue;
                }
            }

            Queue<ParameterInjection> unnamedParameterInjections = new Queue<ParameterInjection>(parameterInjections
                .Where(parameterInjection => parameterInjection.Name == null));

            // This loop will try the best to set the remaining parameters.
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] != null) continue;

                if (unnamedParameterInjections.Count == 0)
                    throw new InstantiationException();

                parameters[i] = unnamedParameterInjections.Dequeue().Value;
                // CHEKC TYPE THEN THROW
            }

            return parameters;
        }
    }
}
