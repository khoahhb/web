namespace Web.Domain.Entities
{
    public class Avatar : BaseEnitity
    {
        public string? FileName { get; set; }
        public string? MimeType { get; set; }
        public long? FileSize { get; set; }
        public bool? IsPublished { get; set; }
        public ICollection<User> Users { get; } = new List<User>();
    }
}
