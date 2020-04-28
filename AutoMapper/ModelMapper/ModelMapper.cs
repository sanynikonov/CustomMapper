using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace ModelMapper
{
    public class ModelMapper : IMapper
    {
        private readonly IDictionary<(Type, Type), IEnumerable<string>> propertiesIntersection;

        public ModelMapper()
        {
            propertiesIntersection = new Dictionary<(Type, Type), IEnumerable<string>>();
        }

        private IEnumerable<string> IntersectPropertyNames<TSource, TDestination>()
        {
            var sourceTypeProperties = typeof(TSource).GetProperties();
            var destinationTypeProperties = typeof(TDestination).GetProperties();

            return sourceTypeProperties
                .Select(x => x.Name)
                .Intersect(destinationTypeProperties
                    .Select(x => x.Name));
        }

        private IEnumerable<string> GetCommonProperties<TSource, TDestination>()
        {
            var typesPair = (typeof(TSource), typeof(TDestination));

            if (!propertiesIntersection.ContainsKey(typesPair))
            {
                var propertiesNamesIntersection = IntersectPropertyNames<TSource, TDestination>();

                propertiesIntersection.Add(typesPair, propertiesNamesIntersection);
            }

            return propertiesIntersection[typesPair];
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            var commonProperties = GetCommonProperties<TSource, TDestination>();

            var destination = Activator.CreateInstance<TDestination>();

            foreach (var propertyName in commonProperties)
            {
                var propSource = typeof(TSource).GetProperty(propertyName);
                var propDestination = typeof(TDestination).GetProperty(propertyName);

                propDestination.SetValue(destination, propSource.GetValue(source));
            }

            return destination;
        }
    }
}
