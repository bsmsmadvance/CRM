using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class TitleDeedDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่โฉนด
        /// </summary>
        [Description("เลขที่โฉนด")]
        public string TitledeedNo { get; set; }
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        [Description("เลขที่แปลง")]
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        /// </summary>
        public double? TitledeedArea { get; set; }
        /// <summary>
        /// ที่ตั้งโฉนด
        /// Project/api/Projects/{projectID}/Addresses/DropdownList
        /// </summary>
        public ProjectAddressDTO Address { get; set; }
        /// <summary>
        /// สำนักงานที่ดิน
        /// Masterdata/api/LandOffices/DropdownList
        /// </summary>
        public MST.LandOfficeListDTO LandOffice { get; set; }
        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        [Description("บ้านเลขที่")]
        public string HouseNo { get; set; }
        /// <summary>
        /// ปีที่ได้บ้านเลขที่
        /// </summary>
        public int? HouseNoReceivedYear { get; set; }
        /// <summary>
        /// พื้นที่ใช้สอย
        /// </summary>
        public double? UsedArea { get; set; }
        /// <summary>
        /// พื้นที่จอดรถ
        /// </summary>
        public double? ParkingArea { get; set; }
        /// <summary>
        /// พื้นที่รั้วคอนกรีด
        /// </summary>
        public double? FenceArea { get; set; }
        /// <summary>
        /// พื้นที่รั้วเหล็กดัด
        /// </summary>
        public double? FenceIronArea { get; set; }
        /// <summary>
        /// พื้นที่ระเบียง
        /// </summary>
        public double? BalconyArea { get; set; }
        /// <summary>
        /// พื้นที่วางแอร์
        /// </summary>
        public double? AirArea { get; set; }
        /// <summary>
        /// เล่ม
        /// </summary>
        [Description("เล่ม")]
        public string BookNo { get; set; }
        /// <summary>
        /// หน้า
        /// </summary>
        [Description("หน้า")]
        public string PageNo { get; set; }
        /// <summary>
        /// ราคาประเมิณ
        /// </summary>
        public decimal? EstimatePrice { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// เหมือนที่อยู่โฉนด
        /// </summary>
        public bool? IsSameAddressAsTitledeed { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์ที่อยู่ทะเบียนบ้าน
        /// </summary>
        [Description("รหัสไปรษณีย์ที่อยู่ทะเบียนบ้าน")]
        public string HousePostalCode { get; set; }
        /// <summary>
        /// จังหวัดที่อยู่ทะเบียนบ้าน
        /// masterdata/api/Provinces/DropdownList
        /// </summary>
        public MST.ProvinceListDTO HouseProvince { get; set; }
        /// <summary>
        /// อำเภอที่อยู่ทะเบียนบ้าน
        /// masterdata/api/Districts/DropdownList
        /// </summary>
        public MST.DistrictListDTO HouseDistrict { get; set; }
        /// <summary>
        /// ตำบลที่อยู่ทะเบียนบ้าน
        /// masterdata/api/SubDistrict/DropdownList
        /// </summary>
        public MST.SubDistrictListDTO HouseSubDistrict { get; set; }
        /// <summary>
        /// หมู่ที่
        /// </summary>
        public string HouseMoo { get; set; }
        /// <summary>
        /// ซอย (TH)
        /// </summary>
        public string HouseSoiTH { get; set; }
        /// <summary>
        /// ซอย (EN)
        /// </summary>
        public string HouseSoiEN { get; set; }
        /// <summary>
        /// ถนน (TH)
        /// </summary>
        public string HouseRoadTH { get; set; }
        /// <summary>
        /// ถนน (EN)
        /// </summary>
        public string HouseRoadEN { get; set; }
        /// <summary>
        /// สถานะโฉนด
        /// Master/api/MasterCenters?masterCenterGroupKey=LandStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO LandStatus { get; set; }
        /// <summary>
        /// วันที่เปลี่ยนสถานะโฉนด
        /// </summary>
        public DateTime? LandStatusDate { get; set; }
        /// <summary>
        /// หมายเหตุสถานะโฉนด
        /// </summary>
        public string LandStatusNote { get; set; }
        /// <summary>
        /// เลขที่ดิน
        /// </summary>
        public string LandNo { get; set; }
        /// <summary>
        /// สถานะขอปลอด
        /// Master/api/MasterCenters?masterCenterGroupKey=PreferStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO PreferStatus { get; set; }

        public static TitleDeedDTO CreateFromModel(TitledeedDetail model)
        {
            if (model != null)
            {
                var result = new TitleDeedDTO();

                result.Id = model.ID;
                result.TitledeedNo = model.TitledeedNo;
                result.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
                result.TitledeedArea = model.TitledeedArea;
                result.Address = ProjectAddressDTO.CreateFromModel(model.Address);
                result.LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.Unit?.LandOffice);
                result.HouseNo = model.Unit?.HouseNo;
                result.HouseNoReceivedYear = model.Unit?.HouseNoReceivedYear;
                result.UsedArea = model.Unit?.UsedArea;
                result.ParkingArea = model.Unit?.ParkingArea;
                result.FenceArea = model.Unit?.FenceArea;
                result.FenceIronArea = model.Unit?.FenceIronArea;
                result.BalconyArea = model.Unit?.BalconyArea;
                result.AirArea = model.Unit?.AirArea;
                result.BookNo = model.BookNo;
                result.PageNo = model.PageNo;
                result.EstimatePrice = model.EstimatePrice;
                result.Remark = model.Remark;
                result.IsSameAddressAsTitledeed = model.Unit?.IsSameAddressAsTitledeed;
                result.HousePostalCode = model.Unit?.HousePostalCode;
                result.HouseProvince = MST.ProvinceListDTO.CreateFromModel(model.Unit?.HouseProvince);
                result.HouseDistrict = MST.DistrictListDTO.CreateFromModel(model.Unit?.HouseDistrict);
                result.HouseSubDistrict = MST.SubDistrictListDTO.CreateFromModel(model.Unit?.HouseSubDistrict);
                result.HouseMoo = model.Unit?.HouseMoo;
                result.HouseSoiTH = model.Unit?.HouseSoiTH;
                result.HouseSoiEN = model.Unit?.HouseSoiEN;
                result.HouseRoadTH = model.Unit?.HouseRoadTH;
                result.HouseRoadEN = model.Unit?.HouseRoadEN;
                result.LandStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LandStatus);
                result.LandStatusDate = model.LandStatusDate;
                result.LandStatusNote = model.LandStatusNote;
                result.LandNo = model.LandNo;
                result.PreferStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PreferStatus);
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static TitleDeedDTO CreateFromHistoryModel(TitledeedDetailHistory model)
        {
            if (model != null)
            {
                var result = new TitleDeedDTO();

                result.Id = model.ID;
                result.TitledeedNo = model.TitledeedNo;
                result.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
                result.TitledeedArea = model.TitledeedArea;
                result.Address = ProjectAddressDTO.CreateFromModel(model.Address);
                result.LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.Unit.LandOffice);
                result.HouseNo = model.Unit.HouseNo;
                result.HouseNoReceivedYear = model.Unit.HouseNoReceivedYear;
                result.UsedArea = model.Unit.UsedArea;
                result.ParkingArea = model.Unit.ParkingArea;
                result.FenceArea = model.Unit.FenceArea;
                result.FenceIronArea = model.Unit.FenceIronArea;
                result.BalconyArea = model.Unit.BalconyArea;
                result.AirArea = model.Unit.AirArea;
                result.BookNo = model.BookNo;
                result.PageNo = model.PageNo;
                result.EstimatePrice = model.EstimatePrice;
                result.Remark = model.Remark;
                result.IsSameAddressAsTitledeed = model.Unit.IsSameAddressAsTitledeed;
                result.HousePostalCode = model.Unit.HousePostalCode;
                result.HouseProvince = MST.ProvinceListDTO.CreateFromModel(model.Unit.HouseProvince);
                result.HouseDistrict = MST.DistrictListDTO.CreateFromModel(model.Unit.HouseDistrict);
                result.HouseSubDistrict = MST.SubDistrictListDTO.CreateFromModel(model.Unit.HouseSubDistrict);
                result.HouseMoo = model.Unit.HouseMoo;
                result.HouseSoiTH = model.Unit.HouseSoiTH;
                result.HouseSoiEN = model.Unit.HouseSoiEN;
                result.HouseRoadTH = model.Unit.HouseRoadTH;
                result.HouseRoadEN = model.Unit.HouseRoadEN;
                result.LandStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LandStatusMasterCenter);
                result.LandStatusDate = model.LandStatusDate;
                result.LandStatusNote = model.LandStatusNote;
                result.LandNo = model.LandNo;
                result.PreferStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PreferStatus);
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(Guid projectID, DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!string.IsNullOrEmpty(this.TitledeedNo))
            {
                if (!this.TitledeedNo.IsOnlyNumberWithSpecialCharacter(true))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TitleDeedDTO.TitledeedNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueTitledeedNo = this.Id != (Guid?)null
                ? await db.TitledeedDetails.Where(o => o.ProjectID == projectID && o.ID != this.Id && o.TitledeedNo == this.TitledeedNo).CountAsync() > 0
                : await db.TitledeedDetails.Where(o => o.ProjectID == projectID && o.TitledeedNo == this.TitledeedNo).CountAsync() > 0;
                if (checkUniqueTitledeedNo)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TitleDeedDTO.TitledeedNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.TitledeedNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.HouseNo))
            {
                if (!this.HouseNo.IsOnlyNumberWithSpecialCharacter(true))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TitleDeedDTO.HouseNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.PageNo))
            {
                if (!this.PageNo.IsOnlyNumber())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TitleDeedDTO.PageNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.BookNo))
            {
                if (!this.BookNo.IsOnlyNumber())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TitleDeedDTO.BookNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.HousePostalCode))
            {
                if (!this.HousePostalCode.IsOnlyNumberWithMaxLength(5))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0024").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TitleDeedDTO.HousePostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.Unit != null)
            {
                var checkUniqueUnit = this.Id != (Guid?)null
                   ? await db.TitledeedDetails.Where(o => o.ProjectID == projectID && o.ID != this.Id && o.UnitID == this.Unit.Id).CountAsync() > 0
                   : await db.TitledeedDetails.Where(o => o.ProjectID == projectID && o.UnitID == this.Unit.Id).CountAsync() > 0;
                if (checkUniqueUnit)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TitleDeedDTO.Unit)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Unit.UnitNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }


        public void ToModel(ref TitledeedDetail model)
        {
            model.TitledeedNo = this.TitledeedNo;
            model.UnitID = this.Unit?.Id;
            model.TitledeedArea = this.TitledeedArea;
            model.AddressID = this.Address?.Id;
            model.Unit.LandOfficeID = this.LandOffice?.Id;
            model.Unit.HouseNo = this.HouseNo;
            model.Unit.HouseNoReceivedYear = this.HouseNoReceivedYear;
            model.Unit.UsedArea = this.UsedArea;
            model.Unit.ParkingArea = this.ParkingArea;
            model.Unit.FenceArea = this.FenceArea;
            model.Unit.FenceIronArea = this.FenceIronArea;
            model.Unit.BalconyArea = this.BalconyArea;
            model.Unit.AirArea = this.AirArea;
            model.BookNo = this.BookNo;
            model.PageNo = this.PageNo;
            model.EstimatePrice = this.EstimatePrice;
            model.Remark = this.Remark;
            model.Unit.IsSameAddressAsTitledeed = this.IsSameAddressAsTitledeed;
            model.Unit.HousePostalCode = this.HousePostalCode;
            model.Unit.HouseProvinceID = this.HouseProvince?.Id;
            model.Unit.HouseDistrictID = this.HouseDistrict?.Id;
            model.Unit.HouseSubDistrictID = this.HouseSubDistrict?.Id;
            model.Unit.HouseMoo = this.HouseMoo;
            model.Unit.HouseSoiTH = this.HouseSoiTH;
            model.Unit.HouseSoiEN = this.HouseSoiEN;
            model.Unit.HouseRoadTH = this.HouseRoadTH;
            model.Unit.HouseRoadEN = this.HouseRoadEN;
            model.LandStatusMasterCenterID = this.LandStatus?.Id;
            model.LandStatusDate = this.LandStatusDate;
            model.LandStatusNote = this.LandStatusNote;
            model.LandNo = this.LandNo;
            model.PreferStatusMasterCenterID = this.PreferStatus?.Id;
        }
    }
}
