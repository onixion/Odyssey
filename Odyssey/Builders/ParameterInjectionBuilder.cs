using Odyssey.Contracts;

namespace Odyssey.Builders
{
    /// <summary>
    /// Parameter injection builder.
    /// </summary>
    public class ParameterInjectionBuilder
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
        public ParameterInjection Build()
        {
            return new ParameterInjection(Name, Value);
        }
    }
}
