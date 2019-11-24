using Odyssey.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Registration.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Interface type.
        /// </summary>
        public Type InterfaceType { get; }

        /// <summary>
        /// Implementation type.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// Create on resolve.
        /// </summary>
        public bool CreateOnResolve { get; }
  
        /// <summary>
        /// Instance.
        /// </summary>
        public object Instance { get; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Parameter injections.
        /// </summary>
        public ParameterInjection[] ParameterInjections { get; }

        /// <summary>
        /// Property injections.
        /// </summary>
        public PropertyInjection[] PropertyInjections { get; }

        /// <summary>
        /// Decorator injection type.
        /// </summary>
        public Type DecoratorInjectionType { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="createOnResolve"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="parameterInjections"></param>
        /// <param name="propertyInjections"></param>
        public Registration(
            Type interfaceType, 
            Type implementationType = null, 
            bool createOnResolve = false,
            object instance = null, 
            string name = null,
            IEnumerable<ParameterInjection> parameterInjections = null,
            IEnumerable<PropertyInjection> propertyInjections = null,
            Type decoratorInjectionType = null)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
            CreateOnResolve = createOnResolve;

            // When create on resolve is true, many other fields make no sense.
            // This if will prevent all those cases from beeing possible.
            if (createOnResolve)
            {
                if (instance != null)
                    throw new RegisterException($"Registration can't define an instance when CreateOnResolve is true.");

                if (name != null)
                    throw new RegisterException($"Registration can't define an name when CreateOnResolve is true.");

                if (parameterInjections != null && parameterInjections.Any())
                    throw new RegisterException($"Registration can't define parameter injections when CreateOnResolve is true.");

                if (propertyInjections != null && propertyInjections.Any())
                    throw new RegisterException($"Registration can't define property injections when CreateOnResolve is true.");

                if (!InterfaceType.IsAssignableFrom(ImplementationType))
                    throw new RegisterException($"{InterfaceType.FullName} is not assignable from {ImplementationType.FullName}.");

                if (decoratorInjectionType != null)
                    throw new RegisterException($"Registration can't define the decorator injection type {decoratorInjectionType.FullName}.");
            }
            else
            {
                Instance = instance;
                Name = name;

                if (Instance != null)
                {
                    if (ImplementationType != null)
                        throw new RegisterException($"Registration can't define an instance when an implementation type is defined.");

                    if (decoratorInjectionType != null)
                        throw new RegisterException($"Registration can't define the decorator injection type {decoratorInjectionType.FullName}.");
                }
                else
                {
                    if (ImplementationType == null)
                        throw new RegisterException($"Registration does not define an implementation type.");

                    if (!InterfaceType.IsAssignableFrom(ImplementationType))
                        throw new RegisterException($"{InterfaceType.FullName} is not assignable from {ImplementationType.FullName}.");
                }

                ParameterInjections = parameterInjections != null ? parameterInjections.ToArray() : new ParameterInjection[0];
                PropertyInjections = propertyInjections != null ? propertyInjections.ToArray() : new PropertyInjection[0];
                DecoratorInjectionType = decoratorInjectionType;
            }
        }
    }
}
