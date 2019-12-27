using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.ACC
{
    public class PostGLHeaderSortByParam
    {
        public PostGLHeaderBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }
    public enum PostGLHeaderBy
    {
        PostGLDocumentType,
        DocumentDate,
        Description,
        Project,
        Unit,
        Amount,
        Remark,
        DocumentNo,
        BankAccountNo,
        Fee,
        RemainAmount,
        CreatedByUser,
        Created,
        PostedStatus
    }
}
