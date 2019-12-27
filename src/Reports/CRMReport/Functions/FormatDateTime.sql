SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- SELECT dbo.FormatDateTime('TH', 'dd MMMM yyyy','2006-09-20')
CREATE FUNCTION[dbo].[FormatDateTime]
(
	@Region VarChar(2), --a.g. 'TH', 'EN'
	@Display VarChar(20), --a.g. dd/MM/yy dd/MM/yyyy, d MMM yyyy, d MMMM yyyy
	@Date DateTime
)
RETURNS VarChar(20)
AS
BEGIN
	IF (@Date IS NULL) RETURN NULL;
	IF (@Region NOT IN ('TH', 'EN')) SET @Region = 'EN';

	DECLARE @Day VarChar(2), @Month Int, @Year VarChar(4);
	DECLARE @nMonth Int, @nYear Int, @Slash1 VarChar(1), @Slash2 VarChar(1), @Loop Bit, @Idx Int;
	DECLARE @MonthName VarChar(20);

	SELECT	@nMonth = 0, @nYear = 0, @Loop = 1;
	SELECT	@Month = MONTH(@Date);
	SELECT  @Year = CAST(CASE @Region WHEN 'TH' THEN YEAR(@Date) + 543 ELSE YEAR(@Date) END AS VarChar(4));
	SELECT	@Display = UPPER(@Display);

	DECLARE @yIdx Int, @mIdx Int, @dIdx Int;
	SELECT @yIdx = PATINDEX('%Y%',	@Display),
		@mIdx = PATINDEX('%M%',	@Display), @dIdx = PATINDEX('%D%', @Display);

	-- Find Day
	IF PATINDEX('%d%', @Display) > 0
		SET @Day = CAST(DAY(@Date) AS VarChar(2));
	ELSE
		SET @Day = '';

	--Count 'M' in format
	WHILE (@Loop = 1)
	BEGIN
		SET @Idx = CHARINDEX('M', @Display);
		IF (@Idx = 0) 
			SET @Loop = 0;
		ELSE
		BEGIN
			SET @nMonth = @nMonth + 1;
			SET @Display = STUFF(@Display, @Idx, 1, '');
		END
	END

	--Count 'Y' in format
	SET @Loop = 1;
	WHILE (@Loop = 1)
	BEGIN
		SET @Idx = CHARINDEX('Y', @Display);
		IF (@Idx = 0) 
			SET @Loop = 0;
		ELSE
		BEGIN
			SET @nYear = @nYear + 1;
			SET @Display = STUFF(@Display, @Idx, 1, '');
		END
	END

	-- Find Slash '/'
	IF PATINDEX('%/%', @Display) > 0
		SELECT @Slash1 = '/', @Slash2 = '/';
	ELSE
		SELECT @Slash1 = CASE @Day WHEN '' THEN '' ELSE ' ' END, 
			@Slash2 = CASE @nMonth WHEN 0 THEN '' ELSE ' ' END;

	IF (@nMonth = 0)
		SET @MonthName = ''
	ELSE IF (@nMonth = 1)
		SET @MonthName = STR(@Month, 2);
	ELSE IF (@nMonth = 2)
		SET @MonthName = REPLACE(STR(@Month, 2), ' ', '0');
	ELSE IF (@nMonth = 3)
		SET @MonthName = dbo.ShortMonthName(@Region, @Month);
	ELSE IF (@nMonth = 4)
		SET @MonthName = dbo.MonthName(@Region, @Month);

	IF (@nYear = 0)
		SET @Year = '';
	ELSE IF (@nYear = 1 OR @nYear = 2) 
		SET @Year = RIGHT(@Year, 2);


	IF (@yIdx > @mIdx AND @mIdx > @dIdx)
		RETURN @Day + @Slash1 + @MonthName + @Slash2 + @Year;	
	ELSE IF (@dIdx > @mIdx AND @mIdx > @yIdx)
		RETURN @Year + @Slash1 + @MonthName + @Slash2 + @Day;	
	ELSE IF (@mIdx > @yIdx)
		RETURN @Year + @Slash1 + @MonthName;	
	ELSE IF (@yIdx > @mIdx)
		RETURN @MonthName + @Slash1 + @Year;	
	
	RETURN @Day + @Slash1 + @MonthName + @Slash2 + @Year;	
END




GO
