using System.Collections.Generic;

namespace BirthdaySystem.Models.Models.Presents
{
    public class PresentWithGiversDescription
    {
        public PresentWithGiversDescription(string name)
        {
            this.Name = name;
            this.GiverNames = new List<string>();
        }

        public string Name { get; private set; }

        public ICollection<string> GiverNames { get; private set; }
    }
}