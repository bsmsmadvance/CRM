SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[AP2SP_RP_LC_004] '','60004','','2016-12-01','2016-12-30','','','','',N'',N''  
 
ALTER PROCEDURE [dbo].[AP2SP_RP_LC_004]  
	@CompanyID nvarchar(50),  
	@ProductID nvarchar(50),  
	@UnitNumber nvarchar(50),  
	@DateStart datetime,  
	@DateEnd   datetime,  
	@DateStart2 datetime,  
	@DateEnd2   datetime,  
	@Deposit  nvarchar(10),  
	@UserName nvarchar(150),  
	@Method nvarchar(50)='',
	@PaymentType nvarchar(50)=''
  
AS  
  
DECLARE @DateEndInStore Datetime  
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)  
  
DECLARE @DateEndInStore2 Datetime  
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)  
Declare @sql nvarchar(max)  
Set  @sql= '
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SELECT	''CompanyID'' = '''' --P.CompanyID   
		, ''CompanyName'' = '''' --C.CompanyNameThai  
		, ''ProductID'' = '''' --P.ProductID  
		, ''ProjectName'' = '''' --ISNULL(P.ProductID,'''')+''-''+ISNULL(P.Project,'''')         
		, ''ShortName'' = '''' --M.TypeOfRealestate --ประเภท  
		, ''Bill'' = '''' --Pay.PrintingID  
		, ''ReceivedID'' = '''' --Pay.ReceivedID  
		, ''RDate'' = '''' --Pay.RDate    --วันที่ชำระ  
		, ''Period'' = '''' /* CASE Pay.PaymentType WHEN ''4'' THEN ''จอง''  
											WHEN ''5'' THEN ''สญ''  
											WHEN ''6'' THEN RIGHT(''00''+CONVERT(nvarchar(5),Pay.Period),3)--CAST(DC.Period As nvarchar(10))   
											WHEN ''8'' THEN ''โอน''   
											ELSE Pay.PaymentType END */  --Pay.Detail
		, ''UnitNumber'' = '''' --U.UnitNumber  --รหัสสินค้า  
		, ''CustomerName'' = '''' /* CASE	WHEN (Pay.RefType = 1 AND Pay.Paymenttype = ''4'') OR Pay.TransferPaymentType=''3'' THEN ISNULL(BO.FirstName,'''')+'' ''+ISNULL(BO.LastName,'''')  
									ELSE ISNULL(AO.FirstName,'''')+'' ''+ISNULL(AO.LastName,'''') END  */
		, ''Site'' = '''' --Pay.CounterID   
		, ''By'' = ''''/* CASE WHEN Pay.Method  = 1 THEN ''CA'' --เงินสด  
						WHEN Pay.Method  = 2 THEN ''CO'' --แคชเชียร์เช็ค  
						WHEN Pay.Method IN (4,5) THEN ''CQ'' --เช็ค  
						WHEN Pay.Method  = 3 THEN ''CR'' --บัตรเครดิต  
						WHEN Pay.Method  = 6 THEN ''TF'' --เงินโอนบัญชี  
						WHEN Pay.Method  = 7 THEN ''BP'' --การ์ดลูกหนี้  
						WHEN Pay.Method  = 8 THEN ''DD'' --ไดเรคเดบิต  
						WHEN Pay.Method  = 9 THEN ''DC'' --ไดเรคเครดิต  
						WHEN Pay.Method  = 10 THEN ''YM'' END */
		, ''Amount'' =  '''' --CASE WHEN (Pay.Method = 1) THEN CASE WHEN ISNULL(Pay.TAmount,0) < ISNULL(Pay.PAmount,0) THEN ISNULL(Pay.TAmount,0) ELSE ISNULL(Pay.PAmount,0) END ELSE 0 END   
		, ''Cheque1'' = '''' --CASE WHEN (Pay.Method = 4) THEN CASE WHEN ISNULL(Pay.TAmount,0) < ISNULL(Pay.PAmount,0) THEN ISNULL(Pay.TAmount,0) ELSE ISNULL(Pay.PAmount,0) END ELSE 0 END       
		, ''Cheque2'' = '''' --CASE WHEN (Pay.Method = 5) THEN CASE WHEN ISNULL(Pay.TAmount,0) < ISNULL(Pay.PAmount,0) THEN ISNULL(Pay.TAmount,0) ELSE ISNULL(Pay.PAmount,0) END ELSE 0 END   
		, ''Other'' = '''' --CASE WHEN Pay.Method IN (2,3,6,7,8,9,10) THEN CASE WHEN ISNULL(Pay.TAmount,0) < ISNULL(Pay.PAmount,0) THEN ISNULL(Pay.TAmount,0) ELSE ISNULL(Pay.PAmount,0) END ELSE 0 END   
		, ''BankBranch''  = '''' --Pay.BankID + ''/'' + Pay.BranchName  
		, ''Number'' = '''' --Pay.Number   
		, ''DueDate''  = '''' --Pay.ChequeDate  --วันครบกำหนด    
		, ''Method'' = '''' --Pay.Method  
		, ''Status'' = '''' /* CASE WHEN Pay.ReconcileDate IS NOT NULL OR Pay.DepositID IS NOT NULL OR Pay.Method = ''10'' THEN ''P''  
							ELSE ''-'' END */
		, ''CreateBy'' = '''' --Pay.CreateBy
        , ''CreateName'' = '''' --Pay.CreateName
        , ''CreateDate'' = '''' --Pay.CreateDate
        , ''GLBatchID'' = '''' --Pay.GLBatchID  
		, ''DepositID'' = '''' --Pay.DepositID   
		, ''ChequeAmount'' = '''' --ChequeAmount

