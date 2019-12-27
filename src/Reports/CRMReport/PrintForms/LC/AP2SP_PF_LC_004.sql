
--

CREATE OR ALTER PROCEDURE AP2SP_PF_LC_004

	@ProductID nvarchar(50),
	@UnitNumber nvarchar(4000),
	@DateStart datetime
AS
BEGIN
	SELECT '' as StatusDate, '' as Project, '' as LoanBank, '' as CompanyName, '' as TransferDate, '' as ProductID, '' as UnitNumber, '' as TitledeedNumber, '' as AreaOfContract,
	'' as AreaFromPFB, '' as AreaFromRE, '' as LandNumber, '' as LandSurveyArea, '' as Customer, '' as PriceOfContract, '' as PreferUnit, '' as LoadReturnType, '' as PreferUnitMinimum,
	'' as Price1, '' as Price2, '' as TotalReducePrice, '' as TotalReducePriceText, '' as TotalReturnPrice, '' as TotalReturnPriceText, '' as LCode, '' as PreferApprove,
	'' as PositionPreferApprove, '' as ModelHomeThai, '' as PreferHouse from ReportTemplate
END
GO

exec AP2SP_PF_LC_004