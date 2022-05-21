using System;
using AutoMapper;
using Users.Application.Common.Mappings;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class UserDetailsVm : IMapWith<User>
    {
        public string Name { get; set; } = String.Empty;
        public int Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>()
                .ForMember(userVm => userVm.Name, opt => opt.MapFrom(user => user.Name))
                .ForMember(userVm => userVm.Gender, opt => opt.MapFrom(user => user.Gender))
                .ForMember(userVm => userVm.BirthDay, opt => opt.MapFrom(user => user.BirthDay))
                .ForMember(userVm => userVm.IsActive, opt =>
                opt.MapFrom(user => user.RevokedOn == DateTime.MinValue));
        }
    }
}
