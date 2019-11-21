using System;

namespace Odyssey.Exceptions
{
    /// <summary>
    /// Register exception.
    /// </summary>
    public class RegisterException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        public RegisterException(string message) : base(message)
        {
        }
    }
}
