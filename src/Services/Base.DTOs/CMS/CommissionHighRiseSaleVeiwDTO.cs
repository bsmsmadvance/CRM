using Database.Models;
using Database.Models.CMS;
using Database.Models.USR;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CMS
{
    public class CommissionHighRiseSaleVeiwDTO : BaseDTO
    {
        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// LC ปิดการขาย
        /// </summary>
        [Description("LC ปิดการขาย")]
        public USR.UserListDTO SaleUser { get; set; }

        /// <summary>
        /// LC ประจำโครงการ
        /// </summary>
        [Description("LC ประจำโครงการ")]
        public USR.UserListDTO ProjectSaleUser { get; set; }

        /// <summary>
        /// อัตรา % Commission
        /// </summary>
        [Description("อัตรา % Commission")]
        public decimal? CommissionPercentRate { get; set; }

        /// <summary>
        /// ประเภท Rate Commission
        /// </summary>
        [Description("ประเภท Rate Commission")]
        public string CommissionPercentType { get; set; }

        /// <summary>
        /// มูลค่าสัญญา
        /// </summary>
        [Description("มูลค่าสัญญา")]
        public decimal? TotalContractNetAmount { get; set; }

        /// <summary>
        /// วันที่อนุมัติสัญญา
        /// </summary>
        [Description("วันที่อนุมัติสัญญา")]
        public DateTime? SignAgreementDate { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่น LC ปิดการขาย (30%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น LC ปิดการขาย (30%)")]
        public decimal? SaleUserSalePaid { get; set; }
        /// <summary>
        /// ค่าคอมมิสชั่น LC ประจำโครงการ (70%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น LC ประจำโครงการ (70%)")]
        public decimal? ProjectSaleSalePaid { get; set; }
        /// <summary>
        /// รวมค่าคอมมิสชั่น (100%)
        /// </summary>
        [Description("รวมค่าคอมมิสชั่น (100%)")]
        public decimal? TotalSalePaid { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่นที่จ่ายในเดือนนี้
        /// </summary>
        [Description("ค่าคอมมิสชั่นที่จ่ายในเดือนนี้")]
        public decimal? CommissionForThisMonth { get; set; }


        public static CommissionHighRiseSaleVeiwDTO CreateFromQueryResult(CommissionHighRiseSaleVeiwQueryResult model)
        {
            if (model != null)
            {
                var result = new CommissionHighRiseSaleVeiwDTO()
                {
                    Id = model.CalculateHighRiseSale?.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.CalculateHighRiseSale.Agreement.Unit),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.CalculateHighRiseSale.SaleUser),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.CalculateHighRiseSale.ProjectSaleUser),
                    CommissionPercentRate = model.CalculateHighRiseSale.CommissionPercentRate,
                    CommissionPercentType = model.CalculateHighRiseSale.CommissionPercentType,
                    TotalContractNetAmount = model.Contract.SellingPrice - model.Contract.TransferDiscount ?? 0 - model.Contract.FreeDownAmount ?? 0,
                    SignAgreementDate = model.Contract.ApproveDate,
                    SaleUserSalePaid = model.CalculateHighRiseSale.SaleUserSalePaid,
                    ProjectSaleSalePaid = model.CalculateHighRiseSale.ProjectSaleSalePaid,
                    TotalSalePaid = model.CalculateHighRiseSale.SaleUserSalePaid ?? 0 + model.CalculateHighRiseSale.ProjectSaleSalePaid ?? 0,
                    CommissionForThisMonth = model.CalculateHighRiseSale.SaleUserSalePaid ?? 0
                                                + model.CalculateHighRiseSale.ProjectSaleSalePaid ?? 0
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CommissionHighRiseSaleVeiwSortByParam sortByParam, ref IQueryable<CommissionHighRiseSaleVeiwQueryResult> query)
        {
            IOrderedQueryable<CommissionHighRiseSaleVeiwQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CommissionHighRiseSaleVeiwSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseSale.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.UnitNo);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.UnitNo);
            }

            orderQuery.ThenBy(o => o.CalculateHighRiseSale.ID);
            query = orderQuery;
        }

    }

    public class CommissionHighRiseSaleVeiwQueryResult
    {
        public CalculateHighRiseSale CalculateHighRiseSale { get; set; }
        //public CalculateHighRiseTransfer CalculateHighRiseTransfer { get; set; }
        public CommissionContract Contract { get; set; }
        //public CommissionTransfer Transfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public User SaleUserName { get; set; }
        public User ProjectSaleUserName { get; set; }
    }
}
