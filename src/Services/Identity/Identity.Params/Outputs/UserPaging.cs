﻿using Base.DTOs.USR;
using System;
using System.Collections.Generic;
using System.Text;
using PagingExtensions;

namespace Identity.Params.Outputs
{
    public class UserPaging
    {
        public List<UserListDTO> Users { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
