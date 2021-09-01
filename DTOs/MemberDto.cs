using System;
using System.Collections.Generic;

namespace Identity_JWT_API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}