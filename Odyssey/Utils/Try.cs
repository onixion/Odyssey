namespace Odyssey.Utils
{
    /// <summary>
    /// Try.
    /// </summary>
    public static class Try
    {
        /// <summary>
        /// Cast
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool Cast<T>(object obj, out T result)
        {
            if (obj is T)
            {
                result = (T)obj;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }
    }
}
