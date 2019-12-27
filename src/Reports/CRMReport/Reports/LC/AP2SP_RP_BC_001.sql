
CREATE OR ALTER PROCEDURE AP2SP_RP_BC_001
      @CompanyID  nvarchar(20)
    , @ProductID  nvarchar(20)
	, @DateStart datetime
	, @DateEnd   datetime
	, @DateStart2 datetime
	, @DateEnd2   datetime
    , @UserName nvarchar(150)
    , @statustransfer nvarchar(20)
    , @UnitNumber nvarchar(50)
AS
BEGIN
	SELECT '' as CompanyName, '' as ProductID, '' as ProjectName, '' as ModelType, '' as OperateDate, '' as ApproveDate, '' as OperateBy, '' as UnitNumber, '' as TransferDate, '' as CustOld, '' as CustTelOld, '' as ReferenceID, '' as SellingPriceOld,
	'' as TotalAmtOld, '' as CustNew, '' as CustTelNew, '' as ContractNew, '' as SellingPriceNew, '' as TotalAmtNew, '' as CustNew, '' as CustTelNew, '' as ContractNew, '' as SellingPriceNew, '' as TotalAmtNew, '' as TransferDiscount, '' as UnitTransferFee,
	'' as UnitTransferAmt, '' as StatusTransfer from ReportTemplate
END
GO

exec AP2SP_RP_BC_001