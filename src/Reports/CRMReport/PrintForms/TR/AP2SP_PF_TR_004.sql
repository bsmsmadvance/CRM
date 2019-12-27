
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_004
	@TransferNumber nvarchar(50)
AS
BEGIN
	SELECT '' as TaxID1, '' as TaxID2, '' as TaxID3, '' as TaxID4, '' as TaxID5, '' as TaxID, '' as CompanyNameThai, '' as BuildingThai, '' as AddressThai,
	'' as Soi, '' as Road, '' as SubDistrict, '' as District, '' as Province, '' as PostCode, '' as Telephone, '' as CurrentYear, '' as Date, '' as BusinessTax,
	'' as LocalTax, '' as UnitNumber, '' as Project, '' as TransferNumber, '' as TransferDate, '' as SellingPrice, '' as BusinessTaxPercent, '' as AttornyNameTransfer,
	'' as TaxValue, '' as LoanAccept, '' as BankFee, '' as BankName from ReportTemplate
END
GO

exec AP2SP_PF_TR_004