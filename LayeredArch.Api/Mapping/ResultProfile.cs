using AutoMapper;
using LayeredArch.Api.Resources;
using LayeredArch.Core.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Mapping
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            // Map Domain Models to Service DTO
            CreateMap(typeof(QueryResultDto<>), typeof(QueryResultResource<>));
        }
    }
}
