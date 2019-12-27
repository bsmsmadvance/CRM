SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--รายละเอียดการรับเงินในการโอน
-- [dbo].[AP2SP_NRP_TR_005_2_CD] NULL,'60004','20180405','20180405','Administrator Account'

CREATE PROC [dbo].[AP2SP_NRP_TR_005_2_CD]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@UserName nvarchar(150)
AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @sql nvarchar(MAX)
SET @sql = '
SELECT DISTINCT 
    ''CompanyName'' = '''' --C.CompanyNameThai
	,''ProjectName'' = '''' --P.Project
	,''UnitNumber'' = '''' --A.UnitNumber
	,''CustomerName'' = '''' --[dbo].[fn_GenCustTrasferAllNoTitle](T.TransferNumber)
	,''Sata'' = '''' --ISNULL(TT.Amount,0)
	,''Funds'' = '''' --ISNULL(TM.FundsAmount,0)
'
SET @sql = @sql+'
	,''Sata_Funds_Payment'' = '''' --Case When ISNULL(TP.SumCashAPUtil,0)=0 Then (Select Sum(pd.Amount) From ICON_Payment_PaymentDetails pd Left Join ICON_Payment_TmpReceipt tr On tr.TmpReceiptID=pd.TmpReceiptID Where pd.ReferentID=t.ContractNumber and tr.Method IN(''1'') and pd.PaymentType In(''00'',''2G'',''2H'',''9K'')) Else ISNULL(TP.SumCashAPUtil,0) End   
	,''Sata_Funds_Cheque'' = '''' /* Case When ISNULL(TP.BringAPUtil,0) > 0 AND ISNULL(TP.APUtilitiesAmount,0) > 0 AND ISNULL((Select Sum(pd.Amount) From ICON_Payment_PaymentDetails pd Left Join ICON_Payment_TmpReceipt tr On tr.TmpReceiptID=pd.TmpReceiptID Where pd.ReferentID=t.ContractNumber and tr.Method IN(''2'',''3'',''4'') and pd.PaymentType In(''00'',''2G'',''2H'',''9K'') and tr.transferpaymenttype=''1''),0) = 0 THEN ISNULL(TP.ChequeAPUtilAmount,0) -- 0  คุณภัท แจ้ง Case ไม่แสดงเงิน 60001/17A07
			When ISNULL(TP.APUtilitiesAmount,0)=0 Then (Select Sum(pd.Amount) From ICON_Payment_PaymentDetails pd Left Join ICON_Payment_TmpReceipt tr On tr.TmpReceiptID=pd.TmpReceiptID Left Join ICON_EntForms_TransferCheque tc on tc.TransferNumber=t.TransferNumber and tc.BankNumber=tr.Number Where pd.ReferentID=t.ContractNumber and tr.Method IN(''2'',''3'',''4'') and pd.PaymentType In(''00'',''2G'',''2H'',''9K'') and Isnull(tc.CompanyID,'''') in('''',''0'',''Z'')) 
			Else ISNULL(TP.ChequeAPUtilAmount,0) + ISNULL((Select Sum(pd.Amount) From ICON_Payment_PaymentDetails pd Left Join ICON_Payment_TmpReceipt tr On tr.TmpReceiptID=pd.TmpReceiptID Where pd.ReferentID=t.ContractNumber and tr.Method IN(''2'',''3'',''4'') and pd.PaymentType In(''00'',''2G'',''2H'',''9K'') and tr.transferpaymenttype=''1''),0) End */
'
SET @sql = @sql+ '
	,''Sata_Funds_AP'' = '''' --Case When ISNULL(TP.SumCarryAPUtil,0)=0 and ISNULL(TP.SumTransferAPUtil,0)=0 Then (Select Sum(pd.Amount) From ICON_Payment_PaymentDetails pd Left Join ICON_Payment_TmpReceipt tr On tr.TmpReceiptID=pd.TmpReceiptID Left Join icon_payment_payintransfer pp on ((pp.RCReferent = tr.RCReferent AND pp.Amount = tr.Amount) OR (ISNULL(pp.AdditionRCReferent,'''') LIKE ''%''+tr.RCReferent+''%'')) Left Join ICON_EntForms_TransferCheque tc on tc.TransferNumber=t.TransferNumber and tc.BankNumber=tr.Number Where pd.ReferentID=t.ContractNumber and pd.PaymentType In(''00'',''2G'',''2H''/*,''9K''*/) and (tr.Method IN(''7'',''8'',''9'') or (tr.Method IN(''2'') and  Isnull(tc.CompanyID,'''')not in('''',''0'',''Z'')) or (tr.Method IN(''6'') and pp.BankAccountID In(Select ID From icon_entforms_bankaccount where Isnull(CompanyID,''0'')<>''0'') /*AND tr.IsHouse_Payment=1*/))) Else ISNULL(TP.SumCarryAPUtil,0) End   
	---- แก้ไขเพื่อให้รองรับกรณีเงินโอนค่านิติผ่าน AP โดยไม่มีค่าบ้านมาปน
	,''Sata_Funds_Niti'' = '''' --Case When ISNULL(TP.SumTransferAPUtil,0)=0 Then (Select Sum(pd.Amount) From ICON_Payment_PaymentDetails pd Left Join ICON_Payment_TmpReceipt tr On tr.TmpReceiptID=pd.TmpReceiptID Left Join icon_payment_payintransfer pp on pp.RCReferent=tr.RCReferent and pp.Amount=tr.Amount Where pd.ReferentID=t.ContractNumber and tr.Method IN(''6'') and pd.PaymentType In(''00'',''2G'',''2H'',''9K'') and pp.BankAccountID In(Select ID From icon_entforms_bankaccount where Isnull(CompanyID,''0'')=''0'')) Else ISNULL(TP.SumTransferAPUtil,0) End
	,''FireMeter_Payment'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''1'',''3'') 
			and pd.PaymentType = ''01'' 
			and tr.IsHouse_Payment = 2),0) 
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN ISNULL(TP.CashAPMeterAmount,0) ELSE 0 END */
'
SET @sql = @sql+ '
	,''FireMeter_CO'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and pd.PaymentType = ''01'' 
			and tr.IsHouse_Payment = 1),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN CASE WHEN TP.MeterFlag = ''1'' THEN Isnull(TP.SumAPMeterAmount,0)-ISNULL(TP.TransferAPMeterAmount,0)-ISNULL(TP.CashAPMeterAmount,0) ELSE 0 END ELSE 0 END */
	,''FireMeter_Cheque'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''2'',''4'',''5'') 
			and pd.PaymentType = ''01'' 
			and tr.IsHouse_Payment = 2),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN CASE WHEN TP.MeterFlag = ''2'' THEN ISNULL(TP.ChequeAPMeterAmount,0) ELSE 0 END ELSE 0 END */
'
SET @sql = @sql+ '
    ,''FireMeter_Transfer'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''6'') 
			and pd.PaymentType = ''01'' 
			and tr.IsHouse_Payment = 2),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN ISNULL(TP.TransferAPMeterAmount,0) ELSE 0 END */
	,''Free_Sata'' = '''' --ISNULL(TF1.SataFee,0)
	,''Free_Water_Fire'' = '''' --ISNULL(TF.FireFee,0)
    ,''Free_Tun'' = '''' --ISNULL(TM2.FundsAmount2,''-'')
	,''Remark'' = '''' --ISNULL(T.Remark,''-'')
    ,''AddressNumber'' = '''' --U.AddressNumber
	,''TransferDateApprove'' = '''' --T.TransferDateApprove
	,''TransferNumber'' = '''' --T.TransferNumber	

FROM [SAL].[Agreement] A' --This is temp table. Actual table start from below 
    /* [ICON_EntForms_Transfer]T LEFT OUTER JOIN
	(
		SELECT T.Transfernumber,T.FireFee,
			''Free'' = CASE WHEN T.FireFee IS NULL  THEN ''-''
						    ELSE''(ฟรีค่ามิเตอร์ไฟฟ้า ''+(convert(varchar(30), convert(money,T.FireFee), 1) )+''บาท)''END
		FROM
		(
			SELECT TransferNumber,
				''FireFee'' = CASE WHEN Code = ''01'' AND ChangeYNH = ''N'' THEN  Amount
								  WHEN Code = ''01'' AND ChangeYNH = ''H''THEN (Amount/2)END
			FROM [ICON_EntForms_TransferFee]
			WHERE ChangeYNH IN (''N'',''H'')
				  AND Code = ''01''
		)T
	)TF ON T.TransferNumber = TF.TransferNumber LEFT OUTER JOIN
	(
		SELECT T.Transfernumber,T.SataFee,
			''Free_Sata'' = CASE WHEN T.SataFee IS NULL  THEN ''-''
						    ELSE''(ฟรีสาธารณูปโภค ''+(convert(varchar(30), convert(money,T.SataFee), 1) )+''บาท)''END
		FROM
		(
			SELECT TransferNumber,
				''SataFee'' = CASE WHEN ChangeYNH = ''N'' THEN  Amount
								  WHEN ChangeYNH = ''H''THEN CEILING(Amount/2) END
			FROM [ICON_EntForms_TransferFee]
			WHERE ChangeYNH IN (''N'',''H'')
				AND Code IN (''00'',''000'')
		)T
	)TF1 ON T.TransferNumber = TF1.TransferNumber LEFT OUTER JOIN
	(
		SELECT TransferNumber,
			''Amount'' = SUM(CASE WHEN Code = ''00'' AND ChangeYNH = ''H'' THEN CEILING(Amount/2) 
								  WHEN Code = ''00'' AND ChangeYNH = ''Y'' THEN Amount END)
		FROM [ICON_EntForms_TransferFee]
		WHERE  Code = ''00''
		GROUP BY TransferNumber
	)TT ON T.TransferNumber = TT.TransferNumber LEFT OUTER JOIN
	(
		SELECT TransferNumber,
			''FundsAmount'' = SUM(CASE WHEN Code = ''2G'' AND ChangeYNH = ''H'' THEN Amount/2 
					  WHEN Code = ''2G'' AND ChangeYNH = ''Y'' THEN Amount END)
		FROM [ICON_EntForms_TransferFee]
		WHERE Code = ''2G''
		GROUP BY TransferNumber
	)TM ON T.TransferNumber = TM.TransferNumber LEFT OUTER JOIN
    (
		SELECT TransferNumber,
			''FundsAmount2'' = SUM(CASE WHEN Code = ''2G'' AND ChangeYNH = ''H'' THEN Amount/2 
					  WHEN Code = ''2G'' AND ChangeYNH = ''N'' THEN Amount END)
		FROM [ICON_EntForms_TransferFee]
		WHERE Code = ''2G'' AND ChangeYNH IN (''N'',''H'')
		GROUP BY TransferNumber
	)TM2 ON T.TransferNumber = TM2.TransferNumber LEFT OUTER JOIN
	[ICON_EntForms_Agreement]A ON T.ContractNumber = A.ContractNumber LEFT OUTER JOIN
	[ICON_EntForms_Products]P ON A.ProductID = P.ProductID LEFT OUTER JOIN
	[ICON_EntForms_Company]C ON P.CompanyID = C.CompanyID LEFT OUTER JOIN
	[ICON_EntForms_Unit]U ON A.ProductID = U.ProductID AND A.UnitNumber = U.UnitNumber LEFT OUTER JOIN
	[ICON_EntForms_TransferPayment] TP ON T.TransferNumber = TP.TransferNumber
WHERE P.ProductType = ''โครงการแนวสูง'' '
if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(P.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(P.ProductID = '''+@ProductID+''')'
if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) = 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
		
set @sql=@sql+'ORDER BY T.TransferDateApprove,A.UnitNumber ASC' */

EXEC(@sql)
--select(@sql)
PRINT(@sql)

GO
