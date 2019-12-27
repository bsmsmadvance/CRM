SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_PF_FI_002_New_2] '10059201413025'
--[dbo].[AP2SP_PF_FI_002_2] '100522009254387'
--[dbo].[AP2SP_PF_FI_002_2] '1004220090020'
--[dbo].[AP2SP_PF_FI_002_2] '1002520090216'
--[dbo].[AP2SP_PF_FI_002_New_2] '00001,1004220090020,1004820090155,1004820090156,1004820090158,1004820090162,1004820090167,1004820090168,1004820090171,1004820090172,1004820090174,1004820090175,1004820090176'

CREATE PROC [dbo].[AP2SP_PF_FI_002_New_2]

	@ReceivedID nvarchar(4000) --เลขที่ใบเสร็จรับเงิน

AS

/* Declare @tbRC Table(rcreferent varchar(50))
Insert into @tbRC select rcreferent from [ICON_Payment_Received] where ReceivedID IN (SELECT * FROM [dbo].[fn_SplitString](@ReceivedID,','))

DECLARE @TableTmp Table	(ReceivedID nvarchar(40),RowNo int,TmpCash int,TmpChequeCash int
						,TmpCredit int,TmpCheque1 int,TmpCheque2 int,TmpTF int,TmpPayIn int,TmpDDebit int,TmpDCredit int,Amount Money)
INSERT INTO @TableTmp	(ReceivedID,RowNo,TmpCash,TmpChequeCash
						,TmpCredit,TmpCheque1,TmpCheque2,TmpTF,TmpPayIn,TmpDDebit,TmpDCredit,Amount)
Select	RC.ReceivedID
		,'RowNo' = Row_Number() OVER (PARTITION BY RC.ReceivedID ORDER BY RC.ReceivedID )
		,'TmpCash' =			CASE	WHEN TR1.Method = 1 THEN 1 ELSE 0 END	--'เงินสด' 
		,'TmpChequeCash' =	CASE	WHEN TR2.Method = 2 THEN 2 ELSE 0 END		--'แคชเชียร์เช็ค'
		,'TmpCredit' =		CASE	WHEN TR3.Method = 3 THEN 3 ELSE 0 END		--'เครดิตการ์ด'
		,'TmpCheque1' =		CASE	WHEN TR4.Method = 4 THEN 4 ELSE 0 END		--เช็คปัจจุบัน					
		,'TmpCheque2' =		CASE	WHEN TR5.Method = 5 THEN 5 ELSE 0 END		--เช็คล่วงหน้า				
		,'TmpTF' =			CASE	WHEN TR6.Method = 6 THEN 6 ELSE 0 END		--โอนเงินผ่านธนาคาร			
		,'TmpPayIn' = 		CASE 	WHEN TR7.Method = 7 THEN 7 ELSE 0 END		--การ์ดลูกหนี้				
		,'TmpDDebit' = 		CASE 	WHEN TR8.Method = 8 THEN 8 ELSE 0 END		--การ์ดลูกหนี้				
		,'TmpDCredit' = 	CASE 	WHEN TR9.Method = 9 THEN 9 ELSE 0 END		--การ์ดลูกหนี้	
		,'Amount' = CASE	WHEN TR1.Method = 1 AND TR1.IsHouse_Payment = 1  THEN TR1.Amount
							ELSE 0 END					
								
From [ICON_Payment_Received] RC
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR1 ON TR1.RCReferent = RC.RCReferent AND TR1.Method = 1 
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR2 ON TR2.RCReferent = RC.RCReferent AND TR2.Method = 2 
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR3 ON TR3.RCReferent = RC.RCReferent AND TR3.Method = 3 
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR4 ON TR4.RCReferent = RC.RCReferent AND TR4.Method = 4 
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR5 ON TR5.RCReferent = RC.RCReferent AND TR5.Method = 5 
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR6 ON TR6.RCReferent = RC.RCReferent AND TR6.Method = 6 
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR7 ON TR7.RCReferent = RC.RCReferent AND TR7.Method = 7 
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR8 ON TR8.RCReferent = RC.RCReferent AND TR8.Method = 8
LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR9 ON TR9.RCReferent = RC.RCReferent AND TR9.Method = 9 

WHERE RC.ReceivedID IN (SELECT * FROM [dbo].[fn_SplitString](@ReceivedID,',')) */





