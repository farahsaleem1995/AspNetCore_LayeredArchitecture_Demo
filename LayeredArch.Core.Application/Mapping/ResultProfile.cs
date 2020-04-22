using AutoMapper;
using LayeredArch.Core.Application.DTO;
using LayeredArch.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.Mapping
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            // Map Domain Models to Service DTO
            CreateMap(typeof(QueryResult<>), typeof(QueryResultDto<>));
        }
    }
}
