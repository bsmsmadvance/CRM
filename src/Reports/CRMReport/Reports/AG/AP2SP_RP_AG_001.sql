SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_AG_001]'','10059','2013-11-01','2013-11-08','ทั้งหมด',NULL,1
-- [dbo].[AP2SP_RP_AG_001]'','10044','1800-01-01','2010-03-30','1',NULL
-- [dbo].[AP2SP_RP_AG_001]'','10059','2014-07-15','2014-07-15',NULL,NULL,'1'
-- [dbo].[AP2SP_RP_AG_001]'','10059','2014-07-15','2014-07-15',NULL,NULL,'2'
-- [dbo].[AP2SP_RP_AG_001]'','','2014-07-15','2014-07-15','ทั้งหมด',NULL,1
-- [dbo].[AP2SP_RP_AG_001]'','30002','2015-07-15','2015-07-15','ทั้งหมด','Administrator Account',''
ALTER PROCEDURE [dbo].[AP2SP_RP_AG_001]
	@CompanyID  nvarchar(20),
	@ProductID	nvarchar(15),
	@DateStart	datetime,
	@DateStart2 Datetime,
    @StatusPeriod nvarchar(10),
	@UserName	nvarchar(50),
	@CustomerStatus nvarchar(10)='0'

AS

DECLARE @DateEndInStore Datetime
IF(YEAR(@DateStart) = 1800)  SET @DateEndInStore = [dbo].[fn_GetMaxDate1](GetDate())
IF(YEAR(@DateStart) <> 1800)  SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateStart)

DECLARE @DateEndInStore2 Datetime
IF(YEAR(@DateStart2) = 1800)  SET @DateEndInStore2 = [dbo].[fn_GetMaxDate1](GetDate()) 
IF(YEAR(@DateStart2) <> 1800)  SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateStart2)

