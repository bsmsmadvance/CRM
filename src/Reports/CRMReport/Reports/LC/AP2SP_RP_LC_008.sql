SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_LC_008] '','10109','05b06','','Administrator Account','' 
--[dbo].[AP2SP_RP_LC_008] 'F','10103','N7A17','','','1' 

ALTER PROC  [dbo].[AP2SP_RP_LC_008]  

	@CompanyID nvarchar(50),  
    @ProductID nvarchar(50),  
    @UnitNumber nvarchar(50),  
    @SBUID  nvarchar(10),  
    @UserName nvarchar(150),  
    @StatusAG   nvarchar(20)  
  
AS  

Declare @UnitID NVARCHAR(max)
SET @UnitID = (SELECT ID FROM PRJ.Unit WHERE UnitNo = @UnitNumber)
  
Declare @sql nvarchar(max)  
/* Set @sql='
DECLARE @MAXDueDate datetime;
SELECT @MAXDueDate = MAX(AP.DueDate)
FROM [SAL].[AgreementDownPeriod] AP  
		LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.ID = AP.AgreementID  
		LEFT OUTER JOIN [FIN].[DirectCreditDebitApprovalForm] AD ON AD.BookingID = AG.BookingID
		LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = AG.ProjectID    
WHERE	AP.PaymentType = ''6'' AND (AD.AccountNo IS NOT NULL) '
IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '  
IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(P.ProjectNo = '''+@ProductID+''') '  
IF(ISNULL(@UnitNumber,'')<>'')set @sql=@sql+'and(AG.UnitID = '''+@UnitID+''') '  
--IF(ISNULL(@SBUID,'')<>'')set @sql=@sql+'and(P.SBUID = '''+@SBUID+''') '  
IF(@StatusAG = '1')set @sql=@sql+'and(AG.Canceldate IS NULL) '  
IF(@StatusAG = '2')set @sql=@sql+'and(AG.Canceldate IS NOT NULL) ' 

Set @sql=@sql+';' 
Set @sql=@sql+  */

--This is for mapping only need to remove this n use line 37 for actual mapping
Set @sql = ' 

