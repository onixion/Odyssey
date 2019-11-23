using Odyssey.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Registrations builder.
    /// </summary>
    public class RegistrationsBuilder
    {
        /// <summary>
        /// Registrations.
        /// </summary>
        readonly IList<Registration> registrations = new List<Registration>();

        /// <summary>
        /// Reverse order.
        /// </summary>
        public bool ReverseOrder { get; set; } = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="reverseOrder">Reverse order.</param>
        public RegistrationsBuilder()
        {
        }

        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <returns>Registration builder.</returns>
        public static RegistrationsBuilder New()
        {
            return new RegistrationsBuilder();
        }

        /// <summary>
        /// Add registration.
        /// </summary>
        /// <param name="registration">Registration.</param>
        /// <returns>Registrations builder.</returns>
        public RegistrationsBuilder Register(Registration registration)
        {
            registrations.Add(registration);
            return this;
        }

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Registrations.</returns>
        public IEnumerable<Registration> Build()
        {
            return ReverseOrder ? registrations.Reverse().ToArray() : registrations.ToArray();
        }
    }
}
