using GroundWork;
using Odyssey.Contracts;
using System.Diagnostics;

namespace Odyssey.Core
{
    /// <summary>
    /// Resolution processor.
    /// </summary>
    /// <remarks>
    /// Processes resolutions.
    /// </remarks>
    public class ResolutionProcessor
    {
        readonly RegistrationProcessRegistry registrationProcessRegistry;
        readonly ServiceCreator serviceCreator;
        readonly bool enableDebugMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registrationProcessRegistry">Registration process registry.</param>
        /// <param name="serviceCreator">Service creator.</param>
        /// <param name="enableDebugMode">Enable debug mode.</param>
        public ResolutionProcessor(RegistrationProcessRegistry registrationProcessRegistry, ServiceCreator serviceCreator, bool enableDebugMode)
        {
            Argument.NotNull(nameof(registrationProcessRegistry), registrationProcessRegistry);
            Argument.NotNull(nameof(serviceCreator), serviceCreator);

            this.registrationProcessRegistry = registrationProcessRegistry;
            this.serviceCreator = serviceCreator;
            this.enableDebugMode = enableDebugMode;
        }

        /// <summary>
        /// Process the resolution.
        /// </summary>
        /// <param name="resolution">Resolution to be processed.</param>
        /// <returns>Service instance.</returns>
        public object Process(Resolution resolution)
        {
            Argument.NotNull(nameof(resolution), resolution);

            if (enableDebugMode)
            {
                Debug.WriteLine($"[{nameof(ResolutionProcessor)}] Processing the following resolution:");
                Debug.WriteLine($"[{nameof(ResolutionProcessor)}] - InterfaceType = {resolution.InterfaceType.FullName}");
                Debug.WriteLine($"[{nameof(ResolutionProcessor)}] - Name = '{(resolution.Name.HasValue ? resolution.Name.Value : "")}'");
            }

            // Retrieve registration process.
            var registrationProcess = registrationProcessRegistry.GetProcess(
                resolution.InterfaceType, 
                resolution.Name.HasValue ? resolution.Name.Value : "");

            // When the process has an instance, return it.
            // The instance is set, when the instance was given when
            // registrating the service or when 'CreateOnResolve' is false.
            if (registrationProcess.Instance.HasValue)
                return registrationProcess.Instance.Value;

            Injections injections = null;

            // If the resolution defined
            if (resolution.Injections.HasValue)
            {
                injections = resolution.Injections.Value;
            }

            if (registrationProcess.Registration.Injections.HasValue)
            {
                injections = registrationProcess.Registration.Injections.Value;
            }

            return serviceCreator.CreateService(
                registrationProcess.Registration.ImplementationType.Value, 
                injections);
        }
    }
}
