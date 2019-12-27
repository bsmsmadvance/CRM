
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_003_3
	@UnitNumber1 nvarchar(50)
    , @ProductID1 nvarchar(50)
AS
BEGIN
	SELECT '' as UnitNumber, '' as ProductID, '' as Period, '' as Payment from ReportTemplate
END
GO

exec AP2SP_PF_AG_003_3