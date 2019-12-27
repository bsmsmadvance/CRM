using AutoFixture;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using CustomAutoFixture;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Services;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sale.UnitTests
{
    public class CreditBankingServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;

        public CreditBankingServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetAgreementDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                       // var booking = await db.Units.Where(o => o.ID == new Guid("8C58791B-1AA5-4577-9207-00010F6C2D63")).FirstOrDefaultAsync();

                        var service = new CreditBankingService(db);
                        var results = await service.GetAgreementDataAsync(new Guid("0BF8AA5B-327F-4B97-947A-4E9DD5D0E1C2"));

                        //tran.Rollback();
                    }
                });
            }

        }
        
        [Fact]
        public async void GetCreditBankingTypeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync();

                        var service = new CreditBankingService(db);
                        var results = await service.GetCreditBankingTypeAsync(booking.ID);

                        tran.Rollback();
                    }
                });
            }

        }


        [Fact]
        public async void GetCreditBankingListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    { 
                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync();

                        var service = new CreditBankingService(db);
                        var result = await service.GetCreditBankingListAsync(booking.ID) ?? new List<CreditBankingDTO>();

                        tran.Rollback();

                    }
                });
            }
        }

        [Fact]
        public async void CreateCreditBankingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //var creditBankingService = new CreditBankingService(db);
                        var service = new CreditBankingService(db);
                        var model = new CreditBankingDTO();

                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync();
                        var financialInstitution = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "FinancialInstitution" && o.Key == "1").FirstOrDefaultAsync();
                        var loanStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LoanStatus" && o.Key == "1").FirstOrDefaultAsync();
                        var bank = await db.Banks.Where(o => o.ID == new Guid("7136b204-ac2d-4300-9747-9c8683c93192")).FirstOrDefaultAsync();
                        var bankBranche = await db.BankBranches.Where(o => o.BankID == bank.ID).FirstOrDefaultAsync();

                        model.Booking = BookingDropdownDTO.CreateFromModel(booking);
                        model.FinancialInstitution = MasterCenterDropdownDTO.CreateFromModel(financialInstitution);
                        model.Bank = BankDropdownDTO.CreateFromModel(bank);
                        model.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranche);
                        model.OtherBank = "";
                        model.LoanSubmitDate = DateTime.Now;
                        model.LoanStatus = MasterCenterDropdownDTO.CreateFromModel(loanStatus);

                        var result = await service.CreateCreditBankingAsync(model);


                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void CreateManyCreditBankingBankAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //var creditBankingService = new CreditBankingService(db);
                        var service = new CreditBankingService(db);
                        var model = new CreditBankingDTO();
                        var list = new List<CreditBankingDTO>();

                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync();
                        var financialInstitution = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "FinancialInstitution" && o.Key == "1").FirstOrDefaultAsync();
                        var loanStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LoanStatus" && o.Key == "3").FirstOrDefaultAsync();
                        var bank = await db.Banks.Where(o => o.BankNo == "006").FirstOrDefaultAsync();
                        var bankBranche = await db.BankBranches.Where(o => o.BankID == bank.ID).FirstOrDefaultAsync();

                        model.Booking = BookingDropdownDTO.CreateFromModel(booking);
                        model.FinancialInstitution = MasterCenterDropdownDTO.CreateFromModel(financialInstitution);
                        model.Bank = BankDropdownDTO.CreateFromModel(bank);
                        model.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranche);
                        model.OtherBank = "";
                        model.LoanSubmitDate = DateTime.Now;
                        model.LoanStatus = MasterCenterDropdownDTO.CreateFromModel(loanStatus);
                        list.Add(model);

                        service.CreateManyCreditBankingBankAsync(list);


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateCreditBankingPrintingHistoryAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CreditBankingService(db);
                        var model = new CreditBankingPrintingHistory();


                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync();

                        model.Booking = booking;
                        model.IsSelectAll = true;
                        model.IsPersonal = true;
                        model.IsCompany = true;
                        model.IsPartnership = true;
                        model.IsRegisteredStore = true;
                        model.IsNotRegisteredStore = true;

                        var result = await service.CreateCreditBankingPrintingHistoryAsync(model);


                        tran.Rollback();
                    }
                });
            }
        }
        

        [Fact]
        public async void UpdateCreditBankingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CreditBankingService(db);
                        var model = new CreditBankingDTO();

                        //var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync();
                        //var financialInstitution = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "FinancialInstitution" && o.Key == "1").FirstOrDefaultAsync();
                        //var loanStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LoanStatus" && o.Key == "3").FirstOrDefaultAsync();
                        //var bank = await db.Banks.Where(o => o.BankNo == "006").FirstOrDefaultAsync();
                        //var bankBranche = await db.BankBranches.Where(o => o.BankID == bank.ID).FirstOrDefaultAsync();

                        //model.Booking = BookingDropdownDTO.CreateFromModel(booking);
                        //model.FinancialInstitution = MasterCenterDropdownDTO.CreateFromModel(financialInstitution);
                        //model.Bank = BankDropdownDTO.CreateFromModel(bank);
                        //model.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranche);
                        //model.OtherBank = "";
                        //model.LoanSubmitDate = DateTime.Now;
                        //model.LoanStatus = MasterCenterDropdownDTO.CreateFromModel(loanStatus);
                        var creditbank = await db.CreditBankings.Where(o => o.ID == new Guid("9C189840-C3B6-467C-9693-3CADFD1DF1FE")).FirstOrDefaultAsync();
                        //create
                        //var resultCreate = await service.CreateCreditBankingAsync(model);

                        //update
                        var bank2 = await db.Banks.Where(o => o.ID == new Guid("A809F16D-2154-4026-BFDD-13465B20E382")).FirstOrDefaultAsync();
                        var bankBranche2 = await db.BankBranches.Where(o => o.BankID == bank2.ID).FirstOrDefaultAsync();
                        model.Bank = BankDropdownDTO.CreateFromModel(bank2);
                        model.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranche2);

                        var result = await service.UpdateCreditBankingAsync(creditbank.ID,model);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateCreditBankingTypeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CreditBankingService(db);
                        var model = new BookingDTO();


                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync(); 
                        var creditBankingType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditBankingType" && o.Key == "2").FirstOrDefaultAsync();

                        booking.CreditBankingType = creditBankingType;
                        booking.CreditBankingTypeMasterCenterID = creditBankingType.ID;

                        model = await BookingDTO.CreateFromModelAsync(booking , db);

                        var result = await service.UpdateCreditBankingTypeAsync(booking.ID, model);

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void DeleteCreditBankingsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CreditBankingService(db);
                        var model = new CreditBankingDTO();

                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001400").FirstOrDefaultAsync();
                        var financialInstitution = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "FinancialInstitution" && o.Key == "1").FirstOrDefaultAsync();
                        var loanStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LoanStatus" && o.Key == "3").FirstOrDefaultAsync();
                        var bank = await db.Banks.Where(o => o.BankNo == "006").FirstOrDefaultAsync();
                        var bankBranche = await db.BankBranches.Where(o => o.BankID == bank.ID).FirstOrDefaultAsync();

                        model.Booking = BookingDropdownDTO.CreateFromModel(booking);
                        model.FinancialInstitution = MasterCenterDropdownDTO.CreateFromModel(financialInstitution);
                        model.Bank = BankDropdownDTO.CreateFromModel(bank);
                        model.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranche);
                        model.OtherBank = "";
                        model.LoanSubmitDate = DateTime.Now;
                        model.LoanStatus = MasterCenterDropdownDTO.CreateFromModel(loanStatus);

                        var result = await service.CreateCreditBankingAsync(model);    
                        await service.DeleteCreditBankingsAsync(result.Id.Value);

                        tran.Rollback();

                    }
                });
            }
        }
    }
}
