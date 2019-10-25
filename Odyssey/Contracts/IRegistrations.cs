using System.Collections.Generic;

namespace SmartContainer.Contracts
{
    /// <summary>
    /// Registrations interface.
    /// </summary>
    public interface IRegistrations
    {
        /// <summary>
        /// Registrations.
        /// </summary>
        IEnumerable<Registration> Registrations { get; }
    }
}
