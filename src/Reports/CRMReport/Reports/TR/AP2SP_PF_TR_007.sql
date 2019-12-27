SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_PF_TR_007] '60002','12d' 

ALTER PROC [dbo].[AP2SP_PF_TR_007]
	@ProductID nvarchar(50)=''
	,@UnitNumber nvarchar(50)=''
	,@UserName nvarchar(50)=''
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

Declare @TransferNumber nvarchar(50)
Set @TransferNumber=''
If(Isnull(@TransferNumber,'')='')
/* Begin
	Set @TransferNumber = ((Select TransferNumber From ICON_EntForms_Transfer t WITH (NOLOCK) 
							Left Join ICON_EntForms_Agreement a WITH (NOLOCK) On a.ContractNumber=t.ContractNumber
							Where a.CancelDate Is Null 
							and a.ProductID=isnull(@ProductID,'') and a.UnitNumber=isnull(@UnitNumber,'')
							))
End

DECLARE @A Table(MoneyContract money)
INSERT INTO @A(MoneyContract)
	SELECT	'MoneyContract' = ISNULL(SUM(PD.Amount),0)
	FROM	[ICON_Payment_PaymentDetails] PD WITH (NOLOCK) LEFT OUTER JOIN
			[ICON_EntForms_Agreement] A WITH (NOLOCK) ON PD.ReferentID =  A.ContractNumber LEFT OUTER JOIN
			[ICON_EntForms_Transfer] T WITH (NOLOCK) ON A.ContractNumber = T.ContractNumber AND T.TransferNumber = @TransferNumber
	WHERE	T.TransferNumber = @TransferNumber AND PD.PaymentType = '5' 

DECLARE @B Table(MoneyBooking money)
INSERT INTO @B(MoneyBooking)
	SELECT	'MoneyBooking' = ISNULL(SUM(PD.Amount),0)
	FROM	[ICON_Payment_PaymentDetails] PD WITH (NOLOCK) LEFT OUTER JOIN
			[ICON_EntForms_Agreement] A WITH (NOLOCK) ON PD.ReferentID =  A.BookingNumber LEFT OUTER JOIN
			[ICON_EntForms_Transfer] T WITH (NOLOCK) ON A.ContractNumber = T.ContractNumber AND T.TransferNumber = @TransferNumber
	WHERE	T.TransferNumber = @TransferNumber AND PD.PaymentType = '4' 

DECLARE @D Table(MoneyDown money)
INSERT INTO @D(MoneyDown)
	SELECT	'MoneyDown' = ISNULL(SUM(PD.Amount),0)
	FROM	[ICON_Payment_PaymentDetails] PD WITH (NOLOCK) LEFT OUTER JOIN
			[ICON_EntForms_Agreement] A WITH (NOLOCK) ON PD.ReferentID =  A.ContractNumber LEFT OUTER JOIN
			[ICON_EntForms_Transfer] T WITH (NOLOCK) ON A.ContractNumber = T.ContractNumber AND T.TransferNumber = @TransferNumber
	WHERE	T.TransferNumber = @TransferNumber AND PD.PaymentType = '6' 

DECLARE @T Table (MoneyTransfer money)
INSERT INTO @T(MoneyTransfer)
	SELECT	'MoneyTransfer' = ISNULL(SUM(PD.Amount),0)
	FROM	[ICON_Payment_PaymentDetails] PD WITH (NOLOCK) LEFT OUTER JOIN
			[ICON_EntForms_Agreement]A WITH (NOLOCK) ON PD.ReferentID =  A.ContractNumber INNER JOIN
			[ICON_EntForms_Transfer]T WITH (NOLOCK) ON A.ContractNumber = T.ContractNumber AND T.TransferNumber = @TransferNumber
	WHERE	T.TransferNumber = @TransferNumber AND PD.PaymentType IN ('8','87') 

DECLARE @A01 Table(MoneyA01 money)
INSERT INTO @A01(MoneyA01)
	SELECT	'MoneyA01' = ISNULL(SUM(PD.Amount),0)
	FROM	[ICON_Payment_PaymentDetails] PD WITH (NOLOCK) LEFT OUTER JOIN
			[ICON_EntForms_PaymentDetail] PD1 WITH (NOLOCK) ON PD1.ServiceCode = PD.PaymentType  LEFT OUTER JOIN
			[ICON_EntForms_Agreement] A WITH (NOLOCK) ON PD.ReferentID  =  A.ContractNumber LEFT OUTER JOIN
			[ICON_EntForms_Transfer] T WITH (NOLOCK) ON A.ContractNumber = T.ContractNumber AND T.TransferNumber = @TransferNumber
	WHERE	T.TransferNumber = @TransferNumber AND PD1.Payment = '4' */



