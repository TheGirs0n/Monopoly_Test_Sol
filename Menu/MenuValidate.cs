public class MenuValidate
{
    public int ValidateMenuInput(string message)
    {
        while (true)
        {
            int number;
            if (int.TryParse(Console.ReadLine(), out number))
                return number;
            else
            {
                Console.WriteLine();
                Console.WriteLine("Ошибка ввода! Введите еще раз: ");
                Console.WriteLine(message);
            }
        }
    }
}