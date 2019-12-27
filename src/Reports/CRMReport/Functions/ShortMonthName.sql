SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION[dbo].[ShortMonthName]
(
	@Display char(2),
	@MM int
)
RETURNS nvarchar(20)
AS
BEGIN
	IF (UPPER(@Display) NOT IN ('TH', 'EN') OR NOT (@MM BETWEEN 1 AND 12)) RETURN ''

	IF (@Display = 'TH')
	BEGIN
		IF (@MM = 1) RETURN 'ม.ค.'
		IF (@MM = 2) RETURN 'ก.พ.'
		IF (@MM = 3) RETURN 'มี.ค.'
		IF (@MM = 4) RETURN 'เม.ย.'
		IF (@MM = 5) RETURN 'พ.ค.'
		IF (@MM = 6) RETURN 'มิ.ย.'
		IF (@MM = 7) RETURN 'ก.ค.'
		IF (@MM = 8) RETURN 'ส.ค.'
		IF (@MM = 9) RETURN 'ก.ย.'
		IF (@MM = 10) RETURN 'ต.ค.'
		IF (@MM = 11) RETURN 'พ.ย.'
		IF (@MM = 12) RETURN 'ธ.ค.'
	END
	ELSE IF (@Display = 'EN')
	BEGIN
		IF (@MM = 1) RETURN 'Jan'
		IF (@MM = 2) RETURN 'Feb'
		IF (@MM = 3) RETURN 'Mar'
		IF (@MM = 4) RETURN 'Apr'
		IF (@MM = 5) RETURN 'May'
		IF (@MM = 6) RETURN 'Jun'
		IF (@MM = 7) RETURN 'Jul'
		IF (@MM = 8) RETURN 'Aug'
		IF (@MM = 9) RETURN 'Sep'
		IF (@MM = 10) RETURN 'Oct'
		IF (@MM = 11) RETURN 'Nov'
		IF (@MM = 12) RETURN 'Dec'
	END

	RETURN '';
END









GO
