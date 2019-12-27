using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services.Service
{
    public class CreditBankingService : ICreditBankingService
    {
        private readonly DatabaseContext DB;

        public CreditBankingService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<BookingDTO> GetCreditBankingTypeAsync(Guid BookingId)
        {
            var model = await DB.Bookings
                .Include(o=>o.BookingStatus)
                .Include(o=>o.Unit)
                .Include(o=>o.Project)
                .Include(o=>o.Model)
                .Include(o=>o.SaleOfficerType)
                .Include(o=>o.SaleUser)
                .Include(o=>o.Agent)
                .Include(o=>o.AgentEmployee)
                .Include(o=>o.ProjectSaleUser)
                .Include(o=>o.CreateBookingFrom)
                .Include(o=>o.CreditBankingType)
                //.Include(o=>o.MinPriceBudgetWorkflow)
                //.Include(o=>o.MinPriceRequestReason)
                .Where(o => o.ID == BookingId)
                .FirstOrDefaultAsync();

            return await BookingDTO.CreateFromModelAsync(model, DB) ?? new BookingDTO();
        }
        
        public async Task<MortgageInfoDTO> GetAgreementDataAsync(Guid unitId)
        {       
            return await MortgageInfoDTO.CreateFromModelAsync(unitId, DB) ?? new MortgageInfoDTO();
        }

        public async Task<List<CreditBankingDTO>> GetCreditBankingListAsync(Guid BookingId)
        {
            var model = await DB.CreditBankings
                .Include(o => o.Bank)
                .Include(o => o.BankBranch)
                .Include(o => o.LoanStatus)
                .Include(o => o.FinancialInstitution)
                .Include(o => o.BankBranch.Province)
                .Include(o => o.BankReason)
                .Include(o => o.UseBankReason)
                .Include(o => o.NotUseBankReason)
                .Include(o => o.BankRejectReason)
                .Include(o => o.BankWaitingReason)
                .Where(o => o.BookingID == BookingId).ToListAsync();

            var result = model.Select(o => CreditBankingDTO.CreateFromModel(o)).ToList();
            return result ?? new List<CreditBankingDTO>();
        }

        public async Task<BookingDTO> UpdateCreditBankingTypeAsync(Guid bookingId, BookingDTO input)
        {

            var model = await DB.Bookings.Include(o => o.UpdatedBy).Where(o => o.ID == bookingId).FirstOrDefaultAsync();

            //var CreditBankingTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.FinancialInstitutionMasterCenterID && o.Key == input.CreditBankingType.Key).Select(o => o.ID).FirstAsync();

            if (model != null)
            {
                //Booqking booking = new Booking()
                //{
                //    CreditBankingTypeMasterCenterID = CreditBankingTypeMasterCenterID
                //};

                //await DB.Bookings.AddAsync(booking);

                model.CreditBankingTypeMasterCenterID = input.CreditBankingType.Id;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await BookingDTO.CreateFromModelAsync(model, DB);
            return result ?? new BookingDTO();
            
        }

        public async Task<CreditBankingDTO> CreateCreditBankingAsync(CreditBankingDTO input)
        {
            await input.ValidateAsync(DB);

            var model = new CreditBanking();
            input.ToModel(ref model);

            await DB.CreditBankings.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await CreditBankingDTO.CreateFromModelAsync(model, DB);
            return result ?? new CreditBankingDTO();


        }

     
        public void CreateManyCreditBankingBankAsync(List<CreditBankingDTO> listInput)
        {
            foreach (var input in listInput)
            {
                 //input.ValidateAsync(DB);

                var model = new CreditBanking();
                input.ToModel(ref model);

                DB.CreditBankings.Add(model);
            }
            
             DB.SaveChanges();
        }


        public async Task<CreditBankingDTO> UpdateCreditBankingAsync(Guid id, CreditBankingDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.CreditBankings.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = CreditBankingDTO.CreateFromModel(model);
            return result ?? new CreditBankingDTO();
        }

        //public async Task<CreditBanking> DeleteCreditBankingAsync(Guid id)
        //{
        //    return null;
        //}

        public async Task DeleteCreditBankingsAsync(Guid id)
        {
            var model = await DB.CreditBankings.Where(c => c.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<CreditBankingPrintingHistory> CreateCreditBankingPrintingHistoryAsync(CreditBankingPrintingHistory input)
        {
           

            await DB.CreditBankingPrintingHistories.AddAsync(input);
            await DB.SaveChangesAsync();

            return input ?? new CreditBankingPrintingHistory();
        }
    }
}
