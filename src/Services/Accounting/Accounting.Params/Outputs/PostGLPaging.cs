using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.ACC;
using PagingExtensions;

namespace Accounting.Params.Outputs
{
    public class PostGLPaging
    {
        public List<PostGLHeaderDTO> PostGLs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
