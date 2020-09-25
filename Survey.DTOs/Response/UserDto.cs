using System;
using System.Collections.Generic;

namespace Survey.DTOs.Response
{
    public class UserDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
