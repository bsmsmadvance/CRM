SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION fnFormatMonth
(
	@incomingDate DateTime
)

	RETURNS nvarchar(50)

AS BEGIN
	DECLARE @convertMonth nvarchar(50)
	SET @convertMonth = (select CASE 
								WHEN MONTH(@incomingDate) = 1 then N'มกราคม'
								WHEN MONTH(@incomingDate) = 2 then N'กุมภาพันธ์'  
								WHEN MONTH(@incomingDate) = 3 then N'มีนาคม' 
								WHEN MONTH(@incomingDate) = 4 then N'เมษายน' 
								WHEN MONTH(@incomingDate) = 5 then N'พฤกษภาคม' 
								WHEN MONTH(@incomingDate) = 6 then N'มิถุนายน' 
								WHEN MONTH(@incomingDate) = 7 then N'กรกฏาคม' 
								WHEN MONTH(@incomingDate) = 8 then N'สิงหาคม' 
								WHEN MONTH(@incomingDate) = 9 then N'กันยายน' 
								WHEN MONTH(@incomingDate) = 10 then N'ตุลาคม' 
								WHEN MONTH(@incomingDate) = 11 then N'พฤศจิกายน' 
								else N'ธันวาคม'
						END);
	RETURN @convertMonth
END