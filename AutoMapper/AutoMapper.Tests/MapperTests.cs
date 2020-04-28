using AutoMapper.Tests.TestData;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutoMapper.Tests
{
    public class Tests
    {
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new ModelMapper();
        }

        [TestCase(1, 10, "Alex")]
        [TestCase(1000, -100, "")]
        [TestCase(int.MaxValue, int.MinValue, null)]
        public void Map_EmployeeToEmployeeDto_MapsPropertiesWithSameNames(int id, int age, string name)
        {
            var employee = new Employee { Id = id, Age = age, Name = name };

            var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

            Assert.AreEqual(employee.Id, employeeDto.Id);
            Assert.AreEqual(employee.Age, employeeDto.Age);
            Assert.AreEqual(employee.Name, employeeDto.Name);
        }

        [TestCase(100)]
        [TestCase(9999.999)]
        [TestCase(-100)]
        public void Map_EmployeeToEmployeeDto_DoesntMapPropertiesWithDifferentNames(double salary)
        {
            var employee = new Employee { MonthSalary = salary };

            var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

            Assert.AreNotEqual(employee.MonthSalary, employeeDto.YearSalary);
        }

        [TestCase()]
        [TestCase("Dina", "Alex")]
        [TestCase("Dina", "Alex", "Michelle", "Blueford")]
        [TestCase("Greta", "Van", "Fleet")]
        public void Map_EmployeeToEmployeeDto_MapsCollectionsWithSameNames(params string[] children)
        {
            var employee = new Employee { Children = children };

            var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

            Assert.AreEqual(employee.Children, employeeDto.Children);
        }
    }
}