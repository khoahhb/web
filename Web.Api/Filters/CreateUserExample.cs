using Swashbuckle.AspNetCore.Filters;
using Web.Model.Dtos.RequestDtos.User;

namespace Web.Api.Filters
{
    public class CreateUserExample : IExamplesProvider<CreateUserRequestDto>
    {
        public CreateUserRequestDto GetExamples() 
        { 
            return new CreateUserRequestDto() 
            {
                Username = "username1",
                Password = "Password@1",
                Fullname = "Nguyen Van A",
                Gender = Model.Enum.GenderType.Male,
                DateOfBirth = "14/10/2001",
                Phone = "0392954288",
                Email = "username1@gmail.com",
                Address = "Hau Giang",
                AvatarId = Guid.Parse("7e42633e-d714-406f-98d6-81909a4502c9"),
                UserProfileId = Guid.Parse("7284e6bc-5913-4dcf-8229-b86a5f52b565")
            }; 
        }
    }
}
