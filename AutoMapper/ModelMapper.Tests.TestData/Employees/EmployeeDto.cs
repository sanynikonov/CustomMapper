﻿using System;
using System.Collections.Generic;

namespace ModelMapper.Tests.TestData
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public double YearSalary { get; set; }

        public ICollection<string> Children { get; set; }

        public Employee Master { get; set; }

        public EmployeeDto()
        {
            Children = new List<string>();
        }
    }
}
