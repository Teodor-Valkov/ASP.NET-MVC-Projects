using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using static BirthdaySystem.Common.MessageConstants;

namespace BirthdaySystem.Web.Models.Votings
{
    public class VoteBindingModel
    {
        public int VotingId { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Present")]
        public int PresentId { get; set; }

        public SelectList PresentsSelectList { get; set; }
    }
}