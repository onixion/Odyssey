using Odyssey.Core;
using Odyssey.Contracts;
using Odyssey.Core.Builders;
using Odyssey.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;

namespace Odyssey.Core
{
    /// <summary>
    /// Type registry.
    /// </summary>
    /// <remarks>
    /// The type registry contains all known types and their additional information.
    /// </remarks>
    public class TypeRegistry
    {
        /// <summary>
        /// Known types.
        /// </summary>
        /// <remarks>
        /// Maps interface type to one or more registrations.
        /// </remarks>
        /// <note>
        /// Key should be "type" and "name".
        /// </note>
        readonly IDictionary<Type, IList<TypeRegistration>> knownTypes = new Dictionary<Type, IList<TypeRegistration>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        public TypeRegistry(IEnumerable<Registration> registrations)
        {
            foreach(Registration registration in registrations)
            {
                if(knownTypes.TryGetValue(registration.InterfaceType, out IList<TypeRegistration> listRegistrations))
                {
                    // Check if name is already used.
                    if (listRegistrations.Any(reg => reg.Registration.Name == registration.Name))
                        throw new RegisterException($"Could not register {registration}: name '{registration.Name}' already registered.");

                    listRegistrations.Add(new TypeRegistration(registration));
                }
                else
                {
                    knownTypes.Add(registration.InterfaceType, new List<TypeRegistration> { new TypeRegistration(registration) });
                }
            }
        }

        /// <summary>
        /// Initialize registrations.
        /// </summary>
        /// <param name="resolver"></param>
        public void Initialize(IContainer container)
        {
            foreach(IList<TypeRegistration> typeRegistrations in knownTypes.Values)
            {
                foreach(TypeRegistration typeRegistration in typeRegistrations)
                {
                    if (!typeRegistration.Registration.CreateOnResolve)
                    {
                        if(typeRegistration.Registration.Instance != null)
                            continue;

                        typeRegistration.RuntimeMetaData.Instance = CreateInstanceFromRegistration(typeRegistration, container);
                    }
                }
            }
        }

        /// <summary>
        /// Try resolve.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="name"></param>
        /// <param name="typeRegistration"></param>
        /// <returns></returns>
        public bool TryResolve(Type interfaceType, string name, out TypeRegistration typeRegistration)
        {
            if (!knownTypes.TryGetValue(interfaceType, out IList<TypeRegistration> typeRegistrations))
            {
                typeRegistration = null;
                return false;
            }
            else
            {
                var foundRegistration = typeRegistrations.FirstOrDefault(reg => reg.Registration.Name == name);

                if (foundRegistration == null)
                {
                    typeRegistration = null;
                    return false;
                }
                else
                {
                    typeRegistration = foundRegistration;
                    return true;
                }
            }
        }

        /// <summary>
        /// Get service instance.
        /// </summary>
        /// <param name="typeRegistration"></param>
        /// <param name="resolution"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public object GetServiceInstance(TypeRegistration typeRegistration, Resolution resolution, IContainer container)
        {
            if (typeRegistration.Registration.Instance != null)
                return typeRegistration.Registration.Instance;

            if (typeRegistration.Registration.CreateOnResolve)
            {
                return CreateInstanceFromResolution(typeRegistration, resolution, container);
            }
            else
            {
                return typeRegistration.RuntimeMetaData.Instance;
            }
        }

        /// <summary>
        /// Creates instance from registration only.
        /// </summary>
        /// <param name="typeRegistration">Registration.</param>
        /// <param name="container">Container.</param>
        /// <returns>Service instance.</returns>
        public object CreateInstanceFromRegistration(TypeRegistration typeRegistration, IContainer container)
        {
            var parameterInfos = typeRegistration.RuntimeMetaData.ConstructorInfo.GetParameters();
            var parameters = SetupParameters(parameterInfos, container, typeRegistration.Registration.ParameterInjections);

            var instance = InstantiateService(typeRegistration.Registration.ImplementationType, parameters);
            SetupProperties(typeRegistration.RuntimeMetaData.PropertyInfos, container, instance, typeRegistration.Registration.PropertyInjections);

            return instance;
        }

