using System;
using System.Security.Cryptography;

namespace Task2.CrossAndZeroGame
{
    public static class Task2CrossAndZeroGame
    {
        public static char[,] TicTacToeField = new char[3, 3] { {' ', ' ', ' '}, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } }; 

        static int step = 1;

        static int indexGamer = 1;

        public struct CoordinatesStep
        {
            public int NumberRow;
            public int NumberColumn;
        }

        public static void Main()
        {
            Console.WriteLine("Выберите тип игры:");
            Console.WriteLine("1 - Два игрока");
            Console.WriteLine("2 - Против компьютера");

            int typeGame = (int)Console.ReadKey(true).Key;

            if (typeGame == (int)ConsoleKey.D1 || typeGame == (int)ConsoleKey.NumPad1)
            {
                StepGamer();
            }
            else if (typeGame == (int)ConsoleKey.D2 || typeGame == (int)ConsoleKey.NumPad2)
            {

            }
            else
            {
                Console.WriteLine("Не верный выбор");
                Console.WriteLine();
            }
        }

        public static void WritePlayingField()
        {
            Console.WriteLine("Игра                   Разметка поля");

            int indexSquare = 1;
            for (int i = 0; i < 3; i++)
            {
                Console.Write("|");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($" {TicTacToeField[i, j]} |");
                }

                Console.Write("          |");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($" {indexSquare} |");
                    indexSquare++;
                }

                Console.WriteLine();
            }
        }

        public static void StepGamer()
        {
            string symbolString;
            char symbol;
            if (indexGamer == 1)
            {
                symbolString = "Крестик";
                symbol = 'X';
            }
            else
            {
                symbolString = "Нолик";
                symbol = 'O';
            }

            WritePlayingField();

            string indexStepString;
            do
            {
                Console.Write($"Игрок {indexGamer}, ход - {step} ({symbolString}). ");
                Console.Write("Куда ходим? --> ");

                indexStepString = Console.ReadLine();
            }
            while (!CheckStepGamer(indexStepString));


            int indexStep = int.Parse(indexStepString);
            Console.WriteLine();

            CoordinatesStep coordinatesStep = GetCoordinatesStep(indexStep);
            TicTacToeField[coordinatesStep.NumberColumn, coordinatesStep.NumberRow] = symbol;

            if (step >= 5)
            {
                if (CheckVictory(coordinatesStep, symbol))
                {
                    Console.WriteLine($"Игрок {indexGamer} победил!!!");
                    Console.WriteLine("Для завершения нажмите люьую клавишу");
                    Environment.Exit(0);
                }
            }

            step++;
            if (indexGamer == 1)
                indexGamer = 2;
            else
                indexGamer = 1;

            StepGamer();
        }

        public static CoordinatesStep GetCoordinatesStep(int indexStep)
        {
            CoordinatesStep coordinatesStep = new CoordinatesStep();
            int n = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (indexStep == n)
                    {
                        coordinatesStep.NumberColumn = i;
                        coordinatesStep.NumberRow = j;
                    }
                    n++;
                }
            }
            
            return coordinatesStep;
        }

        public static bool CheckStepGamer(string indexStepString)
        {
            if (string.IsNullOrEmpty(indexStepString))
            {
                Console.WriteLine("Не указан ход");
                return false;
            }

            int n;
            if (!int.TryParse(indexStepString, out n))
            {
                Console.WriteLine("Нужно указать число от 1 до 9");
                return false;
            }

            if (n < 0 || n > 9)
            {
                Console.WriteLine("Нужно указать число от 1 до 9");
                return false;
            }

            CoordinatesStep coordinatesStep = GetCoordinatesStep(n);
            if (TicTacToeField[coordinatesStep.NumberColumn, coordinatesStep.NumberRow] != ' ')
            {
                Console.WriteLine("Сюда уже сходили");
                return false;
            }

            return true;
        }

        public static bool CheckVictory(CoordinatesStep coordinatesStep, char symbol)
        {
            bool result;
            char ch;
            
            result = true;
            for (int i = 0; i < 3; i++)
            {
                ch = TicTacToeField[coordinatesStep.NumberColumn, i];
                if (ch != symbol)
                  result = false;
            }
            if (result)
              return result;

            result = true;
            for (int i = 0; i < 3; i++)
            {
                ch = TicTacToeField[i, coordinatesStep.NumberRow];
                if (ch != symbol)
                  result = false;
            }
            if (result)
                return result;

            result = true;
            for (int i = 0;i < 3;i++)
            {
               ch = TicTacToeField[i, i];
               if (ch != symbol)
                  result = false;
            }
            if (result)
              return result;

            result = true;
            int j = 2;
            for (int i = 0; i < 3; i++)
            {
                ch = TicTacToeField[i, j];
                if (ch != symbol)
                    result = false;
                j--;
            }

            return result;
        }
    }
}
