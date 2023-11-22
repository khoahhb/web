using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model.EnumerationTypes;

namespace Web.Domain.Entities
{
    public class Credential : AuditEntity<Guid>
    {
        public Credential()
        {
        }

        public string Token { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
