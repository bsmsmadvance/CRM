
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_007
	@CompanyID  nvarchar(20),
	@ProductID	nvarchar(15),
	@DateStart	datetime = null,
	@DateStart2 Datetime = null, 
	@UserName	nvarchar(50),
	@ProjectType int
AS
BEGIN
	SELECT '' as Sample from ReportTemplate
END
GO

exec AP2SP_RP_AG_007