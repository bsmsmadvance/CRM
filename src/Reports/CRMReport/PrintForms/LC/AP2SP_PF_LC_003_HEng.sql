
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_003HEng
	@TransferNumber nvarchar(50)
AS
BEGIN
	SELECT '' as ProjectNameHead, '' as ProjectName, '' as ProjectTel, '' as CompanyName, '' as Customer, '' as UnitNumber, '' as AddressNumber, '' as ContractNumber,
	'' as CustomerTel, '' as TransferDate, '' as TotalSellingPrice, '' as UnitIncreasingAreaPrice, '' as IncreasingAreaPrice, '' as NetSalePrice, '' as AreaInContract,
	'' as LandSize, '' as Pay_Down_Contract_Booking, '' as Act_Pay_Down_Contract_Booking, '' as TransferMoney, '' as Promotion, '' as SinkingFundRate, '' as PublicAdvance,
	'' as PublicFundRate, '' as ExtraDiscount, '' as ExtraPayment, '' as IncreasingArea, '' as LoanAcceptedAP, '' as LoadAccept, '' as RefundCustomers, '' as LoadStatus,
	'' as InsuranceAmount, '' as BankName, '' as BranchName, '' as PayMaterial, '' as Stamp, '' as Witness, '' as PublicFundMoney, '' as Fire, '' as Water, '' as FreeTransfer,
	'' as PercentLoanAccept, '' as ExtraDiscountAmt, '' as LandOfficeName, '' as TransferNumber, '' as MortgageRate, '' as TransferFeeRate from ReportTemplate
END
GO

exec AP2SP_PF_TR_003HEng