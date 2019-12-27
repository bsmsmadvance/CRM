SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO


-- เป็นฟังก์ชั่นย่อยของ BahtText
CREATE FUNCTION [dbo].[fnBHT_TenText]
(
	@c1 char(1),
	@c0 char(1)
)
RETURNS nvarchar(50)
WITH ENCRYPTION
AS
BEGIN
	DECLARE @result nvarchar(50);

IF (@c1 = '0')
  BEGIN
	IF (@c0 = '0')
		RETURN '';
	ELSE IF (@c0 = '1')
		RETURN 'เอ็ด';
	ELSE
		RETURN [dbo].[fnBHT_OneText](@c0);
  END
ELSE
  BEGIN
	IF (@c1 = '1')
		SET @result = 'สิบ';
	ELSE IF (@c1 = '2')
		SET @result = 'ยี่สิบ';
	ELSE
		SET @result = [dbo].[fnBHT_OneText](@c1) + 'สิบ';

	IF (@c0 = '0')
		RETURN @result;
	ELSE IF (@c0 = '1')
		RETURN @result + 'เอ็ด';
	ELSE
		RETURN @result + (SELECT [dbo].[fnBHT_OneText](@c0));
  END

	RETURN '';
END
GO
