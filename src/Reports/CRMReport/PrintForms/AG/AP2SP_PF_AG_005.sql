
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_005
	@ContractNumber nvarchar(50)
AS
BEGIN
	SELECT '' as ContractName, '' as ProjectName, '' as ContractDate, '' as CompanyNameThai, '' as CompanyAddress, '' as AddressThai, '' as SubDistrictThai, '' as SoiTHai, '' as RoadThai, '' as DistrictThai, '' as ProvinceThai, '' as CompanyTel,
	'' as AuthoritarianByCompany, '' as ProjectNumberLocation, '' as Project_Soi, '' as Project_Road, '' as Project_SubDistrict, '' as Project_District, '' as Project_Province, '' as Project_Telephone, '' as CustomerName, '' as Customer_Age,
	'' as Nationality, '' as Customer_Location, '' as Customer_Soi, '' as Customer_Moo, '' as Customer_Road, '' as Customer_SubDistrict, '' as Customer_District, '' as Customer_Province, '' as Customer_Telephone, '' as Customer_Email,
	'' as RelativeNumber, '' as RelativeDate, '' as UnitNumber, '' as AreaOfContract, '' as Model, '' as TitledeedNumber, '' as LandSurveyArea, '' as LandNumber, '' as MortgageWith, '' as MortgageAmount, '' as MortgageAmountText,
	'' as PricePerArea, '' as PricePerAreaText, '' as PriceOfContract, '' as PriceOfContractText, '' as BookingDate, '' as BookingPaid, '' as BookingAmountText, '' as ContractAmount, '' as ContractAmountText, '' as TotalInContractDate,
	'' as TotalInContractDateText, '' as RemainOfPrice, '' as RemainOfPriceText, '' as EveryDate, '' as StartDate, '' as UnitTransferFee, '' as UnitTransferFeeText, '' as PriceMeterWaterElectric, '' as PriceMeterWaterElectricText,
	'' as MeterWater, '' as PriceMeterWater, '' as PriceMeterWaterText, '' as MeterElectric, '' as PriceMeterElectric, '' as PriceMeterElectricText, '' as FindRate, '' as Find, '' as FindText, '' as FindHousingAll, '' as FindHousingAllText,
	'' as FindHousingDay, '' as FindHousingDayText, '' as witness1, '' as witness2, '' as PublicAdvance, '' as PublicFundRate, '' as FrontWidth, '' as Project_SubDistrict2, '' as Project_District2, '' as Project_Province2, '' as ProductID,
	'' as Flag_AG, '' as ForType, '' as AAA, '' as BBB, '' as PromotionAMount, '' as StartSale, '' as BG, '' as RealBookingDate, '' as TransferDiscount, '' as TransferDiscountText, '' as ContractVersion, '' as ContractReferent, '' as HistoryID from ReportTemplate
END
GO

exec AP2SP_PF_AG_005