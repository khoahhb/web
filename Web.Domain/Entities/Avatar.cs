namespace Web.Domain.Entities
{
    public partial class Avatar : AuditEntity<Guid>
    {
        public Avatar()
        {
            Users = new HashSet<User>();
        }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public long FileSize { get; set; }
        public bool IsPublished { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
