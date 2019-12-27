using Base.DTOs.MST;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportHeaderDTO : BaseDTO
    {
        /// <summary>
        /// ชนิดของแบบฟอร์ม Direct Debit/Credit
        /// </summary>
        public MST.MasterCenterDropdownDTO DirectFormType { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        public MST.CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public MST.BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// รอบการตัดเงิน วันที่ 1 หรือ 15
        /// </summary>
        public DateTime? PeriodDate { get; set; }

        /// <summary>
        /// วันที่ตัดเงิน
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// รวมจำนวนรายการใน Textfile ที่จะตัดเงินลูกค้า
        /// </summary>
        public int? TotalRecord { get; set; }

        /// <summary>
        /// จำนวนรายการ Error
        /// </summary>
        public int? TotalErrorRecord { get; set; }

        /// <summary>
        /// รวมจำนวนเงินใน Textfile ที่จะตัดเงินลูกค้า
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// วันที่ Import Textfile ที่ได้จากธนาคาร
        /// </summary>
        public DateTime? ImportDate { get; set; }
        /// <summary>
        /// วันที่ Export Textfile
        /// </summary>
        public DateTime? ExportDate { get; set; }

        public string BacthID { get; set; }

        public DirectCreditDebitExportDetailDTO DirectCreditDebitExportDetail { get; set; }


        public class DirectCreditDebitApprovalFormExportQueryResult
        {
            public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }
            public Agreement Agreement { get; set; }
            public Unit Unit { get; set; }
            public Project Project { get; set; }
            public BankAccount BankAccount { get; set; }
            public UnitPrice UnitPrice { get; set; }
            public UnitPriceItem UnitPriceItem { get; set; }
            public UnitPriceInstallment UnitPriceInstallment { get; set; }
            public Bank Bank { get; set; }
            public Transfer Transfer { get; set; }
        }

        public class DirectCreditDebitExportHeaderQueryResult
        {

            public DirectCreditDebitExportHeader DirectCreditDebitExportHeader { get; set; }
            public MasterCenter DirectFormType { get; set; }
            public Company Company { get; set; }
            public Bank Bank { get; set; }
            public BankAccount BankAccount { get; set; }
            public DirectCreditDebitExportDetail DirectCreditDebitExportDetail { get; set; }
            public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }
            public string BacthID { get; set; }
            public Agreement Agreement { get; set; }
            public Transfer Transfer { get; set; }
            public listBatch BatchStatus { get; set; }
        }
        //public class DirectCreditDebitExportImportQueryResult
        //{

        //    public DirectCreditDebitExportDetail DirectCreditDebitExportDetail { get; set; }
        //    //public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }
        //    public Agreement Agreement { get; set; }
        //    public Transfer Transfer { get; set; }
        //    public listBatch BatchStatus { get; set; }
        //}
        public class listBatch
        {

            public string BatchID { get; set; }
            public string TransCode { get; set; }
        }
        
        public static void SortBy(DirectCreditDebitExportHeaderSortByParam sortByParam, ref IQueryable<DirectCreditDebitExportHeaderQueryResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case DirectCreditDebitExportHeaderSortBy.BatchID:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BacthID);
                            else query = query.OrderByDescending(o => o.BacthID);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.Bank:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Alias);
                            else query = query.OrderByDescending(o => o.Bank.Alias);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.BankAccount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.BankAccountNo);
                            else query = query.OrderByDescending(o => o.BankAccount.BankAccountNo);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.Company:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.Code);
                            else query = query.OrderByDescending(o => o.Company.Code);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.PeriodDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.PeriodDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.PeriodDate);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.DirectCreditDebitType:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.DirectFormType.Name);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.DirectFormType.Name);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.TotalRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.TotalRecord);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.TotalRecord);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.TotalErrorRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.TotalErrorRecord);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.TotalErrorRecord);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.TotalAmount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.TotalAmount);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.TotalAmount);
                            break;

                        case DirectCreditDebitExportHeaderSortBy.ImportDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.ImportDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.ImportDate);
                            break;

                        case DirectCreditDebitExportHeaderSortBy.ImportBy:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.UpdatedBy.DisplayName);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.UpdatedBy.DisplayName);
                            break;

                        default:
                            query = query.OrderBy(o => o.DirectCreditDebitExportHeader.ImportDate);
                            break;
                    }
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.BacthID);
                    else query = query.OrderByDescending(o => o.BacthID);
                }
                else
                {
                    query = query.OrderBy(o => o.BacthID);
                }
            }
        }

        public static void SortByDetail(DirectCreditDebitExportDetailSortByParam sortByParam, ref IQueryable<DirectCreditDebitExportHeaderQueryResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case DirectCreditDebitExportDetailSortBy.BatchID:
                            if (sortByParam.Ascending) query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitExportDetail.BatchID);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitExportDetail.BatchID);
                            break;
                        case DirectCreditDebitExportDetailSortBy.Project:
                            if (sortByParam.Ascending) query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.Booking.Unit.Project.ProjectNameTH);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.Booking.Unit.Project.ProjectNameTH);
                            break;
                        case DirectCreditDebitExportDetailSortBy.UnitNo:
                            if (sortByParam.Ascending) query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.Booking.Unit.UnitNo);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.Booking.Unit.UnitNo);
                            break;
                        case DirectCreditDebitExportDetailSortBy.AccountNo:
                            if (sortByParam.Ascending) query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.AccountNO);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.AccountNO);
                            break;
                        case DirectCreditDebitExportDetailSortBy.PeriodDate:
                            if (sortByParam.Ascending) query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.DirectPeriod);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.DirectPeriod);
                            break;
                        case DirectCreditDebitExportDetailSortBy.DueDate:
                            if (sortByParam.Ascending) query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitExportDetail.UnitPriceInstallment.DueDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitExportDetail.UnitPriceInstallment.DueDate);
                            break;
                        case DirectCreditDebitExportDetailSortBy.AgreementNo:
                            if (sortByParam.Ascending) query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.Agreement.AgreementNo);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.Agreement.AgreementNo);
                            break;
                        case DirectCreditDebitExportDetailSortBy.CustomerName:
                            if (sortByParam.Ascending) query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.OwnerName);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.OwnerName);
                            break;
                        case DirectCreditDebitExportDetailSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            break;
                        case DirectCreditDebitExportDetailSortBy.Amount:
                            if (sortByParam.Ascending) query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => (o.DirectCreditDebitExportDetail.UnitPriceInstallment.Amount- o.DirectCreditDebitExportDetail.UnitPriceInstallment.PaidAmount));
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => (o.DirectCreditDebitExportDetail.UnitPriceInstallment.Amount - o.DirectCreditDebitExportDetail.UnitPriceInstallment.PaidAmount));
                            break;

                        case DirectCreditDebitExportDetailSortBy.DirectCreditDebitExportStatus:
                            if (sortByParam.Ascending) query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order);
                            break;

                        default:
                            query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitExportDetail.BatchID);
                            break;
                    }
                    if (sortByParam.Ascending) query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitExportDetail.BatchID);
                    else query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitExportDetail.BatchID);
                }
                else
                {
                    query = query.OrderByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitExportDetail.BatchID);
                }
            }
        }


        public static DirectCreditDebitExportHeaderDTO CreateFromQueryResult(DirectCreditDebitExportHeaderQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var oBacthID = DB.DirectCreditDebitExportDetails.Where(x => x.DirectCreditDebitExportHeaderID == model.DirectCreditDebitExportHeader.ID).OrderBy(x => x.Seq).Select(x => x.BatchID).FirstOrDefault();
                var result = new DirectCreditDebitExportHeaderDTO()
                {
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),
                    PeriodDate = model.DirectCreditDebitExportHeader.PeriodDate,
                    ReceiveDate = model.DirectCreditDebitExportHeader.ReceiveDate,
                    DirectFormType = MasterCenterDropdownDTO.CreateFromModel(model.DirectFormType),
                    TotalRecord = model.DirectCreditDebitExportHeader.TotalRecord,
                    TotalErrorRecord = model.DirectCreditDebitExportHeader.TotalErrorRecord,
                    TotalAmount = model.DirectCreditDebitExportHeader.TotalAmount,
                    ImportDate = model.DirectCreditDebitExportHeader.ImportDate,
                    Updated = model.DirectCreditDebitExportHeader.Updated,
                    Id = model.DirectCreditDebitExportHeader.ID,
                    BacthID = oBacthID,
                    ExportDate = model.DirectCreditDebitExportHeader.Created
                };
                return result;
            }
            else
            {
                return null;
            }
        }


        public static DirectCreditDebitExportHeaderDTO CreateFromByIDQueryResult(DirectCreditDebitExportHeaderQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = CreateFromQueryResult(model, DB);
                result.DirectCreditDebitExportDetail = new DirectCreditDebitExportDetailDTO();
                result.DirectCreditDebitExportDetail = DirectCreditDebitExportDetailDTO.CreateFromModel(model.DirectCreditDebitExportDetail, model.Agreement);
              
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
