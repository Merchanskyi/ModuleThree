using System;
using System.Linq;
using LibraryApp;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace ModuleThreeApp
{
    class LogicAction
    {
        // Сразу при запуске проверяем был ли создан файл, если нет - создаём.
        public LogicAction()
        {
            if (File.Exists("data.json"))
            {
                SaveJsonData.Servers = JsonConvert.DeserializeObject<List<Server>>(File.ReadAllText("data.json"));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Файл data.json успешно создан!");
                Console.ResetColor();
                File.WriteAllText("data.json", "[]");
                SaveJsonData.Servers = new List<Server>();
            }
        }

        public void Logic()
        {
            var allowdActions = new DCAction[]
            {
                DCAction.EndWork,
                DCAction.GetServers,
                DCAction.WriteServerInData,
                DCAction.EnableThrust,
                DCAction.ReloadServers
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Выберите действие цифрой:");
            Console.ResetColor();

            while (true)
            {
                foreach (var item in allowdActions)
                {
                    Console.WriteLine($"[{(int)item}] {item}");
                }

                DCAction action = (DCAction)HelperOutPut.ReadFromKeyboard("\nКомандная строка: ", x => Convert.ToInt32(x));

                if (!allowdActions.Contains(action))
                {
                    continue;
                }

                switch (action)
                {
                    case DCAction.EndWork:
                        try
                        {
                            //Не работает. Тут я пытаюсь при выходе с программы погасить все рабочие потоки, чтобы не было заддержки при выходе, прога ждёт пока полностью закончатся потоки.
                            //Thread.CurrentThread.Abort();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                        }
                        return;

                    case DCAction.GetServers:
                        CaseGetServers.GetServers();
                        break;

                    case DCAction.WriteServerInData:
                        CaseWriteInData.WriteInData();
                        break;

                    case DCAction.EnableThrust:
                        CaseEnableThrust.EnableThrust();
                        break;

                    case DCAction.ReloadServers:
                        CaseReloadServers.ReloadServers();
                        break;
                }
            }
        }
    }
}
