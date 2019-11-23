using System;
using System.Collections.Generic;
using System.Text;

namespace Odyssey.Utils
{
    /// <summary>
    /// Argument.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Not null.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Value of the argument.</param>
        public static void NotNull(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(name, $"Argument {name} can't be null.");
        }
    }
}