--Header
Select Top 1	
        'TransferNumber' = '' --TF.TransferNumber
		,'Customer' = '' --[dbo].[fn_GenCustTransferAllName](TF.TransferNumber)--1
		,'ProjectNameHead' = '' --PR.Project--2
		,'TransferDateApprove' = '' --TF.TransferDateApprove--3
		,'TransferDate' = '' --TF.TransferDate
		,'UnitNumber' = '' -- UN.UnitNumber+Isnull(' ('+UN.AddressNumber+') ','')--4+5
--ส่วนที่1
		,'Price' = '' --TF.Price
		,'IncreasingAreaPrice' = '' --TF.IncreasingAreaPrice
		,'PhusaDiscount' = '' --TF.PhusaDiscount
		,'NetTransferAmount' = '' /* (ISNULL(TF.NetSalePrice,0)-Isnull((Select Sum(dd.Amount)
												From ICON_Payment_PaymentDetails dd WITH (NOLOCK) 
												left Join ICON_Payment_TmpReceipt tr WITH (NOLOCK) on tr.TmpReceiptID=dd.TmpReceiptID and dd.RCREferent=tr.RCReferent
												Where dd.ReferentID in(AG.ContractNumber,AG.BookingNumber) and dd.PaymentType In('4','5','6','8','A06')
													and tr.RDate<Isnull(TP.PaymentDate,Getdate())),0))-ISNULL(fd.FreeDownAmount,0)
			AS NetTransferAmount */
		,'NetTransferAmount1' = '' /* CASE	WHEN Isnull(TF.ExtraPayment,0) = 0 AND Isnull(TF.ExtraDiscount,0) = 0 THEN	1
				WHEN Isnull(TF.ExtraPayment,0) = 0 AND Isnull(TF.ExtraDiscount,0) < 0 THEN  2
				WHEN Isnull(TF.ExtraPayment,0) > 0 AND Isnull(TF.ExtraDiscount,0) = 0 THEN  3
				WHEN Isnull(TF.ExtraPayment,0) > 0 AND Isnull(TF.ExtraDiscount,0) < 0 THEN  4 END AS NetTransferAmount1 --6 */
		,'CashAPAmount' = '' --Isnull(TP.SumCashAP,0)CashAPAmount -- TP.CashAPAmount //10092:B21-1 edit date 25/07/2012
		,'SumTransferAP' = '' --Isnull(TP.SumTransferAP,0)SumTransferAP ---เงินสด 11,เงินโอนเข้าบัญชี 12
		,'AccountName' = '' --Isnull(BC.AccountName,'')AccountName ---13
		,'ChequeAPEexceedPSAmount' = '' --Isnull(TP.ChequeAPEexceedPSAmount,0)ChequeAPEexceedPSAmount  --18
		,'ChequeAPAmount' = '' /* Isnull((Select Sum(Amount) From icon_entforms_transfercheque tc WITH (NOLOCK) where tc.CompanyID Not In('Z','0') and tc.transfernumber=TF.TransferNumber and (Isnull(tc.TransferPaymentType,0)<>1 or Not Exists(select * from icon_payment_tmpreceipt t
																																																		left join icon_payment_paymentdetails pd on t.TmpReceiptID=pd.TmpReceiptID
																																																		where t.referentid=AG.ContractNumber and t.CancelDate Is Null and t.Method='2' and t.Number=tc.BankNumber and pd.PaymentType In('8','6','A6') ))),0) ChequeAPAmount */ --24  TP.SumChequeAP 
