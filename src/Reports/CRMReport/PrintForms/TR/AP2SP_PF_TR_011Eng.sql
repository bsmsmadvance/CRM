
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_011Eng
    @ContractNumber NVARCHAR(20)
AS
BEGIN
	SELECT '' as CompanyName, '' as ContractTransferDate, '' as UtilityAttornyName, '' as Company_Address, '' as Company_Floor, '' as Company_Building, '' as Company_Road,
	'' as Company_SubDistrict, '' as Company_District, '' as Company_Province, '' as CustomerName, '' as Customer_Address, '' as Customer_Moo, '' as Customer_Road,
	'' as Customer_Soi, '' as Customer_SubDistrict, '' as Customer_District, '' as Customer_Province, '' as ContractNumber, '' as ContractDate, '' as ProjectName,
	'' as UnitNumber, '' as Floor, '' as SalePrice, '' as SalePriceText, '' as PreTransferDate, '' as TransferDate, '' as PreTransferTime, '' as LandOfficeName, '' as TransferFee,
	'' as Electric, '' as PublicFundYear, '' as PublicFund, '' as Fund, '' as Stamp, '' as PreTransferFee, '' as ForType, '' as TransferFeeText, '' as ElectricText,
	'' as PublicFundText, '' as FundText, '' as StampText, '' as PreTransferFeeText from ReportTemplate
END
GO

exec AP2SP_PF_TR_011Eng