using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface ISearchable
    {
        string SeacrhKey { get; }
        int Similarity { set; get; }
    }
}
