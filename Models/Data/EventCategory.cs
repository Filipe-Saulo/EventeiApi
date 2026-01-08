using System;
using System.Collections.Generic;

namespace EventeiApi.Models.Data
{
    public class EventCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsAdult { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
