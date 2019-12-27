SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--รายงานยอดคงเหลือ เงินสด เช็ค และบัตรเคตดิต
--[AP2SP_RP_AR_014] '','10089','22NW01','20120930',NULL
--[AP2SP_RP_AR_014] 'L','10041','B05-C02','2009-10-31',NULL
--[AP2SP_RP_AR_014] 'L','','','2009-10-31','Administrator Account'
CREATE PROCEDURE [dbo].[AP2SP_RP_AR_014]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,
	@UserName nvarchar(150)

AS
IF @DateStart Is Null  Set @DateStart=GetDate()
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateStart)

Declare @sql nvarchar(max)
Set		@sql= '
		SELECT	DISTINCT 
        ''CompanyID'' = '''' --P.CompanyID
		, ''CompanyName'' = '''' --C.CompanyNameThai
		, ''ProductID'' = '''' --P.ProductID
		, ''ProjectName'' = '''' --ISNULL(P.Project,'''') 
		, ''RDate'' = '''' --TR.RDate    --วันที่ชำระ      
		, ''ReceivedID = '''' --RC.ReceivedID
		, ''UnitNumber'' = '''' --TR.UnitNumber  --รหัสสินค้า
		, ''PayBy'' = '''' --CASE	WHEN TR.Method  = 1 THEN ''CA'' --เงินสด
						   -- WHEN TR.Method  = 2 THEN ''CO'' --แคชเชียร์เช็ค
						   -- WHEN TR.Method IN (4,5) THEN ''CQ'' --เช็ค
						   -- WHEN TR.Method  = 3 THEN ''CR'' END  --บัตรเครดิต 
        , ''Cash'' = '''' --CASE WHEN TR.Method = 1 AND (TR.PaymentType <> ''8'' OR TR.PaymentType IS NULL) THEN TR.Amount
                          --WHEN TR.Method = 1 AND TR.PaymentType  = ''8'' AND TR.IsHouse_Payment = 2 THEN ISNULL(TR.Amount,0) - ISNULL(PD.AmountPD,0)
                          --WHEN TR.Method IN(4,5) AND TR.PaymentType  = ''8''AND TR.IsHouse_Payment = 2 THEN TR.Amount
                          --WHEN TR.Method = 1 AND TR.PaymentType = ''8'' AND TR.IsHouse_Payment = 1 THEN ISNULL(TR.Amount ,0)
--WHEN TR.Method = 1 THEN TR.Amount
--                     ELSE 0 END
        , ''ChequeCash'' = '''' --CASE WHEN TR.Method = 2 THEN TR.Amount ELSE 0 END
        , ''ChequePresent'' = '''' --CASE WHEN TR.Method = 4 THEN TR.Amount ELSE 0 END	--เช็คปัจจุบัน					
		, ''ChequePrepaid'' = '''' --CASE WHEN TR.Method = 5 THEN TR.Amount ELSE 0 END		--เช็คล่วงหน้า
        , ''CreditCard'' = '''' --CASE WHEN TR.Method = 3 THEN TR.Amount ELSE 0 END
		
        , ''BankBranch''  = '''' --BANK.AdBankName + ''/'' + TR.BranchName
		, ''Number'' = '''' --TR.Number 
        , ''GLBatchID'' = '''' --TR.GLBatchID
        , ''IsHouse_Payment'' = '''' --TR.IsHouse_Payment

