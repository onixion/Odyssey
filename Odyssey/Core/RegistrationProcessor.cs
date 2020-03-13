using Odyssey.Contracts;

namespace Odyssey.Core
{
    /// <summary>
    /// Registration processor.
    /// </summary>
    public class RegistrationProcessor
    {
        readonly ServiceCreator serviceCreator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="container"></param>
        public RegistrationProcessor(ServiceCreator serviceCreator)
        {
            this.serviceCreator = serviceCreator;
        }

        /// <summary>
        /// Creates the service instance from the registration.
        /// </summary>
        /// <param name="registration">Registration.</param>
        /// <returns>Registration process.</returns>
        public RegistrationProcess CreateProcess(Registration registration)
        {
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
