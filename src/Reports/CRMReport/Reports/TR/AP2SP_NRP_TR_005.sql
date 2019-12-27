SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--รายละเอียดการรับชำระเงินโอน
-- [dbo].[AP2SP_NRP_TR_005] NULL,'70013','2015-8-14','Administrator Account'

CREATE PROC [dbo].[AP2SP_NRP_TR_005]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@UserName nvarchar(150)

AS
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateStart)

DECLARE @sql nvarchar(MAX)
Set @sql= '
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SELECT ''CompanyName'' = '''' --C.CompanyNameThai
	,''ProjectName'' = '''' --P.Project
	,''TransferNumber'' = '''' --T.Transfernumber
	,''UnitNumber'' = '''' --A.UnitNumber
    ,''ProductID'' = '''' --A.ProductID
	,''TitleDeedNumber'' = '''' --[dbo].[fnGenUnitTitledeedNumber](U.ProductID,U.UnitNumber)
	,''CustomerName'' = '''' --[dbo].[fn_GenCustTrasferAllNoTitle](T.TransferNumber)
	,''Cash'' = '''' --ISNULL(TM.CashAPAmount,0)
    ,''TransferAP'' = '''' --ISNULL(TM.TransferAPAmount,0)
	,''BankName'' = '''' --ISNULL(TC.AdBankName,''-'')
	,''BranchName'' = '''' --ISNULL(TC.Branch,''-'')
	,''CheqeNumber'' = '''' --TC.BankNumber
	,''AmountInCheque'' = '''' --ISNULL(TC.Amount,0)
	,''DueCheque'' = '''' --TC.DueDate
	,''NetSalePrice'' = '''' --T.NetSalePrice
	,''Meter_Cash'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''1'',''3'') 
			and pd.PaymentType in (''01'',''02'') 
			and tr.IsHouse_Payment = 2),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN ISNULL(TM.CashAPMeterAmount,0) ELSE 0 END */
	,''Meter_CO'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and pd.PaymentType in (''01'',''02'') 
			and (tr.IsHouse_Payment = 1 or Exists(Select * From ICON_Payment_PaymentDetails pp Where pp.RCReferent=pd.RCReferent and pp.TmpReceiptID=pd.TmpReceiptID and pp.PaymentType In(''37'',''15'',''17'')))),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN CASE WHEN TM.MeterFlag = ''1'' THEN ISNULL(TM.SumAPMeterAmount,0)-ISNULL(TM.CashAPMeterAmount,0)-ISNULL(TM.TransferAPMeterAmount,0) ELSE 0 END ELSE 0 END */
	,''Meter_Cheque'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''2'',''4'',''5'') 
			and pd.PaymentType in (''01'',''02'')  
			and (tr.IsHouse_Payment = 2 and Not Exists(Select * From ICON_Payment_PaymentDetails pp Where pp.RCReferent=pd.RCReferent and pp.PaymentType In(''37'',''15'',''17'')))),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN CASE WHEN TM.MeterFlag = ''2'' THEN ISNULL(TM.ChequeAPMeterAmount,0) ELSE 0 END ELSE 0 END */
    ,''Meter_Transfer'' = '''' /* ISNULL((Select SUM(pd.Amount) 
		From ICON_Payment_TmpReceipt tr
		Left Join ICON_Payment_PaymentDetails pd on tr.RCReferent=pd.RCReferent and tr.TmpReceiptID=pd.TmpReceiptID
		Where tr.ReferentID=t.ContractNumber 
			and tr.CancelDate is null
			and tr.PaymentType=''8''
			and tr.Method In(''6'') 
			and pd.PaymentType in (''01'',''02'')  
			and tr.IsHouse_Payment = 2),0)
		+ CASE WHEN TransferPaymentReceivedID IS NULL OR TransferPaymentReceivedID = ''WaitForAppprove'' THEN ISNULL(TM.TransferAPMeterAmount,0) ELSE 0 END */
	,''Free_Water_Fire'' =  '''' --convert(money, TF.Fee, 1) 
	,''Remark'' = '''' --ISNULL(T.Remark,''-'')
	,''WaterAmount'' = '''' --ISNULL(TW.WaterAmount,0)
	,''FireAmount'' = '''' --ISNULL(TT.FireAmount,0)

FROM [SAL].[Agreement] A' --This is temp table actual table start from below
    /* [ICON_EntForms_Transfer]T LEFT OUTER JOIN
	(
		SELECT TC.Amount,TC.Transfernumber,TC.Duedate,TC.Branch,TC.IDCredit,B.AdBankName,TC.BankNumber
		FROM [ICON_EntForms_TransferCheque]TC LEFT OUTER JOIN 
			[ICON_EntForms_Bank]B ON TC.Bank = B.BankID
			Left Join ICON_Payment_TmpReceipt tr on tr.canceldate is null and Isnull(tr.Number,'''')=(tc.BankNumber) and tr.Amount=tc.Amount and Isnull(tr.BankID,'''')=Isnull(TC.Bank,'''') -- เพิ่ม Case กรณี เงินก่อนโอน ที่เป็นเช็คถ้ายังไม่นำฝากจะแสดง
		WHERE TC.ChequeOrder = 0 AND TC.FlagMeter = 0 
		and (Isnull(TC.TransferPaymentType,0)<>1 or (Isnull(TC.TransferPaymentType,0)=1 and tr.DepositID Is Null))
	)TC ON T.TransferNumber = TC.TransferNumber LEFT OUTER JOIN
	(
		SELECT TransferNumber,APAmount
		FROM [ICON_EntForms_TransferPayment]
	)TP ON T.TransferNumber = TP.TransferNumber LEFT OUTER JOIN
	(
		SELECT T.Transfernumber,
			''Fee'' = ISNULL(T.WaterFee,0)+ISNULL(T.FireFee,0)
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
	[ICON_EntForms_TransferPayment]TM ON T.TransferNumber = TM.TransferNumber
WHERE 1=1 AND T.TransferDateApprove IS NOT NULL'
if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(P.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(P.ProductID = '''+@ProductID+''')'
if(((YEAR(@DateStart)<>1800)) AND (ISNULL(@DateStart,'')<>''))
		set @sql=@sql+' AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
		

set @sql=@sql+' ORDER BY A.UnitNumber ASC' */

PRINT(@sql)
EXEC(@sql)
GO
