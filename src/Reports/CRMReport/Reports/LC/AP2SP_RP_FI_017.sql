SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--ตัดบัญชี Direct Credit
--[AP2SP_RP_FI_017] '10052','','','','','','','Administrator Account'

ALTER PROCEDURE [dbo].[AP2SP_RP_FI_017]
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,
	@DateEnd   datetime,
	@DateStart2 datetime,
	@DateEnd2   datetime,
	@AccountStatus nvarchar(2),
    @UserName nvarchar(150)

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)

Declare @sql nvarchar(max)
Set		@sql= '

SELECT  ''CompanyID'' = '''' --P.CompanyID
	    , ''CompanyName'' = '''' --C.CompanyNameThai
		, ''RddBatchID'' = '''' --DH.RddBatchID
        , ''FileName'' = '''' --substring(DH.FileName,dbo.FindStringFAI(DH.FileName,''\'')+1,LEN(DH.FileName)-dbo.FindStringFAI(DH.FileName,''\''))
        , ''BankAccountID'' = '''' --DH.BankAccountID
        , ''BankAccount'' = '''' --BA.BankAccount
        , ''AdBankName'' = '''' --BK.AdBankName
        , ''BookTypeName'' = '''' --BA.BookTypeName
        , ''AccountName'' = '''' --BA.AccountName
		, ''ProductID'' = '''' --P.ProductID
		, ''ProjectName'' = '''' --P.Project   
		, ''SBU'' = '''' --SBU.SBUID +''-''+SBU.SBUName
	    , ''UnitNumber'' = '''' --AG.UnitNumber
		, ''RAccount'' = '''' --CASE WHEN AD.DirectType = ''DR'' THEN DD.RAccount ELSE AD.RCreditNumber END
        , ''Period'' = '''' --RIGHT(''00''+CONVERT(nvarchar(5),DD.Period),3)
        , ''PeriodDate'' = '''' --DD.PeriodDate
        , ''DueDate'' = '''' --AP.DueDate
        , ''ContractNumber'' = '''' --CASE	WHEN AG.MA_RUNNO IS NOT NULL THEN Substring(AG.ContractNumber,8,20) ElSE AG.ContractNumber END
        , ''CustomerName'' = '''' --ISNULL(AO.ContactID,'''')+''-''+ISNULL(ISNULL(AO.NamesTitle,AO.NamesTitleExt),'''')+ISNULL(AO.FirstName,'''')+''   ''+ISNULL(AO.LastName,'''')
		, ''RAmount'' = '''' --CASE WHEN ISNULL(DD.IsPass,'''') = 1 AND ISNULL(DD.TransCode,'''') = ''0'' THEN DD.RAmount ELSE DD.Amount END
        , ''PayInNO'' = '''' --CASE WHEN ISNULL(DD.IsPass,'''') = 1 AND ISNULL(DD.TransCode,'''') = ''0'' THEN DD.DBatchID ELSE ISNULL(DD.TransCode,'''') +''-''+ISNULL(DR.RName,'''') END
        , ''Ispass'' = '''' --DD.Ispass
        , ''TransCode'' = '''' --DD.TransCode
        , ''PassORNoPass'' = '''' /* CASE	WHEN ISNULL(DD.IsPass,'''') = 1 AND (ISNULL(DD.TransCode,'''') = ''00'' OR ISNULL(DD.TransCode,'''') = ''0'') THEN ''รายการที่หักบัญชีได้'' 
									ELSE ''รายการที่หักบัญชีไม่ได้'' END */
        , ''TowerID'' = '''' --TW.TowerID
        , ''RDate'' = '''' --TR.RDate

FROM	[SAL].[Booking]' --This is temp table, actual table start from below
        /* [ICON_Payment_DirectDBCRHeader] DH
		LEFT OUTER JOIN [ICON_Payment_DirectDBCRDetails] DD ON DD.RddBatchID = DH.RddBatchID
		LEFT OUTER JOIN [MST].[BankAccount] BA ON BA.ID = DH.BankAccountID
		LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.ContractNumber = DD.ContractNumber ---AND AG.CancelDate IS NULL
		LEFT OUTER JOIN [SAL].[AgreementDownPeriod] AP ON AP.ContractNumber = AG.ContractNumber AND AP.Period = DD.Period	AND AP.PaymentType = ''6''
		LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON AO.ContractNumber = AG.ContractNumber AND AO.Header = ''1'' AND ISNULL(AO.IsDelete,0)= 0
		LEFT OUTER JOIN [ICON_EntForms_DDReason] DR ON DR.BankID = DH.BankCode AND DD.TransCode = DR.RCode 
		LEFT OUTER JOIN [ICON_EntForms_AgreementDirectDebit] AD ON AD.DirectID = DD.DirectID  ---AND AD.ApproveStatus = ''Y''
		LEFT OUTER JOIN [MST].[Bank] BK ON BK.ID = BA.BankID 
		LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = AG.ProjectID 
		LEFT OUTER JOIN [MST].[Company] C ON C.ID = P.CompanyID 
		LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = AG.UnitID AND U.ProjectID = AG.ProjectID
		LEFT OUTER JOIN [PRJ].[Tower] TW ON TW.ProjectID = U.ProjectID AND TW.ID = U.TowerID
		LEFT OUTER JOIN [ICON_Payment_TmpReceipt] TR ON TR.ReferentID = DD.ContractNumber AND TR.RCReferent = DD.RCReferent


WHERE DH.DirectType = ''CR'' 

IF(ISNULL(@AccountStatus,0) = '1') SET @sql=@sql+'AND (ISNULL(DD.IsPass,'''') = 1 AND (ISNULL(DD.TransCode,'''') = ''00'' OR ISNULL(DD.TransCode,'''') = ''0'')) '
IF(ISNULL(@AccountStatus,0) = '2') SET @sql=@sql+'AND (ISNULL(DD.TransCode,'''') <> ''00'' AND ISNULL(DD.TransCode,'''') <> ''0'') '

IF(ISNULL(@ProductID,'')<>'') SET @sql=@sql+'and(AG.ProductID = '''+@ProductID+''') '
IF(ISNULL(@UnitNumber,'')<>'')SET @sql=@sql+'and(AG.UnitNumber = '''+@UnitNumber+''') '
--เลือกระหว่างวันที่
IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
                   SET @sql=@sql+'and(AP.DueDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
                   SET @sql=@sql+'and(TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'
--เลือกวันที่สุดท้าย
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
                   SET @sql=@sql+'and(AP.DueDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart2) = 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
                   SET @sql=@sql+'and(TR.RDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'



SET @sql=@sql+' ORDER BY DD.RddBatchID,DD.Ispass,DD.TransCode,AG.UnitNumber,DD.Period ' */

exec(@sql)
print(@sql)

GO
