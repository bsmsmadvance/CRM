SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION[dbo].[MonthName]
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
		IF (@MM = 1) RETURN 'มกราคม'
		IF (@MM = 2) RETURN 'กุมภาพันธ์'
		IF (@MM = 3) RETURN 'มีนาคม'
		IF (@MM = 4) RETURN 'เมษายน'
		IF (@MM = 5) RETURN 'พฤษภาคม'
		IF (@MM = 6) RETURN 'มิถุนายน'
		IF (@MM = 7) RETURN 'กรกฎาคม'
		IF (@MM = 8) RETURN 'สิงหาคม'
		IF (@MM = 9) RETURN 'กันยายน'
		IF (@MM = 10) RETURN 'ตุลาคม'
		IF (@MM = 11) RETURN 'พฤศจิกายน'
		IF (@MM = 12) RETURN 'ธันวาคม'
	END
	ELSE IF (@Display = 'EN')
	BEGIN
		IF (@MM = 1) RETURN 'January'
		IF (@MM = 2) RETURN 'Febuary'
		IF (@MM = 3) RETURN 'March'
		IF (@MM = 4) RETURN 'April'
		IF (@MM = 5) RETURN 'May'
		IF (@MM = 6) RETURN 'June'
		IF (@MM = 7) RETURN 'July'
		IF (@MM = 8) RETURN 'August'
		IF (@MM = 9) RETURN 'September'
		IF (@MM = 10) RETURN 'October'
		IF (@MM = 11) RETURN 'November'
		IF (@MM = 12) RETURN 'December'
	END

	RETURN '';
END

GO
