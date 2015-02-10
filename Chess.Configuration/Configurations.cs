using System.Collections.Generic;

namespace Chess.Configuration
{
    public static class Configurations
    {

        public static string AdminEmail
        {
            get
            {
                return "Admin@gmail.com";
            }
        }

        public static string AdminPassword
        {
            get
            {
                return "Admin";
            }
        }

        public static List<string> Roles
        {
            get
            {
                return new List<string> { "Admin", "Manager" };
            }
        }
    }
}
