using System;
using System.Collections.Generic;
using System.Text;

namespace Kickstart.models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User()
        {

        }
    }
}
