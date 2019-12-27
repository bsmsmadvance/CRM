
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_006H
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as CompanyName, '' as TaxID, '' as AddressCompany, '' as Telephone, '' as TransferName, '' as PersonCardID, '' as CurrentAddress, '' as Moo, '' as Village,
	'' as Soi, '' as Road, '' as SubDistrict, '' as District, '' as Province_Code, '' as TelephoneCus, '' as TypeHome, '' as ContractNumber, '' as ContractDate,
	'' as TitledeedNumber, '' as LandNumber, '' as LandSurveyArea, '' as LandSubDistrict, '' as LandDistrict, '' as LandProvince, '' as Project, '' as AddressProduct,
	'' as TransferDate, '' as NetSalePrice, '' as NetSalePriceText, '' as AddressCus1, '' as AddressCus2 from ReportTemplate
END
GO

exec AP2SP_PF_TR_006H