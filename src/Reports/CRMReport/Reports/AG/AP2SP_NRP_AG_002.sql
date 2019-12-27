
CREATE OR ALTER PROCEDURE AP2SP_NRP_AG_002
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateStart2 Datetime,
	@ProjectType int,
	@UserName nvarchar(150)
AS
BEGIN
	
	SELECT '' as Project, '' as ProductID, '' as CompanyNameThai, '' as CompanyID, '' as ProductType, '' as TotalUnit, '' as UnitSale, '' as UnitContract, '' as No_Pay,
	'' as Pay_InMonth, '' as Total, '' as ContractNumber_1, '' as Money_Unfinish_1, '' as ContractNumber_2, '' as Money_Unfinish_2, '' as ContractNumber456, '' as Money_Unifinish456,
	'' as Contractnumber_6UP, '' as Money_Unfinish_6UP, '' as TotalContractNumber, '' as Total_Money_Unfinish, '' as DateStart1, '' as DateStart2, '' as DateStart3 from ReportTemplate
END
GO

exec AP2SP_NRP_AG_002