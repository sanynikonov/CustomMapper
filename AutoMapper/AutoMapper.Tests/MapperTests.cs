using AutoMapper.Tests.TestData;
using NUnit.Framework;

namespace AutoMapper.Tests
{
    public class Tests
    {
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new Mapper();
        }

        [TestCase(1, 10, "Alex")]
        [TestCase(1000, -100, "")]
        [TestCase(int.MaxValue, int.MinValue, null)]
        public void Map_EmployeeToEmployeeDto_MapsAllProperties(int id, int age, string name)
        {
            var employee = new Employee { Id = id, Age = age, Name = name };

            var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

            Assert.AreEqual(employee.Id, employeeDto.Id);
            Assert.AreEqual(employee.Age, employeeDto.Age);
            Assert.AreEqual(employee.Name, employeeDto.Name);
        }
    }
}