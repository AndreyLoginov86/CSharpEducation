namespace Task2.TicTacToe
{
    public static class Task2TicTacToe
    {
        /// <summary>
        /// Игровое поле элеметы массива могут быть: 'X', 'O' или <пробел> - пустая клетка
        /// </summary>
        public static char[,] TicTacToeField = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

        // номер хода
        static int step = 1;

        // 1-ый/2-ой ирок
        static int indexGamer = 1;

        //Координаты очередного хода
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
                StepGamer(1);
            }
            else if (typeGame == (int)ConsoleKey.D2 || typeGame == (int)ConsoleKey.NumPad2)
            {
                StepGamer(2);
            }
            else
            {
                Console.WriteLine("Не верный выбор");
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Вывод на экран игрового поля
        /// </summary>
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

        /// <summary>
        /// Считываем ход игрока, записываем в соответствующий элемент массива 'X' или 'O'
        /// Для игры с компьютером генерируем его ход
        /// </summary>
        /// <param name="typeGame"></param>
        public static void StepGamer(int typeGame)
        {
            string symbolString;
            char symbol;

            if (typeGame == 1) // два игрока
            {
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
            }
            else // против компьютера: 'X' - игрок, 'O' - компьютер
            {
                indexGamer = 1;
                symbolString = "Крестик";
                symbol = 'X';
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

            CoordinatesStep coordinatesStepComputer = new CoordinatesStep();
            if (typeGame == 2)
            {
                coordinatesStepComputer = GetStepComputer();
                TicTacToeField[coordinatesStepComputer.NumberColumn, coordinatesStepComputer.NumberRow] = 'O';
                step++;
            }

            if (step >= 5)
            {
                if (CheckVictory(coordinatesStep, symbol))
                {
                    WritePlayingField();
                    Console.WriteLine($"Игрок {indexGamer} победил!!!");
                    Console.WriteLine("Для завершения нажмите любую клавишу");
                    Environment.Exit(0);

                }

                if (typeGame == 2)
                {
                    if (CheckVictory(coordinatesStepComputer, 'O'))
                    {
                        WritePlayingField();
                        Console.WriteLine("Компьютер победил!!!");
                        Console.WriteLine("Для завершения нажмите любую клавишу");
                        Environment.Exit(0);
                    }
                }
            }

            if (step >= 9)
            {
                Console.WriteLine($"НИЧЬЯ");
                Console.WriteLine("Для завершения нажмите любую клавишу");
                Environment.Exit(0);
            }

            step++;
            if (indexGamer == 1)
                indexGamer = 2;
            else
                indexGamer = 1;

            StepGamer(typeGame);
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

        /// <summary>
        /// Проверка хода введённого игроком 
        /// </summary>
        /// <param name="indexStepString"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Проверка на победу
        /// </summary>
        /// <param name="coordinatesStep"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
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
            for (int i = 0; i < 3; i++)
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

        /// <summary>
        /// Генерируем ход компьютера
        /// </summary>
        /// <returns></returns>
        public static CoordinatesStep GetStepComputer()
        {
            CoordinatesStep result = new CoordinatesStep();

            int n;
            int m;
            char symbol;

            for (int indexCheck = 0; indexCheck < 2; indexCheck++)
            {
                if (indexCheck == 0)
                    symbol = 'O';
                else
                    symbol = 'X';

                for (int i = 0; i < 3; i++)
                {
                    n = 0;
                    m = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (TicTacToeField[i, j] == symbol)
                            n++;
                        if (TicTacToeField[i, j] == ' ')
                        {
                            result.NumberColumn = i;
                            result.NumberRow = j;
                            m++;
                        }
                    }
                    if (n == 2 && m > 0)
                        return result;
                }

                for (int i = 0; i < 3; i++)
                {
                    n = 0;
                    m = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (TicTacToeField[j, i] == symbol)
                            n++;
                        if (TicTacToeField[j, i] == ' ')
                        {
                            result.NumberColumn = j;
                            result.NumberRow = i;
                            m++;
                        }
                    }
                    if (n == 2 && m > 0)
                        return result;
                }

                n = 0;
                m = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (TicTacToeField[i, i] == symbol)
                        n++;
                    if (TicTacToeField[i, i] == ' ')
                    {
                        result.NumberColumn = i;
                        result.NumberRow = i;
                        m++;
                    }
                }
                if (n == 2 && m > 0)
                    return result;

                n = 0;
                m = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (TicTacToeField[i, 2 - i] == symbol)
                        n++;
                    if (TicTacToeField[i, 2 - i] == ' ')
                    {
                        result.NumberColumn = i;
                        result.NumberRow = 2 - i;
                        m++;
                    }
                }
                if (n == 2 && m > 0)
                    return result;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (TicTacToeField[i, j] == ' ')
                    {
                        result.NumberColumn = i;
                        result.NumberRow = j;
                    }
                }
            }
            return result;
        }

    }
}
