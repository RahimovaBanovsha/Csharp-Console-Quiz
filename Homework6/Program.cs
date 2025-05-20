using System;
using System.Diagnostics;

namespace Homework6
{
    internal class Program
    {
        static string[] questions =
        {
            "Which company created the C# programming language?",
            "Which of these is a value type in C#?",
            "What is the default access modifier for class members in C++?",
            "Which keyword in C++ is used to define a constant value?",
            "What is the purpose of the 'using' directive in C#?",
            "Which of the following is NOT a valid access modifier in C#?",
            "Which C++ feature allows multiple functions with the same name but different parameters?",
            "What is the extension of a C# source file?",
            "What is the C# equivalent of C++'s 'new' keyword for creating objects?",
            "Which C# keyword is used to prevent a class from being inherited?"
        };

        static string[,] answers = {
            {"Microsoft", "Apple", "Sun Microsystems"},
            {"int", "string", "object"},
            {"private", "public", "protected"},
            {"const", "final", "readonly"},
            {"To import namespaces", "To allocate memory", "To handle exceptions"},
            {"internal", "hidden", "protected"},
            {"Function overloading", "Inheritance", "Polymorphism"},
            {".cs", ".cpp", ".c#"},
            {"new", "create", "init"},
            {"sealed", "final", "lock"}
        };

        static int[] correctAnswerIndices = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static void Main(string[] args)
        {
            int score = 0;
            Random rand = new Random();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < questions.Length; i++)
            {
                int[] indices = { 0, 1, 2 };

                Shuffle(indices, rand);
                // In the new currentOptions array, the answers are arranged in random order:
                string[] currentOptions =
                {
                    answers[i,indices[0]],
                    answers[i,indices[1]],
                    answers[i,indices[2]]
                };

                int selectedIndex = SelectAnswer(currentOptions, questions[i], i + 1,score);
                //getting the original index of the selected answer:
                int selectedAnswerRealIndex = indices[selectedIndex];
                
                Console.SetCursorPosition(0, 0);

                string qText = "Question " + (i + 1) + ": " + questions[i];
                string scoreText = "Score: " + score;
                int totalWidth = Console.WindowWidth - 1;
                int padding = totalWidth - qText.Length - scoreText.Length;

                padding = (padding < 1) ? 1 : padding;


                if (selectedAnswerRealIndex == correctAnswerIndices[i])
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(qText); 
                Console.ResetColor();
                Console.WriteLine(new string(' ', padding) + scoreText); 
                Console.SetCursorPosition(0, 4 + selectedIndex);

                if (selectedAnswerRealIndex == correctAnswerIndices[i])
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                //Converting selected index to letter and print the selected answer:
                Console.Write($"> {(char)('a' + selectedIndex)}) {currentOptions[selectedIndex]}   ");
                Console.ResetColor();

                Console.SetCursorPosition(0, 8);

                if (selectedAnswerRealIndex == correctAnswerIndices[i])
                    Console.WriteLine("Correct!");
                else
                    Console.WriteLine("Incorrect!");

                Console.Write("Press any key to continue...");
                Console.ReadKey();

                if (selectedAnswerRealIndex == correctAnswerIndices[i])
                    score += 10;
                else
                {
                    score -= 10;
                    if (score < 0) score = 0;
                }

            }
            Console.Clear();

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            Console.WriteLine($"\nThe Quiz is finished. You scored {score} points.");
            Console.WriteLine($"Time taken: {ts.Minutes} min {ts.Seconds} sec");

        }

        static void Shuffle(int[] array, Random rand)
        {
            // Fisher-Yates algorithm:
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1); // j is a random index from 0 to i (i is included)

                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;

            }
        }

        static int SelectAnswer(string[] options, string question, int questionNumber,int score)
        {
            int selected = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();

                string qText = "Question " + questionNumber + ": " + question;
                string scoreText = "Score: " + score;

                int totalWidth = Console.WindowWidth - 1;
                int padding = totalWidth - qText.Length - scoreText.Length;

                if (padding < 1) padding = 1;

                Console.WriteLine(qText + new string(' ', padding) + scoreText + "\n");
                Console.WriteLine("Use ↑ ↓ to select your answer, press Enter to confirm:\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    Console.WriteLine($"{(char)('a' + i)}) {options[i]}");
                    Console.ResetColor();

                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0)
                            selected--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selected < options.Length - 1)
                            selected++;
                        break;

                }

            } while (key != ConsoleKey.Enter);

            return selected;
        }
    }
}
