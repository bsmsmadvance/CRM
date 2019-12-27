
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_009_PT
	@TransferNumber nvarchar(20),
	@NitiBankName nvarchar(250),
	@NitiBankType nvarchar(250),
	@NitiBankNo nvarchar(250),
	@CustomerBankName nvarchar(250),
	@CustomerBankType nvarchar(250),
	@CustomerBankNo nvarchar(250),
	@ContactID  nvarchar(250),
	@CustomerBankName2 nvarchar(250),
	@CustomerBankType2 nvarchar(250),
	@CustomerBankNo2 nvarchar(250),
	@ContactID2  nvarchar(250)
AS
BEGIN
	SELECT '' as PrintDate, '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as OldCustomerNameAll, '' as NewCustomerNameAll, '' as OldAPAmount, '' as OldAPAmount2 ,
	'' as PreTransferFee, '' as OldPublicUtilityAmount, '' as HomePrice, '' as AdjustPrice, '' as TransferDiscount, '' as BookContractDownPrice, '' as ChequeHomeAmount,
	'' as ChequeHomeLandAmount, '' as ChequeNitiAmount, '' as LandTotalAmount, '' as CustomerFeeAmount, '' as MeterElectricAmount, '' as PublicUtilityAmount, '' as CapitalAmount,
	'' as LCName, '' as LC_Email, '' as AttornyNameTransfer, '' as CheckName, '' as ApproveName, '' as ApproveName2, '' as NitiName, '' as NitiBankName, '' as NitiBankType,
	'' as NitiBankNo, '' as CustomerBankName, '' as CustomerBankType, '' as CustomerBankNo, '' as CustomerBankName2, '' as CustomerBankType2, '' as CustomerBankNo2, '' as ContractNumber,
	'' as ReturnText1, '' as ReturnText2, '' as ReturnNitiText from ReportTemplate
END
GO

exec AP2SP_PF_TR_009_PT