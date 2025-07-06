using System.Globalization;

public static class Menu
{
    private static MenuValidate _validate = new MenuValidate();
    
    public static void ShowChoiceMenu()
    {
        string message = "Будет использована случайная генерация данных:" + 
                         "\n1 - Продолжить" + 
                         "\nДействие:";

        Console.WriteLine(message);
        
        while (true)
        {
            var choice = _validate.ValidateMenuInput(message);

            switch (choice)
            {
                case 1:
                    ShowGenerateData();
                    break;
                default:
                    Console.WriteLine("Такого пункта меню нет");
                    Console.WriteLine();
                    Console.WriteLine(message);
                    break;
            }
        }
    }

    private static void ShowPallets(List<Pallet> pallets)
    {
        Console.WriteLine();
        foreach (var pallet in pallets)
        {
            Console.WriteLine();
            Console.WriteLine($"Паллета ID: {pallet.GetId()}");
            Console.WriteLine($"Размеры: {pallet.GetWidth().ToString("F3", CultureInfo.InvariantCulture)}" +
                              $"x{pallet.GetHeight().ToString("F3", CultureInfo.InvariantCulture)}x" +
                              $"{pallet.GetDepth().ToString("F3", CultureInfo.InvariantCulture)}");
            
            Console.WriteLine("\nИ ее коробки");
            var boxes = pallet.GetBoxes();
            foreach (var box in boxes)
            {
                Console.WriteLine($"Коробка ID: {box.GetId()}");
                Console.WriteLine($"Размеры: {box.GetWidth().ToString("F3", CultureInfo.InvariantCulture)}x" +
                                  $"{box.GetHeight().ToString("F3", CultureInfo.InvariantCulture)}x" +
                                  $"{box.GetDepth().ToString("F3", CultureInfo.InvariantCulture)}");
            }
        }

        Console.WriteLine();
    }

    private static void ShowGenerateData()
    {
        int palletCount = 0;
        int minBoxesCount = 0;
        int maxBoxesCount = 0;
        
        Console.WriteLine("Выберите количество паллет и коробок:");
        Console.WriteLine("Количество паллет:");
        do
        {
            palletCount = _validate.ValidateMenuInput("Количество паллет:");

            if (palletCount <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Введите число больше 0");
            }
        } while (palletCount <= 0);

        Console.WriteLine("Минимальное количество коробок:");
        do
        {
            minBoxesCount = _validate.ValidateMenuInput("Минимальное количество коробок:");
            
            if (minBoxesCount <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Введите число больше 0");
            }
        } while (minBoxesCount <= 0);
        
        Console.WriteLine("Если максимум окажется меньше минимума, они поменяются значениями");
        Console.WriteLine("Максимальное количество коробок:");
        do
        {
            maxBoxesCount = _validate.ValidateMenuInput("Максимальное количество коробок:");
            
            if (maxBoxesCount <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Введите число больше 0");
            }
        } while (maxBoxesCount <= 0);

        if (minBoxesCount > maxBoxesCount)
        {
            (maxBoxesCount, minBoxesCount) = (minBoxesCount, maxBoxesCount);
        }
        
        var pallets = GenerateDataRandom.GeneratePallets(palletCount, minBoxesCount, maxBoxesCount);
        
        Console.WriteLine("Полученные паллеты:");
        ShowPallets(pallets);

        string message = "\nВывести на экран:" +
                         "\n1 - Сгруппировать все паллеты по сроку годности, " +
                         "\nотсортировать по возрастанию срока годности, " +
                         "\nв каждой группе отсортировать паллеты по весу." +
                         "\nи 3 паллеты, которые содержат коробки с наибольшим сроком годности, " +
                         "\nотсортированные по возрастанию объема." +
                         "\n2 - Вернуться назад";

        Console.WriteLine(message);
        do
        {
            var choice = _validate.ValidateMenuInput(message);

            switch (choice)
            {
                case 1:
                    ShowFirstAnswer(pallets);
                    ShowSecondAnswer(pallets);
                    break;
                case 2:
                    ShowChoiceMenu();
                    break;
                default:
                    Console.WriteLine("Такого пункта меню нет");
                    break;
            }
        } while (true);
    }

    private static void ShowFirstAnswer(List<Pallet> pallets)
    {
        var answer = FirstAnswerGetData.ShowAnswer(pallets);
        
        Console.WriteLine("Паллеты сгруппирование по сроку годности + отсортированы по весу");

        foreach (var group in answer!)
        {
            Console.WriteLine($"Срок годности: {group.Key:dd.MM.yyyy}");
    
            foreach (var pallet in group)
            {
                Console.WriteLine($"Паллета ID: {pallet.GetId()}");
                Console.WriteLine($"Вес: {pallet.GetWeight().ToString("F3", CultureInfo.InvariantCulture)}");
                Console.WriteLine($"Объем: {pallet.GetVolume().ToString("F3", CultureInfo.InvariantCulture)}");
                
                Console.WriteLine($"\tКоробки ({pallet.GetBoxes().Count} шт):");
                foreach (var box in pallet.GetBoxes())
                {
                    Console.WriteLine($"[ID: {box.GetId()}] " +
                                      $"Размеры: {box.GetWidth().ToString("F3", CultureInfo.InvariantCulture)}" +
                                      $"x{box.GetHeight().ToString("F3", CultureInfo.InvariantCulture)}" +
                                      $"x{box.GetDepth().ToString("F3", CultureInfo.InvariantCulture)}, " +
                                      $"Вес: {box.GetWeight().ToString("F3", CultureInfo.InvariantCulture)}");
                }
            }
            Console.WriteLine(); 
        }
    }

    private static void ShowSecondAnswer(List<Pallet> pallets)
    {
        var answer = SecondAnswerGetData.ShowAnswer(pallets);
        
        Console.WriteLine("3 паллеты, которые содержат коробки с наибольшим сроком годности, " +
                          "отсортированные по возрастанию объема");

        foreach (var pallet in answer)
        {
            Console.WriteLine($"Паллета ID: {pallet.GetId()}");
            Console.WriteLine($"Наибольший срок годности в паллете: {pallet.GetBoxes()
                .Max(x => x.GetExpirationDate())
                .ToShortDateString()}");
            Console.WriteLine($"Обьем паллеты: {pallet.GetVolume().ToString("F3", CultureInfo.InvariantCulture)}");
        }

        Console.WriteLine("");

        string message = "Хотите вернуться в начало?" + "\n1 - Да" + "\nЛюбая другая клавиша - Выход";
        
        Console.WriteLine(message);
        var choice = _validate.ValidateMenuInput(message);
        Console.WriteLine();
        
        switch (choice)
        {
            case 1:
                ShowChoiceMenu();
                break;
            default:
                Environment.Exit(0);
                break;
        }
    }
}