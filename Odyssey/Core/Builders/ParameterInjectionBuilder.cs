using Odyssey.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odyssey.Core.Builders
{
    /// <summary>
    /// Parameter injection builder.
    /// </summary>
    public class ParameterInjectionBuilder
    {
        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <returns>New parameter injection builder.</returns>
        public static ParameterInjectionBuilder New()
        {
            return new ParameterInjectionBuilder();
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
        public ParameterInjection Build()
        {
            return new ParameterInjection(Value, Name);
        }
    }
}
