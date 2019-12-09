using Odyssey.Contracts;
using System.Linq;
using System.Reflection;

namespace Odyssey.Core
{
    /// <summary>
    /// Runtime meta data.
    /// </summary>
    public class RuntimeMetaData
    {
        /// <summary>
        /// Constructor info.
        /// </summary>
        /// <remarks>
        /// We only support one constructor. Services must not have more than one constructor.
        /// </remarks>
        public ConstructorInfo ConstructorInfo { get; }

        /// <summary>
        /// Constructor info of the decorator, if any.
        /// </summary>
        /// <remarks>
        /// We only support one constructor. Decorators must not have more than one constructor.
        /// </remarks>
        public ConstructorInfo DecoratorConstructorInfo { get; }

        /// <summary>
        /// Property infos.
        /// </summary>
        public PropertyInfo[] PropertyInfos { get; }

        /// <summary>
        /// Instance.
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registration">Registration.</param>
        public RuntimeMetaData(Registration registration)
        {
            if (registration.ImplementationType != null)
            {
                ConstructorInfo = registration.ImplementationType
                    .GetConstructors()
                    .First();
            }

            if (registration.DecoratorInjectionType != null)
            {
                DecoratorConstructorInfo = registration.DecoratorInjectionType
                    .GetConstructors()
                    .First();
            }

            PropertyInfos = registration.InterfaceType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(propertyInfo => propertyInfo.CustomAttributes.Any(customAttributeData => customAttributeData.AttributeType == typeof(Resolve)))
                .ToArray();
        }
    }
}
