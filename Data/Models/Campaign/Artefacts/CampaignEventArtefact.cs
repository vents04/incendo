using System;

namespace Data.Models
{
    public class CampaignEventArtefact : Artefact
    {
        public string EventId { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }
    }
}