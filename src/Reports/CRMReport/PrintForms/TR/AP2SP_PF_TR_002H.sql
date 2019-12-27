
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_002H
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as ContractNumber, '' as UnitNumberHeader, '' as LandOfficeID, '' as LandOfficeName, '' as LandPortionNumber, '' as TitledeedNumber, '' as LandNumber,
	'' as LandSurveyArea, '' as LandSubDistrict, '' as LandDistrict, '' as LandProvince, '' as AreaTitledeed, '' as TypeHome, '' as YearOfAlter, '' as ModelBuiltInArea,
	'' as NetSalePrice, '' as TransferDate, '' as AttornyIssueDate, '' as CompanyName, '' as AttornyName1, '' as CustomerName, '' as LandSize, '' as UnitIncreasingAreaPrice,
	'' as EstimateLandPricePerMetre, '' as BuiltInLand, '' as BuiltInPrice, '' as FactorArea, '' as FactorPrice, '' as CurvedSteelArea, '' as CurvedSteelPrice,
	'' as ALLFactor, '' as DepreRate_Total, '' as [DepreRate_Total(1)], '' as HomeNoReceivedDate from ReportTemplate
END
GO

exec AP2SP_PF_TR_002H