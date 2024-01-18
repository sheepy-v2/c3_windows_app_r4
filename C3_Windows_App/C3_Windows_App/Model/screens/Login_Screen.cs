using C3_Windows_App.Data;

namespace C3_Windows_App.Model.Gamble_seperate
{
    internal class Login_Screen
    {
        User User = null;
        User currentUser;
        GambleApp gambleApp;
        private bool istrue = false;
        private string error = "";
        public Login_Screen(GambleApp gambleapp) 
        {
            gambleApp = gambleapp;
            ShowLoginMenu();
            LoginUserInput();
        }

        private void ShowLoginMenu()
        {



            Console.Clear();
            Console.WriteLine("1. login");
            Console.WriteLine("2. registreer een nieuw account");

            Console.WriteLine("X. Exit");

            gambleApp.SetInput(Helpers.Ask("Make your choice and press <ENTER>."));

        }

        private void LoginUserInput()
        {
            switch (gambleApp.GetInput())
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                default:
                    Console.WriteLine("Incorrect choice...");
                    // Invalid input
                    break;
            }
            Helpers.Pause();
        }

        private void Login()
        {
            istrue = true;
            string emailInput;
            bool emailCheck = false;
            bool passCheck = false;
            
            Console.Clear();
            while (istrue)
            {
                Console.WriteLine("uw email:");
                emailInput = Console.ReadLine();
                foreach (User user in gambleApp.GetDataContext().Users)
                {
                    if (user.Email == emailInput)
                    {
                        emailCheck = true;
                        error = "";

                        string passInput = Helpers.Ask("uw wachtwoord:");
                        if (user.Password == passInput)
                        {
                            passCheck = true;
                            User = user;
                            error = "";
                            
                            currentUser = User;

                            // Check if the user is an Admin
                            if (currentUser.Rank == "Admin")
                            {
                                Console.WriteLine($"{currentUser.Name} u bent ingelogd als Admin. Welkom!");
                            }
                            else
                            {
                                Console.WriteLine($"{currentUser.Name} u bent ingelogd");
                            }

                            if (currentUser.Rank == "Admin")
                            {
                                gambleApp.SetState("admin");
                            }
                            else
                            {
                                gambleApp.SetState("gamble");
                            }
                            istrue = false;
                        }
                        else if (!passCheck)
                        {
                            error = "dit is niet het correcte wachtwoord";

                        }


                    }
                    else if (!emailCheck)
                    {
                        error = "er is geen geregisteerd account met deze email";
                    }
                }

                if (error != "")
                {
                    Console.WriteLine(error);
                }
            }
            
            
            
            
                
            
        }

        private void Register()
        {
            int balance = 50;

            string rank = "Gambler";

            Console.Clear();
            Console.WriteLine("Registreer een nieuw account");
            string name = Helpers.Ask("Geef je naam op");
            string email = Helpers.Ask("Geef je email op");
            string password = Helpers.Ask("Geef je wachtwoord op");


            User newUser = new User(name, email, password, balance, rank);
            gambleApp.GetDataContext().Users.Add(newUser);
            gambleApp.GetDataContext().SaveChanges();
            Console.WriteLine("Registration successful!");
        }

        public User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
