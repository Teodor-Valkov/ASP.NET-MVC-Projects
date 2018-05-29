using BirthdaySystem.Models.Models.Presents;
using System;
using System.Collections.Generic;

namespace BirthdaySystem.Models.ViewModels.Votings
{
    public class VotingViewModel
    {
        public VotingViewModel(int id, DateTime closingDate, string creatorName, string receiverName, bool isClosed)
        {
            this.Id = id;
            this.ClosingDate = closingDate;
            this.CreatorName = creatorName;
            this.ReceiverName = receiverName;
            this.IsClosed = isClosed;
            this.Presents = new List<PresentWithGiversDescription>();
        }

        public int Id { get; private set; }

        public DateTime ClosingDate { get; private set; }

        public string CreatorName { get; private set; }

        public string ReceiverName { get; private set; }

        public bool IsClosed { get; private set; }

        public IList<PresentWithGiversDescription> Presents { get; set; }
    }
}