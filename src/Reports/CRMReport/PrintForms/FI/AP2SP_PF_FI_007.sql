﻿
CREATE OR ALTER PROCEDURE AP2SP_PF_FI_007
	@ContractNumber nvarchar(50)
AS
BEGIN
	SELECT '' as CustomerName, '' as Address1, '' as Address2, '' as CompanyName, '' as AddressCompany, '' as Tel, '' as ProjectName, '' as UnitNumber, '' as AccountName,
	'' as RCreditOwner, '' as BankName, '' as AdBankName, '' as CreditNumber, '' as Period, '' as StatusDate from ReportTemplate
END
GO

exec AP2SP_PF_FI_007