
CREATE OR ALTER PROCEDURE AP_CRM_Report_LC_Individual_Performance_Value
	@HomeType nvarchar(20)=null,
	@ProductID nvarchar(1000)=null,
	@QuarterYear  int=null,
	@Quarter int=null,
	@DateStart Datetime,
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5),
	@UserName nvarchar(20)
AS
BEGIN
	SELECT '' as Month1Name, '' as Month2Name, '' as Month3Name, '' as BU_Code, '' as BU_Name, '' as ProductID, '' as Project, '' as UserID, '' as EmployeeID, '' as Username, '' as FirstName, '' as LastName, '' as DisplayName, '' as LC,
	'' as RoleID, '' as LCType, '' as SaleTarget, '' as SaleTarget1, '' as SaleTarget2, '' as LCStatusFlag, '' as LCStatus, '' as TypeLC, '' as CountLC, '' as TagetTrans, '' as TaretTrans1, '' as TargetTrans2, '' as Month1, '' as Month2, '' as Month3,
	'' as QCancel, '' as Cancel1, '' as Cancel2, '' as Cancel3, '' as TransMonth1, '' as TransMonth2, '' as TransMonth3 from ReportTemplate
END
GO

exec AP_CRM_Report_LC_Individual_Performance_Value