--ส่วนที่2
		,'SumCashAPUtil'= '' /* Isnull(TP.SumCashAPUtil,0) --+ Isnull(TP.SumCashAP,0) -- 10062-07B10 เงินสดค่าใช้จ่ายแสดงเกิน
			+Isnull((Select Sum(dd.Amount)
					From ICON_Payment_PaymentDetails dd WITH (NOLOCK) 
					left Join ICON_Payment_TmpReceipt tr WITH (NOLOCK) on tr.TmpReceiptID=dd.TmpReceiptID and dd.RCREferent=tr.RCReferent
					Where dd.ReferentID=TF.ContractNumber and tr.Method='1' and Isnull(tr.TransferPaymentType,0)=1 and dd.PaymentType In('15','17','9P','37','01','02','2G','00')
					and Not Exists(Select * From ICON_Payment_PaymentDetails pd WITH (NOLOCK)  WHERE pd.TmpReceiptID=tr.TmpReceiptID and pd.PaymentType In('6','8','A06'))
					and tr.RDate<Isnull(TP.PaymentDate,Getdate())),0) --,TP.SumCashAPUtil --27 */
		,'SumTransferAPUtil' = '' --Isnull(TP.SumTransferAPUtil,0)  --28
		,'AccountNameUtil' = '' --Isnull(BC1.AccountName,'') --29
		,'SumChequeAPUtil' = '' /* Isnull(TP.SumChequeAPUtil,0)
			+Isnull((Select Sum(Amount) From icon_entforms_transfercheque tc WITH (NOLOCK) where (tc.CompanyID In('Z') and tc.transfernumber=TF.TransferNumber)),0) 
			+Isnull((Select SUM(pd.Amount) From ICON_Payment_PaymentDetails pd WITH (NOLOCK) 
				Left Join [ICON_Payment_TmpReceipt]tr WITH (NOLOCK) on pd.TmpReceiptID=tr.TmpReceiptID 
				Where tr.Method='2' and Isnull(tr.IsHouse_Payment,0)=0 and tr.referentid =tf.ContractNumber and pd.PaymentType Not In('6','8','A06')
				and tr.RDate<Isnull(TP.PaymentDate,Getdate())),0) */
		 --34
