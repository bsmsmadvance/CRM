SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--select [dbo].[fn_IsNationalityThaiBookingOwner] ('60022BA00028')
ALTER FUNCTION [dbo].[fn_IsNationalityThaiBookingOwner]
(
    @BookingNumber nvarchar(255)
)
RETURNS BIT
AS
BEGIN

	DECLARE @result nvarchar(4000);
	DECLARE @BookingID NVARCHAR(100);
	DECLARE @v TABLE(Nationality BIT);

    SET @BookingID = (SELECT ID FROM SAL.Booking WHERE BookingNo = @BookingID);

	INSERT INTO @v
	SELECT	C.IsThaiNationality
	FROM	[SAL].[BookingOwner] B 
		LEFT OUTER JOIN [CTM].[Contact] C ON C.ID = B.FromContactID			 
	WHERE	B.ID = @BookingID
		AND isNULL(B.IsMainOwner,0) = 1 
		AND isNULL(B.IsDeleted ,0)= 0
    ORDER BY ISNULL(C.IsThaiNationality,'') ASC
					
	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT  TOP 1 Nationality FROM @v Order by Nationality ASC);

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN CAST(CASE WHEN @result = 1 THEN 1 ELSE 0 END AS BIT);
	  END

	--DELETE FROM @v WHERE Nationality=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp nvarchar(250);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 Nationality FROM @v Order by Nationality ASC);

		IF (@tmp = 0) 
			RETURN CAST(0 AS BIT);

		SET @result = @result + ' ' + @tmp;		
		DELETE FROM @v WHERE Nationality=@tmp;
	  END

	RETURN CAST(CASE WHEN @result = 1 THEN 1 ELSE 0 END AS BIT);

END

GO
