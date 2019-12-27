
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_001H
    @ContractNumber  nvarchar(50)	
AS
BEGIN
	SELECT '' as ContractNumber, '' as UnitNumberHeader, '' as LandOfficeID, '' as LandPortionNumber, '' as LandNumber, '' as LandSurveyArea, '' as LandSubDistrict,
	'' as LandDistrict, '' as LandProvince, '' as TitledeedNumber, '' as LandBook, '' as LandBookPage, '' as AreaTitledeed, '' as TransferDate, '' as CompanyName,
	'' as TaxID, '' as AttornyName1, '' as AddressThai, '' as BuildingThai, '' as SoiThai, '' as RoadThai, '' as SubDistrictThai, '' as DistrictThai, '' as ProvinceThai,
	'' as PostCode, '' as Telephone, '' as CusCurrentAddress, '' as CusMoo, '' as CusVillage, '' as CusSoi, '' as CusRoad, '' as CusSubDistrict, '' as CusDistrict, '' as CusProvince,
	'' as CusPhone, '' as NetSalePrice, '' as PriceText, '' as TypeHome, '' as ModelHomeThai, '' as AddressNumber, '' as Address1, '' as Address2, '' as AreaSQM,
	'' as HomeNoReceivedDate, '' as EstimatePrice, '' as Age, '' as Parent, '' as Nationality, '' as Spouse, '' as NationalitySpouse, '' as FactorArea, '' as FactorPrice,
	'' as CurvedsteelArea, '' as CurvedstellPrice from ReportTemplate
END
GO

exec AP2SP_PF_TR_001H