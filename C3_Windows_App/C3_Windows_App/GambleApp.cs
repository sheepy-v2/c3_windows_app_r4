using C3_Windows_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App
{
    internal class GambleApp
    {
        WindowsAppDataContext WindowsAppDataContext;

        public GambleApp()
        {
            WindowsAppDataContext = new WindowsAppDataContext();
        }

        internal void run()
        {
            string userInput = "";

            while (userInput.ToLower() != "x")
            {
                
            }
        }
    }
}
