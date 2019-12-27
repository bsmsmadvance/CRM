
CREATE OR ALTER PROCEDURE AP2SP_RP_FI_008
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
	@DateStart datetime = '',
	@DateEnd   datetime = '',
    @UserName nvarchar(150)
AS
BEGIN
	SELECT '' as Sample from ReportTemplate
END
GO

exec AP2SP_RP_FI_008