--ส่วนที่3
		,'ValueMortgage' = '' --Isnull(TP.landtotalamount,0) ValueMortgage --36
		,'TransferfeetotalAmount' = '' --Isnull(TP.customerfeeamount,0) TransferfeetotalAmount --37
	
		,'Fire' = '' /* ISNULL((SELECT Top 1 CASE WHEN ChangeYNH = 'Y' THEN t.Amount 
							WHEN ChangeYNH = 'H' THEN (t.Amount/2) Else t.Amount END  							
                          FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                          WHERE TransferNumber = @TransferNumber AND Code = '01'),0) */ ---ค่ามิเตอร์ไฟฟ้า 7

		,'Water' = '' /* ISNULL((SELECT Top 1 CASE 
WHEN ChangeYNH = 'Y' THEN t.Amount
WHEN ChangeYNH = 'H' THEN (t.Amount/2) Else t.Amount END  
                           FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                           WHERE TransferNumber = @TransferNumber AND Code ='02'),0) */---ค่ามิเตอร์น้ำ 8

		,'SinkingFundBath' = '' /* ISNULL((SELECT Top 1  CASE 
WHEN ChangeYNH = 'Y' THEN t.Amount
WHEN ChangeYNH = 'H' THEN (t.Amount/2) Else t.Amount END
                                     FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                                     WHERE TransferNumber = @TransferNumber AND Code ='2G'),0)---ค่าบริการสาธารณะ,ค่ากองทุน(เฉพาะแนวสูง) 9 */

		,'PublicFundMoney' = '' /* CEILING(ISNULL((SELECT Top 1 CASE 
WHEN ChangeYNH = 'Y' THEN t.Amount
WHEN ChangeYNH = 'H' THEN (t.Amount/2) Else t.Amount END
                                     FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                                     WHERE TransferNumber = @TransferNumber AND Code ='00'),0))---ค่าส่วนกลาง 9 */

		,'RemainingCashAmount' = '' --Isnull(Case When Isnull(tp.RemainingTotal,0)=1 Then RemainingTotalAmount Else 0 End ,0)RemainingCashAmount
		,'RemainingChequeAmount' = '' --Isnull(Case When Isnull(tp.RemainingTotal,0)=0 Then RemainingTotalAmount Else 0 End ,0)RemainingChequeAmount
		,0.00 AS RemainingTransferAmount --เงินทอนเงินโอน 42
		,'AttornyNameTransfer' = '' --Isnull(PR.AttornyNameTransfer,0)AttornyNameTransfer --10
		,'Producttype' = '' --PR.Producttype
		,'FreeFire' = '' --Isnull((Select Top 1 Case When ChangeYNH='N' Then 'ฟรี' Else '' End From ICON_EntForms_TransferFee t WITH (NOLOCK) Where t.Code='01' And t.TransferNumber=tf.TransferNumber),'')FreeFire
		,'FreeWater' = '' --Isnull((Select Top 1 Case When ChangeYNH='N' Then 'ฟรี' Else '' End From ICON_EntForms_TransferFee t WITH (NOLOCK) Where t.Code='02' And t.TransferNumber=tf.TransferNumber),'')FreeWater
		,'FreeValueMortgage' = '' --Isnull((Select Top 1 Case When ChangeYNH='N' Then 'ฟรี' Else '' End From ICON_EntForms_TransferFee t WITH (NOLOCK) Where t.Code in('17','37') And t.TransferNumber=tf.TransferNumber order by ChangeYNH desc),'')FreeValueMortgage
		,'FreePublicFund' = '' --Isnull((Select Top 1 Case When ChangeYNH='N' Then 'ฟรี' Else '' End From ICON_EntForms_TransferFee t WITH (NOLOCK) Where t.Code='00' And t.TransferNumber=tf.TransferNumber),'')FreePublicFund
		,'FreeSinkingFundBath' = '' --Isnull((Select Top 1 Case When ChangeYNH='N' Then 'ฟรี' Else '' End From ICON_EntForms_TransferFee t WITH (NOLOCK) Where t.Code='2G' And t.TransferNumber=tf.TransferNumber),'ฟรี')FreeSinkingFundBath
		,'FreeTransferfee' = '' --Isnull((Select Top 1 Case When ChangeYNH='N' Then 'ฟรี' Else '' End From ICON_EntForms_TransferFee t WITH (NOLOCK) Where t.Code='15' And t.TransferNumber=tf.TransferNumber),'')FreeTransferfee
		
,'TotalFire' = '' /* ISNULL((SELECT TOp 1 CASE WHEN ChangeYNH In ('Y','N') THEN Amount WHEN ChangeYNH = 'H' THEN (Amount/2) END  
                          FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                          WHERE TransferNumber = @TransferNumber AND Code = '01'),0) */
,'TotalWater' = '' /* ISNULL((SELECT TOp 1 CASE WHEN ChangeYNH In ('Y','N') THEN Amount WHEN ChangeYNH = 'H' THEN (Amount/2) END  
                          FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                          WHERE TransferNumber = @TransferNumber AND Code = '02'),0) */
,'TotalPublicFund' = '' /* CEILING(ISNULL((SELECT TOp 1 CASE WHEN ChangeYNH In ('Y','N') THEN Amount WHEN ChangeYNH = 'H' THEN (Amount/2) END  
                          FROM ICON_EntForms_TransferFee t WITH (NOLOCK)  
                          WHERE TransferNumber = @TransferNumber AND Code = '00'),0)) */
