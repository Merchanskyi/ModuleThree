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
        private static List<DataCenter> DataCenters;

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

                        if (File.Exists("data.json"))
                        {
                            DataCenters = JsonConvert.DeserializeObject<List<DataCenter>>(File.ReadAllText("data.json"));
                        }
                        else
                        {
                            //Console.WriteLine("Список пустой создаёт файл если его вдруг нет");
                            DataCenters = new List<DataCenter>();
                        }

                        foreach (var dataCenter in DataCenters)
                        {
                            Console.WriteLine(dataCenter.ToString());
                            //OutputInfo(dataCenter.Servers);
                        }

                        //Второй вариант
                        /*
                        for (int i = 0; i < DataCenters.Count; i++)
                        {
                            for (int j = 0; j < DataCenters[i].Servers.Count; j++)
                            {
                                OutputInfo(DataCenters[i].Servers[j]);
                            }
                        }*/
                        break;

                    case DCAction.WriteServerInData:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие записи нового сервера в DataFile]");
                        Console.ResetColor();

                        if (File.Exists("data.json"))
                        {
                            DataCenters = JsonConvert.DeserializeObject<List<DataCenter>>(File.ReadAllText("data.json"));
                        }
                        else
                        {
                            DataCenters = new List<DataCenter>();
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[Введите новый сервер]");
                        Console.ResetColor();

                        var newServer = new Server
                        {
                            Name = ReadFromKeyboard("Имя сервера: ", x => Convert.ToString(x)),
                            AdditionalPower = ReadFromKeyboard("Дополнительная мощность: ", x => Convert.ToString(x)),
                            BaseType = ReadFromKeyboard("Базовый тип(0-5): ", x => EnterBaseType(x)),
                            PowerOfSpecific = ReadFromKeyboard("Мощность конкретного сервера: ", x => Convert.ToString(x))
                        };

                        DataCenters[0].Servers.Add(newServer);
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
                Console.WriteLine($"{index + 1}) {servers[index].ToString()}");
            }
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
            File.WriteAllText("data.json", JsonConvert.SerializeObject(DataCenters));
        }
    }
}
