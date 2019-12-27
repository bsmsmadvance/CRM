CREATE OR ALTER FUNCTION fnFormatDateShortTH
(
	@incomingDate DateTime
)

	RETURNS nvarchar(50)

AS BEGIN
	DECLARE @convertDateShort nvarchar(50)
	DECLARE @convertDay nvarchar(50)
	DECLARE @convertMonth nvarchar(50)
	DECLARE @convertYear nvarchar(50)
	SET @convertDay =  DAY(@incomingDate);
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
								else 'ธันวาคม'
						END);
	SET @convertYear = (YEAR(@incomingDate) + 543)
	SET @convertDateShort = @convertDay + '-' + @convertMonth + '-' + SUBSTRING(@convertYear,3,2)
	RETURN @convertDateShort
END