using System;

namespace Odyssey.Exceptions
{
    /// <summary>
    /// Disposed exception.
    /// </summary>
    public class DisposedException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        public DisposedException(string message) : base(message)
        {
        }
    }
}
