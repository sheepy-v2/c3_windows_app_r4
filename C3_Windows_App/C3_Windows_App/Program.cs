using C3_Windows_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App
{
    /// <summary>
    /// The Program class
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main method
        /// </summary>
        /// <param name="args"></param>
        static public void Main(String[] args)
        { 
            GambleApp app = new GambleApp();

            Userdata user = new Userdata();


            app.run();
        }
    }
}