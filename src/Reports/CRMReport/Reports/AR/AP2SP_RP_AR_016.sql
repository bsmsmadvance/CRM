SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--[dbo].[AP2SP_RP_AR_016] 'F','','','RV1106F3054',''
--[dbo].[AP2SP_RP_AR_016] 'F','1800-04-01','2010-07-20','',''
--[dbo].[AP2SP_RP_AR_016] 'I','2009-11-15','2009-11-30','',''

CREATE PROC [dbo].[AP2SP_RP_AR_016]

	@CompanyID nvarchar(4000),
	@DateStart datetime,
	@DateEnd	datetime,
	@BatchID nvarchar(40), 
	@Receipt nvarchar(Max)
AS

Declare @sql nvarchar(max)
Set @sql= '
SELECT	''PostDate'' = '''' --tmp.Postdate
        , ''CompanyNameThai'' = '''' --tmp.CompanyNameThai
        , ''AcctType'' = '''' --tmp.AcctType
        , ''BatchID'' = '''' --tmp.BatchID
        , ''AccountName'' = '''' --tmp.AccountName
		, ''DRAmount'' = '''' --Sum(tmp.DRAmount) as DRAmount 
        , ''CRAmount'' = '''' --sum(tmp.CRAmount) as CRAmount
		, ''CancelDate'' = '''' --tmp.CancelDate
        , ''FirstName'' = '''' --tmp.FirstName
FROM [MST].[Company] C ' --This is temp table actual table start from below
/* (	SELECT	CO.CompanyNameThai
			,PGH.BatchID
			,''PostDate'' = PGH.OperateDate 
			,PGD.Referent_1 AS ProductID
			,PA.NameThai AS AccountName
			,CASE WHEN PostingKey IN (40,21) THEN PGD.Amount ELSE 0 END AS DRAmount
			,CASE WHEN PostingKey IN (50,31) THEN PGD.Amount ELSE 0 END AS CRAmount
			,''AcctType'' = CASE	WHEN PostingKey IN (40,21) THEN ''Dr''
									WHEN PostingKey IN (50,31) THEN ''Cr''
									ELSE ''-'' END
			,''CancelDate'' = CASE	WHEN PGH.CancelDate IS NOT NULL THEN ''Cancel'' 
									ELSE NULL END
			,US.FirstName

	FROM	[ICON_PostToSap_Header] PGH 
			LEFT OUTER JOIN [ICON_PostToSap_Details] PGD ON PGH.BatchID = PGD.BatchID
			LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PGH.CRMCompanyID
			LEFT OUTER JOIN [ICON_PostToSAP_ChartOfAccount] PA ON PA.AccountID = PGD.AccountCode
			LEFT OUTER JOIN [Users] US ON US.UserID = PGH.CancelBy

WHERE	PGH.CRMPostCode = ''RV''  '
		IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND(CO.CompanyID IN (SELECT * FROM [dbo].[fn_SplitString]('''+@CompanyID+''','',''))) '
		IF(Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
                   set @sql=@sql+'and(PGH.OperateDate Between '''+CONVERT(VARCHAR(10),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(10),@DateEnd,120)+''')'
		IF(ISNULL(@BatchID,'')<>'')set @sql=@sql+' AND(PGH.BatchID IN (SELECT * FROM [dbo].[fn_SplitString]('''+@BatchID+''','','')))'
		IF(Isnull(@Receipt,'')<>'' And (@Receipt <> '''ทั้งหมด''')) set @sql=@sql+' and(PGH.BatchID IN ('+@Receipt+'))' 


		set @sql=@sql+'
) AS TMP
Group By TMP.CompanyNameThai,TMP.AcctType,TMP.BatchID,TMP.AccountName,Postdate,TMP.CancelDate,TMP.FirstName order by DRAmount DESC'
*/
EXEC(@sql)
print(@sql)

GO
