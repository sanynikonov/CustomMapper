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

        [Test]
        public void Map_EmployeeToEmployeeDto_MapsAllProperties()
        {
            var employee = new Employee { Id = 1, Age = 10, Name = "Alex" };

            var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

            if (employee.Id == employeeDto.Id
                && employee.Age == employeeDto.Age
                && employee.Name == employeeDto.Name)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}