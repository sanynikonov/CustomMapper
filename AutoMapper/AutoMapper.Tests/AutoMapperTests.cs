using NUnit.Framework;
using AutoMapper;
using ModelMapper.Tests.TestData;

namespace AutoMapper.Tests
{
    public class AutoMapperTests
    {
        private IMapper mapper;

        public AutoMapperTests()
        {
            var conf = new MapperConfiguration(opt => opt.CreateMap<Employee, EmployeeDto>().ReverseMap());
            mapper = new Mapper(conf);
        }

        [TestCase("Abc")]
        public void Map_EmployeeToEmployeeDto_MapsPropertiesWithSameNames(string name)
        {
            var master = new Employee { Name = name };
            var employee = new Employee { Name = name, Master = master };

            var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

            Assert.AreSame(employee.Master, employeeDto.Master);
        }

        [TestCase("Greta", "Van", "Fleet")]
        public void Map_EmployeeToEmployeeDto_MapsCollectionsWithSameNames(params string[] children)
        {
            var employee = new Employee { Children = children };

            var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

            Assert.AreSame(employee.Children, employeeDto.Children);
        }
    }
}