SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--SELECT [dbo].[Currency_ToWords]('3831160.00')

ALTER FUNCTION [dbo].[Currency_ToWords] (@Input NUMERIC(38, 2) -- Input number with as many as 18 digits
                                        )
RETURNS VARCHAR(8000)

/*
* Converts a integer number as large as 34 digits into the 
* equivalent words.  The first letter is capitalized.
*
* Attribution: Based on NumberToWords by Srinivas Sampath
*        as revised by Nick Barclay
*
* Example:
select dbo.udf_Num_ToWords (1234567890) + CHAR(10)
      +  dbo.udf_Num_ToWords (0) + CHAR(10)
      +  dbo.udf_Num_ToWords (123) + CHAR(10)
select dbo.udf_Num_ToWords(76543210987654321098765432109876543210)
 
DECLARE @i numeric (38,0)
SET @i = 0
WHILE @I <= 1000 BEGIN 
    PRINT convert (char(5), @i)  
            + convert(varchar(255), dbo.udf_Num_ToWords(@i)) 
    SET @I  = @i + 1 
END
*
* Published as the T-SQL UDF of the Week Vol 2 #9 2/17/03
****************************************************************/
AS
BEGIN
    DECLARE @Number NUMERIC(38, 0);
    SET @Number = @Input;
    DECLARE @Cents AS INT;
    SET @Cents = 100 * CONVERT(MONEY, (@Input - CONVERT(NUMERIC(38, 3), @Number)));
    DECLARE @inputNumber VARCHAR(38);
    DECLARE @NumbersTable TABLE (number CHAR(2), word VARCHAR(10));
    DECLARE @outputString VARCHAR(8000);
    DECLARE @length INT;
    DECLARE @counter INT;
    DECLARE @loops INT;
    DECLARE @position INT;
    DECLARE @chunk CHAR(3); -- for chunks of 3 numbers
    DECLARE @tensones CHAR(2);
    DECLARE @hundreds CHAR(1);
    DECLARE @tens CHAR(1);
    DECLARE @ones CHAR(1);

    IF (@Cents < 0)
        SET @Number = @Number - 1;


    IF @Number = 0 AND @Cents = 0
        RETURN 'Zero';

    -- initialize the variables
    SELECT @inputNumber = CONVERT(VARCHAR(38), @Number), @outputString = '', @counter = 1;
    SELECT @length = LEN(@inputNumber), @position = LEN(@inputNumber) - 2, @loops = LEN(@inputNumber) / 3;

    -- make sure there is an extra loop added for the remaining numbers
    IF LEN(@inputNumber) % 3 <> 0
        SET @loops = @loops + 1;

    -- insert data for the numbers and words
    INSERT INTO @NumbersTable
    SELECT '00', ''
    UNION ALL
    SELECT '01', 'One'
    UNION ALL
    SELECT '02', 'Two'
    UNION ALL
    SELECT '03', 'Three'
    UNION ALL
    SELECT '04', 'Four'
    UNION ALL
    SELECT '05', 'Five'
    UNION ALL
    SELECT '06', 'Six'
    UNION ALL
    SELECT '07', 'Seven'
    UNION ALL
    SELECT '08', 'Eight'
    UNION ALL
    SELECT '09', 'Nine'
    UNION ALL
    SELECT '10', 'Ten'
    UNION ALL
    SELECT '11', 'Eleven'
    UNION ALL
    SELECT '12', 'Twelve'
    UNION ALL
    SELECT '13', 'Thirteen'
    UNION ALL
    SELECT '14', 'Fourteen'
    UNION ALL
    SELECT '15', 'Fifteen'
    UNION ALL
    SELECT '16', 'Sixteen'
    UNION ALL
    SELECT '17', 'Seventeen'
    UNION ALL
    SELECT '18', 'Eighteen'
    UNION ALL
    SELECT '19', 'Nineteen'
    UNION ALL
    SELECT '20', 'Twenty'
    UNION ALL
    SELECT '30', 'Thirty'
    UNION ALL
    SELECT '40', 'Forty'
    UNION ALL
    SELECT '50', 'Fifty'
    UNION ALL
    SELECT '60', 'Sixty'
    UNION ALL
    SELECT '70', 'Seventy'
    UNION ALL
    SELECT '80', 'Eighty'
    UNION ALL
    SELECT '90', 'Ninety';

    WHILE @counter <= @loops
    BEGIN

        -- get chunks of 3 numbers at a time, padded with leading zeros
        SET @chunk = RIGHT('000' + SUBSTRING(@inputNumber, @position, 3), 3);

        IF @chunk <> '000'
        BEGIN
            SELECT @tensones = SUBSTRING(@chunk, 2, 2), @hundreds = SUBSTRING(@chunk, 1, 1), @tens = SUBSTRING(@chunk, 2, 1), @ones = SUBSTRING(@chunk, 3, 1);

            -- If twenty or less, use the word directly from @NumbersTable
            IF CONVERT(INT, @tensones) <= 20
                OR @ones = '0'
            BEGIN
                SET @outputString = (SELECT word FROM @NumbersTable WHERE @tensones = number) + CASE @counter
                                                                                                    WHEN 1 THEN '' -- No name
                                                                                                    WHEN 2 THEN ' Thousand '
                                                                                                    WHEN 3 THEN ' Million '
                                                                                                    WHEN 4 THEN ' Billion '
                                                                                                    WHEN 5 THEN ' Trillion '
                                                                                                    WHEN 6 THEN ' Quadrillion '
                                                                                                    WHEN 7 THEN ' Quintillion '
                                                                                                    WHEN 8 THEN ' Sextillion '
                                                                                                    WHEN 9 THEN ' Septillion '
                                                                                                    WHEN 10 THEN ' Octillion '
                                                                                                    WHEN 11 THEN ' Nonillion '
                                                                                                    WHEN 12 THEN ' Decillion '
                                                                                                    WHEN 13 THEN ' Undecillion '
                                                                                                ELSE ''
                                                                                                END + @outputString;
            END;
            ELSE
            BEGIN -- break down the ones and the tens separately

                SET @outputString = ' ' + (SELECT word FROM @NumbersTable WHERE @tens + '0' = number) + '-' + (SELECT word FROM @NumbersTable WHERE '0' + @ones = number) + CASE @counter
                                                                                                                                                                                WHEN 1 THEN '' -- No name
                                                                                                                                                                                WHEN 2 THEN ' Thousand '
                                                                                                                                                                                WHEN 3 THEN ' Million '
                                                                                                                                                                                WHEN 4 THEN ' Billion '
                                                                                                                                                                                WHEN 5 THEN ' Trillion '
                                                                                                                                                                                WHEN 6 THEN ' Quadrillion '
                                                                                                                                                                                WHEN 7 THEN ' Quintillion '
                                                                                                                                                                                WHEN 8 THEN ' Sextillion '
                                                                                                                                                                                WHEN 9 THEN ' Septillion '
                                                                                                                                                                                WHEN 10 THEN ' Octillion '
                                                                                                                                                                                WHEN 11 THEN ' Nonillion '
                                                                                                                                                                                WHEN 12 THEN ' Decillion '
                                                                                                                                                                                WHEN 13 THEN ' Undecillion '
                                                                                                                                                                            ELSE ''
                                                                                                                                                                            END + @outputString;
            END;

            -- now get the hundreds
            IF @hundreds <> '0'
            BEGIN
                SET @outputString = (SELECT word FROM @NumbersTable WHERE '0' + @hundreds = number) + ' Hundred ' + @outputString;
            END;
        END;

        SELECT @counter = @counter + 1, @position = @position - 3;

    END;

    -- Remove any double spaces
    SET @outputString = LTRIM(RTRIM(REPLACE(@outputString, '  ', ' ')));
    SET @outputString = (LEFT(@outputString, 1)) + SUBSTRING(@outputString, 2, 8000); --Lower

    IF (@Cents = 0)
        SET @outputString = (@outputString) + ' Baht Only'; --Lower --+convert(Varchar(20),@Cents) + '/100 CENTS'-- return the result

    ELSE
    BEGIN
        SET @inputNumber = CONVERT(VARCHAR(38), @Input);
        IF (@Cents >= 11 AND @Cents <= 19)
        BEGIN
            SET @tensones = @Cents;
            SET @ones = 0;
        END;
        ELSE
        BEGIN
            SET @tensones = LEFT(RIGHT(@inputNumber, 2), 1) + '0';
            SET @ones = RIGHT(@inputNumber, 1);
        END;

		IF @Number = 0 AND @Cents <> 0
			SET @outputString = (SELECT word FROM @NumbersTable WHERE @tensones = number) + ' ' + (SELECT word FROM @NumbersTable WHERE '0' + @ones = number) + ' Satang';
		ELSE
			SET @outputString = (@outputString) + ' Baht and ' + (SELECT word FROM @NumbersTable WHERE @tensones = number) + ' ' + (SELECT word FROM @NumbersTable WHERE '0' + @ones = number) + ' Satang';
    
	--Lower
    END;

    RETURN @outputString;
--RETURN @inputNumber

END;







GO
