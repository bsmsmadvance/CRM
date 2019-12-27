SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--  [dbo].[AP2SP_RP_LC_028] '','10150','',NULL,NULL,NULL,NULL,'administrator account',NULL
--  [dbo].[AP2SP_RP_LC_028] '','10097','','2011-01-01','2012-12-01','2011-01-01','2012-12-31','Administrator Account','1'

ALTER PROC [dbo].[AP2SP_RP_LC_028]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,-- วันที่ทำสัญญา
	@DateEnd   datetime,-- วันที่ทำสัญญา
	@DateStart2 datetime,-- วันที่โอน
	@DateEnd2   datetime,-- วันที่โอน
	@Username nvarchar(150),
	@StatusAG nvarchar(10)
AS

DECLARE @DateEndInStore Datetime,@A varchar(5),@DateEndInStore2 Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)

DECLARE @sql nvarchar(MAX) = ''
DECLARE @sql2 nvarchar(MAX) = ''
DECLARE @sql3 nvarchar(MAX) = ''
DECLARE @sql4 nvarchar(MAX) = ''


--หาโปรโมชั่นทั้งหมด
/* Set @sql = '
DECLARE @P TABLE (nOrder int identity,DocumentID nvarchar(30),PromotionType int,PromotionID nvarchar(30),ItemID nvarchar(30),PromotionDescription nvarchar(500),Amount int,Price money,DeliveryTime int);

INSERT INTO @p
SELECT SPD.DocumentID,1,SPD.PromotionID,SPD.ItemID,
	ISNULL(PD.DescriptionTH,'''') + 
			CASE WHEN PD.BrandTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.BrandTH,'''') + 
			CASE WHEN PD.SpecTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.SpecTH,'''') + 
			CASE WHEN PD.RemarkTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.RemarkTH,'''') , 
	SPD.Amount ,PD.PricePerUnit,PD.DeliveryTime
FROM [SAL].[Booking] B 
    LEFT OUTER JOIN [SAL].[Agreement] A ON B.ID = A.BookingID 
    LEFT OUTER JOIN ZPROM_SalePromotionDetail SPD ON ISNULL(A.ContractNumber,B.BookingNumber) = SPD.DocumentID 
    LEFT OUTER JOIN ZPROM_PromotionDetail PD ON SPD.PromotionID = PD.PromotionID AND SPD.ItemID = PD.ItemID
WHERE (PD.DescriptionTH IS NOT NULL AND PD.DescriptionTH <> '''')'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (ISNULL(A.ProductID,B.ProductID) = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (ISNULL(A.UnitNumber,B.UnitNumber) = '''+@UnitNumber+''')'


Set @sql = @sql + '
INSERT INTO @p
SELECT TP.ContractNumber,2,TP.PromotionID, TPD.ItemID,
	ISNULL(PD.DescriptionTH,'''') + 
			CASE WHEN PD.BrandTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.BrandTH,'''') + 
			CASE WHEN PD.SpecTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.SpecTH,'''') + 
			CASE WHEN PD.RemarkTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.RemarkTH,'''') , 
	TPD.Amount,PD.PricePerUnit,PD.DeliveryTime
FROM ZPROM_TransferPromotionDetail TPD 
    LEFT OUTER JOIN ZPROM_PromotionDetail PD ON TPD.PromotionID = PD.PromotionID AND TPD.ItemID = PD.ItemID 
    LEFT OUTER JOIN ZPROM_TransferPromotion TP ON TP.TransferPromotionID = TPD.TransferPromotionID 
    LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = TP.ContractNumber
WHERE A.CancelDate IS NULL AND ISNULL(TP.IsApproved2,0) = 1  AND ISNULL(IsSelected,0) = 1'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (A.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'
*/

--แสดงผล

