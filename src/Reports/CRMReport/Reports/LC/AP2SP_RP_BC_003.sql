
CREATE OR ALTER PROCEDURE AP2SP_RP_BC_003
	 @ProductID  nvarchar(20)
	, @UserName nvarchar(150)
AS
BEGIN
	SELECT '' as ProductID, '' as ProductName, '' as ModelHomeThai, '' as TotalUnit, '' as BookingUnit, '' as TransferUnit from ReportTemplate
END
GO

exec AP2SP_RP_BC_003