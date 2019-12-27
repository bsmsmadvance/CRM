
CREATE OR ALTER PROCEDURE AP2SP_ExecutiveReportV2_1
	@BatchID nvarchar(4000),
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as ExecutiveReportV2 from ReportTemplate
END
GO

exec AP2SP_ExecutiveReportV2_1