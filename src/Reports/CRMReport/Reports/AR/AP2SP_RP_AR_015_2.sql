SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AP2SP_RP_AR_015_2]

	@BatchID nvarchar(40)

AS
SELECT	'CompanyNameThai' = '' --CO.CompanyNameThai
        , 'CompanyID' = '' --CO.CompanyID
		, 'BatchID' = '' --PGH.BatchID
		, 'PostDate' = '' --PGH.DocumentDate
		, 'ProductID' = '' --PGD.Referent_1 AS ProductID
        , 'AccountName' = '' --PA.NameThai AS AccountName
		, 'DRAmount' = '' --CASE WHEN PGD.PostingKey IN (40,21) THEN PGD.Amount ELSE 0 END AS DRAmount
		, 'CRAmount' = '' --CASE WHEN PGD.PostingKey IN (50,31) THEN PGD.Amount ELSE 0 END AS CRAmount
		, 'AcctType' = '' --CASE	WHEN PGD.PostingKey IN (40,21) THEN 'Dr'
							--	WHEN PGD.PostingKey IN (50,31) THEN 'Cr'
							--	ELSE '-' END
		, 'CancelDate' = '' -- CASE	WHEN PGH.CancelDate IS NOT NULL THEN 'Cancel' 
							--	ELSE NULL END
		, 'FirstName' = '' --US.FirstName

FROM	[MST].[Company] C --This is temp table actual table start from below
        /* ICON_PostToSap_Header PGH
		LEFT OUTER JOIN [ICON_PostToSap_Details] PGD ON PGH.BatchID = PGD.BatchID
		LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PGH.CRMCompanyID
		LEFT OUTER JOIN [ICON_PostToSAP_ChartOfAccount] PA ON PA.AccountID = PGD.AccountCode and Isnull(pa.Actived,0)=1
		LEFT OUTER JOIN [Users] US ON US.UserID = PGH.CancelBy

WHERE	PGH.CRMPostCode = 'PI'
And (IsNull(@BatchID,'')='' Or PGH.BatchID IN (SELECT * FROM [dbo].[fn_SplitString](''+@BatchID+'',',')))
ORDER BY PGD.Referent_1 ASC,DRAmount Desc */


GO
