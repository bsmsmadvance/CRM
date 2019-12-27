
CREATE OR ALTER PROCEDURE AP2SP_RP_AR_020

	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@UserName nvarchar(150),
	@UnitNumber nvarchar(4000)
AS
BEGIN
	SELECT '' as Project, '' as ProjectNameThai, '' as ProductID, '' as UnitNumber, '' as TransferDateApprove, '' as CustomerName, '' as RDate,
	'' as GLBatchID, '' as WaterMeter, '' as FireMeter, '' as TotalMeter from ReportTemplate
END
GO

exec AP2SP_RP_AR_020