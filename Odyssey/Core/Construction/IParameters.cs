using Odyssey.Contracts;

namespace Odyssey.Core.Construction
{
    /// <summary>
    /// Registration parameters interface.
    /// </summary>
    public interface IParameters
    {
        /// <summary>
        /// Get parameters.
        /// </summary>
        /// <param name="typeRegistration">Type registration.</param>
        /// <param name="container">Container.</param>
        /// <param name="resolution">Resolution.</param>
        /// <param name="serviceInstance">Service instance.</param>
        /// <returns>Parameters.</returns>
        object[] GetParameters(TypeRegistration typeRegistration, IContainer container, Resolution resolution = null, object serviceInstance = null);
    }
}
