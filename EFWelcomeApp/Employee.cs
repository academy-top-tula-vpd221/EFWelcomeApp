using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWelcomeApp
{
    public class Employee
    {
        public int Id { get; set; }
        //public int EmployeeId { get; set; }
        public string Name { get; set; } = "";
        public int? Age { get; set; }
        public Company? Company { get; set; }
    }
}
