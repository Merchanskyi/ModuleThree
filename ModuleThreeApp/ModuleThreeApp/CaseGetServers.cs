using LibraryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleThreeApp
{
    class CaseGetServers
    {
        public static void GetServers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Вы выбрали действие просмотра серверов]");
            Console.ResetColor();

            if (!SaveJsonData.Servers.Any() || SaveJsonData.Servers.All(x => x.IsHide))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Серверов нет!");
                Console.ResetColor();
            }

            HelperOutPut.OutputInfo(SaveJsonData.Servers.Where(x => !x.IsHide).ToList());
        }
    }
}
