using AutoMapper;
using Users.Application.Common.Mappings;
using Users.Application.Users.Commands.CreateUser;

namespace Users.WebApi.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string Login { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public int Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool Admin { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(userCommand => userCommand.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(userCommand => userCommand.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(userCommand => userCommand.Name, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(userCommand => userCommand.Gender, opt => opt.MapFrom(userDto => userDto.Gender))
                .ForMember(userCommand => userCommand.BirthDay, opt => opt.MapFrom(userDto => userDto.BirthDay))
                .ForMember(userCommand => userCommand.Admin, opt => opt.MapFrom(userDto => userDto.Admin));
        }
    }
}
