using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.FIN;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using Database.Models.MST;
using Database.Models.MasterKeys;

namespace Base.DTOs
{
    public static class Extensions
    {
        public static string LeadingDigit(this string value, int Digit, string exten)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(exten))
            {
                string sZero = "";
                for (int i = 0; i + value.Length < Digit; i++)
                {
                    sZero += exten;
                }

                value = sZero + value;
            }

            return value;
        }

        public async static Task<UnknownPayment> UpdateUnknownPaymentStatusAsync(this DatabaseContext db, Guid id)
        {
            string UnknownPaymentStatusKey = "";

            var UnknownPaymentModel = await db.UnknownPayments.Where(o => o.ID == id).FirstOrDefaultAsync();

            if (UnknownPaymentModel.UnknownPaymentCode != null)
            {

                var PaymentUnknownPaymentList = await db.PaymentUnknownPayments
                    .Include(o => o.PaymentMethod)
                        .ThenInclude(o => o.Payment)
                    .Where(o => o.UnknownPaymentID == id && o.IsDeleted == true).ToListAsync() ?? new List<PaymentUnknownPayment>();

                var SumReverseAmount = PaymentUnknownPaymentList.Sum(o => o.PaymentMethod.Payment.TotalAmount);

                if (SumReverseAmount == 0)
                    UnknownPaymentStatusKey = UnknownPaymentStatusKeys.Wait;
                else if (UnknownPaymentModel.Amount > SumReverseAmount)
                    UnknownPaymentStatusKey = UnknownPaymentStatusKeys.Partial;
                else if (UnknownPaymentModel.Amount == SumReverseAmount)
                    UnknownPaymentStatusKey = UnknownPaymentStatusKeys.Complete;
            }

            if (UnknownPaymentStatusKey != "")
            {
                var MasterCenter = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnknownPaymentStatus && o.Key == UnknownPaymentStatusKey).FirstOrDefault() ?? new MasterCenter();
                UnknownPaymentModel.UnknownPaymentStatusID = MasterCenter.ID;

                db.Entry(UnknownPaymentModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return UnknownPaymentModel;

        }

        public static string GetFETCustomerName(this DatabaseContext db, Guid BookingID)
        {
            var CustomerName = "";

            if (BookingID != Guid.Empty)
            {
                List<string> CustomerList = new List<string>();

                var AgreementModel = db.Agreements.Where(e => e.BookingID == BookingID).FirstOrDefault() ?? new Agreement();

                if (AgreementModel.BookingID != null)
                {
                    var AgreementOwnerModel = db.AgreementOwners.Where(e => e.AgreementID == AgreementModel.ID && !e.IsThaiNationality).ToList() ?? new List<AgreementOwner>();
                    if (AgreementOwnerModel.Any())
                        CustomerList = AgreementOwnerModel.Select(e => (e.FirstNameEN ?? "") + " " + (e.LastNameEN ?? "")).ToList() ?? new List<string>();
                }
                else
                {
                    var BookingOwnerModel = db.BookingOwners.Where(e => e.BookingID == BookingID && !e.IsThaiNationality).ToList() ?? new List<BookingOwner>();
                    if (BookingOwnerModel.Any())
                        CustomerList = BookingOwnerModel.Select(e => (e.FirstNameEN ?? "") + " " + (e.LastNameEN ?? "")).ToList() ?? new List<string>();
                }


                if (CustomerList.Any())
                {
                    foreach (var name in CustomerList)
                    {
                        CustomerName += "," + name;
                    }

                    CustomerName = (CustomerName.Length > 2) ? CustomerName.Substring(1, CustomerName.Length - 1) : CustomerName;
                }
            }

            return CustomerName;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
