using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Models
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
