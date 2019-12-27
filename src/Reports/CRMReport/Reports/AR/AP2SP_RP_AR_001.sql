SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_AR_001] NULL,'60006','18000101','70000101',1,'Administrator Account'
CREATE PROC [dbo].[AP2SP_RP_AR_001]
	@CompanyID nvarchar(20),
    @ProductID nvarchar(20),
	@DateStart Datetime,    
	@DateEnd Datetime,
    @StatusAG nvarchar(10),
	@Username nvarchar(150)
AS
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
DECLARE @sql nvarchar(MAX)
SET @sql= '
		SELECT	''CompanyName'' = ''--C.CompanyNameThai,
				,''Project'' = '' --ISNULL(P.ProductID,'''')+''-''+P.Project,
				,''TypeOfRealEstate'' = '' --MA.TypeOfRealEstate+''-''+TE.TypeDescription,
				,''UnitNumer'' = '' --U.UnitNumber,
				,''ContractDate'' = ''--[dbo].[FormatDatetime](''TH'',''dd/mm/yy'',A.ContractDate),
				,''ContractNumber'' =  '' --A.ContractNumber,
				,''Customer'' = '' --ISNULL(AO.FirstName,'''')+'' ''+ISNULL(AO.LastName,''''),
				,''Flag'' = '' --UT.OperateType,
				,''TotalSellingPrice'' = '' --A.SellingPrice,
				,''Month'' = '' --[dbo].[fn_GetMonthName](Datepart(month,AP.DueDate))+''''+Substring([dbo].[fn_GetIntToString](Year(AP.DueDate)+543),3,2),
				,''Priority'' = '' --Datediff(month,GetDate(),AP.DueDate)+Year(AP.DueDate),
				, ''PayableAmount'' = '' --AP.PayableAmount,
				,''RowNo'' = '' --Row_Number() OVER (PARTITION BY A.ContractNumber,P.ProductID ORDER BY A.ContractNumber)
		FROM	[SAL].[Agreement] SA --Need to start using from table below
                /* [vw_ICON_Entforms_AgreementPeriod]AP LEFT OUTER JOIN
				[SAL].[Agreement] A ON AP.ContractNumber = A.ContractNumber LEFT OUTER JOIN
				[PRJ].[Unit] U ON A.UnitNumber = U.UnitNumber AND A.ProductID = U.ProductID LEFT OUTER JOIN
				[PRJ].[Model] MA ON MA.ProductID = U.ProductID AND MA.ModelID = U.ModelID LEFT OUTER JOIN
				[MST].[TypeOfRealEstate] TE ON MA.TypeOfRealEstate = TE.TypeID LEFT OUTER JOIN
				[PRJ].[Project] P ON A.ProductID = P.ProductID LEFT OUTER JOIN
				[MST].[Company] C ON P.CompanyID = C.CompanyID LEFT OUTER JOIN
				[ICON_Entforms_AgreementOwner] AO ON AO.ContractNumber = A.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete,0) = 0 LEFT OUTER JOIN
				(
					SELECT	UH.UnitNumber,UH.OldProductID,UH.CurrentStatus,UH.OperateDate,--UH.OperateType
							''OperateType'' = CASE	WHEN UH.OperateType = ''CO''THEN ''''
													WHEN UH.OperateType = ''BV''THEN ''X''
													WHEN UH.OperateType = ''V''THEN ''G'' 
													WHEN UH.OperateType = ''T''THEN ''E''
													WHEN UH.OperateType = ''BN''THEN ''ED''
													ELSE UH.OperateName END
					FROM  [ICON_EntForms_UnitHistory] UH INNER JOIN
					(
						SELECT     OldProductID, UnitNumber, MAX(OperateDate) AS MaxDate
						FROM       [ICON_EntForms_UnitHistory]
						WHERE      1=1'
						IF((YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateEnd,'')<>''))
								set @sql=@sql+' AND (OperateDate <= '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
								set @sql=@sql+'GROUP BY OldProductID, UnitNumber'
					set @sql=@sql+'
					) UHI ON UH.OldProductID = UHI.OldProductID AND UH.UnitNumber = UHI.UnitNumber AND  UH.OperateDate = UHI.MaxDate 
				)UT ON U.UnitNumber = UT.Unitnumber AND UT.OldProductID = U.ProductID 
		WHERE	AP.PaymentType = ''6'' AND A.ApproveDate IS NOT NULL 
			'
		IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(C.CompanyID = '''+@CompanyID+''')'
		IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(P.ProductID = '''+@ProductID+''')'
		IF(@StatusAG = '1') set @sql=@sql+' AND A.CancelDate IS NULL'
		IF(@StatusAG = '2') set @sql=@sql+' AND A.CancelDate IS NOT NULL'
		IF((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>''))
				set @sql=@sql+' AND (AP.DueDate BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
		set @sql=@sql+' ORDER BY U.UnitNumber,Datediff(month,GetDate(),AP.DueDate)+Year(AP.DueDate) */ '
	
	
EXEC(@sql)




GO
