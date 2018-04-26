using System.Collections.Generic;

namespace LibraryApp
{
    public class DataCenter
    {
        public string Name { get; set; }

        public List<Server> Servers { get; set; } = new List<Server>();
    }
}
