
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_015
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
	SELECT '' as BankID, '' as BankName, '' as UnitBU1, '' as LoaValueBU1, '' as UnitBU2, '' as LoaValueBU2, '' as UnitBU3, '' as LoaValueBU3, '' as UnitBU4, 
	'' as LoaValueBU4, '' as UnitBU5, '' as LoaValueBU5 from ReportTemplate
END
GO

exec AP2SP_RP_TR_015