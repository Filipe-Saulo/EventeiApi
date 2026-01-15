namespace EventeiApi.Models.Master
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string SchemaName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
