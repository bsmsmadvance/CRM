SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ใช้ในการหาข้้อมูลของ Country
CREATE FUNCTION [dbo].[fn_GetCountryDetailFromFieldName]
(
	@CountryID nvarchar(50),
	@FieldName nvarchar(50)
)
RETURNS nvarchar(100)
AS
BEGIN

	DECLARE @result NVARCHAR(100);
	DECLARE @Country TABLE(NameTH NVARCHAR(50), NameEN NVARCHAR(50), Code NVARCHAR(10));

	INSERT INTO @Country
		SELECT	NameTH = NameTH
                , NameEN = NameEN
                , Code = Code
		FROM	MST.Country
		WHERE	ID = @CountryID 

	SET @result = CASE 
                    WHEN @FieldName = 'NameTH' THEN  (SELECT NameTH FROM @Country)
                    WHEN @FieldName = 'NameEN' THEN  (SELECT NameEN FROM @Country)
                    WHEN @FieldName = 'Code' THEN  (SELECT Code FROM @Country)
                  END

	RETURN @result;

END














GO
