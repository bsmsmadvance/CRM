
CREATE OR ALTER PROCEDURE AP2SP_RP_LC_024
	@ProductID nvarchar(50),
	@DateStart datetime,-- วันที่ทำจอง
	@DateEnd   datetime,-- วันที่ทำจอง
	@Username nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyNameThai, '' as ProductID, '' as Project, '' as PromotionID, '' as PromotionStartDate, '' as PromotionEndDate, '' as ItemID, 
	'' as PromotionDescription, '' as PricePerUnit, '' as Amount, '' as Total from ReportTemplate
END
GO

exec AP2SP_RP_LC_024