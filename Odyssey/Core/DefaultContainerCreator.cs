using SmartContainer.Contracts;
using System.Collections.Generic;

namespace SmartContainer.Core
{
    /// <summary>
    /// Defaault container creator.
    /// </summary>
    public class DefaultContainerCreator : IContainerCreator
    {
        /// <summary>
        /// Create container.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="parentContainer">Optional parent container.</param>
        /// <returns>Container.</returns>
        public IContainer CreateContainer(IEnumerable<Registration> registrations, IContainer parentContainer = null)
        {
            return new DefaultContainer(registrations, parentContainer);
        }
    }
}
