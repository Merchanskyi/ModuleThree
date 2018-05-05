namespace LibraryApp
{
    public class Server
    {
        public string Name { get; set; }

        public int AdditionalPower { get; set; }

        public string PowerOfSpecific { get; set; }

        public bool IsHide { get; set; }

        public override string ToString()
        {
            return $"Название: {Name} | Дополнительная мощность: {AdditionalPower} | Мощность конкретного сервера: {PowerOfSpecific}";
        }
    }
}
