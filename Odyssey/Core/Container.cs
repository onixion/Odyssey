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
        /// Create container.
        /// </summary>
        /// <param name="registrations"></param>
        /// <param name="parentContainer"></param>
        /// <param name="containerCreator"></param>
        /// <returns>Container.</returns>
        public static IContainer CreateContainer(IEnumerable<Registration> registrations, IContainer parentContainer, IContainerCreator containerCreator)
        {
            return containerCreator.CreateContainer(registrations, parentContainer);
        }
    }
}
