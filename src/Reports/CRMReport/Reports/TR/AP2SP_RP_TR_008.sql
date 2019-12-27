SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--  [dbo].[AP2SP_RP_TR_008] '','10203','a05',''
ALTER PROC  [dbo].[AP2SP_RP_TR_008]
	@CompanyID nvarchar(100)=''
	,@ProductID nvarchar(100)=''
	,@UnitNumber nvarchar(500)=''
	,@UserName nvarchar(250) = ''
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

SET @UnitNumber = Replace(@UnitNumber,'''','');

/* DECLARE @LandWa MONEY,@PublicFundRate_AP MONEY, @NewPublicPrice MONEY, @NewPublicTotal MONEY, @PublicMonths int;		

SET @LandWa = (SELECT ISNULL(AreaFromPFB,ISNULL(AreaFromRE,0))
			FROM ICON_EntForms_Unit
			WHERE ProductID=isnull(@ProductID,'') AND UnitNumber=isnull(@UnitNumber,''));

SELECT @PublicFundRate_AP = ISNULL(ROUND(PublicFundRate_AP,2),0),@PublicMonths=ISNULL(PublicFundMonths,0)
FROM  [ICON_EntForms_Products]
WHERE ProductID = isnull(@ProductID,'');

SET @NewPublicPrice = @PublicFundRate_AP * @LandWa;
SET @NewPublicTotal = dbo.fnTS_RoundUp(@NewPublicPrice * @PublicMonths);


IF OBJECT_ID('tempdb..#pdAgreement') IS NOT NULL DROP TABLE #pdAgreement
Select Sum(pd.Amount) as Amount,pd.ReferentID as ContractNumber
INTO #pdAgreement
From ICON_Payment_PaymentDetails pd
	left outer join ICON_Payment_TmpReceipt tm on pd.RCReferent=tm.RCReferent and pd.TmpReceiptID=tm.TmpReceiptID
Where pd.PaymentType In('4','5','6')
	AND tm.ProductID=isnull(@ProductID,'')
Group By pd.ReferentID

IF OBJECT_ID('tempdb..#pdBooking') IS NOT NULL DROP TABLE #pdBooking
Select Sum(pd.Amount) as Amount,pd.ReferentID as BookingNumber
INTO #pdBooking
From ICON_Payment_PaymentDetails pd
	left outer join ICON_Payment_TmpReceipt tm on pd.RCReferent=tm.RCReferent and pd.TmpReceiptID=tm.TmpReceiptID
Where pd.PaymentType In('4')
	AND tm.ProductID=isnull(@ProductID,'')
Group By pd.ReferentID

IF OBJECT_ID('tempdb..#pdTransfer') IS NOT NULL DROP TABLE #pdTransfer
Select Sum(pd.Amount) as Amount,pd.ReferentID as ContractNumber
INTO #pdTransfer
From ICON_Payment_PaymentDetails pd
	left outer join ICON_Payment_TmpReceipt tm on pd.RCReferent=tm.RCReferent and pd.TmpReceiptID=tm.TmpReceiptID
Where pd.PaymentType In('8','a06')
	AND tm.ProductID=isnull(@ProductID,'')
Group By pd.ReferentID */

-----------------------------------------------------------------------------


Select 'ModelID' = '' --u.ModelID
,'ProductID' = '' --a.ProductID
,'Project' = '' --p.Project
,'ProjectTel' = '' --p.Project,Isnull(p.Tel,'') ProjectTel
,'UnitNumber' = '' --a.UnitNumber
,'ContractNumber' = '' --a.ContractNumber
,'AddressNumber' = '' --u.AddressNumber
,'DisplayName' = '' --LTrim(Isnull(ao.NamesTitle,'')+' '+Isnull(ao.FirstName,'')+' '+Isnull(ao.LastName,''))DisplayName
,'Phone' = '' --Isnull(ao.Phone,Isnull(ao.Mobile,''))Phone
,'TransferDate' = '' --a.TransferDate
,'LandOfficeName' = '' --vt.LandOfficeName
,'TitledeedNumber' = '' --vt.TitledeedNumber
,'Price' = '' --a.SellingPrice Price
,'StandardArea' = '' --Isnull(b.StandardArea,0)StandardArea
,'AreaFromTitledeed' = '' --Isnull(vt.AreaFromTitledeed,Isnull(b.StandardArea,0))AreaFromTitledeed
,'AreaPrice' = '' --Isnull(a.UnitIncreasingAreaPrice,0)AreaPrice --ราคาพื้นที่ต่อหน่วย
,'Amount' = '' --(a.BookingAmount+a.ContractAmount+a.DownAmount)Amount -- เงิน จอง สัญญา ดาวน์ ที่ต้องชำระ
,'AdditionMaterial' = '' --Isnull((Select Sum(Reward) From ICON_Payment_HousePayment Where ContractID=a.ContractNumber and Revenue='48'),0)AdditionMaterial --เพิ่มวัสดุ
,'DiscountMaterial' = '' --Isnull((Select Sum([Money]) From ICON_Payment_HousePayment Where ContractID=a.ContractNumber and Revenue='48'),0)DiscountMaterial --ส่วนลดวัสุด
,'PaidMaterial' = '' --Isnull((Select Sum(Amount) From ICON_Payment_PaymentDetails Where ReferentID=a.ContractNumber and PaymentType='48'),0)PaidMaterial --เงินเพิ่มวัสดุที่จ่ายมาแล้ว
,'PaidAmount' = '' --Isnull(pdAgreement.Amount,0)+Isnull(pdBooking.Amount,0) PaidAmount -- เงิน จอง สัญญา ดาวน์ ที่ชำระมาแล้ว
,'PaidTransfer' = '' --Isnull(pdTransfer.Amount,0) PaidTransfer -- เงิน โอน ที่ชำระมาแล้ว
,'LoanStatus' = '' --Isnull(dcl.[Status],4)LoanStatus --การขอสินเชื่อ 1=โอนสด 2=กู้เอง 3=กู้ผ่านโครงการ 4=ไม่ตัดสินใจ 5=ลูกค้ารอยกเลิก
,'LoanAcceptedAP' = '' --CASE WHEN ISNULL(bank.IsFreeMortgage,0) = 1 THEN 0 ELSE Isnull(cb.LoanAccepted,0) END LoanAcceptedAP --ยอดอนุมัติจำนอง
,'InsuranceAmount' = '' --Isnull(cb.InsuranceAmount,0)InsuranceAmount --เบี้ยประกัน
,'BankName' = '' --Replace(Isnull(bank.BankName,''),'โอนสด','')BankName
,'BranchName' = '' --Isnull(branch.BranchName,'')BranchName
,'Discount' = '' --Isnull(a.TransferDiscount,0)+Isnull(zt.TransferDiscount,0)+isnull(a.SpacialDiscount,0) Discount --ส่วนลด
,'EstimatePrice' = '' --Isnull(([dbo].[fn_GetEstimatePrice](a.ContractNumber, a.TransferDate, a.SellingPrice)),0)EstimatePrice
,'PriceMeterElectric' = '' --Isnull((Select Top 1 PriceMeterElectric From ICON_EntForms_Model Where ModelID = u.ModelID)
,'ElectricPrice' = '' --IsnulL((Select Top 1 PriceMeterElectric From ICON_EntForms_ManageModel Where ModelID = u.ModelID and ProductID = u.ProductID),0))ElectricPrice
,'PriceMeterWater' = '' --Isnull((Select Top 1 PriceMeterWater From ICON_EntForms_Model Where ModelID = u.ModelID)
,'WaterPrice' = '' --IsnulL((Select Top 1 PriceMeterWater From ICON_EntForms_ManageModel Where ModelID = u.ModelID and ProductID = u.ProductID),0))WaterPrice
,'PublicFundRate' = '' --ISNULL(p.PublicFundRate,0)PublicFundRate--ค่าส่วนกลางต่อหน่วย
,'PublicFundRate_AP' = '' --Isnull(p.PublicFundRate_AP,0)PublicFundRate_AP--ค่าส่วนกลางต่อหน่วย*** ap ช่วยจ่าย
,'PublicFundMonths' = '' --ISNULL((SELECT TOP 1 Y FROM ZPROM_SalePromotionFee WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and PromotionFeeID='00' ),12)
,'SinkingFundRate' = '' --Case When p.ProductType='โครงการแนวสูง' Then Isnull(p.SinkingFundRate,0) Else '' End SinkingFundRate--เงินกองทุน
,'FlagPublicFund' = '' --Isnull((SELECT Top 1 f.Charge FROM ZPROM_TransferPromotionFee F WHERE TransferPromotionID = zt.TransferPromotionID and f.PromotionFeeID='00'), Isnull((SELECT Top 1 f.Charge FROM ZPROM_SalePromotionFee F WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and f.PromotionFeeID='00' ), 'Y')) AS FlagPublicFund --ค่าส่วนกลาง
,'FlagElectric' = '' --Isnull((SELECT Top 1 f.Charge FROM ZPROM_TransferPromotionFee F WHERE TransferPromotionID = zt.TransferPromotionID and f.PromotionFeeID='01'), Isnull((SELECT Top 1 f.Charge FROM ZPROM_SalePromotionFee F WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and f.PromotionFeeID='01' ),'Y'))FlagElectric --ค่ามิเตอร์ไฟฟ้า
,'FlagWater' = '' --Isnull((SELECT Top 1 f.Charge FROM ZPROM_TransferPromotionFee F WHERE TransferPromotionID = zt.TransferPromotionID and f.PromotionFeeID='02'), Isnull((SELECT Top 1 f.Charge FROM ZPROM_SalePromotionFee F WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and f.PromotionFeeID='02' ),'Y'))FlagWater --ค่ามิเตอร์น้ำ
,'FlagTransfer' = '' --Isnull((SELECT Top 1 f.Charge FROM ZPROM_TransferPromotionFee F WHERE TransferPromotionID = zt.TransferPromotionID and f.PromotionFeeID='15'), Isnull((SELECT Top 1 f.Charge FROM ZPROM_SalePromotionFee F WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and f.PromotionFeeID='15' ),'Y'))FlagTransfer --ค่าธรรมเนียมการโอน
,'FlagMortgage' = '' --CASE WHEN ISNULL(bank.IsFreeMortgage,0) = 1 THEN '' ELSE Isnull((SELECT Top 1 f.Charge FROM ZPROM_TransferPromotionFee F WHERE TransferPromotionID = zt.TransferPromotionID and f.PromotionFeeID='17'), isnull((SELECT Top 1 f.Charge FROM ZPROM_SalePromotionFee F WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and f.PromotionFeeID='17' ),'Y')) END FlagMortgage --ค่าจดจำนอง (1% ของเงินกู้)
,'FlagSinkingFund' = '' --Isnull((SELECT Top 1 f.Charge FROM ZPROM_TransferPromotionFee F WHERE TransferPromotionID = zt.TransferPromotionID and f.PromotionFeeID='2G'), Isnull((SELECT Top 1 f.Charge FROM ZPROM_SalePromotionFee F WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and f.PromotionFeeID='2G' ),'Y'))FlagSinkingFund --ค่ากองทุนแรกเข้าเรียกเก็บครั้งเดียว
,'FlagStamp' = '' --Isnull((SELECT Top 1 f.Charge FROM ZPROM_TransferPromotionFee F WHERE TransferPromotionID = zt.TransferPromotionID and f.PromotionFeeID='37'), Isnull((SELECT Top 1 f.Charge FROM ZPROM_SalePromotionFee F WHERE DocumentType = 2  AND DocumentID = a.ContractNumber and f.PromotionFeeID='37' ),'Y'))FlagStamp --ค่าอากรแสตมป์ และค่าพยาน
,'CompanyName' = '' --dbo.fn_GetCompanyNameTH(P.CompanyID, ISNULL(tf.TransferDate,GETDATE())) --,com.CompanyNameThai CompanyName
,'NitiName' = '' --p.BankProjectAP
,'MortgageRate' = '' --ISNULL((SELECT CASE WHEN [ExpireDate]>=A.TransferDate THEN New_MortgageRate ELSE MortgageRate END FROM ICON_EntForms_BOConfiguration),1)
,'TransferFeeRate' = '' --ISNULL((SELECT CASE WHEN [ExpireDate]>=A.TransferDate THEN New_TransferFeeRate ELSE TransferFeeRate END FROM ICON_EntForms_BOConfiguration),2)
,'PublicFund_AP' = '' --CASE WHEN EXISTS(SELECT * FROM ZPROM_SalePromotionFee WHERE DocumentID = A.ContractNumber AND Charge = 'N' AND DocumentType = 2 AND PromotionFeeID = '000') THEN ISNULL(@NewPublicTotal,0) ELSE 0 END
,'PublicFund_ProTrans' = '' --ISNULL((SELECT TOP 1 Amount FROM ZPROM_TransferPromotionFee WHERE Charge = 'N' AND PromotionFeeID = '00' AND TransferPromotionID IN (SELECT TOP 1 TransferPromotionID FROM ZPROM_TransferPromotion WHERE IsApproved2=1 AND IsCancel=0 AND ContractNumber=A.ContractNumber)),ISNULL((SELECT TOP 1 Amount FROM ZPROM_TransferPromotionFee WHERE Charge = 'H' AND PromotionFeeID = '00' AND TransferPromotionID IN (SELECT TOP 1 TransferPromotionID FROM ZPROM_TransferPromotion WHERE IsApproved2=1 AND IsCancel=0 AND ContractNumber=A.ContractNumber))/2,0))
,'Producttype' = '' --p.Producttype

From [SAL].[Agreement] a -- This is main table. Need to use table below as well
/* Left Join ICON_EntForms_Products p on a.ProductID = p.ProductID
Left Join ICON_EntForms_Transfer tf on a.Contractnumber=tf.ContractNumber
Left Join ICON_EntForms_Unit u on a.ProductID = u.ProductID and a.UnitNumber = u.UnitNumber
Left Join ICON_EntForms_AgreementOwner ao on ao.ContractNumber = a.ContractNumber and Isnull(ao.IsDelete,0)=0 and Isnull(ao.Header,0)=1
Left Join vw_ICON_EntForms_TitleDeedDetail vt on vt.ProductID = a.ProductID and a.UnitNumber = vt.UnitNumber
Left Join ICON_EntForms_Booking b on a.BookingNumber = b.BookingNumber
Left Join ICON_EntForms_DocumentCheckList dcl on a.ContractNumber=dcl.ContractNumber
Left Join ICON_EntForms_CreditBanking cb on cb.ContractNumber=a.ContractNumber
Left Join ICON_EntForms_Bank bank on bank.BankID=cb.BankID
Left Join ICON_EntForms_BankBranch branch on branch.BankID=cb.BankID and branch.BranchID=cb.BranchID
Left Join ZPROM_TransferPromotion zt on zt.ContractNumber=a.ContractNumber AND ISNULL(zt.IsCancel,0) = 0
Left Join #pdAgreement pdAgreement on pdAgreement.ContractNumber=a.ContractNumber
Left Join #pdBooking pdBooking on pdBooking.BookingNumber=a.BookingNumber
Left Join #pdTransfer pdTransfer on pdTransfer.ContractNumber=a.ContractNumber

Where a.CancelDate Is Null
and (a.ProductID = @ProductID or Isnull(@ProductID,'')='')
and a.UnitNumber In (select * from dbo.fn_SplitString(@UnitNumber,','))

Order by a.ProductID,a.UnitNumber ASC
	,CB.Isselected DESC
	,Case When CB.Status = 1 Then 7 When CB.Status = 3 Then 8 When CB.Status = 2 Then 9 When CB.Status = 4 Then 10 Else 0 End Asc
	,CB.LoanDate Asc
	,Case when dcl.Status = 1 Then 1 Else 0 End Desc
	,CB.ID Asc */



GO
