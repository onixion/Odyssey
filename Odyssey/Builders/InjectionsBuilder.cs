using Odyssey.Contracts;

namespace Odyssey.Builders
{
    /// <summary>
    /// Injections builder.
    /// </summary>
    public class InjectionsBuilder
    {
        /// <summary>
        /// Constructor injections.
        /// </summary>
        public ConstructorInjection ConstructorInjection { get; set; }

        /// <summary>
        /// Property injections.
        /// </summary>
        public PropertyInjections PropertyInjections { get; set; }

        /// <summary>
        /// Build.
        /// </summary>
        /// <returns>Injections.</returns>
        public Injections Build()
        {
            return new Injections(ConstructorInjection, PropertyInjections);
        }
    }
}
