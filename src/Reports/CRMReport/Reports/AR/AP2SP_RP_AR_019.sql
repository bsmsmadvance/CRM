
CREATE OR ALTER PROCEDURE AP2SP_RP_AR_019
	@ProductID nvarchar(30),
	@DateStart datetime,
	@ProjectType int,
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as Project, '' as ProductType, '' as YM, '' as Jan, '' as Feb, '' as Mar, '' as Apr, '' as May, '' as June, '' as July, '' as Aug,
	'' as Sep, '' as Oct, '' as Nov, '' as Dec from ReportTemplate
END
GO

exec AP2SP_RP_AR_019