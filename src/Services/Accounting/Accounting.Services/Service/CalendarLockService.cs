using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.Params.Filters;
using Base.DTOs.ACC;

using Accounting.Services.IService;
using Database.Models.ACC;
using System.Globalization;
using static Base.DTOs.ACC.CalendarLockDTO;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;
using Database.Models.USR;
using Database.Models.MST;

namespace Accounting.Services.Service
{
    public class CalendarLockService : ICalendarLockService //#dev
    {
        private readonly DatabaseContext DB;

        public CalendarLockService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task ValidateAsync(DatabaseContext db, List<string> tDate, int nGuid)
        {
            var CalendarLock = new CalendarLockDTO();

            ValidateException ex = new ValidateException();
            if (nGuid > 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                string desc = CalendarLock.GetType().GetProperty(nameof(CalendarLock.LockDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            foreach (var itemDate in tDate)
            {
                DateTime ChkDate = new DateTime();
                var bDate = DateTime.TryParseExact(itemDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out ChkDate);
                if (!bDate)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                    string desc = CalendarLock.GetType().GetProperty(nameof(CalendarLock.LockDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[message]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task<string> AddUpdateCalendarLockAsync(List<CalendarLockReq> input)
        {
            CalendarLockQueryResult model = null;
            if (input.Count() > 0)
            {
                var listDate = new List<DateTime>();
                var listGuid = new List<Guid>();
                string tGuid = null;

                var chkdate = input.Select(o => o.CalendarLock).ToList();
                tGuid = input.Select(x => x.Guid).FirstOrDefault();
                int ChkGuid = 0;

                if (!string.IsNullOrEmpty(tGuid))
                {
                    var xxx = tGuid.Split(',');
                    foreach (var item in xxx)
                    {
                        var newGuid = Guid.NewGuid();
                        var chkGuid = Guid.TryParse(item, out newGuid);
                        if (!chkGuid)
                        {
                            ChkGuid = 1;
                        }
                        else
                        {
                            listGuid.Add(newGuid);
                        }
                    }
                }

                await ValidateAsync(DB, chkdate, ChkGuid);

                foreach (var itemDate in input)
                {
                    tGuid = itemDate.Guid;
                    listDate.Add(DateTime.ParseExact(itemDate.CalendarLock, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US")));
                }



                IQueryable<CalendarLockQueryResult> query = DB.CalendarLocks
                                 .Select(o => new CalendarLockQueryResult
                                 {
                                     CalendarLock = o
                                 });

                query = query.Where(p => listGuid.Any(p2 => p2 == p.CalendarLock.CompanyID));
                query = query.Where(p => listDate.Any(p2 => p2 == p.CalendarLock.LockDate));

                var Data = query.ToList() ?? new List<CalendarLockQueryResult>();
                foreach (var loopGuid in listGuid)
                {
                    var DataChk = (Data.Where(x => x.CalendarLock.CompanyID == loopGuid).ToList() ?? null);
                    foreach (var loopDate in input)
                    {

                        if (DataChk.Where(x => x.CalendarLock.LockDate == DateTime.ParseExact(loopDate.CalendarLock, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))).FirstOrDefault() != null)
                        {

                            model = DataChk.Where(x => x.CalendarLock.LockDate == DateTime.ParseExact(loopDate.CalendarLock, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))).FirstOrDefault();

                            var result = CalendarLockDTO.CreateFromModel(model.CalendarLock);

                            if (loopDate.status == 2)
                            {
                                loopDate.status = 1;
                                result.IsLocked = Convert.ToBoolean(loopDate.status);
                            }
                            else if (loopDate.status == 0)
                            {
                                result.IsLocked = Convert.ToBoolean(loopDate.status);
                            }

                            result.IsLocked = Convert.ToBoolean(loopDate.status);
                            result.ToModel(ref model);


                            DB.Entry(model.CalendarLock).State = EntityState.Modified;

                        }
                        else
                        {
                            model = new CalendarLockQueryResult();
                            model.CalendarLock = new CalendarLock();
                            var ChkAddData = input.Where(x => DateTime.ParseExact(x.CalendarLock, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US")) == DateTime.ParseExact(loopDate.CalendarLock, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))).Select(o => new CalendarLock
                            {
                                LockDate = DateTime.ParseExact(o.CalendarLock, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US")),
                                IsLocked = Convert.ToBoolean(o.status),
                                CompanyID = loopGuid
                            }).FirstOrDefault();

                            var addData = CalendarLockDTO.CreateFromModel(ChkAddData);

                            addData.ToModel(ref model);
                            await DB.CalendarLocks.AddAsync(model.CalendarLock);
                        }
                    }
                    await DB.SaveChangesAsync();
                }
                return GetCalendarAddUpdate(listDate, listGuid, tGuid);
            }
            else
            {
                var CalendarLock = new CalendarLockDTO();
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = CalendarLock.GetType().GetProperty(nameof(CalendarLock.Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }

        public string GetCalendarAddUpdate(List<DateTime> inDate, List<Guid> id, string tGuid)
        {
            if (id != null)
            {
                IQueryable<CalendarLockQueryResult> query = DB.CalendarLocks
                             .Select(o => new CalendarLockQueryResult
                             {
                                 CalendarLock = o
                             });
                query = query.Where(p => id.Any(p2 => p2 == p.CalendarLock.CompanyID));
                query = query.Where(p => inDate.Any(p2 => p2 == p.CalendarLock.LockDate));

                var queryList = query
                        .GroupBy(t => t.CalendarLock.LockDate)
                        .Select(t => new { LockDate = t.Key, Value = t.Sum(u => (u.CalendarLock.IsLocked ? 1 : 0)) }).OrderBy(t => t.LockDate).ToList();

                string tModel = "{";
                string tStatus = null;
                foreach (var item in queryList)
                {
                    tStatus = "0";
                    if (item.Value == id.Count())
                    {
                        tStatus = "2";
                    }
                    else if (item.Value > 0)
                    {
                        tStatus = "1";
                    }

                    tModel = tModel + "\"" + item.LockDate.ToString("dd/MM/yyyy") + "\": { \"Guid\": \"" + tGuid + "\",\"CalendarLock\":\"" + item.LockDate.ToString("dd/MM/yyyy") + "\",\"status\": " + tStatus + " },\n";
                }
                tModel = tModel.Remove(tModel.Length - 2);
                tModel = tModel + "}";
                return tModel;
            }
            else
            {
                var CalendarLock = new CalendarLockDTO();
                ValidateException ex = new ValidateException();
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = CalendarLock.GetType().GetProperty(nameof(CalendarLock.Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Result.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Result.Key, msg, (int)errMsg.Result.Type);
                throw ex;
     
            }
        }


        public async Task<string> GetCalendarLockListAsync(CalendarLockFilter filter)
        {
            if (filter.Companies != null)
            {
                var listGuid = new List<Guid>();
                var xxx = filter.Companies.Split(',');

                IQueryable<CalendarLockQueryResult> query = DB.CalendarLocks
                          .Select(o => new CalendarLockQueryResult
                          {
                              CalendarLock = o //?? new CalendarLock()
                              ,
                              Company = o.Company //?? new Database.Models.MST.Company()

                          });


                if (!string.IsNullOrEmpty(filter.Companies))
                {
                    foreach (var item in xxx)
                    {
                        var newGuid = Guid.NewGuid();
                        var chkGuid = Guid.TryParse(item, out newGuid);

                        if (!chkGuid)
                        {
                            // Return ""l
                            break;
                        }

                        listGuid.Add(newGuid);
                    }
                    query = query.Where(p => listGuid.Any(p2 => p2 == p.CalendarLock.CompanyID));

                }
                DateTime sDate = DateTime.ParseExact("01/" + filter.Month + "/" + filter.Year, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));
                DateTime lDate = new DateTime(sDate.Year, sDate.Month, DateTime.DaysInMonth(sDate.Year, sDate.Month));

                lDate = lDate.AddDays(+7);
                sDate = sDate.AddDays(-7);
                if (!string.IsNullOrEmpty(filter.Year))
                {
                    if (!string.IsNullOrEmpty(filter.Month))
                    {
                        query = query.Where(x => x.CalendarLock.LockDate >= sDate && x.CalendarLock.LockDate <= lDate);
                    }
                }

                string tModel = "{";
                for (DateTime x = sDate; x <= lDate;)
                {
                    CalendarLockReq Model = new CalendarLockReq();
                    Model.Date = x.ToString("dd/MM/yyyy");
                    Model.Guid = filter.Companies;
                    Model.CalendarLock = sDate.ToString("dd/MM/yyyy");
                    Model.status = 0;

                    if (query == null || query.Count() > 0)
                    {
                        var queryList = query
                         .GroupBy(t => t.CalendarLock.LockDate)
                         .Select(t => new { LockDate = t.Key, Value = t.Sum(u => (u.CalendarLock.IsLocked ? 1 : 0)) }).ToList();

                        foreach (var item in queryList)
                        {
                            if (x == item.LockDate)
                            {
                                if (item.Value == listGuid.Count())
                                {
                                    Model.status = 2;
                                }
                                else if (item.Value > 0)
                                {
                                    Model.status = 1;
                                }
                            }
                        }
                    }

                    x = x.AddDays(+1);

                    tModel = tModel + "\"" + Model.Date + "\": { \"Guid\": \"" + Model.Guid + "\",\"CalendarLock\":\"" + Model.Date + "\",\"status\": " + Model.status.ToString() + " },\n";
                }
                tModel = tModel.Remove(tModel.Length - 2);
                tModel = tModel + "}";
                return tModel;
            }
            else
            {
                var CalendarLock = new CalendarLockDTO();
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = CalendarLock.GetType().GetProperty(nameof(CalendarLock.Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }

        public async Task<List<CalendarLockHistoryDTO>> GetCalendarLockHistoryAsync(CalendarLockReq input)
        {
            /////////////////////////////////////////////
            if (input.Guid != null)
            {
                var listGuid = new List<Guid>();
                var xxx = input.Guid.Split(',');
                IQueryable<CalendarLockQueryResult> query = DB.CalendarLocks

                              .Select(o => new CalendarLockQueryResult
                              {
                                  CalendarLock = o,
                                  Company = o.Company,
                                  UpdatedBy = o.UpdatedBy
                              });

                if (!string.IsNullOrEmpty(input.Guid))
                {
                    foreach (var item in xxx)
                    {
                        var newGuid = Guid.NewGuid();
                        var chkGuid = Guid.TryParse(item, out newGuid);

                        if (!chkGuid)
                        {
                            // Return ""l
                            break;
                        }

                        listGuid.Add(newGuid);
                    }
                    query = query.Where(p => listGuid.Any(p2 => p2 == p.CalendarLock.CompanyID));

                }
                DateTime sDate = DateTime.ParseExact(input.CalendarLock, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));
                query = query.Where(x => x.CalendarLock.LockDate == sDate);
                query = query.Where(x => x.CalendarLock.IsLocked == true);
                query = query.OrderByDescending(x => x.CalendarLock.Updated);
                var queryResults = query.ToList();

                var results = queryResults.Select(o => CalendarLockHistoryDTO.CreateFromModel(o)).ToList();

                return results;
            }
            else
            {
                return null;
            }
        }
    }
}
