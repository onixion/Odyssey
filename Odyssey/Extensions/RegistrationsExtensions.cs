using SmartContainer.Contracts;
using System;

namespace SmartContainer.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IRegistrator"/>.
    /// </summary>
    public static class RegistratorExtensions
    {
        /// <summary>
        /// Register service.
        /// </summary>
        /// <typeparam name="TInterface">Interface the service implements.</typeparam>
        /// <typeparam name="TImplementation">Implementation that implements the interface.</typeparam>
        /// <param name="registrator">Registrator.</param>
        public static void Register<TInterface, TImplementation>(this IRegistrator registrator)
        {
            // TODO
        }

        /// <summary>
        /// Register an instance service.
        /// </summary>
        /// <typeparam name="TInterface">Interface the service implements.</typeparam>
        /// <typeparam name="TImplementation">Implementation that implements the interface.</typeparam>
        /// <param name="registrator">Registrator.</param>
        /// <param name="instance">Instance.</param>
        public static void RegisterInstance<TInterface, TImplementation>(this IRegistrator registrator, TImplementation instance)
        {
            // TODO
        }

        /// <summary>
        /// Register an instance service.
        /// </summary>
        /// <typeparam name="TInterface">Interface the service implements.</typeparam>
        /// <typeparam name="TImplementation">Implementation that implements the interface.</typeparam>
        /// <param name="registrator">Registrator.</param>
        public static void RegisterOneShot<TInterface, TImplementation>(this IRegistrator registrator)
        {
            // TODO
        }

        /// <summary>
        /// Register an instance service.
        /// </summary>
        /// <typeparam name="TInterface">Interface the service implements.</typeparam>
        /// <typeparam name="TImplementation">Implementation that implements the interface.</typeparam>
        /// <param name="smartContainer">Smart container.</param>
        /// <param name="cacheTimeSpan">Cache time span.</param>
        public static void RegisterCache<TInterface, TImplementation>(this IRegistrator registrator, TimeSpan? cacheTimeSpan = null)
        {
            // TODO
        }
    }
}
