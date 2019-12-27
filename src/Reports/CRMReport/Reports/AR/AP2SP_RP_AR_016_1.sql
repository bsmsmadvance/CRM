SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec "AP2SP_RP_AR_016_1";1 N'RV1211F1748'


--[dbo].[AP2SP_RP_AR_016_1] 'rv1109f2536'
--[dbo].[AP2SP_RP_AR_016_1] 'RV1007F0023'
--[dbo].[AP2SP_RP_AR_016_1] 'RV1004R0087'
--[dbo].[AP2SP_RP_AR_016_1] 'RV1004P0044'
--[dbo].[AP2SP_RP_AR_016_1] 'RV1004O0002'
--[dbo].[AP2SP_RP_AR_016_1] 'RV1005P0014'

CREATE PROC [dbo].[AP2SP_RP_AR_016_1]

	@GLBatchID nvarchar(40) 
AS



Declare @sql nvarchar(max)
Set @sql='
Declare @TMPRV TABLE(ProductID nvarchar(40),AccountID nvarchar(40),Method nvarchar(40),RAmount money)
INSERT INTO @TMPRV
SELECT ProductID, AccountID, Method, RAmount
FROM [dbo].[fn_GenTotalAmtRV] ('''+@GLBatchID+''') '

Set @Sql = @Sql+ '

SELECT  ''ProductID'' = '''' --TR.ProductID
        , ''DepositID'' = '''' --TR.DepositID
		, ''MethodName'' =	'''' --CASE	WHEN TR.Method = 1 THEN ''เงินสด'' 
									--WHEN TR.Method = 2 THEN ''เช็ค''	--เช็คเงินสด
									--WHEN TR.Method = 3 THEN ''บัตรเครดิต''
									--WHEN TR.Method = 4 THEN ''เช็ค''	--เช็คปัจจุบัน
									--WHEN TR.Method = 5 THEN ''เช็ค''	--เช็คล่วงหน้า
									--WHEN TR.Method = 6 THEN ''โอนผ่านธนาคาร''		--โอนเงินผ่านธนาคาร
									--WHEN TR.Method = 7 THEN ''BillPayment'' 
									--WHEN TR.Method = 8 THEN ''DirectDebit''
									--WHEN TR.Method = 9 THEN ''DirectCredit'' END --การ์ดลูกหนี้
		, ''RAmount'' = '''' --RC.Amount AS RAmount
        , ''ReceivedID'' = '''' --RC.ReceivedID
        , ''UnitNumber'' = '''' --TR.UnitNumber
        , ''TAmount'' = '''' --ISNULL(TR.DepAmount,TR.Amount) AS TAmount
        , ''GLBatchID'' = '''' --TR.GLBatchID AS BatchID  
		, ''GLDepositBatchID'' = '''' --TR.GLDepositBatchID
		, ''TotalAmount'' = '''' --TMPRV.RAmount AS TotalAmount
        , ''GLAccount'' = '''' --BA.GLAccount

FROM	[MST].[Bank] B '  --This is temp table actual table start from below
        /* [ICON_Payment_Received] RC
		LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR ON RC.RCReferent = TR.RCReferent
		LEFT JOIN ICON_Payment_PayInTransfer AS PTF ON (TR.DepositID = PTF.PayInID OR TR.Amount = PTF.Amount) AND TR.RCReferent = PTF.RCReferent AND PTF.GLBatchID IS NULL    
		LEFT JOIN ICON_payment_DownloadedPayment DP ON DP.RCReferent = TR.RCReferent                      
		LEFT JOIN ICON_Payment_DirectDBCRDetails DDD ON DDD.RCReferent = TR.RCReferent                              
		LEFT JOIN ICON_Entforms_BankAccount BA ON BA.ID = CASE TR.Method WHEN 6 THEN PTF.BankAccountID WHEN 7 THEN DP.BankAccountID WHEN 8 THEN DDD.BankAccountID WHEN 9 THEN DDD.BankAccountID END    
		LEFT JOIN ICON_PostToSAP_ChartOfAccount AS CA ON CA.AccountRef = CASE	WHEN TR.Method IN (1,2,3,4) THEN CAST(TR.Method AS NVARCHAR(10)) WHEN TR.Method IN (6,7,8,9) THEN BA.BankAccount END   
																				AND CASE WHEN TR.Method IN (6,7,8,9) THEN CA.BankType ELSE ''1'' END = CASE WHEN TR.Method IN (6,7) THEN ''Incoming'' WHEN TR.Method IN (8,9) THEN ''Incoming'' ELSE ''1'' END  
		LEFT JOIN ICON_Payment_Payment PP ON PP.RCReferent = TR.RCReferent AND PP.Number = TR.Number  AND PP.Amount = TR.Amount AND TR.Method = 2
		LEFT JOIN @TMPRV TMPRV ON TMPRV.AccountID = (CASE WHEN TR.Method = 1 THEN ''1000030'' WHEN TR.Method IN (2,4) THEN ''1000040'' WHEN TR.Method = 3 THEN ''1000050'' ELSE CA.AccountID END)
WHERE	RC.CancelDate Is null and TR.CancelDate IS NULL AND Isnull(PP.CompanyID,'''') Not In (''Z'') --ตัดเช็คกระทรวงการคลังออก
		And ((TR.IsHouse_Payment = 1) OR
			 (TR.IsHouse_Payment = 0 AND TR.Method <> 1 AND PP.CompanyID Not In (''0'',''Z'')) OR
			 (TR.IsHouse_Payment = 2 AND TR.Method = 1 AND TR.DepositID IS NOT NULL) OR		
		     (TR.IsHouse_Payment = 2 AND TR.Method <> 1 ) 
			) '

		IF(ISNULL(@GLBatchID,'')<>'')set @sql=@sql+' AND(TR.GLBatchID  IN (SELECT * FROM [dbo].[fn_SplitString]('''+@GLBatchID+''','',''))) '
		set @sql=@sql+'ORDER BY RC.ReceivedID' */
		print(@Sql)
		exec(@sql)


GO
