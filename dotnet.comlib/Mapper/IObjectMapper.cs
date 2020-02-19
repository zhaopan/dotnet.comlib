using System.Collections.Generic;

namespace Comlib.Mapper
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : class where TSource : class;

        List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source) where TDestination : class where TSource : class;
    }
}