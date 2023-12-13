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

        public Userdata() 
        {
            WindowsAppDataContext = new WindowsAppDataContext();

           WindowsAppDataContext.Users.Add(new User("test", "test@test.nl", "test", 50, "Admin"));

            WindowsAppDataContext.SaveChanges();
            
        }
    }


}
