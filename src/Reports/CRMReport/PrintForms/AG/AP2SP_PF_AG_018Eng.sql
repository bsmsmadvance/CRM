
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_018Eng
    @ContractNumber NVARCHAR(4000)
AS
BEGIN
	SELECT '' as CompanyID, '' as CompanyName, '' as LetterHeadNumber, '' as DateHeader, '' as CustomerName, '' as Address1, '' as Address2, '' as ContractDate,
	'' as ContractNumber, '' as Notification1, '' as Notification2, '' as ProjectName, '' as UnitNumber, '' as Approver_Name, '' as Approver_Code, '' as Approver_Position from ReportTemplate
END
GO

exec AP2SP_PF_AG_018Eng