Declare @sql nvarchar(max)
Set @sql='SELECT	''ProjectName'' = '''' --PR.ProductID+'' ''+'':''+'' ''+PR.Project
		, ''CompanyID'' = '''' --CO.CompanyID
        , ''CompanyNameThai'' = '''' --CO.CompanyNameThai
        , ''ProductID'' = '''' --PR.ProductID
        , ''ShortName'' = '''' --MA.TypeOfRealEstate
		, ''UnitNumber'' = '''' --AG.UnitNumber
        , ''ContractNumber'' = '''' --AG.ContractNumber
        , ''ContactID'' = '''' --AO.ContactID
        , ''AgreemantName'' = '''' --dbo.fn_GenCustAgreementAllNoTitle(AG.ContractNumber)
		, ''TotalSellingPrice'' = '''' --AG.SellingPrice,AO.Phone
        , ''Mobile'' = '''' --ISNULL(CT.Tel_4,AO.Mobile)
		, ''Period'' = '''' --AP.Period
        , ''DueDate'' = '''' --AP.DueDate
        , ''PayableAmount'' = '''' --AP.PayableAmount
        , ''Amount'' = '''' --ISNULL(DC.Amount,0)
		, ''Nextpayable'' = '''' --ISNULL(AP.PayableAmount,0)-ISNULL(DC.Amount,0)
		, ''RowNo'' = '''' --Row_Number() OVER (PARTITION BY AP.ContractNumber ORDER BY AP.ContractNumber,AP.Period)
		, ''COUNTPriod'' =	'''' --CASE WHEN AP1.COUNTPriod < 3 THEN AP1.COUNTPriod
						--WHEN AP1.COUNTPriod IN (3,4,5,6) THEN 4
						--ELSE 7 END 
		, ''NetSalePrice'' = '''' --TR.NetSalePrice
		, ''ApproveStatus'' = '''' --(CASE WHEN DD.ApproveStatus IS NULL THEN ''-'' ELSE DD.ApproveStatus END)+'' ''+''/''+'' ''+(CASE WHEN AG.ApproveDate IS NULL THEN ''-'' ELSE ''A'' END)
		,''StartDate'' =  '''' --DD.StartDate
		,''DateStart1'' =  '''' --[dbo].[fn_GetMaxDate1](GetDate())
		,''DateStart2'' =  '''' --[dbo].[fn_GetMaxDate]('''+CONVERT(VARCHAR(50),@DateStart,120)+''')
		,''DateStart3'' =  '''' --[dbo].[fn_GetMaxDate]('''+CONVERT(VARCHAR(50),@DateStart2,120)+''')
		,''CustomerStatusName'' = '''' --[dbo].[fn_GenCustEmployeeAgreement](AG.ContractNumber)
		,''EmployeeAgreementType'' = '''' --CT.CustomerType
FROM	[SAL].[Agreement] A' --This is temp table actual table start from below
        /* [ICON_EntForms_AgreementPeriod] AP
		LEFT OUTER JOIN
		(
			SELECT PD.Period,PD.ReferentID,PD.PaymentType,
				''Amount''=SUM(PD.Amount)
			FROM [ICON_Payment_TmpReceipt]TR LEFT OUTER JOIN
				[ICON_Payment_PaymentDetails]PD ON TR.TmpReceiptID=PD.TmpReceiptID AND TR.RCReferent = PD.RCReferent
			WHERE PD.PaymentType = ''6''
				  AND TR.CancelDate IS NULL
                  AND TR.RDate<='''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' '
                  IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(TR.ProductID = '''+@ProductID+''')'
			      set @sql=@sql+'GROUP BY PD.Period,PD.ReferentID,PD.PaymentType'
                  set @sql=@sql+'                
		)DC ON AP.ContractNumber = DC.ReferentID AND AP.Period = DC.Period
		LEFT OUTER JOIN 
		(	
			SELECT ContractNumber,SellingPrice,UnitNumber,ProductID,ApproveDate
			FROM [ICON_EntForms_Agreement]
			WHERE (CancelDate IS NULL OR CancelDate >'''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')
                  AND ApproveDate IS NOT NULL 
		)AG ON AG.ContractNumber = AP.ContractNumber
		LEFT OUTER JOIN [ICON_Entforms_Products] PR ON PR.ProductID = AG.ProductID
		LEFT OUTER JOIN [ICON_Entforms_Company] CO ON CO.CompanyID = PR.CompanyID
		LEFT OUTER JOIN [ICON_Entforms_Unit] UN ON UN.ProductID = AG.ProductID AND UN.UnitNumber = AG.UnitNumber
		LEFT OUTER JOIN [ICON_Entforms_ManageModel] MA ON MA.ProductID = UN.ProductID AND MA.ModelID = UN.ModelID
		LEFT OUTER JOIN [ICON_Entforms_AgreementOwner] AO ON AO.ContractNumber = AG.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete,0) = 0	
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN [ICON_EntForms_Contacts] CT ON CT.ContactID = AO.ContactID
		LEFT OUTER JOIN
		(
			SELECT COUNT(AP.Period)AS COUNTPriod,AP.ContractNumber
			FROM [ICON_EntForms_AgreementPeriod]AP LEFT OUTER JOIN
			(
				SELECT	PD.Period,PD.ReferentID,PD.PaymentType,
						''Amount''=SUM(PD.Amount)
				FROM	[ICON_Payment_TmpReceipt] TR LEFT OUTER JOIN
						[ICON_Payment_PaymentDetails] PD ON TR.TmpReceiptID=PD.TmpReceiptID AND TR.RCReferent = PD.RCReferent
				WHERE	PD.PaymentType = ''6''
						AND TR.CancelDate IS NULL
						AND TR.RDate<='''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''' ' 
						IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(TR.ProductID = '''+@ProductID+''')'
						set @sql=@sql+'GROUP BY PD.Period,PD.ReferentID,PD.PaymentType'
set @sql=@sql+' 			
			)PD ON AP.ContractNumber=PD.ReferentID AND AP.Period=PD.Period AND AP.PaymentType=PD.PaymentType
			WHERE	AP.Duedate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
					AND AP.PaymentType = ''6''
					AND AP.PayableAmount > ISNULL(PD.Amount,0)
			GROUP BY AP.ContractNumber
		)AP1 ON AP1.ContractNumber = AP.ContractNumber
	  LEFT OUTER JOIN
	  (
			SELECT	Sum(PayableAmount)AS NetSalePrice,ContractNumber
			FROM	[ICON_EntForms_AgreementPeriod]
			WHERE PaymentType NOT LIKE ''TR%''
			GROUP BY ContractNumber
	  )	AS TR ON TR.ContractNumber = AP.ContractNumber
	  LEFT OUTER JOIN
	  (
			SELECT	DD.ContractNumber,DD.ApproveStatus,DD.StartDate,DD.ApproveDate
			FROM	[ICON_EntForms_AgreementDirectDebit] DD
					INNER JOIN
					(
						SELECT	ContractNumber,MAX(CreateDate) AS MaxCreateDate
						FROM	[ICON_EntForms_AgreementDirectDebit]
						GROUP BY ContractNumber
					)AD ON AD.ContractNumber = DD.ContractNumber AND AD.MaxCreateDate = DD.CreateDate
			WHERE DD.ApproveDate IS NOT NULL
	  )DD ON DD.ContractNumber = AG.ContractNumber

WHERE  AP.PaymentType = ''6''  
	   AND AG.ProductID IS NOT NULL
       AND ((ISNULL(AP.PayableAmount,0)-ISNULL(DC.Amount,0))>0)
	   AND (ISNULL(TF.Approve3,0) = 0)
       AND (AP.DueDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')  '
	   
       IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND (CO.CompanyID = '''+@CompanyID+''')'
       IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND (AG.ProductID = '''+@ProductID+''')'

	   IF(@statusperiod = '1')set @sql=@sql+' AND (AP1.COUNTPriod = 1)'
	   IF(@statusperiod = '2')set @sql=@sql+' AND (AP1.COUNTPriod = 2)'
	   IF(@statusperiod = '3')set @sql=@sql+' AND (AP1.COUNTPriod IN (3,4,5,6))'
	   IF(@statusperiod = '4')set @sql=@sql+' AND (AP1.COUNTPriod = 7)'
	   
	   IF(@CustomerStatus = '1')set @sql=@sql+' AND (CT.CustomerType = 1)'
	   IF(@CustomerStatus = '2')set @sql=@sql+' AND (CT.CustomerType = 2)'

set @sql=@sql+'
ORDER BY ProductID,ShortName,UnitNumber,ContractNumber,Period;' */


--PRINT(@sql)
EXEC(@sql)

GO
