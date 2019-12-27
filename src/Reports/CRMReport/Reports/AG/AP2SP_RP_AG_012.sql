
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_012
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
	@SBUID		nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,	
	@DateStart2 datetime ,
	@DateEnd2   datetime ,	
    @UserName   nvarchar(150)
AS
BEGIN
	SELECT '' as ProductID, '' as ProjectName, '' as UnitNumber, '' as SuggestionPrice, '' as ImportDate, '' as TotalSellingPrice, 
	'' as PromotionPrice from ReportTemplate
END
GO

exec AP2SP_RP_AG_012