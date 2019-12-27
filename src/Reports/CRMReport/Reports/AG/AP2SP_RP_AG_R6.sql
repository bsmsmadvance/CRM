
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_R6
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
	@SBUID		nvarchar(20),
	@DateStart  datetime ,
	@DateEnd    datetime ,	
	@DateStart2 datetime ,
	@DateEnd2   datetime ,	
    @UserName   nvarchar(150),
	@HomeType nvarchar(20),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as PType, '' as ProjectName, '' as TotalSellingPrice, '' as MiniPriceOld, '' as MiniPriceNew, '' as CashDiscount, '' as TFADiscount, '' as TFTDiscount, '' as BudgetOld, '' as BudgetNew, '' as ItemPrice, '' as PayPrice,
	'' as FGFDiscount, '' as TFBudgetOld, '' as TFBudgetNew, '' as TFItemPrice, '' as TFPayPrice from ReportTemplate
END
GO

exec AP2SP_RP_AG_R6