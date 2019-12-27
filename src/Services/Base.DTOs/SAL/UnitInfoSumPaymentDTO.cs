using Database.Models.FIN;
using Database.Models.PRJ;
using Database.Models.SAL;

namespace Base.DTOs.SAL
{
    public class UnitInfoSumPaymentDTO
    {
        /// <summary>
        /// รวมเงินชำระ
        /// </summary>
        public decimal SumPayment { get; set; }

        /// <summary>
        /// จำนวนเงินที่ค้างชำระ
        /// </summary>
        public decimal SumWaitingForPayment { get; set; }

        /// <summary>
        /// จำนวนเงินที่ค้างชำระ Overdue
        /// </summary>
        public decimal SumOverdueBalance { get; set; }

        /// <summary>
        /// จำนวนงวดที่ค้างชำระ
        /// </summary>
        public int SumOverduePeriod { get; set; }

        /// <summary>
        /// เงินทราบผู้โอน
        /// </summary>
        public decimal OtherPayment { get; set; }
    }

    public class UnitInfoSumPaymentQueryResult
    {
        public Unit Unit { get; set; }
        public Booking Booking { get; set; }

        public Payment Payment { get; set; }
    }

}
