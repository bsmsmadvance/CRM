
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_010Eng
    @ContractNumber  nvarchar(20)
AS
BEGIN
	SELECT '' as CompanyID, '' as AuthoritarianByCompany, '' as CompanyName, '' as AddressThai, '' as BuildingThai, '' as SoiThai, '' as RoadThai, '' as SubDistrictThai,
    '' as DistrictThai, '' as ProvinceThai, '' as CustomerName, '' as Age, '' as CusCurrentAddress, '' as CusMoo, '' as CusVillage, '' as CusSoi, '' as CusRoad, '' as CusSubDistrict,
	'' as CusDistrict, '' as CusProvince, '' as ContractNumber, '' as ContractDate, '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as Condition, '' as PromotionTransferDate,
	'' as PromotionDescription, '' as Amount, '' as ProductType, '' as witness1, '' as witness2, '' as Flag, '' as PromotionID, '' as Expr1034 from ReportTemplate
END
GO

exec AP2SP_PF_AG_010Eng