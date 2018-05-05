using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleThreeApp
{
    class CaseReloadServers
    {
        public static void ReloadServers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Вы выбрали действие перезагрузки серверов]");
            Console.ResetColor();

            SaveJsonData.Servers.Select(x => x.IsHide = false).ToList();
        }
    }
}
