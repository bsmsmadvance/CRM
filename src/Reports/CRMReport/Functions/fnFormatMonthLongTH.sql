SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fnFormatMonthLongTH]
(
	@incomingMonth datetime
)

	RETURNS nvarchar(50)

AS BEGIN
	DECLARE @convertMonth nvarchar(50)
	SET @convertMonth = (select CASE 
								WHEN MONTH(@incomingMonth) = 1 then N'มกราคม'
								WHEN MONTH(@incomingMonth) = 2 then N'กุมภาพันธ์'  
								WHEN MONTH(@incomingMonth) = 3 then N'มีนาคม' 
								WHEN MONTH(@incomingMonth) = 4 then N'เมษายน' 
								WHEN MONTH(@incomingMonth) = 5 then N'พฤกษภาคม' 
								WHEN MONTH(@incomingMonth) = 6 then N'มิถุนายน' 
								WHEN MONTH(@incomingMonth) = 7 then N'กรกฏาคม' 
								WHEN MONTH(@incomingMonth) = 8 then N'สิงหาคม' 
								WHEN MONTH(@incomingMonth) = 9 then N'กันยายน' 
								WHEN MONTH(@incomingMonth) = 10 then N'ตุลาคม' 
								WHEN MONTH(@incomingMonth) = 11 then N'พฤศจิกายน' 
								else N'ธันวาคม'
						END);
	RETURN @convertMonth
END

GO
