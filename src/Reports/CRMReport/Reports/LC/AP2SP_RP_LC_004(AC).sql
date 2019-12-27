SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--รายงานรายละเอียดการรับเงิน(บัญชี)
--[AP2SP_RP_LC_004(AC)] 'F','','',NULL,NULL,'2009-10-01','2009-12-31','รับชำระ',NULL,NULL,'15',NULL,NULL,NULL
--[AP2SP_RP_LC_004(AC)] '','10016','',NULL,NULL,'2009-11-01','2009-11-30',NULL,NULL,NULL,NULL,NULL
--[AP2SP_RP_LC_004(AC)] '','10016','04-A12','2009-11-01','2009-11-30','2009-11-01','2009-11-30',NULL,NULL,NULL,NULL,NULL
--[AP2SP_RP_LC_004(AC)] '','10041','B04-A01','','',NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL
--[AP2SP_RP_LC_004(AC)] '','10045','','','',NULL,NULL,'รับชำระ',NULL,NULL,NULL,NULL
--  [AP2SP_RP_LC_004(AC)] '','10152','30C08','','',NULL,NULL,'','',NULL,NULL,NULL,NULL,NULL  

CREATE PROCEDURE [dbo].[AP2SP_RP_LC_004(AC)]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,
	@DateEnd   datetime,
	@DateStart2 datetime,
	@DateEnd2   datetime,
    @Deposit  nvarchar(10),
	@UserName nvarchar(150),
    @Method   nvarchar(30),
	@BankAccount nvarchar(20),
	@ReceivedID nvarchar(4000),
    @DateStart3 datetime,
	@DateEnd3 datetime

AS



DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)
DECLARE @DateEndInStore3 Datetime
SET @DateEndInStore3 = [dbo].[fn_GetMaxDate](@DateEnd3)


