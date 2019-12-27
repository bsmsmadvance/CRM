SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- SELECT dbo.[fnGenUnitTitledeedNumber]('A89BAE1E-A28A-4CEB-B191-0D20C4CA61E2')

-- ใช้ในการสร้างข้อความแสดงรายการเลขที่โฉนด โดยใช้คอมม่าคั่น
ALTER FUNCTION [dbo].[fnGenUnitTitledeedNumber]
(
	@UnitID NVARCHAR(50)
)
RETURNS varchar(500)
AS
BEGIN

	DECLARE @result varchar(500);
	
	DECLARE @v TABLE(TitledeedNo varchar(100));

	INSERT INTO @v
		SELECT	TitledeedNo
		FROM	 [PRJ].[TitledeedDetail]
		WHERE	@UnitID = UnitID
				 AND TitledeedNo IS NOT NULL
		ORDER BY TitledeedNo ASC;

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 TitledeedNo FROM @v);	

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN @result;
	  END

	DELETE FROM @v WHERE TitledeedNo=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp varchar(500);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 TitledeedNo FROM @v);
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE TitledeedNo=@tmp;
	  END

	RETURN @result;

END

GO
