SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_AR_017_1] 'JV1007F0013'

CREATE PROC [dbo].[AP2SP_RP_AR_017_1]

	@BatchID nvarchar(40) 

AS

Declare @sql nvarchar(max)
Set @sql= '

Select	''Postdate'' = '''' --tmp.Postdate
        , ''CompanyNameThai'' = '''' --tmp.CompanyNameThai
        , ''AcctType'' = '''' --tmp.AcctType
        , ''BatchID'' = '''' --tmp.BatchID
        , ''AccountName'' = '''' --tmp.AccountName
		, ''DRAmount'' = '''' --Sum(tmp.DRAmount) as DRAmount 
        , ''CRAmount'' = '''' --sum(tmp.CRAmount) as CRAmount
		, ''CancelDate'' = '''' --tmp.CancelDate
        , ''FirstName'' = '''' --tmp.FirstName
From [MST].[Company] C' --This is temp table actual table start from below
/* (	SELECT		CO.CompanyNameThai
				,PGH.BatchID,PGH.OperateDate AS Postdate,PGD.Referent_1 AS ProductID,PA.NameThai AS AccountName
				,CASE WHEN PGD.PostingKey IN (40,21) THEN PGD.Amount ELSE 0 END AS DRAmount
				,CASE WHEN PGD.PostingKey IN (50,31) THEN PGD.Amount ELSE 0 END AS CRAmount
				,''AcctType'' = CASE	WHEN PGD.PostingKey IN (40,21) THEN ''Dr''
										WHEN PGD.PostingKey IN (50,31) THEN ''Cr''
										ELSE ''-'' END
				,''CancelDate'' = CASE	WHEN PGH.CancelDate IS NOT NULL THEN ''Cancel'' 
										ELSE NULL END
				,US.FirstName

	FROM		[ICON_PostToSap_Header] PGH 
				LEFT OUTER JOIN [ICON_PostToSap_Details] PGD ON PGH.BatchID = PGD.BatchID
				LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PGH.CRMCompanyID
				LEFT OUTER JOIN [Users] US ON US.UserID = PGH.CancelBy
				LEFT OUTER JOIN [ICON_PostToSAP_ChartOfAccount] PA ON PA.AccountID = PGD.AccountCode

	WHERE	PGH.CRMPostCode = ''JV'' '
			IF(ISNULL(@BatchID,'')<>'')set @sql=@sql+' AND(PGH.BatchID IN (SELECT * FROM [dbo].[fn_SplitString]('''+@BatchID+''','','')))'
			set @sql=@sql+'
) AS tmp
Group By tmp.CompanyNameThai,tmp.AcctType,tmp.BatchID,tmp.AccountName,Postdate,tmp.CancelDate,tmp.FirstName order by DRAmount DESC'
*/
		exec(@sql)
		--print(@sql)



GO
