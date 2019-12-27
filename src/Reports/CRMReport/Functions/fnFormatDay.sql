SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION fnFormatDay
(
	@incomingDate DateTime
)

	RETURNS nvarchar(50)

AS BEGIN
	DECLARE @convertDay nvarchar(50)
	SET @convertDay =  (select DAY(@incomingDate));
	RETURN @convertDay
END