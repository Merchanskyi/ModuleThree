using System.Collections.Generic;
using System.Linq;

namespace LibraryApp
{
    public class DataCenter
    {
        public string Name { get; set; }

        public List<Server> Servers { get; set; } = new List<Server>();


        public override string ToString()
        {
            var result = $"DataCenter: {Name}\nServers:\n";
            
            foreach (var server in Servers)
            {
                result += $" - {server.ToString()}\n";
            }

            return result;
        }
    }
}