SELECT	'ReceivedID' = '' --RC.ReceivedID 
		,'AmouontInReceived' =	'' --MT.Amount
		-----การจ่ายเงิน------
		,'MethodName' =	'' /* CASE	WHEN TR.Method = 1 THEN 'เงินสด' 
								WHEN TR.Method = 2 THEN 'แคชเชียร์เช็ค'	--เช็คเงินสด
								WHEN TR.Method = 3 THEN 'เครดิตการ์ด'
								WHEN TR.Method = 4 THEN 'เช็คธนาคาร'	--เช็คปัจจุบัน
								WHEN TR.Method = 5 THEN 'เช็คธนาคาร'	--เช็คล่วงหน้า
								WHEN TR.Method = 6 THEN 'เงินโอน'		--โอนเงินผ่านธนาคาร
								WHEN TR.Method = 7 THEN 'PayIn' 
								WHEN TR.Method = 8 THEN 'DirectDebit'
								WHEN TR.Method = 9 THEN 'DirectCredit'END --การ์ดลูกหนี้ */
		--เช็คธนาคาร---
		,'ChequeBankName' = '' --CASE WHEN TR.Method IN ('2','4','5') THEN Left('ธ.'+Replace(BB.BankName,'ธนาคาร',''),40) ELSE NULL END
		,'ChequeBranchName' = '' --CASE WHEN TR.Method IN ('2','4','5') THEN Left(TR.BranchName,30) ELSE NULL END
		,'ChequeNumber'= '' --CASE WHEN TR.Method IN ('2','4','5') THEN TR.Number ELSE NULL END
		,'ChequeDueDate' = '' --CASE WHEN TR.Method IN ('2','4','5') THEN TR.DueDate ELSE NULL END
		,'ChequeAmount' = '' --CASE WHEN TR.Method IN ('2','4','5') THEN TR.Amount ELSE NULL END
		--บัตรเครดิต---
		,'CreditBankName' = '' 
        ,'CreditType' = ''
		,'CreditAccount' = '' --CASE WHEN MT.TmpCredit = 3 THEN [dbo].[fn_GetCreditNumber](RC.ReceivedID) ELSE '' END
		,'ExpireDate' = ''
		,'AccountName' = '' --CASE WHEN MT.TmpTF = 6 OR MT.TmpPayIn = 7 OR MT.TmpDDebit= 8 OR MT.TmpDCredit = 9  THEN [dbo].[fn_GetAccountName](RC.ReceivedID) ELSE '' END
		,'Method' = '' --TR.Method
		,'TmpCash' = '' --MT.TmpCash
		,'TmpChequeCash' = '' --MT.TmpChequeCash
		,'TmpCredit' = '' --MT.TmpCredit
		,'TmpCheque1' = '' --MT.TmpCheque1
		,'TmpCheque2' = '' --MT.TmpCheque2
		,'TmpTF' = '' --MT.TmpTF
		,'TmpPayIn' = '' --MT.TmpPayIn
		,'TmpDDebit' = '' --MT.TmpDDebit
		,'TmpDCredit' = '' --MT.TmpDCredit
,'CashAmount' = '' --(Select Sum(Amount) From [ICON_Payment_TmpReceipt]t Where t.RCReferent=TR.RCReferent and t.Method='1')CashAmount
,'TransferAmount' = '' --(Select Sum(Amount) From [ICON_Payment_TmpReceipt]t Where t.RCReferent=TR.RCReferent and t.Method='6')TransferAmount
,'CreditAmount' = '' --(Select Sum(Amount) From [ICON_Payment_TmpReceipt]t Where t.RCReferent=TR.RCReferent and t.Method='3')CreditAmount

FROM	[FIN].[ReceiptTempHeader] RTH --This is main table need to use table below as well
		/* LEFT OUTER JOIN	[ICON_Payment_TmpReceipt] TR ON TR.RCReferent = RC.RCReferent  AND TR.CancelDate IS NULL 
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = TR.ReferentID  AND TR.Method = 1 AND TR.PaymentType = '8' 
		LEFT OUTER JOIN [ICON_EntForms_TransferPayment] TP ON TP.TransferNumber = TF.TransferNumber
		LEFT OUTER JOIN	[ICON_Payment_DownloadedPayment] DP ON DP.RCReferent = RC.RCReferent 
		LEFT OUTER JOIN	[ICON_Payment_DirectDBCRDetails] DD ON DD.RCReferent = TR.RCReferent
		LEFT OUTER JOIN	[ICON_Payment_DirectDBCRHeader] DH ON DD.RddBatchID = DH.RddBatchID 
		LEFT OUTER JOIN [ICON_Payment_PayInTransfer] PT ON PT.PayInID = TR.Number AND PT.RCReferent = TR.RCReferent 
		LEFT OUTER JOIN	[ICON_EntForms_Bank] BB ON BB.BankID = TR.BankID 
		LEFT OUTER JOIN	[ICON_EntForms_BankAccount] BA ON BA.ID = CASE	WHEN TR.Method = '6' THEN PT.BankAccountID 
																		WHEN TR.Method = '7' THEN DP.BankAccountID
																		ELSE DH.BankAccountID END

		INNER JOIN @TableTmp MT ON MT.ReceivedID = RC.ReceivedID AND MT.RowNo = 1 */

GO
