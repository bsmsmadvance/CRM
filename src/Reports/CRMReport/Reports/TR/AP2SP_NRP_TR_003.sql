
CREATE OR ALTER PROCEDURE AP2SP_NRP_TR_003
	@ProductID nvarchar(50),
--    @CompanyID nvarchar(20),
    @Type nvarchar(10),
    @Month nvarchar(10),
    @Year nvarchar(10),
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as ProjectName, '' as [Group], '' as BankName, '' as Payment, '' as TotalHomePrice from ReportTemplate
END
GO

exec AP2SP_NRP_TR_003