using System;
using System.Threading;

namespace LibraryApp
{
    public class ThreadThrust
    {
        private static Random Random = new Random((int)DateTime.UtcNow.Ticks);

        // Делаю нагрузку. На каждый сервер по одному потоку. Гуглил Server server особо не понятно как работает(спросить Ярика).
        public static void ProcessAdditionalPower(Server server)
        {
            while (true)
            {
                var value = Random.Next(0, server.AdditionalPower + 10);

                //Console.WriteLine($"SERVER {server.Name}. VALUE: {value}");
                if (value > server.AdditionalPower)
                {
                    Thread.Sleep(2000);
                    server.IsHide = true;
                    Console.WriteLine($"[Сервер получил недопустимую нагрузку и выключился: {server.Name}]");
                    return;
                }

                Thread.Sleep(2000);
            }
        }
    }
}