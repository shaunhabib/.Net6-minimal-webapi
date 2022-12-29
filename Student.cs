using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minimal_webapi
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public int Roll { get; set; }
        public long RegistrationNo { get; set; }
    }
}