
--ยกเลิกสัญญาและโอนค่างวดภาษาอังกฤษ
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_007Eng
	 @HistoryID nvarchar(40)
	 , @ContractNumber nvarchar(20)
AS
BEGIN
	SELECT '' as CompanyName, '' as OperateDate, '' as AttornyName1, '' as CompanyFullAddress, '' as CustomerName, '' as CurrentAddress, '' as Moo, '' as Soi, '' as Road, '' as SubDistrict, '' as District, '' as Province, '' as ProductType,
	'' as ContractNUmber, '' as ContractDate, '' as ProjectName, '' as UnitNumber, '' as Paid, '' as PaidText, '' as SuspenseAmount, '' as SuspenseAmountText, '' as ProductTypeNew, '' as ContractNumberNew, '' as ContractDateNew, 
	'' as ProjectNameNew, '' as UnitNumberNew from ReportTemplate
END
GO

exec AP2SP_PF_AG_007Eng