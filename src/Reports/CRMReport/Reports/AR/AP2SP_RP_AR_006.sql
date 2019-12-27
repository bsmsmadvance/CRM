
CREATE OR ALTER PROCEDURE AP2SP_RP_AR_006
	@CompanyID nvarchar(50),
    @ProductID nvarchar(20),
	@DateStart Datetime,
	@Username nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyNameThai, '' as ProjectName, '' as TypeOfRealEstate, '' as UnitNumber, '' as Customer, '' as ContractNumber, '' as ContractDate, '' as SellingPrice, '' as DownPaymentPeriod, '' as DownAmount,
	'' as Promotion, '' as PayDown, '' as DueDate, '' as Period, '' as Nextpayable, '' as Amount, '' as RowNo, '' as NO1 from ReportTemplate
END
GO

exec AP2SP_RP_AR_006