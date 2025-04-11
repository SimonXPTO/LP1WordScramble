using System;
using System.Collections.Generic;
using System.IO;
using Humanizer;

namespace WordScramble
{
    public class WordProvider
    {
        private readonly List<string> words;
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordProvider"/> class.
        /// Reads words from a file and stores them in a list. If the file is 
        /// not found, default words are added to the list.
        /// </summary>
        /// <remarks>
        /// This constructor attempts to read a file named "words.txt" located
        /// in the directory two levels above the application's base directory. 
        /// Each word is transformed to uppercase using the 
        /// <see cref="Humanizer"/> library's <c>Transform(To.UpperCase)</c> 
        /// method before being added to the list. If the file does not exist,
        /// the list will contain default words: "FILE", "NOT", and "FOUND".
        /// </remarks>
        public WordProvider()
        {
            random = // ////////// => TO IMPLEMENT <= //////////// //

            string directoryPath = Path.GetFullPath(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            string filePath = Path.Combine(directoryPath, "words.txt");

            words = new List<string>();

            if (File.Exists(filePath))
            {
                string[] fileWords = File.ReadAllLines(filePath);
                foreach (string word in fileWords)
                {
                    string transformedWord = // ////////// => TO IMPLEMENT <= //////////// //
                    words.Add(transformedWord);
                }
            }
            else
            {
                words.Add("FILE");
                words.Add("NOT");
                words.Add("FOUND");
            }
        }

        /// <summary>
        /// Retrieves a random word from the list of words.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing a randomly selected word from 
        /// the list of words.
        /// </returns>
        /// <remarks>
        /// A random word is selected from the list of words stored in memory.
        /// </remarks>
        public string GetRandomWord()
        {
            // ////////// => TO IMPLEMENT <= //////////// //
        }

        /// <summary>
        /// Scrambles the characters of a given word randomly.
        /// </summary>
        /// <param name="word">A <see cref="string"/> representing the word to 
        /// be scrambled.</param>
        /// <returns>
        /// A <see cref="string"/> representing the scrambled version of the 
        /// given word.
        /// </returns>
        /// <remarks>
        /// This method takes the characters of the word, randomly shuffles 
        /// their order, and returns a new string with the characters scrambled.
        /// </remarks>
        public string GetScrambledWord(string word)
        {
            char[] scrambled = word.ToCharArray();
            for (int i = 0; i < scrambled.Length; i++)
            {
                int j = random.Next(i, scrambled.Length);

                char temp = // ////////// => TO IMPLEMENT <= //////////// //
                scrambled[i] = // ////////// => TO IMPLEMENT <= //////////// //
                scrambled[j] = // ////////// => TO IMPLEMENT <= //////////// //
            }
            return new string(scrambled);
        }
    }
}