FROM [SAL].[Agreement] A ' --This is temp table actual table start from below
 /* [ICON_Payment_TmpReceipt] TR
	  LEFT OUTER JOIN [ICON_Payment_Received] RC ON RC.RCReferent = TR.RCReferent 
      LEFT OUTER JOIN [ICON_Payment_Payment] PP ON PP.RCReferent = TR.RCReferent AND TR.Amount = PP.Amount AND ISNULL(PP.Number,'''') = ISNULL(TR.Number,'''') 
      LEFT OUTER JOIN
      (
		  --SELECT SUM(Amount) AS AmountPD,RCReferent 
		  --FROM ICON_Payment_PaymentDetails 
		  --WHERE paymenttype IN (''15'',''9P'',''17'') 
		  --GROUP BY RCReferent
		  SELECT SUM(a.Amount) AS AmountPD,a.RCReferent 
		  FROM ICON_Payment_PaymentDetails a
		Left Join ICON_Payment_TmpReceipt b on a.RCReferent=b.RCReferent and a.tmpreceiptid=b.tmpreceiptid
		  Left Join ICON_Payment_Payment c on c.RCReferent=b.RCReferent and c.Amount=b.Amount
		  WHERE a.paymenttype IN (''15'',''9P'',''17'')  and c.CompanyID Not in(''Z'')
		  GROUP BY a.RCReferent
	  ) PD ON PD.RCReferent = TR.RCReferent 
      LEFT OUTER JOIN [ICON_Payment_Deposit] DS ON DS.DepositID = TR.DepositID
      LEFT OUTER JOIN [ICON_EntForms_Bank] BANK ON TR.BankID = BANK.BankID 
	  LEFT OUTER JOIN [ICON_EntForms_Products] P ON P.ProductID = TR.ProductID
	  LEFT OUTER JOIN [ICON_EntForms_Company] C ON C.CompanyID = P.CompanyID

WHERE TR.CancelDate IS NULL 
	AND TR.Method IN (''1'',''2'',''3'',''4'',''5'') 
	AND ISNULL(PP.CompanyID,''H'') Not In (''Z'',''0'') 
	AND (TR.IsHouse_Payment = ''1'' OR (TR.IsHouse_Payment IN (''2'',''0'') AND TR.Method IN(''2'',''4'',''5'') AND TR.PaymentType = ''8'' AND ISNULL(PP.CompanyID,'''') NOT IN (''0'',''Z''))
		OR (TR.IsHouse_Payment IN (''2'',''0'') AND TR.Method IN(''1'') AND TR.TmpReceiptID IN(Select TmpReceiptID From ICON_Payment_PaymentDetails Where PaymentType In(''01'',''02'')) AND ISNULL(PP.CompanyID,'''') NOT IN (''0'',''Z''))
		OR (ISNULL(TR.TransferPaymentType,0) IN (''1'',''2'') AND TR.DepositID Is Null and TR.IsHouse_Payment in (2,0) and TR.Method IN(''1'') and TR.PaymentType = ''8'' AND ISNULL(PP.CompanyID,''H'') Not In (''0'',''Z'')))
	AND (TR.Method Not IN(''2'',''4'',''5'') or Isnull(TR.Number,'''')='''' or (TR.Method IN(''2'',''4'',''5'') and TR.Number Not in (Select BankNumber From ICON_EntForms_TransferCheque tc Left Join ICON_EntForms_Transfer t on tc.TransferNumber=t.TransferNumber Where t.ContractNumber=tr.ReferentID and tc.ChequeOrder<>0 ))) 
	--AND P.ProductID IN (SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](''' + @UserName + '''))  
	'

IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' and(C.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' and(P.ProductID = '''+@ProductID+''')'


IF(ISNULL(@UnitNumber,'')<>'')set @sql=@sql+' and(TR.UnitNumber = '''+@UnitNumber+''')'
--set @sql=@sql+' and(TR.RDate >= '''+CONVERT(VARCHAR(50),'2009-10-01',120)+''')'
if(YEAR(@DateStart) <> 1800) AND ISNULL(@DateStart,'')<>'' 
                   set @sql=@sql+' and(TR.RDate BETWEEN '''+CONVERT(VARCHAR(50),'2009-10-01',120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') ' 
if(YEAR(@DateStart) <> 7000) AND ISNULL(@DateStart,'')<>''
                   set @sql=@sql+' and((DS.DepositDate >= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')OR TR.DepositID IS NULL) '

-- ถ้าเป็น เงินสด จ่ายที่ดิน หลังโอน ไม่ต้องแสดงรายการ
set @sql=@sql+' and (TR.Method Not In(''1'')or Isnull(tr.TransferPaymentType,0)<>2 OR (TR.Method In(''1'') and Isnull(tr.TransferPaymentType,0)=2 and Tr.Amount<>Isnull((SELECT SUM(Amount)  
																											  FROM ICON_Payment_PaymentDetails pp
																											  WHERE paymenttype IN (''15'',''9P'',''17'') and pp.TmpReceiptID=tr.TmpReceiptID
																											  ),0) 
								)
)'
set @sql=@sql+' ORDER BY P.ProductID,TR.RDate,RC.ReceivedID ' */

exec(@sql)
--print(@sql)

GO
