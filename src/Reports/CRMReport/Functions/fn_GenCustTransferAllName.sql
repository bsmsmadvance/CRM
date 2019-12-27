SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











--select [dbo].[fn_GenCustTransferAllName] ('10054CT9121487')

CREATE FUNCTION [dbo].[fn_GenCustTransferAllName]
(
	@TransferNumber nvarchar(50)
)
RETURNS nvarchar(4000)
AS
BEGIN

	DECLARE @result nvarchar(4000);
	
	DECLARE @v TABLE(nOrder INT IDENTITY,FLName nvarchar(300));

	INSERT INTO @v

		SELECT CASE
               WHEN ISNULL(NamesTitleExt, '') = '' OR ISNULL(NamesTitleExt, '') = '-' THEN
                   ISNULL(NamesTitle, '')
               ELSE
                   ISNULL(NamesTitleExt, '')
           END +ISNULL(FirstName,'')+' '+ISNULL(LastName,'') As FLName		
		FROM [ICON_EntForms_TransferOwner]
		WHERE TransferNumber = @TransferNumber AND ISNULL(IsDelete,0) = 0 
		ORDER BY ID
						
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
		SET @result = @result + '   ' + @tmp;		
		DELETE FROM @v WHERE FLName=@tmp;
	  END

	RETURN @result;

END



































GO
