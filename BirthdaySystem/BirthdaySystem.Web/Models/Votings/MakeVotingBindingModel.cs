using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using static BirthdaySystem.Common.MessageConstants;

namespace BirthdaySystem.Web.Models.Votings
{
    public class MakeVotingBindingModel
    {
        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Employee")]
        public int ReceiverId { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Present")]
        public int PresentId { get; set; }

        public SelectList ReceiversSelectList { get; set; }

        public SelectList PresentsSelectList { get; set; }
    }
}