
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_017_1
	@HomeType nvarchar(20),
	@DateStart datetime,
	@DateEnd datetime,
	@Quarter int, 
	@QuarterYear int,
	@UserName nvarchar(250) = '',
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as BrandID, '' as BrandName, '' as PercentLoan50_LESS, '' as PercentLoan51_60, '' as PercentLoan61_70, '' as PercentLoand71_80, '' as PercentLoad81_90,
	'' as PercentLoan91_100, '' as PercentLoan100_UP from ReportTemplate
END
GO

exec AP2SP_RP_TR_017_1