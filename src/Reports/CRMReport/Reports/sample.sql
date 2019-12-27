
CREATE OR ALTER PROCEDURE RPT.SAMPLE
	@alias VARCHAR(10)
AS
BEGIN
	SELECT id , NameTH, NameEN, dbo.fnFormatDateLongTH(GETDATE()) as BankSample  from MST.Bank where Alias = @alias
END
GO

exec RPT.SAMPLE 'KBank'