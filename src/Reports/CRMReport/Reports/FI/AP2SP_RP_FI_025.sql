
CREATE OR ALTER PROCEDURE AP2SP_RP_FI_025
	@CompanyID  nvarchar(20),
    @ProductID  nvarchar(20),
    @UnitNumber  nvarchar(20),
	@DateStart  datetime ,
	@DateEnd    datetime ,	
    @UserName   nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyID, '' as CompanyName, '' as ProductID, '' as ProjectName, '' as RDate, '' as ReceivedID, '' as UnitNumber, '' as CustomerName, 
	'' as Method, '' as Amount, '' as Number, '' as BankName, '' as RV, '' as PI, '' as DepositDate, '' as BankAccount from ReportTemplate
END
GO

exec AP2SP_RP_FI_025