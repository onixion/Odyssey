using System;

namespace SmartContainer.Contracts
{
    /// <summary>
    /// Container.
    /// </summary>
    public interface IContainer : IContainerCreator, IResolver, IDisposable
    {
    }
}
