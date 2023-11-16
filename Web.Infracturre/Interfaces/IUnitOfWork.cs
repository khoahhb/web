using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities;

namespace Web.Infracturre.Interfaces
{
    public interface IUnitOfWork
    {
        Repository<User> RepositoryUser { get; }
        Repository<UserProfile> RepositoryProfile { get; }
        Repository<Avatar> RepositoryAvatar { get; }
        Task Commit();
    }
}
