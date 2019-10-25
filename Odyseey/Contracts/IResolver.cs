namespace SmartContainer.Contracts
{
    /// <summary>
    /// Resolver interface.
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Resolve service.
        /// </summary>
        /// <param name="resolution"></param>
        /// <returns>Service.</returns>
        object Resolve(Resolution resolution);
    }
}
