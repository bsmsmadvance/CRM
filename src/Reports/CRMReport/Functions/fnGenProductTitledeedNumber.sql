SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--SELECT * FROM [ICON_EntForms_AgreementPeriod]
-- SELECT dbo.fnGenLateDownPaymentPeriod('PKBBBBBBBBCO00001');
-- ใช้ในการสร้างข้อความแสดงรายการที่ดินโครงการ เช่น 814, 2228, 2229, 3578, 17951, 26887-26890, 28314, 28711, 34132, 34133, 34328, 34329

ALTER FUNCTION [dbo].[fnGenProductTitledeedNumber]
(
	@ProductID nvarchar(30)
)
RETURNS nvarchar(300)
AS
BEGIN

	DECLARE @result nvarchar(300);
	
	DECLARE @v TABLE(ProductTitledeedNumber varchar(255));

	INSERT INTO @v
		SELECT	CAST(TitleDeedNo AS varchar(255))
		FROM	[PRJ].[Address]
		WHERE	ProjectID = @ProductID 
				AND (TitleDeedNo IS NOT NULL) AND (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (ProjectAddressTypeMasterCenterID, 'Key') ) IN (0,2)
		ORDER BY TitleDeedNo ASC;

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 ProductTitledeedNumber FROM @v);	

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN @result;
	  END

	DELETE FROM @v WHERE ProductTitledeedNumber=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp varchar(255);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 ProductTitledeedNumber FROM @v);
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE ProductTitledeedNumber=@tmp;
	  END

	RETURN @result;

END

GO
