
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_014
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
	SELECT '' as ID, '' as LoanType, '' as UnitBU1, '' as NetPriceBU1, '' as UnitBU2, '' as NetPriceBU2, '' as UnitBU3, '' as NetPriceBU3, '' as UnitBU4, 
	'' as NetPriceBU4, '' as UnitBU5, '' as NetPriceBU5 from ReportTemplate
END
GO

exec AP2SP_RP_TR_014