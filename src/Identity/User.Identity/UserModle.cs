using System;
using System.Collections.Generic;

namespace Super.User.Identity
{
    public class UserModle
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<string> Addresses { get; set; }
    }
}