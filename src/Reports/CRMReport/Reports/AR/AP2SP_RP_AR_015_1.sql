SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[AP2SP_RP_AR_015_1] 'PI1201F0207'
ALTER PROC [dbo].[AP2SP_RP_AR_015_1]

	@BatchID nvarchar(40) 
AS

Declare @sql nvarchar(max)
Set @sql= '

SELECT  ''ProductID'' = '''' --TR.ProductID
        , ''DepositID'' = '''' --TR.DepositID
        , ''RCReferent'' = '''' --TR.RCReferent
		, ''MethodName'' = ''''	--CASE	WHEN TR.Method = 1 THEN ''เงินสด'' 
								--	WHEN TR.Method = 2 THEN ''เช็ค''	--เช็คเงินสด
								--	WHEN TR.Method = 3 THEN ''บัตรเครดิต''
								--	WHEN TR.Method = 4 THEN ''เช็ค''	--เช็คปัจจุบัน
								--	WHEN TR.Method = 5 THEN ''เช็ค''	--เช็คล่วงหน้า
								--	WHEN TR.Method = 6 THEN ''โอนผ่านธนาคาร''		--โอนเงินผ่านธนาคาร
								--	WHEN TR.Method = 7 THEN ''PayIn'' 
								--	WHEN TR.Method = 8 THEN ''DirectDebit''
								--	WHEN TR.Method = 9 THEN ''DirectCredit''END --การ์ดลูกหนี้
		, ''AccountName'' = '''' --BA.AccountName
        , ''RAmount'' = '''' --RC.Amount AS RAmount
        , ''ReceivedID'' = '''' --RC.ReceivedID
        , ''UnitNumber'' = '''' --TR.UnitNumber
        , ''TAmount'' = '''' --TR.Amount AS TAmount
		, ''RDate'' = '''' --TR.RDate
        , ''GLDepositBatch'' = '''' --TR.GLDepositBatchID
        , ''GLBatchID'' = '''' --TR.GLBatchID

FROM	[MST].[Bank] B ' --This is temp table actual table start from below
        /* [ICON_Payment_Received] RC
		LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR ON RC.RCReferent = TR.RCReferent
		LEFT OUTER JOIN [ICON_Payment_Deposit] DP ON DP.DepositID = TR.DepositID
		LEFT OUTER JOIN	[ICON_EntForms_BankAccount] BA ON BA.ID = DP.BankAccountID

WHERE	TR.GLDepositBatchID IS NOT NULL AND TR.CancelDate IS NULL '
		IF(ISNULL(@BatchID,'')<>'')set @sql=@sql+' AND(TR.GLDepositBatchID IN (SELECT * FROM [dbo].[fn_SplitString]('''+@BatchID+''','','')))'
		SET @sql=@sql+' ORDER BY RC.ReceivedID'
        print(@sql)
		EXEC(@sql) */


GO
