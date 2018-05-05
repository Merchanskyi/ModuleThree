using LibraryApp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ModuleThreeApp
{
    class SaveJsonData
    {
        public static List<Server> Servers;

        // Сохранение любого изменения в дате через json
        public static void SaveData()
        {
            File.WriteAllText("data.json", JsonConvert.SerializeObject(Servers));
        }
    }
}
