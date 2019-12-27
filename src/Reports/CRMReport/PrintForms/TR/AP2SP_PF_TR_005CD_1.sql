
--

CREATE OR ALTER PROCEDURE AP2SP_PF_TR_005CD_1
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as ContractNumber, '' as CustomerName1, '' as PersonCardID1, '' as CustomerName2, '' as PersonCardID2, '' as CustomerName3, '' as PersonCardID3, 
	'' as Age1, '' as Age2, '' as Age3 from ReportTemplate
END
GO

exec AP2SP_PF_TR_005CD_1