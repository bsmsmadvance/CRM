SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ใช้ในการหาข้้อมูลของ District
CREATE FUNCTION [dbo].[fn_GetDistrictDetailFromFieldName]
(
	@DistrictID nvarchar(50),
	@FieldName nvarchar(50)
)
RETURNS nvarchar(100)
AS
BEGIN

	DECLARE @result NVARCHAR(100);
	DECLARE @District TABLE(ProvinceID NVARCHAR(50), NameTH NVARCHAR(50), NameEN NVARCHAR(50), PostalCode NVARCHAR(10));

	INSERT INTO @District
		SELECT	ProvinceID =  ProvinceID
                , NameTH = NameTH
                , NameEN = NameEN
                , PostalCode = PostalCode
		FROM	MST.District
		WHERE	ID = @DistrictID 

	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = CASE 
                    WHEN @FieldName = 'ProvinceID' THEN  (SELECT ProvinceID FROM @District)
                    WHEN @FieldName = 'NameTH' THEN  (SELECT NameTH FROM @District)
                    WHEN @FieldName = 'NameEN' THEN  (SELECT NameEN FROM @District)
                    WHEN @FieldName = 'PostalCode' THEN  (SELECT PostalCode FROM @District)
                  END

	RETURN @result;

END














GO
