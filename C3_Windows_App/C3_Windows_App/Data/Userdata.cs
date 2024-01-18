using C3_Windows_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App.Data
{
    public class Userdata
    {
        WindowsAppDataContext WindowsAppDataContext;
        bool AccountMade = false;
        public Userdata() 
        {
            WindowsAppDataContext = new WindowsAppDataContext();
            foreach(User user in WindowsAppDataContext.Users)
            {
                if (user.Email == "test@test.nl")
                {
                    AccountMade = true;
                }
            }
            if (!AccountMade)
            {
                WindowsAppDataContext.Users.Add(new User("test", "test@test.nl", "test", 50, "Admin"));
            }


            WindowsAppDataContext.SaveChanges();
            
        }
    }


}
