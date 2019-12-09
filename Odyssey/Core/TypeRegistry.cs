using Odyssey.Contracts;
using Odyssey.Core.Construction;
using Odyssey.Core.Construction.Parameters;
using Odyssey.Core.Constructor;
using Odyssey.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Parameters.
        /// </summary>
        readonly IParameters parameters = new Parameters();

        /// <summary>
        /// Properties setter.
        /// </summary>
        readonly IPropertiesSetter propertiesSetter = new PropertiesSetter();

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

                        typeRegistration.RuntimeMetaData.Instance = CreateInstance(typeRegistration, container);
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
            // Instance provided when registered.
            if (typeRegistration.Registration.Instance != null)
                return typeRegistration.Registration.Instance;

            // Create instance on resolve.
            if (typeRegistration.Registration.CreateOnResolve)
                return CreateInstance(typeRegistration, container, resolution: resolution);

            // Instance created on registered.
            return typeRegistration.RuntimeMetaData.Instance;
        }

        /// <summary>
        /// Creates instance from registration only.
        /// </summary>
        /// <param name="typeRegistration">Registration.</param>
        /// <param name="container">Container.</param>
        /// <param name="resolution">Resolution.</param>
        /// <param name="serviceInstance">Service instance.</param>
        /// <returns>Service instance.</returns>
        public object CreateInstance(TypeRegistration typeRegistration, IContainer container, Resolution resolution = null, object serviceInstance = null)
        {
            try
            {
                // Get parameters for the constructor.
                var pars = parameters.GetParameters(typeRegistration, container, resolution, serviceInstance);

                // Create instance.
                var instance = serviceInstance == null ?
                    Activator.CreateInstance(typeRegistration.Registration.ImplementationType, pars) :
                    Activator.CreateInstance(typeRegistration.Registration.DecoratorInjectionType, pars);

                // Set properties of the instance.
                propertiesSetter.SetProperties(
                    instance,
                    typeRegistration.RuntimeMetaData.PropertyInfos,
                    typeRegistration.Registration.PropertyInjections,
                    container);

                // Create decorator and return it instead.
                if (typeRegistration.Registration.DecoratorInjectionType != null && serviceInstance == null)
                    return CreateInstance(typeRegistration, container, resolution, serviceInstance: instance);

                return instance;
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
