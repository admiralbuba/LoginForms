using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class UserData
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public Roles Role { get; set; }
        public DateTime? LastFailedAttemptDate { get; set; }
    }
}
