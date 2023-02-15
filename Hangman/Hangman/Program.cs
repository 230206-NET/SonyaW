namespace Hangman;

using System;
using System.Text;

public class MainMenu {
    /*TO-DO:
    - Allow the user to input a hidden word through terminal using ReadKey or to use a random word from a preset.    
    - 'User proof' the input because right now it'll just take whatever they throw at it, NOT only letters and NOT only single characters...
    */ 
    public void Start() {
        int tries = 0;
        FrameHolder frameHolder = new FrameHolder();
        string[] frames = frameHolder.hangFrames;
        string[] wordBank = new string[5] {"Birthday", "Memorial Day", "Christmas Eve", "New Years", "Thanksgiving"};
        string wordToGuess = "Halloween";
        string wrongGuesses = "";
        string rightguesses = "";
        
        while(tries < frames.Length) {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("H A N G M A N");
            Console.WriteLine("=X=X=X=X=X=X=");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(frames[tries]);

            Console.Write("WORD: ");

            foreach(char letter in wordToGuess) {
                string tempLetter = letter.ToString();
                if (rightguesses.ToUpper().Contains(tempLetter.ToUpper())) {
                    Console.Write(letter);
                }else {
                    Console.Write("_");
                }
            }

            if (tries != 0) {
                Console.WriteLine("\nWrong guesses: {0}", string.Join(", ", wrongGuesses.Where(c => !string.IsNullOrEmpty(c.ToString()))));
            }


            Console.WriteLine("\nEnter your guess:");
            string currentGuess = Console.ReadLine().ToUpper();

            if (rightguesses.ToUpper().Contains(currentGuess) || wrongGuesses.ToUpper().Contains(currentGuess)) {
                Console.WriteLine("You've already guessed that letter! Try a different one.");
                continue;
            }else if(wordToGuess.ToUpper().Contains(currentGuess.ToUpper())) {
                rightguesses += currentGuess;
            } else {
                wrongGuesses += currentGuess;
                tries++;
                continue;
            }

            if (rightguesses.Length.Equals(wordToGuess.Distinct().Count())) {
                break;
            }
        }

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine("H A N G M A N");
        Console.WriteLine("=X=X=X=X=X=X=");

        if (tries >= frames.Length) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(frameHolder.loseFrame);
            Console.ForegroundColor = ConsoleColor.White;
        } else {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(frameHolder.winFrame);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
