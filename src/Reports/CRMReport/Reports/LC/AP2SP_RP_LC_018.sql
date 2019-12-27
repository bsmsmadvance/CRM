SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- [dbo].[AP2SP_RP_LC_018] '','10060','',NULL,NULL,'',NULL,'Administrator Account'

ALTER PROC [dbo].[AP2SP_RP_LC_018]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,-- วันที่ทำสัญญา
	@DateEnd   datetime,-- วันที่ทำสัญญา
	@P_ID  nvarchar(10),
	@PromotionID  nvarchar(25),
	@Username nvarchar(150)
AS
DECLARE @sql nvarchar(MAX)


--หาโปรโมชั่นทั้งหมด
/* Set @sql = '
DECLARE @P TABLE (nOrder int identity,
		ContractNumber nvarchar(30),
		PromotionID nvarchar(30),
		ItemID nvarchar(30),
		PromotionDescription nvarchar(500),
		Amount money,
		SystemAmount money)'


Set @sql = @sql + '
INSERT INTO @p
SELECT T.ContractNumber, 
		T.PromotionID, NULL,
		''ส่วนลด ณ วันโอน'', ISNULL(T.TransferDiscount,0), ISNULL(P.TransferDiscount,0)
FROM ZPROM_TransferPromotion T LEFT OUTER JOIN
	ZPROM_Promotion P ON T.PromotionID = P.PromotionID 
    LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = T.ContractNumber
WHERE ISNULL(T.TransferDiscount,0) > 0 
	AND ISNULL(IsCancel,0) = 0 
	AND A.CancelDate IS NULL AND ISNULL(IsApproved2,0) = 1 '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (A.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'


Set @sql = @sql + '
INSERT INTO @p
SELECT TP.ContractNumber, 
	TP.PromotionID, TPD.ItemID,
	ISNULL(PD.DescriptionTH,'''') + 
			CASE WHEN PD.BrandTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.BrandTH,'''') + 
			CASE WHEN PD.SpecTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.SpecTH,'''') + 
			CASE WHEN PD.RemarkTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.RemarkTH,'''') + 
			'' จำนวน '' + CAST(TPD.Amount AS varchar(10)) + '' '' + ISNULL(PD.UnitNameTH,''''),
	TPD.Amount * PD.PricePerUnit, 0
FROM ZPROM_TransferPromotionDetail TPD LEFT OUTER JOIN
	ZPROM_PromotionDetail PD ON TPD.PromotionID = PD.PromotionID AND TPD.ItemID = PD.ItemID 
    LEFT OUTER JOIN ZPROM_TransferPromotion TP ON TP.TransferPromotionID = TPD.TransferPromotionID 
    LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = TP.ContractNumber
WHERE A.CancelDate IS NULL AND ISNULL(TP.IsApproved2,0) = 1  AND ISNULL(IsSelected,0) = 1'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (A.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (TPD.PromotionID = '''+@PromotionID+''')'
IF(ISNULL(@P_ID,'')<>'')Set @sql = @sql+ ' AND (TPD.ItemID = '''+@P_ID+''')'


Set @sql = @sql + '
INSERT INTO @p
SELECT TP.ContractNumber, 
	TP.PromotionID, NULL,
	CASE PromotionFeeID 
	WHEN ''00'' THEN ''ฟรีค่าส่วนกลาง '' + CASE WHEN Charge = ''N'' THEN CAST(CAST(Y AS Int) AS nvarchar(20)) ELSE CAST(CAST(Y AS Int) / 2 AS nvarchar(20)) END + '' เดือน''
	WHEN ''01'' THEN ''ฟรีค่ามิเตอร์ไฟฟ้า''
	WHEN ''02'' THEN ''ฟรีค่ามิเตอร์น้ำ''
	WHEN ''15'' THEN ''ฟรีค่าธรรมเนียมการโอน''				
	WHEN ''17'' THEN ''ฟรีค่าจดจำนอง เท่ากับ 1% ของราคาหน้าสัญญาจะซื้อจะขาย''
	WHEN ''2G'' THEN ''ฟรีค่ากองทุน''
	WHEN ''37'' THEN ''ฟรีค่าอากรแสตมป์ และค่าพยาน'' END, 
	CASE PromotionFeeID WHEN ''15'' THEN Amount / 2 ELSE Amount END, 0
FROM ZPROM_TransferPromotionFee TF 
    LEFT OUTER JOIN ZPROM_TransferPromotion TP ON TP.TransferPromotionID = TF.TransferPromotionID 
    LEFT OUTER JOIN [SAL].[Agreement] A ON TP.ContractNumber = A.ContractNumber 
WHERE A.CancelDate IS NULL AND ISNULL(TP.IsApproved2,0) = 1 
	AND	((PromotionFeeID = ''15'' AND Charge=''N'')
		OR (PromotionFeeID IN (''00'',''01'',''02'',''17'',''2G'',''37'') AND (Charge=''N'' OR Charge=''H'')))'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (A.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (A.PromotionID = '''+@PromotionID+''')'


--แสดงผล

Set @sql = @sql + ' */

