SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--Select 'A' = [dbo].[fn_GenCustBookingAllNoTitle]('10087BA00312')

CREATE FUNCTION [dbo].[fn_GenCustBookingAllNoTitle]
(
	@BookingNumber nvarchar(50)
)
RETURNS nvarchar(4000)
AS
BEGIN

	DECLARE @result nvarchar(4000);
	
	DECLARE @v TABLE(FLName nvarchar(300));

    DECLARE @BookingID NVARCHAR(50);
    SET @BookingID = (SELECT ID FROM SAL.Booking WHERE BookingNo = @BookingNumber);

	INSERT INTO @v

		SELECT ISNULL(FirstNameTH,'') + ' '+ISNULL(LastNameTH,'') As FLName		
		FROM [SAL].[BookingOwner]
		WHERE BookingID = @BookingID AND ISNULL(IsDeleted,0) = 0  AND ISNULL(IsMainOwner,0)= 1
		ORDER BY IsMainOwner DESC,FromContactID Asc
						
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
		SET @result = @result + ', ' + @tmp;		
		DELETE FROM @v WHERE FLName=@tmp;
	  END

	RETURN @result;

END



GO
