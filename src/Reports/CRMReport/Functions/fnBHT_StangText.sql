SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO


-- เป็นฟังก์ชั่นย่อยของ BahtText
CREATE FUNCTION [dbo].[fnBHT_StangText]
(
	@d money
)
RETURNS nvarchar(50)
WITH ENCRYPTION
AS
BEGIN
	DECLARE @MoneyText nvarchar(500), @len int, @position int, @c1 char(1), @c2 char(1);
	DECLARE @tMoney TABLE(nOrder int IDENTITY, MonText nvarchar(50));

	SET @MoneyText = CAST(@d AS NVARCHAR(500));
	SET @len = LEN(@MoneyText);
	SET @position = 1;

	WHILE @position <= @len
	 BEGIN
		INSERT INTO @tMoney
			SELECT SUBSTRING(@MoneyText, @position, 1);
	   
		SET @position = @position + 1
	 END


	IF (@len < 4)
		RETURN '';
	ELSE
	  BEGIN
		SELECT @c1 = MonText FROM @tMoney WHERE nOrder = @len;
		SELECT @c2 = MonText FROM @tMoney WHERE nOrder = @len - 1;

		IF (@c1 = '0' AND @c2 = '0')
			RETURN 'บาทถ้วน';
		ELSE IF (@c2 = '0' AND @c1 = '1')
			RETURN 'บาทหนึ่งสตางค์';
		ELSE IF FLOOR(@d) = 0
            RETURN [dbo].[fnBHT_TenText](@c2, @c1) + 'สตางค์';
		ELSE
			RETURN 'บาท' + [dbo].[fnBHT_TenText](@c2, @c1) + 'สตางค์';
	  END;

	RETURN '';

END

GO
