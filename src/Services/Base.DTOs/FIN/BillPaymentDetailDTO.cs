using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using Base.DTOs.USR;
using Base.DTOs.SAL;

using static Base.DTOs.FIN.BillPaymentHeaderDTO;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class BillPaymentDetailDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ BatchID ของ Detail
        /// </summary>
        [Description("เลขที่ BatchID ของ Detail")]
        public string DetailBatchID { get; set; }

        /// <summary>
        /// วันที่ลูกค้าชำระเงิน 
        /// </summary>
        [Description("วันที่ลูกค้าชำระเงิน")]
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// Referenct Code จากธนาคาร 1
        /// </summary>
        [Description("Referenct Code จากธนาคาร 1")]
        public string BankRef1 { get; set; }

        /// <summary>
        /// Referenct Code จากธนาคาร 2
        /// </summary>
        [Description("Referenct Code จากธนาคาร 2")]
        public string BankRef2 { get; set; }

        /// <summary>
        /// Referenct Code จากธนาคาร 3
        /// </summary>
        [Description("Referenct Code จากธนาคาร 3")]
        public string BankRef3 { get; set; }

        /// <summary>
        /// การชำระ
        /// </summary>
        [Description("การชำระ")]//Migrate from TransCode
        public string PayType { get; set; }

        /// <summary>
        /// จำนวนเงินที่จ่าย
        /// </summary>
        [Description("จำนวนเงินที่จ่าย")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// Booking ที่ลูกค้าชำระ Bill Payment
        /// </summary>
        [Description("Booking ที่ลูกค้าชำระ Bill Payment")]
        public SAL.BookingDTO Booking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Description("สถานะ Bill Payment")]
        public MST.MasterCenterDropdownDTO BillPaymentStatus { get; set; }

        /// <summary>
        /// วันที่-เวลา Reconcile หรือ confirm รายการ
        /// </summary>
        [Description("วันที่-เวลา Reconcile หรือ confirm รายการ")]
        public DateTime? ReconcileDate { get; set; }

        /// <summary>
        /// กลุ่มเหตุผลการยกเลิก
        /// </summary>
        [Description("กลุ่มเหตุผลการยกเลิก")]
        public MST.MasterCenterDropdownDTO DeleteReason { get; set; }

        /// <summary>
        /// หมายเหตุการยกเลิก
        /// </summary>
        [Description("หมายเหตุการยกเลิก")]
        public string Remark { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        ///// <summary>
        ///// บัญชีธนาคาร filter ตามข้อมูลบริษัท
        ///// </summary>
        //[Description("บัญชีธนาคาร")]
        //public BankAccountDropdownDTO BankAccount { get; set; }

        ///// <summary>
        ///// เลขที่ Booking
        ///// </summary>
        //[Description("เลขที่ Booking")]
        //public Guid? BookingID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public List<ProjectDropdownDTO> Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public List<UnitDropdownDTO> Unit { get; set; }

        ///// <summary>
        ///// เงินตั้งพัก
        ///// </summary>
        //[Description("เงินตั้งพัก")]
        //public decimal Amount { get; set; }

        ///// <summary>
        ///// Post PO
        ///// </summary>
        //[Description("Post PO")]
        //public bool IsPostPO { get; set; }

        ///// <summary>
        ///// เงินตั้งพัก
        ///// </summary>
        //[Description("เลขที่ RV")]
        //public string RVNumber { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        //[Description("สถานะ")]
        //public MasterCenterDropdownDTO BillPaymentStatusMasterCenter { get; set; }

        /// <summary>
        /// ผู้บันทึก
        /// </summary>
        [Description("ผู้บันทึก")]
        public UserDTO CreatedBy { get; set; }

        /// <summary>
        /// ผู้กลับรายการ
        /// </summary>
        [Description("ผู้กลับรายการ")]
        public UserDTO ReversedBy { get; set; }

        ///// <summary>
        ///// หมายเหตุ
        ///// </summary>
        //[Description("หมายเหตุ")]
        //public string Remark { get; set; }

        /// <summary>
        /// CancelRemark
        /// </summary>
        [Description("หมายเหตุยกเลิก")]
        public string CancelRemark { get; set; }

        /// <summary>
        /// หมายเหตุรายการด้าน SAP
        /// </summary>
        [Description("หมายเหตุรายการด้าน SAP")]
        public string SAPRemark { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        [Description("สัญญา")]
        public List<AgreementDropdownDTO> Agreement { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("ชื่อลูกค้า")]
        public string CustomerName { get; set; }

        /// <summary>
        /// ผิดบัญชี
        /// </summary>
        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }




        /// <summary>
        /// รวมเลข AgreementNo
        /// </summary>
        [Description("รวมเลข AgreementNo")]
        public string strAgreementNo { get; set; }
        /// <summary>
        /// รวมชื่อโครงการ
        /// </summary>
        [Description("รวมชื่อโครงการ")]
        public string strProject { get; set; }

        /// <summary>
        /// รวมชื่อโครงการ
        /// </summary>
        [Description("รวมชื่อโครงการ")]
        public string strProjectID { get; set; }
        /// <summary>
        /// รวมชื่อแปลง
        /// </summary>
        [Description("รวมชื่อแปลง")]
        public string strUnit { get; set; }


        public static BillPaymentDetailDTO CreateFromModel(BillPaymentDetail model, DatabaseContext DB)
        {
            if (model != null)
            {
                BillPaymentDetailDTO result = new BillPaymentDetailDTO()
                {
                    Id = model.ID,
                    ReceiveDate = model.ReceiveDate,
                    BankRef1 = model.BankRef1,
                    BankRef2 = model.BankRef2,
                    BankRef3 = model.BankRef3,
                    PayType = model.PayType,
                    PayAmount = model.PayAmount,
                    ReconcileDate = model.ReconcileDate,
                    Remark = model.Remark,
                    BillPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                    DetailBatchID = model.DetailBatchID,
                    DeleteReason = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentDeleteReason),
                    CreatedBy = UserDTO.CreateFromModel(model.CreatedBy),
                    UpdatedBy = model.UpdatedBy.DisplayName,
                    //strAgreementNo = strAgreementNo,
                    //strProject = strProject,
                    //strUnit = strUnit,
                    CustomerName = model.CustomerName,
                    CancelRemark = model.Remark,
                    //BillPaymentStatusMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                    IsWrongAccount = model.IsWrongAccount,

                    Agreement = new List<AgreementDropdownDTO>(),
                    //Booking = new List<BookingForBillPaymentDTO>(),
                    Project = new List<ProjectDropdownDTO>(),
                    Unit = new List<UnitDropdownDTO>(),
                    //Company = new CompanyDropdownDTO(),

                };
                IQueryable<QueryResult> Query = from o in DB.PaymentBillPayments.Where(x => x.BillPaymentDetailID == result.Id)
                    .Include(x => x.PaymentMethod)
                        .ThenInclude(x => x.Payment)
                            .ThenInclude(x => x.Booking)
                                .ThenInclude(x => x.Unit)
                                    .ThenInclude(x => x.Project)
                                        .ThenInclude(x => x.Company)

                                                join AgreementData in DB.Agreements on o.PaymentMethod.Payment.BookingID equals AgreementData.BookingID into AgreementGroup
                                                from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                select new QueryResult
                                                {
                                                    PaymentBillPayment = o,
                                                    Agreement = AgreementModel
                                                };
                var QueryTolist = Query.ToList();
                if (QueryTolist.Count > 0)
                {
                    result.strAgreementNo = String.Join(",", QueryTolist.Select(o => o.Agreement.AgreementNo ?? o.PaymentBillPayment.PaymentMethod.Payment.Booking.BookingNo).ToList());
                    result.strProject = String.Join(",", QueryTolist.Select(o => o.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit.Project.ProjectNameTH).ToList());
                    result.strUnit = String.Join(",", QueryTolist.Select(o => o.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit.UnitNo).ToList());
                    result.strProjectID = String.Join(",", QueryTolist.Select(o => o.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit.Project.ID).ToList());

                    //result.Booking = QueryTolist.Select(o => BookingForBillPaymentDTO.CreateFromModel(o.PaymentBillPayment.PaymentMethod.Payment.Booking,o.PaymentBillPayment.PaymentMethod.PayAmount)).ToList();
                    result.Project = QueryTolist.Select(o => ProjectDropdownDTO.CreateFromModel(o.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit.Project)).ToList();
                    result.Unit = QueryTolist.Select(o => UnitDropdownDTO.CreateFromModel(o.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit)).ToList();
                    result.Agreement = QueryTolist.Select(o => AgreementDropdownDTO.CreateFromModel(o.Agreement)).ToList();
                }
                return result;
            }
            else
            {
                return null;
            }
        }
       


        public class QueryResult
        {
            public PaymentBillPayment PaymentBillPayment { get; set; }
            public Agreement Agreement { get; set; }
        }

        public class QueryTampResult
        {
            public Booking Booking { get; set; }
            public Agreement Agreement { get; set; }
        }
        public static BillPaymentDetailDTO CreateFromModelTamp(BillPaymentDetailTemp model, Agreement Agreement, DatabaseContext DB)
        {
            if (model != null)
            {
                BillPaymentDetailDTO result = new BillPaymentDetailDTO();
                if (model.Booking == null)
                {
                    BillPaymentDetailDTO genDTO = new BillPaymentDetailDTO()
                    {
                        Id = model.ID,
                        ReceiveDate = model.ReceiveDate,
                        BankRef1 = model.BankRef1,
                        BankRef2 = model.BankRef2,
                        BankRef3 = model.BankRef3,
                        PayType = model.PayType,
                        PayAmount = model.PayAmount,

                        ReconcileDate = model.ReconcileDate,
                        Remark = model.Remark,
                        BillPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                        DetailBatchID = model.DetailBatchID,
                        DeleteReason = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentDeleteReason),
                        CreatedBy = UserDTO.CreateFromModel(model.CreatedBy),
                        UpdatedBy = model.UpdatedBy.DisplayName,
                        CustomerName = model.CustomerName,

                        Agreement = new List<AgreementDropdownDTO>(),
                        Booking = new BookingDTO(),
                        Project = new List<ProjectDropdownDTO>(),
                        Unit = new List<UnitDropdownDTO>(),
                        Company = new CompanyDropdownDTO(),

                        CancelRemark = model.Remark,
                        //BillPaymentStatusMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                        IsWrongAccount = model.IsWrongAccount
                    };

                    result = genDTO;
                }
                else
                {
                    BillPaymentDetailDTO genDTO = new BillPaymentDetailDTO()
                    {
                        Id = model.ID,
                        ReceiveDate = model.ReceiveDate,
                        BankRef1 = model.BankRef1,
                        BankRef2 = model.BankRef2,
                        BankRef3 = model.BankRef3,
                        PayType = model.PayType,
                        PayAmount = model.PayAmount,
                        ReconcileDate = model.ReconcileDate,
                        Remark = model.Remark,
                        BillPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                        DetailBatchID = model.DetailBatchID,
                        DeleteReason = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentDeleteReason),
                        CreatedBy = UserDTO.CreateFromModel(model.CreatedBy),
                        UpdatedBy = model.UpdatedBy.DisplayName,
                        CustomerName = model.CustomerName,
                        // Company = CompanyDropdownDTO.CreateFromModel(model.Booking.Project.Company),
                        CancelRemark = model.Remark,
                        //BillPaymentStatusMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                        IsWrongAccount = model.IsWrongAccount,


                        //Booking = SAL.BookingDropdownDTO.CreateFromModel(model.Booking),
                        //Agreement = AgreementDropdownDTO.CreateFromModel(Agreement),
                        //Project = ProjectDropdownDTO.CreateFromModel(model.Booking.Project),
                        //Unit = UnitDropdownDTO.CreateFromModel(model.Booking.Unit),
                        Agreement = new List<AgreementDropdownDTO>(),
                        Booking = new BookingDTO(),
                        Project = new List<ProjectDropdownDTO>(),
                        Unit = new List<UnitDropdownDTO>(),
                        Company = new CompanyDropdownDTO(),
                    };
                    IQueryable<QueryTampResult> Query = from o in DB.Bookings.Where(x => x.ID == model.BookingID)
                               .Include(x => x.Unit)
                                   .ThenInclude(x => x.Project)
                                       .ThenInclude(x => x.Company)

                                                    join AgreementData in DB.Agreements on o.ID equals AgreementData.BookingID into AgreementGroup
                                                    from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                    select new QueryTampResult
                                                    {
                                                        Booking = o,
                                                        Agreement = AgreementModel
                                                    };
                    var QueryTolist = Query.ToList();
                    if (QueryTolist.Count > 0)
                    {


                        result.strAgreementNo = String.Join(",", QueryTolist.Select(o => o.Agreement.AgreementNo ?? o.Booking.BookingNo).ToList());
                        result.strProject = String.Join(",", QueryTolist.Select(o => o.Booking.Unit.Project.ProjectNameTH).ToList());
                        result.strUnit = String.Join(",", QueryTolist.Select(o => o.Booking.Unit.UnitNo).ToList());
                        result.strProjectID = String.Join(",", QueryTolist.Select(o => o.Booking.Unit.Project.ID).ToList());

                        //genDTO.Booking = QueryTolist.Select(o => BookingDTO.CreateFromModel(o.Booking, model.PayAmount)).ToList();
                        genDTO.Project = QueryTolist.Select(o => ProjectDropdownDTO.CreateFromModel(o.Booking.Unit.Project)).ToList();
                        genDTO.Unit = QueryTolist.Select(o => UnitDropdownDTO.CreateFromModel(o.Booking.Unit)).ToList();
                        genDTO.Agreement = QueryTolist.Select(o => AgreementDropdownDTO.CreateFromModel(o.Agreement)).ToList();
                    }
                    result = genDTO;
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BillPaymentDetailSortByParam sortByParam, ref IQueryable<BillPaymentQueryResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        //,
                        //,
                        //,
                        //,
                        //,
                        //,
                        //Project,
                        //Unit,
                        //Payment,
                        //Amount,
                        //Status
                        case BillPaymentWaitingSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.ReceiveDate);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.ReceiveDate);
                            break;
                        case BillPaymentWaitingSortBy.CustomerName:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.CustomerName);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.CustomerName);
                            break;
                        case BillPaymentWaitingSortBy.BankRef1:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.BankRef1);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.BankRef1);
                            break;
                        case BillPaymentWaitingSortBy.BankRef2:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.BankRef2);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.BankRef2);
                            break;
                        case BillPaymentWaitingSortBy.AgreementNO:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Agreement.AgreementNo);
                            else query = query.OrderByDescending(o => o.Agreement.AgreementNo);
                            break;
                            //case BillPaymentSortBy.ReceiveDate:
                            //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.ReceiveDate);
                            //    else query = query.OrderByDescending(o => o.BillPaymentHeader.ReceiveDate);
                            //    break;
                            //case BillPaymentSortBy.CreateDate:
                            //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.Created);
                            //    else query = query.OrderByDescending(o => o.BillPaymentHeader.Created);
                            //    break;
                            //case BillPaymentSortBy.TotalRecord:
                            //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.TotalRecord);
                            //    else query = query.OrderByDescending(o => o.BillPaymentHeader.TotalRecord);
                            //    break;
                            //case BillPaymentSortBy.TotalSuccessRecord:
                            //    if (sortByParam.Ascending) query = query.OrderBy(o => o.countDone);
                            //    else query = query.OrderByDescending(o => o.countDone);
                            //    break;
                            //case BillPaymentSortBy.TotalWatingRecord:
                            //    if (sortByParam.Ascending) query = query.OrderBy(o => o.countWiat);
                            //    else query = query.OrderByDescending(o => o.countWiat);
                            //    break;
                            //case BillPaymentSortBy.TotalAmount:
                            //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.TotalAmount);
                            //    else query = query.OrderByDescending(o => o.BillPaymentHeader.TotalAmount);
                            //    break;
                            //case BillPaymentSortBy.ImportBy:
                            //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.CreatedBy);
                            //    else query = query.OrderByDescending(o => o.BillPaymentHeader.CreatedBy);
                            //    break;
                            //default:
                            //    query = query.OrderBy(o => o.BillPaymentHeader.Created);
                            //    break;
                    }
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.Created);
                    else query = query.OrderByDescending(o => o.BillPaymentHeader.Created);
                }
                else
                {
                    query = query.OrderBy(o => o.BillPaymentDetail.Created);
                }
            }
        }

        public static BillPaymentDetail ToModel(BillPaymentDetailTemp model, Guid BillPaymentHeaderID)
        {
            if (model != null)
            {
                BillPaymentDetail result = new BillPaymentDetail()
                {
                    ID = model.ID,
                    ReceiveDate = model.ReceiveDate,
                    BankRef1 = model.BankRef1,
                    BankRef2 = model.BankRef2,
                    BankRef3 = model.BankRef3,
                    PayType = model.PayType,
                    PayAmount = model.PayAmount,
                    //BookingID = model.BookingID,
                    ReconcileDate = model.ReconcileDate,
                    Remark = model.Remark,
                    BillPaymentStatusMasterCenterID = model.BillPaymentStatusMasterCenterID,
                    DetailBatchID = model.DetailBatchID,
                    BillPaymentHeaderID = BillPaymentHeaderID,
                    IsWrongAccount = model.IsWrongAccount,
                    BillPaymentDeleteReasonMasterCenterID = model.BillPaymentDeleteReasonMasterCenterID,
                    CustomerName = model.CustomerName
                };
                return result;
            }
            else
            {
                return null;
            }
        }



    }
}
