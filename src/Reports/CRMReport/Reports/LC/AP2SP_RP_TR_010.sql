
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_010
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,
	@LandStatus nvarchar(10),
	@UnitStatus nvarchar(2),
	@LoanStatus1 nvarchar(2),
	@QCStatus nvarchar(2),		
    @UserName   nvarchar(150),
	@currentuserid nvarchar(20),
	@WorkTransferStatus nvarchar(10),
	@WorkTransferDate datetime
AS
BEGIN
	SELECT '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as AddressNumber, '' as LandNUmber, '' as TotalSellingPrice, '' as PaymentedAmount, 
	'' as TitledeedStatus, '' as UnitStatus, '' as TransferDate, '' as LoanStatus, '' as SAPProductID,
	'' as WBSNumber, '' as BookingNumber, '' as ContractNumber, '' as END_PRODUCT_COMPLETE, '' as SE_COUNT, '' as FINISHDATE, '' as BankName, '' as BranchName, 
	'' as IsImport, '' as WaiveQCDate from ReportTemplate
END
GO

exec AP2SP_RP_TR_010