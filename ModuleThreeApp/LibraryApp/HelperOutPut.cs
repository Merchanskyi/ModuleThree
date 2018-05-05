using System;
using System.Collections.Generic;

namespace LibraryApp
{
    public class HelperOutPut
    {
        // Удобный и читабельный вывод для действия просмотра серверов в консольке
        public static void OutputInfo(List<Server> servers)
        {
            for (var index = 0; index < servers.Count; index++)
            {
                Console.WriteLine($"Слот {index + 1}) {servers[index].ToString()}");
            }
        }

        // Ввод базового типа с условием
        public static int EnterBaseType(string value)
        {
            var baseType = Convert.ToInt32(value);

            if (baseType < 0 || baseType > 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Значение должно быть не меньше 0 и не больше 100");
            }

            return baseType;
        }

        // Передаю обобщенный тип елемента через делегат
        public static T ReadFromKeyboard<T>(string message, Func<string, T> mapper)
        {
            while (true)
            {
                Console.Write(message);

                try
                {
                    return mapper.Invoke(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }
    }
}
