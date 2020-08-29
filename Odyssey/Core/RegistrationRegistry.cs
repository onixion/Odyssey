using GroundWork;
using GroundWork.Contracts;
using Odyssey.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Odyssey.Core
{
    /// <summary>
    /// Registration process registry.
    /// </summary>
    public class RegistrationProcessRegistry
    {
        readonly IDictionary<Tuple<Type, string>, RegistrationProcess> typeToProcessMap = new Dictionary<Tuple<Type, string>, RegistrationProcess>();

        readonly IOptional<RegistrationProcessRegistry> parentRegistrationProcessRegistry;

        readonly bool enableDebugMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RegistrationProcessRegistry(RegistrationProcessRegistry parentRegistrationProcessRegistry, bool enableDebugMode)
        {
            this.parentRegistrationProcessRegistry = new Optional<RegistrationProcessRegistry>(parentRegistrationProcessRegistry);
            this.enableDebugMode = enableDebugMode;
        }

        /// <summary>
        /// Attach registration process to registry.
        /// </summary>
        /// <param name="registrationProcess">Registration process.</param>
        public void AttachProcess(RegistrationProcess registrationProcess)
        {
            if (enableDebugMode)
            {
                Debug.WriteLine($"[{nameof(RegistrationProcessRegistry)}] Attaching registration process ...");
                Debug.WriteLine($"[{nameof(RegistrationProcessRegistry)}] - Registration={registrationProcess.Registration}");
            }

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
            if (enableDebugMode)
            {
                Debug.WriteLine($"[{nameof(RegistrationProcessRegistry)}] Getting registration process ...");
                Debug.WriteLine($"[{nameof(RegistrationProcessRegistry)}] - InterfaceType={interfaceType}");
                Debug.WriteLine($"[{nameof(RegistrationProcessRegistry)}] - Name={name}");
            }

            if (!typeToProcessMap.TryGetValue(Tuple.Create(interfaceType, name), out RegistrationProcess process))
            {
                if (!parentRegistrationProcessRegistry.HasValue)
                    throw RegistrationNotFoundException.New(interfaceType, name);

                return parentRegistrationProcessRegistry.Value.GetProcess(interfaceType, name);
            }

            return process;
        }
    }
}
