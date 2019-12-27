
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_008
    @CompanyID  nvarchar(20),
	@ProductID	nvarchar(15) = '',
	@UnitNumber	nvarchar(15) = '',
	@StatusAG   nvarchar(20),
	@DateStart datetime=NULL,
	@DateEnd Datetime = null ,
	@UserName	nvarchar(50) = ''
AS
BEGIN
	SELECT '' as ProductID, '' as UnitNumber, '' as ContractNumber, '' as ContactID, '' as CustomerName, '' as Price, '' as BookingDate, 
	'' as ContractDueDate, '' as RDate, '' as TransferContact, '' as TransferDateApprove, '' as Status, '' as CompanyID,
	'' as CompanyNameThai, '' as Project,'' as Cancel, '' as OverDue1, '' as OverDue61, '' as OverDUe121, '' as OverDUe181, '' as OverDueTransferDate, 
	'' as TransferDate1, '' as TransferDate2 from ReportTemplate
END
GO

exec AP2SP_RP_AG_008