using Odyssey.Contracts;
using Utilinator.Core;

namespace Odyssey.Core
{
    /// <summary>
    /// Registration process.
    /// </summary>
    public class RegistrationProcess
    {
        /// <summary>
        /// Registration.
        /// </summary>
        public Registration Registration { get; }

        /// <summary>
        /// Object info.
        /// </summary>
        public Optional<ObjectInfo> ObjectInfo { get; } = new Optional<ObjectInfo>();

        /// <summary>
        /// Instance.
        /// </summary>
        public Optional<object> Instance { get; } = new Optional<object>();

        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="instance"></param>
        public RegistrationProcess(Registration registration, object instance)
        {
            Registration = registration;
            Instance = instance;
        }

        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="objectInfo"></param>
        public RegistrationProcess(Registration registration, ObjectInfo objectInfo)
        {
            Registration = registration;
            ObjectInfo = objectInfo;
        }
    }
}
