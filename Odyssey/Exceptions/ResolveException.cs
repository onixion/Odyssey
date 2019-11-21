using System;

namespace Odyssey.Exceptions
{
    /// <summary>
    /// Resolve exception.
    /// </summary>
    public class ResolveException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ResolveException(string message) : base(message)
        {
        }
    }
}
