using System;
using System.Collections.Generic;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Container.
    /// </summary>
    public interface IContainer : IDisposable
    {
        /// <summary>
        /// Create child container.
        /// </summary>
        /// <param name="registrations">Registrations.</param>
        /// <returns>Child container.</returns>
        IContainer CreateChild(IEnumerable<Registration> registrations);

        /// <summary>
        /// Resolve for resolution.
        /// </summary>
        /// <param name="resolution">Resolution.</param>
        /// <returns>Service.</returns>
        object Resolve(Resolution resolution);
    }
}
