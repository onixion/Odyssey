using Odyssey.Contracts;
using System.Diagnostics;

namespace Odyssey.Core
{
    /// <summary>
    /// Registration processor.
    /// </summary>
    public class RegistrationProcessor
    {
        readonly ServiceCreator serviceCreator;

        readonly bool enableDebugMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RegistrationProcessor(ServiceCreator serviceCreator, bool enableDebugMode)
        {
            this.serviceCreator = serviceCreator;
            this.enableDebugMode = enableDebugMode;
        }

        /// <summary>
        /// Creates the service instance from the registration.
        /// </summary>
        /// <param name="registration">Registration.</param>
        /// <returns>Registration process.</returns>
        public RegistrationProcess CreateProcess(Registration registration)
        {
            if (enableDebugMode)
            {
                Debug.WriteLine($"[{nameof(RegistrationProcessor)}] Processing registration ...");
                Debug.WriteLine($"[{nameof(RegistrationProcessor)}] - Registration={registration}");
            }

            if (registration.Instance.HasValue)
                return new RegistrationProcess(registration, registration.Instance.Value);

            if (registration.CreateOnResolve)
                return new RegistrationProcess(registration, new ObjectInfo(registration.ImplementationType.Value));

            return new RegistrationProcess(registration, CreateService(registration));
        }

        object CreateService(Registration registration)
        {
            return serviceCreator.CreateService(
                registration.ImplementationType.Value,
                registration.Injections.ValueOrDefault);
        }
    }
}
