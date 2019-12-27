
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_010_TH_EN
    @ContractNumber  nvarchar(20)
AS
BEGIN
	SELECT '' as CompanyID, '' as AuthoritarianByCompany, '' as AuthoritariamByCompanyEng, '' as CompanyName, '' as CompanyNameEng, '' as AddressThai, '' as BuildingThai, '' as SoiThai, '' as RoadThai, '' as SubDistrictThai,
    '' as DistrictThai, '' as ProvinceThai, '' as CustomerName, '' as CustomerNameEng, '' as Age, '' as CusCurrentAddress, '' as CusMoo, '' as CusVillage, '' as CusSoi, '' as CusRoad, '' as CusSubDistrict,
	'' as CusDistrict, '' as CusProvince, '' as CusCurrentAddressEng, '' as CusMooEng, '' as CusVillageEng, '' as CusSoiEng, '' as CusRoadEng, '' as CusSubDistrictEng, '' as CusDistrictEng,
	'' as CusProvinceEng , '' as ContractNumber, '' as ContractDate, '' as ProductID, '' as ProjectName, '' as ProjectNameEng, '' as UnitNumber, '' as Condition, '' as ConditionEng,
	'' as PromotionTransferDate, '' as PromotionDescription, '' as PromotionDescriptionEng, '' as Amount, '' as ProductType, '' as ProductTypeEng, '' as witness1, '' as witness2, 
	'' as witness1Eng, '' as witness2Eng,'' as Flag, '' as PromotionID, '' as Customer_Nationality	 from ReportTemplate
END
GO

exec AP2SP_PF_AG_010_TH_EN