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
using Database.Models.MasterKeys;

namespace Finance.Services.Service
{
    public class UnknownPaymentService : IUnknownPaymentService
    {
        private readonly DatabaseContext DB;

        public UnknownPaymentService(DatabaseContext db)
        {
            DB = db;
        }


        // Dropdown
        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string displayName, Guid? companyID)
        {
            IQueryable<Project> query = DB.Projects;
            //.Include(o => o.Company);
            //.Include(o => o.ProjectStatus)
            //.Include(o => o.ProductType)

            //query = query.Where(o => o.ProjectStatus.Key == ProjectStatusKeys.Active);

            if ((companyID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);

            if ((displayName ?? "") != "")
                query = query.Where(o => o.ProjectNameTH.Contains(displayName) || o.ProjectNo.Contains(displayName));

            var results = await query.Take(100).Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync();

            return results;
        }

        public async Task<List<SoldUnitDropdownDTO>> GetSoldUnitDropdowListAsync(string displayName, Guid? projectID)
        {
            var query = from o in DB.Bookings
                .Include(o => o.Unit)
                .Include(o => o.Project)

                        join ag in DB.Agreements on o.ID equals ag.BookingID into agData
                        from agModel in agData.DefaultIfEmpty()

                        join tf in DB.Transfers on agModel.ID equals tf.AgreementID into tfData
                        from tfModel in tfData.DefaultIfEmpty()

                        select new SoldUnitQueryResult
                        {
                            Unit = o.Unit,
                            Booking = o,
                            Project = o.Project,
                            Agreement = agModel ?? new Agreement(),
                            Transfer = tfModel ?? new Transfer()
                        };

            query = query.Where(o => (o.Booking.IsPaid ?? false) == true && o.Transfer.IsReadyToTransfer == false);

            if ((projectID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(o => o.Booking.ProjectID == projectID);

            if ((displayName ?? "") != "")
                query = query.Where(o => o.Unit.UnitNo.Contains(displayName));

            //var xx = query.ToList();

            var results = await query.Take(100).Select(o => SoldUnitDropdownDTO.CreateFromQueryResult(o)).ToListAsync();

            return results;
        }

        public async Task<UnknownPaymentDTO> GetUnknownPaymentAsync(Guid id)
        {
            var UnknownPaymentQuery = GetUnknownPaymentQueryResult();

            UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.ID == id);

            var result = await UnknownPaymentQuery.Select(o => UnknownPaymentDTO.CreateFromQueryResult(o)).FirstOrDefaultAsync() ?? new UnknownPaymentDTO();

            return result;
        }

        public async Task<UnknownPaymentPaging> GetUnknownPaymentListAsync(UnknownPaymentFilter filter, PageParam pageParam, UnknownPaymentSortByParam sortByParam)
        {
            #region Query

            var UnknownPaymentQuery = GetUnknownPaymentQueryResult();

            #endregion Query

            #region Filter

            if (filter.ReceiveDateFrom != null)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.ReceiveDate >= filter.ReceiveDateFrom);
            if (filter.ReceiveDateTo != null)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.ReceiveDate <= filter.ReceiveDateTo);

            if (filter.ReverseDateFrom != null)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.PaymentUnknownPayment.Created >= filter.ReverseDateFrom);
            if (filter.ReverseDateTo != null)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.PaymentUnknownPayment.Created <= filter.ReverseDateTo);

            if ((filter.CompanyID ?? Guid.Empty) != Guid.Empty)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.BankAccount.Company.ID == filter.CompanyID);

            if ((filter.BankAccountID ?? Guid.Empty) != Guid.Empty)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.BankAccount.ID == filter.BankAccountID);

            if ((filter.ProjectID ?? Guid.Empty) != Guid.Empty)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.Booking.Project.ID == filter.ProjectID);

            if ((filter.Unit ?? Guid.Empty) != Guid.Empty)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.Booking.Unit.ID == filter.Unit);

            if ((filter.UnknownPaymentStatus ?? Guid.Empty) != Guid.Empty)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.UnknownPaymentStatus.ID == filter.UnknownPaymentStatus);

            if ((filter.UnknownPaymentCode ?? "") != "")
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.UnknownPaymentCode.Contains(filter.UnknownPaymentCode));

            if (filter.AmountFrom != null)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.Amount >= filter.AmountFrom);
            if (filter.AmountTo != null)
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.Amount <= filter.AmountTo);

            if (filter.IsPostPI != null)
                if (filter.IsPostPI == true)
                    UnknownPaymentQuery = UnknownPaymentQuery.Where(o => (o.PostGLAccount.DocCode ?? "") != "");
                else
                    UnknownPaymentQuery = UnknownPaymentQuery.Where(o => (o.PostGLAccount.DocCode ?? "") == "");

            if ((filter.RVDocumentCode ?? "") != "")
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.PostGLAccount.DocCode.Contains(filter.RVDocumentCode));

            if ((filter.CreatedUserText ?? "") != "")
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.CreatedBy.DisplayName.Contains(filter.CreatedUserText));

            if ((filter.ReversedUserText ?? "") != "")
                UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.ReversedBy.DisplayName.Contains(filter.ReversedUserText));

            #endregion Filter

            UnknownPaymentDTO.SortBy(sortByParam, ref UnknownPaymentQuery);

            var pageOuput = PagingHelper.Paging(pageParam, ref UnknownPaymentQuery);

            var queryResults = await UnknownPaymentQuery.ToListAsync();

            var result = queryResults.Select(o => UnknownPaymentDTO.CreateFromQueryResult(o)).ToList();

            return new UnknownPaymentPaging()
            {
                UnknownPayments = result,
                PageOutput = pageOuput
            };
        }

        public async Task<UnknownPayment> CreateUnknownPaymentAsync(UnknownPaymentDTO input)
        {
            await input.ValidateOnUpdateAsync(DB);

            var CompanyModel = DB.Companies.Where(o => o.ID == input.Company.Id).FirstOrDefault();

            #region Running

            /*
                Format : <UN><SAP Com Code><yy><MM><NNNN>
                Table  : MST.RunningNumberCounters
                Column : Key = "FIN.UnknownPayment"
            */

            string year = Convert.ToString(DateTime.Today.Year);
            string month = DateTime.Today.ToString("MM");
            var runningKey = "UN" + CompanyModel.SAPCompanyID + year[2] + year[3] + month;
            var UnknowbPaymentCode = string.Empty;

            var runningNumber = DB.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == "FIN.UnknownPayment").FirstOrDefault();
            if (runningNumber == null)
            {
                var runningModel = new RunningNumberCounter()
                {
                    Key = runningKey,
                    Type = "FIN.UnknownPayment",
                    Count = 1
                };

                await DB.RunningNumberCounters.AddAsync(runningModel);
                UnknowbPaymentCode = runningKey + runningModel.Count.ToString("0000");
            }
            else
            {
                runningNumber.Count = runningNumber.Count + 1;
                UnknowbPaymentCode = runningKey + runningNumber.Count.ToString("0000");
                DB.Entry(runningNumber).State = EntityState.Modified;
            }

            #endregion

            UnknownPayment model = new UnknownPayment();
            model.UnknownPaymentCode = UnknowbPaymentCode;

            input.ToModel(ref model);

            var MasterCenter = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnknownPaymentStatus && o.Key == UnknownPaymentStatusKeys.Wait).FirstOrDefault() ?? new MasterCenter();
            model.UnknownPaymentStatusID = MasterCenter.ID;

            await DB.UnknownPayments.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await DB.UnknownPayments.Where(o => o.ID == model.ID).FirstAsync();
            return result;
        }

        #region ValidateBeforeUpdate
        public async Task<UnknownPaymentDTO> ValidateBeforeUpdateAsync(Guid id, int UpdateType)
        {
            var UnknownPaymentQuery = GetUnknownPaymentQueryResult();

            UnknownPaymentQuery = UnknownPaymentQuery.Where(o => o.UnknownPayment.ID == id);

            var result = await UnknownPaymentQuery.Select(o => UnknownPaymentDTO.CreateFromQueryResult(o)).FirstOrDefaultAsync() ?? new UnknownPaymentDTO();

            await result.ValidateBeforeUpdateAsync(UpdateType, DB);

            return result;
        }

        #endregion ValidateBeforeUpdate

        #region Reverse

        public async Task<UnknownPayment> UpdateUnknownPaymentAsync(UnknownPaymentDTO input)
        {
            await input.ValidateOnUpdateAsync(DB);

            var model = await DB.UnknownPayments.Where(o => o.ID == input.Id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            //var result = await DB.UnknownPayments.Where(o => o.ID == input.Id).FirstAsync();
            return model;
        }

        public async Task<UnknownPayment> UpdateUnknownPaymentForSAPAsync(UnknownPaymentDTO input)
        {
            var model = await DB.UnknownPayments.Where(o => o.ID == input.Id).FirstAsync();
            //model.IsDeleted = true;
            model.SAPRemark = input.SAPRemark;

            var MasterCenter = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnknownPaymentStatus && o.Key == UnknownPaymentStatusKeys.SAP).FirstOrDefault() ?? new MasterCenter();
            model.UnknownPaymentStatusID = MasterCenter.ID;

            await DB.SaveChangesAsync();

            //var result = await DB.UnknownPayments.Where(o => o.ID == input.Id).FirstAsync();

            return model;
        }

        public async Task<UnknownPayment> DeleteUnknownPaymentAsync(UnknownPaymentDTO input)
        {            
            var model = await DB.UnknownPayments.Where(o => o.ID == input.Id).FirstAsync();
            model.IsDeleted = true;
            model.CancelRemark = input.CancelRemark;

            var MasterCenter = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnknownPaymentStatus && o.Key == UnknownPaymentStatusKeys.Cancel).FirstOrDefault() ?? new MasterCenter();
            model.UnknownPaymentStatusID = MasterCenter.ID;

            await DB.SaveChangesAsync();
            return model;
        }

        
        public async Task<UnknownPaymentDetailDTO> GetUnknownPaymentForReverseAsync(Guid id)
        {
            var unknownPaymentQuery = GetUnknownPaymentQueryResult();

            unknownPaymentQuery = unknownPaymentQuery.Where(o => o.UnknownPayment.ID == id);

            var model = await unknownPaymentQuery.Select(o => o).FirstOrDefaultAsync();

            var result = UnknownPaymentDetailDTO.CreateFromQueryResultAsync(model, DB);

            return result;
        }

        #endregion Reverse

        private IQueryable<UnknownPaymentQueryResult> GetUnknownPaymentQueryResult()
        {
            var unknownPaymentQuery = from o in DB.UnknownPayments
                    .Include(o => o.UnknownPaymentStatus)
                    .Include(o => o.Booking)
                        .ThenInclude(o => o.Project)
                    .Include(o => o.Booking)
                        .ThenInclude(o => o.Unit)
                    .Include(o => o.BankAccount)
                        .ThenInclude(o => o.Company)
                    .Include(o => o.CreatedBy)

                                      join pup in DB.PaymentUnknownPayments on o.ID equals pup.UnknownPaymentID into pupData
                                      from pupModel in pupData.DefaultIfEmpty()

                                      join pm in DB.PaymentMethods on pupModel.PaymentMethodID equals pm.ID into pmData
                                      from pmModel in pmData.DefaultIfEmpty()

                                      join p in DB.Payments on pmModel.PaymentID equals p.ID into pData
                                      from pModel in pData.DefaultIfEmpty()

                                      join rth in DB.ReceiptTempHeaders on pModel.ID equals rth.PaymentID into rthData
                                      from rthModel in rthData.DefaultIfEmpty()

                                      join u in DB.Users on pmModel.CreatedByUserID equals u.ID into uData
                                      from uModel in uData.DefaultIfEmpty()

                                      join gl in DB.PostGLAccounts on o.UnknownPaymentCode equals gl.DocCode into glData
                                      from glModel in glData.DefaultIfEmpty()

                                      select new UnknownPaymentQueryResult
                                      {
                                          UnknownPayment = o,
                                          UnknownPaymentStatus = o.UnknownPaymentStatus,

                                          Booking = o.Booking,
                                          Project = o.Booking.Project,
                                          Unit = o.Booking.Unit,

                                          BankAccount = o.BankAccount,
                                          Company = o.BankAccount.Company,
                                          CreatedBy = o.CreatedBy,
                                          PostGLAccount = glModel ?? new PostGLAccount(),

                                          PaymentUnknownPayment = pupModel ?? new PaymentUnknownPayment(),
                                          PaymentMethod = pmModel ?? new PaymentMethod(),

                                          Payment = pModel ?? new Payment(),
                                          ReceiptTempHeader = rthModel ?? new ReceiptTempHeader(),
                                          ReversedBy = uModel ?? new User()
                                      };

            return unknownPaymentQuery;
        }

    }
}

