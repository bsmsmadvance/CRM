SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
	Funcion: NumberText (แปลงตัวเลขเป็นข้อความภาษาไทย)
	Author: Boonserm Sangkarnnok
	CreateDate: 2019-02-14
	ใช้ได้กับตัวเลขไม่เกิน  9 แสนล้าน (13หลัก)
*/

CREATE FUNCTION [dbo].[fnTH_NumberText]
(
	@MoneyAmt MONEY
)
RETURNS NVARCHAR(500)
AS
BEGIN
	DECLARE @MoneyText nvarchar(500),@txt nvarchar(500), @len int, @temp nvarchar(500),@position int;
	DECLARE @tMoney TABLE(nOrder int IDENTITY, MonText char(1));

	SET @MoneyText = CAST(@MoneyAmt AS NVARCHAR(500));
	SET @len = LEN(@MoneyText);
	SET @temp = '';
	SET @txt = '';
	SET @position = 1;

	WHILE @position <= @len
	 BEGIN
		INSERT INTO @tMoney
			SELECT SUBSTRING(@MoneyText, @position, 1);
	   
		SET @position = @position + 1
	 END


	IF (@MoneyAmt = 0)
		SET @temp = 'ศูนย์';
	ELSE IF (@MoneyAmt = 1)
		SET @temp = 'หนึ่ง';
	ELSE IF (@len = 1)
		SELECT @temp = [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = 1));
	ELSE IF (@len > 1)
	  BEGIN 
		IF (@len > 10)
		  BEGIN
			IF (@len > 14 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 14) <> '0') 
				SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 14)) + 'แสน';
			IF (@len > 13 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 13) <> '0') 
				SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 13)) + 'หมื่น';
			IF (@len > 12 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 12) <> '0') 
				SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 12)) + 'พัน';
			IF (@len > 11 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 11) <> '0') 
				SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 11)) + 'ร้อย';

			SET @txt = @txt + [dbo].[fnBHT_TenText] ((SELECT MonText FROM @tMoney WHERE nOrder = @len - 10), (SELECT MonText FROM @tMoney WHERE nOrder = @len - 9)) + 'ล้าน';
		  END
		ELSE IF (@len = 10)
			SET @txt = [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = 1)) + 'ล้าน';

		IF (@len > 8 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 8) <> '0') 
			SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 8)) + 'แสน';
		IF (@len > 7 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 7) <> '0') 
			SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 7)) + 'หมื่น';
		IF (@len > 6 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 6) <> '0') 
			SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 6)) + 'พัน';
		IF (@len > 5 AND (SELECT MonText FROM @tMoney WHERE nOrder = @len - 5) <> '0') 
			SET @txt = @txt + [dbo].[fnBHT_OneText]((SELECT MonText FROM @tMoney WHERE nOrder = @len - 5)) + 'ร้อย';

		SET @temp = @txt + [dbo].[fnBHT_TenText] ((SELECT MonText FROM @tMoney WHERE nOrder = @len - 4), (SELECT MonText FROM @tMoney WHERE nOrder = @len - 3));

	  END

		SET @temp = @temp + [dbo].[fnTH_StangText](@MoneyAmt);

	RETURN @temp;
	           
END



GO
