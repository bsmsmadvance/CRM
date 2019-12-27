SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_009] '','40012','A02B08','',NULL,NULL,'Administrator Account'
--[AP2SP_RP_LC_009] '','70020','','','20190101','20190131','Administrator Account'

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_009]
	@CompanyID nvarchar(50) = '',
    @ProductID nvarchar(50) = '',
	@UnitNumber nvarchar(50)= '',
    @SBUID     nvarchar(50) = '',
	@DateStart datetime,
	@DateEnd   datetime,
	@UserName nvarchar(150)
AS
DECLARE @DateEndInStore Datetime

DECLARE @ProjectID NVARCHAR(max)
SET @ProjectID = (SELECT ID FROM PRJ.Project WHERE ProjectNo = @ProductID)

DECLARE @UnitID NVARCHAR(MAX)
SET @UnitID = (SELECT ID FROM PRJ.UNIT WHERE UnitNo = @UnitNumber)
	
DECLARE @UPI Table(Name VARCHAR(20), Amount money)
INSERT INTO @UPI(Name, Amount)
	SELECT	'Name' = UPI.Name 
            , 'Amount' = UPI.Amount
	FROM [SAL].[Booking] B WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[UnitPrice] UP WITH (NOLOCK) ON UP.BookingID = B.ID
    LEFT OUTER JOIN [SAL].[UnitPriceItem] UPI WITH (NOLOCK) ON UPI.UnitPriceID = UP.ID
    WHERE B.UnitID = @UnitID AND UP.IsActive = 1


SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

Declare @sql nvarchar(max)
/* Set @sql='
DECLARE @B Table(Pass money,NoPass money,BookingNumber nvarchar(50))
INSERT INTO @B
SELECT SUM(CASE WHEN Pay.IsReconcile = 1 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS B_PASS
	   ,SUM(CASE WHEN Pay.IsReconcile = 0 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS B_NOPASS
	   ,Pay.BookingNumber
FROM (

		SELECT	CASE WHEN TR.ReconcileDate IS NOT NULL OR TR.Method = ''10'' OR TR.DepositID IS NOT NULL THEN 1 ELSE 0 END AS IsReconcile
				,PD.Amount
				,PD.PaymentType
				,PD.Period,PD.RCReferent,B.BookingNumber
		FROM	Icon_Payment_PaymentDetails PD
				LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
				LEFT OUTER JOIN ICON_EntForms_Booking B ON PD.ReferentID =  B.BookingNumber
		WHERE	PD.PaymentType = ''4'' AND B.CancelDate IS NULL AND TR.CancelDate IS NULL '

		if(Isnull(@ProductID,'')<>'')set @sql=@sql+' AND(TR.ProductID = '''+@ProductID+''')'
		if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+'AND(LTrim(RTrim(TR.UnitNumber)) = '''+@UnitNumber+''')'
		Set @sql=@sql+'
	  )AS Pay

GROUP BY Pay.BookingNumber'

SET @sql=@sql+'
DECLARE @AG Table(Pass money,NoPass money,ReferentID nvarchar(50))
INSERT INTO @AG
SELECT SUM(CASE WHEN Pay.IsReconcile = 1 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS T_PASS
	   ,SUM(CASE WHEN Pay.IsReconcile = 0 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS T_NOPASS
       ,Pay.ReferentID

FROM (
		SELECT	CASE WHEN TR.ReconcileDate IS NOT NULL OR TR.Method = ''10'' OR TR.DepositID IS NOT NULL THEN 1 ELSE 0 END AS IsReconcile
				,PD.Amount,PD.PaymentType
				,PD.Period,PD.RCReferent,PD.ReferentID

		FROM	Icon_Payment_PaymentDetails PD
				LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
				LEFT OUTER JOIN ICON_EntForms_Agreement A ON PD.ReferentID =  A.ContractNumber
				INNER JOIN Icon_EntForms_PaymentDetail PS ON PS.ServiceCode = PD.PaymentType AND ISNULL(PS.Payment,0) IN (''0'',''4'',''6'')

		WHERE  PD.PaymentType NOT LIKE ''TR%'' AND PD.PaymentType NOT IN(''A08'',''4'') AND A.CancelDate IS NULL AND TR.CancelDate IS NULL'

		if(Isnull(@ProductID,'')<>'')set @sql=@sql+' AND(TR.ProductID = '''+@ProductID+''')'
		if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+'AND(LTrim(RTrim(TR.UnitNumber)) = '''+@UnitNumber+''')'
		Set @sql=@sql+'
	 )AS Pay 
GROUP BY Pay.ReferentID '

SET @sql=@sql+'
DECLARE @Down Table(DocumentID nvarchar(50),DocumentType int,FreeDownAmount money)
INSERT INTO @Down
SELECT DocumentID,DocumentType ,FreeDownAmount
FROM dbo.CRM_FreeDown WITH (NOLOCK)
where DocumentType = 2 AND  ISNULL(FreeDownAmount,0) > 0' 

set @sql= @sql+ ' */

