
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_002CD
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as LandOfficeName, '' as TransferDate, '' as TitledeedNumber, '' as LandNumber, '' as LandSurveyArea, '' as LandSubDistrict, '' as LandDistrict, '' as LandProvince,
	'' as AreaTitledeed, '' as ProjectName, '' as BuildingProduct, '' as TowerID, '' as FloorID, '' as UnitNumber, '' as UnitNumberHeader, '' as AreaUnit, '' as NetSalePrice,
	'' as CompanyName, '' as CustomerName, '' as AttornyIssueDate, '' as AttornyName1, '' as ProductID, '' as VerandaArea, '' as VerandaPrice, '' as TotalVerandaPrice,
	'' as AirArea, '' as AirAreaPrice, '' as TotalAirPrice, '' as BuildinPrice, '' as BuildinLand, '' as TotalBuiltinPrice, '' as TotalEstimatePrice, '' as Parking, '' as ParkingArea,
	'' as ParkingPrice, '' as TotalParkingPrice, '' as ALLFactor, '' as ParkingStatus, '' as LandOfficeID, '' as CityName from ReportTemplate
END
GO

exec AP2SP_PF_TR_002CD