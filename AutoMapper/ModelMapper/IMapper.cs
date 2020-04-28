using System;
using System.Collections.Generic;
using System.Text;

namespace ModelMapper
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
