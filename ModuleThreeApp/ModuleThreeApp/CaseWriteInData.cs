using LibraryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleThreeApp
{
    class CaseWriteInData
    {
        public static void WriteInData()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Вы выбрали действие записи нового сервера в DataFile]");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Введите новый сервер]");
            Console.ResetColor();

            var newServer = new Server
            {
                Name = HelperOutPut.ReadFromKeyboard("Имя сервера: ", x => Convert.ToString(x)),
                AdditionalPower = HelperOutPut.ReadFromKeyboard("Дополнительная мощность: ", x => HelperOutPut.EnterBaseType(x)),
                PowerOfSpecific = HelperOutPut.ReadFromKeyboard("Мощность конкретного сервера: ", x => Convert.ToString(x))
            };

            SaveJsonData.Servers.Add(newServer);
            SaveJsonData.SaveData();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Сервер успешно добавлен и сохранен в DataFile]\n");
            Console.ResetColor();
        }
    }
}
