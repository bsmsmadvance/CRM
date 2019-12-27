SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- 
-- SELECT dbo.[fnGenUnitLandNumber]('A7A5930F-56B7-42C3-9B86-AC1E27FFA273')

-- ใช้ในการสร้างข้อความแสดงรายการเลขที่ดิน โดยใช้คอมม่าคั่น
ALTER FUNCTION [dbo].[fnGenUnitLandNumber]
(
	@UnitID nvarchar(50)
)
RETURNS varchar(500)
AS
BEGIN

--	SELECT * FROM ICON_EntForms_Titledeeddetail

	DECLARE @result varchar(500);
	
	DECLARE @v TABLE(LandNo varchar(100));

	INSERT INTO @v
		SELECT	LandNo
		FROM	[PRJ].[TitledeedDetail]
		WHERE	 UnitID = @UnitID
				AND LandNo IS NOT NULL
		ORDER BY LandNo ASC;

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 LandNo FROM @v);	

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN @result;
	  END

	DELETE FROM @v WHERE LandNo=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp varchar(500);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 LandNo FROM @v);
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE LandNo=@tmp;
	  END

	RETURN @result;

END
s
GO
