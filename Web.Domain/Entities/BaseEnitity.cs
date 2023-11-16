using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Entities
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface IDeleteEntity<TKey> : IDeleteEntity, IBaseEntity<TKey>
    {
    }

    public interface IAuditEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
    public interface IAuditEntity<TKey> : IAuditEntity, IDeleteEntity<TKey>
    {
    }
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }
    }

    public abstract class DeleteEntity<TKey> : BaseEntity<TKey>, IDeleteEntity<TKey>
    {
        public bool IsDeleted { get; set; }
    }

    public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
