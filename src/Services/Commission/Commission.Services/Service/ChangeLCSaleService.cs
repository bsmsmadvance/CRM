using Database.Models;
using Database.Models.CMS;
using Database.Models.MST;
using Database.Models.USR;
using Commission.Params.Filters;
using Base.DTOs;
using Base.DTOs.CMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;

namespace Commission.Services
{
    public class ChangeLCSaleService : IChangeLCSaleService
    {
        private readonly DatabaseContext DB;

        public ChangeLCSaleService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ChangeLCSalePaging> GetChangeLCSaleListAsync(ChangeLCSaleFilter filter, PageParam pageParam, ChangeLCSaleSortByParam sortByParam)
        {
            //IQueryable<ChangeLCSaleQueryResult> query = DB.ChangeLCSales
            //                                      .Select(o => new ChangeLCSaleQueryResult()
            //                                      {
            //                                          ChangeLCSale = o,
            //                                          //Project = o.Project,
            //                                          Agreement = o.Agreement,
            //                                          OldSaleOfficerType = o.OldSaleOfficerType,
            //                                          OldAgent = o.OldAgent,
            //                                          OldAgentEmployee = o.OldAgentEmployee,
            //                                          OldSaleUser = o.OldSaleUser,
            //                                          OldProjectSaleUser = o.OldProjectSaleUser
            //                                      });


            //สัญญาที่ (รอโอนกรรมสิทธิ์ หรือ โอนกรรมสิทธิ์แล้ว) และ อนุมัติ sign contract แล้ว
            IQueryable<ChangeLCSaleQueryResult> query = from a in DB.Agreements
                                                            .Include(a => a.Booking)
                                                            .Include(a => a.Project)
                                                            .Include(a => a.Unit)
                                                        join ag in DB.AgreementOwners on a.ID equals ag.AgreementID into g
                                                        from ago in g.Where(x => x.IsMainOwner).DefaultIfEmpty()
                                                        join c in DB.ChangeLCSales on a.ID equals c.AgreementID into gg
                                                        from lc in gg.DefaultIfEmpty()
                                                        where (lc == null
                                                                    || lc.ActiveDate == (DB.ChangeLCSales.Where(n => n.AgreementID == a.ID).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate)))
                                                            && (a.AgreementStatus.Key == AgreementStatusKeys.WaitingForTransfer
                                                                    || a.AgreementStatus.Key == AgreementStatusKeys.Transfer)
                                                            && (a.IsSignContractApproved ?? false)
                                                        select new ChangeLCSaleQueryResult()
                                                        {
                                                            ChangeLCSale = lc,
                                                            Agreement = a,
                                                            AgreementOwner = ago,

                                                            OldSaleOfficerType = lc != null ? lc.NewSaleOfficerType : a.Booking.SaleOfficerType,
                                                            OldAgent = lc != null ? lc.NewAgent : a.Booking.Agent,
                                                            OldAgentEmployee = lc != null ? lc.NewAgentEmployee : a.Booking.AgentEmployee,
                                                            OldSaleUser = lc != null ? lc.NewSaleUser : a.Booking.SaleUser,
                                                            OldProjectSaleUser = lc != null ? lc.NewProjectSaleUser : a.Booking.ProjectSaleUser,

                                                            NewSaleOfficerType = new MasterCenter(),
                                                            NewAgent = new Agent(),
                                                            NewAgentEmployee = new AgentEmployee(),
                                                            NewSaleUser = new User(),
                                                            NewProjectSaleUser = new User(),

                                                            Project = a.Project,
                                                            Booking = a.Booking,
                                                            Unit = a.Unit
                                                        };

            #region Filter
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.Agreement.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.BookingNo))
            {
                query = query.Where(x => x.Booking.BookingNo.Contains(filter.BookingNo));
            }
            if (!string.IsNullOrEmpty(filter.ContractNo))
            {
                query = query.Where(x => x.Agreement.AgreementNo.Contains(filter.ContractNo));
            }
            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                query = query.Where(x => (x.AgreementOwner.FirstNameTH + " " + x.AgreementOwner.LastNameTH).Contains(filter.CustomerName));
            }
            if (filter.ActiveDateForm.HasValue)
            {
                query = query.Where(x => x.ChangeLCSale.ActiveDate >= filter.ActiveDateForm);
            }
            if (filter.ActiveDateTo.HasValue)
            {
                query = query.Where(x => x.ChangeLCSale.ActiveDate <= filter.ActiveDateTo);
            }
            if (filter.OldSaleOfficerTypeMasterCenterID.HasValue)
            {
                query = query.Where(x => x.OldSaleOfficerType.ID == filter.OldSaleOfficerTypeMasterCenterID);
            }
            if (filter.OldAgentID.HasValue)
            {
                query = query.Where(x => x.ChangeLCSale.OldAgentID == filter.OldAgentID);
            }
            if (filter.OldAgentEmployeeID.HasValue)
            {
                query = query.Where(x => x.ChangeLCSale.OldAgentEmployeeID == filter.OldAgentEmployeeID);
            }
            if (filter.OldSaleUserID.HasValue)
            {
                query = query.Where(x => x.ChangeLCSale.OldSaleUserID == filter.OldSaleUserID);
            }
            if (filter.OldProjectSaleUserID.HasValue)
            {
                query = query.Where(x => x.ChangeLCSale.OldProjectSaleUserID == filter.OldProjectSaleUserID);
            }
            #endregion

            ChangeLCSaleDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ChangeLCSaleQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => ChangeLCSaleDTO.CreateFromQueryResult(o)).ToList();

            return new ChangeLCSalePaging()
            {
                PageOutput = pageOutput,
                ChangeLCSales = results
            };
        }

        public async Task<ChangeLCSaleDTO> GetChangeLCSaleAsync(Guid id)
        {
            var model = await DB.ChangeLCSales.Where(o => o.ID == id).FirstAsync();
            var result = ChangeLCSaleDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ChangeLCSaleDTO> CreateChangeLCSaleAsync(ChangeLCSaleDTO input)
        {
            var lstUpdateChangeLCSale = new List<ChangeLCSale>();
            await input.ValidateAsync(DB);

            ChangeLCSale model = new ChangeLCSale();
            input.ToModel(ref model);

            var lstUpdate = await DB.ChangeLCSales.Where(o => o.AgreementID == model.AgreementID
                                                                && o.ActiveDate.Value.Date <= model.ActiveDate.Value.Date
                                                                && o.IsDeleted == false).ToListAsync();
            foreach (var update in lstUpdate)
            {
                update.IsDeleted = true;

                lstUpdateChangeLCSale.Add(update);
            }
            DB.ChangeLCSales.UpdateRange(lstUpdateChangeLCSale);

            await DB.ChangeLCSales.AddAsync(model);
            await DB.SaveChangesAsync();          

            var agr = await DB.Agreements.Where(o => o.ID == model.AgreementID).FirstAsync();
            model.Agreement = agr;

            var result = ChangeLCSaleDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ChangeLCSaleDTO> UpdateChangeLCSaleAsync(Guid id, ChangeLCSaleDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.ChangeLCSales.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var agr = await DB.Agreements.Where(o => o.ID == model.AgreementID).FirstAsync();
            model.Agreement = agr;

            var result = ChangeLCSaleDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ChangeLCSale> DeleteChangeLCSaleAsync(Guid id)
        {
            var model = await DB.ChangeLCSales.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
