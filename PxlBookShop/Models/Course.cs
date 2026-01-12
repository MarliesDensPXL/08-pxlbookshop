using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PxlBookShop.Models
{
    public class Course
    {
        public int Id { get; }
        public string Name { get; set; }

        public Course(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
