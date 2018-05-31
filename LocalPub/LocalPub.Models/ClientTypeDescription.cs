namespace LocalPub.Models
{
    public class ClientTypeDescription
    {
        public ClientTypeDescription(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}