﻿using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class BGDTO : BaseDTO
    {
        /// <summary>
        /// รหัส BG
        /// </summary>
        [Description("รหัส BG")]
        public string BGNo { get; set; }
        /// <summary>
        /// ชื่อ BG
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// สำหรับประเภทของโครงการ (แนวสูง แนวราบ)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ProductType
        /// </summary>
        [Description("สำหรับประเภทของโครงการ (แนวสูง แนวราบ)")]
        public MST.MasterCenterDropdownDTO ProductType { get; set; }

        public static BGDTO CreateFromModel(BG model)
        {
            if (model != null)
            {
                var result = new BGDTO()
                {
                    Id = model.ID,
                    BGNo = model.BGNo,
                    Name = model.Name,
                    ProductType = MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static BGDTO CreateFromQueryResult(BGQueryResult model)
        {
            if (model != null)
            {
                var result = new BGDTO()
                {
                    Id = model.BG.ID,
                    BGNo = model.BG.BGNo,
                    Name = model.BG.Name,
                    ProductType = MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    Updated = model.BG.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BGSortByParam sortByParam, ref IQueryable<BGQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BGSortBy.BGNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BG.BGNo);
                        else query = query.OrderByDescending(o => o.BG.BGNo);
                        break;
                    case BGSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BG.Name);
                        else query = query.OrderByDescending(o => o.BG.Name);
                        break;
                    case BGSortBy.ProductType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ProductType.Name);
                        else query = query.OrderByDescending(o => o.ProductType.Name);
                        break;
                    case BGSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BG.Updated);
                        else query = query.OrderByDescending(o => o.BG.Updated);
                        break;
                    case BGSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.BG.Name);
                        break;

                }
            }
            else
            {
                query = query.OrderBy(o => o.BG.Name);
            }
        }
        public void ToModel(ref BG model)
        {
            model.BGNo = this.BGNo;
            model.Name = this.Name;
            model.ProductTypeMasterCenterID = this.ProductType?.Id;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.BGNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BGDTO.BGNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.BGNo.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BGDTO.BGNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            var checkUniqueBGNo = this.Id != (Guid?)null ? await db.BGs.Where(o => o.BGNo == this.BGNo && o.ID != this.Id).CountAsync() > 0 : await db.BGs.Where(o => o.BGNo == this.BGNo).CountAsync() > 0;
            if (checkUniqueBGNo)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BGDTO.BGNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.BGNo);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (string.IsNullOrEmpty(this.Name))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BGDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.Name.CheckLang(false, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BGDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
           
            if (this.ProductType==null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BGDTO.ProductType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
    public class BGQueryResult
    {
        public BG BG { get; set; }
        public MasterCenter ProductType { get; set; }
        public User UpdatedBy { get; set; }
    }
}
