SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--select [dbo].[fn_IsNationalityThaiAgreementOwner] ('60021AA00006')
CREATE FUNCTION [dbo].[fn_IsNationalityThaiAgreementOwner]
(
    @ContractNumber nvarchar(255)
)
RETURNS BIT
AS
BEGIN

	DECLARE @result nvarchar(4000);
	
	DECLARE @v TABLE(Nationality nvarchar(300));

	INSERT INTO @v
	SELECT	ISNULL(C.Nationality,'')
	FROM	dbo.[ICON_EntForms_AgreementOwner] B 
		LEFT OUTER JOIN dbo.ICON_EntForms_Contacts C ON C.ContactID = B.ContactID			 
	WHERE	ContractNumber = @ContractNumber
		AND isNULL(B.IsDelete ,0)= 0
    ORDER BY ISNULL(C.Nationality,'') ASC
					
	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT  TOP 1 Nationality FROM @v Order by Nationality ASC);

	-- ถ้าข้อมูลมีเรคคอร์ดเดียว (หรือไม่มี) ก็ให้ส่งกลับไปได้เลย
	IF ((SELECT COUNT(*) FROM @v) <= 1)
	  BEGIN
		RETURN CAST(CASE WHEN @result like '%ไทย%' OR @result like '%thai%' THEN 1 ELSE 0 END AS BIT);
	  END

	--DELETE FROM @v WHERE Nationality=@result;	-- ลบรายการแรกที่ดึงเอาไปผสมข้อความแล้วทิ้ง

	-- ผสมข้อความ
	DECLARE @tmp nvarchar(150);
	WHILE EXISTS(SELECT * FROM @v)
	  BEGIN
		SET @tmp = (SELECT TOP 1 Nationality FROM @v Order by Nationality ASC);

		IF (@tmp NOT LIKE '%ไทย%' and @tmp NOT LIKE '%thai%') 
			RETURN CAST(0 AS BIT);

		SET @result = @result + ' ' + @tmp;		
		DELETE FROM @v WHERE Nationality=@tmp;
	  END

	RETURN CAST(CASE WHEN @result like '%ไทย%' OR @result like '%thai%' THEN 1 ELSE 0 END AS BIT);

END
GO
