﻿using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Container.
    /// </summary>
    public interface IContainer : IContainerCreator, IDisposable
    {
        object Resolve(Resolution resolution);
    }
}
