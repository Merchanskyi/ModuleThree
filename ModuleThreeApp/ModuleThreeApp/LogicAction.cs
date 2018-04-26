using System;
using System.Linq;
using LibraryApp;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace ModuleThreeApp
{
    class LogicAction
    {
        private static List<DataCenter> Data;

        public static void Logic()
        {
            DCAction[] allowdActions;
            allowdActions = new DCAction[]
                {
                    DCAction.GetServers,
                    DCAction.WriteServerInData,
                    DCAction.EndWork
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

                        var dataCenter = new DataCenter();

                        // Ярик вот тут помоги мне понять как вывести мне все серваки списком в консольке
                        for (int i = 0; i < dataCenter.Servers.Count; i++)
                        {
                            OutputInfo(dataCenter.Servers[i]);
                        }
                        break;

                    case DCAction.WriteServerInData:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие записи нового сервера в DataFile]");
                        Console.ResetColor();

                        if (File.Exists("data.json"))
                        {
                            Data = JsonConvert.DeserializeObject<List<DataCenter>>(File.ReadAllText("data.json"));
                        }
                        else
                        {
                            Data = new List<DataCenter>();
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[Введите новый сервер]");
                        Console.ResetColor();

                        var server = new Server
                        {
                            Name = ReadFromKeyboard("Имя сервера: ", x => Convert.ToString(x)),
                            AdditionalPower = ReadFromKeyboard("Дополнительная мощность: ", x => Convert.ToString(x)),
                            BaseType = ReadFromKeyboard("Базовый тип(0-5): ", x => EnterBaseType(x)),
                            PowerOfSpecific = ReadFromKeyboard("Мощность конкретного сервера: ", x => Convert.ToString(x))
                        };

                        Data[0].Servers.Add(server);
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
        private static void OutputInfo(Server server)
        {
            Console.Write(server.Name + " | ");
            Console.Write(server.AdditionalPower + " | ");
            Console.Write(server.BaseType + " | ");
            Console.WriteLine(server.PowerOfSpecific);
        }

        // Ввожу базовый тип с условием
        public static int EnterBaseType(string value)
        {
            var baseType = Convert.ToInt32(value);

            if (baseType < 0 || baseType > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Значение должно быть не меньше 0 и не больше 5");
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
            File.WriteAllText("data.json", JsonConvert.SerializeObject(Data));
        }
    }
}
