using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.DataAccess;
using VoteApp.Models;

namespace VoteApp
{
    class Menu
    {
        public void DisplayMenu()
        {

            Console.WriteLine("---------- Aplikacja do glosowania ----------");
            Console.WriteLine("Podaj login");
            var userLogin = Console.ReadLine();

            while (!userLogin.Equals("Admin") && !userLogin.Equals("User"))
            {
                Console.WriteLine("Bledny login, podaj poprawny login");
                userLogin = Console.ReadLine();
            }

            if (userLogin.Equals("Admin")) DisplayAdminMenu();
            else if (userLogin.Equals("User")) DisplayUserMenu();
        }

        private void DisplayAdminMenu()
        {
            Console.WriteLine("---------- Wybierz co chcesz zrobic ----------");
            Console.WriteLine("1. Dodaj kandydata");
            Console.WriteLine("2. Sfałszuj wybory");
            Console.WriteLine("3. Zobacz wyniki");
            Console.WriteLine("4. Wyloguj się");
            switch (Console.ReadLine())
            {
                case "1":
                    AddCandidate();
                    break;
                case "2":
                    break;
                case "3":
                    ShowCandidates();
                    break;
                case "4":
                    DisplayMenu();
                    break;
            }
        }

        private void DisplayUserMenu()
        {
            Console.WriteLine("---------- Wybierz co chcesz zrobic ----------");
            Console.WriteLine("1. Oddaj głos");
            Console.WriteLine("2. Zobacz kandydatow");
            Console.WriteLine("3. Wyloguj się");
            if (Console.ReadLine().Equals("3")) DisplayMenu();
        }


        private void AddCandidate()
        {
            Console.WriteLine("Podaj imie kandydata");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko kandydata");
            string surname = Console.ReadLine();

            using (CandidateContext candidateContext = new CandidateContext())
            {
                if (name != null & surname != null)
                {
                    candidateContext.Add(new Candidate() { Name = name, Surname = surname });
                    candidateContext.SaveChanges();
                    DisplayAdminMenu();
                }
                else Console.WriteLine("---------- Bledne dane kandydata ----------");
            }
        }

        private void ShowCandidates()
        {
            using (CandidateContext candidateContext = new CandidateContext())
            {
                List<Candidate> candidates = candidateContext.Candidate.ToList();
                foreach (var candidate in candidates)
                {
                    
                    Console.WriteLine("Kandydate nr " + candidate.Id);
                    Console.WriteLine(candidate.Name);
                    Console.WriteLine(candidate.Surname);
                    Console.WriteLine("--------------------------------" /n);
                }
                DisplayAdminMenu();
            }
        }
    }
}