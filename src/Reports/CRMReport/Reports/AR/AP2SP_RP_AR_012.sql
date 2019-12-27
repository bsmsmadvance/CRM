SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_AR_012] NULL,'10016','2009-06-01','2009-06-30','2009-06-01','2009-06-30',1,'Administrator Account'
CREATE PROC [dbo].[AP2SP_RP_AR_012]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@DateStart2 Datetime,
	@DateEnd2 Datetime,
    @StatusAG nvarchar(10),
	@Username nvarchar(150)

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)

DECLARE @sql nvarchar(Max)
SET @sql = '
	SELECT ''CompanyName'' = '''' --A.CompanyNameThai,
		, ''Project'' = '''' --A.Project,
        , ''TypeOfRealEstate'' = '''' --A.TypeOfRealEstate,
        , ''UnitNumber'' = '''' --A.UnitNumber,
        , ''ContractDate'' = '''' --A.ContractDate,
        , ''ContractNumber'' = '''' --A.ContractNumber,
		, ''Customer'' = '''' --A.Customer,
        , ''Flag'' = '''' --A.Flag,
        , ''Month'' = '''' --A.[Month],
        , ''Priority'' = '''' --A.Priority,
		,''TotaSellingPrice'' = '''' --A.SellingPrice
		,''PayableAmount'' = '''' --SUM(A.PayableAmount)
		,''RowNo'' = '''' --Row_Number() OVER (PARTITION BY A.ContractNumber,A.Project ORDER BY A.ContractNumber)
	FROM [SAL].[Agreement] A ' -- This is temp table. actual table start from below
	/* (
		SELECT ''Project'' = P.ProductID+''-''+P.Project,
			''TypeOfRealEstate'' = ISNULL(MA.TypeOfRealEstate,'''')+''-''+TE.TypeDescription,
			U.UnitNumber,
			''ContractDate'' = [dbo].[FormatDatetime](''TH'',''dd/mm/yy'',A.ContractDate),
			A.ContractNumber,
			''Customer'' = ISNULL(AO.FirstName,'''')+''''+ISNULL(AO.LastName,''''),
			''Flag'' = UT.OperateType,
			''TotaSellingPrice'' = A.SellingPrice,
			''Month'' = [dbo].[fn_GetMonthName](Datepart(month,DC.DueDate))+''''+Substring([dbo].[fn_GetIntToString](Year(DC.DueDate)+543),3,2),
			''Priority'' = Datediff(month,GetDate(),DC.DueDate)+Year(DC.DueDate),
			''PayableAmount'' = DC.Amount,
			C.CompanyNameThai,A.SellingPrice
		FROM
			(
				SELECT AP.DueDate,AP.PayableAmount,AP.Period,AP.Contractnumber,
					''Amount'' = ISNULL(PD.Amount,0),
					''Nextpayable'' = ISNULL(AP.PayableAmount,0)-ISNULL(PD.Amount,0)
				FROM [ICON_EntForms_AgreementPeriod]AP LEFT OUTER JOIN
				(
					SELECT PD.Period,PD.ReferentID,PD.PaymentType,
						''Amount''=SUM(PD.Amount)
					FROM [ICON_Payment_TmpReceipt]TR LEFT OUTER JOIN
						[ICON_Payment_PaymentDetails]PD ON TR.ReferentID=PD.ReferentID AND TR.RCReferent=PD.RCReferent
					WHERE PD.PaymentType = ''6'' '
					IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(TR.ProductID = '''+@ProductID+''')'
					IF((YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND (ISNULL(@DateStart2,'')<>'') AND (ISNULL(@DateEnd2,'')<>''))
							set @sql=@sql+' AND (TR.RDate BETWEEN '''+Convert(nvarchar(50),@DateStart2,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore2,120)+''')'
					SET @sql=@sql+' GROUP BY PD.Period,PD.ReferentID,PD.PaymentType'
			SET @sql=@sql+'
				)PD ON AP.ContractNumber=PD.ReferentID AND AP.Period=PD.Period AND AP.PaymentType=PD.PaymentType
				WHERE 	AP.PaymentType = ''6'' AND ISNULL(PD.Amount,0)>0 '
				IF((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>''))
						set @sql=@sql+' AND (AP.Duedate BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
			SET @sql=@sql+'

			)DC LEFT OUTER JOIN
			[ICON_Entforms_Agreement]A ON DC.ContractNumber = A.ContractNumber LEFT OUTER JOIN
			[ICON_Entforms_Unit]U ON A.UnitNumber = U.UnitNumber AND A.ProductID = U.ProductID LEFT OUTER JOIN
			[ICON_Entforms_ManageModel] MA ON MA.ProductID = U.ProductID AND MA.ModelID = U.ModelID LEFT OUTER JOIN
			[ICON_Entforms_TypeOFRealEstate]TE ON MA.TypeOfRealEstate = TE.TypeID LEFT OUTER JOIN
			[ICON_Entforms_Products]P ON A.ProductID = P.ProductID LEFT OUTER JOIN
			[ICON_Entforms_Company]C ON P.CompanyID = C.CompanyID LEFT OUTER JOIN
			[ICON_Entforms_AgreementOwner] AO ON AO.ContractNumber = A.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete,0) = 0 LEFT OUTER JOIN
				(
					SELECT	UH.UnitNumber,UH.OldProductID,UH.CurrentStatus,UH.OperateDate,--UH.OperateType
						''OperateType'' = CASE WHEN UH.OperateType = ''CO''THEN ''''
										WHEN UH.OperateType = ''BV''THEN ''X''
										WHEN UH.OperateType = ''V''THEN ''G'' 
										WHEN UH.OperateType = ''T''THEN ''E''
										WHEN UH.OperateType = ''BN''THEN ''ED''
										ELSE UH.OperateName END
					FROM  [ICON_EntForms_UnitHistory] UH INNER JOIN
						(
							 SELECT     OldProductID, UnitNumber, MAX(OperateDate) AS MaxDate
							 FROM       [ICON_EntForms_UnitHistory]
							 WHERE     1=1 '
							 IF((YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateEnd,'')<>''))
								set @sql=@sql+' AND (OperateDate <= '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
							 set @sql=@sql+'GROUP BY OldProductID, UnitNumber'
						SET @sql=@sql+'
						) UHI ON UH.OldProductID = UHI.OldProductID AND UH.UnitNumber = UHI.UnitNumber AND  UH.OperateDate = UHI.MaxDate 
			)UT ON U.UnitNumber = UT.Unitnumber AND UT.OldProductID = U.ProductID

		WHERE A.ApproveDate IS NOT NULL 
			'
		IF(@StatusAG = '1') set @sql=@sql+' AND A.CancelDate IS NULL'
		IF(@StatusAG = '2') set @sql=@sql+' AND A.CancelDate IS NOT NULL'
		IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND(C.CompanyID = '''+@CompanyID+''')'
		IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(P.ProductID = '''+@ProductID+''')'
	SET @sql=@sql+'
	)A
	GROUP BY A.Project,A.TypeOfRealEstate,A.UnitNumber,A.ContractDate,A.ContractNumber,A.Customer,A.Flag,A.TotaSellingPrice,
		A.[Month],A.Priority,A.CompanyNameThai,A.SellingPrice' */
EXEC(@sql)







GO
