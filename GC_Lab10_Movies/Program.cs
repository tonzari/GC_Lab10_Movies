using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GC_Lab10_Movies
{
    class Program
    {
        public static List<string> movieListUnparsed;
        public static List<Movie> movies;
        public static bool userWantsToContinue = false;
        public static string userInput;

        static void Main(string[] args)
        {
            InitializeData();
            PrintWelcomeMessage();

            //RunTestPrintMovieData();

            do
            {
                AccessMovieListLoop();
            } while (userWantsToContinue);

            ExitApp();
        }

        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to Antonio's Movie List Application!");
        }

        public static void InitializeData()
        {
            movieListUnparsed = RetrieveDataToList();
            movies = GenerateMovieObjectsInList(movieListUnparsed);
        }

        public static List<string> RetrieveDataToList()
        {
            string fileName = "MovieData.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            return File.ReadAllLines(path).ToList();
        }

        public static List<Movie> GenerateMovieObjectsInList(List<string> unparsedList)
        {
            movies = new List<Movie>();

            foreach (string line in unparsedList)
            {
                string[] data = line.Split(',');

                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = data[i].Trim();
                }

                movies.Add(new Movie(data[0], data[1]));
            }

            return movies;
        }

        private static void AccessMovieListLoop()
        {
            string[] responsesConfirmation = { "y", "n" };
            string[] responsesCatergories = { "animated", "drama", "horror", "scifi" };

            Console.WriteLine($"There are {movies.Count} movies in this list.");
            Console.WriteLine("What category are you interested in?");
            userInput = GetAndValidateUserStringAgainst(responsesCatergories);
            


            
            Console.WriteLine("Continue? (y/n):");
            userInput = GetAndValidateUserStringAgainst(responsesConfirmation);

        }


        public static string GetAndValidateUserStringAgainst(string[] accetableResponses)
        {
            // I tried to write a method that was abstract enough to handle both the "continue: y/n" and "what catergory do you want?" prompts
            // This will simply only let certain response through, but it does not process what to do with those responses. That can be done somewhere else.
            
            userInput = Console.ReadLine().ToLower();

            for (int i = 0; i < accetableResponses.Length; i++)
            {
                if (userInput.Equals(accetableResponses[i]))
                {
                    return userInput;
                }
            }

            Console.WriteLine("Invalid response. Please try again:  ");
            return GetAndValidateUserStringAgainst(accetableResponses);
        }


        private static void ExitApp()
        {
            Console.WriteLine("Thanks! Go watch a movie now!");
            Console.WriteLine("Exiting application...");
        }

        #region TESTS
        private static void RunTestPrintMovieData()
        {
            foreach (var m in movies)
            {
                Console.WriteLine($"TITLE: {m.Title}");
                Console.WriteLine($"TITLE: {m.Catergory}");
                Console.WriteLine(Environment.NewLine);
            }
        }
        #endregion
    }
}
