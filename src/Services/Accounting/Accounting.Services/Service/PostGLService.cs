using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.Params.Filters;
using Base.DTOs.ACC;

using Accounting.Services.IService;
using Database.Models.ACC;
using System.Globalization;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;
using Database.Models.USR;
using Database.Models.MST;

namespace Accounting.Services.Service
{
    public class PostGLService //: IPostGLService 
    {
        private readonly DatabaseContext DB;

        public PostGLService(DatabaseContext db)
        {
            this.DB = db;
        }
    }
}