Set @sql2 = '
SELECT	''CompanyID'' = C.Code
    ,''CompanyNameThai'' = C.NameTH
    ,''ProductID'' = P.ProjectNo
    ,''Project'' = P.ProjectNameTH
    ,''UnitNumber'' = U.UnitNo
    ,''Customer'' = [dbo].[fn_GenCustAgreementAllNoTitle](A.ID)
	,''ContractNumber'' = A.AgreementNo
    ,''ApproveDate'' = '''' --dbo.FormatDateTime(''TH'', ''dd MMM yy'',A.ApproveDate)
	,''TransferDateApprove'' = '''' --dbo.FormatDateTime(''TH'', ''dd MMM yy'',T.TransferDateApprove)
	,''PromotionType'' = '''' --PM.PromotionType
    ,''PromotionID'' = '''' --PM.PromotionID
    ,''ItemID'' = '''' --PM.ItemID
    ,''PromotionDescription'' = '''' --PM.PromotionDescription
    ,''Amount'' = '''' --PM.Amount
    ,''Price'' = '''' --PM.Price
    ,''TotalPrice'' = '''' --(PM.Amount * PM.Price) AS TotalPrice
	,''DeliveryTimeType'' =  '''' --CASE WHEN PM.DeliveryTime = 1 THEN ''วันทำสัญญา'' WHEN PM.DeliveryTime = 2 THEN ''วันโอน'' END
	,''DeliveryTime'' = '''' --CASE WHEN PM.DeliveryTime = 1 THEN ISNULL(dbo.FormatDateTime(''TH'', ''dd MMM yy'',A.ApproveDate),'''') WHEN PM.DeliveryTime = 2 THEN ISNULL(dbo.FormatDateTime(''TH'', ''dd MMM yy'',T.TransferDateApprove),'''') END
	,''CreateDate'' = '''' --dbo.FormatDateTime(''TH'', ''dd MMM yy'',ISNULL(RO.OldCreateDate,RP.CreateDate))
	,''LCMApproveDate'' =  '''' --dbo.FormatDateTime(''TH'', ''dd MMM yy'',ISNULL(RO.OldApproveDate,RP.ApproveDate))
	,''DeliveryDate'' =  '''' --dbo.FormatDateTime(''TH'', ''dd MMM yy'',ISNULL(DO.OldApproveDate,DP.ApproveDate))
	,''DeliveryDay'' = '''' --DATEDIFF(day ,ISNULL(RO.OldApproveDate,RP.ApproveDate),ISNULL(DO.OldApproveDate,DP.ApproveDate))
	,''ReceiveAmount'' = '''' --ISNULL(RP.ReceiveAmount,0) + ISNULL(RO.OldReceiveAmount,0)
	,''DeliveryAmount'' = '''' --ISNULL(DP.DeliveryAmount,0) + ISNULL(DO.OldDeliveryAmount,0)
	,''PRNo'' = '''' ' --RP.PRNo '
	