,'TotalPublicFund1' = '' /* ISNULL((SELECT TOp 1 CASE WHEN ChangeYNH In ('Y','N') THEN Amount WHEN ChangeYNH = 'H' THEN (Amount/2) END  
                          FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                          WHERE TransferNumber = @TransferNumber AND Code = '2G' AND PR.ProductType='โครงการแนวสูง'),0) */
,'TotalValueMortgage' = '' /* ISNULL((SELECT TOP 1 CASE WHEN ChangeYNH IN ('Y','N') THEN Amount WHEN ChangeYNH = 'H' THEN (Amount/2) END  
                          FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                          WHERE TransferNumber = @TransferNumber AND Code = '17'),0) */
,'TotalTransferfee' = '' /* ISNULL((SELECT TOp 1 CASE WHEN ChangeYNH In ('Y','N') THEN Amount WHEN ChangeYNH = 'H' THEN (Amount/2) END  
                          FROM ICON_EntForms_TransferFee t WITH (NOLOCK) 
                          WHERE TransferNumber = @TransferNumber AND Code = '15'
							),0) */

-- Change for 10080-D05 15/06/2012 ไม่ต้องแสดงค่านิติ และ ต้องมีค่าบ้านปนอยู่ด้วย
,'BeforeTransferAmount' = '' /* Isnull((Select Sum(dd.Amount)
		From ICON_Payment_PaymentDetails dd WITH (NOLOCK) 
		left Join ICON_Payment_TmpReceipt tr WITH (NOLOCK) on tr.TmpReceiptID=dd.TmpReceiptID and dd.RCREferent=tr.RCReferent
		Where dd.ReferentID=TF.ContractNumber 
			AND Isnull(tr.TransferPaymentType,0)=1 
			AND dd.PaymentType In('15','17','9P','37','00','01','02','2G','9K')
			AND (EXISTS(Select * From ICON_Payment_PaymentDetails pd WITH (NOLOCK) Where pd.TmpReceiptID=tr.TmpReceiptID and pd.PaymentType In('6','8','A06'))
					Or (tr.Method='6'
						And Not EXISTS(Select * From ICON_Payment_PaymentDetails pd WITH (NOLOCK) Where pd.TmpReceiptID=tr.TmpReceiptID and pd.PaymentType In('6','8','A06'))
					)
			)
-- ยกเลิกเงื่อนไขการตรจสอบ ต้องมีค่าบ้านปนอยู่ด้วย      03/04/2017 นน
-- แก้ไขกลับ      11/04/2017 นน  60005/14B8 เนื่องจากมีแสดงเงินรับก่อนโอนเกินไป
			),0) 
  --@@@รวมค่าใช้จ่ายอื่นๆที่ไม่ได้ปนมากับค่าบ้าน By เสริม 9/1/60
  + Isnull((Select Sum(dd.Amount)
		From ICON_Payment_PaymentDetails dd WITH (NOLOCK) 
		left Join ICON_Payment_TmpReceipt tr WITH (NOLOCK) on tr.TmpReceiptID=dd.TmpReceiptID and dd.RCREferent=tr.RCReferent
		Where dd.ReferentID=TF.ContractNumber 
			AND Isnull(tr.TransferPaymentType,0)=1 
			And tr.Method<>'6'--ถ้าเป็นเงินโอนผ่าน ธ ไม่นับ เนื่องจากนับจากข้างบนไปแล้ว
			AND dd.PaymentType In('15','17','9P','37','01','02')
			AND NOT EXISTS(Select * From ICON_Payment_PaymentDetails pd WITH (NOLOCK) Where pd.TmpReceiptID=tr.TmpReceiptID and pd.PaymentType In('6','8','A06','00','2G'))),0) AS BeforeTransferAmount */

