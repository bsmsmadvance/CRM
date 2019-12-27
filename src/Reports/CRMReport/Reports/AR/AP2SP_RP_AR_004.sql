SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_AR_004] NULL,'10044','2009-12-31',''
CREATE PROC [dbo].[AP2SP_RP_AR_004]
	@CompanyID nvarchar(50),
    @ProductID nvarchar(20),
	@DateStart Datetime,
	@Username nvarchar(150)
AS

DECLARE @DateStartInStore Datetime
IF(YEAR(@DateStart) = 1800)  SET @DateStartInStore = GetDate()
IF(YEAR(@DateStart) <> 1800)  SET @DateStartInStore = [dbo].[fn_GetMaxDate] (@DateStart)

Declare @sql nvarchar(max)
Set @sql= '

SELECT	''CompanyID'' = '''' --A.CompanyID
        , ''CompanyNamethai'' = '''' --A.CompanyNamethai
        , ''Project'' = '''' --A.Project
		,''Count1'' = '''' --COUNT (DISTINCT(CASE WHEN A.RowNo IN(1,2,3)THEN A.ContractNumber ELSE NULL END))
		,''Count2'' = '''' --COUNT(DISTINCT(CASE WHEN A.RowNo IN(4,5,6)THEN A.ContractNumber ELSE NULL END))
		,''Count3'' = '''' --COUNT(DISTINCT(CASE WHEN A.RowNo >6 THEN A.ContractNumber ELSE NULL END))
		,''Arrears1'' = '''' --SUM(CASE WHEN A.RowNo IN(1,2,3)THEN A.Nextpayable ELSE 0 END)
		,''Arrears2'' = '''' --SUM(CASE WHEN A.RowNo IN(4,5,6)THEN A.Nextpayable ELSE 0 END)
		,''Arrears3'' = '''' --SUM(CASE WHEN A.RowNo >6 THEN A.Nextpayable ELSE 0 END)
		,''TotalCount'' = '''' --COUNT(DISTINCT A.ContractNumber)
FROM [PRJ].[Project] P ' --Need to start from below this is temp only
	/* (
		SELECT	CO.CompanyNamethai,CO.CompanyID
				,''Project'' = P.ProductID+'' ''+'':''+'' ''+P.Project
				,A.ContractNumber,AP.Nextpayable
				,''RowNo'' = Row_Number() OVER (PARTITION BY A.ContractNumber,P.ProductID ORDER BY A.ContractNumber,AP.PaymentType DESC,AP.Period DESC)
		FROM 
			(
				SELECT	AP.DueDate,AP.PayableAmount,AP.Period,AP.PaymentType
						,AP.Contractnumber,PD.Amount
						,''Nextpayable'' = CASE	WHEN AP.PaymentType = ''4'' THEN ISNULL(AP.PayableAmount,0)-ISNULL(PD2.Amount,0)
												ELSE ISNULL(AP.PayableAmount,0)-ISNULL(PD.Amount,0) END
				FROM	[ICON_EntForms_AgreementPeriod] AP 
						LEFT OUTER JOIN
						(
							SELECT	PD.Period,PD.ReferentID,PD.PaymentType,
									''Amount''=SUM(PD.Amount)
							FROM	[ICON_Payment_TmpReceipt]  TR 
									LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON TR.RCReferent = PD.RCReferent AND TR.TmpReceiptID = PD.TmpReceiptID
							WHERE	TR.RDate<='''+CONVERT(VARCHAR(50),@DateStartInStore,120)+'''
									AND PD.PaymentType IN (''5'',''6'') '
									IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(TR.ProductID = '''+@ProductID+''')'
									set @sql=@sql+'GROUP BY PD.Period,PD.ReferentID,PD.PaymentType'
								 set @sql=@sql+'
						)PD ON AP.ContractNumber = PD.ReferentID AND AP.Period = PD.Period AND AP.PaymentType = PD.PaymentType
						LEFT OUTER JOIN 
						(
							SELECT	PD.Period,PD.ReferentID,PD.PaymentType,AG.Contractnumber
									,''Amount''=SUM(PD.Amount)
							FROM	[ICON_Payment_TmpReceipt]TR 
									LEFT OUTER JOIN [ICON_Payment_PaymentDetails]PD ON TR.RCReferent = PD.RCReferent AND TR.TmpReceiptID = PD.TmpReceiptID
									LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON AG.Bookingnumber = PD.Referentid
							WHERE	TR.RDate<='''+CONVERT(VARCHAR(50),@DateStartInStore,120)+'''
									AND PD.PaymentType IN (''4'') '
									IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(TR.ProductID = '''+@ProductID+''')'
									set @sql=@sql+'GROUP BY PD.Period,PD.ReferentID,PD.PaymentType,AG.Contractnumber '
									set @sql=@sql+'            
						)PD2 ON AP.ContractNumber = PD2.Contractnumber AND AP.Period = PD2.Period AND AP.PaymentType = PD2.PaymentType
				WHERE	AP.Duedate <= '''+CONVERT(VARCHAR(50),@DateStartInStore,120)+'''
						AND AP.PaymentType IN (''4'',''5'',''6'')
						AND (CASE	WHEN AP.PaymentType = ''4'' THEN ISNULL(AP.PayableAmount,0)-ISNULL(PD2.Amount,0)
									ELSE ISNULL(AP.PayableAmount,0)-ISNULL(PD.Amount,0) END) > 0
			)AP 
			LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = AP.ContractNumber 
			LEFT OUTER JOIN [PRJ].[Project] P ON P.ProductID = A.ProductID	
			LEFT OUTER JOIN [MST].[Company] CO ON CO.CompanyID = P.CompanyID 
			LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = A.ContractNumber
	WHERE	(TF.TransferDateApprove IS NULL OR TF.TransferDateApprove >'''+CONVERT(VARCHAR(50),@DateStartInStore,120)+''')
			AND (A.CancelDate IS NULL OR A.CancelDate >'''+CONVERT(VARCHAR(50),@DateStartInStore,120)+''')
			AND AP.DueDate<= '''+CONVERT(VARCHAR(50),@DateStartInStore,120)+'''
			AND A.ApproveDate <= '''+CONVERT(VARCHAR(50),@DateStartInStore,120)+'''
			'
			
			IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND(CO.CompanyID = '''+@CompanyID+''')'
			IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(A.ProductID = '''+@ProductID+''')'

set @sql=@sql+'      
)A '
set @sql=@sql+' GROUP BY A.CompanyNamethai,A.Project,A.CompanyID ' */

exec(@sql)


GO
