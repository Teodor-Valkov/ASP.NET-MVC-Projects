namespace BirthdaySystem.Models.Models.Employees
{
    public class EmployeeDescription
    {
        public EmployeeDescription(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}