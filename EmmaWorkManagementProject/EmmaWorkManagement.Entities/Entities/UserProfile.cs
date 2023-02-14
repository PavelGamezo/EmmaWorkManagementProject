using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.Entities.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? About { get; set; }
        public DateTime Registered { get; set; }
        public string Email { get; set; }
        public byte[]? Avatar { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
