SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--  [dbo].[AP2SP_RP_FI_016]'','','','2010-09-13','2010-09-14','Administrator Account'

CREATE PROC [dbo].[AP2SP_RP_FI_016]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@Username nvarchar(50)
AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @sql nvarchar(MAX)
Set @sql = '
SELECT	''CreateDate'' = '''' --ISNULL(AD.ModifyDate,AD.CreateDate),
		,''ApproveStatus'' = '''' /* CASE	WHEN AD.ApproveStatus = ''Y'' THEN ''อนุมัติ''
									WHEN AD.ApproveStatus = ''R'' THEN ''Reject'' 
									WHEN AD.ApproveStatus = ''N'' THEN ''New'' 
									WHEN AD.ApproveStatus = ''C'' THEN ''Cancel'' END, */
		,''CreditCardNo'' = '''' --AD.RCreditNumber, 
		,''CardNew'' = ''''
		,''CardOwner'' = '''' --ISNULL(AD.RAccountName,ISNULL(AO.NamesTitle,'''')+ISNULL(AO.FirstName,'''')+'' ''+ISNULL(AO.LastName,'''')),
		,''CardOwnerID'' = '''' --ISNULL(ISNULL(AD.RCreditOwner,AO.PersonCardID),''-''),
		,''CustomerCode'' = '''' --AD.ContactID,
		,''UnitNumber'' = '''' --A.UnitNumber
        ,''Project'' = '''' --P.Project
        ,''ProductID'' = '''' --P.ProductID,
		,''UnitOwner'' = '''' --[dbo].[fn_GenCustAgreementAll_Contract](AD.ContractNumber),
		,''ContractAmount'' = '''' --CASE	WHEN ISNULL(A.ExtraDownAmount,0) = 0 THEN A.DownPaymentPerMonth
										--ELSE A.ExtraDownAmount END, 
	    ,''CompanyNameThai'' = '''' --C.CompanyNameThai

FROM	[SAL].[Booking] B' --This is temp table actual table start from below
        /* [ICON_EntForms_AgreementDirectDebit] AD 
		LEFT OUTER JOIN [ICON_EntForms_Agreement]A ON AD.ContractNumber = A.ContractNumber
		LEFT OUTER JOIN [ICON_EntForms_AgreementOwner]AO ON A.ContractNumber = AO.ContractNumber AND AO.ContactID = AD.ContactID 
		LEFT OUTER JOIN [ICON_EntForms_Products]P ON A.ProductID = P.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_Company]C ON P.CompanyID = C.CompanyID

WHERE AD.DirectType = ''CR'' 
	'
IF(ISNULL(@CompanyID,'')<>'')Set @sql = @sql+' AND (P.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (P.ProductID = '''+@PRoductID+''')'
IF(ISNULL(@UnitNumber,'')<>'')Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'
IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
Set @sql = @sql+' AND (ISNULL(AD.ModifyDate,AD.CreateDate) BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
Set @sql = @sql+' AND (ISNULL(AD.ModifyDate,AD.CreateDate) BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'

Set @sql = @sql+' ORDER BY AD.CreateDate DESC,P.ProductID,A.UnitNumber ' */

EXEC(@sql)

--print(@sql)

GO