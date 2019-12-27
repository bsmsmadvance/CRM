using Base.DTOs.FIN;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using System;
using System.Threading.Tasks;
using Finance.Services.IService;
using Finance.Params.Filters;
using Finance.Params.Outputs;
using PagingExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using Database.Models.ACC;
using Base.DTOs.PRJ;
using System.Collections.Generic;

namespace Finance.Services.Service
{
    public class TransferService //: ITransferService
    {
        private readonly DatabaseContext DB;

        public TransferService(DatabaseContext db)
        {
            DB = db;
        }

    }
}
