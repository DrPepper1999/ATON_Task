using System;
using AutoMapper;
using Users.Application.Common.Mappings;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserList
{
    public class UserLoockupDto : IMapWith<User>
    {
        public string Login { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public int Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool Admin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; } = String.Empty;
        public DateTime? RevokedOn { get; set; }
        public string RevokedBy { get; set; } = String.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLoockupDto>()
                .ForMember(userVm => userVm.Login, opt => opt.MapFrom(user => user.Login))
                .ForMember(userVm => userVm.Password, opt => opt.MapFrom(user => user.Password))
                .ForMember(userVm => userVm.Name, opt => opt.MapFrom(user => user.Name))
                .ForMember(userVm => userVm.Gender, opt => opt.MapFrom(user => user.Gender))
                .ForMember(userVm => userVm.BirthDay, opt => opt.MapFrom(user => user.BirthDay))
                .ForMember(userVm => userVm.Admin, opt => opt.MapFrom(user => user.Admin))
                .ForMember(userVm => userVm.CreatedOn, opt => opt.MapFrom(user => user.CreatedOn))
                .ForMember(userVm => userVm.CreatedBy, opt => opt.MapFrom(user => user.CreatedBy))
                .ForMember(userVm => userVm.ModifiedOn, opt =>
                opt.MapFrom(user => ConvertDateTime(user.ModifiedOn)))
                .ForMember(userVm => userVm.ModifiedBy, opt => opt.MapFrom(user => user.ModifiedBy))
                .ForMember(userVm => userVm.RevokedOn, opt =>
                opt.MapFrom(user => ConvertDateTime(user.RevokedOn)))
                .ForMember(userVm => userVm.RevokedBy, opt => opt.MapFrom(user => user.RevokedBy));
        }
        private static DateTime? ConvertDateTime(DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? null : dateTime;
        }
    }
}
