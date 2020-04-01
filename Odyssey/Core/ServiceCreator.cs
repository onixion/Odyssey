using Odyssey.Contracts;
using Odyssey.Exceptions;
using System;
using System.Diagnostics;
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

        readonly bool enableDebugMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceCreator(IContainer container, ObjectInfoRegistry objectInfoRegistry, bool enableDebugMode)
        {
            this.container = container;
            this.objectInfoRegistry = objectInfoRegistry;
            this.enableDebugMode = enableDebugMode;
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
            if (enableDebugMode)
            {
                Debug.WriteLine($"[{nameof(ServiceCreator)}] Creating service ...");
                Debug.WriteLine($"[{nameof(ServiceCreator)}] - ServiceType={serviceType}");
            }

            var objectInfo = objectInfoRegistry.GetObjectInfo(serviceType);

            object[] constructorArguments = null;

            try
            {
                constructorArguments = GetConstructorArguments(serviceType, objectInfo, injections, instance);
            }
            catch(Exception e)
            {
                throw new ServiceCreationException(
                    $"Couldn't get constructor arguments for service type {serviceType}.", e);
            }

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
            SetPropertyInjections(objectInfo, serviceInstance);

            if (injections != null && injections.DecoratorInjection.HasValue)
            {
                serviceInstance = CreateService(
                    injections.DecoratorInjection.Value.ImplementationType.Value,
                    injections.DecoratorInjection.Value.Injections.ValueOrDefault,
                    serviceInstance);
            }

            return serviceInstance;
        }

        /// <summary>
        /// Get constructor arguments.
        /// </summary>
        /// <param name="serviceType">Service type.</param>
        /// <param name="objectInfo">Object info for the given service type.</param>
        /// <param name="injections">Injections that shall be performed on the instance.</param>
        /// <param name="innerInstance">Inner instance (used for decorator injection).</param>
        /// <returns>Constructor arguments.</returns>
        private object[] GetConstructorArguments(Type serviceType, ObjectInfo objectInfo, Injections injections = null, object innerInstance = null)
        {
            var parameters = objectInfo.ConstructorInfo.GetParameters();
            var injectedArguments = new object[parameters.Length];

            if (injections != null)
            {
                if (injections.ConstructorInjection.HasValue)
                { 
                    var constructorInjection = injections.ConstructorInjection.Value;

                    // Go through the parameters we got from the constructor.
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        // All IContainer types are always set to the current container.
                        if (parameters[i].GetType() == typeof(IContainer))
                        {
                            injectedArguments[i] = container;
                            continue;
                        }

                        foreach (ParameterInjection parameterInjection in constructorInjection.Injections)
                        {
                            if (parameterInjection.Name == parameters[i].Name)
                            {
                                if (!parameterInjection.Value.GetType().IsAssignableFrom(parameters[i].ParameterType))
                                    throw new ServiceCreationException(
                                        $"Can't cast type {parameterInjection.Value.GetType()} to {parameters[i].ParameterType}.");

                                injectedArguments[i] = parameterInjection.Value;
                                break;
                            }
                        }
                    }
                }
            }

            // Overwrite injection arguments in order to inject the original instance.
            if (innerInstance != null)
            {
                injectedArguments[0] = innerInstance;
            }

            if (injections == null || 
               (injections.ConstructorInjection.HasValue && injections.ConstructorInjection.Value.InjectionBehaviour == InjectionBehaviour.Default))
            {
                for (int i = 0; i < injectedArguments.Length; i++)
                {
                    if (injectedArguments[i] == null)
                    {
                        try
                        {
                            injectedArguments[i] = container.Resolve(new Resolution(parameters[i].ParameterType));
                        }
                        catch(Exception e)
                        {
                            throw new ResolveException(
                                $"Couldn't resolve the parameter of {parameters[i].ParameterType} for the constructor of {serviceType}.", e);
                        }
                    }
                }
            }

            return injectedArguments;
        }

        private void SetPropertyInjections(ObjectInfo objectInfo, object instance)
        {
            // Resolve property injections.
            foreach (PropertyDetail propertyDetail in objectInfo.Properties)
            {
                var propertyInfo = propertyDetail.PropertyInfo;
                var resolution = new Resolution(propertyInfo.PropertyType, propertyDetail.Name.ValueOrDefault);
                var resolvedPropertyInstance = container.Resolve(resolution);

                propertyInfo.SetValue(instance, resolvedPropertyInstance);
            }
        }
    }
}
