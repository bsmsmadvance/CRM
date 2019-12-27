SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ดึงชื่อผู้ทำสัญญาที่ระบุในใบจอง
-- [dbo].[AP2SP_PF_AG_003_1]'10060BA00106'
ALTER PROCEDURE [dbo].[AP2SP_PF_AG_003_1]
    @BookingNumber  nvarchar(50)	
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @BookingID NVARCHAR(50)
SET @BookingID = (SELECT ID FROM SAL.Booking WHERE BookingNo = @BookingNumber)

DECLARE @x int
SET @x = (SELECT	SUM(CASE WHEN ISNULL(IsMainOwner,0) = 1 THEN 1 ELSE 0 END)
		  FROM		[SAL].[BookingOwner]
		  WHERE		@BookingID = @BookingNumber)

IF(@x > 0)
BEGIN
	SELECT	ISNULL(BO.TitleExtTH,'')+ BO.FirstNameTH+'  '+ISNULL(BO.LastNameTH,'') As FullName,	
			'Age' = Year(BK.BookingDate)-Year(CT.ContactsBirthDay)
	FROM	[SAL].[BookingOwner] BO WITH (NOLOCK) 
            INNER JOIN [SAL].[Booking] BK WITH (NOLOCK) ON BK.ID = BO.BookingID 
            INNER JOIN [CTM].[Contact] C WITH (NOLOCK) ON C.ID = BO.FromContactID
            --INNER JOIN [vw_ICON_EntForms_ContactsMap] CT WITH (NOLOCK) ON CT.ContactID = BO.ContactID AND ISNULL(BO.IsContract,0) = 1
	WHERE	ISNULL(BO.IsDelete,0) = 0 AND (BO.BookingID = @BookingID)
	ORDER BY BO.IsContractHeader DESC,BO.ContactID Asc
END
IF(@x = 0)
BEGIN
	SELECT  ISNULL(BO.TitleExtTH,'')+ BO.FirstNameTH+'  '+ISNULL(BO.LastNameTH,'') As FullName,	
			'Age' = Year(BK.BookingDate)-Year(CT.BirthDate)
	FROM	[SAL].[BookingOwner] BO WITH (NOLOCK) 
            INNER JOIN [SAL].[Booking] BK WITH (NOLOCK) ON BK.ID = BO.BookingID 
            INNER JOIN [CTM].[Contact] CT WITH (NOLOCK) ON CT.ID = BO.FromContactID AND ISNULL(BO.IsContract,0) = 0
	WHERE	ISNULL(BO.IsDelete,0) = 0 AND (BO.BookingNumber = @BookingNumber)	
	ORDER BY BO.Header DESC,BO.FromContactID Asc
END

-- SELECT * FROM [ICON_EntForms_BookingOwner] Where BookingNumber = '10056BA9004539' AND IsContract = '1'

GO
