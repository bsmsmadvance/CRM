﻿using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class ModelListDTO : BaseDTO
    {
        /// <summary>
        ///  รหัสแบบบ้าน
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (EN)
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        ///  ชื่อย่อ
        ///  Master/api/MasterCenters?masterCenterGroupKey=ModelShortName
        /// </summary>
        public MST.MasterCenterDropdownDTO ModelShortName { get; set; }
        /// <summary>
        ///  ลักษณะแบบบ้าน
        ///  Master/api/MasterCenters?masterCenterGroupKey=ModelUnitType
        /// </summary>
        public MST.MasterCenterDropdownDTO ModelUnitType { get; set; }
        /// <summary>
        ///  ประเภทแบบบ้าน
        ///  Master/api/TypeOfRealEstates//DropdownList
        /// </summary>
        public MST.TypeOfRealEstateDropdownDTO TypeOfRealEstate { get; set; }
        /// <summary>
        ///  ModelType
        ///  Master/api/MasterCenters?masterCenterGroupKey=ModelType
        /// </summary>
        public MST.MasterCenterDropdownDTO ModelType { get; set; }
        /// <summary>
        ///  หน้ากว้าง
        /// </summary>
        public double? FrontWidth { get; set; }
        /// <summary>
        ///  อัตราชำระคืน
        /// </summary>
        public double? PreferUnit { get; set; }
        /// <summary>
        ///  อัตราชำระคืนต่อพื้นที่
        /// </summary>
        public double? PreferUnitMinimum { get; set; }
        /// <summary>
        ///  จำนวนชำระคืนต่อหน่วย
        /// </summary>
        public double? PreferHouse { get; set; }
        /// <summary>
        /// WaterElectricMeterPrices
        /// </summary>
        public WaterElectricMeterPriceDTO WaterElectricMeterPrices { get; set; }
        public static ModelListDTO CreateFromQueryResult(ModelQueryResult model)
        {
            if (model != null)
            {
                var result = new ModelListDTO()
                {
                    Id = model.Model.ID,
                    Code = model.Model.Code,
                    NameTH = model.Model.NameTH,
                    NameEN = model.Model.NameEN,
                    ModelShortName = MST.MasterCenterDropdownDTO.CreateFromModel(model.ModelShortName),
                    ModelUnitType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ModelUnitType),
                    ModelType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ModelType),
                    TypeOfRealEstate = MST.TypeOfRealEstateDropdownDTO.CreateFromModel(model.TypeOfRealEstate),
                    WaterElectricMeterPrices = WaterElectricMeterPriceDTO.CreateFromModel(model.WaterElectricMeterPrice),
                    FrontWidth = model.Model.FrontWidth,
                    PreferUnit = model.Model.PreferUnit,
                    PreferUnitMinimum = model.Model.PreferUnitMinimum,
                    PreferHouse = model.Model.PreferHouse,
                    Updated = model.Model.Updated,
                    UpdatedBy = model.Model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(ModelListSortByParam sortByParam, ref IQueryable<ModelQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ModelListSortBy.Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.Code);
                        else query = query.OrderByDescending(o => o.Model.Code);
                        break;
                    case ModelListSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.NameTH);
                        else query = query.OrderByDescending(o => o.Model.NameTH);
                        break;
                    case ModelListSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.NameEN);
                        else query = query.OrderByDescending(o => o.Model.NameEN);
                        break;
                    case ModelListSortBy.ModelShortName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ModelShortName.Name);
                        else query = query.OrderByDescending(o => o.ModelShortName.Name);
                        break;
                    case ModelListSortBy.ModelUnitType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ModelUnitType.Name);
                        else query = query.OrderByDescending(o => o.ModelUnitType.Name);
                        break;
                    case ModelListSortBy.TypeOfRealEstate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TypeOfRealEstate.Name);
                        else query = query.OrderByDescending(o => o.TypeOfRealEstate.Name);
                        break;
                    case ModelListSortBy.ModelType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ModelType.Name);
                        else query = query.OrderByDescending(o => o.ModelType.Name);
                        break;
                    case ModelListSortBy.WaterElectricMeterPrice_WaterMeterPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.WaterMeterPrice);
                        else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.WaterMeterPrice);
                        break;
                    case ModelListSortBy.WaterElectricMeterPrice_ElectricMeterPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.ElectricMeterPrice);
                        else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.ElectricMeterPrice);
                        break;
                    case ModelListSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.Updated);
                        else query = query.OrderByDescending(o => o.Model.Updated);
                        break;
                    case ModelListSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Model.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Model.NameTH);
            }
        }
    }
    public class ModelQueryResult
    {
        public MasterCenter ModelShortName { get; set; }
        public MasterCenter ModelUnitType { get; set; }
        public MasterCenter ModelType { get; set; }
        public Model Model { get; set; }
        public TypeOfRealEstate TypeOfRealEstate { get; set; }
        public WaterElectricMeterPrice WaterElectricMeterPrice { get; set; }
        public User UpdatedBy { get; set; }
    }
}
