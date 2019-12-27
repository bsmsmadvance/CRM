
CREATE OR ALTER PROCEDURE AP2SP_ExecutiveReportV3_ByPType

	@BatchID nvarchar(4000)=null,
	@DateStart datetime=null,
	@DateEnd   datetime=null,
	@StatusProject nvarchar(2)=null,
	@UserName nvarchar(150)=''
AS
BEGIN
	SELECT '' as ProjectName, '' as TotalUnit, '' as UnitNoBK, '' as UnitEmpty, '' as PriceEmpty, '' as ProductType, '' as StartSale, '' as PType, '' as UnitBKW, 
	'' as PriceBKW, '' as UnitBKM, '' as PriceBKM, '' as UnitBKY, '' as PriceBKY,
	'' as UnitBKP, '' as PriceBKP, '' as UnitCBKW, '' as PriceCBKW, '' as UnitCBKM, '' as PriceCBKM, '' as UnitCBKY, '' as PriceCBKY, '' as UnitCBKP, '' as PriceCBKP,
	'' as UnitNetW, '' as PriceNetW, '' as UnitNetM, '' as PriceNetM, '' as UnitNetY, '' as PriceNetY, '' as UnitNetP, '' as PriceNetP,
	'' as UnitAGDP, '' as PriceAGDP, '' as UnitTFW, '' as PriceTFW, '' as UnitTFM, '' as PriceTFM, '' as UnitTFY, '' as PriceTFY, '' as UnitTFP, '' as PriceTFP, 
	'' as UnitNTF, '' as PriceNTF from ReportTemplate
END
GO

exec AP2SP_ExecutiveReportV3_ByPType