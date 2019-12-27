SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_FI_009]'','40017','2018-09-08','2019-09-08',NULL,NULL,'Administrator Account','',NULL,NULL,'n30b25','','',''

CREATE PROCEDURE [dbo].[AP2SP_RP_FI_009]
    @CompanyID NVARCHAR(20) = ''
  , @ProductID NVARCHAR(20) = ''
  , @DateStart DATETIME
  , @DateEnd DATETIME
  , @DateStart2 DATETIME
  , @DateEnd2 DATETIME
  , @UserName NVARCHAR(50) = ''
  , @BankAccount NVARCHAR(20)
  , @DateStart3 DATETIME
  , @DateEnd3 DATETIME
  , @UnitNumber NVARCHAR(50) = ''
  , @Deposit3 NVARCHAR(200) = ''
  , @Method NVARCHAR(50) = ''
  , @PaymentType NVARCHAR(50) = ''
AS
BEGIN

    DECLARE @DateEndInStore DATETIME;
    SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd);
    DECLARE @DateEndInStore2 DATETIME;
    SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2);
    DECLARE @DateEndInStore3 DATETIME;
    SET @DateEndInStore3 = [dbo].[fn_GetMaxDate](@DateEnd3);

    DECLARE @sql NVARCHAR(MAX);
    SET @sql = '
	SELECT	''ID'' = '''' --(CASE WHEN TR.Method IN (1,2,3,4) THEN BA1.ID ELSE BA.ID END) AS ID
		,''CompanyNameThai'' = '''' --CO.CompanyNameThai
		, ''AccountName'' = '''' --CASE WHEN TR.Method IN(''1'',''2'',''3'',''4'',''5'') THEN BA1.BankAccount
                                 --ELSE TR.Number END
        , ''DepositDate'' = '''' -- CASE WHEN TR.Method IN(''1'',''2'',''3'',''4'',''5'') THEN DE.DepositDate
								 --When tr.Method IN(''6'') Then TR.DueDate
                                 --ELSE TR.RDate END
        , ''ProductID'' = '''' --TR.ProductID
		, ''ProjectName'' = '''' --ISNULL(TR.ProductID,'''')+'' ''+PR.Project
		, ''PrintingID'' = '''' --CASE  WHEN TR.Method IN (''1'',''2'',''3'',''4'',''5'') THEN RC.PrintingID
                                 --ELSE '''' END
		, ''ReceivedID'' = '''' --RC.ReceivedID
		, ''ReceiptID'' = '''' --ISNULL(RC.PrintingID,RC.ReceivedID) AS ReceiptID
        , ''ReceiveDate'' = '''' --TR.RDate
        , ''UnitNumber'' = '''' --TR.UnitNumber
		, ''Period'' =  '''' /* CASE PD.PaymentType	WHEN ''6'' THEN RIGHT(''00''+CONVERT(nvarchar(20),PD.Period),3) 
											WHEN ''4'' THEN ''จอง''
											WHEN ''5'' THEN ''สัญญา'' 
											ELSE EPD.Detail END */
		,''By'' = '''' /* CASE	WHEN TR.Method  = 1  THEN ''CA'' --เงินสด         
						WHEN TR.Method  = 2  THEN ''CO'' --แคชเชียร์เช็ค      
						WHEN TR.Method  = 3  THEN ''CR'' --บัตรเครดิต      
						WHEN (TR.Method = 4 OR TR.Method = 5) THEN ''CQ'' --เช็ค   
						WHEN TR.Method  in (6,11)  THEN ''TF''  --เงินโอนบัญชี      
						WHEN TR.Method  in (7,12)  THEN ''BP''   --การ์ดลูกหนี้ Bill Payment     
						WHEN TR.Method  in (8,13)  THEN ''DD''   --DirectDebit   
						WHEN TR.Method  in (9,14)  THEN ''DC''  --DirectCredit     
						WHEN TR.Method = 10 THEN ''YM'' END  --เงินมาจากแปลงอื่น */   
		,''BK/BR'' = '''' --TR.BankID+'' ''+ISNULL(''/''+TR.BranchName,'''')
		,''DueDate'' = '''' --TR.DueDate
        ,''Number'' = '''' --CASE WHEN TR.Method IN(''2'',''3'',''4'',''5'') THEN TR.Number ELSE '''' END ';
    SET @sql = @sql + '
        ,''Amount'' = '''' --PD.Amount
		,''ChargeAmount'' = '''' --ISNULL(TR.ChargeAmount,0)
		,''Vat''= '''' --TR.ChargeVat
		,''Net'' = '''' --ISNULL(TR.Amount,0)-ISNULL(TR.ChargeAmount,0)-ISNULL(TR.ChargeVat,0)
		,''CQAmt'' = '''' --CASE WHEN (TR.Method = ''4'' OR TR.Method = ''5'') THEN TR.Amount ELSE 0.00 END
		,''Status''=  ''P''
		,''RecordBy'' = '''' --SE.FirstName
        ,''RDate'' = '''' --TR.RDate
        ,''DepositIN'' = '''' --CASE WHEN TR.Method IN (1,2,3,4) THEN TR.DepositID ELSE NULL END
        ,''DepositINDate'' = '''' /* CASE	WHEN TR.Method  = 1  THEN DE.CreateDate        
						WHEN TR.Method  = 2  THEN DE.CreateDate      
						WHEN TR.Method  = 3  THEN DE.CreateDate     
						WHEN (TR.Method = 4 OR TR.Method = 5)Then DE.CreateDate   
						WHEN TR.Method  in (6,11)  THEN TR.CreateDate  
						WHEN TR.Method  in (7,12)  THEN TR.CreateDate  --การ์ดลูกหนี้ Bill Payment     
						WHEN TR.Method  in (8,13)  THEN TR.CreateDate   --DirectDebit   
						WHEN TR.Method  in (9,14)  THEN TR.CreateDate --DirectCredit     
						ELSE TR.CreateDate END */
		,''CustomerName'' =	'''' --CASE WHEN TR.RefType = 1 THEN LEFT(dbo.fn_GenCustBookingAllNameRC(TR.ReferentID),22) 
									--ELSE LEFT(dbo.fn_GenCustAgreementAllNameRC(TR.ReferentID),22) END
		,''PeriodAmt'' = '''' /* CASE	WHEN PD.PaymentType = ''4'' THEN 1
								WHEN PD.PaymentType = ''5'' THEN 2
								WHEN PD.PaymentType = ''6'' THEN 3
								WHEN PD.PaymentType = ''8'' THEN 4
								WHEN PD.PaymentType = ''48'' THEN 5 
								WHEN PD.PaymentType = ''A06'' THEN 6 
								ELSE 7 END */
		,DepID = '''' /* CASE	WHEN TR.Method  = 1  THEN TR.DepositID        
						WHEN TR.Method  = 2  THEN TR.DepositID      
						WHEN TR.Method  = 3  THEN TR.DepositID     
						WHEN (TR.Method = 4 OR TR.Method = 5) THEN TR.DepositID   
						WHEN TR.Method  in (6,11)  THEN ''00''      
						WHEN TR.Method  in (7,12)  THEN ''00''   --การ์ดลูกหนี้ Bill Payment     
						WHEN TR.Method  in (8,13)  THEN ''00''   --DirectDebit   
						WHEN TR.Method  in (9,14)  THEN ''00''  --DirectCredit     
						ELSE ''00'' END  --เงินมาจากแปลงอื่น  */
		,''TmpReceiptID'' = '''' --PD.TmpReceiptID
        ,''ReferentID'' = '''' --TR.ReferentID
        ,''RefType'' = '''' --TR.RefType
        ,''Method'' = '''' --TR.Method
