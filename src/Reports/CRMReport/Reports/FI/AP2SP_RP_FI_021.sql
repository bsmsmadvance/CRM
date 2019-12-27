SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_FI_021] '','10080',NULL,NULL,'','','',''
--[AP2SP_RP_FI_021] '','10085','2011-01-01','2012-04-30','','','',''

--รายงานใบเสร็จรับเงิน offline
CREATE PROC [dbo].[AP2SP_RP_FI_021]
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
SELECT ''ProjectID'' = '''' --R.ProjectID
    ,''ProjectName'' ='''' --P.Project
    ,''ReceiptDate'' = '''' --R.ReceiptDate
    ,''ReceiptID'' = '''' --R.ReceiptID
    ,''ReceivedID'' = '''' --RV.ReceivedID
    ,''UnitNumber'' = '''' --R.UnitNumber, 
	,''CustomerName'' = '''' --ISNULL(C.TitleName,'''')+ISNULL(C.FirstName,'''')+'' ''+ISNULL(C.LastName,'''') AS CustomerName, 
	,''Period'' = '''' --CASE R.PayType WHEN 1 THEN ''ค่าจอง'' WHEN 2 THEN ''ค่าทำสัญญา'' WHEN 3 THEN ''ค่างวดดาวน์'' END AS Period,
	,''PaymentType'' = '''' --PaymentType
    ,''BankName'' = '''' -- BK.AdBankName
    ,''Branch'' = '''' --Branch
    ,''ReferenceDate'' = '''' --ReferenceDate
    ,''REferenceNo'' = '''' --ReferenceNo
    ,''Amount'' = '''' --RD.Amount
    ,''CreditCardFee'' = '''' --CreditCardFee
    ,''FeeAmount'' = '''' --FeeAmount
    ,''IsUploaded'' = 1
    ,''IsCancel'' = '''' --R.IsCancel
    ,''FirstName'' = '''' --U.FirstName
    ,''CreateDate'' = '''' --R.CreateDate
    ,''PrintingID'' = '''' --RV.PrintingID,
	,''Status'' = '''' /* CASE WHEN ISNULL(R.IsCancel,0) = 1 THEN ''ยกเลิก'' 
			WHEN RV.PrintingID IS NULL THEN ''รอยืนยัน'' 
			WHEN RV.PrintingID IS NOT NULL THEN ''ยืนยันแล้ว'' END AS Status, */
	,''CRMCustomerName'' = '''' --ISNULL(BKOW.NamesTitle,'''')+ISNULL(BKOW.FirstName,'''')+'' ''+ISNULL(BKOW.LastName,'''') AS CRMCustomerName
	,''CRMCustomerName2'' == '''' --ISNULL(AGO.NamesTitle,'''')+ISNULL(AGO.FirstName,'''')+'' ''+ISNULL(AGO.LastName,'''') AS CRMCustomerName2
FROM [SAL].[Booking] B' --This is temp table actual table start from below
    /* dbo.ZTEMP_Receipt R LEFT OUTER JOIN 
	dbo.ZTEMP_ReceiptDetail RD ON R.ReceiptID=RD.ReceiptID LEFT OUTER JOIN 
	dbo.ZTEMP_Customer C ON R.CustomerID=C.CustomerID LEFT OUTER JOIN 
	dbo.ICON_EntForms_Products P ON R.ProjectID=P.ProductID LEFT OUTER JOIN 
	dbo.Users U ON R.UserID=U.UserID LEFT OUTER JOIN 
	dbo.ICON_EntForms_Bank BK ON BK.BankID=RD.BankID LEFT OUTER JOIN 
	dbo.ICON_Payment_Received RV ON RV.PrintingID=R.ReceiptID LEFT OUTER JOIN 
	dbo.ICON_EntForms_Booking BKK ON R.ProjectID = BKK.ProductID AND R.UnitNumber = BKK.UnitNumber AND BKK.CancelDate IS NULL LEFT OUTER JOIN 
	dbo.ICON_EntForms_BookingOwner BKOW ON BKK.BookingNumber = BKOW.BookingNumber AND ISNULL(BKOW.Header, 0) = 1 AND ISNULL(BKOW.IsDelete, 0) = 0 AND ISNULL(BKOW.IsBooking, 0) = 1 AND ISNULL(BKK.Cancel, 0) = 0 LEFT OUTER JOIN 
	dbo.ICON_EntForms_Agreement AG ON R.ProjectID = AG.ProductID AND R.UnitNumber = AG.UnitNumber AND AG.CancelDate IS NULL LEFT OUTER JOIN 
	dbo.ICON_EntForms_AgreementOwner AGO ON AG.ContractNumber = AGO.ContractNumber AND ISNULL(AGO.Header, 0) = 1 AND ISNULL(AGO.IsDelete, 0) = 0 AND ISNULL(AG.Cancel, 0) = 0


WHERE ISNULL(R.BookingNumber,'''')='''' 
	 '

if(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND (R.ProjectID = '''+@ProductID+''')'

IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	SET @sql=@sql+' AND (R.ReceiptDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	SET @sql=@sql+' AND (R.ReceiptDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '


IF(ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A >= 1)) set @sql=@sql+' AND (R.UnitNumber IN ('+@UnitNumber+'))' 
IF(ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A <= 0)) set @sql=@sql+' AND (R.UnitNumber = '''+@UnitNumber+''')'

IF(@Method <> '0' AND @Method <> '') set @sql=@sql+'AND (RD.PaymentType IN ('+@Method+')) '

IF(@BookingOfflineStatus = '2') set @sql=@sql+' AND (RV.PrintingID IS NOT NULL) AND (ISNULL(R.IsCancel,0) = 0)'
IF(@BookingOfflineStatus = '1') set @sql=@sql+' AND (RV.PrintingID IS NULL) AND (ISNULL(R.IsCancel,0) = 0)'
IF(@BookingOfflineStatus = '0') set @sql=@sql+' AND (ISNULL(R.IsCancel,0) = 1)'

IF(Isnull(@CompanyID,'')<>'')set @sql=@sql+' AND (P.CompanyID = '''+@CompanyID+''')'


set @sql=@sql+' ORDER BY R.ReceiptID ASC;' */


print(@sql)
exec(@sql)






GO
