using Odyssey.Contracts;

namespace Odyssey.Builders
{
    /// <summary>
    /// Property injection builder.
    /// </summary>
    public class PropertyInjectionBuilder
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Build parameter injection.
        /// </summary>
        /// <returns></returns>
        public PropertyInjection Build()
        {
            return new PropertyInjection(Name, Value);
        }
    }
}
