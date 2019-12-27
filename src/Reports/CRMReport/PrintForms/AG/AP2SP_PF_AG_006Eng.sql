
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_006Eng

	@HistoryID nvarchar(40)
	,@ContractNumber nvarchar(20)
AS
BEGIN
	SELECT '' as Header, '' as CompanyID, '' as ProductID, '' as CompanyName, '' as CompanyAddress, '' as AttornyName1, '' as AddressThai, '' as CustomerName, '' as CusCurrentAddress, '' as CusMoo, '' as CusVillage, '' as CusSoi, '' as CusRoad,
	'' as CusSubDistrict, '' as CusDistrict, '' as CusProvince, '' as ProductType, '' as ContractNumber, '' as ContractDate, '' as ProjectName, '' as UnitNumber, '' as ApproveDate, '' as TotalPaidAmount, '' as TotalPaidText, '' as Paid,
	'' as PaidText, '' as SuspenseAmount, '' as SuspenseAmountText from ReportTemplate
END
GO

exec AP2SP_PF_AG_006Eng