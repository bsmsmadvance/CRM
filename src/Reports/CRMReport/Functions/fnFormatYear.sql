SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION fnFormatYear
(
	@incomingDate DateTime
)

	RETURNS nvarchar(50)

AS BEGIN
	DECLARE @convertYear nvarchar(50)
	SET @convertYear = (YEAR(@incomingDate) + 543)
	RETURN @convertYear
END