,'LoanAmount' = '' --Isnull(CB.LoanAccepted,0)
,'MortgageRate' = '' --ISNULL((SELECT CAST(X AS Float) FROM ICON_EntForms_TransferFee WITH (NOLOCK) WHERE TransferNumber = TF.TransferNumber  AND Code ='17'),1)
,'TransferFeeRate' = '' -- ISNULL((SELECT CAST(X AS Float) FROM ICON_EntForms_TransferFee WITH (NOLOCK) WHERE TransferNumber = TF.TransferNumber  AND Code ='15'),2)

FROM	[SAL].[Booking] B --This is temp table. Need to use actual table start from below
        /* [ICON_EntForms_Transfer] TF WITH (NOLOCK) 
		LEFT OUTER JOIN [ICON_EntForms_TransferOwner] TW WITH (NOLOCK) ON TF.TransferNumber = TW.TransferNumber 
		LEFT OUTER JOIN [ICON_EntForms_Agreement] AG WITH (NOLOCK) ON TF.ContractNumber = AG.ContractNumber 
		LEFT OUTER JOIN [ICON_EntForms_Unit] UN WITH (NOLOCK) ON AG.UnitNumber = UN.UnitNumber AND AG.ProductID = UN.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_Products] PR WITH (NOLOCK) ON UN.ProductID = PR.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_Company] CO WITH (NOLOCK) ON PR.CompanyID = CO.CompanyID 
		LEFT OUTER JOIN [ICON_EntForms_CreditBanking] CB WITH (NOLOCK) ON AG.ContractNumber = CB.ContractNumber AND CB.IsSelected = 1 AND CB.IsPass = 1 
		LEFT OUTER JOIN [ICON_EntForms_TransferPayment] TP WITH (NOLOCK) ON TF.TransferNumber = TP.TransferNumber
		LEFT OUTER JOIN 
		(	
			SELECT  TOP 1 TF.TransferNumber,BA.AccountName
			FROM	[ICON_EntForms_Transfer] TF WITH (NOLOCK) 
					LEFT OUTER JOIN [ICON_EntForms_TransferPayment] TP WITH (NOLOCK) ON TF.TransferNumber = TP.TransferNumber
					LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR WITH (NOLOCK) ON TR.RDate = TP.PaymentDate AND TR.Amount = TP.SumTransferAP
					LEFT OUTER JOIN [ICON_Entforms_BankAccount] BA WITH (NOLOCK) ON BA.BankAccount = TR.Number
			WHERE	TF.TransferNumber = @TransferNumber AND TR.Method = '6' AND TR.IsHouse_Payment = 1
		)AS BC ON BC.TransferNumber = TF.TransferNumber
		LEFT OUTER JOIN 
		(	
			SELECT  TOP 1 TF.TransferNumber,BA.AccountName
			FROM	[ICON_EntForms_Transfer] TF WITH (NOLOCK) 
					LEFT OUTER JOIN [ICON_EntForms_TransferPayment] TP WITH (NOLOCK) ON TF.TransferNumber = TP.TransferNumber
					LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR WITH (NOLOCK)  ON TR.RDate = TP.PaymentDate AND TR.Amount = TP.SumCashAPUtil
					LEFT OUTER JOIN [ICON_Entforms_BankAccount] BA WITH (NOLOCK)  ON BA.BankAccount = TR.Number
			WHERE	TF.TransferNumber = @TransferNumber AND TR.Method = '6' AND TR.IsHouse_Payment = 0
		)AS BC1 ON BC1.TransferNumber = TF.TransferNumber
		LEFT OUTER JOIN (SELECT DocumentID,DocumentType ,FreeDownAmount
						FROM dbo.CRM_FreeDown WITH (NOLOCK)
						where DocumentType = 2 AND ISNULL(FreeDownAmount,0)>0  ) fd ON fd.DocumentID = ag.ContractNumber

WHERE	TF.TransferNumber = @TransferNumber */


GO
