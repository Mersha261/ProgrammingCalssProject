using AutoMapper;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Sevices.Mapper.MapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Sevices.Mapper.MapperBuilder
{
    public class AutoMapperBuilder:Profile
    {
        public AutoMapperBuilder() {            
            CreateMap<TblAboutUs,AboutUsDto>()
                .ForMember(a=>a.MyProperty,opt=>opt.MapFrom(b=>b.AboutUs)).ReverseMap();

            CreateMap<TblProductComment, ProductCommentDto>()
                .ForMember(a => a.ProductId1, opt => opt.MapFrom(b => b.ProductId))
                .ForMember(a => a.IsRead1, opt => opt.MapFrom(b => b.IsRead))
                .ForMember(a => a.PhoneNumber1, opt => opt.MapFrom(b => b.PhoneNumber))
                .ForMember(a => a.Comment1, opt => opt.MapFrom(b => b.Comment))
                .ForMember(a => a.Email1, opt => opt.MapFrom(b => b.Email))
                .ForMember(a => a.UserIp1, opt => opt.MapFrom(b => b.UserIp)).ReverseMap();

        }
    }
}
