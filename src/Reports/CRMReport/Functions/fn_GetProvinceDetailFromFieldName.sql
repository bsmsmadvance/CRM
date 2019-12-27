SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ใช้ในการหาข้้อมูลของ Province
CREATE FUNCTION [dbo].[fn_GetProvinceDetailFromFieldName]
(
	@ProvinceID nvarchar(50),
	@FieldName nvarchar(50)
)
RETURNS nvarchar(100)
AS
BEGIN

	DECLARE @result NVARCHAR(100);
	DECLARE @Province TABLE(NameTH NVARCHAR(50), NameEN NVARCHAR(50));

	INSERT INTO @Province
		SELECT	NameTH = NameTH
                , NameEN = NameEN
		FROM	MST.Province
		WHERE	ID = @ProvinceID 

	SET @result = CASE 
                    WHEN @FieldName = 'NameTH' THEN  (SELECT NameTH FROM @Province)
                    WHEN @FieldName = 'NameEN' THEN  (SELECT NameEN FROM @Province)
                  END

	RETURN @result;

END














GO
