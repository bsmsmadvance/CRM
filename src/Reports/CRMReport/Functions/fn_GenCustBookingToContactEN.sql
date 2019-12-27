SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--SELECT [dbo].[fn_GenCustBookingToContactEN] ('10132BA00472'); 

ALTER FUNCTION [dbo].[fn_GenCustBookingToContactEN]
(
	@BookingNumber nvarchar(50)
)
RETURNS nvarchar(4000)
AS
BEGIN

	DECLARE @result nvarchar(4000);
	DECLARE @bookingID nvarchar(100);
	DECLARE @v TABLE(nOrder INT IDENTITY,FLName nvarchar(300));

    -- ต้องหาเลข BookingID จาก เลข BookingNumber ที่ใส่เข้ามาก่อน
    SET @bookingID = (SELECT BookingNo FROM [SAL].[Booking] where BookingNo = @BookingNumber);

	INSERT INTO @v

		SELECT CASE WHEN ISNULL(CT.TitleExtEN,'')='' OR ISNULL(CT.TitleExtEN,'')='0' THEN ISNULL(CT.TitleExtEN,'') ELSE ISNULL(CT.TitleExtEN,'') END + ISNULL(CT.FirstNameEN,'') + ' '+ISNULL(CT.LastNameEN,'')  As FLName		
		FROM [SAL].[BookingOwner] BO
		LEFT OUTER JOIN [CTM].[Contact] CT ON CT.ID = BO.FromContactID
		WHERE BookingID = @bookingID 
			AND ISNULL(BO.IsDeleted,0) = 0 
				--AND ISNULL(IsContract,0) = 1 
		--ORDER BY ISNULL(IsContractHeader,0) DESC,BO.ContactID Asc
						
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
