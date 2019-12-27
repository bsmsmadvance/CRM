SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ใช้ในการหาข้้อมูลของ SubDistrict
CREATE FUNCTION [dbo].[fn_GetSubDistrictDetailFromFieldName]
(
	@SubDistrictID nvarchar(50),
	@FieldName nvarchar(50)
)
RETURNS nvarchar(100)
AS
BEGIN

	DECLARE @result NVARCHAR(100);
	DECLARE @SubDistrict TABLE(DistrictID NVARCHAR(50), NameTH NVARCHAR(50), NameEN NVARCHAR(50), PostalCode NVARCHAR(10));

	INSERT INTO @SubDistrict
		SELECT	DistrictID =  DistrictID
                , NameTH = NameTH
                , NameEN = NameEN
                , PostalCode = PostalCode
		FROM	MST.SubDistrict
		WHERE	ID = @SubDistrictID 

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = CASE 
                    WHEN @FieldName = 'DistrictID' THEN  (SELECT DistrictID FROM @SubDistrict)
                    WHEN @FieldName = 'NameTH' THEN  (SELECT NameTH FROM @SubDistrict)
                    WHEN @FieldName = 'NameEN' THEN  (SELECT NameEN FROM @SubDistrict)
                    WHEN @FieldName = 'PostalCode' THEN  (SELECT PostalCode FROM @SubDistrict)
                  END

	RETURN @result;

END














GO
