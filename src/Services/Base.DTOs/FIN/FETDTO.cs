using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;

namespace Base.DTOs.FIN
{
    public class FETDTO : BaseDTO
    {

        /// <summary>
        /// ID ข้อมูลการรับชำระเงินโอนต่างประเทศ
        /// </summary>
        public PaymentForeignBankTransferDTO PaymentForeignBankTransfer { get; set; }

        /// <summary>
        /// ID ข้อมูลการรับชำระเงินบัตรเครดิต
        /// </summary>
        public PaymentCreditCardDTO PaymentCreditCard { get; set; }

        /// <summary>
        /// Booking
        /// </summary>
        public Guid? BookingID { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// จำนวนเงินที่ขอ FET
        /// </summary>
        public decimal FETAmount { get; set; }

        // <summary>
        // หมายเหตุ
        // </summary>
        public string Remark { get; set; }

        /// <summary>
        /// สถานะการขอ FET
        /// </summary>
        public MasterCenterDTO FETStatus { get; set; }

        /// <summary>
        /// File แนบ Credit Advice
        /// </summary>
        public FileDTO AttachFile { get; set; }

        // <summary>
        // หมายเหตุ
        // </summary>
        public string CancelRemark { get; set; }

        /// <summary>
        /// User ตีเอกสารกลับไปให้ LC
        /// </summary>
        public USR.UserDTO RejectByUserID { get; set; }

        /// <summary>
        /// วันที่ การตีเอกสารกลับไปให้ LC
        /// </summary>
        public DateTime? RejectDate { get; set; }

        // <summary>
        // หมายเหตุ
        // </summary>
        [Description("หมายเหตุ Reject")]
        public string RejectRemark { get; set; }


        public UnitDropdownDTO Unit { get; set; }
        public ProjectDropdownDTO Project { get; set; }


        public int? countFET { get; set; }
        public int? countUnit { get; set; }
        public decimal? SumAmountFET { get; set; }

        public User PaymentUserOwner { get; set; }

        //public DepositHeader DepositHeader { get; set; }


        // <DepositNo>
        // เลขที่นำฝาก
        // </DepositNo>
        public string DepositNo { get; set; }

        // <ReceiptTempNo>
        // วันที่ใบเสร็จ
        // </ReceiptTempNo>
        public string ReceiptTempNo { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public MasterCenterDropdownDTO FETRequesterMasterCenter { get; set; }

        public MasterCenterDropdownDTO FETStatusMasterCenter { get; set; }

        public MasterCenterDropdownDTO PaymentMethodTypeMasterCenter { get; set; }

        public BankDropdownDTO Bank { get; set; }


        //public CancelRemark CancelRemark { get; set; }

        //-----------------------------------------------FETQueryResult----------------------------------------------------------------------------------------------------

        public class FETQueryResult
        {
            public FET FET { get; set; }
            public Booking Booking { get; set; }
            public Unit Unit { get; set; }
            public Project Project { get; set; }

            public PaymentMethod PaymentMethod { get; set; }

            public Payment Payment { get; set; }
            public Company Company { get; set; }

            public PaymentForeignBankTransfer PaymentForeignBankTransfer { get; set; }
            public PaymentCreditCard PaymentCreditCard { get; set; }

            public DepositHeader DepositHeader { get; set; }

            public MasterCenter FETRequesterMasterCenter { get; set; }
            public MasterCenter FETStatusMasterCenter { get; set; }
            public MasterCenter PaymentMethodTypeMasterCenter { get; set; }

            public Bank Bank { get; set; }
        }

        public class FETPaymentQueryResult
        {
            public Guid FETID { get; set; }

            public PaymentMethod PaymentMethod { get; set; }
            public MasterCenter PaymentMethodTypeMasterCenter { get; set; }

            public DepositHeader DepositHeader { get; set; }

            public PaymentForeignBankTransfer PaymentForeignBankTransfer { get; set; }
            public PaymentCreditCard PaymentCreditCard { get; set; }
        }

        public static FETDTO CreateFromQuery(FETQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                FETDTO result = new FETDTO()
                {
                    Id = model.FET.ID,
                    PaymentForeignBankTransfer = PaymentForeignBankTransferDTO.CreateFromModel(model.PaymentForeignBankTransfer),
                    PaymentCreditCard = PaymentCreditCardDTO.CreateFromModel(model.PaymentCreditCard),
                    BookingID = model.Booking?.ID,
                    FETAmount = model.FET.Amount,
                    Remark = model.FET.Remark,
                    FETStatus = MasterCenterDTO.CreateFromModel(model.FET.FETStatus),
                    CancelRemark = model.FET.CancelRemark,
                    RejectByUserID = USR.UserDTO.CreateFromModel(model.FET.RejectByUser),
                    RejectDate = model.FET.RejectDate,
                    RejectRemark = model.FET.RejectRemark,
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    DepositNo = model.DepositHeader?.DepositNo,

                    CustomerName = model.FET.CustomerName,

                    PaymentUserOwner = (model.Payment?.CreatedBy == null) ? model.FET.CreatedBy : model.Payment?.CreatedBy,
                    ReceiveDate = model.Payment?.ReceiveDate,

                    FETRequesterMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.FETRequesterMasterCenter),
                    FETStatusMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.FETStatusMasterCenter),
                    PaymentMethodTypeMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.PaymentMethodTypeMasterCenter)
                };

