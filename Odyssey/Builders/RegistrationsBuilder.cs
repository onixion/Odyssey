using Odyssey.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Odyssey.Builders
{
    /// <summary>
    /// Registrations builder.
    /// </summary>
    public class RegistrationsBuilder
    {
        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <returns>Registration builder.</returns>
        public static RegistrationsBuilder New()
        {
            return new RegistrationsBuilder();
        }

        /// <summary>
        /// Registrations.
        /// </summary>
        public IEnumerable<Registration> Registrations { get; set; }

        readonly IList<Registration> registrations = new List<Registration>();

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
        /// Reverse order.
        /// </summary>
        public bool ReverseOrder { get; set; } = false;

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Registrations.</returns>
        public IEnumerable<Registration> Build()
        {
            var allRegistrations = Registrations != null ? Registrations.Concat(registrations) : registrations;

            return ReverseOrder ? allRegistrations.Reverse().ToArray() : allRegistrations.ToArray();
        }
    }
}
