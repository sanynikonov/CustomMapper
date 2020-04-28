using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
    public class ModelMapper : IMapper
    {
        private IDictionary<(Type, Type), IEnumerable<string>> propertiesIntersection;

        public ModelMapper()
        {
            propertiesIntersection = new Dictionary<(Type, Type), IEnumerable<string>>();
        }

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
            var typesPair = (typeof(TSource), typeof(TDestination));

            if (!propertiesIntersection.ContainsKey(typesPair))
            {
                var propertiesNamesIntersection = GetPropertiesIntersection<TSource, TDestination>();

                propertiesIntersection.Add(typesPair, propertiesNamesIntersection);
            }

            var destination = Activator.CreateInstance<TDestination>();

            foreach (var propertyName in propertiesIntersection[typesPair])
            {
                var propSource = typeof(TSource).GetProperty(propertyName);
                var propDestination = typeof(TDestination).GetProperty(propertyName);

                propDestination.SetValue(destination, propSource.GetValue(source));
            }

            return destination;
        }
    }
}
