namespace LibraryApp
{
    public class Server
    {
        public string Name { get; set; }

        public string AdditionalPower { get; set; }

        public int BaseType { get; set; }

        public string PowerOfSpecific { get; set; }


        public override string ToString()
        {
            return $"{Name} | {AdditionalPower} | {BaseType} | {PowerOfSpecific}";
        }
    }
}
