
CREATE OR ALTER PROCEDURE RPT.RP_CommisionRate
	@ProductID nvarchar(20),
	@DateStart datetime,
	@DateEnd datetime,
	@UserName	nvarchar(50)
AS
BEGIN
	SELECT '' as Sample from ReportTemplate
END
GO

exec RPT.RP_CommisionRate