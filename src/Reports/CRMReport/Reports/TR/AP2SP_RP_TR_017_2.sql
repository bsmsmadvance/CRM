
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_017_2
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
	SELECT '' as BankID, '' as BankName, '' as BrandID, '' as BrandName, '' as ProductID, '' as Unit from ReportTemplate
END
GO

exec AP2SP_RP_TR_017_2