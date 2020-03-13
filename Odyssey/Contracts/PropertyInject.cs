using System;

namespace Odyssey.Contracts
{
    /// <summary>
    /// Property inject attribute.
    /// </summary>
    /// <remarks>
    /// Marks the property in order to be used for injection.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyInject : Attribute
    {
    }
}
