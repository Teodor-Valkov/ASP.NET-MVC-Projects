using BirthdaySystem.Domain.Interfaces;
using BirthdaySystem.Domain.Managers;
using BirthdaySystem.Models.Models.Employees;
using BirthdaySystem.Models.Models.Presents;
using BirthdaySystem.Models.ViewModels.Votings;
using BirthdaySystem.Web.Filters;
using BirthdaySystem.Web.Models.Votings;
using System.Collections.Generic;
using System.Web.Mvc;
using static BirthdaySystem.Common.MessageConstants;
using static BirthdaySystem.Common.WebConstants;

namespace BirthdaySystem.Web.Controllers
{
    [AuthorizeEmployee]
    public class VotingsController : Controller
    {
        private IVotingManager votingManager;

        public VotingsController()
            : this(new VotingManager())
        {
        }

        public VotingsController(IVotingManager votingManager)
        {
            this.votingManager = votingManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            string username = this.User.Identity.Name;
            ICollection<VotingViewModel> votings = this.votingManager.GetAllVotingsExceptForCurrentUserByIsClosed(username, false);

            return this.View(votings);
        }

        [HttpGet]
        public ActionResult Closed()
        {
            string username = this.User.Identity.Name;
            ICollection<VotingViewModel> votings = this.votingManager.GetAllVotingsExceptForCurrentUserByIsClosed(username, true);

            return this.View(votings);
        }

        [HttpGet]
        public ActionResult MakeVoting()
        {
            string username = this.User.Identity.Name;
            ICollection<EmployeeDescription> employees = this.votingManager.GetAllEmployeesExceptForCurrentUser(username);
            ICollection<PresentDescription> presents = this.votingManager.GetAllPresents();

            MakeVotingBindingModel makeVotingModel = new MakeVotingBindingModel();
            makeVotingModel.ReceiversSelectList = new SelectList(employees, "Id", "Name", "-- none --");
            makeVotingModel.PresentsSelectList = new SelectList(presents, "Id", "Name", "-- none --");

            return this.View(makeVotingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeVoting(MakeVotingBindingModel makeVotingModel)
        {
            int userId = this.User.GetUserId();

            bool makeVotingResult = this.votingManager.MakeVoting(userId, makeVotingModel.ReceiverId, makeVotingModel.PresentId);
            if (!makeVotingResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, MakeVotingError);
                return this.RedirectToAction(nameof(VotingsController.MakeVoting));
            }

            this.TempData.Add(TempDataSuccessMessageKey, MakeVotingSuccessful);
            return this.RedirectToAction(nameof(VotingsController.Index));
        }

        [HttpGet]
        public ActionResult Vote(int id)
        {
            int userId = this.User.GetUserId();

            bool canEmployeeVote = this.votingManager.CanEmployeeVoteInVoting(id, userId);
            if (!canEmployeeVote)
            {
                this.TempData.Add(TempDataErrorMessageKey, VoteError);
                return this.RedirectToAction(nameof(VotingsController.Index));
            }

            ICollection<PresentDescription> presents = this.votingManager.GetAllPresents();

            VoteBindingModel voteModel = new VoteBindingModel();
            voteModel.VotingId = id;
            voteModel.PresentsSelectList = new SelectList(presents, "Id", "Name", "-- none --");

            return this.View(voteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteBindingModel voteModel)
        {
            int userId = this.User.GetUserId();

            bool voteResult = this.votingManager.Vote(voteModel.VotingId, userId, voteModel.PresentId);
            if (!voteResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, VoteError);
            }
            else
            {
                this.TempData.Add(TempDataSuccessMessageKey, VoteSuccessful);
            }

            return RedirectToAction(nameof(VotingsController.Index));
        }

        [HttpGet]
        public ActionResult Close(int id)
        {
            int userId = this.User.GetUserId();

            bool closeResult = this.votingManager.Close(id, userId);
            if (!closeResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, CloseVotingError);
            }
            else
            {
                this.TempData.Add(TempDataSuccessMessageKey, CloseVotingSuccesful);
            }

            return RedirectToAction(nameof(VotingsController.Index));
        }
    }
}