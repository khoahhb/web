using Web.Domain.Entities;
using Web.Model.EnumerationTypes;

namespace Web.Infracturre.AuthenService
{
    public interface IAuthorizedUserService
    {
        public ProfileType? GetUserRole();
        public Guid? GetUserId();
        public string GetUserFullname();
        public string GetToken();
        bool IsValid();
    }
}
