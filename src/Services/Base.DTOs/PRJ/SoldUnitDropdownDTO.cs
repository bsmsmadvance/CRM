using Database.Models.PRJ;
using Database.Models.SAL;
using System;

namespace Base.DTOs.PRJ
{
    public class SoldUnitDropdownDTO
    {
        /// <summary>
        /// UnitID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// UnitID
        /// </summary>
        public Guid? BookingID { get; set; }


        ///// <summary>
        ///// จอง
        ///// </summary>
        //public Booking Booking { get; set; }

        ///// <summary>
        ///// สัญญา
        ///// </summary>
        //public Agreement Agreement { get; set; }

        public static SoldUnitDropdownDTO CreateFromQueryResult(SoldUnitQueryResult model)
        {
            if (model != null)
            {
                var result = new SoldUnitDropdownDTO
                {
                    Id = model.Unit.ID,
                    UnitNo = model.Unit.UnitNo,
                    BookingID = model.Booking.ID
                    //Booking = model.Booking,
                    //Agreement = model.Agreement,
                };

                return result;
            }
            else
            {
                return null;
            }
        }

    }
    public class SoldUnitQueryResult
    {
        public Unit Unit { get; set; }
        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Agreement Agreement { get; set; }
        public Transfer Transfer { get; set; }
    }

}
