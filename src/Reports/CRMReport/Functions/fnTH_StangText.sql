SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--SELECT [dbo].[fnTH_StangText] (100.50);
--SELECT [dbo].[fnTH_StangText] (100.00);
--SELECT [dbo].[fnTH_StangText] (100.05);

CREATE FUNCTION [dbo].[fnTH_StangText]
(
	@d money
)
RETURNS nvarchar(50)
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
			RETURN '';
		ELSE IF (@c1 = '0' AND @c2 <> '0')
			RETURN 'จุด' + [dbo].[fnTH_OneText](@c2);
		ELSE
			RETURN 'จุด' + [dbo].[fnTH_OneText](@c2) + '' + [dbo].[fnTH_OneText](@c1);
	  END

	RETURN '';

END



GO