Set @sql3 = @sql3 + '
FROM [SAL].[Agreement] A' --This is main table need to use below table as well
	/* LEFT OUTER JOIN [SAL].[Transfer] T ON T.AgreementID = A.ID
	LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = A.ProjectID 
	LEFT OUTER JOIN [MST].[Company] C ON C.ID = P.CompanyID 
    LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID
	LEFT OUTER JOIN @p PM ON A.ContractNumber = PM.DocumentID
	LEFT OUTER JOIN (
		SELECT R.DocumentID,R.PromotionType,RD.PromotionID,RD.ItemID,ISNULL(vwR.sap_prno,'''') as PRNo,
			MAX(R.CreateDate) AS CreateDate,MAX(R.ApproveDate) AS ApproveDate,COUNT(*) AS ReceiveAmount
		FROM [ZPROM_ReceivePromotion] R 
			LEFT OUTER JOIN [ZPROM_ReceivePromotionDetail] RD ON R.ReceivePromotionID=RD.ReceivePromotionID 
			LEFT OUTER JOIN AP_CRMPRO.dbo.vw_ProRec_Receiveitem vwR ON RD.ReceivePromotionID=vwR.ReceivePromotionID AND RD.PromotionID=vwR.PromotionID AND RD.ItemID=vwR.ItemID AND (vwR.sap_sts = ''S'' OR vwR.sap_sts = ''N'')
		WHERE R.IsApproved = 1
		GROUP BY R.DocumentID,R.PromotionType,RD.PromotionID,RD.ItemID,vwR.sap_prno
	)RP ON RP.DocumentID=PM.DocumentID AND RP.PromotionType=PM.PromotionType AND RP.PromotionID=PM.PromotionID AND RP.ItemID=PM.ItemID LEFT OUTER JOIN 
	(
		SELECT D.DocumentID,D.DocumentType,D.PromotionType,DD.PromotionID,DD.ItemID,MAX(D.ApproveDate) AS ApproveDate,COUNT(*) AS DeliveryAmount
		FROM [ZPROM_DeliveryPromotion] D 
			LEFT OUTER JOIN [ZPROM_DeliveryPromotionDetail] DD ON D.DeliveryPromotionID=DD.DeliveryPromotionID
		WHERE ISNULL(D.IsCancel,0) = 0 AND D.IsApproved = 1
		GROUP BY D.DocumentID,D.DocumentType,D.PromotionType,D.ApproveDate,DD.PromotionID,DD.ItemID
	)DP ON DP.DocumentID=PM.DocumentID AND DP.PromotionType=PM.PromotionType AND DP.PromotionID=PM.PromotionID AND DP.ItemID=PM.ItemID LEFT OUTER JOIN 
	(
		SELECT AG.ContractNumber,RD.PromotionID,RD.ItemID,MAX(R.CreateDate) AS OldCreateDate,MAX(R.ApproveDate) AS OldApproveDate,SUM(RD.ReceiveAmount) AS OldReceiveAmount
		FROM dbo.ICON_EntForms_Agreement AG  
			LEFT OUTER JOIN dbo.ZPROM_ReceivePromotion R ON AG.ContractReferent=R.DocumentID
			LEFT OUTER JOIN dbo.ZPROM_ReceivePromotionDetail RD ON R.ReceivePromotionID=RD.ReceivePromotionID 
		WHERE R.IsApproved = 1 AND R.PromotionType = 1 AND R.DocumentID IS NOT NULL
		GROUP BY AG.ContractNumber,RD.PromotionID,RD.ItemID	
	)RO ON RO.ContractNumber=PM.DocumentID AND RO.PromotionID=PM.PromotionID AND RO.ItemID=PM.ItemID LEFT OUTER JOIN 
	(	'

SET @sql4 = @sql4 +	
	'	SELECT AG.ContractNumber,DD.PromotionID,DD.ItemID,MAX(D.ApproveDate) AS OldApproveDate,SUM(DD.DeliveryAmount) AS OldDeliveryAmount
		FROM dbo.ICON_EntForms_Agreement AG  
			LEFT OUTER JOIN dbo.ZPROM_DeliveryPromotion D ON AG.ContractReferent=D.DocumentID
			LEFT OUTER JOIN dbo.ZPROM_DeliveryPromotionDetail DD ON D.DeliveryPromotionID=DD.DeliveryPromotionID 
		WHERE ISNULL(D.IsCancel,0) = 0 AND D.IsApproved = 1 AND D.PromotionType = 1 AND D.DocumentID IS NOT NULL
		GROUP BY AG.ContractNumber,DD.PromotionID,DD.ItemID	
	)DO ON DO.ContractNumber=PM.DocumentID AND DO.PromotionID=PM.PromotionID AND DO.ItemID=PM.ItemID 		
WHERE ISNULL(PM.PromotionDescription,'''') <>'''' '

IF(ISNULL(@CompanyID,'')<>'')Set @sql4 = @sql4+' AND (C.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')Set @sql4 = @sql4+' AND (P.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'')Set @sql4 = @sql4+' AND (A.UnitNumber = '''+@UnitNumber+''')'

IF((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	Set @sql4 = @sql4+' AND (A.ApproveDate BETWEEN '''+Convert(nvarchar(10),@DateStart,120)+''' AND '''+Convert(nvarchar(10),@DateEndInStore,120)+''')'
IF((YEAR(@DateStart) <> 1800) AND  ISNULL(@DateStart,'')<>'')
	Set @sql4 = @sql4+' AND (A.ApproveDate  >= '''+Convert(nvarchar(10),@DateStart,120)+''')'
IF((YEAR(@DateEnd) <> 7000) AND  ISNULL(@DateEnd,'')<>'')
	Set @sql4 = @sql4+' AND (A.ApproveDate  <= '''+Convert(nvarchar(10),@DateEndInStore,120)+''')'

IF((YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>'')
	Set @sql4 = @sql4+' AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(10),@DateStart2,120)+''' AND '''+Convert(nvarchar(10),@DateEndInStore2,120)+''')'
IF((YEAR(@DateStart2) <> 1800) AND  ISNULL(@DateStart2,'')<>'')
	Set @sql4 = @sql4+' AND (T.TransferDateApprove >= '''+Convert(nvarchar(10),@DateStart2,120)+''')'
IF((YEAR(@DateEnd2) <> 7000) AND  ISNULL(@DateEnd2,'')<>'')
	Set @sql4 = @sql4+' AND (T.TransferDateApprove <= '''+Convert(nvarchar(10),@DateEndInStore2,120)+''')'

IF(@StatusAG = '1') set @sql4=@sql4+' AND A.CancelDate IS NULL'
IF(@StatusAG = '2') set @sql4=@sql4+' AND A.CancelDate IS NOT NULL'

SET @sql4=@sql4+' ORDER BY A.ProductID,A.UnitNumber,PM.PromotionID,PM.ItemID ASC;'

EXEC(@sql+@sql2+@sql3+@sql4) */
--PRINT(@sql+@sql2+@sql3)

--This EXEC is for temp mapping only actual mapping need to remove line 167 and use line 163
EXEC(@sql2+@sql3)

GO