                result.AttachFile = new FileDTO();
                result.AttachFile.Name = model.FET.AttachFileName;
                result.AttachFile.Url = model.FET.AttachFileUrl;

                return result;
            }
            else
            {
                return null;
            }
        }

        public class FETQueryResultViewProject
        {
            public FET FET { get; set; }
            public Project Project { get; set; }
            public int? countFET { get; set; }
            public int? countUnit { get; set; }
            public int? countAmountFET { get; set; }
        }

        public static FETDTO CreateProjectListModel(Project model, DatabaseContext db)
        {
            if (model != null)
            {
                FETDTO result = new FETDTO()
                {
                    Project = ProjectDropdownDTO.CreateFromModel(model),
                };

                var countUnit = db.FETs
                    .Include(o => o.Booking)
                        .ThenInclude(o => o.Unit)
                        .ThenInclude(o => o.Project)
                        .Where(o => o.Booking.Unit.Project.ID == model.ID).ToList();
                result.countUnit = (from x in countUnit select x.Booking.Unit.UnitNo).Distinct().Count();

                var paymentMothodModel = (from o in db.FETs
                    .Include(o => o.Booking)
                        .ThenInclude(o => o.Unit)
                        .ThenInclude(o => o.Project).Where(o => o.Booking.Unit.Project.ID == model.ID)

                                          join pcc in db.PaymentCreditCards on o.ReferentGUID equals pcc.ID into pccData
                                          from pccModel in pccData.DefaultIfEmpty()

                                          join pfbt in db.PaymentForeignBankTransfers on o.ReferentGUID equals pfbt.ID into pfbtData
                                          from pfbtModel in pfbtData.DefaultIfEmpty()

                                          from p in db.PaymentMethods
                                          .Where(p => p.ID == ((pccModel != null) ? pccModel.PaymentMethodID : (pfbtModel ?? new PaymentForeignBankTransfer()).PaymentMethodID))
                                               .Include(p => p.Payment)
                                               .Include(p => p.PaymentMethodType)

                                          select p).ToList() ?? new List<PaymentMethod>();

                result.SumAmountFET = (from x in paymentMothodModel select x.PayAmount).Sum();

                var xx = from bo in db.BookingOwners.Where(bo => !bo.IsThaiNationality)
               .Include(bo => bo.Booking)
                   .ThenInclude(bo => bo.Unit)

                         join ag in db.Agreements on bo.BookingID equals ag.BookingID into agData
                         from agModel in agData.DefaultIfEmpty()

                         join ago in db.AgreementOwners on agModel.ID equals ago.AgreementID into agoData
                         from agoModel in agoData.DefaultIfEmpty()

                         select bo.Booking;

                xx = xx.Where(e => e.ProjectID == model.ID);
                result.countFET = xx.Select(o => o.Unit).Distinct().Count();

                return result;
            }
            else
            {
                return null;
            }
        }

        public class FETQueryResultViewUnit
        {
            public Booking Booking { get; set; }
            public Unit Unit { get; set; }
            public Project Project { get; set; }
            public PaymentMethod PaymentMethod { get; set; }
        }

        public static FETDTO CreateUnitListModel(FETQueryResultViewUnit model, DatabaseContext db)
        {
            if (model != null)
            {
                FETDTO result = new FETDTO();

                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);

                model.Booking = model.Booking ?? new Booking { ID = Guid.Empty };

                if (model.Booking.ID != Guid.Empty)
                {

                    result.CustomerName = db.GetFETCustomerName(model.Booking?.ID ?? Guid.Empty);

                    var countUnit = db.FETs
                        .Include(o => o.Booking)
                            .ThenInclude(o => o.Unit)
                            .ThenInclude(o => o.Project);

                    var countUnitList = countUnit.Where(e => e.ID == (model.Booking ?? new Booking()).ID).ToList();

                    result.countUnit = (from x in countUnitList select x.Booking.Unit?.UnitNo).Count();
                }

                model.Project = model.Project ?? new Project { ID = Guid.Empty };

                if (model.Project.ID != Guid.Empty)
                {
                    var paymentMothod = from o in db.FETs
                    .Include(o => o.Booking)
                        .ThenInclude(o => o.Unit)
                        .ThenInclude(o => o.Project)

                                        join pm in db.PaymentMethods on o.ReferentGUID equals pm.ID into pmData
                                        from pmModel in pmData.DefaultIfEmpty()

                                        select new FETQueryResultViewUnit
                                        {
                                            Booking = o.Booking,
                                            Unit = o.Booking.Unit,
                                            Project = o.Booking.Project,
                                            PaymentMethod = pmModel ?? new PaymentMethod()
                                        };

                    var paymentMothodModel = paymentMothod.Where(o => o.Project.ID == model.Project.ID).Select(o => o.PaymentMethod).ToList() ?? new List<PaymentMethod>();

                    result.SumAmountFET = (from x in paymentMothodModel select x?.PayAmount).Sum();
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        //----------------------------------------------FETQueryResultViewUnit-----------------------------------------------------------------------------------------------------
        public static void SortBy(FETSortByParam sortByParam, ref IQueryable<FETQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FETSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case FETSortBy.ReceiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Payment.ReceiveDate);
                        else query = query.OrderByDescending(o => o.Payment.ReceiveDate);
                        break;
                    case FETSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case FETSortBy.FETNumber:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FET.FETStatusMasterCenterID);
                        else query = query.OrderByDescending(o => o.FET.FETStatusMasterCenterID);
                        break;
                    case FETSortBy.FETAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentMethod.PayAmount);
                        else query = query.OrderByDescending(o => o.PaymentMethod.PayAmount);
                        break;
                    case FETSortBy.Company:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.Company);
                        else query = query.OrderByDescending(o => o.Project.Company);
                        break;
                    case FETSortBy.DepositCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DepositHeader.DepositNo);
                        else query = query.OrderByDescending(o => o.DepositHeader.DepositNo);
                        break;
                    case FETSortBy.ReceiptAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentMethod.PayAmount);
                        else query = query.OrderByDescending(o => o.PaymentMethod.PayAmount);
                        break;
                    case FETSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FET.Updated);
                        else query = query.OrderByDescending(o => o.FET.Updated);
                        break;
                    case FETSortBy.UpdateByuser:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FET.UpdatedByUserID);
                        else query = query.OrderByDescending(o => o.FET.UpdatedByUserID);
                        break;

                    default:
                        query = query.OrderBy(o => o.Project.ProjectNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Project.ProjectNo);
            }

        }

        public static void SortProjectListBy(FETSortProjectListByParam sortByParam, ref List<FETDTO> model)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FETSortProjectListBy.Project:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Project.Id).ToList();
                        else model = model.OrderByDescending(o => o.Project.Id).ToList();
                        break;

                    case FETSortProjectListBy.countFET:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.countFET).ToList();
                        else model = model.OrderByDescending(o => o.countFET).ToList();
                        break;

                    case FETSortProjectListBy.countUnit:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.countUnit).ToList();
                        else model = model.OrderByDescending(o => o.countUnit).ToList();
                        break;

                    case FETSortProjectListBy.countAmountFET:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.SumAmountFET).ToList();
                        else model = model.OrderByDescending(o => o.SumAmountFET).ToList();
                        break;
                    default:
                        model = model.OrderBy(o => o.Project.ProjectNo).ToList();
                        break;
                }
            }
            else
            {
                model = model.OrderBy(o => o.Project.ProjectNo).ToList();
            }
        }

        public static void SortUnitListBy(FETSortUnitListByParam sortByParam, ref List<FETDTO> model)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FETSortUnitListBy.countUnit:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.countUnit).ToList();
                        else model = model.OrderByDescending(o => o.countUnit).ToList();
                        break;

                    case FETSortUnitListBy.countAmountFET:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.SumAmountFET).ToList();
                        else model = model.OrderByDescending(o => o.SumAmountFET).ToList();
                        break;
                    default:
                        model = model.OrderBy(o => o.Unit).ToList();
                        break;
                }
            }
            else
            {
                model = model.OrderBy(o => o.Unit.UnitNo).ToList();
            }
        }

        public void ToModel(ref FET model)
        {
            model.FETRequesterMasterCenterID = FETRequesterMasterCenter.Id;
            model.ProjectID = Project.Id;
            model.BookingID = BookingID;
            model.CustomerName = CustomerName;

            model.Amount = FETAmount;

            model.Remark = Remark;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (FETAmount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = GetType().GetProperty(nameof(FETAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }


    }
}
