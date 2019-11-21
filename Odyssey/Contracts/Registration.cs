using Odyssey.Exceptions;
using System;

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
            Type implementationType, 
            bool createOnResolve = false,
            object instance = null, 
            string name = null,
            ParameterInjection[] parameterInjections = null,
            PropertyInjection[] propertyInjections = null)
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

                if (parameterInjections != null)
                    throw new RegisterException($"Registration can't define parameter injections when CreateOnResolve is true.");

                if (propertyInjections != null)
                    throw new RegisterException($"Registration can't define property injections when CreateOnResolve is true.");
            }
            else
            {
                Instance = instance;
                Name = name;

                if (parameterInjections != null)
                    ParameterInjections = (ParameterInjection[])parameterInjections.Clone();

                if (propertyInjections != null)
                    PropertyInjections = (PropertyInjection[])propertyInjections.Clone();
            }
        }
    }
}
