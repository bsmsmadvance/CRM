SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- SELECT dbo.[fnGenUnitLandSurveyArea]('10191','H05')

-- ใช้ในการสร้างข้อความแสดงรายการหน้าสำรวจ โดยใช้คอมม่าคั่น
ALTER FUNCTION [dbo].[fnGenUnitLandSurveyArea]
(
	@ProductID nvarchar(30),
	@UnitNumber nvarchar(30)
)
RETURNS varchar(500)
AS
BEGIN

	DECLARE @result varchar(500);
	DECLARE @UnitID varchar(100);
	DECLARE @ProjectID varchar(100);
	DECLARE @v TABLE(LandSurveyArea varchar(100));

	SET @UnitID = (select Top 1 u.ID from PRJ.Unit u where u.UnitNo = @UnitNumber);
	SET @ProjectID = (select Top 1 p.ID from PRJ.Project p where p.ProjectNo = @ProductID);

	INSERT INTO @v
		SELECT	LandSurveyArea
		FROM	PRJ.TitledeedDetail
		WHERE	ProjectID = @ProjectID 
				AND UnitID = @UnitID
				AND LandSurveyArea IS NOT NULL
		ORDER BY LandSurveyArea ASC;

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 LandSurveyArea FROM @v);	

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN @result;
	  END

	DELETE FROM @v WHERE LandSurveyArea=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp varchar(500);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 LandSurveyArea FROM @v);
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE LandSurveyArea=@tmp;
	  END

	RETURN @result;

END














GO
