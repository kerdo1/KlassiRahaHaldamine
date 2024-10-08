using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlassiRahaHaldamine.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Amount { get; set; }
        public string ContactName { get; set; }
        public int ContactNumber { get; set; }

        public Student()
        {
        }
    }
}
