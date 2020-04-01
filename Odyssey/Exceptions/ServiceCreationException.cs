using System;

namespace Odyssey.Exceptions
{
    /// <summary>
    /// Service creation exception.
    /// </summary>
    public class ServiceCreationException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceCreationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
