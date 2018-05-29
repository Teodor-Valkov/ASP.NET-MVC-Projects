namespace BirthdaySystem.Models.Models.Presents
{
    public class PresentDescription
    {
        public PresentDescription(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}