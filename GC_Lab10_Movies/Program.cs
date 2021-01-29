using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GC_Lab10_Movies
{
    class Program
    {
        // GC Lab10 Movie List
        // Antonio Manzari

        public static List<string> movieListUnparsed;
        public static List<Movie> movies;
        public static bool userWantsToContinue = true;
        public static string userInput;

        static void Main(string[] args)
        {
            InitializeData();
            PrintWelcomeMessage();
            do
            {
                AccessMovieListLoop();
            } while (userWantsToContinue);
            
            ExitApp();
        }

        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to Antonio's Movie List Application!");
            Console.WriteLine(Environment.NewLine);
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

            // Order alphabetically
            movies.Sort((x, y) => x.Title.CompareTo(y.Title));

            return movies;
        }

        public static void InitializeData()
        {
            movieListUnparsed = RetrieveDataToList();
            movies = GenerateMovieObjectsInList(movieListUnparsed);            
        }

        private static void AccessMovieListLoop()
        {
            // Init acceptable responses
            string[] responsesConfirmation = { "y", "n" };
            string[] responsesCatergories = { "animated", "drama", "horror", "scifi" };


            // Prompt User for catergory choice
            Console.WriteLine($"There are {movies.Count} movies in this list. What category are you interested in? You can choose from the following: [{String.Join(", ", responsesCatergories)}]");
            userInput = GetUserInputMatchAny(responsesCatergories);
            Console.WriteLine(Environment.NewLine);


            // Find all movies that match user's choice
            foreach (var movie in movies)
            {
                if (userInput.Equals(movie.Catergory))
                {
                    Console.WriteLine(movie.Title);
                }
            }
            Console.WriteLine(Environment.NewLine);


            // Prompt user to continue/quit
            Console.WriteLine("Continue? (y/n):");
            userInput = GetUserInputMatchAny(responsesConfirmation);
            Console.WriteLine(Environment.NewLine);
            if (!userInput.Equals(responsesConfirmation[0]))
            {
                userWantsToContinue = false;
            }

        }


        public static string GetUserInputMatchAny(string[] accetableResponses)
        {
            // I tried to write a method that was abstract enough to handle both the "continue: y/n" and "what catergory do you want?" prompts. (or whatever)
            // This will simply only let certain responses through (stored in an array), 
            // but it does not process what to do with those responses. That can be done somewhere else.
            
            userInput = Console.ReadLine().ToLower();

            for (int i = 0; i < accetableResponses.Length; i++)
            {
                if (userInput.Equals(accetableResponses[i]))
                {
                    return userInput;
                }
            }

            Console.WriteLine("Invalid response. Please try again:  ");
            return GetUserInputMatchAny(accetableResponses);
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
