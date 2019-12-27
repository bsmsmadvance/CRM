SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--select [dbo].[fn_GenCustAgreementAll] ('10019AA06502')

CREATE FUNCTION [dbo].[fn_GenCustAgreementAll_AG]
(
	@AgreementNo nvarchar(50)
)
RETURNS nvarchar(4000)
AS
BEGIN

	DECLARE @result nvarchar(4000);

    DECLARE @AgreementID NVARCHAR(100);
    SET @AgreementID = (SELECT ID FROM SAL.Agreement WHERE AgreementNo = @AgreementNo);
	
	DECLARE @v TABLE(FLName nvarchar(300),Header bit,ContactID nvarchar(50));

	INSERT INTO @v

		SELECT	CASE WHEN ISNULL(TitleExtTH,'')=''THEN  ISNULL(TitleExtTH,'')
                     ELSE ISNULL(TitleExtTH,'')  END
			+ISNULL(FirstNameTH,'')+' '+ISNULL(LastNameTH,'') As FLName
				,IsMainOwner,ContactNo
		FROM	[SAL].[AgreementOwner]
		WHERE	AgreementID = @AgreementID AND ISNULL(IsDeleted,0) = 0 
		ORDER BY IsMainOwner DESC,ContactNo Asc
				
	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 FLName FROM @v ORDER BY Header DESC,ContactID Asc);	

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN @result;
	  END

	DELETE FROM @v WHERE FLName=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp nvarchar(150);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 FLName FROM @v ORDER BY Header DESC,ContactID Asc);
		SET @result = @result + ' ' + @tmp;		
		DELETE FROM @v WHERE FLName=@tmp;
	  END

	RETURN @result;

END

GO
