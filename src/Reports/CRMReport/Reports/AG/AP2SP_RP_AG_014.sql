SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- AP2SP_RP_AG_014 '10117','','','','','','','','อริยดา ตึกดี','','','1','1','SH'

ALTER PROCEDURE [dbo].[AP2SP_RP_AG_014]
    @ProductID  nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,	
	@DateStart2 datetime ,
	@DateEnd2   datetime ,	
	@DateStart3 datetime ,
	@DateEnd3   datetime ,	
    @UserName   nvarchar(250),
    @MinPriceType int,  
	@StatusAG varchar(50)='',
	@HomeType nvarchar(20),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS

DECLARE @DateEndInStore Datetime,@A varchar(10),@DateEndInStore2 Datetime,@DateEndInStore3 Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)
SET @DateEndInStore3 = [dbo].[fn_GetMaxDate](@DateEnd3)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

Declare @sql nvarchar(max)
Set @sql =  'SELECT	''BUID'' = '''' --PType AS BUID
        ,''ProductID'' = '''' --PR.ProductID
        ,''ProjectName'' = '''' --PR.Project AS ProjectName
        ,''UnitNumber'' = '''' --UN.UnitNumber,
		,''AreaSale'' = '''' --ISNULL(UN.AreaFromPFB,ISNULL(UN.AreaFromRE,0)),
		,''BookingDate'' = '''' --BK.BookingDate
        ,''CancelDate'' = '''' --ISNULL(AG.CancelDate,BK.CancelDate),
		,''TransferDueDate'' = '''' --TF.TransferDateApprove,
		,''StatusName'' = '''' /* CASE WHEN BK.CancelDate IS NOT NULL THEN ''ยกเลิก''
					WHEN TF.TransferNumber IS NOT NULL AND TF.TransferDateApprove IS NOT NULL THEN ''โอน'' 
					WHEN AG.ContractNumber IS NOT NULL AND TF.TransferDateApprove IS NULL THEN ''สัญญา'' 
					WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL THEN ''จอง'' 
					ELSE ''ว่าง'' END, */
		,''TotalSellingPrice'' = '''' --ISNULL(AG.TotalSellingPrice,BK.TotalSellingPrice),
		,''CashDiscount'' = '''' --ISNULL(ISNULL(AG.CashDiscount,BK.CashDiscount),0) + ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0),
		,''SellingPrice'' = '''' --ISNULL(AG.SellingPrice,BK.SellingPrice) - ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0),
		,''MinROI'' = '''' --ISNULL(MaxOGN.SuggestionPrice, ISNULL(MinROI.SuggestionPrice,0)),
		,''MaxROI'' = '''' --ISNULL(MaxROI.SuggestionPrice, ISNULL(MaxOGN.SuggestionPrice,0)),
		,''MaxPType'' = '''' /* ISNULL(ZM.MinPriceType,CASE MAXPRICE.MinPriceType WHEN 1 THEN ''Original''
							WHEN 2 THEN ''ROI''
							WHEN 3 THEN ''Sale Price''
							WHEN 4 THEN ''Quarterly'' End), */
		,''MaxMinPrice'' = '''' --ISNULL(ISNULL(ZM.MinPrice,MAXPRICE.SuggestionPrice),0),
		,''TotalSalePromotionAmount'' = ''''--ISNULL(ZSP.SPPrice,0) + ISNULL(ZSF.SFPrice,0) + ISNULL(ISNULL(AG.FGFDiscount,BK.FGFDiscount),0) + ISNULL(ISNULL(OPMAg.SumPromotion,OPMBk.SumPromotion),0),
		,''TransferPromotionTransferDiscount'' = '''' --ISNULL(ZT.TransferDiscount,0),
		,''TotalTransferPromotionAmount'' = '''' --ISNULL(ZTP.TFPPrice,0) + ISNULL(ZTF.TFFPrice,0),
		,''TotalPromotionAmount'' = '''' --ISNULL(ZSP.SPPrice,0) + ISNULL(ZSF.SFPrice,0) + ISNULL(ISNULL(AG.FGFDiscount,BK.FGFDiscount),0) + ISNULL(ISNULL(OPMAg.SumPromotion,OPMBk.SumPromotion),0) + 
			--(ISNULL(ZT.TransferDiscount,0) + ISNULL(ZTP.TFPPrice,0) + ISNULL(ZTF.TFFPrice,0)),
		,''TotalReceivePromotion'' = '''' --ISNULL(ZRP.TotalReceivePromotion,0),
		,''IncreasingAreaPrice'' = '''' --ISNULL(TF.IncreasingAreaPrice,0)
