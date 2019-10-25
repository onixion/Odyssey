using SmartContainer.Contracts;

namespace SmartContainer.Core
{
    /// <summary>
    /// Container.
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// Create container.
        /// </summary>
        /// <param name="parentContainer"></param>
        /// <param name="registrations"></param>
        /// <param name="containerCreator"></param>
        /// <returns>Container.</returns>
        public static IContainer CreateContainer(IContainer parentContainer, IRegistrations registrations, IContainerCreator containerCreator)
        {
            return containerCreator.CreateContainer(parentContainer, registrations);
        }
    }
}
