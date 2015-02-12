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

        public static string AdminFirstName
        {
            get
            {
                return "Andrey";
            }
        }

        public static string AdminSecondName
        {
            get
            {
                return "Kiselyov";
            }
        }

        public static string AdminUserName
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
                return new List<string> { "Admin", "User" };
            }
        }
    }
}
