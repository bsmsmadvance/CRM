
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_003
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as ContractNumber, '' as CompanyID, '' as CompanyName, '' as TaxID, '' as AttornyName1, '' as TitledeedNumber, '' as ProjectName, '' as UnitNumber,
	'' as UnitNumberHeader, '' as LandSubDistrict, '' as LandDistrict, '' as LandProvince, '' as TransferDate, '' as NetSalePrice, '' as LandEstimatePrice, '' as IncomeTax,
	'' as BusinessTax, '' as LocalTax, '' as SumMoney, '' as AttornyIssueDate, '' as LandOfficeID, '' as ProductID from ReportTemplate
END
GO

exec AP2SP_PF_TR_003