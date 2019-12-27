
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_001CD
    @ContractNumber NVARCHAR(20)
AS
BEGIN
	SELECT '' as ContractNumber, '' as ProductID, '' as TitledeedNumber, '' as LandSubDistrict, '' as LandDistrict, '' as LandProvince, '' as UnitNumber, '' as UnitNumberHeader,
	'' as FloorID, '' as TowerID, '' as TowerName, '' as ProjectName, '' as TowerTitledeedNumber, '' as AreaTitledeed, '' as TransferDate, '' as CompanyName, '' as TaxID,
	'' as AttornyName, '' as AttornyIssueDate, '' as AddressThai, '' as BuildingThai, '' as SoiThai, '' as RoadThai, '' as SubDistrictThai, '' as DistrictThai, '' as ProvinceThai,
	'' as PostCode, '' as Telephone, '' as CusCurrentAddress, '' as CusMoo, '' as CusVillage, '' as CusSoi, '' as CusRoad, '' as CusSubDistrict, '' as CusDistrict, '' as CusProvince,
	'' as CusPhone, '' as NetSalePrice, '' as PriceText, '' as EstimatePrice, '' as Age, '' as Parent, '' as Nationality, '' as Spouse, '' as NationalitySpouse, '' as CityName from ReportTemplate
END
GO

exec AP2SP_PF_TR_001CD