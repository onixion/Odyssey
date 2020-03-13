using Odyssey.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Odyssey.Core
{
    /// <summary>
    /// Object info.
    /// </summary>
    public class ObjectInfo
    {
        /// <summary>
        /// Constructor info.
        /// </summary>
        /// <remarks>
        /// We only support one constructor. Services must not have more than one constructor.
        /// </remarks>
        public ConstructorInfo ConstructorInfo { get; }

        /// <summary>
        /// Property inject infos.
        /// </summary>
        public PropertyInfo[] PropertyInjectInfos { get; }

        /// <summary>
        /// Property resolve infos.
        /// </summary>
        public PropertyInfo[] PropertyResolveInfos { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ObjectInfo(Type type)
        {
            ConstructorInfo = type
                .GetConstructors()
                .First();

            PropertyInjectInfos = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(propertyInfo => propertyInfo.CustomAttributes
                    .Any(customAttributeData => customAttributeData.AttributeType == typeof(PropertyInject)))
                .ToArray();

            PropertyResolveInfos = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(propertyInfo => propertyInfo.CustomAttributes
                    .Any(customAttributeData => customAttributeData.AttributeType == typeof(Resolve)))
                .ToArray();
        }
    }
}
