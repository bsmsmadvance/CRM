SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





--select [dbo].[fn_GenCustAgreementAllNoTitle] ('10059AA00253')

CREATE FUNCTION [dbo].[fn_GenCustAgreementAllNoTitle]
(
	@AgreementID nvarchar(50)
)
RETURNS nvarchar(4000)
AS
BEGIN

	DECLARE @result nvarchar(4000);
	
	DECLARE @v TABLE(FLName nvarchar(300));

	INSERT INTO @v

		SELECT ISNULL(FirstNameTH,'')+' '+ISNULL(LastNameTH,'') As FLName		
		FROM [SAL].[AgreementOwner]
		WHERE AgreementID = @AgreementID AND ISNULL(IsDeleted,0) = 0 
		ORDER BY IsMainOwner DESC,AgreementID Asc
						
	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 FLName FROM @v);	

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
		SET @tmp = (SELECT TOP 1 FLName FROM @v);
		SET @result = @result + ',  ' + @tmp;		
		DELETE FROM @v WHERE FLName=@tmp;
	  END

	RETURN @result;

END


GO
