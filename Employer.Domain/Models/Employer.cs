using System;
using System.Collections.Generic;
using System.Text;

namespace Employer.Domain.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BeginningOfWork { get; set; }
    }
}
