using System;

namespace Odyssey.Exceptions
{
    /// <summary>
    /// Registration not found exception.
    /// </summary>
    public class RegistrationNotFoundException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        private RegistrationNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public static RegistrationNotFoundException New(Type interfaceType, string name)
        {
            return new RegistrationNotFoundException(
                $"Could not find a registration for interface type {interfaceType.FullName} and name '{name}'.");
        }
    }
}
