using AutoMapper;
using System;
using Users.Application.Common.Mappings;
using Users.Application.Users.Commands.UpdateUser;

namespace Users.WebApi.Models
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ForMember(userCommand => userCommand.Login, otp => otp.MapFrom(userDto => userDto.Login))
                .ForMember(userCommand => userCommand.Password, otp => otp.MapFrom(userDto => userDto.Password))
                .ForMember(userCommand => userCommand.Name, otp => otp.MapFrom(userDto => userDto.Name))
                .ForMember(userCommand => userCommand.Gender, otp => otp.MapFrom(userDto => userDto.Gender))
                .ForMember(userCommand => userCommand.BirthDay, otp => otp.MapFrom(userDto => userDto.BirthDay));
        }
    }
}
