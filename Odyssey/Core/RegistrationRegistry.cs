using System;
using System.Collections.Generic;

namespace Odyssey.Core
{
    /// <summary>
    /// Registration process registry.
    /// </summary>
    public class RegistrationProcessRegistry
    {
        readonly IDictionary<Tuple<Type, string>, RegistrationProcess> typeToProcessMap = new Dictionary<Tuple<Type, string>, RegistrationProcess>();

        /// <summary>
        /// Attach registration process to registry.
        /// </summary>
        /// <param name="registrationProcess">Registration process.</param>
        public void AttachProcess(RegistrationProcess registrationProcess)
        {
            var key = Tuple.Create(
                registrationProcess.Registration.InterfaceType,
                registrationProcess.Registration.Name.HasValue ? registrationProcess.Registration.Name.Value : "");

            typeToProcessMap.Add(key, registrationProcess);
        }

        /// <summary>
        /// Get process.
        /// </summary>
        /// <param name="interfaceType">Interface type.</param>
        /// <returns>Registration process.</returns>
        public RegistrationProcess GetProcess(Type interfaceType, string name)
        {
            if (!typeToProcessMap.TryGetValue(Tuple.Create(interfaceType, name), out RegistrationProcess process))
                throw new Exception();

            return process;
        }
    }
}