FROM	[SAL].[Booking] BK' --This is main table need to use table below as well
		/* LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ICON_EntForms_Transfer TF ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN ICON_EntForms_Unit UN ON BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN ICON_EntForms_Products PR ON PR.ProductID = UN.ProductID 
		LEFT OUTER JOIN	(
			SELECT A.SuggestionPrice,A.ProductID,A.UnitNumber
			FROM dbo.ICON_EntForms_SuggestionPrice AS A INNER JOIN (
				SELECT ProductID,UnitNumber,MIN(ImportDate) AS MinDate
				FROM dbo.ICON_EntForms_SuggestionPrice S
				WHERE MinPriceType = 2 -- ROI
				GROUP BY ProductID,UnitNumber
			) AS B ON A.ProductID=B.ProductID AND A.UnitNumber=B.UnitNumber AND A.ImportDate=B.MinDate
		)MinROI ON MinROI.ProductID = UN.ProductID AND MinROI.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN (
			SELECT	A.ProductID,A.UnitNumber,A.SuggestionPrice
			FROM	ICON_EntForms_SuggestionPrice A INNER JOIN (
						SELECT	ProductID,UnitNumber,Max(ImportDate) AS MaxDate
						FROM	ICON_EntForms_SuggestionPrice
						WHERE MinPriceType = 2 -- ROI
						GROUP BY ProductID,UnitNumber
					)B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND A.ImportDate = B.MaxDate
		)MaxROI ON MaxROI.ProductID = UN.ProductID AND MaxROI.UnitNumber = UN.UnitNumber 	
		LEFT OUTER JOIN (
			SELECT	A.ProductID,A.UnitNumber,A.SuggestionPrice
			FROM	ICON_EntForms_SuggestionPrice A INNER JOIN (
						SELECT	ProductID,UnitNumber,Max(ImportDate) AS MaxDate
						FROM	ICON_EntForms_SuggestionPrice
						WHERE MinPriceType = 1 -- Original
						GROUP BY ProductID,UnitNumber
					)B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND A.ImportDate = B.MaxDate
		)MaxOGN ON MaxOGN.ProductID = UN.ProductID AND MaxOGN.UnitNumber = UN.UnitNumber 	
		LEFT OUTER JOIN (
			SELECT	A.ProductID,A.UnitNumber,A.SuggestionPrice,A.MinPriceType
			FROM	ICON_EntForms_SuggestionPrice A INNER JOIN (
						SELECT	ProductID,UnitNumber,Max(ImportDate) AS MAXImportDate
						FROM	ICON_EntForms_SuggestionPrice
						GROUP BY ProductID,UnitNumber
					)B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND A.ImportDate = B.MAXImportDate
		)MAXPRICE ON MAXPRICE.ProductID = UN.ProductID AND MAXPRICE.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN ZPROM_TransferPromotion ZT ON AG.ContractNumber = ZT.ContractNumber AND ISNULL(ZT.Iscancel,0) = 0 AND ISNULL(ZT.IsApproved2,0) = 1
		LEFT OUTER JOIN	(
			SELECT	A.DocumentID,SUM(A.Amount*B.PricePerUnit) AS SPPrice
			FROM	ZPROM_SalePromotionDetail A LEFT OUTER JOIN dbo.ZPROM_PromotionDetail B ON A.PromotionID=B.PromotionID AND A.ItemID=B.ItemID
			WHERE	A.Amount > 0 AND B.PricePerUnit > 0
			GROUP BY A.DocumentID
		)ZSP ON ZSP.DocumentID = ISNULL(AG.ContractNumber,BK.BookingNumber) 
		LEFT OUTER JOIN (	
			SELECT  DocumentID,
					SUM(CASE PromotionFeeID WHEN ''15'' THEN Amount / 2 ELSE Amount END) AS SFPrice
			FROM	ZPROM_SalePromotionFee
			WHERE   ((PromotionFeeID = ''15'' AND Charge=''N'') OR (PromotionFeeID IN (''000'',''00'',''01'',''02'',''17'',''2G'',''37'') AND Charge IN (''N'',''H'')))
			GROUP BY DocumentID		
		)ZSF ON ZSF.DocumentID = ISNULL(AG.ContractNumber,BK.BookingNumber)
		LEFT OUTER JOIN	(
			SELECT	ZT.ContractNumber,SUM(ZS.Amount*ZP.PricePerUnit) AS TFPPrice
			FROM	ZPROM_TransferPromotion ZT 
					LEFT OUTER JOIN ZPROM_TransferPromotionDetail ZS ON ZT.TransferPromotionID = ZS.TransferPromotionID
					LEFT OUTER JOIN ZPROM_PromotionDetail ZP ON ZS.PromotionID = ZP.PromotionID AND ZS.ItemID = ZP.ItemID
			WHERE	ISNULL(ZT.Iscancel,0) = 0 AND ISNULL(ZT.IsApproved2,0) = 1 AND IsSelected = 1
			GROUP BY ZT.ContractNumber
		)ZTP ON ZTP.ContractNumber = AG.ContractNumber 
		LEFT OUTER JOIN (	
			SELECT  ZT.ContractNumber,SUM(CASE PromotionFeeID WHEN ''15'' THEN ZF.Amount / 2 ELSE ZF.Amount END) AS TFFPrice
			FROM	ZPROM_TransferPromotion ZT 
					LEFT OUTER JOIN ZPROM_TransferPromotionFee ZF ON ZT.TransferPromotionID = ZF.TransferPromotionID
			WHERE   ISNULL(ZT.IsCancel,0) = 0 AND ISNULL(ZT.IsApproved2,0) = 1 AND ((PromotionFeeID = ''15'' AND Charge=''N'') OR (PromotionFeeID IN (''00'',''01'',''02'',''17'',''2G'',''37'') AND Charge IN (''N'',''H'')))
			GROUP BY ZT.ContractNumber		
		)ZTF ON ZTF.ContractNumber = AG.ContractNumber 
		LEFT OUTER JOIN 
		(
			SELECT DocumentID,SUM(RD.ReceiveAmount*PD.PricePerunit) AS TotalReceivePromotion
			FROM dbo.ZPROM_ReceivePromotion R LEFT OUTER JOIN 
				dbo.ZPROM_ReceivePromotionDetail RD ON R.ReceivePromotionID=RD.ReceivePromotionID LEFT OUTER JOIN 
				dbo.ZPROM_PromotionDetail PD ON RD.PromotionID=PD.PromotionID AND RD.ItemID=PD.ItemID
			WHERE IsApproved = 1 AND PromotionType = 1
			GROUP BY DocumentID		
		)ZRP ON ZRP.DocumentID = AG.ContractNumber 		
		LEFT OUTER JOIN 
		(
			SELECT B.BookingNumber
			,''SumPromotion'' = SUM(CASE WHEN PM.PromotionDescription LIKE ''%ส่วนลด ณ.วันโอนกรรมสิทธิ์%''	AND PM.Flag = 2 THEN 0 
						WHEN PM.PromotionDescription LIKE ''%ส่วนลด ณ วันโอนกรรมสิทธิ์%''	AND PM.Flag = 2   THEN 0
						WHEN PM.PromotionDescription LIKE ''%ส่วนลดณ.วันโอน%''	 AND PM.Flag = 2   THEN 0
						WHEN PM.PromotionDescription LIKE ''%ส่วนลดณ วันโอน%''	 AND PM.Flag = 2   THEN 0
						WHEN PM.PromotionDescription LIKE ''%ส่วนลด ณ วันโอน%''	 AND PM.Flag = 2   THEN 0
						ELSE PM.Amount END)	   
			FROM [ICON_EntForms_Booking] B  LEFT OUTER JOIN
				[ICON_EntForms_PromotionDescription] PM ON PM.ID IN(SELECT * FROM dbo.fn_SplitString(B.PromotionDetail,'','')) AND PM.PromotionID = B.PromotionID AND PM.PromotionDescription NOT LIKE ''%ส่วนลดเงินสด%'' AND PM.PromotionDescription NOT LIKE ''%ส่วนลดหน้าสัญญา%''		
			WHERE 1=1 '
			
IF (Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	set @sql=@sql+' AND (B.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF (Year(@DateStart) = 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	set @sql=@sql+' AND (B.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'				   			

SET @sql = @sql + '
			GROUP BY B.BookingNumber
		)OPMBk ON OPMBk.BookingNumber = BK.BookingNumber	
		LEFT OUTER JOIN 
		(
			SELECT A.ContractNumber
			,''SumPromotion'' = SUM(CASE WHEN PM.PromotionDescription LIKE ''%ส่วนลด ณ.วันโอนกรรมสิทธิ์%''	AND PM.Flag = 2 THEN 0 
						WHEN PM.PromotionDescription LIKE ''%ส่วนลด ณ วันโอนกรรมสิทธิ์%''	AND PM.Flag = 2   THEN 0
						WHEN PM.PromotionDescription LIKE ''%ส่วนลดณ.วันโอน%''	 AND PM.Flag = 2   THEN 0
						WHEN PM.PromotionDescription LIKE ''%ส่วนลดณ วันโอน%''	 AND PM.Flag = 2   THEN 0
						WHEN PM.PromotionDescription LIKE ''%ส่วนลด ณ วันโอน%''	 AND PM.Flag = 2   THEN 0
						ELSE PM.Amount END)	   
			FROM [ICON_EntForms_Agreement] A  LEFT OUTER JOIN
				[ICON_EntForms_PromotionDescription] PM ON CAST(PM.ID AS nvarchar(50)) IN(SELECT * FROM dbo.fn_SplitString(A.PromotionDetail,'','')) AND PM.PromotionID = A.PromotionID AND PM.PromotionDescription NOT LIKE ''%ส่วนลดเงินสด%'' AND PM.PromotionDescription NOT LIKE ''%ส่วนลดหน้าสัญญา%'' LEFT OUTER JOIN	
				[ICON_EntForms_Booking] B ON A.BookingNumber=B.BookingNumber
			WHERE 1=1 '	

IF (Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	set @sql=@sql+' AND (B.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF (Year(@DateStart) = 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	set @sql=@sql+' AND (B.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'		
	
IF (Year(@DateStart2) <> 1800) AND (Year(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
   set @sql=@sql+' AND (ISNULL(A.pContractDate,A.ContractDate) Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'
IF (Year(@DateStart2) = 1800) AND (Year(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
   set @sql=@sql+' AND (ISNULL(A.pContractDate,A.ContractDate) <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'

SET @sql = @sql + '
			GROUP BY A.ContractNumber
		)OPMAg ON OPMAg.ContractNumber = AG.ContractNumber LEFT OUTER JOIN 
		(
			SELECT A.ProductID,B.UnitNumber,A.NetAmount AS MinPrice,A.DocumentType,
				CASE WHEN A.BudgetType = ''AdHoc1'' OR A.BudgetType = ''AdHoc2'' THEN ''Sale Price*''
					WHEN A.BudgetType = ''Quarterly'' THEN ''Quarterly*'' END AS MinPriceType							
			FROM dbo.Z_BudgetApprove A INNER JOIN  
				(
					SELECT ProductID,UnitNumber,MAX(CreateDate) AS MaxDate  
					FROM dbo.Z_BudgetApprove  
					WHERE [Status] = ''Finish'' AND DocumentCancel=0 AND BudgetType<>''Extra''
					GROUP BY ProductID, UnitNumber
				) AS B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND B.MaxDate = A.CreateDate
			WHERE A.[Status] = ''Finish'' AND A.DocumentCancel=0 AND A.BudgetType<>''Extra''
		) ZM ON BK.ProductID=ZM.ProductID AND BK.UnitNumber=ZM.UnitNumber 		
WHERE	UN.AssetType IN (2,4) 
		AND BK.BookingNumber IS NOT NULL 
		AND PR.ProductID IN (SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](''' + @UserName + ''')) '
		
IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' AND (PR.ProductID = '''+@ProductID+''')'

IF (Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql=@sql+' AND (UN.UnitNumber IN ('+@UnitNumber+'))' 
IF (Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql=@sql+' AND (UN.UnitNumber = '''+@UnitNumber+''')'

IF (Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
   set @sql=@sql+' AND (BK.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF (Year(@DateStart) = 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
   set @sql=@sql+' AND (BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'

IF (Year(@DateStart2) <> 1800) AND (Year(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
   set @sql=@sql+' AND (ISNULL(AG.pContractDate,AG.ContractDate) Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'
IF (Year(@DateStart2) = 1800) AND (Year(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
   set @sql=@sql+' AND (ISNULL(AG.pContractDate,AG.ContractDate) <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'

IF (Year(@DateStart3) <> 1800) AND (Year(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
   set @sql=@sql+' AND (TF.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart3,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''')'
IF (Year(@DateStart3) = 1800) AND (Year(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
   set @sql=@sql+' AND (TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''')'


IF (Isnull(@MinPriceType,0)<>0) set @sql=@sql+' AND (MAXPRICE.MinPriceType = '''+CAST(@MinPriceType AS nvarchar(2))+''')'

IF(@StatusAG = '1')set @sql=@sql+' AND (ISNULL(AG.Canceldate,BK.CancelDate) IS NULL)'
IF(@StatusAG = '2')set @sql=@sql+' AND (ISNULL(AG.Canceldate,BK.CancelDate) IS NOT NULL)'
IF(@StatusAG = '3')set @sql=@sql+' AND (TF.TransferDateApprove IS NULL)' -- ยังไม่โอน
IF(@StatusAG = '4')set @sql=@sql+' AND (ISNULL(AG.Canceldate,BK.CancelDate) IS NULL AND TF.TransferDateApprove IS NOT NULL)' -- โอนแล้ว


IF(ISNULL(@HomeType,'')<>'' AND ISNULL(@HomeType,'')<>'ทั้งหมด') set @sql=@sql+' AND (PR.PType = ''' + @HomeType + ''')'

IF(ISNULL(@ProjectGroup,'')<>'') 
	set @sql=@sql+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'

IF(ISNULL(@ProjectType2,'')<>'') 
	set @sql=@sql+' AND (PR.ProjectType = ''' + @ProjectType2 + ''')' */

