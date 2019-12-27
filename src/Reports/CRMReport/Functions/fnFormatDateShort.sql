SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fnFormatDateShort
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
								WHEN MONTH(@incomingDate) = 1 then '01'
								WHEN MONTH(@incomingDate) = 2 then '02'  
								WHEN MONTH(@incomingDate) = 3 then '03' 
								WHEN MONTH(@incomingDate) = 4 then '04' 
								WHEN MONTH(@incomingDate) = 5 then '05' 
								WHEN MONTH(@incomingDate) = 6 then '06' 
								WHEN MONTH(@incomingDate) = 7 then '07' 
								WHEN MONTH(@incomingDate) = 8 then '08' 
								WHEN MONTH(@incomingDate) = 9 then '09' 
								WHEN MONTH(@incomingDate) = 10 then '10' 
								WHEN MONTH(@incomingDate) = 11 then '11'
								else '12'
						END);
	SET @convertYear = YEAR(@incomingDate)
	SET @convertDateShort = @convertDay + '-' + @convertMonth + '-' + SUBSTRING(@convertYear,3,2)
	RETURN @convertDateShort
END
