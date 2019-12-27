
--

CREATE OR ALTER PROCEDURE AP2SP_PF_LC_006
	@TransferNumber nvarchar(50)
AS
BEGIN
	SELECT '' as CustomerName, '' as CustomerTel, '' as ProjectName, '' as HomeNumber, '' as RoomNumber, '' as TitleDeedNumber, '' as TransferDate, '' as StandardArea,
	'' as LCName, '' as LC_Tel_No, '' as LC_Fax_No, '' as TransferEmp, '' as TransferEmp_Tel_No, '' as FirstFundsRate, '' as FirstFunds, '' as PublicAdvance,
	'' as PublicFundRate, '' as PublicFundMoney, '' as Baht from ReportTemplate
END
GO

exec AP2SP_PF_LC_006