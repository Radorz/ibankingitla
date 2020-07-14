using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ViewModels;
using Database;
using Database.Models;

namespace Ibanking_Itla.Infraestructure.Automapper
{

    public class AutoMapperConfi : Profile
    {

        public AutoMapperConfi()
        {

            Configureregister();
            ConfigureManager();

        }

        private void Configureregister()
        {
            CreateMap<NewAccountViewModel, Users>().ReverseMap().ForMember(dest => dest.MontoInicial, opt => opt.Ignore()).ForMember
                (dest => dest.RepeatContraseña, opt => opt.Ignore()).ForMember
                (dest => dest.Roles, opt => opt.Ignore()).ForMember
                (dest => dest.RoleSelect, opt => opt.Ignore());


        }
        private void ConfigureManager()
        {
            CreateMap<Users, ManagementViewModel>().ReverseMap();


        }
    }
}