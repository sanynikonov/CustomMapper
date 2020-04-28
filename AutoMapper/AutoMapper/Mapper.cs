using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
    public class Mapper : IMapper
    {
        private IEnumerable<string> GetPropertiesIntersection<TSource, TDestination>()
        {
            var sourceTypeProperties = typeof(TSource).GetProperties();
            var destinationTypeProperties = typeof(TDestination).GetProperties();

            return sourceTypeProperties
                .Select(x => x.Name)
                .Intersect(destinationTypeProperties
                    .Select(x => x.Name));
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            var propertiesNamesIntersection = GetPropertiesIntersection<TSource, TDestination>();

            var destination = Activator.CreateInstance<TDestination>();

            foreach (var propertyName in propertiesNamesIntersection)
            {
                var propSource = typeof(TSource).GetProperty(propertyName);
                var propDestination = typeof(TDestination).GetProperty(propertyName);

                propDestination.SetValue(destination, propSource.GetValue(source));
            }

            return destination;
        }
    }
}
