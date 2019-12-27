SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--AP2SP_RP_FI_019 '','10060','20121026','20121026','','','',''
--AP2SP_RP_FI_019 '','10071','','','s09','','',''
--AP2SP_RP_FI_019 '','10080','','','G10','','',''
--AP2SP_RP_FI_019 '','','20170521','20170521','','','1','Administrator Account'

--รายงานใบจอง offline
ALTER PROC [dbo].[AP2SP_RP_FI_019]
	@CompanyID  nvarchar(20),
	@ProductID	nvarchar(20),
	@DateStart	datetime,
	@DateEnd	datetime,   
	@UnitNumber varchar(8000),--แปลง
	@Method varchar(8000),--ประเภทรับเงิน
    @BookingOfflineStatus   nvarchar(10),--สถานะ
	@UserName	nvarchar(50) 
AS

DECLARE @DateEndInStore Datetime,@A varchar(5)
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

Declare @sql nvarchar(max)
Set @sql = '

SELECT ''ProjectID'' = '''' --B.ProjectID
    ,''ProjectName'' = '''' --P.Project AS ProjectName
    ,''BookingDate'' = '''' --BookingDate
    ,''ReceiptDate'' = '''' --R.ReceiptDate
    ,''ReceiptID'' = '''' --R.ReceiptID
    ,''BookingID'' = '''' --B.BookingNumber AS BookingID
    ,''UnitNumber'' = '''' --B.UnitNumber
    ,''CustomerName'' = '''' --ISNULL(C.TitleName,'''')+ISNULL(C.FirstName,'''')+'' ''+ISNULL(C.LastName,'''') AS CustomerName
    ,''Period'' = ''จอง''
    ,''PaymentType'' = '''' --RD.PaymentType
    ,''BankName'' = '''' --BK.AdBankName AS BankName
    ,''Branch'' = '''' --Branch
    ,''ReferenceDate'' = '''' --ReferenceDate
    ,''ReferenceNo'' = '''' --ReferenceNo
    ,''Amount'' = '''' --Amount
    ,''CreditCardFee'' = '''' --CreditCardFee
    ,''FeeAmount'' = '''' --FeeAmount
    ,''IsUploaded'' = 1 
    ,''IsCancel'' = '''' --B.IsCancel
    ,''FirstName'' = '''' --U.FirstName
    ,''CreateDate'' = '''' --B.CreateDate
    ,''BookingNumberOnline'' = '''' --BKK.BookingNumber AS BookingNumberOnline,
	,''Status'' = '''' /* CASE WHEN ISNULL(B.IsCancel,0) = 1 THEN ''ยกเลิก'' 
		WHEN BKK.BookingNumber IS NOT NULL THEN ''ยืนยันแล้ว'' 
		ELSE ''รอยืนยัน'' END AS Status */
	,''NetPrice'' = '''' --ISNULL(SellNetPrice,0) - ISNULL(TransferDiscount,0)

FROM [SAL].[Booking] B' --This is temp table actual table start from below
    /* dbo.ZTEMP_Booking B LEFT OUTER JOIN 
	dbo.ZTEMP_BookingOwner BO ON B.BookingNumber=BO.BookingNumber AND ISNULL(BO.IsBooking,0)=1 LEFT OUTER JOIN 
	dbo.ZTEMP_Customer C ON BO.CustomerID=C.CustomerID LEFT OUTER JOIN 
	dbo.ZTEMP_Receipt R ON B.BookingNumber=R.BookingNumber LEFT OUTER JOIN 
	dbo.ZTEMP_ReceiptDetail RD ON R.ReceiptID=RD.ReceiptID LEFT OUTER JOIN 
	dbo.ICON_EntForms_Products P ON B.ProjectID=P.ProductID LEFT OUTER JOIN 
	dbo.Users U ON B.UserID=U.UserID LEFT OUTER JOIN 
	dbo.ICON_EntForms_Bank BK ON BK.BankID=RD.BankID LEFT OUTER JOIN 
	(
		SELECT BK.BookingNumber,BK.ProductID,BK.UnitNumber,BKOW.ContactID,BK.BookingNumberOffline
		FROM ICON_EntForms_Booking BK LEFT OUTER JOIN 
			ICON_EntForms_BookingOwner BKOW ON BK.BookingNumber = BKOW.BookingNumber AND ISNULL(BKOW.Header, 0) = 1 AND ISNULL(BKOW.IsDelete, 0) = 0 AND ISNULL(BKOW.IsBooking, 0) = 1
		--WHERE ISNULL(BK.Cancel, 0) = 0
	) BKK ON B.ProjectID = BKK.ProductID AND B.UnitNumber = BKK.UnitNumber AND B.BookingNumber = BKK.BookingNumberOffline

WHERE 1 = 1 '

if(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND (B.ProjectID = '''+@ProductID+''')'


IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	SET @sql=@sql+' AND (B.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	SET @sql=@sql+' AND (B.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '


IF(ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A >= 1)) set @sql=@sql+' AND (B.UnitNumber IN ('+@UnitNumber+'))' 
IF(ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A <= 0)) set @sql=@sql+' AND (B.UnitNumber = '''+@UnitNumber+''')'

IF(@Method <> '0' AND @Method <> '') set @sql=@sql+'and(RD.PaymentType IN ('+@Method+')) '

IF(@BookingOfflineStatus = '2') set @sql=@sql+' AND (BKK.BookingNumber IS NOT NULL) AND (ISNULL(B.IsCancel,0) = 0)'
IF(@BookingOfflineStatus = '1') set @sql=@sql+' AND (BKK.BookingNumber IS NULL) AND (ISNULL(B.IsCancel,0) = 0)'
IF(@BookingOfflineStatus = '0') set @sql=@sql+' AND (ISNULL(B.IsCancel,0) = 1)'

IF(Isnull(@CompanyID,'')<>'')set @sql=@sql+' AND (P.CompanyID = '''+@CompanyID+''')'


set @sql=@sql+' ORDER BY B.BookingNumber ASC;' */

--print(@sql)
exec(@sql)





GO
