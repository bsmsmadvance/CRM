SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








--SELECT [dbo].[fn_GenCustBBefore_A] ('10049BA200907014','90074336')
CREATE FUNCTION [dbo].[fn_GenCustBBefore_A]
(
	@BookingNumber nvarchar(50),
	@ContactID  nvarchar(150)
)
RETURNS nvarchar(4000)
AS
BEGIN

	DECLARE @result nvarchar(4000);
	
	DECLARE @v TABLE(FLName nvarchar(300),Header nvarchar(2),ContactID nvarchar(20));

	INSERT INTO @v

		SELECT	CAST(ISNULL(ContactID,'') AS nvarchar(10))+'-'+ ISNULL(FirstName,'') + ' '+ISNULL(LastName,'') As FLName	
				,Header,ContactID	
		FROM	[ICON_EntForms_BookingOwner]
		WHERE	BookingNumber = @BookingNumber
				AND ContactID IN (SELECT * FROM dbo.fn_SplitString(@ContactID,','))
--		AND (ISNULL(IsDelete,0) = 0) 
		ORDER BY Header DESC,ContactID Asc
						
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
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE FLName=@tmp;
	  END

	RETURN @result;

END










































GO
