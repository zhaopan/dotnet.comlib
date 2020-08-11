/*
 * Copyright (c) 2020 ZP
 * Revision: 0.0.0.1
 * CLR: 4.0.30319.42000
 * Date 8/11/2020 9:39:44 AM
 * Name ReflectionTypeChecker
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System;
using System.Reflection;

namespace Comlib.Reflection
{
    /// <summary>
    /// Various reflection based utility methods.
    /// </summary>
    public class ReflectionTypeChecker
    {
        /// <summary>
        /// Checks whether or not the
        /// </summary>
        /// <param name="propInfo">The property represnting the type to convert
        /// val to</param>
        /// <param name="val">The value to test for conversion to the type
        /// associated with the property</param>
        /// <returns></returns>
        public static bool CanConvertTo<T>(string val)
        {
            return CanConvertTo(typeof(T), val);
        }

        /// <summary>
        /// Checks whether or not the
        /// </summary>
        /// <param name="propInfo">The property represnting the type to convert
        /// val to</param>
        /// <param name="val">The value to test for conversion to the type
        /// associated with the property</param>
        /// <returns></returns>
        public static bool CanConvertTo(Type type, string val)
        {
            // Data could be passed as string value.
            // Try to change type to check type safety.
            try
            {
                if (type == typeof(int))
                {
                    if (int.TryParse(val, out var result))
                    {
                        return true;
                    }

                    return false;
                }
                else if (type == typeof(string))
                {
                    return true;
                }
                else if (type == typeof(double))
                {
                    if (double.TryParse(val, out var d))
                    {
                        return true;
                    }

                    return false;
                }
                else if (type == typeof(long))
                {
                    if (long.TryParse(val, out var l))
                    {
                        return true;
                    }

                    return false;
                }
                else if (type == typeof(float))
                {
                    if (float.TryParse(val, out var f))
                    {
                        return true;
                    }

                    return false;
                }
                else if (type == typeof(bool))
                {
                    if (bool.TryParse(val, out var b))
                    {
                        return true;
                    }

                    return false;
                }
                else if (type == typeof(DateTime))
                {
                    var d = DateTime.MinValue;
                    if (DateTime.TryParse(val, out d))
                    {
                        return true;
                    }

                    return false;
                }
                else if (type.BaseType == typeof(Enum))
                {
                    Enum.Parse(type, val, true);
                }
            }
            catch (Exception)
            {
                return false;
            }

            //Conversion worked.
            return true;
        }

        /// <summary>
        /// Check to see if can convert to appropriate type
        /// </summary>
        /// <param name="propInfo"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool CanConvertToCorrectType(PropertyInfo propInfo, object val)
        {
            // Data could be passed as string value.
            // Try to change type to check type safety.
            try
            {
                if (propInfo.PropertyType == typeof(int))
                {
                    var i = Convert.ToInt32(val);
                }
                else if (propInfo.PropertyType == typeof(double))
                {
                    var d = Convert.ToDouble(val);
                }
                else if (propInfo.PropertyType == typeof(long))
                {
                    double l = Convert.ToInt64(val);
                }
                else if (propInfo.PropertyType == typeof(float))
                {
                    double f = Convert.ToSingle(val);
                }
                else if (propInfo.PropertyType == typeof(bool))
                {
                    var b = Convert.ToBoolean(val);
                }
                else if (propInfo.PropertyType == typeof(DateTime))
                {
                    var d = Convert.ToDateTime(val);
                }
                else if (propInfo.PropertyType.BaseType == typeof(Enum) && val is string)
                {
                    Enum.Parse(propInfo.PropertyType, (string)val, true);
                }
            }
            catch (Exception)
            {
                return false;
            }

            //Conversion worked.
            return true;
        }

        /// <summary>
        /// Checks whether or not the
        /// </summary>
        /// <param name="propInfo">The property represnting the type to convert
        /// val to</param>
        /// <param name="val">The value to test for conversion to the type
        /// associated with the property</param>
        /// <returns></returns>
        public static bool CanConvertToCorrectType(PropertyInfo propInfo, string val)
        {
            // Data could be passed as string value.
            // Try to change type to check type safety.
            try
            {
                if (propInfo.PropertyType == typeof(int))
                {
                    if (int.TryParse(val, out var result))
                    {
                        return true;
                    }

                    return false;
                }
                else if (propInfo.PropertyType == typeof(string))
                {
                    return true;
                }
                else if (propInfo.PropertyType == typeof(double))
                {
                    if (double.TryParse(val, out var d))
                    {
                        return true;
                    }

                    return false;
                }
                else if (propInfo.PropertyType == typeof(long))
                {
                    if (long.TryParse(val, out var l))
                    {
                        return true;
                    }

                    return false;
                }
                else if (propInfo.PropertyType == typeof(float))
                {
                    if (float.TryParse(val, out var f))
                    {
                        return true;
                    }

                    return false;
                }
                else if (propInfo.PropertyType == typeof(bool))
                {
                    if (bool.TryParse(val, out var b))
                    {
                        return true;
                    }

                    return false;
                }
                else if (propInfo.PropertyType == typeof(DateTime))
                {
                    var d = DateTime.MinValue;
                    if (DateTime.TryParse(val, out d))
                    {
                        return true;
                    }

                    return false;
                }
                else if (propInfo.PropertyType.BaseType == typeof(Enum))
                {
                    Enum.Parse(propInfo.PropertyType, val, true);
                }
            }
            catch (Exception)
            {
                return false;
            }

            //Conversion worked.
            return true;
        }

        /// <summary>
        /// Convert the val from string type to the same time as the property.
        /// </summary>
        /// <param name="propInfo">Property representing the type to convert to</param>
        /// <param name="val">val to convert</param>
        /// <returns>converted value with the same time as the property</returns>
        public static object ConvertToSameType(PropertyInfo propInfo, object val)
        {
            object convertedType = null;

            if (propInfo.PropertyType == typeof(int))
            {
                convertedType = Convert.ChangeType(val, typeof(int));
            }
            else if (propInfo.PropertyType == typeof(double))
            {
                convertedType = Convert.ChangeType(val, typeof(double));
            }
            else if (propInfo.PropertyType == typeof(long))
            {
                convertedType = Convert.ChangeType(val, typeof(long));
            }
            else if (propInfo.PropertyType == typeof(float))
            {
                convertedType = Convert.ChangeType(val, typeof(float));
            }
            else if (propInfo.PropertyType == typeof(bool))
            {
                convertedType = Convert.ChangeType(val, typeof(bool));
            }
            else if (propInfo.PropertyType == typeof(DateTime))
            {
                convertedType = Convert.ChangeType(val, typeof(DateTime));
            }
            else if (propInfo.PropertyType == typeof(string))
            {
                convertedType = Convert.ChangeType(val, typeof(string));
            }
            else if (propInfo.PropertyType.BaseType == typeof(Enum) && val is string)
            {
                convertedType = Enum.Parse(propInfo.PropertyType, (string)val, true);
            }
            return convertedType;
        }

        /// <summary>
        /// Determine if the type of the property and the val are the same
        /// </summary>
        /// <param name="propInfo"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsSameType(PropertyInfo propInfo, object val)
        {
            // Quick Validation.
            if (propInfo.PropertyType == typeof(int) && val is int)
            { return true; }
            if (propInfo.PropertyType == typeof(bool) && val is bool)
            { return true; }
            if (propInfo.PropertyType == typeof(string) && val is string)
            { return true; }
            if (propInfo.PropertyType == typeof(double) && val is double)
            { return true; }
            if (propInfo.PropertyType == typeof(long) && val is long)
            { return true; }
            if (propInfo.PropertyType == typeof(float) && val is float)
            { return true; }
            if (propInfo.PropertyType == typeof(DateTime) && val is DateTime)
            { return true; }
            if (propInfo.PropertyType is object && propInfo.PropertyType.GetType() == val.GetType())
            { return true; }

            return false;
        }
    }
}