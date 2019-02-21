using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringSystemApp.Models.Login
{
    //creating get set properties for attributes needed for the login system
    public class Users
    {
        public string name { get; set; }
        public string house { get; set; }
        public Users(string name, string Password)
        {
            this.name = name;
            this.house = house;
        }
        //creating get set properties for attributes needed for the login system
        public class MonitorUser
      {
         public string Name { get; set; }
         public string Email { get; set; }
         public string Phone { get; set; }
         public string Password { get; set; }
         public int CheckInterevalInSeconds { get; set; }
         public string Servers { get; set; }
         public string Token { get; set; }

         public bool RememberMe { get; set; }
      }

   }
}
