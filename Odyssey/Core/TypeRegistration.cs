using Odyssey.Contracts;

namespace Odyssey.Core
{
    /// <summary>
    /// Type registration.
    /// </summary>
    public class TypeRegistration
    {
        /// <summary>
        /// Registration.
        /// </summary>
        /// <remarks>
        /// Read only.
        /// </remarks>
        public Registration Registration { get; }

        /// <summary>
        /// Runtime meta data.
        /// </summary>
        public RuntimeMetaData RuntimeMetaData { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registration">Registration.</param>
        public TypeRegistration(Registration registration)
        {
            Registration = registration;
            RuntimeMetaData = new RuntimeMetaData(registration);
        }
    }
}
