
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_001Eng
    @ContractNumber  nvarchar(15)
AS
BEGIN
	SELECT '' as ContractNumber, '' as ContractDate, '' as BookingNumber, '' as BookingDate, '' as ProductID, '' as ProjectName, '' as NameBooking, '' as Age, '' as BCurrentAddress,
	'' as BMoo, '' as [B.Soi], '' as BRoad, '' as BSubDistrict, '' as BDistrict, '' as BProvince, '' as BPostCode, '' as BPhone, '' as BMobile, '' as CompanyName, '' as AddressThai,
	'' as Road, '' as SubDistrict, '' as District, '' as Province, '' as TowerName, '' as UnitNumber, '' as FloorID, '' as StandardArea, '' as IncreasingAreaPrice, '' as IncreasingAreaPriceText,
    '' as TotalSellingPrice, '' as TotalSellingPriceText, '' as BookingPaid, '' as BookingPaidText, '' as ContractPaid, '' as DownPaymentPeriod, '' as ContractPaidText, '' as TotalPaid,
	'' as TotalPaidText, '' as TotalNoPay, '' as TotalNoPayText, '' as PeriodDate, '' as StartDate, '' as NameAgreement, '' as PermanentAddress, '' as PermanentMoo, '' as PermanentVillage,
	'' as PermanentSoi, '' as PermanentRoad, '' as PermanentSubDistrict, '' as PermanentDistrict, '' as PermanentProvince, '' as PermanentPostID, '' as PermanentPhone, '' as InsurancePremiumBuilding,
	'' as PublicFundRate, '' as UnitTransferFee, '' as UnitTransferfeeText, '' as FlagFur, '' as ForType from ReportTemplate
END
GO

exec AP2SP_PF_AG_001Eng