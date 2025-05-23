﻿using System;

namespace Data.Models
{
    public class CampaignEvent : IArtefactable<CampaignEvent>
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string data { get; set; }
        public CampaignEventType Type { get; set; }

        private CampaignEvent()
        {
        }

        public CampaignEvent(CampaignEventType type, string data)
        {
            this.data = data;
            TimeStamp = DateTime.UtcNow;
            Id = Guid.NewGuid();
            Type = type;
        }

        public override Artefact GetArtefact()
        {
            return new CampaignEventArtefact() { EventId = Id.ToString(), EventType = Type.ToString(), EventData = data };
        }

        public new CampaignEvent FromArtefact(Artefact artefact)
        {
            if (artefact == null || artefact.GetType() != typeof(CampaignEventArtefact)) throw new ArgumentException();
            return new CampaignEvent(Type, data) { Id = Guid.Parse((artefact as CampaignEventArtefact).EventId) };
        }
    }
}