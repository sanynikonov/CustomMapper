using System;
using System.Reflection;

namespace AutoMapper
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }
    }
}
