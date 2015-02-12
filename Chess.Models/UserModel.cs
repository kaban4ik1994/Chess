using System;
using System.Collections.Generic;

namespace Chess.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public Guid Token { get; set; }
    }
}
