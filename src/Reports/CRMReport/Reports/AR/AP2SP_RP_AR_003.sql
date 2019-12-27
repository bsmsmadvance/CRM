
CREATE OR ALTER PROCEDURE AP2SP_RP_AR_003

	@CompanyID nvarchar(50),
    @ProductID nvarchar(50),
	@DateStart  datetime,
	@UserName nvarchar(200)
AS
BEGIN
	SELECT '' as CompanyName, '' as CompanyID, '' as ProjectName, '' as ProductID, '' as SellAlready, '' as TransferAlready, '' as WaitTFPrice, '' as CountWaitTransfer,
	'' as PayableAmount, '' as Amount from ReportTemplate
END
GO

exec AP2SP_RP_AR_003