
CREATE OR ALTER PROCEDURE AP2SP_FollowCustomer_Revisit
	@ProductID nvarchar(50),
	@UserID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@Username nvarchar(50)
AS
BEGIN
	SELECT '' as VisitDate, '' as ProductCompare, '' as Probability, '' as FirstName, '' as SurName, '' as Contact_Tel, '' as Address, '' as LC, '' as Project, 
	'' as ModifyDate, '' as Budget, '' as Presentation, '' as ReasonByHome, '' as CompleteQuestionnaire,
	'' as StatusUpdate, '' as DateUpdate, '' as NextStep, '' as UnitNumber, '' as BuyReason, '' as Remark, '' as Income_1 from ReportTemplate
END
GO

exec AP2SP_FollowCustomer_Revisit