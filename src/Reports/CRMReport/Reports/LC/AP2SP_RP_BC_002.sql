
CREATE OR ALTER PROCEDURE AP2SP_RP_BC_002
	 @ProductID  nvarchar(20)
	, @UnitNumber nvarchar(50)
	, @DateStart datetime
	, @DateEnd   datetime
	, @UserName nvarchar(150)
AS
BEGIN
	SELECT '' as ProductID, '' as ProductName, '' as UnitNumber, '' as CustomerName, '' as Phone, '' as SellingPrice, '' as StatusUnit from ReportTemplate
END
GO

exec AP2SP_RP_BC_002