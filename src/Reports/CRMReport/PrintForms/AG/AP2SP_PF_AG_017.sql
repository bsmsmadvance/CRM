
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_017
    @ContractNumber NVARCHAR(4000)
AS
BEGIN
	SELECT '' as CompanyID, '' as CompanyName, '' as NotificationDate, '' as LetterHeadNumber, '' as CustomerName, '' as Address1, '' as Address2, '' as ContractDate,
	'' as ProductID, '' as ProjectName, '' as UnitNumber, '' as DateInLetter, '' as TimeInLetter, '' as LandOfficeID, '' as LandOfficeName, '' as Tel, '' as ContractNumber,
	'' as Project, '' as Unitnumber, '' as Approver_Name, '' as Approver_Code, '' as Approver_Position from ReportTemplate 
END
GO

exec AP2SP_PF_AG_017