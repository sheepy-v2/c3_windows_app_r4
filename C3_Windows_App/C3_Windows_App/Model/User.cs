﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rank { get; set; }
        public int Balance { get; set; }





        public User(string name, string email, string password, int balance, string rank)

        {
            Name = name;
            Email = email;
            Password = password;
            Balance = balance;
            Rank = rank;
        }
    }
}
