SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- 
-- SELECT dbo.[fnGenUnitLandBookPage]('PK0150')

-- ใช้ในการสร้างข้อความแสดงรายการเลขที่ดิน โดยใช้คอมม่าคั่น
CREATE FUNCTION [dbo].[fnGenUnitLandBookPage]
(
	@UnitID nvarchar(30)
)
RETURNS varchar(500)
AS
BEGIN

--	SELECT * FROM ICON_EntForms_Titledeeddetail

	DECLARE @result varchar(500);
	
	DECLARE @v TABLE(PageNo varchar(30));

	INSERT INTO @v
		SELECT	PageNo
		FROM	[PRJ].[TitledeedDetail]
		WHERE	UnitID = @UnitID
				AND PageNo IS NOT NULL
		ORDER BY PageNo ASC;

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 PageNo FROM @v);	

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN @result;
	  END

	DELETE FROM @v WHERE PageNo=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp varchar(500);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 PageNo FROM @v);
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE PageNo=@tmp;
	  END

	RETURN @result;

END














GO
