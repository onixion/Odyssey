using Odyssey.Core;
using Odyssey.Contracts;
using Odyssey.Exceptions;
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
        /// Type registry.
        /// </summary>
        readonly TypeRegistry typeRegistry;

        /// <summary>
        /// Parent container.
        /// </summary>
        readonly IContainer parentContainer;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="parentContainer">Optional parent container.</param>
        public DefaultContainer(IEnumerable<Registration> registrations, IContainer parentContainer = null)
        {
            typeRegistry = new TypeRegistry(registrations);
            typeRegistry.Initialize(this);
            this.parentContainer = parentContainer;
        }

        /// <summary>
        /// Create container.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="parentContainer">Optional parent container.</param>
        /// <returns>Container.</returns>
        public IContainer CreateContainer(IEnumerable<Registration> registrations, IContainer parentContainer = null)
        {
            if (disposed) throw new ObjectDisposedException(nameof(DefaultContainer));
            return new DefaultContainerCreator().CreateContainer(registrations, parentContainer);
        }

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="resolution">Resolution.</param>
        /// <returns>Service.</returns>
        public object Resolve(Resolution resolution)
        {
            if (disposed) throw new ObjectDisposedException(nameof(DefaultContainer));

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
