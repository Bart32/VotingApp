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

        //while true w momencie wybrania wartosci break;
        private void DisplayAdminMenu()
        {
            Console.WriteLine("---------- Wybierz co chcesz zrobic ----------");
            Console.WriteLine("1. Dodaj kandydata");
            Console.WriteLine("2. Usun kandydata");
            Console.WriteLine("3. Zobacz wyniki");
            Console.WriteLine("4. Wyloguj się");
            switch (Console.ReadLine())
            {
                case "1":
                    AddCandidate();
                    break;
                case "2":
                    RemoveCandidate();
                    break;
                case "3":
                    ShowCandidates();
                    break;
                case "4":
                    DisplayMenu();
                    break;
                default:
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
            while (true)
            {
                Console.WriteLine("Podaj imie kandydata");
                var name = Console.ReadLine();

                Console.WriteLine("Podaj nazwisko kandydata");
                var surname = Console.ReadLine();

                using (CandidateContext candidateContext = new CandidateContext())
                {
                    if (!string.IsNullOrWhiteSpace(name) & !string.IsNullOrWhiteSpace(surname))
                    {
                        candidateContext.Add(new Candidate() { Name = name, Surname = surname });
                        candidateContext.SaveChanges();
                        DisplayAdminMenu();
                        break;
                    }
                    else Console.WriteLine("---------- Bledne dane kandydata ----------");
                }
            }
        }

        private void RemoveCandidate()
        {
            while (true)
            {
                    Console.WriteLine("Podaj numer kandydata");
                    int number = Convert.ToInt32(Console.ReadLine());

                    using (CandidateContext candidateContext = new CandidateContext())
                    {
                        if (number > 0)
                        {
                            candidateContext.Remove(new Candidate() { Id = number });
                            candidateContext.SaveChanges();
                            DisplayAdminMenu();
                            break;
                        }
                        else Console.WriteLine("---------- Bledne dane kandydata ----------");
                    }
        }

        private void ShowCandidates()
        {
            using (CandidateContext candidateContext = new CandidateContext())
            {
                var candidates = candidateContext.Candidate.ToArray();
                foreach (var candidate in candidates)
                {
                    Console.WriteLine("--- Kandydat nr " + candidate.Id + " ---");
                    Console.WriteLine("{1} {0}", candidate.Name, candidate.Surname);
                    Console.WriteLine("--------------------------------" + Environment.NewLine);
                }
                DisplayAdminMenu();
            }
        }
    }
}