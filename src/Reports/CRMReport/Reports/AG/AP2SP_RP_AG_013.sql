
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_013
    @CompanyID  nvarchar(20),
	@ProductID	nvarchar(15) = '',
	@UnitNumber	nvarchar(15) = '',
	@ModelTypeName nvarchar(15) = '',
	@CurrentPeriod nvarchar(15) = '',
	@PercentConstruction nvarchar(15) = '',
	@UserName	nvarchar(50) = ''

AS
BEGIN
	
	SELECT '' as ProductID, '' as UnitNumber, '' as ProductName, '' as ModelName, '' as ModelTypeID, '' as ConstructionType, '' as CurrentPhase, '' as MaxPhase, '' as PercentConstruction, '' as WaiveQCDate, '' as PassEndMajorDate,
	'' as PassEndFullDate, '' as BookingDate, '' as ContractDate, '' as TransferDate, '' as TransferRealDate from ReportTemplate
END
GO

exec AP2SP_RP_AG_013