
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_010
    @ContractNumber NVARCHAR(20)
AS
BEGIN
	SELECT '' as ContractDay, '' as ContractMonth, '' as ContractYear, '' as CustomerName, '' as ProductID, '' as ProjectName, '' as ContractNumber, '' as UnitNumber,
	'' as DisplayName, '' as PreTransferDate from ReportTemplate
END
GO

exec AP2SP_PF_TR_010