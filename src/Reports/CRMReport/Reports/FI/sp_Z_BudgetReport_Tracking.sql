
CREATE OR ALTER PROCEDURE sp_Z_BudgetReport_Tracking
	@HomeType NVARCHAR(50)
	,@CompanyID NVARCHAR(50)
    ,@ProductID NVARCHAR(19)
	,@DateStart DATETIME
	,@DateEnd DATETIME
	,@DateStart2 DATETIME
	,@DateEnd2 DATETIME	
	,@Status3 NVARCHAR(100) = '' --ทั้งหมด=0/จอง=1/สัญญา=2/โอน=3/Active=4
	,@Status4 NVARCHAR(100) = '' --All=0/Pending=1/Approve=2/Headof=3/CCO=4/MD=5	
	,@UserName  NVARCHAR(150) = ''
	,@ProjectGroup nvarchar(5)
	,@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as BU, '' as ProjectCode, '' as ProjectName, '' as CompanyID, '' as Unit, '' as Status1, '' as BookingDate, '' as TransferDate, '' as SellingPrice, 
	'' as NetSellingPrice, '' as Impact, '' as QuarterlyBudget, '' as Type, '' as Status2, '' as PendingWho,
	'' as Sales, '' as Status1Type, '' as Status2Type, '' as DocStatus, '' as ProcessStatus, '' as BUName, '' as CompanyName, '' as ProductName, '' as UserName, 
	'' as Status, '' as ProjectGroup, '' as ProjectType from ReportTemplate
END
GO

exec sp_Z_BudgetReport_Tracking