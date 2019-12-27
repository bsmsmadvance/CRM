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
    public class CommissionLowRiseVeiwDTO : BaseDTO
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
        /// วันที่โอนจริง
        /// </summary>
        [Description("วันที่โอนจริง")]
        public DateTime? ActualTransferDate { get; set; }


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
        /// ค่าคอมมิสชั่น New Launch LC ปิดการขาย (30%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น New Launch LC ปิดการขาย (30%)")]
        public decimal? SaleUserNewLaunchPaid { get; set; }
        /// <summary>
        /// ค่าคอมมิสชั่น New Launch LC ประจำโครงการ (70%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น New Launch LC ประจำโครงการ (70%)")]
        public decimal? ProjectSaleNewLaunchPaid { get; set; }
        /// <summary>
        /// รวมค่าคอมมิสชั่น New Launch (100%)
        /// </summary>
        [Description("รวมค่าคอมมิสชั่น New Launch (100%)")]
        public decimal? TotalNewLaunchPaid { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่นที่จ่ายในเดือนนี้
        /// </summary>
        [Description("ค่าคอมมิสชั่นที่จ่ายในเดือนนี้")]
        public decimal? CommissionForThisMonth { get; set; }


        //public static CommissionLowRiseVeiwDTO CreateFromModel(CalculateLowRiseSale model)
        //{
        //    if (model != null)
        //    {
        //        var result = new CommissionLowRiseVeiwDTO()
        //        {
        //            Id = model.ID,
        //            Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
        //            SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
        //            ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.ProjectSaleUser),
        //            CommissionPercentRate = model.CommissionPercentRate,
        //            CommissionPercentType = model.CommissionPercentType,
        //            //TotalContractNetAmount = model.Agreement,
        //            //ActualTransferDate = model.Transfer.ActualTransferDate,
        //            SaleUserSalePaid = model.SaleUserSalePaid,
        //            ProjectSaleSalePaid = model.ProjectSaleSalePaid,
        //            TotalSalePaid = model.SaleUserSalePaid ?? 0 + model.ProjectSaleSalePaid ?? 0,
        //            SaleUserNewLaunchPaid = model.SaleUserNewLaunchPaid,
        //            ProjectSaleNewLaunchPaid = model.ProjectSaleNewLaunchPaid,
        //            TotalNewLaunchPaid = model.SaleUserNewLaunchPaid ?? 0 + model.ProjectSaleNewLaunchPaid ?? 0,
        //            CommissionForThisMonth = model.CommissionForThisMonth,

        //        };
        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static CommissionLowRiseVeiwDTO CreateFromQueryResult(CommissionLowRiseVeiwQueryResult model)
        {
            if (model != null)
            {
                var result = new CommissionLowRiseVeiwDTO()
                {
                    Id = model.CalculateLowRiseSale?.ID ?? model.CalculateLowRiseTransfer?.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.CalculateLowRiseSale.Agreement.Unit ?? model.CalculateLowRiseTransfer.Transfer.Unit),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.CalculateLowRiseSale.SaleUser ?? model.CalculateLowRiseTransfer.SaleUser),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.CalculateLowRiseSale.ProjectSaleUser ?? model.CalculateLowRiseTransfer.SaleUser),
                    CommissionPercentRate = model.CalculateLowRiseSale.CommissionPercentRate ?? model.CalculateLowRiseTransfer.CommissionPercentRate,
                    CommissionPercentType = model.CalculateLowRiseSale.CommissionPercentType ?? model.CalculateLowRiseTransfer.CommissionPercentType,
                    TotalContractNetAmount = model.Contract.SellingPrice - model.Contract.TransferDiscount ?? 0 - model.Contract.FreeDownAmount ?? 0,
                    ActualTransferDate = model.Transfer.TransferDate,
                    SaleUserSalePaid = model.CalculateLowRiseSale.SaleUserSalePaid ?? model.CalculateLowRiseTransfer.SaleUserSalePaid,
                    ProjectSaleSalePaid = model.CalculateLowRiseSale.ProjectSaleSalePaid ?? model.CalculateLowRiseTransfer.ProjectSaleSalePaid,
                    TotalSalePaid = ((model.CalculateLowRiseSale != null) ? model.CalculateLowRiseSale.SaleUserSalePaid ?? 0 + model.CalculateLowRiseSale.ProjectSaleSalePaid ?? 0 :
                                                               model.CalculateLowRiseTransfer.SaleUserSalePaid ?? 0 + model.CalculateLowRiseTransfer.ProjectSaleSalePaid ?? 0),
                    SaleUserNewLaunchPaid = model.CalculateLowRiseSale.SaleUserNewLaunchPaid ?? 0,
                    ProjectSaleNewLaunchPaid = model.CalculateLowRiseSale.ProjectSaleNewLaunchPaid ?? 0,
                    TotalNewLaunchPaid = model.CalculateLowRiseSale.SaleUserNewLaunchPaid ?? 0 + model.CalculateLowRiseSale.ProjectSaleNewLaunchPaid ?? 0,
                    CommissionForThisMonth = model.CalculateLowRiseSale.SaleUserSalePaid ?? model.CalculateLowRiseTransfer.SaleUserSalePaid ?? 0
                                                + model.CalculateLowRiseSale.ProjectSaleSalePaid ?? model.CalculateLowRiseTransfer.ProjectSaleSalePaid ?? 0
                                                + model.CalculateLowRiseSale.SaleUserNewLaunchPaid ?? 0 
                                                + model.CalculateLowRiseSale.ProjectSaleNewLaunchPaid ?? 0
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CommissionLowRiseVeiwSortByParam sortByParam, ref IQueryable<CommissionLowRiseVeiwQueryResult> query)
        {
            IOrderedQueryable<CommissionLowRiseVeiwQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CommissionLowRiseVeiwSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
                        break;
                    //case CommissionLowRiseVeiwSortBy.CalculateUserName:
                    //    if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionLowRiseVeiw.CreatedBy.DisplayName);
                    //    else orderQuery = query.OrderByDescending(o => o.CommissionLowRiseVeiw.CreatedBy.DisplayName);
                    //    break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
            }

            orderQuery.ThenBy(o => o.CalculateLowRiseSale.ID);
            query = orderQuery;
        }

    }

    public class CommissionLowRiseVeiwQueryResult
    {
        public CalculateLowRiseSale CalculateLowRiseSale { get; set; }
        public CalculateLowRiseTransfer CalculateLowRiseTransfer { get; set; }
        public CommissionContract Contract { get; set; }
        public CommissionTransfer Transfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public User SaleUserName { get; set; }
        public User ProjectSaleUserName { get; set; }
    }
}
