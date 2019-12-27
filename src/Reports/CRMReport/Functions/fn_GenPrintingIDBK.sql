SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--Select 'A' = [dbo].[fn_GenPrintingIDBK]('10101BN00006')
--Select 'A' = [dbo].[fn_GenPrintingIDBK]('10063BN00346')

CREATE FUNCTION [dbo].[fn_GenPrintingIDBK] (@BookingNumber NVARCHAR(50))
RETURNS NVARCHAR(4000)
AS
--declare @BookingNumber nvarchar(50)
--set @BookingNumber='10091BN00003'
BEGIN

    DECLARE @Flag_BK TABLE (BK NVARCHAR(50));
    INSERT INTO @Flag_BK
    --Select DISTINCT CASE WHEN Method = 10 Then RCReferent ELSE ReferentID END AS BK From ICON_Payment_TmpReceipt Where ReferentID = @BookingNumber AND CancelDate IS NULL;
    -- แก้ไขกรณีย้ายแปลงครั้งที่สองแล้วไม่แสดงเลขที่ใบเสร็จ 20/05/2015
    SELECT DISTINCT (CASE
                        WHEN t.Method = 10 THEN 
						ISNULL(b8.BookingNumber, ISNULL(b7.BookingNumber, ISNULL(b6.BookingNumber, ISNULL(b5.BookingNumber, ISNULL(b4.BookingNumber, ISNULL(b3.BookingNumber, ISNULL(b2.BookingNumber, ISNULL(b1.BookingNumber, t.ReferentID))))))))
                    ELSE t.ReferentID
                    END) AS BK
    FROM ICON_Payment_TmpReceipt t
    LEFT JOIN ICON_EntForms_Booking b1 ON b1.BookingNumber = t.ReferentID
    LEFT JOIN ICON_EntForms_Booking b2 ON b2.BookingNumber = b1.BookingReferent AND b1.BookingReferent IS NOT NULL
    LEFT JOIN ICON_EntForms_Booking b3 ON b3.BookingNumber = b2.BookingReferent AND b2.BookingReferent IS NOT NULL
    LEFT JOIN ICON_EntForms_Booking b4 ON b4.BookingNumber = b3.BookingReferent AND b3.BookingReferent IS NOT NULL
    LEFT JOIN ICON_EntForms_Booking b5 ON b5.BookingNumber = b4.BookingReferent AND b4.BookingReferent IS NOT NULL
	LEFT JOIN ICON_EntForms_Booking b6 ON b6.BookingNumber = b5.BookingReferent AND b5.BookingReferent IS NOT NULL
	LEFT JOIN ICON_EntForms_Booking b7 ON b7.BookingNumber = b6.BookingReferent AND b6.BookingReferent IS NOT NULL
	LEFT JOIN ICON_EntForms_Booking b8 ON b8.BookingNumber = b7.BookingReferent AND b7.BookingReferent IS NOT NULL
    WHERE t.ReferentID = @BookingNumber
        AND t.CancelDate IS NULL;

    DECLARE @result NVARCHAR(4000);
    DECLARE @v TABLE (PrintingID NVARCHAR(50));

    INSERT INTO @v
    SELECT ISNULL(RC.PrintingID, '')
    FROM [ICON_Payment_TmpReceipt] TR
    LEFT OUTER JOIN [ICON_Payment_Received] RC ON RC.RCReferent = TR.RCReferent
    WHERE (TR.ReferentID IN (SELECT BK FROM @Flag_BK))
        AND TR.CancelDate IS NULL
        AND ISNULL(RC.PrintingID, '') <> ''
    ORDER BY RC.PrintingID;

    -- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
    SET @result = (SELECT TOP 1 PrintingID FROM @v);

    -- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
    IF ((SELECT COUNT(*)FROM @v) <= 1)
    BEGIN
        RETURN @result;
    --select * from @Flag_BK
    END;

    DELETE FROM @v WHERE PrintingID = @result; -- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

    -- ผสมข้อความ
    DECLARE @tmp NVARCHAR(500);
    WHILE EXISTS (SELECT * FROM @v)
    BEGIN
        SET @tmp = (SELECT TOP 1 PrintingID FROM @v);
        SET @result = @result + ', ' + @tmp;
        DELETE FROM @v WHERE PrintingID = @tmp;
    END;

    RETURN @result;
--select * from @Flag_BK
END;



GO
