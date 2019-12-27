SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_AR_021] 'P','1800-02-08 00:00:00.000','2010-05-31 00:00:00.000','','Administrator Account'
--[dbo].[AP2SP_RP_AR_021] '','2010-02-08 00:00:00.000','2010-02-10 00:00:00.000',''
--[dbo].[AP2SP_RP_AR_021] 'H','1800-02-08 00:00:00.000','2010-05-31 00:00:00.000','',''

ALTER PROCEDURE [dbo].[AP2SP_RP_AR_021]
	@CompanyID nvarchar(40)
	,@DateStart datetime
	,@DateEnd   datetime
	,@BankAccount nvarchar(20)
	,@UserName nvarchar(40)
AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

Declare @sql nvarchar(max)
Set		@sql= '

SELECT ''RDate'' = '''' --GL.RDate
        , ''GLBatchID'' = '''' --GL.GLBatchID
        , ''AccountName'' = '''' --GL.AccountName
        , ''DRAmount'' = '''' --ISNULL(GL.DRAmount,0) AS DRAmount
        , ''CRAmount'' = '''' --ISNULL(GL.CRAmount,0) AS CRAmount
        , ''RCReferent'' = '''' --GL.RCReferent
        , ''CompAccountName'' = '''' --GL.CompAccountName
        , ''CompanyID'' = '''' --GL.CompanyID
        , ''ID'' = '''' --GL.ID
        , ''BankAccount'' = '''' --GL.BankAccount
FROM [MST].[BankAccount] BA' --This is temp table actual table start from below
/* (
SELECT	PT.RDate,PT.GLBatchID,BA.AccountName,PT.Amount AS DRAmount, NULL AS CRAmount,PT.RCReferent
		,BA.CompAccountName,BA.CompanyID,BA.ID,BA.BankAccount
FROM	ICON_Payment_PayInTransfer PT
		LEFT OUTER JOIN ICON_EntForms_BankAccount BA ON BA.ID = PT.BankAccountID

Where	(PT.RCReferent IS NULL AND PT.GLBatchID IS NOT NULL)	     --โพสลงบัญชีพักแล้วยังไม่ทราบที่มา
		OR (PT.RCReferent IS NOT NULL AND PT.GLBatchID IS NOT NULL)  --โพสลงบัญชีพักแล้วและทราบที่มาแล้ว
		AND PT.CancelDate IS NULL

UNION ALL
SELECT	PT.RDate,TR.GLBatchID,GL.AccountName+'' ''+PT.GLBatchID AS AccountName,NULL AS DRAmount,PT.Amount AS CRAmount,PT.RCReferent
		,BA.CompAccountName,BA.CompanyID,BA.ID,BA.BankAccount
FROM	[ICON_Payment_PayInTransfer] PT
		LEFT OUTER JOIN [ICON_EntForms_BankAccount] BA ON BA.ID = PT.BankAccountID
		LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR ON TR.RCReferent = PT.RCReferent AND TR.CancelDate IS NULL AND TR.DepositID = PT.PayInID
		LEFT OUTER JOIN [ICON_PostGL_PostToGL] GL ON GL.BatchID = TR.GLBatchID AND GL.DRAmount IS NULL
Where	(PT.RCReferent IS NOT NULL AND PT.GLBatchID IS NOT NULL)	--โพสลงบัญชีพักแล้วและทราบที่มาแล้ว
		AND PT.CancelDate IS NULL 
) AS GL
WHERE 1=1 '

IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND(GL.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@BankAccount,'')<>'')set @sql=@sql+' AND(GL.ID = '''+@BankAccount+''')'
IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
                   set @sql=@sql+' AND(GL.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') ' 

set @sql=@sql+' ORDER BY GL.RDATE,GL.AccountName,GL.DRAmount DESC,GL.CRAmount DESC ' */
EXEC(@sql)











GO
