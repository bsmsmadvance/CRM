
CREATE OR ALTER PROCEDURE AP2SP_RP_AR_013
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@DateStart2 Datetime,
	@DateEnd2 Datetime,
    @StatusAG nvarchar(10),
	@Username nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyName, '' as Project, '' as TypeOfRealEstate, '' as UnitNumber, '' as ContractDate, '' as ContractNumber, '' as Customer, '' as Flag,
	'' as TotalSellingPrice, '' as Month, '' as Priority, '' as PayableAmount, '' as RowNo from ReportTemplate
END
GO

exec AP2SP_RP_AR_013