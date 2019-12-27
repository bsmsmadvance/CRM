SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_LC_016]'','10172','',NULL,NULL,NULL,NULL,'ITM20001','ST15000118','Administrator Account',''
-- [dbo].[AP2SP_RP_LC_016]'','10087','12asa09',NULL,NULL,NULL,NULL,'','','Administrator Account',''

ALTER PROC [dbo].[AP2SP_RP_LC_016]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,-- วันที่จอง
	@DateEnd   datetime,-- วันที่จอง
	@DateStart2 datetime,-- วันที่ทำสัญญา
	@DateEnd2   datetime,-- วันที่ทำสัญญา
	@P_ID  nvarchar(10),
	@PromotionID  nvarchar(25),
	@Username nvarchar(150),
	@StatusAG nvarchar(10)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
DECLARE @DateEndInStore Datetime,@A varchar(5),@DateEndInStore2 Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)

DECLARE @sql nvarchar(MAX)

--หาโปรโมชั่นทั้งหมด
/* Set @sql = '
DECLARE @P TABLE (nOrder int identity,
		DocumentID nvarchar(30),
		DocumentType int,
		PromotionID nvarchar(30),
		ItemID nvarchar(30),
		PromotionDescription nvarchar(500),
		Amount money,
		SystemAmount money);


INSERT INTO @p
SELECT ISNULL(A.AgreementNo,B.BookingNo),
	   CASE WHEN A.AgreementNo IS NULL THEN 1 ELSE 2 END,
		P.PromotionID, NULL,
		''ส่วนลดเงินสด'', ISNULL(A.CashDiscount,ISNULL(B.CashDiscount,0)), ISNULL(P.CashDiscount,0)
FROM [SAL].[Booking] B WITH (NOLOCK)  LEFT OUTER JOIN 
	[SAL].[Agreement] A WITH (NOLOCK)  ON B.ID=A.BookingID LEFT OUTER JOIN 
	ZPROM_Promotion P WITH (NOLOCK)  ON ISNULL(A.PromotionID,B.PromotionID) = P.PromotionID
WHERE ISNULL(A.CashDiscount,ISNULL(B.CashDiscount,0)) > 0 
	AND EXISTS(SELECT * FROM ZPROM_Promotion WITH (NOLOCK)  WHERE PromotionID = ISNULL(A.PromotionID,B.PromotionID)) '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (ISNULL(A.ProductID,B.ProductID) = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (ISNULL(A.UnitNumber,B.UnitNumber) = '''+@UnitNumber+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (P.PromotionID = '''+@PromotionID+''')'

Set @sql = @sql + '
INSERT INTO @p
SELECT ISNULL(A.AgreementNo,B.BookingNo), 
		CASE WHEN A.AgreementNo IS NULL THEN 1 ELSE 2 END,
		P.PromotionID, NULL,
		''ส่วนลด ณ วันโอน'', ISNULL(A.TransferDiscount,ISNULL(B.TransferDiscount,0)), ISNULL(P.TransferDiscount,0)
FROM [SAL].[Booking] B WITH (NOLOCK)  LEFT OUTER JOIN 
	[SAL].[Agreement] A WITH (NOLOCK)  ON B.ID=A.BookingID LEFT OUTER JOIN 
	ZPROM_Promotion P WITH (NOLOCK)  ON ISNULL(A.PromotionID,B.PromotionID) = P.PromotionID
WHERE ISNULL(A.TransferDiscount,ISNULL(B.TransferDiscount,0)) > 0
	AND EXISTS(SELECT * FROM ZPROM_Promotion WITH (NOLOCK)  WHERE PromotionID = ISNULL(A.PromotionID,B.PromotionID)) '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (ISNULL(A.ProductID,B.ProductID) = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (ISNULL(A.UnitNumber,B.UnitNumber) = '''+@UnitNumber+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (P.PromotionID = '''+@PromotionID+''')'

Set @sql = @sql + '
INSERT INTO @p
SELECT ISNULL(A.AgreementNo,B.BookingNo), 
		CASE WHEN A.AgreementNo IS NULL THEN 1 ELSE 2 END,
		P.PromotionID, NULL,
		''ส่วนลด FGF'', ISNULL(A.FGFDiscount,ISNULL(B.FGFDiscount,0)), ISNULL(P.FGFDiscount,0)
FROM [SAL].[Booking] B WITH (NOLOCK)  LEFT OUTER JOIN 
	[SAL].[Agreement] A WITH (NOLOCK)  ON B.ID=A.BookingID LEFT OUTER JOIN 
	ZPROM_Promotion P WITH (NOLOCK)  ON ISNULL(A.PromotionID,B.PromotionID) = P.PromotionID
WHERE ISNULL(A.FGFDiscount,ISNULL(B.FGFDiscount,0)) > 0 
	AND EXISTS(SELECT * FROM ZPROM_Promotion WITH (NOLOCK)  WHERE PromotionID = ISNULL(A.PromotionID,B.PromotionID)) '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (ISNULL(A.ProductID,B.ProductID) = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (ISNULL(A.UnitNumber,B.UnitNumber) = '''+@UnitNumber+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (P.PromotionID = '''+@PromotionID+''')'

Set @sql = @sql + '
INSERT INTO @p
SELECT SPD.DocumentID,SPD.DocumentType,SPD.PromotionID,SPD.ItemID,
	ISNULL(PD.DescriptionTH,'''') + 
			CASE WHEN PD.BrandTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.BrandTH,'''') + 
			CASE WHEN PD.SpecTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.SpecTH,'''') + 
			CASE WHEN PD.RemarkTH IS NULL THEN '''' ELSE '' '' END + ISNULL(PD.RemarkTH,'''') + 
			'' จำนวน '' + CAST(SPD.Amount AS varchar(10)) + '' '' + ISNULL(PD.UnitNameTH,''''),
	SPD.Amount * PD.PricePerUnit, SPD.Amount * PD.PricePerUnit
FROM [SAL].[Booking] B WITH (NOLOCK)  LEFT OUTER JOIN 
	[SAL].[Agreement] A WITH (NOLOCK)  ON B.ID=A.BookingID LEFT OUTER JOIN  
	ZPROM_SalePromotionDetail SPD WITH (NOLOCK)  ON ISNULL(A.ContractNumber,B.BookingNumbe) = SPD.DocumentID LEFT OUTER JOIN
	ZPROM_PromotionDetail PD WITH (NOLOCK)  ON SPD.PromotionID = PD.PromotionID AND SPD.ItemID = PD.ItemID
WHERE (PD.DescriptionTH IS NOT NULL AND PD.DescriptionTH <> '''') '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (ISNULL(A.ProductID,B.ProductID) = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (ISNULL(A.UnitNumber,B.UnitNumber) = '''+@UnitNumber+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (SPD.PromotionID = '''+@PromotionID+''')'
IF(ISNULL(@P_ID,'')<>'')Set @sql = @sql+ ' AND (SPD.ItemID = '''+@P_ID+''')'

Set @sql = @sql + '
INSERT INTO @p
SELECT DocumentID,S.DocumentType,ISNULL(A.PromotionID,B.PromotionID),NULL,
	CASE PromotionFeeID 
	WHEN ''00'' THEN ''ฟรีค่าส่วนกลาง '' + CASE WHEN Charge = ''N'' THEN CAST(CAST(Y AS Int) AS nvarchar(20)) ELSE CAST(CAST(Y AS Int) / 2 AS nvarchar(20)) END + '' เดือน''
	WHEN ''01'' THEN ''ฟรีค่ามิเตอร์ไฟฟ้า''
	WHEN ''02'' THEN ''ฟรีค่ามิเตอร์น้ำ''
	WHEN ''15'' THEN ''ฟรีค่าธรรมเนียมการโอน''				
	WHEN ''17'' THEN ''ฟรีค่าจดจำนอง''
	WHEN ''2G'' THEN ''ฟรีค่ากองทุน''
	WHEN ''37'' THEN ''ฟรีค่าอากรแสตมป์ และค่าพยาน'' END, 
	CASE PromotionFeeID WHEN ''15'' THEN Amount / 2 ELSE Amount END, 0
FROM [SAL].[Booking] B WITH (NOLOCK)  LEFT OUTER JOIN 
	[SAL].[Agreement] A WITH (NOLOCK)  ON B.ID=A.BookingID LEFT OUTER JOIN  
	ZPROM_SalePromotionFee S WITH (NOLOCK)  ON ISNULL(A.ContractNumber,B.BookingNumber) = S.DocumentID
WHERE  ((PromotionFeeID = ''15'' AND Charge=''N'')
	OR (PromotionFeeID IN (''00'',''01'',''02'',''17'',''2G'',''37'') AND (Charge=''N'' OR Charge=''H''))) '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (ISNULL(A.ProductID,B.ProductID) = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (ISNULL(A.UnitNumber,B.UnitNumber) = '''+@UnitNumber+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (ISNULL(A.PromotionID,B.PromotionID) = '''+@PromotionID+''')'

Set @sql = @sql + '
INSERT INTO @p
SELECT  B.Bookingnumber, 1
	,PM.PromotionID
	,PM.ID
	, PM.PromotionDescription
	,CASE WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 1 THEN B.CashDiscount
	WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 2 THEN B.TransferDisCount
	ELSE PM.Amount END
	,PM.Amount   
FROM [SAL].[Booking] B WITH (NOLOCK)  LEFT OUTER JOIN
	[ICON_EntForms_PromotionDescription] PM WITH (NOLOCK)  ON PM.ID IN(SELECT * FROM dbo.fn_SplitString(B.PromotionDetail,'','')) AND PM.PromotionID = B.PromotionID 
WHERE NOT EXISTS(SELECT * FROM ZPROM_SalePromotionDetail WITH (NOLOCK)  WHERE DocumentID = B.BookingNumber AND DocumentType = 1) '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (B.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (B.UnitNumber = '''+@UnitNumber+''')'
IF(ISNULL(@P_ID,'')<>'')Set @sql = @sql+ ' AND (CAST(PM.ID AS nvarchar(20)) = '''+@P_ID+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (PM.PromotionID = '''+@PromotionID+''')'

Set @sql = @sql + '
INSERT INTO @p
SELECT  A.ContractNumber, 2
	,PM.PromotionID
	,PM.ID
	, PM.PromotionDescription
	,CASE WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 1 THEN A.CashDiscount
	WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 2 THEN A.TransferDisCount
	ELSE PM.Amount END
	,PM.Amount   
FROM [SAL].[Agreement] A  WITH (NOLOCK)  LEFT OUTER JOIN
	[ICON_EntForms_PromotionDescription] PM WITH (NOLOCK)  ON PM.ID IN(SELECT * FROM dbo.fn_SplitString(A.PromotionDetail,'','')) AND PM.PromotionID = A.PromotionID 
WHERE NOT EXISTS(SELECT * FROM ZPROM_SalePromotionDetail WITH (NOLOCK)  WHERE DocumentID = A.ContractNumber AND DocumentType = 2) '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (A.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'
IF(ISNULL(@P_ID,'')<>'') Set @sql = @sql+ ' AND (CAST(PM.ID AS nvarchar(20)) = '''+@P_ID+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (PM.PromotionID = '''+@PromotionID+''')'

Set @sql = @sql + '
INSERT INTO @p
select DocumentID
,DocumentType
,''PromotionID'' = a.PromotionID
,''ID'' = null
,''PromotionDescription'' = ''ฟรีดาวน์''
,ISNULL(D.FreeDownAmount,0) Amount
,SystemAmount =ISNULL(D.FreeDownAmount,0)
from [SAL].[Agreement] A  WITH (NOLOCK)
inner join CRM_FreeDown D  WITH (NOLOCK)  on a.ContractNumber = D.DocumentID
where DocumentType = 2  AND  ISNULL(FreeDownAmount,0) > 0'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (A.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (A.UnitNumber = '''+@UnitNumber+''')'

--แสดงผล
Set @sql = @sql + ' */

