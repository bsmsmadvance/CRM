SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--  [dbo].[AP2SP_RP_AG_001_1] NULL,'10059','0','2013-05-01','2013-05-09',1,'Administrator Account'
CREATE PROCEDURE [dbo].[AP2SP_RP_AG_001_1]
	@CompanyID  nvarchar(20),
	@ProductID	nvarchar(15),
    @StatusPeriod nvarchar(10),
	@DateStart	datetime,
	@DateStart2 Datetime,
	@CustomerStatus nvarchar(10)='0',
	@UserName nvarchar(250) = ''

AS

DECLARE @DateEndInStore Datetime
IF(YEAR(@DateStart) = 1800)  SET @DateEndInStore = [dbo].[fn_GetMaxDate](GetDate())
IF(YEAR(@DateStart) <> 1800)  SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateStart)

DECLARE @DateEndInStore2 Datetime
IF(YEAR(@DateStart2) = 1800)  SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](GetDate()) 
IF(YEAR(@DateStart2) <> 1800)  SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateStart2)


Declare @sql nvarchar(max)
Set @sql= '

SELECT  ''NextPayable'' = '''' --SUM(AP.Nextpayable) AS Nextpayable
        , ''PeriodAmt'' = '''' --AP.PeriodAmt
        , ''PeriodCount'' = '''' --SUM(AP.PeriodCount) AS PeriodCount
        , ''ProductID'' = ''''AP.ProductID
FROM [SAL].[Agreement] A' --This is temp table actual table start from below
/* (
	SELECT	A.Nextpayable
			,CASE WHEN A.PeriodAmt IN (3,4,5,6) THEN 4 ELSE A.PeriodAmt END AS PeriodAmt
			,A.PeriodCount
			,A.ProductID
	FROM
	(
		SELECT	''Nextpayable'' = SUM(AP.Nextpayable),AP.PeriodAmt
				,''PeriodCount'' = COUNT(AP.PeriodAmt),A.ProductID
		FROM
		(
				SELECT	AP.ContractNumber
						,''PeriodAmt'' = CASE	WHEN  COUNT(AP.Period) < 7 THEN  COUNT(AP.Period) ELSE 7 END
						,''Nextpayable'' = SUM(ISNULL(AP.PayableAmount,0))-SUM(ISNULL(PD.Amount,0))
				FROM	[ICON_Entforms_AgreementPeriod] AP LEFT OUTER JOIN
				(
					SELECT	''Amount'' = SUM(PD.Amount),
							PD.ReferentID,PD.Period
					FROM	[ICON_Payment_TmpReceipt]TR LEFT OUTER JOIN
							[ICON_Payment_PaymentDetails]PD ON TR.TmpReceiptID=PD.TmpReceiptID AND TR.RCReferent=PD.RCReferent
					WHERE	TR.RDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+'''
							AND PD.PaymentType = ''6''
							AND TR.CancelDate IS NULL
					GROUP BY PD.ReferentID,PD.Period
				)PD ON AP.ContractNumber = PD.ReferentID AND AP.Period = PD.Period
				WHERE (AP.DueDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
					AND (AP.PaymentType = ''6'')
					AND ((AP.PayableAmount - ISNULL(PD.Amount,0))>0) '			
				
	SET @sql = @sql + ' 
				GROUP BY AP.ContractNumber
		)AP 
		LEFT OUTER JOIN
		(
			SELECT	AG.ContractNumber,UnitNumber,ProductID,SellingPrice
			FROM	[ICON_Entforms_Agreement] AG
				LEFT OUTER JOIN [ICON_Entforms_AgreementOwner] AO ON AO.ContractNumber = AG.ContractNumber AND ISNULL(AO.Header,0) = 1 AND ISNULL(AO.IsDelete,0) = 0	
				LEFT OUTER JOIN [ICON_EntForms_Contacts] CT ON CT.ContactID = AO.ContactID
			WHERE	(CancelDate IS NULL OR CancelDate >'''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')
					AND ApproveDate IS NOT NULL '
					
				IF(@CustomerStatus = '1')set @sql=@sql+' AND (CT.CustomerType = 1)'
				IF(@CustomerStatus = '2')set @sql=@sql+' AND (CT.CustomerType = 2)'

				--IF(@CustomerStatus = '1')set @sql=@sql+' AND ([dbo].[fn_IsCustEmployeeAgreementType](ContractNumber) = 1)'
				--IF(@CustomerStatus = '2')set @sql=@sql+' AND ([dbo].[fn_IsCustEmployeeAgreementType](ContractNumber) = 2)'

SET @sql = @sql + ' 
		)A ON AP.ContractNumber = A.ContractNumber 
		LEFT OUTER JOIN [ICON_Entforms_Products]P ON A.ProductID = P.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_Transfer]TF ON AP.ContractNumber = TF.ContractNumber
		WHERE (ISNULL(TF.Approve3,0) = 0)'
			  IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND(P.CompanyID = '''+@CompanyID+''')'
			  IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(P.ProductID = '''+@ProductID+''')'
			  set @sql=@sql+ 'GROUP BY A.ProductID,AP.PeriodAmt' 
			  set @sql=@sql+ '
	)A 
--	WHERE 1=1 
)AS AP
WHERE 1=1 '

	   --IF(@statusperiod = '1')set @sql=@sql+' and(AP.PeriodAmt = 1)'
	   --IF(@statusperiod = '2')set @sql=@sql+' and(AP.PeriodAmt = 2)'
	   --IF(@statusperiod = '3')set @sql=@sql+' and(AP.PeriodAmt = 3)'
	   --IF(@statusperiod = '4')set @sql=@sql+' and(AP.PeriodAmt = 4)'
	   --IF(@statusperiod = '5')set @sql=@sql+' and(AP.PeriodAmt >= 7)'	  
	   
	   IF(@statusperiod = '1')set @sql=@sql+' AND (AP.PeriodAmt = 1)'
	   IF(@statusperiod = '2')set @sql=@sql+' AND (AP.PeriodAmt = 2)'
	   IF(@statusperiod = '3')set @sql=@sql+' AND (AP.PeriodAmt IN (3,4,5,6))'
	   IF(@statusperiod = '4')set @sql=@sql+' AND (AP.PeriodAmt >= 7)'
	   
	   SET @sql=@sql+'AND AP.ProductID IN (SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](''' + @UserName + ''')) '

	   SET @sql=@sql+'GROUP BY AP.ProductID,AP.PeriodAmt '
*/
PRINT( @sql)   
EXEC( @sql)


GO
