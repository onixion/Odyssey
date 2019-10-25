namespace SmartContainer.Contracts
{
    /// <summary>
    /// Life time.
    /// </summary>
    public enum Lifetime
    {
        /// <summary>
        /// Creates one instance of the service.
        /// </summary>
        CreateOnes = 0,

        /// <summary>
        /// Creates a new instance of the service, everytime it's resolved.
        /// </summary>
        CreateOnResolve = 1,
    }
}
