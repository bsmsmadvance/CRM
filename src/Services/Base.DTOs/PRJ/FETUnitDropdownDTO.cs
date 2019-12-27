﻿using Database.Models;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;

namespace Base.DTOs.PRJ
{
    public class FETUnitDropdownDTO
    {
        /// <summary>
        /// Identity UnitID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// จอง
        /// </summary>
        public Booking Booking { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        public Agreement Agreement { get; set; }

        public string CustomerName { get; set; }

        public static FETUnitDropdownDTO CreateFromQueryResult(FETUnitQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new FETUnitDropdownDTO
                {
                    Id = model.Unit.ID,
                    UnitNo = model.Unit.UnitNo,
                    Booking = model.Booking,
                    Agreement = model.Agreement
                };

                result.CustomerName = db.GetFETCustomerName(model.Booking.ID);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
    public class FETUnitQueryResult
    {
        public Unit Unit { get; set; }
        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Agreement Agreement { get; set; }
    }

}