set @sql=@sql+'
UNION
SELECT	''BUID'' = '''' --PType AS BUID
        ,''ProductID'' = '''' --PR.ProductID
        ,''Project'' = '''' --PR.Project AS ProjectName
        ,''UnitNumber'' = '''' --UN.UnitNumber,
		,''AreaSale'' = '''' --ISNULL(UN.AreaFromPFB,ISNULL(UN.AreaFromRE,0)),NULL,NULL,NULL,''ว่าง'',0,0,0,			
		,''MinROI'' = '''' --ISNULL(MaxOGN.SuggestionPrice, ISNULL(MinROI.SuggestionPrice,0)),
		,''MaxROI'' = '''' --ISNULL(MaxROI.SuggestionPrice, ISNULL(MaxOGN.SuggestionPrice,0)),
		,''MaxPType'' = '''' --CASE MAXPRICE.MinPriceType WHEN 1 THEN ''Original'' WHEN 2 THEN ''ROI'' WHEN 3 THEN ''Sale Price'' WHEN 4 THEN ''Quarterly'' End,
		,''MaxMinPrice'' = '''' --ISNULL(MAXPRICE.SuggestionPrice,0),0,0,0,0,0,0
FROM	[PRJ].[Unit] UN' --This is main table need to use below table as well
		/* LEFT OUTER JOIN ICON_EntForms_Products PR ON PR.ProductID = UN.ProductID 
		LEFT OUTER JOIN	(
			SELECT A.SuggestionPrice,A.ProductID,A.UnitNumber
			FROM dbo.ICON_EntForms_SuggestionPrice AS A INNER JOIN (
				SELECT ProductID,UnitNumber,MIN(ImportDate) AS MinDate
				FROM dbo.ICON_EntForms_SuggestionPrice S
				WHERE MinPriceType = 2 -- ROI
				GROUP BY ProductID,UnitNumber
			) AS B ON A.ProductID=B.ProductID AND A.UnitNumber=B.UnitNumber AND A.ImportDate=B.MinDate
		)MinROI ON MinROI.ProductID = UN.ProductID AND MinROI.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN (
			SELECT	A.ProductID,A.UnitNumber,A.SuggestionPrice
			FROM	ICON_EntForms_SuggestionPrice A
					INNER JOIN (
						SELECT	ProductID,UnitNumber,Max(ImportDate) AS MaxDate
						FROM	ICON_EntForms_SuggestionPrice
						WHERE MinPriceType = 2 -- ROI
						GROUP BY ProductID,UnitNumber
					)B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND A.ImportDate = B.MaxDate
		)MaxROI ON MaxROI.ProductID = UN.ProductID AND MaxROI.UnitNumber = UN.UnitNumber 	
		LEFT OUTER JOIN (
			SELECT	A.ProductID,A.UnitNumber,A.SuggestionPrice
			FROM	ICON_EntForms_SuggestionPrice A
					INNER JOIN (
						SELECT	ProductID,UnitNumber,Max(ImportDate) AS MaxDate
						FROM	ICON_EntForms_SuggestionPrice
						WHERE MinPriceType = 1 -- Original
						GROUP BY ProductID,UnitNumber
					)B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND A.ImportDate = B.MaxDate
		)MaxOGN ON MaxOGN.ProductID = UN.ProductID AND MaxOGN.UnitNumber = UN.UnitNumber 	
		LEFT OUTER JOIN (
			SELECT	A.ProductID,A.UnitNumber,A.SuggestionPrice,A.MinPriceType
			FROM	ICON_EntForms_SuggestionPrice A
					INNER JOIN (
						SELECT	ProductID,UnitNumber,Max(ImportDate) AS MAXImportDate
						FROM	ICON_EntForms_SuggestionPrice
						GROUP BY ProductID,UnitNumber
					)B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND A.ImportDate = B.MAXImportDate
		)MAXPRICE ON MAXPRICE.ProductID = UN.ProductID AND MAXPRICE.UnitNumber = UN.UnitNumber					
WHERE	UN.AssetType IN (2,4) 
	AND NOT EXISTS(SELECT * FROM ICON_EntForms_Booking WHERE ProductID = UN.ProductID AND UnitNumber = UN.UnitNumber AND CancelDate IS NULL) '
	
IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' AND (PR.ProductID = '''+@ProductID+''')'

IF (Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql=@sql+' AND (UN.UnitNumber IN ('+@UnitNumber+'))' 
IF (Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql=@sql+' AND (UN.UnitNumber = '''+@UnitNumber+''')'

IF (Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	set @sql=@sql+' AND 1=0'

IF (Year(@DateStart2) <> 1800) AND (Year(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
	set @sql=@sql+' AND 1=0'

IF (Year(@DateStart3) <> 1800) AND (Year(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
	set @sql=@sql+' AND 1=0'

IF (Isnull(@MinPriceType,0)<>0) set @sql=@sql+' AND (MAXPRICE.MinPriceType = '''+CAST(@MinPriceType AS nvarchar(2))+''')'

IF(ISNULL(@HomeType,'')<>'' AND ISNULL(@HomeType,'')<>'ทั้งหมด') set @sql=@sql+' AND (PR.PType = ''' + @HomeType + ''') '

Set @sql = @sql+ ' AND PR.ProductID IN (SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](''' + @UserName + ''')) '

IF(ISNULL(@ProjectGroup,'')<>'') 
	set @sql=@sql+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'
	
IF(ISNULL(@ProjectType2,'')<>'') 
	set @sql=@sql+' AND (PR.ProjectType = ''' + @ProjectType2 + ''')'

Set @sql = @sql+ ' ORDER BY BUID,ProductID,UnitNumber ASC;' */

EXEC(@sql)
--Print(@sql)


GO
