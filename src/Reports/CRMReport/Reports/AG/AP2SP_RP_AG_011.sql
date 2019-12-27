
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_011
--	@BatchID nvarchar(4000),
	@Projects	nvarchar(4000),
	@DateStart datetime=NULL,
	@DateEnd   datetime = NULL,
	@HomeType nvarchar(20),
	@StatusProject nvarchar(2),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5),
	@UserName	nvarchar(50) = ''
AS
BEGIN
	
	SELECT '' as ProjectName, '' as TotalUnit, '' as UnitEmty, '' as PriceEmty, '' as Producttype, '' as PType, '' as StartSale, '' as UnitBKP, '' as PriceBKP, 
	'' as UnitCBKP, '' as PriceCBKP, '' as UnitNetP, '' as PriceNetP, '' as UnitTFP,
	'' as PriceTFP, '' as OverDue1, '' as OverDue61, '' as OverDue121, '' as OverDue181, '' as OverDue1Price, '' as OverDue61Price, '' as OverDue121Price, 
	'' as OverDue181Price from ReportTemplate
END
GO

exec AP2SP_RP_AG_011