FROM [SAL].[Booking]'  --This is temp table actual table start from below
	/* ( '  
set @sql=@sql+ '  
	 SELECT	TR.Method  
			, ISNULL(PD.Amount,TR.Amount) AS PAmount  
			, ISNULL(PD.RCReferent, N'' - '') AS RCReferent  
			, CASE WHEN TR.Method = 10 THEN '' - '' ELSE RC.ReceivedID END AS ReceivedID  
			, RC.PrintingID  
			, TR.RDate  
			, TR.Amount AS TAmount  
			, TR.BranchName  
			, TR.Number  
			, TR.Charge  
			, TR.ChargeAmount  
			, RC.ReceivedID AS Code  
			, RC.ReceiveDate  
			, TR.BankID  
			, TR.DepositID --CASE WHEN TR.DepositID like ''%PI%'' THEN TR.DepositID ELSE NULL END AS DepositID  --แก้ไชเนื่องจาก มีเลขนำฝากของข้อมูลเก่าที่ไม่แสดงเลขที่นำฝากเนื่องจาก ไม่มีคำว่า IP
			, RC.CounterID  
			, ''CreateBy'' = US.Firstname  
			, TR.CreateDate AS CreateDate  
			, TR.ReconcileDate  AS ReconcileDate  
			, TR.CreateName  
			, TR.ProductID  
			, TR.UnitNumber  
			, ISNULL(PD.ReferentID,TR.Referentid) AS ReferentID  
			, ISNULL(PD.PaymentType,TR.PaymentType) AS PaymentType  
			, PD.Period  
			, ISNULL(PD.RefType,TR.RefType) AS RefType  
			, CreditBankID = ''-''  
			, CreditType = ''-''  
			, TR.DueDate AS ChequeDate  
			, PA.Detail,TR.GLBatchID  
			, DS.DepositDate  
			, Case When TR.Method in(''2'',''4'',''5'') Then Isnull(TR.Amount,0)Else 0 End ChequeAmount
			, TR.TransferPaymentType
	 FROM	[ICON_Payment_TmpReceipt] AS TR   
			LEFT OUTER JOIN [vw_ICON_Payment_PaymentDetails] AS PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID AND TR.CancelDate IS NULL ----AND TR.IsHouse_Payment = 1  
			LEFT OUTER JOIN [ICON_Payment_Received] AS RC ON RC.RCReferent = TR.RCReferent  AND RC.CancelDate IS NULL  
			LEFT OUTER JOIN [ICON_EntForms_PaymentDetail] AS PA ON PA.ServiceCode = PD.PaymentType -- AND (PA.Payment NOT IN (8,1,2) OR PD.PaymentType like ''TR%'')  
			LEFT OUTER JOIN [ICON_Payment_Deposit] DS ON DS.DepositID = TR.DepositID  
			LEFT OUTER JOIN [Users] US ON US.UserID = TR.CreateBy  
	 WHERE	1=1 
	 '
			IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(TR.ProductID = '''+@ProductID+''')'  
			IF(ISNULL(@UnitNumber,'')<>'')set @sql=@sql+'and(LTrim(RTrim(TR.UnitNumber)) = '''+@UnitNumber+''')'  
						
			if (Isnull(@Method,'')<>'' ) set @sql=@sql+' and(TR.Method IN ('+@Method+'))' 
			if (Isnull(@PaymentType,'')<>'' and Isnull(@PaymentType,'')<>'0') set @sql=@sql+' and(PD.PaymentType IN ('''+Replace(@PaymentType,',',''',''')+'''))' 

Set @sql= @sql+ '
) AS Pay ' 
Set @sql= @sql+ '  
	LEFT OUTER JOIN [ICON_EntForms_ExtCod] AS EC ON EC.Ref = Pay.Method AND EC.GType = ''Method''   
	LEFT OUTER JOIN [ICON_EntForms_Bank] AS BANK ON Pay.BankID = BANK.BankID   
	LEFT OUTER JOIN [ICON_EntForms_Booking] B ON B.BookingNumber = Pay.ReferentID   
	LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = Pay.ReferentID AND ISNULL(BO.IsDelete,0) = 0 AND BO.Header = ''1''  
	LEFT OUTER JOIN [ICON_EntForms_Agreement] A ON A.ContractNumber = Pay.ReferentID   
	LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AO.ContractNumber = A.ContractNumber AND ISNULL(AO.IsDelete,0) = 0 AND AO.Header = ''1''   
	LEFT OUTER JOIN [ICON_EntForms_Products] P ON P.ProductID = Pay.ProductID  
	LEFT OUTER JOIN [ICON_EntForms_Company] C ON C.CompanyID = P.CompanyID    
	LEFT OUTER JOIN [ICON_EntForms_Unit] U ON U.UnitNumber = Pay.UnitNumber  AND U.ProductID = Pay.ProductID    
	LEFT OUTER JOIN [ICON_EntForms_ManageModel] M ON M.ProductID = U.ProductID AND M.ModelID = U.ModelID '  
Set @sql= @sql+ '  
	WHERE 1=1 '    
	IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''')'  
	if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''  
					   set @sql=@sql+'and(Pay.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'  
	
	if(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''  
	Begin  
	set @sql=@sql+'and ((Pay.DepositDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' And Pay.Method IN (1,2,3,4,5))'  
	set @sql=@sql+' or(Pay.RDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' And Pay.Method IN (7,8,9))'  
	set @sql=@sql+' or(Pay.ChequeDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' AND  Pay.Method = 6))'  
	End  
  
  
	IF(@Deposit = '1')set @sql=@sql+'and(Pay.DepositID IS NOT NULL OR Pay.ReconcileDate IS NOT NULL OR Pay.Method = ''10'') '  
	IF(@Deposit = '2')set @sql=@sql+'and(Pay.DepositID IS NULL AND Pay.Method NOT IN (6,7,8,9,10)) '  
	  
	set @sql=@sql+' ORDER BY P.ProductID,Pay.RDate,U.UnitNumber,Pay.ReceivedID,Pay.PaymentType,Pay.Period '  
  */

EXEC(@sql)  
--Print(@sql)  
GO
