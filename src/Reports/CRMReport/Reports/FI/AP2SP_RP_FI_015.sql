-- รายงานรายละเอียดการออกใบเสร็จรับเงิน
CREATE OR ALTER PROCEDURE AP2SP_RP_FI_015
    @CompanyID  nvarchar(20),
	@ProductID	nvarchar(15) = '',
	@UnitNumber	nvarchar(15) = '',
	@UserName	nvarchar(50) = '',
	@DateStart  DateTime,
	@DateEnd	DateTime,
	@ReceivedStart   nvarchar(20) = '',
	@ReceivedEnd	 nvarchar(20) = '',
    @PrintingIDStart nvarchar(30) = '',
    @PrintingIDEnd   nvarchar(30) = ''
AS
BEGIN
	SELECT '' as RDate, '' as CompanyNameThai, '' as ProductName, '' as ProductID, '' as ReceivedID, '' as PrintingID, '' as CustomerName, '' as UnitNumber, 
	'' as Amount, '' as MethodBy, '' as AdBankName, '' as BranchName, '' as Number,
	'' as PrintDate, '' as StatusReceive, '' as Method, '' as DueDate, '' as PrintName, '' as Period, '' as AmountPeriod from ReportTemplate
END
GO

exec AP2SP_RP_FI_015