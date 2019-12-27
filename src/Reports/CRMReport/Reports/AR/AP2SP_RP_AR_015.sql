SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_AR_015] 'F','2009-10-23','2010-07-20','','
--[dbo].[AP2SP_RP_AR_015] 'F',NULL,NULL,'','''PI1201F0207'''
-- [dbo].[AP2SP_RP_AR_015] 'AG','20170101','20171231','''PI1709AG0013‎'''

CREATE PROC [dbo].[AP2SP_RP_AR_015]
    @CompanyID NVARCHAR(4000)
  , @DateStart DATETIME
  , @DateEnd DATETIME
  , @BatchID NVARCHAR(40)
  , @Deposit NVARCHAR(MAX) = ''
AS
BEGIN

	SET @Deposit = REPLACE(@Deposit,'''','')

    SELECT 'CompanyNameThai' = '' --CO.CompanyNameThai
      , 'BatchID' = '' --PGH.BatchID
      , 'PostDate' = '' --PGH.DocumentDate
      , 'ProductID' = '' --PGD.Referent_1 AS ProductID
      , 'AccountName' = '' --PA.NameThai AS AccountName
      , 'DRAmount' = '' --(CASE WHEN PGD.PostingKey IN (40, 21) THEN PGD.Amount ELSE 0 END) AS DRAmount
      , 'CRAmount' = '' --(CASE WHEN PGD.PostingKey IN (50, 31) THEN PGD.Amount ELSE 0 END) AS CRAmount
      , 'AcctType' = '' --(CASE WHEN PGD.PostingKey IN (40, 21) THEN 'Dr' WHEN PGD.PostingKey IN (50, 31) THEN 'Cr' ELSE '-' END)
      , 'CancelDate' = '' --(CASE WHEN PGH.CancelDate IS NOT NULL THEN 'Cancel' ELSE NULL END)
      , 'FirstName' = '' --US.FirstName
    FROM [MST].[Company] C --This is temp table actual table start from below
    /* ICON_PostToSAP_Header PGH
    LEFT OUTER JOIN [ICON_PostToSAP_Details] PGD ON PGH.BatchID = PGD.BatchID
    LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PGH.CRMCompanyID
    LEFT OUTER JOIN [ICON_PostToSAP_ChartOfAccount] PA ON PA.AccountID = PGD.AccountCode
                                                           AND ISNULL(PA.Actived, 0) = 1
    LEFT OUTER JOIN [Users] US ON US.UserID = PGH.CancelBy
    WHERE PGH.CRMPostCode = 'PI'
		AND (ISNULL(@CompanyID, '') = '' OR PGH.CRMCompanyID = @CompanyID)

		AND ( (ISNULL(@DateStart,'') = '' AND ISNULL(@DateEnd,'') = '') 
				OR (Year(@DateStart) = 1800 AND Year(@DateEnd) = 7000)
				OR (PGH.DocumentDate Between CONVERT(VARCHAR(10),@DateStart,120) AND CONVERT(VARCHAR(10),@DateEnd,120))
			)
		AND (ISNULL(@BatchID, '') = '' OR PGH.BatchID IN (SELECT Val FROM [dbo].[fn_SplitString](@BatchID,',')))
		AND (ISNULL(@Deposit, '') = '' OR @Deposit = 'ทั้งหมด' OR PGH.BatchID IN (SELECT Val FROM [dbo].[fn_SplitString](@Deposit,',')))
	ORDER BY PGD.Referent_1 ASC,DRAmount DESC */
		
END;




GO
