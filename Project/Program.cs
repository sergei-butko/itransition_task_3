using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Task_3
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            CheckArgs(args);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n------- Welcome to the game \"Rock Paper Scissors\" -------");

            GetHmac(args, out byte[] key, out string move);

            int userMove = MakeMove(args);

            Console.WriteLine($"\nComputer move: {move}.");

            ShowWin(args, userMove, move);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nHMAC key: {BitConverter.ToString(key).Replace("-", "")}");
            
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Environment.Exit(0);
        }

        static void CheckArgs(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (args.Length < 3)
            {
                Console.WriteLine("\nAmount of parameters must be 3 or more!");
                ShowExample();
            }
            if (args.Length % 2 == 0)
            {
                Console.WriteLine("\nAmount of parameters must be odd!");
                ShowExample();
            }
            if (args.Distinct().Count() != args.Length)
            {
                Console.WriteLine("\nParameters should not be repeated!");
                ShowExample();
            }
        }

        static void ShowExample()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nExample: rock paper scissors lizard Spock.");
            Console.ReadKey();
            Console.Clear();
            Environment.Exit(0);
        }

        static void GetHmac(string[] args, out byte[] key, out string move)
        {
            RandomNumberGenerator num = RandomNumberGenerator.Create();
            key = new byte[16];
            num.GetBytes(key);

            var rand = new Random();
            move = args[rand.Next(0, args.Length)];

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nHMAC: {BitConverter.ToString(HmacSha256(move, key)).Replace("-", "")}");
            Console.ForegroundColor = ConsoleColor.Black;
        }

        static byte[] HmacSha256(string move, byte[] key)
        {
            using HMACSHA256 hmac = new HMACSHA256(key);
            return hmac.ComputeHash(Encoding.Default.GetBytes(move));
        }

        static int MakeMove(string[] args)
        {
            bool alive = true;
            while (alive)
            {
                Console.WriteLine("\nAvailable moves:");
                for (int i = 0; i < args.Length; i++)
                    Console.WriteLine($"{i + 1} - {args[i]}");
                Console.WriteLine("0 - exit");

                return CheckMove(args, ref alive);
            }
            return 0;
        }

        static int CheckMove(string[] args, ref bool alive)
        {
            try
            {
                Console.Write("\nMake your move: ");
                int command = Convert.ToInt32(Console.ReadLine());

                if (command > 0 && command <= args.Length)
                {
                    Console.WriteLine($"\nYour move: {args[command - 1]}.");
                    return command;
                }
                if (command == 0)
                {
                    alive = false;
                    Environment.Exit(0);
                }
                else
                {
                    throw new Exception("\nNo such command was found!");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Environment.Exit(0);
            }
            return 0;
        }

        static bool CheckWin(string[] args, int userMove, string compMove)
        {
            for (int i = 1; i <= args.Length / 2; i++)
            {
                int a = userMove + i;
                if (a >= args.Length)
                    a -= args.Length;
                if (args[a] == compMove)
                    return true;
            }
            return false;
        }

        static void ShowWin(string[] args, int userMove, string move)
        {
            if (args[userMove - 1] == move)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nDraw!");
            }
            else if (!CheckWin(args, userMove - 1, move))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nYou win!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou lose!");
            }
        }
    }
}