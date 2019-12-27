
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_005H
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as ContractNumber, '' as UnitNumberHeader, '' as TitledeedNumber, '' as LandNumber, '' as LandSurveyArea, '' as LandSubDistrict, '' as LandDistrict,
	'' as LandProvince, '' as TransferDate, '' as LandOfficeID, '' as LandOfficeName, '' as CompanyName, '' as TaxID, '' as AttornyName1, '' as AddressThai, '' as BuildingThai,
	'' as SoiThai, '' as RoadThai, '' as SubDistricthai, '' as DistrictThai, '' as ProvinceThai, '' as PostCode, '' as Telephone, '' as CusCurrentAddress,
	'' as CusMoo, '' as CusVillage, '' as CusSoi, '' as CusRoad, '' as CusSubDistrict, '' as CusDistrict, '' as CusProvince, '' as CusPhone, '' as TotalSellingPrice,
	'' as PriceText, '' as ModelHomeThai, '' as AddressNumber, '' as Address1, '' as Address2, '' as AreaFromPFB, '' as HomeNoReceivedDate, '' as TypeHome, '' as Parent,
	'' as Nationality, '' as FactorArea, '' as FactorPrice, '' as CurvedSteelArea, '' as CurvedSteelPrice from ReportTemplate
END
GO

exec AP2SP_PF_TR_005H