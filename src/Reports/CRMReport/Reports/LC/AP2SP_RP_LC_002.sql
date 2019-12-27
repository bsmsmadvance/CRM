SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--  [AP2SP_RP_LC_002] NULL,10049,NULL,NULL,NULL,'2009-12-31','',NULL,NULL,0,''
ALTER PROCEDURE [dbo].[AP2SP_RP_LC_002]
	@CompanyID nvarchar(50),
    @ProductID nvarchar(50),
	@DateStart datetime,
    @DateEnd datetime,
    @DateStart2 datetime,
    @DateEnd2 datetime,
    @UserName nvarchar(150),
    @DateStart3 datetime,
    @DateEnd3 datetime,
	@StatusAG2 nvarchar(2),
	@AccountType nvarchar(2)

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)
DECLARE @DateEndInStore3 Datetime
SET @DateEndInStore3 = [dbo].[fn_GetMaxDate](@DateEnd3)

Declare @sql nvarchar(max)
Set @sql= '

SELECT ''ContractNumber'' = AG.AgreementNo
       , ''CompanyID'' =  PR.Code
	   , ''CompanyName'' =  CO.NameTH
	   , ''ProductID'' =  PR.ProjectNo
       , ''Project'' =  PR.ProjectNameTH
	   , ''UnitNumber'' =  AG.UnitID
       , ''Area'' =  ISNULL(BK.SaleArea,0)
       , ''TotalSellingPrice'' =  '''' --AG.SellingPrice 
       , ''ContactName'' =  ISNULL(ISNULL(CT.FirstNameTH,'''')+'' ''+CT.LastNameTH,ISNULL(AO.FirstNameTH,'''')+'' ''+ISNULL(AO.LastNameTH,''''))
       , ''BankName'' =  '''' --Substring(BA.NameTH,1,charindex('' '',BA.NameTH))+ISNULL(''/''+DD.RBranchName,'''')
       , ''RBranchName'' =  '''' --DD.RBranchName
       , ''RAccountID'' =  '''' /* CASE WHEN DD.DirectType = ''DR'' THEN DD.RAccountID
                               WHEN DD.DirectType = ''CR'' THEN DD.RCreditNumber END */
       , ''ApproveStatus'' =  '''' --(CASE WHEN DD.ApproveStatus IS NULL THEN ''-'' ELSE DD.ApproveStatus END)+'' ''+''/''+'' ''+(CASE WHEN AG.ApproveDate IS NULL THEN ''-'' ELSE ''A'' END)
       , ''StartDate'' =   '''' --DD.StartDate
	   , ''ContractDate'' =  AG.ContractDate
	   , ''DirectType'' = '''' /* CASE WHEN DD.DirectType = ''DR'' THEN ''DD''
                               WHEN DD.DirectType = ''CR'' THEN ''DC'' END */
       , ''PayableAmount'' =  '''' --ISNULL(AP1.PayableAmount,0)
       , ''AGPayAll'' =  '''' --ISNULL(DC1.AGPayAll,0)

FROM  [SAL].[Agreement] AG' --This is main table need to use table below as well
	  /* LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON AO.AgreementID = AG.ID AND ISNULL(AO.IsDeleted,0) = 0 AND AO.IsMainOwner = ''1''
	  LEFT OUTER JOIN [SAL].[Booking] BK ON BK.BookingNumber = AG.BookingNumber
      LEFT OUTER JOIN [FIN].[DirectCreditDebitApprovalForm] DD ON DD.BookingID = AG.BookingID 
	  LEFT OUTER JOIN [CTM].[Contact] CT ON CT.ContactID = DD.ContactID
      LEFT OUTER JOIN [MST].[Bank] BA ON BA.BankID = DD.RBankID 
	  LEFT OUTER JOIN [PRJ].[Project] PR ON PR.ProductID = AG.ProductID 
	  LEFT OUTER JOIN [MST].[Company] CO ON CO.CompanyID = PR.CompanyID 
      LEFT OUTER JOIN '
	  Set @sql= @sql+ ' 
      (
			SELECT	SUM(PayableAmount) AS PayableAmount,AgreementID
			FROM	[ICON_EntForms_AgreementPeriod]
			WHERE	PaymentType = ''5''
			GROUP BY ContractNumber
	  ) AP1 ON AP1.AgreementID = AG.ID '
      Set @sql= @sql+ ' 
      LEFT OUTER JOIN 
	  (
		    SELECT SUM(Amount) AS AGPayAll,ReferentID
		    FROM [ICON_Payment_PaymentDetails]
		    WHERE PaymentType IN (''5'')
		    GROUP BY ReferentID
	  ) DC1 ON DC1.ReferentID = AG.ContractNumber
WHERE AG.CancelDate IS NULL 
	'

IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(PR.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(PR.ProjectID = '''+@ProductID+''')'


IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
                   set @sql=@sql+' and(DD.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF((YEAR(@DateStart) <> 1800) AND ISNULL(@DateStart,'')<>'' AND ((YEAR(@DateEnd) = 7000) OR ISNULL(@DateEnd,'')=''))
                   set @sql=@sql+' and(DD.ApproveDate >= '''+CONVERT(VARCHAR(50),@DateStart,120)+''')'

IF((YEAR(@DateEnd) <> 7000) AND ISNULL(@DateEnd,'')<>'' AND ((YEAR(@DateStart) = 1800) OR ISNULL(@DateStart,'')=''))
                   set @sql=@sql+' and(DD.ApproveDate <='''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'


IF(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
                   set @sql=@sql+' and(AG.ContractDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'
IF((YEAR(@DateStart2) <> 1800) AND ISNULL(@DateStart2,'')<>'' AND ((YEAR(@DateEnd2) = 7000) OR ISNULL(@DateEnd2,'')=''))
                   set @sql=@sql+' and(AG.ContractDate >= '''+CONVERT(VARCHAR(50),@DateStart2,120)+''')'
IF((YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateEnd2,'')<>'' AND ((YEAR(@DateStart2) = 1800) OR ISNULL(@DateStart2,'')=''))
                   set @sql=@sql+' and(AG.ContractDate <='''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'


IF(YEAR(@DateStart3) <> 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
                   set @sql=@sql+' and(AG.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart3,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''')'

IF((YEAR(@DateStart3) <> 1800) AND ISNULL(@DateStart3,'')<>'' AND ((YEAR(@DateEnd3) = 7000) OR ISNULL(@DateEnd3,'')=''))
                   set @sql=@sql+' and(AG.ApproveDate >= '''+CONVERT(VARCHAR(50),@DateStart3,120)+''')'
IF((YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateEnd3,'')<>'' AND ((YEAR(@DateStart3) = 1800) OR ISNULL(@DateStart3,'')=''))
                   set @sql=@sql+' and(AG.ApproveDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''')'

IF(@StatusAG2 = '1')set @sql=@sql+' and(AG.ApproveDate IS NULL)'
IF(@StatusAG2 = '2')set @sql=@sql+' and(AG.ApproveDate IS NOT NULL)'

IF(@AccountType = '1')set @sql=@sql+' and(DD.DirectType = ''CR'')'
IF(@AccountType = '2')set @sql=@sql+' and(DD.DirectType = ''DR'')'



set @sql=@sql+' ORDER BY AG.UnitNumber,DD.ApproveStatus' */
--print (@sql)
exec( @sql)

GO
