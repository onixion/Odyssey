using System.Collections.Generic;

namespace SmartContainer.Contracts
{
    /// <summary>
    /// Container creator interface.
    /// </summary>
    public interface IContainerCreator
    {
        /// <summary>
        /// Create container.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <param name="parentContainer">Parent container.</param>
        /// <returns>Container.</returns>
        IContainer CreateContainer(IEnumerable<Registration> registrations, IContainer parentContainer = null);
    }
}
