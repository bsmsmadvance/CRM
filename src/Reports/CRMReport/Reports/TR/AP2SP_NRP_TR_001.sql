
CREATE OR ALTER PROCEDURE AP2SP_NRP_TR_001
	@DateStart Datetime,    
	@DateEnd Datetime,
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as [Group], '' as BankName, '' as Payment, '' as TotalHomePrice from ReportTemplate
END
GO

exec AP2SP_NRP_TR_001