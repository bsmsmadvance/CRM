
CREATE OR ALTER PROCEDURE ZCM_SP_Report_SummaryCommissionLCC
	@HomeType nvarchar(20),
	@DateStart datetime ,
	@DateEnd datetime,
	@UserName	nvarchar(150)
AS
BEGIN
	SELECT '' as BU, '' as HCSName, '' as UserID, '' as EmployeeID, '' as DisplayName, '' as RoleID, '' as RoleName, '' as LCCGuarantee, 
	'' as LCCCommissionSalePaid, '' as LCCCommissionTransPaid, '' as SumOther, '' as SumAdjust, '' as TotalCommission from ReportTemplate
END
GO

exec ZCM_SP_Report_SummaryCommissionLCC