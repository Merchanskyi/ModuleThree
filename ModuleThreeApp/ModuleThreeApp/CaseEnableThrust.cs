using LibraryApp;
using System;
using System.Threading;

namespace ModuleThreeApp
{
    class CaseEnableThrust
    {
        public static void EnableThrust()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Вы выбрали действие рандомной нагрузки на сервера]");
            Console.ResetColor();

            foreach (var server in SaveJsonData.Servers)
            {
                var thread = new Thread(s => ThreadThrust.ProcessAdditionalPower(s as Server));
                thread.Start(server);
            }
        }
    }
}
