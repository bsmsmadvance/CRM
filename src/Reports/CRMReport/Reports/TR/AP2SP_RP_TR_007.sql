
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_007
@CompanyID nvarchar(50)=''
,@ProductID nvarchar(50)=''
,@DateStart DateTime
,@DateEnd DateTime
,@UserName nvarchar(150)=''
AS
BEGIN
	SELECT '' as CompanyName, '' as ProductID, '' as Project, '' as TransferDateApprove, '' as UnitNumber, '' as CashAmount, '' as MinistryOfFinanceCheque, 
	'' as MinistryCash, '' as SumChequeAP, '' as TransferToUtil, '' as SumChequeAPUtil,
	'' as APAmount, '' as APMeterAmount, '' as APLandsAmount, '' as RemainingAPAmount, '' as AllIncomeTax, '' as FeeAPAmount, '' as FeeCustomerAmount, 
	'' as FeeAccountReturnAmount, '' as GreetingAmount, '' as XeroxAmount, '' as FareAmount,
	'' as FeeMortgageAmount, '' as CashChangeAmount, '' as ChangeAmount, '' as ReceiveCash, '' as TransferNumber from ReportTemplate
END
GO

exec AP2SP_RP_TR_007