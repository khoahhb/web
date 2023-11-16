﻿using System.ComponentModel.DataAnnotations;
using Web.Model.Enum;

namespace Web.Model.Dtos.RequestDtos.User
{
    public class CreateUserRequestDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public GenderType? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Guid? AvatarId { get; set; }
        public Guid? UserProfileId { get; set; }
    }
}