'   ;
    SET @sql = @sql + '
FROM   [SAL].[Booking] B' --This is temp table actual table start from below
      /* [ICON_Payment_TmpReceipt] TR  
       LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
	   LEFT OUTER JOIN [ICON_EntForms_PaymentDetail] PA ON PA.ServiceCode = PD.PaymentType  
       LEFT OUTER JOIN [ICON_Payment_Received] RC ON  RC.RCReferent = TR.RCReferent AND RC.CancelDate IS NULL
       LEFT OUTER JOIN [ICON_Payment_Deposit] DE ON DE.DepositID = TR.DepositID
       LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = TR.ProductID
       LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PR.CompanyID
	   LEFT OUTER JOIN (select distinct max(id)id,bankaccount from [ICON_EntForms_BankAccount]where isnull(isdelete,0)=0 group by bankaccount) BA ON BA.BankAccount = TR.Number  AND TR.Method IN (6,7,8,9,12,13,14,11) 
	   LEFT OUTER JOIN (select distinct id,bankaccount from [ICON_EntForms_BankAccount]where isnull(isdelete,0)=0) BA1 ON BA1.ID = DE.BankAccountID AND TR.Method IN (1,2,3,4)
       LEFT OUTER JOIN [ICON_EntForms_PaymentDetail] EPD ON PD.PaymentType = EPD.ServiceCode
	   LEFT OUTER JOIN [USERS] SE ON SE.UserID = ISNULL(DE.DepositeriD,TR.CreateBy) 

WHERE  TR.CancelDate IS NULL 
AND (TR.DepositID IS NOT NULL OR TR.ReconcileDate IS NOT NULL 
		OR (TR.Method IN (6,7,8,9,12,13,14,11) AND TR.DepositID IS NULL AND TR.ReconcileDate IS NULL))
AND (TR.Method<>''1'' 
		OR TR.TransferPaymentType<>2 
		OR (TR.Method=''1'' AND PD.PaymentType IN (''4'',''5'',''6'',''8'',''A06'',''TR2'',''9K'',''9P'',''02'',''01'',''00'',''2H'',''2G'',''37'',''17'',''15'',''48'') AND TR.TransferPaymentType=2))

'   ; 

    DECLARE @sql1 NVARCHAR(MAX);
    SET @sql1 = '';
    IF (ISNULL(@CompanyID, '') <> '')
        SET @sql1 = @sql1 + ' and(CO.CompanyID = ''' + @CompanyID + ''')';

    IF (ISNULL(@BankAccount, '') <> '')
        SET @sql1 = @sql1 + ' and ISNULL(BA.ID,BA1.ID) = ''' + @BankAccount + ''' ';

    IF (ISNULL(@UnitNumber, '') <> '')
        SET @sql1 = @sql1 + ' and(TR.UnitNumber = ''' + @UnitNumber + ''')';


    IF (ISNULL(@DateStart, '') <> '' AND ISNULL(@DateEnd, '') <> '')
        AND (YEAR(@DateStart) > 1800 AND YEAR(@DateEnd) < 7000)
    BEGIN
        SET @sql1 = @sql1 + ' and((DE.DepositDate Between ''' + CONVERT(VARCHAR(50), @DateStart, 120) + ''' And ''' + CONVERT(VARCHAR(50), @DateEndInStore, 120) + ''')';
        SET @sql1 = @sql1 + ' or(TR.RDate Between ''' + CONVERT(VARCHAR(50), @DateStart, 120) + ''' And ''' + CONVERT(VARCHAR(50), @DateEndInStore, 120) + ''' And TR.Method IN (7,8,9,12,13,14))';
        SET @sql1 = @sql1 + ' or(TR.DueDate Between ''' + CONVERT(VARCHAR(50), @DateStart, 120) + ''' And ''' + CONVERT(VARCHAR(50), @DateEndInStore, 120) + ''' And TR.Method in (6,11)))';
    END;

    IF (ISNULL(@ProductID, '') <> '')
        SET @sql1 = @sql1 + ' and(TR.ProductID = ''' + @ProductID + ''')';

    IF (ISNULL(@DateStart2, '') <> '' AND ISNULL(@DateEnd2, '') <> '')
        AND (YEAR(@DateStart2) > 1800 AND YEAR(@DateEnd2) < 7000)
        SET @sql1 = @sql1 + ' and(DE.CreateDate Between ''' + CONVERT(VARCHAR(50), @DateStart2, 120) + ''' And ''' + CONVERT(VARCHAR(50), @DateEndInStore2, 120) + ''')';
    IF (ISNULL(@DateStart3, '') <> '' AND ISNULL(@DateEnd3, '') <> '')
        AND (YEAR(@DateStart3) > 1800 AND YEAR(@DateEnd3) < 7000)
        SET @sql1 = @sql1 + ' and(TR.RDate Between ''' + CONVERT(VARCHAR(50), @DateStart3, 120) + ''' And ''' + CONVERT(VARCHAR(50), @DateEndInStore3, 120) + ''')';
    IF (ISNULL(@Deposit3, '') <> '' AND (@Deposit3 <> '''ทั้งหมด'''))
        SET @sql1 = @sql1 + ' and(TR.DepositID IN (' + @Deposit3 + '))';
    IF (ISNULL(@Method, '') <> '')
    BEGIN */
        /*เพิ่มเงินประเภทรับเงินหลังโอน*/
    /* SET @Method = REPLACE(@Method, ',6', ',6,11');
        SET @Method = REPLACE(@Method, ',7', ',7,12');
        SET @Method = REPLACE(@Method, ',8', ',8,13');
        SET @Method = REPLACE(@Method, ',9', ',9,14');
        SET @sql1 = @sql1 + ' and(TR.Method IN (10, 12,' + @Method + '))';
    END;
    IF (ISNULL(@PaymentType, '') <> '' AND ISNULL(@PaymentType, '') <> '0')
        SET @sql1 = @sql1 + ' and(PD.PaymentType IN (''' + REPLACE(@PaymentType, ',', ''',''') + '''))';

    SET @sql1 = @sql1 + ' ORDER BY ISNULL(BA1.BankAccount,TR.Number),dbo.fn_ClearTime(ISNULL(DE.DepositDate,TR.RDate)),DepID,TR.ProductID,ISNULL(CASE WHEN RC.PrintingID = '''' THEN NULL ELSE RC.PrintingID END,RC.ReceivedID),TR.RDate,TR.UnitNumber,PD.TmpReceiptID,PeriodAmt,Period ';

    PRINT (@sql);
    PRINT (@sql1);
    EXEC (@sql + @sql1); */
    EXEC (@sql) --For mapping only need to use upper one for actual result
END;

GO