--This set is use for temp mapping only actual mapping need to remove this and use line 79
set @sql = '
SELECT  ''TransferNumber'' = T.TransferNo
	   , ''UnitNumber'' = U.UnitNo  --รหัสสินค้า
       , ''TitledeedNumber'' = '''' /* CASE WHEN (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (P.ProductTypeMasterCenterID)) = ''แนวราบ'' THEN [dbo].[fnGenUnitTitledeedNumber] (A.UnitID)  
                                    WHEN (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (P.ProductTypeMasterCenterID)) = ''แนวสูง'' THEN [dbo].[fnGenProductTitledeedNumber] (A.ProjectID) END --เลขที่โฉนด */
       , ''AddressNumber'' = '''' --U.AddressNumber    --บ้านเลขที่
       , ''AreaFromTitledeed'' = '''' --T.LandSize --พื้นที่ตามโฉนด
       , ''AreaSale''= '''' --T.StandardArea --พื้นที่วันทำสัญญา
	   , ''CustomerName'' =  '''' --[dbo].[fn_GenCustTransferAllName] (T.TransferNumber)
       , ''ContractNumber'' = '''' --A.ContractNumber --สัญญาหลัก
	   , ''BankBranch'' = '''' /* CASE WHEN DC.Status = 1 THEN ''CASH/โอนสด''
						ELSE B.AdBankName+''/''+ BB.BranchName END	--กู้ธนาคาร/สาขา */
       , ''TotalSellingPrice'' = '''' --A.SellingPrice   --มูลค่าตามสัญญา
	   , ''Increasing''= '''' --CASE WHEN (T.IncreasingAreaPrice > 0) THEN ISNULL(T.IncreasingAreaPrice,0) END    --ราคาที่ดินเพิ่ม
	   , ''Less''= '''' --CASE WHEN (T.IncreasingAreaPrice < 0) THEN ISNULL(T.IncreasingAreaPrice,0)*-1 END     --ราคาที่ดินลด
       , ''JobIncreasing'' = '''' --ISNULL(T.Extrapayment,0) --เพิ่มวัสดุ
       , ''Cancle'' = '''' --ISNULL(T.ExtraDiscount,0)*-1  --ลดวัสดุ
	   , ''PromotionDiscount''  = '''' --T.PhusaDiscount --ส่วนลด ณ วันโอน
       , ''NetPrice'' = '''' --A.SellingPrice --ราคาตามสัญญา
       , ''NoPass'' = '''' --ISNULL(TB.NoPass,0)+ISNULL(TA.NoPass,0)
       , ''Pass'' = '''' --ISNULL(TB.Pass,0) + ISNULL(TA.Pass,0)
	   , ''TransferPayment'' = '''' --ISNULL(T.TransferPayment,0) --ราคาโอน
	   , ''BorrowBank'' = '''' --ISNULL(CB.LoanAccepted,0)  --กู้ธนาคาร
	   , ''CustomerPay'' = '''' --ISNULL(T.TransferPayment,0)-ISNULL(CB.LoanAcceptedAP,0)--ลูกค้าชำระเอง
	   , ''CompanyID'' = '''' --P.CompanyID
	   , ''CompanyName'' = '''' --CO.CompanyNameThai
       , ''ProductID'' = '''' --P.ProductID
	   , ''ProjectName'' = '''' --P.Project
	   , ''SBUID'' = '''' --P.SBUID
	   , ''SBUName'' = '''' --SB.SBUName
	   , ''NetSalePrice'' = '''' --T.NetSalePrice --ราคาสุทธิ
	   , ''Trasferpayment'' = '''' --ISNULL(T.TransferPayment,0)
	   , ''FreeDown'' = '''' --ISNULL(d.FreeDownAmount,0)

FROM  [SAL].[Transfer] T'  --This is actual table need to use below table as well
	  /* LEFT OUTER JOIN [SAL].[Agreement] A ON T.ContractNumber = A.ContractNumber  
	  LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID AND U.ProjectID = A.ProjectID
	  LEFT OUTER JOIN [PRJ].[Project] P ON P.ProductID = A.ProductID
	  LEFT OUTER JOIN [MST].[Company] CO ON P.CompanyID = CO.CompanyID
	  LEFT OUTER JOIN [ICON_EntForms_DocumentCheckList] DC ON DC.ContractNumber = T.ContractNumber
	  LEFT OUTER JOIN [ICON_EntForms_CreditBanking] CB ON CB.ContractNumber = T.ContractNumber AND CB.IsSelected = 1 AND CB.IsPass = 1 
      LEFT OUTER JOIN [MST].[BankBranch] BB ON BB.BranchID = CB.BranchID AND BB.BankID = CB.BankID
      LEFT OUTER JOIN [MST].[Bank] B ON B.BankID = CB.BankID
      LEFT OUTER JOIN @B TB ON A.BookingNumber = TB.BookingNumber 
	  LEFT OUTER JOIN @AG TA ON A.ContractNumber = TA.ReferentID
	  left outer join @Down d on a.ContractNumber = d.DocumentID

WHERE 1=1 '

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(P.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProjectID,'')<>'')set @sql=@sql+' and(P.ProjectID = '''+@ProjectID+''')'
if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+' and(LTrim(RTrim(U.UnitNo)) = '''+@UnitNumber+''')'
if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	set @sql=@sql+' and (T.ScheduleTransferDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') 
ORDER BY U.UnitNo desc ' */

exec(@sql)
print @sql

--SELECT @sql

GO
