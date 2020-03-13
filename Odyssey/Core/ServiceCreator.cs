using Odyssey.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Odyssey.Core
{
    /// <summary>
    /// Service creator.
    /// </summary>
    public class ServiceCreator
    {
        /// <summary>
        /// Container.
        /// </summary>
        readonly IContainer container;

        /// <summary>
        /// Object info registry.
        /// </summary>
        readonly ObjectInfoRegistry objectInfoRegistry;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceCreator(IContainer container, ObjectInfoRegistry objectInfoRegistry)
        {
            this.container = container;
            this.objectInfoRegistry = objectInfoRegistry;
        }

        /// <summary>
        /// Create service.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="injections"></param>
        /// <param name="instance"></param>
        /// <returns>Service instace.</returns>
        public object CreateService(Type serviceType, Injections injections = null, object instance = null)
        {
            var objectInfo = objectInfoRegistry.GetObjectInfo(serviceType);

            var constructorArguments = GetConstructorArguments(objectInfo, injections, instance);

            object serviceInstance;

            try
            {
                serviceInstance = Activator.CreateInstance(serviceType, constructorArguments);
            }
            catch (Exception e)
            {
                // Todo
                throw e;
            }

            // Set properties.
            if (injections != null)
            {
                if (injections.PropertyInjections.HasValue)
                    SetPropertyInjections(objectInfo, serviceInstance, injections.PropertyInjections.Value);

                if (injections.DecoratorInjection.HasValue)
                {
                    serviceInstance = CreateService(
                        injections.DecoratorInjection.Value.ImplementationType.Value,
                        injections.DecoratorInjection.Value.Injections.ValueOrDefault,
                        serviceInstance);
                }
            }

            return serviceInstance;
        }

        private object[] GetConstructorArguments(ObjectInfo objectInfo, Injections injections = null, object instance = null)
        {
            var parameters = objectInfo.ConstructorInfo.GetParameters();
            var injectedArguments = new object[parameters.Length];

            if (injections != null)
            {
                if (injections.ConstructorInjection.HasValue)
                { 
                    var constructorInjection = injections.ConstructorInjection.Value;

                    foreach (ParameterInjection parameterInjection in constructorInjection.Injections)
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (parameterInjection.Name == parameters[i].Name)
                            {
                                // TODO check type.
                                injectedArguments[i] = parameterInjection.Value;
                                break;
                            }
                        }
                    }
                }
            }

            // Overwrite injection arguments in order to inject the original instance.
            if (instance != null)
            {
                injectedArguments[0] = instance;
            }

            if (injections == null || 
                (injections.ConstructorInjection.HasValue && injections.ConstructorInjection.Value.InjectionBehaviour == InjectionBehaviour.Default))
            {
                for (int i = 0; i < injectedArguments.Length; i++)
                {
                    if (injectedArguments[i] == null)
                    {
                        // TODO Check name in resolve.
                        injectedArguments[i] = container.Resolve(new Resolution(parameters[i].ParameterType));
                    }
                }
            }

            return injectedArguments;
        }

        private void SetPropertyInjections(ObjectInfo objectInfo, object instance, PropertyInjections propertyInjections)
        {
            if (propertyInjections.InjectionBehaviour == InjectionBehaviour.Default)
            {
                if (propertyInjections.Injections.Count() != objectInfo.PropertyInjectInfos.Length)
                    throw new Exception("Property injection fix");
            }

            foreach(PropertyInjection propertyInjection in propertyInjections.Injections)
            {
                foreach(PropertyInfo propertyInfo in objectInfo.PropertyInjectInfos)
                {
                    if (propertyInjection.Name == propertyInfo.Name)
                    {
                        propertyInfo.SetValue(instance, propertyInjection.Value);
                        break;
                    }
                }
            }

            foreach (PropertyInfo propertyInfo in objectInfo.PropertyResolveInfos)
            {
                propertyInfo.SetValue(instance, container.Resolve(new Resolution(propertyInfo.PropertyType)));
            }
        }
    }
}
