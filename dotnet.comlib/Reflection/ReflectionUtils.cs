﻿/*
 * Copyright (c) 2020 ZP
 * Revision: 0.0.0.1
 * CLR: 4.0.30319.42000
 * Date 8/11/2020 9:40:02 AM
 * Name ReflectionUtils
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace Comlib.Reflection
{
    /// <summary>
    /// Various reflection based utility methods.
    /// </summary>
    public class ReflectionUtils
    {
        /// <summary>
        /// Set object properties on T using the properties collection supplied.
        /// The properties collection is the collection of "property" to value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="properties"></param>
        /// <returns>true if all properties set, false otherwise</returns>
        public static void SetProperties<T>(T obj, IList<KeyValuePair<string, string>> properties) where T : class
        {
            // Validate
            if (obj == null)
            { return; }

            foreach (var propVal in properties)
            {
                SetProperty<T>(obj, propVal.Key, propVal.Value);
            }
        }

        /// <summary>
        /// Set the object properties using the prop name and value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static void SetProperty<T>(T obj, string propName, string propVal) where T : class
        {
            Guard.IsNotNull(obj, "Object containing properties to set is null");
            Guard.IsTrue(!string.IsNullOrEmpty(propName), "Property name not supplied.");

            // Remove spaces.
            propName = propName.Trim();
            if (string.IsNullOrEmpty(propName))
            { throw new ArgumentException("Property name is empty."); }

            var type = obj.GetType();
            var propertyInfo = type.GetProperty(propName);

            // Correct property with write access
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                // Check same Type
                if (ReflectionTypeChecker.CanConvertToCorrectType(propertyInfo, propVal))
                {
                    var convertedVal = ReflectionTypeChecker.ConvertToSameType(propertyInfo, propVal);
                    propertyInfo.SetValue(obj, convertedVal, null);
                }
            }
        }

        /// <summary>
        /// Set the property value using the string value.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="prop"></param>
        /// <param name="configValue"></param>
        public static void SetProperty(object obj, PropertyInfo prop, string propVal)
        {
            Guard.IsNotNull(obj, "Object containing properties to set is null");
            Guard.IsNotNull(prop, "Property not supplied.");

            // Correct property with write access
            if (prop != null && prop.CanWrite)
            {
                // Check same Type
                if (ReflectionTypeChecker.CanConvertToCorrectType(prop, propVal))
                {
                    var convertedVal = ReflectionTypeChecker.ConvertToSameType(prop, propVal);
                    prop.SetValue(obj, convertedVal, null);
                }
            }
        }

        /// <summary>
        /// Get the property value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object obj, string propName)
        {
            Guard.IsNotNull(obj, "Must provide object to get it's property.");
            Guard.IsTrue(!string.IsNullOrEmpty(propName), "Must provide property name to get property value.");

            propName = propName.ToLower().Trim();

            var property = obj.GetType().GetProperty(propName);
            if (property == null)
            {
                return null;
            }

            return property.GetValue(obj, null);
        }

        /// <summary>
        /// Get all the property values.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IList<object> GetPropertyValues(object obj, IList<string> properties)
        {
            IList<object> propertyValues = new List<object>();

            foreach (var property in properties)
            {
                var propInfo = obj.GetType().GetProperty(property);
                var val = propInfo.GetValue(obj, null);
                propertyValues.Add(val);
            }
            return propertyValues;
        }

        /// <summary>
        /// Get all the properties.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetProperties(object obj, string propsDelimited)
        {
            return GetProperties(obj.GetType(), propsDelimited.Split(','));
        }

        /// <summary>
        /// Get all the properties.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetProperties(Type type, string[] props)
        {
            var allProps = type.GetProperties();
            var propToGet = new List<PropertyInfo>();
            var propsMap = props.ToDictionary<string>();
            foreach (var prop in allProps)
            {
                if (propsMap.ContainsKey(prop.Name))
                {
                    propToGet.Add(prop);
                }
            }
            return propToGet;
        }

        /// <summary>
        /// Gets the property value safely, without throwing an exception.
        /// If an exception is caught, null is returned.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        public static object GetPropertyValueSafely(object obj, PropertyInfo propInfo)
        {
            Guard.IsNotNull(obj, "Must provide object to get it's property.");
            if (propInfo == null)
            {
                return null;
            }

            object result = null;
            try
            {
                result = propInfo.GetValue(obj, null);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Gets all the properties of the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetAllProperties(object obj, Predicate<PropertyInfo> criteria)
        {
            if (obj == null)
            { return null; }
            return GetProperties(obj.GetType(), criteria);
        }

        /// <summary>
        /// Get the
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetProperties(Type type, Predicate<PropertyInfo> criteria)
        {
            IList<PropertyInfo> allProperties = new List<PropertyInfo>();
            var properties = type.GetProperties();
            if (properties == null || properties.Length == 0)
            { return null; }

            // Now check for all writeable properties.
            foreach (var property in properties)
            {
                // Only include writable properties and ones that are not in the exclude list.
                var okToAdd = (criteria == null) ? true : criteria(property);
                if (okToAdd)
                {
                    allProperties.Add(property);
                }
            }
            return allProperties;
        }

        /// <summary>
        /// Gets all the properties of the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetWritableProperties(object obj, StringDictionary propsToExclude)
        {
            var props = ReflectionUtils.GetWritableProperties(obj.GetType(),
                 delegate (PropertyInfo property)
                 {
                     var okToAdd = propsToExclude == null ? property.CanWrite : (property.CanWrite && !propsToExclude.ContainsKey(property.Name));
                     return okToAdd;
                 });
            return props;
        }

        /// <summary>
        /// Gets all the properties of the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetProperties(StringDictionary propsToExclude, Type typ)
        {
            var props = ReflectionUtils.GetWritableProperties(typ,
                 delegate (PropertyInfo property)
                 {
                     var okToAdd = propsToExclude == null ? true : (!propsToExclude.ContainsKey(property.Name));
                     return okToAdd;
                 });
            return props;
        }

        /// <summary>
        /// Gets all the properties of the object as dictionary of property names to propertyInfo.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, PropertyInfo> GetPropertiesAsMap(object obj, Predicate<PropertyInfo> criteria)
        {
            var matchedProps = GetProperties(obj.GetType(), criteria);
            IDictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>();

            // Now check for all writeable properties.
            foreach (var prop in matchedProps)
            {
                props.Add(prop.Name, prop);
            }
            return props;
        }

        /// <summary>
        /// Get the propertyInfo of the specified property name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            var props = GetProperties(type,
                delegate (PropertyInfo property)
                {
                    return property.Name == propertyName;
                });
            return props[0];
        }

        /// <summary>
        /// Gets a list of all the writable properties of the class associated with the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <remarks>This method does not take into account, security, generics, etc.
        /// It only checks whether or not the property can be written to.</remarks>
        /// <returns></returns>
        public static IList<PropertyInfo> GetWritableProperties(Type type, Predicate<PropertyInfo> criteria)
        {
            var props = ReflectionUtils.GetProperties(type,
                 delegate (PropertyInfo property)
                 {
                     // Now determine if it can be added based on criteria.
                     var okToAdd = (criteria == null) ? property.CanWrite : (property.CanWrite && criteria(property));
                     return okToAdd;
                 });
            return props;
        }

        /// <summary>
        /// Invokes the method on the object provided.
        /// </summary>
        /// <param name="obj">The object containing the method to invoke</param>
        /// <param name="methodName">arguments to the method.</param>
        /// <param name="args"></param>
        public static object InvokeMethod(object obj, string methodName, object[] parameters)
        {
            Guard.IsNotNull(methodName, "Method name not provided.");
            Guard.IsNotNull(obj, "Can not invoke method on null object");

            methodName = methodName.Trim();

            // Validate.
            if (string.IsNullOrEmpty(methodName))
            { throw new ArgumentException("Method name not provided."); }

            var method = obj.GetType().GetMethod(methodName);
            var output = method.Invoke(obj, parameters);
            return output;
        }
    }
}