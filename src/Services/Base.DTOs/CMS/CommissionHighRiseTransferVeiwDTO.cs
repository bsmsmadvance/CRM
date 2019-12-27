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
    public class CommissionHighRiseTransferVeiwDTO : BaseDTO
    {
        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// LC โอน
        /// </summary>
        [Description("LC โอน")]
        public USR.UserListDTO LCTransfer { get; set; }

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
        /// มูลค่าโอนจริง
        /// </summary>
        [Description("มูลค่าโอนจริง")]
        public decimal? NetSellPrice { get; set; }

        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        [Description("วันที่โอนจริง")]
        public DateTime? TransferDate { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่นโอน
        /// </summary>
        [Description("ค่าคอมมิสชั่นโอน")]
        public decimal? LCTransferPaid { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่นที่จ่ายในเดือนนี้
        /// </summary>
        [Description("ค่าคอมมิสชั่นที่จ่ายในเดือนนี้")]
        public decimal? CommissionForThisMonth { get; set; }


        public static CommissionHighRiseTransferVeiwDTO CreateFromQueryResult(CommissionHighRiseTransferVeiwQueryResult model)
        {
            if (model != null)
            {
                var result = new CommissionHighRiseTransferVeiwDTO()
                {
                    Id = model.CalculateHighRiseTransfer?.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.CalculateHighRiseTransfer.Transfer.Unit),
                    LCTransfer = USR.UserListDTO.CreateFromModel(model.CalculateHighRiseTransfer.LCTransfer),
                    CommissionPercentRate = model.CalculateHighRiseTransfer.CommissionPercentRate,
                    CommissionPercentType = model.CalculateHighRiseTransfer.CommissionPercentType,
                    NetSellPrice = model.Transfer.NetSellPrice ?? 0,
                    TransferDate = model.Transfer.TransferDate,
                    LCTransferPaid = model.CalculateHighRiseTransfer.LCTransferPaid,
                    CommissionForThisMonth = model.CalculateHighRiseTransfer.LCTransferPaid ?? 0
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CommissionHighRiseTransferVeiwSortByParam sortByParam, ref IQueryable<CommissionHighRiseTransferVeiwQueryResult> query)
        {
            IOrderedQueryable<CommissionHighRiseTransferVeiwQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CommissionHighRiseTransferVeiwSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseTransfer.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.UnitNo);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.UnitNo);
            }

            orderQuery.ThenBy(o => o.CalculateHighRiseTransfer.ID);
            query = orderQuery;
        }

    }

    public class CommissionHighRiseTransferVeiwQueryResult
    {
        public CalculateHighRiseTransfer CalculateHighRiseTransfer { get; set; }
        public CommissionContract Contract { get; set; }
        public CommissionTransfer Transfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public User LCTransferName { get; set; }
    }
}
