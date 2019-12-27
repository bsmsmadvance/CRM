SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [dbo].[fnGenUnitRavang]
(
	@ProductID nvarchar(30),
	@UnitNumber nvarchar(30)
)
RETURNS varchar(700)
AS
BEGIN

--	SELECT * FROM ICON_EntForms_Titledeeddetail

	DECLARE @result varchar(700);
	
	DECLARE @v TABLE(LandPortionNumber varchar(200));

	INSERT INTO @v
		SELECT	LandPortionNumber
		FROM	[ICON_EntForms_Titledeeddetail]
		WHERE	ProductID = @ProductID 
				AND UnitNumber = @UnitNumber
				AND LandPortionNumber IS NOT NULL
		ORDER BY LandPortionNumber ASC;

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 LandPortionNumber FROM @v);	

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN @result;
	  END

	DELETE FROM @v WHERE LandPortionNumber=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp varchar(700);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 LandPortionNumber FROM @v);
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE LandPortionNumber=@tmp;
	  END

	RETURN @result;

END

















GO
