using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities;
using Web.Model.EnumerationTypes;

namespace Web.Application.Interfaces
{
    public interface IAuthorizedUserService
    {
        public ProfileType GetUserRole();
        public Guid GetUserId();
        public string GetUserFullname();
        public User GetUser();
        public string GetToken();
    }
}
