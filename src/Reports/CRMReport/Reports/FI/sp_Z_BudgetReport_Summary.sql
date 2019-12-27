
CREATE OR ALTER PROCEDURE sp_Z_BudgetReport_Summary
	@HomeType NVARCHAR(50)
	,@CompanyID NVARCHAR(50)
    ,@ProductID NVARCHAR(19)
	,@DateStart DATETIME
	,@DateEnd DATETIME
	,@DateStart2 DATETIME
	,@DateEnd2 DATETIME	
	,@Status5 NVARCHAR(100) = '' --ทั้งหมด/Completed/Pending 
	,@UserName  NVARCHAR(150) = ''
	,@ProjectGroup nvarchar(5)
	,@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as BU, '' as ProjetcCode, '' as ProjectName, '' as CompanyID, '' as Y, '' as Q, '' as Unit1, '' as Total1, '' as RemainingBudget1, '' as Unit2, 
	'' as Total2, '' as RemianingBudget2, '' as Unit3, '' as PriceAfterDiscount, '' as NetSellingPrice1, '' as Impact1, '' as ROIMiniPrice1, '' as Impact_ROI1,
	'' as Unit4, '' as NetSellingPrice2, '' as Impact2, '' as ROIMiniPrice2, '' as Impact_ROI2, '' as Unit5, '' as NetSellingPrice3, '' as Impact3, '' as ROIMiniPrice3,
	'' as Impact_ROI3, '' as Unit6, '' as NetSellingPrice4, '' as Impact4, '' as ROIMiniPrice4, '' as Impact_ROI4, '' as Unit7, '' as NetSellingPrice5,
	'' as Impact5, '' as ROIMiniPrice5, '' as Impact_ROI5, '' as ProcessStatus, '' as BUName, '' as CompanyName, '' as PriductName, '' as UserName, '' as ProjectGroup,
	'' as ProjectType from ReportTemplate
END
GO

exec sp_Z_BudgetReport_Summary