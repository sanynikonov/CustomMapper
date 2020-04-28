using System;
using System.Collections.Generic;
using System.Text;

namespace ModelMapper.Tests.TestData
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        
        public bool Sex { get; set; }

        public double MonthSalary { get; set; }

        public ICollection<string> Children { get; set; }

        public Employee()
        {
            Children = new List<string>();
        }
    }
}
