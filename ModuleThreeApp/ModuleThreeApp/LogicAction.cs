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
        private static List<Server> Servers;

        public static void Logic()
        {
            DCAction[] allowdActions;
            allowdActions = new DCAction[]
                {
                    DCAction.EndWork,
                    DCAction.GetServers,
                    DCAction.WriteServerInData
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

                DCAction action = (DCAction)int.Parse(Console.ReadLine());

                if (!allowdActions.Contains(action))
                {
                    continue;
                }

                switch (action)
                {
                    case DCAction.GetServers:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие просмотра серверов]");
                        Console.ResetColor();

                        if (File.Exists("data.json"))
                        {
                            Servers = JsonConvert.DeserializeObject<List<Server>>(File.ReadAllText("data.json"));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Файл data.json успешно создан!");
                            Console.ResetColor();
                            File.WriteAllText("data.json", "[]");
                            Servers = new List<Server>();
                        }

                        try
                        {
                            if (Servers == null || File.ReadAllText("data.json") == "[]")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Файл пуст!");
                                Console.ResetColor();
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                        }

                        OutputInfo(Servers);
                        break;

                    case DCAction.WriteServerInData:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие записи нового сервера в DataFile]");
                        Console.ResetColor();

                        if (File.Exists("data.json"))
                        {
                            Servers = JsonConvert.DeserializeObject<List<Server>>(File.ReadAllText("data.json"));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Файл data.json успешно создан!");
                            Console.ResetColor();
                            File.WriteAllText("data.json", "[]");
                            Servers = new List<Server>();
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[Введите новый сервер]");
                        Console.ResetColor();

                        var newServer = new Server
                        {
                            Name = ReadFromKeyboard("Имя сервера: ", x => Convert.ToString(x)),
                            AdditionalPower = ReadFromKeyboard("Дополнительная мощность: ", x => EnterBaseType(x)),
                            PowerOfSpecific = ReadFromKeyboard("Мощность конкретного сервера: ", x => Convert.ToString(x))
                        };

                        Servers.Add(newServer);
                        SaveData();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[Сервер успешно добавлен и сохранен в DataFile]\n");
                        Console.ResetColor();
                        break;

                    case DCAction.EndWork:
                        return;
                }
            }
        }

        // Удобный и читабельный вывод для действия просмотра серверов в консольке
        private static void OutputInfo(List<Server> servers)
        {
            for (var index = 0; index < servers.Count; index++)
            {
                Console.WriteLine($"Слот {index + 1}) {servers[index].ToString()}");
            }
        }

        // Ввожу базовый тип с условием
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

        // Передаю тип елемента
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

        // Сохранение любого изменения в дате через json
        public static void SaveData()
        {
            File.WriteAllText("data.json", JsonConvert.SerializeObject(Servers));
        }
    }
}
