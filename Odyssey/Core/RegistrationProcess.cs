using GroundWork;
using GroundWork.Contracts;
using Odyssey.Contracts;

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
        public IOptional<ObjectInfo> ObjectInfo { get; } = new Optional<ObjectInfo>();

        /// <summary>
        /// Instance.
        /// </summary>
        public IOptional<object> Instance { get; } = new Optional<object>();

        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="instance"></param>
        public RegistrationProcess(Registration registration, object instance)
        {
            Argument.NotNull(nameof(registration), registration);
            Argument.NotNull(nameof(instance), instance);

            Registration = registration;
            Instance = new Optional<object>(instance);
        }

        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="objectInfo"></param>
        public RegistrationProcess(Registration registration, ObjectInfo objectInfo)
        {
            Argument.NotNull(nameof(registration), registration);
            Argument.NotNull(nameof(objectInfo), objectInfo);

            Registration = registration;
            ObjectInfo = new Optional<ObjectInfo>(objectInfo);
        }
    }
}
