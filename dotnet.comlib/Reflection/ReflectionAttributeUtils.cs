/*
 * Copyright (c) 2020 ZP
 * Revision: 0.0.0.1
 * CLR: 4.0.30319.42000
 * Date 8/11/2020 9:39:18 AM
 * Name ReflectionAttributeUtils
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Comlib.Reflection
{
    /// <summary>
    /// Reflection utility class for attributes.
    /// </summary>
    public class AttributeHelper
    {
        /// <summary>
        /// Get the description attribute from the assembly associated with <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type who's assembly's description should be obtained.</param>
        /// <param name="defaultVal">Default value to use if description is not available.</param>
        /// <returns></returns>
        public static string GetAssemblyInfoDescription(Type type, string defaultVal)
        {
            // Get the assembly object.
            var assembly = type.Assembly;

            // See if the Assembly Description is defined.
            var isDefined = Attribute.IsDefined(assembly, typeof(AssemblyDescriptionAttribute));
            var description = defaultVal;

            if (isDefined)
            {
                var adAttr = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly,
                    typeof(AssemblyDescriptionAttribute));

                if (adAttr != null)
                {
                    description = adAttr.Description;
                }
            }
            return description;
        }

        /// <summary>
        /// Gets the attributes of the specified type applied to the class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns></returns>
        public static IList<T> GetClassAttributes<T>(object obj)
        {
            // Check
            if (obj == null)
            {
                return new List<T>();
            }

            var attributes = obj.GetType().GetCustomAttributes(typeof(T), false);

            IList<T> attributeList = new List<T>();

            // iterate through the attributes, retrieving the
            // properties
            foreach (var attribute in attributes)
            {
                attributeList.Add((T)attribute);
            }
            return attributeList;
        }

        /// <summary>
        /// Get a list of property info's that have the supplied attribute applied to it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, KeyValuePair<T, PropertyInfo>> GetPropsWithAttributes<T>(object obj) where T : Attribute
        {
            // Check
            if (obj == null)
            {
                return new Dictionary<string, KeyValuePair<T, PropertyInfo>>();
            }

            var map = new Dictionary<string, KeyValuePair<T, PropertyInfo>>();

            var props = ReflectionUtils.GetAllProperties(obj, null);
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(typeof(T), true);
                if (attrs != null && attrs.Length > 0)
                {
                    map[prop.Name] = new KeyValuePair<T, PropertyInfo>(attrs[0] as T, prop);
                }
            }
            return map;
        }

        /// <summary>
        /// Get a list of property info's that have the supplied attribute applied to it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<T, PropertyInfo>> GetPropsWithAttributesList<T>(object obj) where T : Attribute
        {
            // Check
            if (obj == null)
            {
                return new List<KeyValuePair<T, PropertyInfo>>();
            }

            var map = new List<KeyValuePair<T, PropertyInfo>>();

            var props = ReflectionUtils.GetAllProperties(obj, null);
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(typeof(T), true);
                if (attrs != null && attrs.Length > 0)
                {
                    map.Add(new KeyValuePair<T, PropertyInfo>(attrs[0] as T, prop));
                }
            }
            return map;
        }
    }
}