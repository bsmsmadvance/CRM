SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--SELECT [dbo].[fnTH_OneText] ('5');

CREATE FUNCTION [dbo].[fnTH_OneText]
(
	@c char(1)
)
RETURNS nvarchar(50)
AS
BEGIN
	DECLARE @result nvarchar(50);
	DECLARE @tNum TABLE (nOrder char(1), NumText nvarchar(50));

	INSERT INTO @tNum(nOrder, NumText) VALUES ('0', 'ศูนย์');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('1', 'หนึ่ง');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('2', 'สอง');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('3', 'สาม');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('4', 'สี่');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('5', 'ห้า');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('6', 'หก');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('7', 'เจ็ด');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('8', 'แปด');
	INSERT INTO @tNum(nOrder, NumText) VALUES ('9', 'เก้า');

	SELECT @result = NumText FROM @tNum WHERE nOrder = @c;


	RETURN @result;
END



GO