--This @sql is for temp mapping only, actual mapping need to remove line 97 and use line 95
Set @sql = '

SELECT	''CompanyNameThai'' = C.NameTH
        ,''ProductID'' = P.ProjectNo
        ,''Project'' = P.ProjectNameTH
        ,''UnitNumber'' = U.UnitNo
		,''ShortName'' = ISNULL(TE.Name,''-'')
		,''TypeOfRealEstate'' = ISNULL(TE.Code,'''')+''-''+ISNULL(TE.Name,'''')
		,''Customer'' = [dbo].[fn_GenCustAgreementAllNoTitle](A.ID)
		,''ContractNumber'' = A.AgreementNo
		,''ID'' = '''' --PM.ItemID
		,''PromotionDescription'' = '''' --PM.PromotionDescription
		,''Amount'' = '''' --PM.Amount
		,''STAmount'' = '''' --PM.SystemAmount AS STAmount
		,''Cancel'' = '''' --A.Cancel
		,''Status'' = '''' /* CASE	WHEN A.Cancel = ''1'' THEN ''ยกเลิก''
							WHEN A.Cancel = ''2'' THEN ''เปลี่ยนห้อง''
							WHEN A.Cancel = ''3'' THEN ''ตั้งเรื่องยกเลิก''
							WHEN A.Cancel = ''4'' THEN ''เปลี่ยนชื่อ''
							WHEN ISNULL(A.Cancel,'''')=''''OR A.Cancel=0 THEN ''Active'' END */

FROM	[SAL].[Agreement] A --This is actual table need to use below table as well
		LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID AND U.ProjectID = A.ProjectID
		LEFT OUTER JOIN [PRJ].[Model] M ON U.ModelID = M.ID AND U.ProjectID = M.ProjectID
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TE ON TE.ID = M.TypeofRealEstateID
		LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = U.ProjectID 
		LEFT OUTER JOIN [MST].[Company] C ON C.ID = P.CompanyID '
		/* LEFT OUTER JOIN @p PM ON A.ContractNumber = PM.ContractNumber

WHERE ISNULL(PM.PromotionDescription,'''') <>'''' '

IF(ISNULL(@CompanyID,'')<>'')Set @sql = @Sql+' AND(C.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (P.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'')Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'
IF((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	Set @sql = @sql+' AND (A.ContractDate BETWEEN '''+Convert(nvarchar(10),@DateStart,120)+''' AND '''+Convert(nvarchar(10),@DateEnd,120)+''')'
IF((YEAR(@DateStart) <> 1800) AND  ISNULL(@DateStart,'')<>'')
	Set @sql = @sql+' AND (A.ContractDate >= '''+Convert(nvarchar(10),@DateStart,120)+''')'
IF((YEAR(@DateEnd) <> 7000) AND  ISNULL(@DateEnd,'')<>'')
	Set @sql = @sql+' AND (A.ContractDate <= '''+Convert(nvarchar(10),@DateEnd,120)+''')'
IF(ISNULL(@P_ID,'')<>'')Set @sql = @sql+ ' AND (PM.ItemID = '''+@P_ID+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (PM.PromotionID = '''+@PromotionID+''')'

SET @sql = @sql + ' ORDER BY P.ProductID,A.UnitNumber ASC;' */

EXEC(@sql)
--PRINT(@sql)


GO
