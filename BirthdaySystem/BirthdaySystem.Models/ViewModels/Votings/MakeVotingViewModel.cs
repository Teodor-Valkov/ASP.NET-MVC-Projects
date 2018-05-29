using BirthdaySystem.Models.Models.Employees;
using BirthdaySystem.Models.Models.Presents;
using System.Collections.Generic;

namespace BirthdaySystem.Models.ViewModels.Votings
{
    public class MakeVotingViewModel
    {
        public MakeVotingViewModel(ICollection<EmployeeDescription> employees, ICollection<PresentDescription> presents)
        {
            this.Employees = employees;
            this.Presents = presents;
        }

        public ICollection<EmployeeDescription> Employees { get; private set; }

        public ICollection<PresentDescription> Presents { get; private set; }
    }
}