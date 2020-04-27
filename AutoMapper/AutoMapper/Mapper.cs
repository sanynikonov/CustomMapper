using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var sourceTypeProperties = sourceType.GetProperties();
            var destinationTypeProperties = destinationType.GetProperties();

            var propertiesNamesIntersection = sourceTypeProperties
                .Select(x => x.Name)
                .Intersect(
                    destinationTypeProperties.Select(x => x.Name));

            var destination = Activator.CreateInstance(destinationType);

            foreach (var propertyName in propertiesNamesIntersection)
            {
                var propSource = sourceTypeProperties.First(x => x.Name == propertyName);
                var propDestination = destinationTypeProperties.First(x => x.Name == propertyName);

                propDestination.SetValue(destination, propSource.GetValue(source));
            }

            return (TDestination)destination;
        }
    }
}
