using Odyssey.Contracts;
using SmartContainer.Contracts;
using SmartContainer.Core.Builders;
using SmartContainer.Exceptions;
using System;
using System.Collections.Generic;
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
            PropertyInfos = registration.InterfaceType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(propertyInfo => propertyInfo.CustomAttributes.Any(customAttributeData => customAttributeData.AttributeType == typeof(PropertyDependency)))
                .ToArray();
        }
    }
}
