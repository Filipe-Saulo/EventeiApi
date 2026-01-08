using System;

namespace EventeiApi.Models.Data
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string UrlPhoto { get; set; }
        public bool AfterEvent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
