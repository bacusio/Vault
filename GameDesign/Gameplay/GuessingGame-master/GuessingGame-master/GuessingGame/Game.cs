using GuessingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;

namespace GuessingGame
{
    class Game
    {
        public string answer;
        public string guess = "";
        public int guessLimit = 3;
        public bool remainingGuesses = true;
        public bool gameWon = false;
        public List<string> guessedWords = new List<string>();

        public Game(string answer)
        {
            this.answer = answer;
            handleRemainingGuesses(guess, answer, remainingGuesses, guessedWords, guessLimit);
        }

        private void handleRemainingGuesses(string guess, string rightGuess, bool remainingGuesses, List<string> guessedWords, int guessLimit)
        {
            while (remainingGuesses && !gameWon)
            {
                Console.WriteLine("Guess a word: ");
                guess = validateGuess(guessedWords, Console.ReadLine()).ToLower();
                guessedWords.Add(guess);

                // win condition
                if(guess == rightGuess)
                {
                    Console.WriteLine("Nailed it!");
                    gameWon = true;
                }
                // lose condition
                else if (guessedWords.Count >= guessLimit)
                {
                    Console.WriteLine("Out of guesses. You lost because you're dumb.");
                    Console.WriteLine("The right guess was {0}", rightGuess);
                    remainingGuesses = false;
                }
                else
                {
                    string remainingGuessCount = (guessLimit - guessedWords.Count).ToString();
                    Console.WriteLine($"Wrong. {remainingGuessCount} guesses left.");
                }
            }
            Console.ReadLine();
        }

        private bool checkIfAlreadyGuessed(List<string> alreadyGuessed, string newGuess)
        {
            bool existingGuess = false;
            foreach (string guess in alreadyGuessed)
            {
                if (newGuess == guess)
                {
                    existingGuess = true;
                }
            }

            return existingGuess;
        }
        private string validateGuess(List<string> alreadyGuessed, string currentGuess)
        {
            bool wasValidGuess = true;

            while (wasValidGuess)
            {
                if (checkIfAlreadyGuessed(alreadyGuessed, currentGuess))
                {
                    Console.WriteLine("You already guessed that.");
                    currentGuess = Console.ReadLine().ToLower();
                }
                else
                {
                    wasValidGuess = false;
                }
            }

            return currentGuess;
        }
    }
}
