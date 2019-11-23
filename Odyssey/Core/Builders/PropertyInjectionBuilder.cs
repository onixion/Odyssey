using Odyssey.Contracts;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Property injection builder.
    /// </summary>
    public class PropertyInjectionBuilder
    {
        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <returns>New parameter injection builder.</returns>
        public static PropertyInjectionBuilder New()
        {
            return new PropertyInjectionBuilder();
        }

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