SELECT  ''Period''  = '''' --RIGHT(''00''+CONVERT(nvarchar(5),AP.Period),3)  
		, ''Amount'' = '''' --ISNULL(AP.PayableAmount,0)  
		, ''PeriodDate'' = '''' --AP.DueDate --วัน Due  
		, ''EffectDate'' = '''' --DD.EffectDate --วันที่มีผล  
		, ''ExportNo'' = '''' --ISNULL(DD.RddBatchID,''-'')   
		, ''RAmount'' = '''' --CASE WHEN ISNULL(DD.IsPass,'''') = 1 AND ISNULL(DD.TransCode,'''') = ''0'' THEN ISNULL(DD.RAmount,0) ELSE ISNULL(DD.Amount,0) END  
		, ''LoadDate'' = '''' --DH.LoadDate  
		,''IsPass'' = '''' /* CASE	WHEN ISNULL(DD.IsPass,'''') = 1 AND ISNULL(DD.TransCode,'''') = ''0'' THEN ''Y''   
							WHEN ISNULL(DD.IsPass,'''') = 1 AND ISNULL(DD.TransCode,'''') <> ''0'' THEN ''N''   
							ELSE ''-'' END */
		, ''BatchID'' = '''' --DD.DBatchID  
		, ''PayInNO'' = '''' --CASE WHEN ISNULL(DD.IsPass,'''') = 1 AND ISNULL(DD.TransCode,'''') = ''0'' THEN DD.DBatchID ELSE ISNULL(DD.TransCode,'''') +''-''+ISNULL(DR.RName,'''') END  
		, ''ContractNumber'' = '''' --AG.ContractNumber  
		, ''CompanyID'' = '''' --P.CompanyID  
		, ''CompanyName'' = '''' --[dbo].[fn_GetCompanyNameTH](P.CompanyID,@MAXDueDate)  
		, ''ProductID'' = '''' --P.ProductID  
		, ''ProjectName'' = '''' --P.Project     
		, ''SBU'' = '''' --SBU.SBUID +''-''+SBU.SBUName  
		, ''UnitNumber'' = '''' --AG.UnitNumber  
		, ''TypeProduct'' = ''-''  
		, ''ContactName'' = '''' --ISNULL(CONVERT(NVARCHAR(10),AO.ContactID),'''') +''-''+ ISNULL(AO.NamesTitle,'''')+ISNULL(AO.FirstName,'''')+'' ''+ISNULL(AO.LastName,'''')  
		, ''BankName'' = '''' --BA.BankName
        , ''AdBankName'' = '''' --BA.AdBankName  
		, ''RBranchName'' = '''' --AD.RBranchName  
		, ''RAccountID'' = '''' /* CASE WHEN AD.DirectType = ''DR'' THEN AD.RAccountID  
								WHEN AD.DirectType = ''CR'' THEN AD.RCreditNumber END */
		, ''ApproveStatus'' = '''' /* CASE	WHEN AD.ApproveStatus = ''Y'' THEN ''อนุมัติ''  
									WHEN AD.ApproveStatus = ''N'' THEN ''ยังไม่ตัดบัญชี รอส่งเรื่องให้ธนาคาร''  
									WHEN AD.ApproveStatus = ''R'' THEN ''ไม่อนุมัติ''   
									WHEN AD.ApproveStatus = ''C'' THEN ''ยกเลิก'' 
									WHEN AD.ApproveStatus = ''A'' THEN ''Auto Cancel แต่ยังไม่ยกเลิกรอทางการเงินอนุมัติในระบบ'' END  */
		, ''StartDate'' = '''' --AD.StartDate 

FROM	[SAL].[AgreementDownPeriod] AP  '
		/* LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.ID = AP.AgreementID  
		LEFT OUTER JOIN [FIN].[DirectCreditDebitApprovalForm] AD ON AD.BookingID = AG.BookingID  
		LEFT OUTER JOIN [ICON_Payment_DirectDBCRDetails] DD ON DD.ContractNumber = AD.ContractNumber AND DD.Period = AP.Period  AND AD.DirectID = DD.DirectID AND DD.CancelDate IS NULL
		LEFT OUTER JOIN [ICON_Payment_DirectDBCRHeader]DH ON DH.RddBatchID = DD.RddBatchID   
		LEFT OUTER JOIN [ICON_EntForms_DDReason] DR ON DR.BankID = DH.BankCode AND DD.TransCode = DR.RCode   
		LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON AO.AgreementID = AG.ID AND AO.IsMainOwner = ''1'' AND ISNULL(AO.IsDeleted,0) = 0  
		LEFT OUTER JOIN [MST].[Bank] BA ON BA.ID = AD.RBankID   
		LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = AG.ProjectID   
		LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = AG.UnitID AND U.ProjectID = AG.ProjectID 
  
WHERE	AP.PaymentType = ''6'' 
	AND (AD.RAccountID IS NOT NULL OR AD.AccountNO IS NOT NULL) '


IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(C.Code = '''+@CompanyID+''') '  
IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(P.ProjectNo = '''+@ProductID+''') '  
IF(ISNULL(@UnitID,'')<>'')set @sql=@sql+'and(AG.UnitID = '''+@UnitID+''') '  
IF(@StatusAG = '1')set @sql=@sql+'and(AG.Canceldate IS NULL) '  
IF(@StatusAG = '2')set @sql=@sql+'and(AG.Canceldate IS NOT NULL) '  
  
SET @sql=@sql+' ORDER BY AG.ProductID,AG.UnitNumber,AG.ContractNumber,AD.ApproveStatus,AP.Period,AP.DueDate,DD.EffectDate,DD.RddBatchID ASC;'  
--print (@sql)  */

EXEC( @sql)  
  


GO
