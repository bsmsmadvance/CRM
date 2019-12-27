
--

CREATE OR ALTER PROCEDURE AP2SP_PF_LC_003_CDEng
	@TransferNumber nvarchar(50)
AS
BEGIN
	SELECT '' as ProjectNameHead, '' as ProjectName, '' as ProjectTel, '' as CompanyName, '' as Customer, '' as UnitNumber, '' as AddressNumber, '' as ContractNumber,
	'' as CustomerTel, '' as TransferDate, '' as TotalSellingPrice, '' as UnitIncreasingAreaPrice, '' as NetSalePrice, '' as AreaInContract, '' as LandSize,
	'' as Pay_Down_Contract_Booking, '' as Act_Pay_Down_Contract_Booking, '' as TransferMoney, '' as Promotion, '' as SinkingFundRate, '' as LoanAccept, '' as ExtraDiscount,
	'' as ExtraPayment, '' as IncreasingArea, '' as LoanAcceptedAP, '' as PayMaterial, '' as PublicFundMoney, '' as Fire, '' as FreeTransfer, '' as Stamp, '' as PercentLoanAccept,
	'' as SinkingFundBaht, '' as ExtraDiscountAmt, '' as Witness, '' as LandOfficeName, '' as LoanStatus, '' as InsuranceAmount, '' as BankName, '' as BranchName,
	'' as RefundCustomers, '' as TransferNumber, '' as MortgageRate, '' as TransferFeeRate from ReportTemplate
END
GO

exec AP2SP_PF_LC_003_CDEng