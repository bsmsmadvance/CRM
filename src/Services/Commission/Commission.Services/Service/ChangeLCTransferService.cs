using Database.Models;
using Database.Models.CMS;
using Database.Models.USR;
using Commission.Params.Filters;
using Base.DTOs.CMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace Commission.Services
{
    public class ChangeLCTransferService : IChangeLCTransferService
    {
        private readonly DatabaseContext DB;

        public ChangeLCTransferService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ChangeLCTransferPaging> GetChangeLCTransferListAsync(ChangeLCTransferFilter filter, PageParam pageParam, ChangeLCTransferSortByParam sortByParam)
        {
            //IQueryable<ChangeLCTransferQueryResult> query = DB.ChangeLCTransfers
            //                                      .Select(o => new ChangeLCTransferQueryResult()
            //                                      {
            //                                          ChangeLCTransfer = o,
            //                                          //Project = o.Project,
            //                                          Transfer = o.Transfer,
            //                                          OldLCTransfer = o.OldLCTransfer
            //                                      });

            //โอนแล้ว
            IQueryable<ChangeLCTransferQueryResult> query = from t in DB.Transfers
                                                       .Include(t => t.Agreement)
                                                       .Include(t => t.Unit)
                                                       .Include(t => t.Agreement.Project)
                                                    join tw in DB.TransferOwners on t.ID equals tw.TransferID into g
                                                    from two in g.Where(x => x.Order == 1).DefaultIfEmpty()
                                                    join c in DB.ChangeLCTransfers on t.ID equals c.TransferID into gg
                                                    from lc in gg.DefaultIfEmpty()
                                                    where (lc == null
                                                                || lc.ActiveDate == (DB.ChangeLCTransfers.Where(n => n.TransferID == t.ID).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate)))
                                                        && (t.ActualTransferDate != null)
                                                    select new ChangeLCTransferQueryResult()
                                                    {
                                                        ChangeLCTransfer = lc,
                                                        Transfer = t,
                                                        TransferOwner = two,

                                                        OldLCTransfer = lc != null ? lc.OldLCTransfer : t.TransferSale,

                                                        NewLCTransfer = new User(),

                                                        Project = t.Agreement.Project,
                                                        Agreement = t.Agreement,
                                                        Unit = t.Unit
                                                    };

            #region Filter
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.ChangeLCTransfer.Transfer.Agreement.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.ChangeLCTransfer.Transfer.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.TransferNo))
            {
                query = query.Where(x => x.ChangeLCTransfer.Transfer.TransferNo.Contains(filter.TransferNo));
            }
            if (!string.IsNullOrEmpty(filter.ContractNo))
            {
                query = query.Where(x => x.ChangeLCTransfer.Transfer.Agreement.AgreementNo.Contains(filter.ContractNo));
            }
            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                query = query.Where(x => (x.TransferOwner.FirstNameTH + " " + x.TransferOwner.LastNameTH).Contains(filter.CustomerName));
            }
            if (filter.ActiveDateForm.HasValue)
            {
                query = query.Where(x => x.ChangeLCTransfer.ActiveDate >= filter.ActiveDateForm);
            }
            if (filter.ActiveDateTo.HasValue)
            {
                query = query.Where(x => x.ChangeLCTransfer.ActiveDate <= filter.ActiveDateTo);
            }
            if (filter.ActiveDateForm.HasValue && filter.ActiveDateTo.HasValue)
            {
                query = query.Where(x => x.ChangeLCTransfer.ActiveDate >= filter.ActiveDateForm && x.ChangeLCTransfer.ActiveDate <= filter.ActiveDateTo);
            }
            if (filter.OldLCTransferID.HasValue)
            {
                query = query.Where(x => x.ChangeLCTransfer.OldLCTransferID == filter.OldLCTransferID);
            }
            #endregion

            ChangeLCTransferDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ChangeLCTransferQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => ChangeLCTransferDTO.CreateFromQueryResult(o)).ToList();

            return new ChangeLCTransferPaging()
            {
                PageOutput = pageOutput,
                ChangeLCTransfers = results
            };
        }

        public async Task<ChangeLCTransferDTO> GetChangeLCTransferAsync(Guid id)
        {
            var model = await DB.ChangeLCTransfers.Where(o => o.ID == id).FirstAsync();
            var result = ChangeLCTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ChangeLCTransferDTO> CreateChangeLCTransferAsync(ChangeLCTransferDTO input)
        {
            var lstUpdateChangeLCTransfer = new List<ChangeLCTransfer>();
            await input.ValidateAsync(DB);

            ChangeLCTransfer model = new ChangeLCTransfer();
            input.ToModel(ref model);

            var lstUpdate = await DB.ChangeLCTransfers.Where(o => o.TransferID == model.TransferID
                                                               && o.ActiveDate.Value.Date <= model.ActiveDate.Value.Date
                                                               && o.IsDeleted == false).ToListAsync();
            foreach (var update in lstUpdate)
            {
                update.IsDeleted = true;

                lstUpdateChangeLCTransfer.Add(update);
            }
            DB.ChangeLCTransfers.UpdateRange(lstUpdateChangeLCTransfer);

            await DB.ChangeLCTransfers.AddAsync(model);
            await DB.SaveChangesAsync();

            var trf = await DB.Transfers.Where(o => o.ID == model.TransferID).FirstAsync();
            model.Transfer = trf;

            var result = ChangeLCTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ChangeLCTransferDTO> UpdateChangeLCTransferAsync(Guid id, ChangeLCTransferDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.ChangeLCTransfers.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var trf = await DB.Transfers.Where(o => o.ID == model.TransferID).FirstAsync();
            model.Transfer = trf;

            var result = ChangeLCTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ChangeLCTransfer> DeleteChangeLCTransferAsync(Guid id)
        {
            var model = await DB.ChangeLCTransfers.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
