using Odyssey.Contracts;
using System.Collections.Generic;

namespace Odyssey.Core
{
    /// <summary>
    /// Container.
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// Creates a new container.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="enableDebugMode">Enable debug mode.</param>
        /// <returns>Container.</returns>
        public static IContainer New(IEnumerable<Registration> registrations, bool enableDebugMode)
        {
            return new DefaultContainer(registrations, enableDebugMode: enableDebugMode);
        }
    }
}
