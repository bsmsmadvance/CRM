SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- [dbo].[AP2SP_NRP_TR_005_2_H] NULL,'20011','20180517','20180517','Administrator Account'

ALTER PROC [dbo].[AP2SP_NRP_TR_005_2_H]
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
SELECT ''CompanyName'' = '''' --C.CompanyNameThai,
	,''ProjectName'' = '''' --P.Project,
	,''UnitNumber'' = '''' --A.UnitNumber
    ,''AddressNumber'' = '''' --U.AddressNumber,
	,''TransferDateApprove'' = '''' --T.TransferDateApprove,
	,''CustomerName'' = '''' --[dbo].[fn_GenCustTrasferAllNoTitle](T.TransferNumber),
	,''WaterAmount'' = '''' --TW.WaterAmount,
	,''FireAmount'' = '''' --TT.FireAmount,
	,''Meter_Payment'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.tmpreceiptid=pd.tmpreceiptid
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''1'',''3'') 
			and pd.PaymentType in (''01'',''02'') 
			and tr.IsHouse_Payment = 2),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN ISNULL(TP.CashAPMeterAmount,0) ELSE 0 END, */
	,''Meter_CO'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.tmpreceiptid=pd.tmpreceiptid
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8'' 
			and tr.Method In(''2'',''4'',''5'')-- ใช้เฉพาะกรณีที่เป็นเช็คเท่านั้น
			and pd.PaymentType in (''01'',''02'') 
			and (tr.IsHouse_Payment = 1 or Exists(Select * From ICON_Payment_PaymentDetails pp Where pp.RCReferent=pd.RCReferent and pp.PaymentType In(''37'',''15'',''17'')))),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' and TP.MeterFlag = ''1'' THEN ISNULL(TP.APMeterAmount,0) ELSE 0 END, */
	,''Meter_Cheque'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.tmpreceiptid=pd.tmpreceiptid
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''2'',''4'',''5'') 
			and pd.PaymentType in (''01'',''02'')  
			and (tr.IsHouse_Payment = 2 and Not Exists(Select * From ICON_Payment_PaymentDetails pp Where pp.RCReferent=pd.RCReferent and pp.PaymentType In(''37'',''15'',''17'')))),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN CASE WHEN TP.MeterFlag = ''2'' THEN ISNULL(TP.ChequeAPMeterAmount,0) ELSE 0 END ELSE 0 END, */
    ,''Meter_Transfer'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.tmpreceiptid=pd.tmpreceiptid
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''6'') 
			and pd.PaymentType in (''01'',''02'')  
			),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN ISNULL(TP.TransferAPMeterAmount,0) ELSE 0 END, */
	,''Sata_Payment'' = '''' /* Isnull((Select SUM(pd.Amount) 
				From ICON_Payment_TmpReceipt tr
				Left Join ICON_Payment_PaymentDetails pd on tr.TmpReceiptID=pd.TmpReceiptID
				Left Join ICON_Payment_Payment pp on pp.RCReferent=tr.RCReferent and pp.Number=tr.Number and pp.Amount=tr.Amount
				Where tr.ReferentID=t.ContractNumber and tr.Method In(''1'',''3'') and pd.PaymentType IN(''00'',''2G'',''2H'') and Isnull(pp.CompanyID,'''')Not In(Select CompanyID From ICON_EntForms_Company)
				),0)
				+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN ISNULL(TP.SumCashAPUtil,0) ELSE 0 END,--ISNULL(TP.SumCashAPUtil,0), */
	,''Sata_Cheque'' = '''' 
--Isnull((Select Sum(tc.Amount) From ICON_EntForms_TransferCheque tc Where tc.TransferNumber=Tp.TransferNumber and tc.ChequeOrder=1),0),
	,''Sata_Funds_AP'' = '''' /*Isnull((Select Sum(pd.Amount) 
				From  ICON_Payment_TmpReceipt tr
				Left Join ICON_Payment_PaymentDetails pd on tr.TmpReceiptID=pd.TmpReceiptID 
				Where tr.ReferentID=t.ContractNumber and tr.TransferPaymentType=1 and ((tr.NetAmount<>pd.Amount AND Method<>''1'') or (tr.NetAmount=pd.Amount AND Method not in (''1'',''2'')) ) and pd.paymenttype in(''00'',''2G'',''2H'')),0)
		+ (Case when Isnull(t.TransferPaymentReceivedID,'''')<>''WaitForAppprove'' then Isnull((Select Sum(pd.Amount) From ICON_Payment_TmpReceipt tr
					Left Join ICON_Payment_PaymentDetails pd on tr.TmpReceiptID=pd.TmpReceiptID Where tr.ReferentID=t.ContractNumber and tr.TransferPaymentType<>1 and pd.paymenttype in(''00'',''2G'',''2H'')),0) 
				when Isnull(t.TransferPaymentReceivedID,'''')=''WaitForAppprove'' and Isnull(tp.CarryAPUtil,0)=1 Then Isnull(tp.CarryAPUtilAmount,0) Else 0 End), */
    ,''Sata_Funds_Niti'' = '''' --isnull((Select Sum(pd.Amount) From ICON_Payment_PaymentDetails pd Left Join ICON_Payment_TmpReceipt tr On tr.TmpReceiptID=pd.TmpReceiptID Left Join icon_payment_payintransfer pp on pp.RCReferent=tr.RCReferent and pp.Amount=tr.Amount Where pd.ReferentID=t.ContractNumber and tr.Method IN(''6'') and pd.PaymentType In(''00'',''2G'',''2H'',''9K'') and pp.BankAccountID In(Select ID From icon_entforms_bankaccount where Isnull(CompanyID,''0'')=''0'')),0) + ISNULL(TP.SumTransferAPUtil,0), --ISNULL(TP.SumTransferAPUtil,0),
	,''Free_Sata'' = '''' --ISNULL(TF1.SataFee,0),
	,''Free_Water_Fire'' = '''' --ISNULL(TF.FireFee,0),
	,''Remark'' = '''' --ISNULL(T.Remark,''-''),
	,''TransferNumber'' = '''' --T.TransferNumber	

FROM [SAL].[Booking] B' --This is temp table. Actual table start from below
    /* [ICON_EntForms_Transfer]T LEFT OUTER JOIN
	(
		SELECT T.Transfernumber,''FireFee'' = ISNULL(T.FireFee,0)+ISNULL(T.WaterFee,0),
			''Free'' = CASE WHEN T.FireFee IS NULL AND T.WaterFee IS NULL THEN ''-''
						 WHEN T.FireFee IS NULL AND T.WaterFee IS NOT NULL THEN ''(ฟรีค่ามิเตอร์น้ำ ''+(convert(varchar(30), convert(money, T.WaterFee), 1) )+''บาท)''
						 WHEN T.FireFee IS NOT NULL AND T.WaterFee IS  NULL THEN ''(ฟรีค่ามิเตอร์ไฟฟ้า ''+(convert(varchar(30), convert(money,T.FireFee), 1) )+''บาท)''
						 WHEN T.FireFee IS NOT NULL AND T.WaterFee IS NOT NULL THEN ''(ฟรีค่ามิเตอร์น้ำ ''+(convert(varchar(30), convert(money, T.WaterFee), 1) )+'' บาท และ ฟรีค่ามิเตอร์ไฟฟ้า ''+(convert(varchar(29), convert(money,T.FireFee), 1) )+'' บาท)''END
		FROM
		(
           SELECT TF1.TransferNumber,
                  ''FireFee'' = CASE WHEN TF2.Code = ''01'' AND TF2.ChangeYNH = ''N'' THEN TF2.Amount
                                   WHEN TF2.Code = ''01'' AND TF2.ChangeYNH = ''H'' THEN (TF2.Amount/2)END,
                  ''WaterFee'' = CASE WHEN TF3.Code = ''02'' AND TF3.ChangeYNH = ''N'' THEN TF3.Amount
                                    WHEN TF3.Code = ''02'' AND TF3.ChangeYNH = ''H'' THEN (TF3.Amount/2)END
           FROM [ICON_EntForms_Transfer] TF1 
                LEFT OUTER JOIN [ICON_EntForms_TransferFee] TF2 ON TF2.TransferNumber = TF1.TransferNumber AND TF2.Code = ''01'' AND TF2.ChangeYNH IN (''N'',''H'')
                LEFT OUTER JOIN [ICON_EntForms_TransferFee] TF3 ON TF3.TransferNumber = TF1.TransferNumber AND TF3.Code = ''02'' AND TF3.ChangeYNH IN (''N'',''H'')
		)T
	)TF ON T.TransferNumber = TF.TransferNumber LEFT OUTER JOIN
	(
		SELECT T.Transfernumber,T.SataFee,
			''Free_Sata'' = CASE WHEN T.SataFee IS NULL  THEN ''-''
						    ELSE''(ฟรีสาธารณูปโภค ''+(convert(varchar(30), convert(money,T.SataFee), 1) )+''บาท)''END
		FROM
		(
			SELECT TransferNumber,
				''SataFee'' = CASE WHEN Code = ''00'' AND ChangeYNH = ''N'' THEN  Amount
								  WHEN Code = ''00'' AND ChangeYNH = ''H''THEN CEILING(Amount/2) END
			FROM [ICON_EntForms_TransferFee]
			WHERE ChangeYNH IN (''N'',''H'')
				AND Code = ''00''
		)T
	)TF1 ON T.TransferNumber = TF1.TransferNumber LEFT OUTER JOIN
	[ICON_EntForms_Agreement]A ON T.ContractNumber = A.ContractNumber LEFT OUTER JOIN
	[ICON_EntForms_Products]P ON A.ProductID = P.ProductID LEFT OUTER JOIN
	[ICON_EntForms_Company]C ON P.CompanyID = C.CompanyID LEFT OUTER JOIN
	[ICON_EntForms_Unit]U ON A.ProductID = U.ProductID AND A.UnitNumber = U.UnitNumber LEFT OUTER JOIN
	(
		SELECT ''WaterAmount'' = SUM(Amount),TransferNumber
		FROM [ICON_EntForms_Transferfee]
		WHERE Code = ''02''
		GROUP BY TransferNumber
	)TW ON T.TransferNumber = TW.TransferNumber LEFT OUTER JOIN
	(
		SELECT ''FireAmount'' = SUM(Amount),TransferNumber
		FROM [ICON_EntForms_Transferfee]
		WHERE Code = ''01''
		GROUP BY TransferNumber
	)TT ON T.TransferNumber = TT.TransferNumber LEFT OUTER JOIN
	[ICON_EntForms_TransferPayment] TP ON T.TransferNumber = TP.TransferNumber
WHERE P.ProductType = ''โครงการแนวราบ'' '
if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(P.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(P.ProductID = '''+@ProductID+''')'
if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) = 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
		
set @sql=@sql+'ORDER BY T.TransferDateApprove,A.UnitNumber ASC' */
--print(@sql)

EXEC(@sql)


GO
