using AutoMapper;
using OilTeamProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OilTeamProject.Models.Products;

namespace OilTeamProject.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Message, MessageDto>();
            Mapper.CreateMap<MessageDto, Message>();
        }
    }
}