Declare @sql nvarchar(max)
Set		@sql= 'SELECT DISTINCT
        ''CompanyID'' = '''' --P.CompanyID
		, ''RDate'' = '''' --TR.RDate    --วันที่ชำระ
		, ''ProductID'' = '''' --P.ProductID
		, ''ShortName'' = '''' --M.TypeOfRealestate --ประเภท
		, ''Bill'' = '''' --RC.PrintingID
		, ''ReceivedID'' = '''' --RC.ReceivedID
        , ''GLBatchID'' = '''' --TR.GLBatchID
		, ''UnitNumber'' = '''' --TR.UnitNumber  --รหัสสินค้า
		, ''CustomerName'' = /* CASE WHEN TR.RefType = 1 THEN ISNULL(BO.FirstName,'''')+'' ''+ISNULL(BO.LastName,'''')
								ELSE ISNULL(AO.FirstName,'''')+'' ''+ISNULL(AO.LastName,'''') END */
		, ''Site'' = '''' --TR.CounterID
		, ''By'' = '''' /* CASE	WHEN TR.Method  = 1 THEN ''CA'' --เงินสด
						WHEN TR.Method  = 2 THEN ''CO'' --แคชเชียร์เช็ค
						WHEN TR.Method IN (4,5) THEN ''CQ'' --เช็ค
						WHEN TR.Method  = 3 THEN ''CR'' --บัตรเครดิต
						WHEN TR.Method  = 6 THEN ''TF'' --เงินโอนบัญชี
						WHEN TR.Method  = 7 THEN ''BP'' --การ์ดลูกหนี้
						WHEN TR.Method  = 8 THEN ''DD'' --ไดเรคเดบิต
						WHEN TR.Method  = 9 THEN ''DC'' --ไดเรคเครดิต
						WHEN TR.Method  = 10 THEN ''YM'' END */
		, ''Amount'' = '''' /* CASE WHEN TR.Method = 1 AND (TR.PaymentType <> ''8'' OR TR.PaymentType IS NULL) THEN TR.Amount
                             WHEN TR.Method = 1 AND TR.PaymentType  = ''8'' AND TR.IsHouse_Payment = 2 THEN ISNULL(TR.Amount ,0)- ISNULL(PD.AmountPD,0)
                             WHEN TR.Method IN(4,5) AND TR.PaymentType  = ''8''AND TR.IsHouse_Payment = 2 THEN TR.Amount
                             WHEN TR.Method = 1 AND TR.PaymentType = ''8'' AND TR.IsHouse_Payment = 1 THEN ISNULL(TR.Amount ,0)
                        ELSE 0 END */
		, ''Cheque1'' = '''' --CASE WHEN TR.Method = 4 THEN TR.Amount ELSE 0 END	--เช็คปัจจุบัน
		, ''Cheque2'' = '''' --CASE WHEN TR.Method = 5 THEN TR.Amount ELSE 0 END		--เช็คล่วงหน้า
		, ''Other'' = '''' --CASE WHEN TR.Method IN (2,3,6,7,8,9,10) THEN TR.Amount ELSE 0 END
		, ''BankBranch''  = '''' --TR.BankID + ''/'' + TR.BranchName
		, ''Number'' = '''' --TR.Number 
		, ''DueDate''  = '''' --TR.DueDate  --วันครบกำหนด  
		, ''Status'' = '''' --CASE	WHEN TR.ReconcileDate IS NOT NULL THEN ''P''
							--ELSE '''' END
		, ''CompanyName'' = '''' --C.CompanyNameThai
		, ''ProjectName'' = '''' --ISNULL(P.ProductID,'''')+''-''+ISNULL(P.Project,'''')       
		, ''Method'' = '''' --TR.Method
        , ''Deposit'' = '''' --DS.DepositDate
        , ''GLDepositBatchID'' = '''' --TR.GLDepositBatchID
        , ''CreateDate'' = '''' --TR.CreateDate
        , ''TmpReceiptID'' = '''' --TR.TmpReceiptID
		,''AccountName'' = '''' --(Select top 1 AccountName From ICON_EntForms_BankAccount acc where acc.BankAccount = TR.Number AND TR.Method IN (''6'',''7'',''8'',''9''))

FROM [SAL].[Booking] B' --This is temp table actual table start from below
    /* [ICON_Payment_TmpReceipt] TR
	  LEFT OUTER JOIN [ICON_Payment_Received] RC ON RC.RCReferent = TR.RCReferent 
      LEFT OUTER JOIN [ICON_Payment_Payment] PP ON PP.RCReferent = TR.RCReferent AND PP.Number = TR.Number AND PP.Amount = TR.Amount 
      LEFT OUTER JOIN ( SELECT SUM(Amount) AS AmountPD,RCReferent FROM ICON_Payment_PaymentDetails WHERE paymenttype IN (''15'',''9P'') GROUP BY RCReferent) PD ON PD.RCReferent = TR.RCReferent 
      LEFT OUTER JOIN [ICON_Payment_Deposit] DS ON DS.DepositID = TR.DepositID AND TR.Method IN (''1'',''2'',''3'',''4'',''5'')
	  LEFT OUTER JOIN [ICON_EntForms_BankAccount] BA1 ON BA1.ID = DS.BankAccountID
	  LEFT OUTER JOIN [ICON_EntForms_BankAccount] BA ON BA.BankAccount = TR.Number AND TR.Method IN (''6'',''7'',''8'',''9'')
      LEFT OUTER JOIN [ICON_EntForms_Bank] BANK ON TR.BankID = BANK.BankID  
	  LEFT OUTER JOIN [ICON_EntForms_Products] P ON P.ProductID = TR.ProductID
	  LEFT OUTER JOIN [ICON_EntForms_Company] C ON C.CompanyID = P.CompanyID
	  LEFT OUTER JOIN [ICON_EntForms_Booking] B ON B.BookingNumber = TR.ReferentID 
	  LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = TR.ReferentID AND ISNULL(BO.IsDelete,0) = 0 AND BO.Header = ''1''
	  LEFT OUTER JOIN [ICON_EntForms_Agreement] A ON A.ContractNumber = TR.ReferentID 
	  LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AO.ContractNumber = A.ContractNumber AND ISNULL(AO.IsDelete,0) = 0 AND AO.Header = ''1'' 
	  LEFT OUTER JOIN [ICON_EntForms_Unit] U ON U.UnitNumber = CASE WHEN TR.RefType = 1 THEN B.UnitNumber ELSE A.UnitNumber END  AND U.ProductID = CASE WHEN TR.RefType = 1 THEN B.ProductID ELSE A.ProductID END
	  LEFT OUTER JOIN [ICON_EntForms_ManageModel] M ON M.ProductID = U.ProductID AND M.ModelID = U.ModelID '

set @sql=@sql+'WHERE TR.CancelDate IS NULL 
	AND TR.Method <> ''10'' 
    AND (TR.IsHouse_Payment IN (1,2) OR (TR.IsHouse_Payment = 0 and TR.Method IN(''2'',''4'',''5'') AND TR.PaymentType = ''8'' AND ISNULL(PP.CompanyID,''H'') <> ''0'')) 
	'
	
IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' and(C.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' and(P.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'')set @sql=@sql+' and(TR.UnitNumber = '''+@UnitNumber+''')'
if(ISNULL(@BankAccount,'')<>'')set @sql=@sql+' and(BA.ID = '''+@BankAccount+''' OR BA1.ID = '''+@BankAccount+''')'
                   set @sql=@sql+' and(TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') ' 

if(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
Begin
set @sql=@sql+'and ((DS.DepositDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' And TR.Method IN (1,2,3,4,5))'
set @sql=@sql+' or(TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' And TR.Method IN (7,8,9))'
set @sql=@sql+' or(TR.DueDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' AND  TR.Method = 6))'
End 

if(YEAR(@DateStart3) <> 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
                   set @sql=@sql+' and(TR.DueDate Between '''+CONVERT(VARCHAR(50),@DateStart3,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''' AND TR.Method IN (2,4,5)) ' 


IF(@Deposit = 'รับชำระ')set @sql=@sql+' and(TR.RDate IS NOT NULL) '
IF(@Deposit = '1')set @sql=@sql+' and(TR.DepositID IS NOT NULL OR TR.ReconcileDate IS NOT NULL) '
IF(@Deposit = '2')set @sql=@sql+'and(TR.DepositID IS NULL AND TR.ReconcileDate IS NULL) '
IF(@Method <> '0')set @sql=@sql+'and(TR.Method IN ('+@Method+')) '
IF(Isnull(@ReceivedID,'''''')<> '''''' And (@ReceivedID <> '''ทั้งหมด''')) set @sql=@sql+' and(RC.ReceivedID IN ('+@ReceivedID+'))' 

set @sql=@sql+' ORDER BY P.ProductID,TR.RDate,RC.ReceivedID,TR.Number ' */

exec(@sql)
--print(@sql)

GO
