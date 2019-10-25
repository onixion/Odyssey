using SmartContainer.Contracts;
using SmartContainer.Core.Builders;
using SmartContainer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Odyssey.Core
{
    /// <summary>
    /// Runtime meta data.
    /// </summary>
    public class RuntimeMetaData
    {
        /// <summary>
        /// Property infos.
        /// </summary>
        public PropertyInfo[] PropertyInfos { get; }

        /// <summary>
        /// Constructor info.
        /// </summary>
        public ConstructorInfo ConstructorInfo { get; }

        /// <summary>
        /// Instance.
        /// </summary>
        public object Instance { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registration">Registration.</param>
        public RuntimeMetaData(Registration registration)
        {
            PropertyInfos = registration.InterfaceType.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.SetProperty);
        }

        /// <summary>
        /// Find suitable constructor.
        /// </summary>
        /// <param name="implementationType"></param>
        /// <param name="parameterInjections"></param>
        /// <returns></returns>
        ConstructorInfo FindConstructor(Type implementationType, IEnumerable<ParameterInjection> parameterInjections)
        {
            foreach(ConstructorInfo constructorInfo in implementationType.GetConstructors())
            {
                
                var parameters = constructorInfo.GetParameters();

                // TODO
                
            }

            throw new RegisterException($"Could not find a suitable constructor.");
        }

        /// <summary>
        /// Creates the service.
        /// </summary>
        /// <param name="registration">Registration.</param>
        /// <returns>Service instance.</returns>
        object CreateServiceInstance(Registration registration)
        {
            return Activator.CreateInstance(
                registration.ImplementationType, 
                registration.ParameterInjections.Select(par => par.Value).ToArray());
        }

        /// <summary>
        /// Inject properties.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="registration"></param>
        void InjectProperties(object instance, Registration registration, IResolver resolver)
        {
            // TODO: optimize this
            foreach(PropertyInfo propertyInfo in PropertyInfos)
            {
                Resolution resolution = new ResolutionBuilder()
                    .SetInterfaceType(propertyInfo.PropertyType)
                    //.SetName()
                    .Build();

                propertyInfo.SetValue(instance, resolver.Resolve(resolution));
            }
        }
    }
}
