SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_AG_R4] '','10140','','B01-3','','','','',''
-- [dbo].[AP2SP_RP_AG_R4] '','10073','','','2011-01-01','2011-05-31','','',''
-- [dbo].[AP2SP_RP_AG_R4] '','10073','','','','','','','2011-01-01','2011-05-31','Administrator Account'

CREATE PROCEDURE [dbo].[AP2SP_RP_AG_R4]
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
	@SBUID		nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,	
	@DateStart2 datetime ,
	@DateEnd2   datetime ,	
	@DateStart3 datetime ,
	@DateEnd3   datetime ,	
    @UserName   nvarchar(150)

AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
DECLARE @DateEndInStore Datetime,@A varchar(5),@DateEndInStore2 Datetime,@DateEndInStore3 Datetime
SET @DateEndInStore  = [dbo].[fn_GetMaxDate](@DateEnd)
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)
SET @DateEndInStore3 = [dbo].[fn_GetMaxDate](@DateEnd3)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

Declare @sql nvarchar(max)
Set @sql = '

SELECT	DISTINCT 
        ''ProjectName'' = '''' --PR.ProductID+''-''+PR.Project AS ProjectName
		,''UnitNumber'' = '''' --UN.UnitNumber
		,''TotalSellingPrice'' = '''' --ISNULL(ISNULL(AG.TotalSellingPrice,BK.TotalSellingPrice),0) AS TotalSellingPrice
		,''MiniPriceOld'' = '''' --SPMIN.SuggestionPrice AS MiniPriceOld
		,''MiniPriceNew'' = '''' --SPMAX.SuggestionPrice AS MiniPriceNew
		,''CashDiscount'' = '''' --CASE WHEN ISNULL(ISNULL(AG.PromotionID,BK.PromotionID),''SP'') like ''%SP%'' THEN 0 ELSE ISNULL(AG.CashDiscount,BK.CashDiscount) END AS CashDiscount
		,''TFADiscount'' = '''' --CASE WHEN ISNULL(ISNULL(AG.PromotionID,BK.PromotionID),''SP'') like ''%SP%'' THEN 0 ELSE ISNULL(AG.TransferDiscount,BK.TransferDiscount) END  AS TFADiscount
		,''TFTDiscount'' = '''' --ISNULL(TP.TransferDiscount,0) AS TFTDiscount
		,''BudgetOld'' = '''' --ISNULL(ZBMIN.BudgetPromotion,0) AS BudgetOld
		,''BudgetNew'' = '''' --ISNULL(ZBMAX.BudgetPromotion,0) AS BudgetNew
		,''ItemPrice'' = '''' --ISNULL(ZD.ItemPrice,0) AS ItemPrice
		,''PayPrice'' = '''' --ISNULL(ZF.PayPrice,0) AS PayPrice
		,''FGFDiscount'' = '''' --ISNULL(AG.FGFDiscount,0) AS FGFDiscount
		,''TFBudgetOld'' = '''' --ISNULL(TFMIN.BudgetPromotion,0) AS TFBudgetOld
		,''TFBudgetNew'' = '''' --ISNULL(TFMAX.BudgetPromotion,0) AS TFBudgetNew
		,''TFItemPrice'' = '''' --ISNULL(ZP.ItemPrice,0) AS TFItemPrice
		,''TFPayPrice'' = '''' --ISNULL(TF.PayPrice,0) AS TFPayPrice
		,''PromotionID'' = '''' --ISNULL(AG.PromotionID,BK.PromotionID) AS PromotionID
		,''DocumentID'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber) AS DocumentID
		,''TransferDateApprove'' = '''' --T.TransferDateApprove

FROM	[PRJ].[Unit] UN' --Need to use below table as well
		/* LEFT OUTER JOIN
		(
			SELECT	SP1.ProductID,SP1.UnitNumber,SP1.SuggestionPrice
			FROM	ICON_EntForms_SuggestionPrice SP1
					INNER JOIN
					(
						SELECT	ProductID,UnitNumber,Min(ImportDate) AS MINImportDate
						FROM	ICON_EntForms_SuggestionPrice
						GROUP BY ProductID,UnitNumber
					)SP2 ON SP1.ProductID = SP2.ProductID AND SP1.UnitNumber = SP2.UnitNumber AND SP1.ImportDate = SP2.MINImportDate
		)SPMIN ON SPMIN.ProductID = UN.ProductID AND SPMIN.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN 
		(
			SELECT	SP1.ProductID,SP1.UnitNumber,SP1.SuggestionPrice
			FROM	ICON_EntForms_SuggestionPrice SP1
					INNER JOIN
					(
						SELECT	ProductID,UnitNumber,Max(ImportDate) AS MAXImportDate
						FROM	ICON_EntForms_SuggestionPrice
						GROUP BY ProductID,UnitNumber
					)SP2 ON SP1.ProductID = SP2.ProductID AND SP1.UnitNumber = SP2.UnitNumber AND SP1.ImportDate = SP2.MAXImportDate
		)SPMAX ON SPMAX.ProductID = UN.ProductID AND SPMAX.UnitNumber = UN.UnitNumber '
Set @sql = @sql+ ' 
		LEFT OUTER JOIN ICON_EntForms_Booking BK ON BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber AND BK.CancelDate IS NULL
		LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber 
		LEFT OUTER JOIN ICON_EntForms_Products PR ON PR.ProductID = UN.ProductID
		LEFT OUTER JOIN ZPROM_TransferPromotion TP ON TP.ContractNumber = AG.ContractNumber AND ISNULL(TP.IsCancel,0) = 0
		LEFT OUTER JOIN ICON_EntForms_Transfer T ON AG.ContractNumber=T.ContractNumber
		LEFT OUTER JOIN
		(
			SELECT	ZB1.ProjectID,ZB1.UnitNumber,ZB1.BudgetPromotion
			FROM	ZPROM_BudgetPromotion ZB1
					INNER JOIN
					(
						SELECT	ProjectID,UnitNumber,Min(ImportDate) AS MINImportDate
						FROM	ZPROM_BudgetPromotion
						WHERE	BudgetType = 1 
							--AND BudgetPromotion > 0
						GROUP BY ProjectID,UnitNumber
					)ZB2 ON ZB1.ProjectID = ZB2.ProjectID AND ZB1.UnitNumber = ZB2.UnitNumber AND ZB1.ImportDate = ZB2.MINImportDate
		)ZBMIN ON ZBMIN.ProjectID = UN.ProductID AND ZBMIN.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN
		(
			SELECT	ZB1.ProjectID,ZB1.UnitNumber,ZB1.BudgetPromotion
			FROM	ZPROM_BudgetPromotion ZB1
					INNER JOIN
					(
						SELECT	ProjectID,UnitNumber,MAX(ImportDate) AS MAXImportDate
						FROM	ZPROM_BudgetPromotion
						WHERE	BudgetType = 1 
								--AND BudgetPromotion > 0
						GROUP BY ProjectID,UnitNumber
					)ZB2 ON ZB1.ProjectID = ZB2.ProjectID AND ZB1.UnitNumber = ZB2.UnitNumber AND ZB1.ImportDate = ZB2.MAXImportDate
		)ZBMAX ON ZBMAX.ProjectID = UN.ProductID AND ZBMAX.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN 
		(	
			SELECT  ZS.DocumentID,SUM(ZD.PricePerUnit*ZS.Amount) AS ItemPrice
			FROM	ZPROM_SalePromotionDetail ZS
					LEFT OUTER JOIN ZPROM_PromotionDetail ZD ON ZD.PromotionID = ZS.PromotionID AND ZD.ItemID = ZS.ItemID
			GROUP BY ZS.DocumentID		
		)ZD ON ZD.DocumentID = CASE WHEN AG.ContractNumber IS NULL THEN BK.BookingNumber ELSE AG.ContractNumber END 
		LEFT OUTER JOIN 
		(	
			SELECT  DocumentID,SUM(CASE PromotionFeeID WHEN ''15'' THEN Amount / 2 ELSE 
						CASE WHEN Charge=''H'' THEN Amount / 2 ELSE Amount END END) AS PayPrice
			FROM	ZPROM_SalePromotionFee 
			WHERE   ((PromotionFeeID = ''15'' AND Charge=''N'')
					OR (PromotionFeeID IN (''00'',''01'',''02'',''17'',''2G'',''37'',''000'') AND (Charge=''N'' OR Charge=''H'')))
			GROUP BY DocumentID		
		)ZF ON ZF.DocumentID = CASE WHEN AG.ContractNumber IS NULL THEN BK.BookingNumber ELSE AG.ContractNumber END   '
Set @sql = @sql+ ' 
		LEFT OUTER JOIN
		(
			SELECT	ZB1.ProjectID,ZB1.UnitNumber,ZB1.BudgetPromotion
			FROM	ZPROM_BudgetPromotion ZB1
					INNER JOIN
					(
						SELECT	ProjectID,UnitNumber,Min(ImportDate) AS MINImportDate
						FROM	ZPROM_BudgetPromotion
						WHERE	BudgetType = 2 
						GROUP BY ProjectID,UnitNumber
					)ZB2 ON ZB1.ProjectID = ZB2.ProjectID AND ZB1.UnitNumber = ZB2.UnitNumber AND ZB1.ImportDate = ZB2.MINImportDate
		)TFMIN ON TFMIN.ProjectID = UN.ProductID AND TFMIN.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN
		(
			SELECT	ZB1.ProjectID,ZB1.UnitNumber,ZB1.BudgetPromotion
			FROM	ZPROM_BudgetPromotion ZB1
					INNER JOIN
					(
						SELECT	ProjectID,UnitNumber,MAX(ImportDate) AS MAXImportDate
						FROM	ZPROM_BudgetPromotion
						WHERE	BudgetType = 2 
						GROUP BY ProjectID,UnitNumber
					)ZB2 ON ZB1.ProjectID = ZB2.ProjectID AND ZB1.UnitNumber = ZB2.UnitNumber AND ZB1.ImportDate = ZB2.MAXImportDate
		)TFMAX ON TFMAX.ProjectID = UN.ProductID AND TFMAX.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN 
		(	
			SELECT  ZP.TransferPromotionID,SUM(ZD.PricePerUnit*ZP.Amount) AS ItemPrice
			FROM	ZPROM_TransferPromotionDetail ZP
					LEFT OUTER JOIN ZPROM_PromotionDetail ZD ON ZD.PromotionID = ZP.PromotionID AND ZD.ItemID = ZP.ItemID
			WHERE ISNULL(ZP.IsSelected,0) = 1
			GROUP BY ZP.TransferPromotionID		
		)ZP ON ZP.TransferPromotionID = TP.TransferPromotionID
		LEFT OUTER JOIN 
		(	
			SELECT  TransferPromotionID,SUM(CASE PromotionFeeID WHEN ''15'' THEN Amount / 2 ELSE Amount END) AS PayPrice ---,SUM(Amount) AS PayPrice
			FROM	ZPROM_TransferPromotionFee 
			WHERE   ((PromotionFeeID = ''15'' AND Charge=''N'')
					OR (PromotionFeeID IN (''00'',''01'',''02'',''17'',''2G'',''37'') AND (Charge=''N'' OR Charge=''H'')))
			GROUP BY TransferPromotionID		
		)TF ON TF.TransferPromotionID = TP.TransferPromotionID 

WHERE UN.AssetType IN (2,4) 
	'

IF (Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(PR.CompanyID = '''+@CompanyID+''')'
IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' and(PR.ProductID = '''+@ProductID+''')'
IF (Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql=@sql+' and(UN.UnitNumber IN ('+@UnitNumber+'))' 
IF (Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql=@sql+' and(UN.UnitNumber = '''+@UnitNumber+''')'
IF (Isnull(@SBUID,'')<>'')set @sql=@sql+' and(PR.SBUID = '''+@SBUID+''')'
IF (Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   set @sql=@sql+' and(BK.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF (Year(@DateStart) = 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   set @sql=@sql+' and(BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'

IF (Year(@DateStart2) <> 1800) AND (Year(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
				   set @sql=@sql+' and(ISNULL(AG.pContractDate,AG.ContractDate) Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'
IF (Year(@DateStart2) = 1800) AND (Year(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
				   set @sql=@sql+' and(ISNULL(AG.pContractDate,AG.ContractDate) <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'

IF (Year(@DateStart3) <> 1800) AND (Year(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
				   set @sql=@sql+' and(T.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart3,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''')'
IF (Year(@DateStart3) = 1800) AND (Year(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
				   set @sql=@sql+' and(T.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''')'


Set @sql = @sql+ ' ORDER BY UN.UnitNumber ' */

EXEC(@sql)

GO