-- This @sql is for temp mapping only actual mapping need to remove line 179 and use line 177
Set @sql = '

SELECT	''CompanyNameThai'' = '''' --C.NameTH
        ,''ProductID'' = '''' --P.ProjectNo
        ,''Project'' = '''' --P.ProjectNameTH,
		,''UnitNumber'' = '''' --U.UnitNo
		,''ShortName'' = '''' --ISNULL(TE.Code,''-'')
		,''TypeOfRealEstate'' = '''' --ISNULL(TE.Code,'''')+''-''+ISNULL(TE.Name,'''')
		,''Customer'' = '''' --ISNULL(AO.FirstNameTH,ISNULL(BO.FirstNameTH,''''))+'' ''+ ISNULL(AO.LastNameTH,ISNULL(BO.LastNameTH,''''))
		,''ContractNumber'' = '''' --ISNULL(A.AgreementNo,B.BookingNo)
		,''DocumentType'' = '''' --DocumentType
		,''ID'' = '''' --PM.ItemID
		,''PromotionDescription'' = '''' --PM.PromotionDescription
		,''Amount'' = '''' --PM.Amount
		,''STAmount'' = '''' --PM.SystemAmount AS STAmount
		,''Cancel'' = '''' --ISNULL(A.Cancel,B.Cancel)
		,''Status'' = '''' /* CASE	WHEN ISNULL(A.Cancel,B.Cancel) = ''1'' THEN ''ยกเลิก''
							WHEN ISNULL(A.Cancel,B.Cancel) = ''2'' THEN ''เปลี่ยนห้อง''
							WHEN ISNULL(A.Cancel,B.Cancel) = ''3'' THEN ''ตั้งเรื่องยกเลิก''
							WHEN ISNULL(A.Cancel,B.Cancel) = ''4'' THEN ''เปลี่ยนชื่อ''
							WHEN ISNULL(A.Cancel,ISNULL(B.Cancel,''''))=''''OR ISNULL(A.Cancel,B.Cancel) = 0 THEN ''Active'' END */

FROM	[SAL].[Booking] B  WITH (NOLOCK)'  --This is actual table need to use table below as well
		/* LEFT OUTER JOIN [SAL].[BookingOwner] BO WITH (NOLOCK)  ON BO.BookingID = B.ID AND BO.IsMainOwner = 1 AND ISNULL(BO.IsDeleted,0) = 0 
		LEFT OUTER JOIN [SAL].[Agreement] A  WITH (NOLOCK)  ON A.BookingID = B.ID 
		LEFT OUTER JOIN [SAL].[AgreementOwner] AO WITH (NOLOCK)  ON AO.AgreementID = A.ID AND AO.Header = 1 AND ISNULL(AO.IsDelete,0) = 0 
		LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK)  ON U.ID = B.UnitID AND U.ProjectID = B.ProjectID 
		LEFT OUTER JOIN [PRJ].[Model] M WITH (NOLOCK)  ON U.ModelID = M.ID AND U.ProjectID = M.ProjectID 
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TE WITH (NOLOCK)  ON TE.ID = M.TypeofRealEstateID
		LEFT OUTER JOIN [PRJ].[Project] P WITH (NOLOCK)  ON P.ID = U.ProjectID 
		LEFT OUTER JOIN [MST].[Company] C WITH (NOLOCK)  ON C.ID = P.CompanyID  
		LEFT OUTER JOIN @p PM ON PM.DocumentID = ISNULL(A.ContractNumber,B.BookingNumber) AND PM.PromotionID IN(ISNULL(A.PromotionID,B.PromotionID),ISNULL(A.PresalePromotionID,B.PresalePromotionID))

WHERE 1=1 '

IF(ISNULL(@CompanyID,'')<>'')Set @sql = @Sql+' AND(C.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (P.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'')Set @sql = @sql+' AND (B.UnitNumber = '''+@UnitNumber+''')'

IF((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	Set @sql = @sql+' AND (B.BookingDate BETWEEN '''+Convert(nvarchar(10),@DateStart,120)+''' AND '''+Convert(nvarchar(10),@DateEndInStore,120)+''')'
IF((YEAR(@DateStart) <> 1800) AND  ISNULL(@DateStart,'')<>'')
	Set @sql = @sql+' AND (B.BookingDate  >= '''+Convert(nvarchar(10),@DateStart,120)+''')'
IF((YEAR(@DateEnd) <> 7000) AND  ISNULL(@DateEnd,'')<>'')
	Set @sql = @sql+' AND (B.BookingDate  <= '''+Convert(nvarchar(10),@DateEndInStore,120)+''')'

IF((YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>'')
	Set @sql = @sql+' AND (A.ContractDate BETWEEN '''+Convert(nvarchar(10),@DateStart2,120)+''' AND '''+Convert(nvarchar(10),@DateEndInStore2,120)+''')'
IF((YEAR(@DateStart2) <> 1800) AND  ISNULL(@DateStart2,'')<>'')
	Set @sql = @sql+' AND (A.ContractDate >= '''+Convert(nvarchar(10),@DateStart2,120)+''')'
IF((YEAR(@DateEnd2) <> 7000) AND  ISNULL(@DateEnd2,'')<>'')
	Set @sql = @sql+' AND (A.ContractDate <= '''+Convert(nvarchar(10),@DateEndInStore2,120)+''')'

IF(ISNULL(@P_ID,'')<>'')Set @sql = @sql+ ' AND (PM.ItemID = '''+@P_ID+''')'
IF(ISNULL(@PromotionID,'')<>'')Set @sql = @sql+' AND (PM.PromotionID = '''+@PromotionID+''')'

IF(@StatusAG = '1') set @sql=@sql+' AND ISNULL(A.CancelDate,B.CancelDate) IS NULL'
IF(@StatusAG = '2') set @sql=@sql+' AND ISNULL(A.CancelDate,B.CancelDate) IS NOT NULL'

SET @sql=@sql+' ORDER BY P.ProductID,B.UnitNumber,DocumentType,ISNULL(A.ContractNumber,B.BookingNumber),PM.ItemID ASC;'
*/

EXEC(@sql)
--PRINT(@sql)

GO
