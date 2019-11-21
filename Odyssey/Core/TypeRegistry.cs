using Odyssey.Core;
using Odyssey.Contracts;
using Odyssey.Core.Builders;
using Odyssey.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        public void Initialize(IResolver resolver)
        {
            foreach(IList<TypeRegistration> typeRegistrations in knownTypes.Values)
            {
                foreach(TypeRegistration typeRegistration in typeRegistrations)
                {
                    if (!typeRegistration.Registration.CreateOnResolve)
                    {
                        typeRegistration.RuntimeMetaData.Instance = CreateInstanceFromRegistration(typeRegistration, resolver);
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
        /// <param name="resolver"></param>
        /// <returns></returns>
        public object GetServiceInstance(TypeRegistration typeRegistration, Resolution resolution, IResolver resolver)
        {
            if(typeRegistration.Registration.CreateOnResolve)
            {
                return CreateInstanceFromResolution(typeRegistration, resolution, resolver);
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
        /// <param name="resolver">Resolver.</param>
        /// <returns>Service instance.</returns>
        public object CreateInstanceFromRegistration(TypeRegistration typeRegistration, IResolver resolver)
        {
            object instance = null;

            if(typeRegistration.Registration.ParameterInjections == null)
            {
                instance = Activator.CreateInstance(typeRegistration.Registration.ImplementationType);
            }
            else
            {
                instance = Activator.CreateInstance(
                    typeRegistration.Registration.ImplementationType,
                    typeRegistration.Registration.ParameterInjections.Select(par => par.Value).ToArray());
            }

            // TODO
            /*
            foreach (PropertyInfo propertyInfo in PropertyInfos)
            {
                Resolution resolution = new ResolutionBuilder()
                    .SetInterfaceType(propertyInfo.PropertyType)
                    //.SetName()
                    .Build();

                propertyInfo.SetValue(instance, resolver.Resolve(resolution));
            }*/

            return instance;
        }

        /// <summary>
        /// Create instance from resolution (and registration).
        /// </summary>
        /// <param name="typeRegistration"></param>
        /// <param name="resolution"></param>
        /// <param name="resolver"></param>
        /// <returns>Service instance.</returns>
        public object CreateInstanceFromResolution(TypeRegistration typeRegistration, Resolution resolution, IResolver resolver)
        {
            object instance = null;

            // If paramter injections are defined in the resolution,
            // then use them.
            if (resolution.ParameterInjections != null)
            {
                instance = Activator.CreateInstance(
                    typeRegistration.Registration.ImplementationType,
                    resolution.ParameterInjections.Select(par => par.Value).ToArray());
            }
            // If not, take them from the registration.
            else
            {
                if (typeRegistration.Registration.ParameterInjections == null)
                {
                    instance = Activator.CreateInstance(typeRegistration.Registration.ImplementationType);
                }
                else
                {
                    instance = Activator.CreateInstance(
                        typeRegistration.Registration.ImplementationType,
                        typeRegistration.Registration.ParameterInjections.Select(par => par.Value).ToArray());
                }
            }

            /*
            foreach (PropertyInfo propertyInfo in typeRegistration.RuntimeMetaData.PropertyInfos)
            {
                Resolution propertyResolution = new ResolutionBuilder()
                    .SetInterfaceType(propertyInfo.PropertyType)
                    //.SetName()
                    .Build();

                propertyInfo.SetValue(instance, resolver.Resolve(propertyResolution));
            }*/

            return instance;
        }
    }
}
