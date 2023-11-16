using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Web.Model.Enum;

namespace Web.Domain.Entities
{
    public class UserProfile : BaseEnitity
    {
        public string? Name { get; set; }
        public ProfileType Type { get; set; }
        public string? Descrtiption { get; set; }
        public ICollection<User> Users { get; } = new List<User>();

    }
}
