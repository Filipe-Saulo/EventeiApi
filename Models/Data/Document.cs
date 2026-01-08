using System;

namespace EventeiApi.Models.Data
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public string DocumentType { get; set; } // CPF, RG, PASSPORT, CNPJ
        public string DocumentNumber { get; set; }
        public string IssuedCountry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; }
    }
}
