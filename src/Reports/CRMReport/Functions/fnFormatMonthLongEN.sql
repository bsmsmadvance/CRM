SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fnFormatMonthLongEN]
(
	@incomingMonth datetime
)

	RETURNS nvarchar(50)

AS BEGIN
	DECLARE @convertMonth nvarchar(50)
	SET @convertMonth = (select CASE 
								WHEN MONTH(@incomingMonth) = 1 then 'January'
								WHEN MONTH(@incomingMonth) = 2 then 'February'  
								WHEN MONTH(@incomingMonth) = 3 then 'March' 
								WHEN MONTH(@incomingMonth) = 4 then 'April' 
								WHEN MONTH(@incomingMonth) = 5 then 'May' 
								WHEN MONTH(@incomingMonth) = 6 then 'June' 
								WHEN MONTH(@incomingMonth) = 7 then 'July' 
								WHEN MONTH(@incomingMonth) = 8 then 'August' 
								WHEN MONTH(@incomingMonth) = 9 then 'September' 
								WHEN MONTH(@incomingMonth) = 10 then 'October' 
								WHEN MONTH(@incomingMonth) = 11 then 'November' 
								else 'December'
						END);
	RETURN @convertMonth
END

GO
