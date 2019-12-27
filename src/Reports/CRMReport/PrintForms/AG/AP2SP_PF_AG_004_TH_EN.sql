
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_004_TH_EN
	@ContractNumber nvarchar(50)
AS
BEGIN
	SELECT '' as ContractNumber, '' as ProductID, '' as ProjectName, '' as ProjectNameEng, '' as ContractDate, '' as ContractDateEng, '' as ContractMonthEng, '' as ContractYearEng,
	'' as CompanyNameThai, '' as CompanyNameEng, '' as AddressThai, '' as Company_Soi, '' as Company_Road, '' as Company_Moo, '' as Company_SubDistrict, '' as Company_District,
	'' as Company_Province, '' as Company_Tel, '' as UtilityAttorneyName, '' as UtilityAttorneyNameEng, '' as AttorneyIssueDate, '' as AttorneyIssueMonth, '' as AttorneyIssueYear, '' as AttorneyIssueMonthEng, '' as AttorneyYearEng,
	'' as Project_Address, '' as Project_Soi, '' as Project_Road, '' as Project_Moo, '' as Project_SubDistrict, '' as Project_District, '' as Project_Province, '' as Project_Tel, 
	'' as ProjectWechat, '' as Project_AddressEng, '' as Project_SoiEng, '' as Project_RoadEng, '' as Project_MooEng, '' as Project_SubDistrictEng, '' as Project_DistrictEng, '' as Project_ProvinceEng,
	'' as CustomerName, '' as CustomerNameEng, '' as Customer_Age, '' as Customer_Nationality, '' as Customer_Address, '' as Customer_Village, '' as Customer_Soi, '' as Customer_Moo, '' as Customer_Road,
	'' as Customer_SubDistrict, '' as Customer_District, '' as Customer_Province, '' as Customer_Telephone, '' as Customer_EMail, '' as Customer_NationalityEng, '' as Customer_AddressEng,
	'' as Customer_VillageEng, '' as Customer_SoiEng, '' as Customer_MooEng, '' as Customer_RoadEng, '' as Customer_SubDistrictEng, '' as Customer_DistrictEng, '' as Customer_ProvinceEng,
	'' as Customer_Wechat, '' as Customer_LineID, '' as Project_TitleDeedNumber, '' as Project_Survey, '' as Project_LandNumber, '' as Project_Area,
	'' as LicenseProductNumber, '' as LicenseProductDate, '' as UnitNumber, '' as FloorID, '' as Unit_Area, '' as IncreasingAreaPrice, '' as IncreasingAreaBaht, '' as IncreasingAreaBahtEng,
	'' as TotalSellingPrice, '' as TotalSellingPriceText, '' as TotalSellingPriceTextEng, '' as BookingDate, '' as BookingPaid, '' as BookingBaht, '' as BookingBahtEng, '' as ContractAmount, 
	'' as ContractAmountText, '' as ContractAmountTextEng, '' as TotalInContractDate, '' as TotalInContractDateText, '' as TotalContractDateTextEng, '' as RemainOfPrice, '' as RemainOfPriceText, '' as RemainOfPriceTextEng,
	'' as PaymentDown, '' as PaymentDownPerMonth, '' as PaymentDownPerMonthText, '' as PaymentDownPerMonthTextEng, '' as Payment_DueDate, '' as LastPayment, '' as LastPaymentText, '' as LastPaymentTextEng,
	'' as TransferDate, '' as SinkingFundRate, '' as SinkingFundRateText, '' as SinkingFundRateTextEng ,'' as PublicFundRate, '' as PublicFundRateText, '' as PublicFundRateEng,
	'' as BurimasitBank, '' as BurimasitBankEng, '' as BurimasitAmount, '' as BurimasitAmountText, '' as BurimasitAmountTextEng,
	'' as ParkingUnit, '' as LastPeriod, '' as IsBuildComplete, '' as witness1, '' as witness2, '' as witness1Eng, '' as witness2Eng, '' as Parking, '' as ParkingIncrease, '' as TotalParking, 
	'' as Rai, '' as Ngarn, '' as SquareWah, '' as Preferunit, '' as Project_SubDistrict2, '' as Project_District2, '' as Project_Province2, '' as Project_SubDistrict2Eng, '' as Project_District2Eng, '' as Project_Province2Eng,
	'' as Flag_AG, '' as TicketAmt, '' as TParking, '' as ParkingAmt, '' as ParkingNoAmt, '' as NoParkingAmt, '' as ForType, '' as ForTypeEng, '' as UnitNumber_Old, '' as Logo, '' as FloorPlanImage, '' as RoomPlanImage,
	'' as image_FloorPlanImage, '' as image_RoomPlanImage, '' as IsNotPassEIA, '' as DocumentName, '' as DocumentNameEng, '' as HistoryID, '' as OperateType, '' as PromotionAmount,
	'' as IsForeignUnit, '' as IsNationalityThai, '' as IsForeignQuataProject, '' as ParkingText, '' as ParkingTextEng from ReportTemplate
END
GO

exec AP2SP_PF_AG_004_TH_EN