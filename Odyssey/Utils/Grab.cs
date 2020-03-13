using System;

namespace ProtosInfinium.Utils
{
    /// <summary>
    /// Grab utility.
    /// </summary>
    /// <remarks>
    /// Grabs a copy of a variable.
    /// </remarks>
    public static class Grab
    {
        /// <summary>
        /// Grab property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grabFunc"></param>
        /// <param name="action"></param>
        public static void Property<T>(Func<T> grabFunc, Action<T> action)
        {
            var property = grabFunc();

            // Only execute if property is not null.
            if (property != null)
            {
                action(property);
            }
        }
    }
}
