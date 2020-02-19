using System;
using System.Collections.Generic;
using AutoMapper;

namespace Comlib.Mapper
{
    internal class ObjectAutoMapper : IObjectMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) throw new ArgumentNullException();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        public List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) throw new ArgumentNullException();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }
    }
}