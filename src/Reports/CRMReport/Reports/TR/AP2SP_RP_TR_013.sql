
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_013
	@HomeType nvarchar(20),
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,
	@UnitStatus nvarchar(2),
	@LoanStatus1 nvarchar(2),
    @UserName   nvarchar(150),
	@currentuserid nvarchar(20),
	@DateStart2 datetime ,
	@DateEnd2   datetime,
	@DateStart3 datetime,
	@DateEnd3   datetime,
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as PType, '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as CustomerName, '' as LandNumber, '' as AddressNumber, '' as TitledeedStatus, 
	'' as UnitStatus, '' as LoanStatus, '' as BankName, '' as BranchName, '' as LoadAccepted,
	'' as LoadAcceptedAP, '' as InsuranceAmount, '' as RefundCustomers, '' as TransferDate, '' as TransferDateInContract, '' as WotkTransferDate, 
	'' as SAPProductID, '' as WBSNumber from ReportTemplate
END
GO

exec AP2SP_RP_TR_013