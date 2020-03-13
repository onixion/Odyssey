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
        /// Parent container.
        /// </summary>
        readonly IContainer parentContainer;

        /// <summary>
        /// Enable debug mode.
        /// </summary>
        readonly bool enableDebugMode;

        /// <summary>
        /// Object info registry.
        /// </summary>
        readonly ObjectInfoRegistry objectInfoRegistry;

        /// <summary>
        /// Registration process registry.
        /// </summary>
        readonly RegistrationProcessRegistry registrationProcessRegistry = new RegistrationProcessRegistry();

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
            this.parentContainer = parentContainer;
            this.enableDebugMode = enableDebugMode;

            objectInfoRegistry = new ObjectInfoRegistry(registrations);

            serviceCreator = new ServiceCreator(this, objectInfoRegistry);

            resolutionProcessor = new ResolutionProcessor(registrationProcessRegistry, serviceCreator);

            var registrationProcessor = new RegistrationProcessor(serviceCreator);
            foreach (Registration registration in registrations)
            {
                var process = registrationProcessor.CreateProcess(registration);
                registrationProcessRegistry.AttachProcess(process);
            }
        }

        /// <summary>
        /// Create container.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="parentContainer">Optional parent container.</param>
        /// <returns>Container.</returns>
        public IContainer CreateChild(IEnumerable<Registration> registrations)
        {
            if (disposed) throw new ObjectDisposedException(nameof(DefaultContainer));
            return new DefaultContainer(registrations, this);
        }

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="resolution">Resolution.</param>
        /// <returns>Service.</returns>
        public object Resolve(Resolution resolution)
        {
            if (disposed) throw new ObjectDisposedException(nameof(DefaultContainer));
            return resolutionProcessor.Process(resolution);
        }

        /// <summary>
        /// Disposed.
        /// </summary>
        bool disposed = false;

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
        }
    }
}
