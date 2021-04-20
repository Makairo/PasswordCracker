using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace PasswordCracker
{
    class Cracker
    {
        public static string toCrack;
        public static bool found;
        public static string Promt()
        {
            Console.WriteLine("Please enter your string:");
            string input = Console.ReadLine();
            return input;
        }
        public static void Start()
        {
            found = false;
            // Declare stopwatch for timing, grab user input
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            toCrack = Promt();

            
            Thread gen1 = new Thread(() => GeneratePasswordForward(String.Empty));
            Thread gen2 = new Thread(() => GeneratePasswordBackward(String.Empty));
            gen1.Start();
            gen2.Start();


            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            string totalTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10
                );


            Console.WriteLine($"Password {(found ? "cracked" : "not cracked")}.");
            Console.WriteLine("Total runime: " + totalTime);
        }

        public static void GeneratePasswordForward(string input)
        {

            if (input == toCrack)
            {
                found = true;
                return;
            }

            char temp = ' ';

            // Valid ASCII characters are dec 33 - dec 126
            for (int i = 33; i <= 126; i++)
            {
                // This function is recursively called, will bounce back if 
                // the length goes over the search bounds.
                if (found == true || input.Length >= toCrack.Length) return;

                // Casting int to char, as well as printing current iteration to console.
                temp = (char)i;
                Console.WriteLine(input + temp);
                GeneratePasswordForward(input + temp);
            }
        }

        public static void GeneratePasswordBackward(string input)
        {

            if (input == toCrack)
            {
                found = true;
                return;
            }

            char temp = ' ';

            // Valid ASCII characters are dec 33 - dec 126
            for (int i = 126; i >= 33; i--)
            {
                // This function is recursively called, will bounce back if 
                // the length goes over the search bounds.
                if (found == true || input.Length >= toCrack.Length) return;

                // Casting int to char, as well as printing current iteration to console.
                temp = (char)i;
                Console.WriteLine(input + temp);
                GeneratePasswordBackward(input + temp);
            }
        }

    }
}
