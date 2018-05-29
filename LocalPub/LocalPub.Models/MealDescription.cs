namespace LocalPub.Models
{
    public class MealDescription
    {
        public MealDescription(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}