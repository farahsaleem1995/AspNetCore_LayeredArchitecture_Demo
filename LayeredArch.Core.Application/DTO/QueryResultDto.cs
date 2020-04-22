using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.DTO
{
    public class QueryResultDto<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
