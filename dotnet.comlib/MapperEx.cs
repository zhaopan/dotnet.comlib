/*
 * Copyright (c) 2020 WeiKe
 * Revision: 0.0.0.1
 * CLR: 4.0.30319.42000
 * Date 8/10/2020 5:21:33 PM
 * Name MapperEx
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System;
using System.Collections.Generic;

using AutoMapper;

namespace Comlib
{
    /// <summary>
    /// AutoMapper扩展
    /// <para>线程安全的Mapper</para>
    /// </summary>
    public static class MapperEx
    {
        /// <summary>
        /// 单个对象映射
        /// <![CDATA[
        /// ex.MapTo<Exception>();
        /// ]]>
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object src)
            where TDestination : class, new()
        {
            if (src == null)
                return default;
            var config = new MapperConfiguration(cfg => cfg.CreateMap(src.GetType(), typeof(TDestination)));
            var mapper = config.CreateMapper();

            return mapper.Map<TDestination>(src);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// <![CDATA[
        /// CopyTo<Exception, AggregateException>(ex, argsEx);
        /// ]]>
        /// </summary>
        /// <typeparam name="TSource">Source type to use</typeparam>
        /// <typeparam name="TDestination">Dsetination type</typeparam>
        /// <param name="src">Source object to map from</param>
        /// <param name="opt">Destination object to map into</param>
        /// <returns>The mapped destination object, same instance as the destination object</returns>
        public static TDestination CopyTo<TSource, TDestination>(TSource src, TDestination opt)
            where TDestination : class, new()
            where TSource : class
        {
            if (src == null)
            {
                return default;
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map(src, opt);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// <![CDATA[
        /// CopyToEx<Exception, AggregateException>(ex, argsEx, (cfg) => {
        ///     cfg.CreateMap<Exception, AggregateException>()
        ///         .ForMember(src => src.AddItems, opt => opt.MapFrom(s => s.AddItems))
        ///         .ForMember(src => src.Details, opt => opt.MapFrom(s => s.Details));
        /// });
        /// ]]>
        /// </summary>
        /// <typeparam name="TSource">Source type to use</typeparam>
        /// <typeparam name="TDestination">Dsetination type</typeparam>
        /// <param name="src">Source object to map from</param>
        /// <param name="opt">Destination object to map into</param>
        /// <param name="createMap">Mapper.Configuration</param>
        /// <returns>The mapped destination object, same instance as the destination object</returns>
        public static TDestination CopyToEx<TSource, TDestination>(TSource src, TDestination opt, Action<IMapperConfigurationExpression> createMap)
            where TDestination : class, new()
            where TSource : class
        {
            if (src == null)
            {
                return default;
            }

            if (createMap == null)
            {
                return CopyTo(src, opt);
            }
            else
            {
                var config = new MapperConfiguration(createMap);
                var mapper = config.CreateMapper();
                return mapper.Map(src, opt);
            }
        }

        /// <summary>
        /// 集合列表类型映射
        /// <![CDATA[
        /// source.MapToList<Exception, AggregateException>();
        /// ]]>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> src)
            where TDestination : class, new()
            where TSource : class
        {
            if (src == null)
            {
                return null;
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(src);
        }
    }
}