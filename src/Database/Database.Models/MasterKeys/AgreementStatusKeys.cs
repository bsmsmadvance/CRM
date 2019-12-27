using System;
namespace Database.Models
{
    public class AgreementStatusKeys
    {
        /// <summary>
        /// รอทำสัญญา
        /// </summary>
        public static string WaitingForContract = "1";
        /// <summary>
        /// รอลงนามสัญญา
        /// </summary>
        public static string WaitingForSignContract = "2";
        /// <summary>
        /// รอโอนกรรมสิทธิ์
        /// </summary>
        public static string WaitingForTransfer = "3";
        /// <summary>
        /// โอนกรรมสิทธิ์แล้ว
        /// </summary>
        public static string Transfer = "4";
    }
}
