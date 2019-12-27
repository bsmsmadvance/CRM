
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_004Eng
	@ContractNumber nvarchar(50)
AS
BEGIN
	SELECT '' as ContractNumber, '' as ProductID, '' as ProjectName, '' as ContractDate, '' as CompanyNameThai, '' as AddressThai, '' as Company_Soi, '' as Company_Road, '' as Company_Moo, '' as Company_SubDistrict, '' as Company_District,
	'' as Company_Province, '' as Company_Tel, '' as UtilityAttornyName, '' as AttoryIssueDate, '' as AttoryIssueMonth, '' as AttoryIssueYear, '' as Project_Address, '' as Project_Soi, '' as Project_Road, '' as Project_Moo, '' as Project_SubDistrict,
	'' as Project_District, '' as Project_Province, '' as Project_Tel, '' as CustomerName, '' as Customer_Age, '' as Customer_Nationality, '' as Customer_Address, '' as Customer_Village, '' as Customer_Soi, '' as Customer_Moo, '' as Customer_Road,
	'' as Customer_SubDistrict, '' as Customer_District, '' as Customer_Province, '' as Customer_Telephone, '' as Customer_EMail, '' as Project_TitleDeedNumber, '' as Project_Survey, '' as Project_LandNumber, '' as Project_Area,
	'' as LicenseProductNumber, '' as LicenseProductDate, '' as UnitNumber, '' as FloorID, '' as Unit_Area, '' as IncreasingAreaPrice, '' as IncreasingAreaBath, '' as TotalSellingPrice, '' as TotalSellingPriceText, '' as BookingDate,
	'' as BookingPaid, '' as BookingBath, '' as ContractAmount, '' as ContractAmountText, '' as TotalInContractDate, '' as TotalInContractDateText, '' as Expr1059, '' as RemainOfPrice, '' as RemainOfPriceText, '' as PaymentDown, '' as PaymentDownPerMonth,
	'' as PaymentDownPerMonthText, '' as Payment_DueDate, '' as LastPayment, '' as LastPaymentText, '' as TransferDate, '' as SinkingFundRate, '' as PublicFundRate, '' as BurimasitBank, '' as BurimasitAmount, '' as BurimasitAmountText,
	'' as ParkingUnit, '' as LastPeriod, '' as IsBuildComplete, '' as witness1, '' as witness2, '' as Parking, '' as ParkingIncrease, '' as TotalParking, '' as Rai, '' as Ngarn, '' as SquareWah, '' as Preferunit, '' as Project_SubDistrict2,
	'' as Project_District2, '' as Project_Province2, '' as Flag_AG, '' as TicketAmt, '' as TParking, '' as ParkingAmt, '' as ParkingNoAmt, '' as NoParkingAmt, '' as ForType, '' as UnitNumber_Old, '' as Logo, '' as FloorPlanImage, '' as RoomPlanImage,
	'' as image_FloorPlanImage, '' as image_RoomPlanImage, '' as IsNotPassEIA, '' as DocumentName, '' as HistoryID, '' as OperateType, '' as PromotionAmount from ReportTemplate
END
GO

exec AP2SP_PF_AG_004Eng