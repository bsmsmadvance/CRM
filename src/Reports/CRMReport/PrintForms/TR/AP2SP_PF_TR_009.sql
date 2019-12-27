
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_009
	@TransferNumber nvarchar(20),
	@NitiBankName nvarchar(250),
	@NitiBankType nvarchar(250),
	@NitiBankNo nvarchar(250),
	@CustomerBankName nvarchar(250),
	@CustomerBankType nvarchar(250),
	@CustomerBankNo nvarchar(250),
	@ContactID  nvarchar(250)
AS
BEGIN
	SELECT '' as PrintDate, '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as CustomerNameAll, '' as TransferDateApprove, '' as APAmount, '' as SumAllAPAmount, '' as ZChequeLandAmount,
	'' as ChequeLandAmount, '' as ChequePublicUtilityAmount, '' as APUtilitiesAmount, '' as LandTotalAmount, '' as CustomerFeeAmount, '' as MeterElectricAmount, '' as MeterWaterAmount,
	'' as PublicUtilityAmount, '' as CapitalAmount, '' as NitiName, '' as BringAPUtil,'' as NitiBETransfer, '' as BringAPUtilText, '' as CustomerName, '' as RemainingTotalAmount, '' as RemainingTotalAmountText,
	'' as LCName, '' as AttornyNameTransfer, '' as CheckName, '' as ApproveName, '' as ApproveName2, '' as NitiBankName, '' as NitiBankType, '' as NitiBankNo, '' as CustomerBankName,
	'' as CustomerBankType, '' as CustomerBankNo, '' as RemainingAPUtilAmount, '' as RemainingAPUtilAmountText, '' as ContractNumber, '' as LC_Email from ReportTemplate
END
GO

exec AP2SP_PF_TR_009