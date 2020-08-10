/*
 * Copyright (c) 2020 WeiKe
 * Revision: 2.1
 * CLR: 4.0.30319.42000
 * Date 2/16/2020 12:55:19 AM
 * Name JsonTools
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Comlib
{
    /// <summary>
    /// JSON 序列化
    /// </summary>
    public static class JsonTools
    {
        #region Static Fields

        /// <summary>
        /// The date time format.
        /// </summary>
        public static string DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";

        #endregion Static Fields

        #region Public Methods and Operators

        /// <summary>
        /// 将 json 格式的串转化为相应的实体
        /// </summary>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <param name="json">
        /// json串
        /// </param>
        /// <returns>
        /// T
        /// </returns>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, SerializerSetting());
        }

        /// <summary>
        /// 将 json 格式的串转化为相应的实体
        /// </summary>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <param name="json">
        /// json串
        /// </param>
        /// <returns>
        /// T
        /// </returns>
        public static T Deserialize<T>(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// 将实体转化为 json 格式的字符串
        /// </summary>
        /// <param name="data">
        /// entity
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize(object data)
        {
            var setting = SerializerSetting();
            setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(data, Formatting.Indented, setting);
        }

        /// <summary>
        /// 序列号
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SerializeSimple(object data)
        {
            var setting = SerializerSetting();
            setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(data, Formatting.None, setting);
        }

        /// <summary>
        /// 将实体转化为 json 格式的字符串
        /// </summary>
        /// <param name="data">
        /// entity
        /// </param>
        /// <param name="dateFormatString">日期格式</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize(object data, string dateFormatString)
        {
            var settings = SerializerSetting();
            if (!string.IsNullOrEmpty(dateFormatString))
                settings.DateFormatString = dateFormatString;
            return JsonConvert.SerializeObject(data, Formatting.Indented, settings);
        }

        /// <summary>
        /// The to object.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object ToObject(object o)
        {
            if (o == null)
            {
                return null;
            }

            if (o is string)
            {
                // 判断是否符合2010-09-02T10:00:00的格式
                string s = o.ToString();
                if (s.Length == 19 && s[10] == 'T' && s[4] == '-' && s[13] == ':')
                {
                    o = Convert.ToDateTime(o);
                }
            }
            else if (o is JObject)
            {
                var jo = o as JObject;

                var h = new Hashtable();

                foreach (var entry in jo)
                {
                    h[entry.Key] = ToObject(entry.Value);
                }

                o = h;
            }
            else if (o is IList)
            {
                var list = new ArrayList();
                list.AddRange(o as IList);
                int i = 0, l = list.Count;
                for (; i < l; i++)
                {
                    list[i] = ToObject(list[i]);
                }

                o = list;
            }
            else if (typeof(JValue) == o.GetType())
            {
                var v = (JValue)o;
                o = ToObject(v.Value);
            }

            return o;
        }

        #endregion Public Methods and Operators

        #region Methods

        /// <summary>
        /// json 序列化设置
        /// </summary>
        /// <returns>
        /// The <see cref="JsonSerializerSettings" />.
        /// </returns>
        public static JsonSerializerSettings SerializerSetting()
        {
            var setting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,

                ContractResolver = new DefaultContractResolver
                {
                    IgnoreSerializableInterface = true,
                    IgnoreSerializableAttribute = true,
                },
                Error = delegate (object obj, ErrorEventArgs args)
                {
                    //args.ErrorContext.Error.LogError(); TODO log
                    args.ErrorContext.Handled = true;
                }
            };

            return setting;
        }

        #endregion Methods

        #region JObject

        /// <summary>
        /// 将Json字符串转换成JObject, 使之可用Linq方式取数据
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static JObject ToJsonObj(string jsonStr)
        {
            return JObject.Parse(jsonStr);
        }

        /// <summary>
        /// 将对象转成JObject对象, 使之可用Linq方式取数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="transmit">是否用string作为转换过渡</param>
        /// <returns></returns>
        public static JObject ToJsonObj(object obj, bool transmit = true)
        {
            if (!transmit)
            {
                return JObject.FromObject(obj);
            }
            else
            {
                return ToJsonObj(Serialize(obj));
            }
        }

        /// <summary>
        /// 将集合型Json字符串转换成JArray, 使之可用Linq方式取数据
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static JArray ToJsonArray(string jsonStr)
        {
            return JArray.Parse(jsonStr);
        }

        /// <summary>
        /// 将集合型对象转成JArray，使之可用Linq方式取数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JArray ToJsonArray(object obj)
        {
            return JArray.FromObject(obj);
        }

        /// <summary>
        /// 将Json字符串转换成指定的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <param name="dateFormatString"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T ToEntity<T>(string jsonStr, string dateFormatString = "yyyy-MM-dd", JsonSerializerSettings settings = null)
        {
            if (!string.IsNullOrWhiteSpace(dateFormatString))
            {
                settings.DateFormatString = dateFormatString;
            }
            return Deserialize<T>(jsonStr, settings);
        }

        /// <summary>
        /// JObject转换成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jObj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public static T ToEntity<T>(JObject jObj, bool mapping = true) where T : new()
        {
            if (!mapping)
            {
                return jObj == null ? default(T) : jObj.ToObject<T>();
            }
            else
            {
                T entity = new T();
                var ps = jObj.Properties().ToList();
                ps.ForEach(p =>
                {
                    PropertyInfo propertyInfo = entity.GetType().GetRuntimeProperty(p.Name);
                    if (propertyInfo != null && p.Value != null)
                    {
                        propertyInfo.SetValue(entity, ChangeType(p.Value, propertyInfo.PropertyType), null);
                    }
                });
                return entity;
            }
        }

        /// <summary>
        /// 将Json字符串转换成dynamic
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="dateFormatString"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(string jsonStr, string dateFormatString = "yyyy-MM-dd", JsonSerializerSettings settings = null)
        {
            return ToEntity<ExpandoObject>(jsonStr, dateFormatString, settings);
        }

        /// <summary>
        /// 将JObject转换成dynamic类型
        /// </summary>
        /// <param name="jObj"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(JObject jObj)
        {
            dynamic res = jObj;
            return res;
        }

        /// <summary>
        /// 移除JObject中的指定属性
        /// </summary>
        /// <param name="jObj"></param>
        /// <param name="propertyNames"></param>
        public static void JObjectExcludeFields(JObject jObj, params string[] propertyNames)
        {
            if (jObj != null)
            {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    jObj.Remove(propertyNames[i]);
                }
            }
        }

        /// <summary>
        /// 让JObject只包含指定的属性
        /// </summary>
        /// <param name="jObj"></param>
        /// <param name="propertyNames"></param>
        public static void JObjectOnlyIncludeFields(JObject jObj, params string[] propertyNames)
        {
            if (jObj != null)
            {
                if (propertyNames.Length > 0)
                {
                    var ps = jObj.Properties().ToList();
                    for (int i = 0; i < ps.Count; i++)
                    {
                        if (!propertyNames.Contains(ps[i].Name))
                        {
                            jObj.Remove(ps[i].Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 复制JObject，并移除指定的属性
        /// </summary>
        /// <param name="jObj"></param>
        /// <param name="exCludePropertyNames"></param>
        /// <returns></returns>
        public static JObject JObjectCopy(JObject jObj, params string[] exCludePropertyNames)
        {
            var res = jObj;
            if (jObj != null)
            {
                res = jObj.DeepClone() as JObject;
                JObjectExcludeFields(res, exCludePropertyNames);
            }
            return res;
        }

        /// <summary>
        /// 将JObject根据Mapper指定的映射转换第一级的属性, 变化是动态的，因此按照顺序可以转成当前不存在的属性，如：A=>NewA, B=>A顺序可以，颠倒后，B就因为A存在而不能转换
        /// </summary>
        /// <param name="jObj"></param>
        /// <param name="mapper"></param>
        /// <param name="keepOld"></param>
        public static void JObjectMapper(JObject jObj, Dictionary<string, string> mapper, bool keepOld = false)
        {
            if (jObj != null)
            {
                var ps = jObj.Properties();
                foreach (var kv in mapper)
                {
                    if (ps.FirstOrDefault(i => i.Name == kv.Value) == null
                        && ps.FirstOrDefault(i => i.Name == kv.Key) != null)
                    {
                        JToken v = jObj.GetValue(kv.Key);
                        jObj.Add(kv.Value, v);
                        if (!keepOld)
                        {
                            jObj.Remove(kv.Key);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 允许Nullable的类型转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsConstructedGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }

        #endregion JObject

        #region Extensions

        /// <summary>
        /// 对象序列化
        /// </summary>
        public static string ToJson(this object obj, Formatting jsonFormatting = Formatting.Indented, string dateFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var setting = SerializerSetting();
            setting.DateFormatString = dateFormat;
            return JsonConvert.SerializeObject(obj, jsonFormatting, setting);
        }

        #endregion Extensions
    }
}