﻿using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitMeterDTO : BaseDTO
    {
        /// <summary>
        /// เลขมิเตอร์ไฟฟ้า
        /// </summary>
        public string ElectricMeter { get; set; }
        /// <summary>
        /// เลขมิเตอร์น้ำ
        /// </summary>
        public string WaterMeter { get; set; }
        /// <summary>
        /// ราคามิเตอร์ไฟฟ้า
        /// Project/api/UnitMeters/{Id}/ElectricMeterPriceDropdownList
        /// </summary>
        public ElectricMeterPriceDropdownDTO ElectricMeterPrice { get; set; }
        /// <summary>
        /// ราคามิเตอร์น้ำ
        /// Project/api/UnitMeters/{Id}/WaterMeterPriceDropdownList
        /// </summary>
        public WaterMeterPriceDropdownDTO WaterMeterPrice { get; set; }
        /// <summary>
        /// สถานะโอน (ไฟฟ้า)
        /// </summary>
        public bool? IsTransferElectricMeter { get; set; }
        /// <summary>
        /// สถานะโอน (น้ำ)
        /// </summary>
        public bool? IsTransferWaterMeter { get; set; }
        /// <summary>
        /// วันที่โอน (ไฟฟ้า)
        /// </summary>
        public DateTime? ElectricMeterTransferDate { get; set; }
        /// <summary>
        /// วันที่โอน (น้ำ)
        /// </summary>
        public DateTime? WaterMeterTransferDate { get; set; }
        /// <summary>
        /// หัวข้อ (ไฟฟ้า)
        /// Master/api/MasterCenters?masterCenterGroupKey=MeterTopic
        /// </summary>
        public MST.MasterCenterDropdownDTO ElectricMeterTopic { get; set; }
        /// <summary>
        /// หัวข้อ (น้ำ)
        /// Master/api/MasterCenters?masterCenterGroupKey=MeterTopic
        /// </summary>
        public MST.MasterCenterDropdownDTO WaterMeterTopic { get; set; }
        /// <summary>
        /// หมายเหตุ (ไฟฟ้า)
        /// </summary>
        public string ElectricMeterRemark { get; set; }
        /// <summary>
        /// หมายเหตุ (น้ำ)
        /// </summary>
        public string WaterMeterRemark { get; set; }
        public static UnitMeterDTO CreateFromModel(Unit model)
        {
            if (model != null)
            {
                var result = new UnitMeterDTO()
                {
                    Id = model.ID,
                    ElectricMeter = model.ElectricMeter,
                    WaterMeter = model.WaterMeter,
                    ElectricMeterPrice = ElectricMeterPriceDropdownDTO.CreateFromModel(model.ElectricMeterPrice),
                    WaterMeterPrice = WaterMeterPriceDropdownDTO.CreateFromModel(model.WaterMeterPrice),
                    IsTransferElectricMeter = model.IsTransferElectricMeter,
                    IsTransferWaterMeter = model.IsTransferWaterMeter,
                    ElectricMeterTransferDate = model.ElectricMeterTransferDate,
                    WaterMeterTransferDate = model.WaterMeterTransferDate,
                    ElectricMeterTopic = MST.MasterCenterDropdownDTO.CreateFromModel(model.ElectricMeterTopic),
                    WaterMeterTopic = MST.MasterCenterDropdownDTO.CreateFromModel(model.WaterMeterTopic),
                    ElectricMeterRemark = model.ElectricMeterRemark,
                    WaterMeterRemark = model.WaterMeterRemark,
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
        public void ToModel(ref Unit model)
        {
            if (model.ElectricMeter != this.ElectricMeter)
            {
                model.ElectrictMeterSaved = DateTime.Now;
            }
            if (model.WaterMeter != this.WaterMeter)
            {
                model.WaterMeterSaved = DateTime.Now;
            }
            if (model.ElectricMeterTransferDate != this.ElectricMeterTransferDate)
            {
                model.ElectrictMeterTransferDateSaved = DateTime.Now;
            }
            if (model.WaterMeterTransferDate != this.WaterMeterTransferDate)
            {
                model.WaterMeterTransferDateSaved = DateTime.Now;
            }
            model.ElectricMeter = this.ElectricMeter;
            model.WaterMeter = this.WaterMeter;
            model.ElectricMeterPriceID = this.ElectricMeterPrice?.Id;
            model.WaterMeterPriceID = this.WaterMeterPrice?.Id;
            model.IsTransferElectricMeter = this.IsTransferElectricMeter;
            model.IsTransferWaterMeter = this.IsTransferWaterMeter;
            model.ElectricMeterTransferDate = this.ElectricMeterTransferDate;
            model.WaterMeterTransferDate = this.WaterMeterTransferDate;
            model.ElectricMeterTopicMasterCenterID = this.ElectricMeterTopic?.Id;
            model.WaterMeterTopicMasterCenterID = this.WaterMeterTopic?.Id;
            model.ElectricMeterRemark = this.ElectricMeterRemark;
            model.WaterMeterRemark = this.WaterMeterRemark;
        }
    }
}