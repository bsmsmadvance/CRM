using Base.DTOs.SAL;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Base.DTOs.SAL.UnitDocumentDTO;
using Database.Models.SAL;
using Sale.Params.Filters;
using PagingExtensions;
using Sale.Params.Outputs;
using Database.Models.MasterKeys;
using Database.Models.MST;

namespace Sale.Services
{
    public class UnitDocumentService : IUnitDocumentService
    {
        private readonly DatabaseContext DB;

        public UnitDocumentService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<UnitDocumentPaging> GetUnitDocumentDropdownListAsync(UnitDocumentFilter filter, PageParam pageParam, UnitDocumentSortByParam sortByParam)
        {

            var query = GetUnitDocumentQueryResult();

            #region Filter

            // สถานะ จอง สัญญา โอน เท่านั้น
            List<string> UnitStatusCase = new List<string>();
            UnitStatusCase.Add(BookingStatusKeys.Booking);
            UnitStatusCase.Add(BookingStatusKeys.Contract);
            UnitStatusCase.Add(BookingStatusKeys.TransferOwnership);

            query = query.Where(q => UnitStatusCase.Any(r => r.Equals(q.UnitStatus.Key)));

            // ต้องยืนยันจองแล้ว
            query = query.Where(q => q.Booking.IsPaid == true);

            // ยังไม่ตั้งเรื่องโอน
            query = query.Where(q => q.Transfer.IsReadyToTransfer == false);

            if (!string.IsNullOrEmpty(filter.DocumentNo))
            {
                query = query.Where(q => q.Booking.BookingNo.Contains(filter.DocumentNo) || q.Agreement.AgreementNo.Contains(filter.DocumentNo));
            }

            if ((filter.CompanyID ?? Guid.Empty) != Guid.Empty)
            {
                query = query.Where(q => q.Project.Company.ID == filter.CompanyID);
            }

            if ((filter.ProjectID ?? Guid.Empty) != Guid.Empty)
            {
                query = query.Where(q => q.Project.ID == filter.ProjectID);
            }

            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(q => q.Unit.UnitNo.Contains(filter.UnitNo));
            }

            #endregion Filter

            var pageOuput = PagingHelper.Paging(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => UnitDocumentDTO.CreateFromQuery(o, DB)).ToList() ?? new List<UnitDocumentDTO>();

            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                results = results.Where(q => q.CustomerName.Contains(filter.CustomerName)).ToList() ?? new List<UnitDocumentDTO>();
            }

            UnitDocumentDTO.SortBy(sortByParam, ref results);

            return new UnitDocumentPaging()
            {
                UnitDocuments = results,
                PageOutput = pageOuput
            };

        }

        // แยก Query ไว้ เพราะแต่ละหน้าใช้ filter ไม่เหมือนกัน จะได้แยกใช้ได้
        private IQueryable<UnitDocumentQueryResult> GetUnitDocumentQueryResult()
        {
            var query = from u in DB.Units
                        .Include(o => o.UnitStatus)
                        .Include(o => o.Project)
                            .ThenInclude(o => o.Company)

                        join b in DB.Bookings.Include(o => o.BookingStatus) on u.ID equals b.UnitID into bData
                        from bModel in bData.DefaultIfEmpty()

                        join ag in DB.Agreements on bModel.ID equals ag.BookingID into agData
                        from agModel in agData.DefaultIfEmpty()

                        join tf in DB.Transfers on agModel.ID equals tf.AgreementID into tfData
                        from tfModel in tfData.DefaultIfEmpty()

                        select new UnitDocumentQueryResult
                        {
                            Unit = u,
                            UnitStatus = u.UnitStatus,
                            Project = u.Project,
                            Company = u.Project.Company,
                            Booking = bModel ?? new Booking(),
                            BookingStatus = (bModel ?? new Booking()).BookingStatus ?? new MasterCenter(),
                            Agreement = agModel ?? new Agreement(),
                            Transfer = tfModel ?? new Transfer(),
                        };

            return query;
        }
    }
}
