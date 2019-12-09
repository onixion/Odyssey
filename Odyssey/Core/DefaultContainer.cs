using Odyssey.Core;
using Odyssey.Contracts;
using Odyssey.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Odyssey.Core
{
    /// <summary>
    /// Implementation of smart container.
    /// </summary>
    public class DefaultContainer : IContainer
    {
        /// <summary>
        /// Type registry.
        /// </summary>
        readonly TypeRegistry typeRegistry;

        /// <summary>
        /// Parent container.
        /// </summary>
        readonly IContainer parentContainer;

        /// <summary>
        /// Enable debug mode.
        /// </summary>
        readonly bool enableDebugMode;

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

            typeRegistry = new TypeRegistry(registrations);
            typeRegistry.Initialize(this);
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

            if (resolution.InterfaceType == typeof(IContainer))
                return this;

            // Get type registration.
            if (!typeRegistry.TryResolve(resolution.InterfaceType, resolution.Name, out TypeRegistration typeRegistration))
            {
                if (parentContainer != null)
                    return parentContainer.Resolve(resolution);

                throw new ResolveException($"Could not resolve {resolution}: matching registration not found.");
            }

            return typeRegistry.GetServiceInstance(typeRegistration, resolution, this);
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
