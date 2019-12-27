
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_005CD
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as ContractNumber, '' as ProductID, '' as TitledeedNumber, '' as LandSubDistrict, '' as LandDistrict, '' as LandProvince, '' as UnitNumber, '' as UnitNumberHeader,
	'' as FloorID, '' as TowerID, '' as TowerName, '' as ProjectName, '' as TowerTitledeedNumber, '' as AreaTitledeed, '' as ContractDate, '' as TransferDate, '' as LandOfficeID,
	'' as LandOfficeName, '' as CompanyName, '' as TaxID, '' as AttornyName1, '' as AddressThai, '' as BuildingThai, '' as SoiThai, '' as RoadThai, '' as SubDistrictThai,
	'' as DistrictThai, '' as ProvinceThai, '' as PostCode, '' as Telephone, '' as CusCurrentAddress, '' as CusMo, '' as CusVillage, '' as CusSoi, '' as CusRoad, '' as CusSubDistrict,
	'' as CusDistrict, '' as CusProvince, '' as CusPhone, '' as TotalSellingPrice, '' as PriceText, '' as Parent, '' as Nationality from ReportTemplate
END
GO

exec AP2SP_PF_TR_005CD