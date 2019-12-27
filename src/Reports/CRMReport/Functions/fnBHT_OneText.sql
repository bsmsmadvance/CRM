SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO

-- เป็นฟังก์ชั่นย่อยของ BahtText
CREATE FUNCTION [dbo].[fnBHT_OneText]
(
	@c char(1)
)
RETURNS nvarchar(10)
WITH ENCRYPTION
AS
BEGIN	
	RETURN CASE @c WHEN 0 THEN ''			
			WHEN '1' THEN 'หนึ่ง'
			WHEN '2' THEN 'สอง'
			WHEN '3' THEN 'สาม'
			WHEN '4' THEN 'สี่'
			WHEN '5' THEN 'ห้า'
			WHEN '6' THEN 'หก'
			WHEN '7' THEN 'เจ็ด'
			WHEN '8' THEN 'แปด'
			WHEN '9' THEN 'เก้า' END	
END
GO
