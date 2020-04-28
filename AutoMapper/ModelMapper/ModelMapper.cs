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

        private IEnumerable<string> IntersectPropertyNames(Type sourceType, Type destinationType)
        {
            var sourceTypeProperties = sourceType.GetProperties();
            var destinationTypeProperties = destinationType.GetProperties();

            return sourceTypeProperties
                .Select(x => x.Name)
                .Intersect(destinationTypeProperties
                    .Select(x => x.Name));
        }

        private IEnumerable<string> GetCommonProperties(Type sourceType, Type destinationType)
        {
            var typesPair = (sourceType, destinationType);

            if (!propertiesIntersection.ContainsKey(typesPair))
            {
                var propertiesNamesIntersection = IntersectPropertyNames(sourceType, destinationType);

                propertiesIntersection.Add(typesPair, propertiesNamesIntersection);
            }

            return propertiesIntersection[typesPair];
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            var commonProperties = GetCommonProperties(sourceType, destinationType);

            var destination = Activator.CreateInstance(destinationType);

            foreach (var propertyName in commonProperties)
            {
                var propSource = sourceType.GetProperty(propertyName);
                var propDestination = destinationType.GetProperty(propertyName);

                var value = propSource.GetValue(source);

                if (HaveEntityAndDtoRelation(propSource, propDestination))
                {
                    var mappedValue = Map(value, propSource.PropertyType, propDestination.PropertyType);
                    propDestination.SetValue(destination, mappedValue);
                }
                else
                {
                    propDestination.SetValue(destination, value);
                }
            }

            return destination;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return (TDestination)Map(source, typeof(TSource), typeof(TDestination));
        }

        private bool HaveEntityAndDtoRelation(PropertyInfo first, PropertyInfo second)
        {
            var firstName = first.PropertyType.Name.ToLower();
            var secondName = second.PropertyType.Name.ToLower();

            return firstName == secondName + "dto" 
                || firstName + "dto" == secondName;
        }

        private object GetPropertyValue(object source, PropertyInfo property)
        {
            return property.GetValue(source);
        }
    }
}
