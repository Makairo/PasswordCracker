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
        public static long attempts;
        public  string Promt()
        {
            string input = "";
            Console.WriteLine("Please enter your string:");
            try
            {
                input = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine($"An error occurred; please try again.");
                return Promt();
            }
            
            return input;
        }
        public void Start()
        {
            found = false;
            attempts = 0;
            Console.WriteLine($"    PASSWORD CRACKER    ");
            Console.WriteLine($"========================");



            // Grab User Input
            toCrack = Promt();


            // Declare stopwatch for timing
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.WriteLine($"========================");
            Console.WriteLine($"");
            Console.WriteLine("     Cracking . . .   ");
            Console.WriteLine($"");
            Console.WriteLine($"========================");
            Console.WriteLine($"");

            // Declare 2 threads to implement multithreading.
            // Gen 1 goes from ASCII 33 -> 126
            // Gen 2 goes from ASCII 126 -> 33
            Thread gen1 = new Thread(() => GeneratePasswordForward(String.Empty));
            Thread gen2 = new Thread(() => GeneratePasswordBackward(String.Empty));

            // Two threads created with a starting character to reduce time for words starting with
            // P or C, could create more specific ones to narrow times further.
            // Want to avoid too many as each thread is resources used.
            Thread gen3 = new Thread(() => GeneratePasswordForward("P"));
            Thread gen4 = new Thread(() => GeneratePasswordForward("C"));



            gen1.Start();
            gen2.Start();
            //gen3.Start();
            //gen4.Start();

            // Ensure threads are all completed before method continues.
            gen1.Join();
            gen2.Join();
            //gen3.Join();
            //gen4.Join();

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            string totalTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10
                );

            Console.WriteLine($"===================================================");
            Console.WriteLine($"Password was {(found ? "cracked" : "not cracked")}.");
            Console.WriteLine("Total runime: " + totalTime);
            Console.WriteLine($"Total passwords generated and tried: {attempts}");
        }

        public void GeneratePasswordForward(string input)
        {
            attempts++;
            if (input == toCrack)
            {
                found = true;
                
                return;
            }

            char temp = ' ';

            // Valid ASCII characters are dec 32 - dec 126
            for (int i = 32; i <= 126; i++)
            {
                // This function is recursively called, will bounce back if 
                // the length goes over the search bounds.
                if (found == true || input.Length >= toCrack.Length) return;

                // Casting int to char, as well as printing current iteration to console.
                temp = (char)i;
                //Console.WriteLine(input + temp);
                GeneratePasswordForward(input + temp);
            }
        }

        public void GeneratePasswordBackward(string input)
        {
            attempts++;
            if (input == toCrack)
            {
                found = true;
                
                return;
            }

            char temp = ' ';

            // Valid ASCII characters are dec 32 - dec 126
            for (int i = 126; i >= 32; i--)
            {
                // This function is recursively called, will bounce back if 
                // the length goes over the search bounds.
                if (found == true || input.Length >= toCrack.Length) return;

                // Casting int to char, as well as printing current iteration to console.
                temp = (char)i;
                //Console.WriteLine(input + temp);
                GeneratePasswordBackward(input + temp);
            }
        }

    }
}
