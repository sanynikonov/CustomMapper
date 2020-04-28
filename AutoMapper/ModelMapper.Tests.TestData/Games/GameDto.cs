using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelMapper.Tests.TestData
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public AuthorDto Author { get; set; }
    }
}