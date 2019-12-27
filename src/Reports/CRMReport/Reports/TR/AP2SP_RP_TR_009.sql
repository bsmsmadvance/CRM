
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_009
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,
	@LandStatus nvarchar(10),
	@UnitStatus nvarchar(2),
	@LoanStatus1 nvarchar(2),		
    @UserName   nvarchar(150),
	@currentuserid nvarchar(20)
AS
BEGIN
	SELECT '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as AddressNumber, '' as LandNumber, '' as TotalSellingPrice, '' as TitledeedStatus, 
	'' as UnitStatus, '' as TransferDate, '' as LoanStatus, '' as SAPProductID,
	'' as WBSNumber, '' as NOW_PHASE, '' as COMPLETE_PHASE, '' as ALL_PHASE from ReportTemplate
END
GO

exec AP2SP_RP_TR_009