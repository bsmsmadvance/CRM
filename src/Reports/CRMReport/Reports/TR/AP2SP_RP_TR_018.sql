
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_018
	@HomeType nvarchar(20),
	@DateStart datetime,
	@DateEnd datetime,
	@Quarter int, 
	@QuarterYear int,
	@UserName nvarchar(250),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as ContractNumber, '' as HistoryType, '' as NewData, '' as OldData, '' as HistoryDate, 
	'' as HistoryByName from ReportTemplate
END
GO

exec AP2SP_RP_TR_018