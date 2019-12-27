
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_002Eng
    @BookingNumber  nvarchar(15)
AS
BEGIN
	SELECT '' as BookingNumber, '' as BookingDate, '' as ProductID, '' as ProjectName, '' as NameBooking, '' as Age, '' as Nationality, '' as BCurrentAddress, '' as BMoo, '' as BVillage,
	'' as [B.Soi], '' as BRoad, '' as BSubDistrict, '' as BDistrict, '' as BProvince, '' as BPostCode, '' as BPhone, '' as BMobile, '' as BEmail, '' as NameAgreement, '' as CompanyID,
	'' as CompanyName, '' as Address1, '' as Address2, '' as Soi, '' as Road, '' as SubDistrict, '' as District, '' as Province, '' as UnitNumber, '' as FloorID, '' as StandardArea,
	'' as TotalSellingPrice, '' as SellingPrice, '' as PriceText, '' as ContractDueDate, '' as PermanentAddress, '' as PermanentMoo, '' as PermanentVillage, '' as PermanentSoi,
	'' as PermanentRoad, '' as PermanentSubDistrict, '' as PermanentDistrict, '' as PermanentProvince, '' as PermanentPostID, '' as PermanentPhone, '' as PermanentMobile, '' as PermanentEmail,
	'' as InsurancePremiumBuilding, '' as PublicFundRate, '' as UnitTransferfee, '' as UnitTransferfeeText, '' as SaleName, '' as PromotionDetail, '' as CashDiscount, '' as TransferDiscount,
	'' as Condition, '' as ReceivedID, '' as Parking, '' as Is3Step, '' as ApproveDate, '' as PromotionAmount from ReportTemplate
END
GO

exec AP2SP_PF_AG_002Eng