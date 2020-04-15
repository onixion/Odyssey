using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Core
{
    /// <summary>
    /// Default implementation of the <see cref="IContainer"/>.
    /// </summary>
    public class OdysseyContainer : IContainer
    {
        /// <summary>
        /// Object info registry.
        /// </summary>
        readonly ObjectInfoRegistry objectInfoRegistry;

        /// <summary>
        /// Registration process registry.
        /// </summary>
        readonly RegistrationProcessRegistry registrationProcessRegistry;

        /// <summary>
        /// Resolution processor.
        /// </summary>
        readonly ResolutionProcessor resolutionProcessor;

        /// <summary>
        /// Service creator.
        /// </summary>
        readonly ServiceCreator serviceCreator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="parentContainer">Optional parent container.</param>
        /// <param name="enableDebugMode">Enable debug mode.</param>
        public OdysseyContainer(IEnumerable<Registration> registrations, OdysseyContainer parentContainer = null, bool enableDebugMode = false)
        {
            // Setup registration process registry.
            registrationProcessRegistry = new RegistrationProcessRegistry(
                parentContainer != null ? parentContainer.registrationProcessRegistry : null,
                enableDebugMode);

            // Setup object info registry.
            objectInfoRegistry = new ObjectInfoRegistry(registrations);

            // Setup service creator.
            serviceCreator = new ServiceCreator(this, objectInfoRegistry, enableDebugMode);

            // Setup resolution processor.
            resolutionProcessor = new ResolutionProcessor(registrationProcessRegistry, serviceCreator, enableDebugMode);

            // Setup the registration processor.
            var registrationProcessor = new RegistrationProcessor(serviceCreator, enableDebugMode);
            AddThisContainerToRegistrationProcessRegistry(registrationProcessor);

            // Add each registration to the registration process registry.
            foreach (Registration registration in registrations)
            {
                var process = registrationProcessor.CreateProcess(registration);
                registrationProcessRegistry.AttachProcess(process);
            }
        }

        /// <summary>
        /// Adds this container to the process registry.
        /// </summary>
        void AddThisContainerToRegistrationProcessRegistry(RegistrationProcessor registrationProcessor)
        {
            var process = registrationProcessor.CreateProcess(new Registration(typeof(IContainer), instance: this));
            registrationProcessRegistry.AttachProcess(process);
        }

        /// <summary>
        /// Create container.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="parentContainer">Optional parent container.</param>
        /// <returns>Container.</returns>
        public IContainer CreateChild(IEnumerable<Registration> registrations)
        {
            return new OdysseyContainer(registrations, this);
        }

        /// <summary>
        /// Resolve the given resolution.
        /// </summary>
        /// <param name="resolution">Resolution.</param>
        /// <returns>Service.</returns>
        public object Resolve(Resolution resolution)
        {
            return resolutionProcessor.Process(resolution);
        }
    }
}
