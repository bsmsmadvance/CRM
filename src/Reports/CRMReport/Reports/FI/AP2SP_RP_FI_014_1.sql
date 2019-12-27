SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--หนังสือรับรองการชำระเงิน
--[AP2SP_RP_FI_014_1] '40012','A02B22','','','''4'',''5'',''6'',''8'',''TR2''','''6''','','2014-03-09 00:00:00.000','2017-03-09 00:00:00.000'

ALTER PROCEDURE [dbo].[AP2SP_RP_FI_014_1]
	@ProductID	nvarchar(15)
	, @UnitNumber	nvarchar(15) 
	, @PeriodStart  nvarchar(15)
	, @PeriodEnd	nvarchar(15)
    , @PaymentType  nvarchar(250)
    , @PaymentType2 nvarchar(250)
    , @UserName	nvarchar(150) = ''
	,@DateStart datetime
	,@DateEnd   datetime

AS


DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

Declare @sql nvarchar(max)
Set @sql= '

SELECT    ''ContractNumber'' = '''' --AG.ContractNumber
        ,''BookingNumber'' = '''' --Isnull(AG.BookingNumber,TR.ReferentID) AS BookingNumber
        , ''ReferentNumber'' = '''' --Isnull(Isnull(AG.ContractNumber,(Select top 1 ContractNumber From Icon_entforms_agreement where bookingnumber=TR.ReferentID)),TR.ReferentID)--TR.ReferentID
        , ''RDate'' = '''' --TR.RDate
        , ''Amount'' = '''' --PD.Amount
		, ''Period'' = '''' /* CASE WHEN PD.PaymentType = ''4'' THEN ''จอง''
						  WHEN PD.PaymentType = ''5'' THEN ''สัญญา''
						  WHEN PD.PaymentType = ''TR2'' THEN ''ค่าธรรมเนียมการเปลี่ยนมือ'' 
						  WHEN PD.PaymentType = ''8'' THEN ''ค่าบ้าน''
						  WHEN PD.PaymentType = ''6'' THEN RIGHT(''00''+CONVERT(nvarchar(5),PD.Period),3) 
						  WHEN PD.PaymentType = ''A06'' THEN ''ค่าเนื้อที่เพิ่ม'' END */
		, ''PeriodEng'' = '''' /* CASE WHEN PD.PaymentType = ''4'' THEN ''Booking''
						  WHEN PD.PaymentType = ''5'' THEN ''Contract''
						  WHEN PD.PaymentType = ''TR2'' THEN ''Transfer Fee'' 
						  WHEN PD.PaymentType = ''8'' THEN ''Transfer''
						  WHEN PD.PaymentType = ''6'' THEN RIGHT(''00''+CONVERT(nvarchar(5),PD.Period),3)
						  WHEN PD.PaymentType = ''A06'' THEN ''Additional amount from increasing area'' END */
        , ''ReceivedID'' = '''' --RC.ReceivedID
		, ''ReceiveDate'' = '''' --RC.ReceiveDate
		, ''PaymentType'' = '''' --PD.PaymentType
		, ''PrintingID'' = '''' --RC.PrintingID

FROM   [SAL].[Booking] B' --This is temp table actual table start from below
       /*  [ICON_Payment_TmpReceipt] TR  
       LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
       LEFT OUTER JOIN [ICON_Payment_Received] RC ON  RC.RCReferent = TR.RCReferent AND RC.CancelDate IS NULL
       LEFT OUTER JOIN [ICON_Payment_Deposit] DE ON DE.DepositID = TR.DepositID
       LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON AG.ContractNumber = TR.ReferentID
       LEFT OUTER JOIN [ICON_EntForms_Booking] BK ON BK.BookingNumber = TR.ReferentID

WHERE AG.CancelDate IS NULL AND BK.CancelDate IS NULL AND TR.CancelDate IS NULL 
	'

if(ISNULL(@ProductID,'')<>'')set @sql=@sql+' and(TR.ProductID = '''+@ProductID+''')'
if(ISNULL(@UnitNumber,'')<>'')set @sql=@sql+' and(TR.UnitNumber = '''+@UnitNumber+''')'
if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')<>'')
   set @sql=@sql+' AND (PD.PaymentType IN ('+@PaymentType+') OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period Between  '''+@PeriodStart+''' AND '''+@PeriodEnd+'''))) '
if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')='')
   set @sql=@sql+' AND (PD.PaymentType IN ('+@PaymentType+') OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period >= '''+@PeriodStart+'''))) '
if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')<>'')
   set @sql=@sql+' AND (PD.PaymentType IN ('+@PaymentType+') OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period <= '''+@PeriodEnd+'''))) '
if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')='')
   set @sql=@sql+' AND (PD.PaymentType IN ('+@PaymentType+') OR (PD.PaymentType = '+@PaymentType2+')) '

--เลือกระหว่างวันที่
IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	SET @sql=@sql+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+' ORDER BY PD.PaymentType,PD.Period,TR.RDate ' */
exec(@sql)
print(@sql)

GO
