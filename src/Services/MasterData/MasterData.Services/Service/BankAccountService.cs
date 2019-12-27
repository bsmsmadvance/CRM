using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;

namespace MasterData.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly DatabaseContext DB;

        public BankAccountService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<BankAccountDropdownDTO>> GetBankAccountDropdownListAsync(string displayName, string bankAccountTypeKey, Guid? companyID)
        {
            var gLAccountTypeMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == MasterCenterGroupKeys.GLAccountType)
                                                                           .Select(o => o.ID)
                                                                           .FirstAsync();

            var query = DB.BankAccounts.Include(o => o.Bank).Include(o => o.BankAccountType).Where(o => o.GLAccountTypeMasterCenterID == gLAccountTypeMasterCenterID).AsQueryable();
            if (!string.IsNullOrEmpty(displayName))
            {
                query = from o in query
                        let dName = o.Bank.Alias + " " + o.BankAccountType.Name + " " + o.BankAccountNo
                        where dName.Contains(displayName.Replace("-", string.Empty))
                        select o;
            }
            if (!string.IsNullOrEmpty(bankAccountTypeKey))
            {
                var bankAccountTypeMasterCenterID = await DB.MasterCenters.Where(o => o.Key == bankAccountTypeKey && o.MasterCenterGroupKey == MasterCenterGroupKeys.BankAccountType)
                                                                          .Select(o => o.ID)
                                                                          .FirstAsync();
                query = from o in query
                        let dName = o.Bank.Alias + " " + o.BankAccountType.Name + " " + o.BankAccountNo
                        where o.BankAccountTypeMasterCenterID == bankAccountTypeMasterCenterID
                        select o;
            }
            if (companyID != null)
            {
                query = from o in query
                        let dName = o.Bank.Alias + " " + o.BankAccountType.Name + " " + o.BankAccountNo
                        where o.CompanyID == companyID
                        select o;
            }
            var results = await query.Take(100).Select(o => BankAccountDropdownDTO.CreateFromModel(o)).ToListAsync();

            return results;
        }

        /// <summary>
        /// ดึงรายการบัญชีธนาคาร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367408/preview
        /// </summary>
        /// <returns>The bank account list async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<BankAccountPaging> GetBankAccountListAsync(BankAccountFilter filter, PageParam pageParam, BankAccountSortByParam sortByParam)
        {
            IQueryable<BankAccountQueryResult> query = DB.BankAccounts.Select(o =>
                                                                  new BankAccountQueryResult
                                                                  {
                                                                      BankAccount = o,
                                                                      Bank = o.Bank,
                                                                      BankAccountType = o.BankAccountType,
                                                                      BankBranch = o.BankBranch,
                                                                      Company = o.Company,
                                                                      Province = o.Province,
                                                                      GLAccountType = o.GLAccountType,
                                                                      UpdatedBy = o.UpdatedBy
                                                                  });

            #region Filter
            if (filter.BankID != null && filter.BankID != Guid.Empty)
            {
                query = query.Where(o => o.Bank.ID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.BankBranchName))
            {
                query = query.Where(o => o.BankBranch.Name.Contains(filter.BankBranchName));
            }
            if (!string.IsNullOrEmpty(filter.BankAccountNo))
            {
                query = query.Where(o => o.BankAccount.BankAccountNo.Contains(filter.BankAccountNo));
            }
            if (!string.IsNullOrEmpty(filter.BankAccountTypeKey))
            {
                var bankAccountTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.BankAccountTypeKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.BankAccountType)
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.BankAccountType.ID == bankAccountTypeMasterCenterID);
            }

            if (filter.CompanyID != null && filter.CompanyID != Guid.Empty)
            {
                query = query.Where(o => o.Company.ID == filter.CompanyID);
            }
            if (!string.IsNullOrEmpty(filter.GLAccountNo))
            {
                query = query.Where(o => o.BankAccount.GLAccountNo.Contains(filter.GLAccountNo));
            }
            if (!string.IsNullOrEmpty(filter.GLRefCode))
            {
                query = query.Where(o => o.BankAccount.GLRefCode.Contains(filter.GLRefCode));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(o => o.BankAccount.Name.Contains(filter.Name));
            }
            if (filter.IsActive != null)
            {
                query = query.Where(o => o.BankAccount.IsActive == filter.IsActive);
            }
            if (filter.HasVat != null)
            {
                query = query.Where(o => o.BankAccount.HasVat == filter.HasVat);
            }
            if (!string.IsNullOrEmpty(filter.GLAccountTypeKey))
            {
                var gLAccountTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.GLAccountTypeKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.GLAccountType)
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.GLAccountType.ID == gLAccountTypeMasterCenterID);
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(o => o.BankAccount.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(o => o.BankAccount.Updated <= filter.UpdatedTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(o => o.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }

            #endregion

            BankAccountDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<BankAccountQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => BankAccountDTO.CreateFromQueryResult(o)).ToList();

            return new BankAccountPaging()
            {
                PageOutput = pageOutput,
                BankAccounts = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367410/preview
        /// </summary>
        /// <returns>The bank account detail async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<BankAccountDTO> GetBankAccountDetailAsync(Guid id)
        {
            var model = await DB.BankAccounts.Where(o => o.ID == id)
                                             .Include(o => o.Bank)
                                             .Include(o => o.BankAccountType)
                                             .Include(o => o.BankBranch)
                                             .Include(o => o.Company)
                                             .Include(o => o.Province)
                                             .Include(o => o.GLAccountType)
                                             .Include(o => o.UpdatedBy)
                                             .FirstAsync();
            var result = BankAccountDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367410/preview
        /// </summary>
        /// <returns>The bank account async.</returns>
        /// <param name="input">Input.</param>
        public async Task<BankAccountDTO> CreateBankAccountAsync(BankAccountDTO input)
        {
            await input.ValidateAsync(DB);
            BankAccount model = new BankAccount();
            var glAccountTypeID = await DB.MasterCenters
                .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.GLAccountType && o.Key == "1")
                .Select(o => o.ID)
                .FirstAsync();

            input.GLAccountType = new MasterCenterDropdownDTO()
            {
                Id = glAccountTypeID
            };

            input.ToModel(ref model);

            model.Name = input.Bank?.NameTH + " " + input.BankAccountType?.Name + " " + input.BankAccountNo;

            await DB.BankAccounts.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetBankAccountDetailAsync(model.ID);
            return result;
        }

        public async Task<BankAccountDTO> CreateChartOfAccountAsync(BankAccountDTO input)
        {
            await input.ValidateChartOfAccountAsync(DB);
            BankAccount model = new BankAccount();
            input.ToModel(ref model);
            var key = "GL";
            var type = "MST.BankAccount";
            var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
            if (runningno == null)
            {
                var runningNumberCounter = new RunningNumberCounter
                {
                    Key = key,
                    Type = type,
                    Count = 1
                };
                await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                await DB.SaveChangesAsync();

                model.GLRefCode = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
            }
            else
            {
                model.GLRefCode = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
            }
            await DB.BankAccounts.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetBankAccountDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367410/preview
        /// </summary>
        /// <returns>The bank account async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<BankAccountDTO> UpdateBankAccountAsync(Guid id, BankAccountDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.BankAccounts.Where(x => x.ID == id)
                                             .FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetBankAccountDetailAsync(model.ID);
            return result;
        }

        public async Task<BankAccountDTO> UpdateChartOfAccountAsync(Guid id, BankAccountDTO input)
        {
            await input.ValidateChartOfAccountAsync(DB);
            var model = await DB.BankAccounts.Where(x => x.ID == id)
                                             .FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetBankAccountDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367408/preview
        /// </summary>
        /// <returns>The bank account async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteBankAccountAsync(Guid id)
        {
            var model = await DB.BankAccounts.Where(x => x.ID == id)
                                             .FirstAsync();
            model.IsDeleted = true;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367408/preview
        /// </summary>
        public async Task DeleteBankAccountListAsync(List<BankAccountDTO> inputs)
        {
            foreach (var item in inputs)
            {
                var model = await DB.BankAccounts.Where(x => x.ID == item.Id).FirstAsync();
                model.IsDeleted = true;

                DB.Entry(model).State = EntityState.Modified;
            }
            await DB.SaveChangesAsync();
        }

    }
}