        /// <summary>
        /// Create instance from resolution (and registration).
        /// </summary>
        /// <param name="typeRegistration"></param>
        /// <param name="resolution"></param>
        /// <param name="container"></param>
        /// <returns>Service instance.</returns>
        public object CreateInstanceFromResolution(TypeRegistration typeRegistration, Resolution resolution, IContainer container)
        {
            var parameterInfos = typeRegistration.RuntimeMetaData.ConstructorInfo.GetParameters();
            var parameters = SetupParameters(parameterInfos, container, resolution.ParameterInjections);

            var instance = InstantiateService(typeRegistration.Registration.ImplementationType, parameters);
            SetupProperties(typeRegistration.RuntimeMetaData.PropertyInfos, container, instance, resolution.PropertyInjections);

            return instance;
        }

        /// <summary>
        /// Setup parameters.
        /// </summary>
        object[] SetupParameters(
            ParameterInfo[] parameterInfos,
            IContainer container,
            IEnumerable<ParameterInjection> parameterInjections)
        {
            var parameters = new object[parameterInfos.Length];

            // This loop will first try to inject a named parameter value (if there is one).
            // If there is none, it will try to resolve the parameter if it has the resolve attribute attached.
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                var parameterInfo = parameterInfos[i];

                // If a named parameter injection is found, use it.
                if (parameterInjections.TryGetParameterInjection(parameterInfo, out ParameterInjection parameterInjection))
                {
                    parameters[i] = parameterInjection.Value;
                    continue;
                }

                // If the parameter has the attribute, resolve it.
                if (parameterInfo.CustomAttributes.Any(customAttributeData => customAttributeData.AttributeType == typeof(Resolve)))
                {
                    // If the parameter type is IContainer just set it directly.
                    if (parameterInfo.ParameterType == typeof(IContainer))
                    {
                        parameters[i] = container;
                    }
                    // Else resolve it.
                    else
                    {
                        var resolution = new ResolutionBuilder
                        {
                            InterfaceType = parameterInfo.ParameterType,
                        }
                        .Build();

                        parameters[i] = container.Resolve(resolution);
                    }

                    continue;
                }
            }

            // TODO refactore this.

            IList<ParameterInjection> unnamedParameterInjections = parameterInjections
                .UnnameParameterInjections()
                .ToList();

            // This loop will try the best to set the remaining parameters.
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                if (parameter != null)
                    continue;

                if (unnamedParameterInjections.Count == 0)
                    throw new InstantiationException();

                parameters[i] = unnamedParameterInjections.First().Value;
                unnamedParameterInjections.RemoveAt(0);
            }

            return parameters;
        }

        /// <summary>
        /// Instantiate the service.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <param name="parameters">Parameters to inject.</param>
        /// <returns>Instance of the service.</returns>
        object InstantiateService(Type type, object[] parameters)
        {
#if DEBUG
            DebugInstantiateService(type, parameters);
#endif

            try
            {
                if (parameters.Length == 0)
                {
                    return Activator.CreateInstance(type);
                }
                else
                {
                    return Activator.CreateInstance(type, parameters);
                }
            }
            catch (Exception e)
            {
                throw new InstantiationException();
            }
        }

        /// <summary>
        /// Setup properties.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="container"></param>
        /// <param name="instance"></param>
        /// <param name="propertyInjections"></param>
        void SetupProperties(PropertyInfo[] properties, IContainer container, object instance, IEnumerable<PropertyInjection> propertyInjections)
        {
            foreach (PropertyInfo property in properties)
            {
                if (propertyInjections.TryGetPropertyInjection(property, out PropertyInjection propertyInjection))
                {
                    property.SetValue(instance, propertyInjection.Value, null);
                }
                else
                {
                    Resolution resolution = new ResolutionBuilder
                    {
                        InterfaceType = property.PropertyType,
                        // TODO name
                    }
                    .Build();

                    property.SetValue(instance, container.Resolve(resolution), null);
                }
            }
        }

#if DEBUG
        void DebugInstantiateService(Type type, object[] parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("{ ");
            foreach (object parameter in parameters)
                stringBuilder.Append($"'{parameter.GetType().FullName}', ");
            stringBuilder.Append("}");

            Debug.WriteLine($"Instantiating object of type '{type.FullName}' with parameters: {stringBuilder}");
        }
#endif
    }
}
