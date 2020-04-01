using Odyssey.Contracts;
using System;
using System.Collections.Generic;

namespace Odyssey.Core
{
    /// <summary>
    /// Implementation of smart container.
    /// </summary>
    public class DefaultContainer : IContainer
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
        public DefaultContainer(IEnumerable<Registration> registrations, IContainer parentContainer = null, bool enableDebugMode = false)
        {
            // todo: improve this
            registrationProcessRegistry = new RegistrationProcessRegistry(
                parentContainer != null ? ((DefaultContainer)parentContainer).registrationProcessRegistry : null,
                enableDebugMode);

            objectInfoRegistry = new ObjectInfoRegistry(registrations);
            serviceCreator = new ServiceCreator(this, objectInfoRegistry, enableDebugMode);
            resolutionProcessor = new ResolutionProcessor(registrationProcessRegistry, serviceCreator, enableDebugMode);

            var registrationProcessor = new RegistrationProcessor(serviceCreator, enableDebugMode);
            AddThisContainerToRegistrationProcessRegistry(registrationProcessor);

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
            return new DefaultContainer(registrations, this);
        }

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="resolution">Resolution.</param>
        /// <returns>Service.</returns>
        public object Resolve(Resolution resolution)
        {
            return resolutionProcessor.Process(resolution);
        }
    }
}
