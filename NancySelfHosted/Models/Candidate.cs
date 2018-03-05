using System;
using System.Collections.Generic;
using System.Text;

namespace NancySelfHosted.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int ExpectedSalary { get; set; }
        public string Status { get; set; }  
    }
}
