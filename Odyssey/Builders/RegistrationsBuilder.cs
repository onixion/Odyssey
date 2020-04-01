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
        /// Registrations.
        /// </summary>
        public IEnumerable<Registration> Registrations { get; set; }
  
        /// <summary>
        /// Registrations.
        /// </summary>
        readonly IList<Registration> registrations = new List<Registration>();

        /// <summary>
        /// Add registration.
        /// </summary>
        /// <param name="registration">Registration to add.</param>
        /// <returns>Registrations builder.</returns>
        public RegistrationsBuilder AddRegistration(Registration registration)
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
            var allRegistrations = Registrations != null ? Registrations.Concat(registrations) : registrations;
            return allRegistrations.ToArray();
        }
    }
}
