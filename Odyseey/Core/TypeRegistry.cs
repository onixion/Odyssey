using SmartContainer.Contracts;
using SmartContainer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartContainer.Core
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
        readonly IDictionary<Type, IList<Registration>> knownTypes = new Dictionary<Type, IList<Registration>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        public TypeRegistry(IRegistrations registrations)
        {
            foreach(Registration registration in registrations.Registrations)
            {
                if(knownTypes.TryGetValue(registration.InterfaceType, out IList<Registration> listRegistrations))
                {
                    // Check if name is already used.
                    if (listRegistrations.Any(reg => reg.Name == registration.Name))
                        throw new RegisterException($"Could not register {registration}: name '{registration.Name}' already registered.");

                    listRegistrations.Add(registration);
                }
                else
                {
                    knownTypes.Add(registration.InterfaceType, new List<Registration> { registration });
                }
            }
        }

        /// <summary>
        /// Try resolve.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="name"></param>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool TryResolve(Type interfaceType, string name, out Registration registration)
        {
            if (!knownTypes.TryGetValue(interfaceType, out IList<Registration> registrations))
            {
                registration = null;
                return false;
            }
            else
            {
                var foundRegistration = registrations.FirstOrDefault(reg => reg.Name == name);

                if (foundRegistration == null)
                {
                    registration = null;
                    return false;
                }
                else
                {
                    registration = foundRegistration;
                    return true;
                }
            }
        }
    }
}
