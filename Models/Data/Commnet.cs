using System;

namespace EventeiApi.Models.Data
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int? EventId { get; set; }
        public int? OrganizerId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } 

        // Navigation
        public User User { get; set; }
        public Event Event { get; set; }
        public User Organizer { get; set; }
    }
}
