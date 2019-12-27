SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_AG_R3] '','10106','C06','','','Administrator Account'

CREATE PROC [dbo].[AP2SP_RP_AG_R3]
	@CompanyID nvarchar(50)
	,@ProductID nvarchar(50)
	,@UnitNumber nvarchar(50)
	,@DateStart datetime-- วันที่ทำสัญญา
	,@DateEnd   datetime-- วันที่ทำสัญญา
	,@Username nvarchar(150)
AS
----/////// หา Promotion ทั้งหมด ของโปรโมชั่นขาย
---ส่วนลดเงินสดของใหม่  ไม่มีของเก่าเพราะอยู่รวมกับของแถม
DECLARE @sql nvarchar(MAX)
Set @sql = '
DECLARE	@TMPPro TABLE(ProductID varchar(20),UnitNumber varchar(20),ContractNumber varchar(50),ID int,Text1 varchar(250),Amt int,Price1 money,Price2 money,SaleNo int)
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --BK.ProductID  
		,''UnitNumber'' = '''' --BK.UnitNumber
		,''ContractNumber'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber) AS ContractNumber
		,1 AS ID
		,''ส่วนลดเงินสด'' AS Text1
		,0 AS Amt
		,''Price1'' = '''' --ISNULL(ZP.CashDiscount,0) AS Price1
		,''Price2'' = '''' --ISNULL(ISNULL(AG.CashDiscount,BK.CashDiscount),0) AS Price2
		,1 AS SaleNo

FROM	[SAL].[Booking] BK ' --Need to use table below as well
		/* LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ---Promotion ใหม่
		(
			SELECT	PromotionID,CashDiscount
			FROM	ZPROM_Promotion
			WHERE	PromotionType = 1 
		)ZP ON  ZP.PromotionID = ISNULL(AG.PromotionID,BK.PromotionID) 
WHERE	BK.CancelDate IS NULL 
		AND ZP.PromotionID IS NOT NULL 
		AND ISNULL(ISNULL(AG.CashDiscount,BK.CashDiscount),0) > 0 '
		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (BK.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (BK.UnitNumber = '''+@UnitNumber+''')' */
---ส่วนลด ณ วันโอน  ไม่มีของเก่าเพราะอยู่รวมกับของแถม
Set @sql = @sql + '
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --BK.ProductID
		,''UnitNumber'' = '''' --BK.UnitNumber
		,''ContractNumber'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber) AS ContractNumber
		,2 AS ID
		,''ส่วนลด ณ วันโอน'' AS Text1
		,0 AS Amt
		,''Price1'' = '''' --ISNULL(ZP.TransferDiscount,0) AS Price1
		,''Price2'' = '''' --ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0) AS Price2
		,1 AS SaleNo
FROM	[SAL].[Booking] BK' --Need to use table below as well
		/* LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ---Promotion ใหม่
		(
			SELECT	PromotionID,TransferDiscount
			FROM	ZPROM_Promotion
			WHERE	PromotionType = 1 
		)ZP ON  ZP.PromotionID = ISNULL(AG.PromotionID,BK.PromotionID) 
WHERE	BK.CancelDate IS NULL 
		AND ZP.PromotionID IS NOT NULL 
		AND ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0) > 0 '
		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (BK.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (BK.UnitNumber = '''+@UnitNumber+''')' */

---ของแถมขายทั้ง tableใหม่+เก่า
Set @sql = @sql + '
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --BK.ProductID
		,''UnitNumber'' = '''' --BK.UnitNumber
		,''ContractNumber'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber) AS ContractNumber
		,3 AS ID
		,''Text1'' = '''' /* CASE	WHEN ZD.PromotionID IS NULL	THEN CASE	WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 1 THEN PM.PromotionDescription
														WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 2 THEN PM.PromotionDescription
														ELSE CAST(PM.ID AS varchar(20))+'' - ''+PM.PromotionDescription END
				ELSE ZD.ItemID+'' - ''+ZD.DescriptionTH+'' ''+ZD.BrandTH+'' ''+ZD.SpecTH+'' ''+ZD.RemarkTH END AS Text1 */
		,''Amt'' = '''' --ISNULL(ZS.Amount,0) AS Amt
		,0 AS Price1
		,''Price2'' = '''' /* CASE	WHEN ZD.PromotionID IS NULL THEN CASE	WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 1 THEN ISNULL(AG.CashDiscount,BK.CashDiscount)
														WHEN PM.PromotionDescription like ''%ส่วนลด%'' AND PM.Flag = 2 THEN ISNULL(AG.TransferDisCount,BK.TransferDisCount)
														ELSE PM.Amount END
				ELSE ZD.PricePerUnit*ZS.Amount END AS Price2 */
		,1 AS SaleNo

FROM	[SAL].[Booking] BK' --Need to use table below as well
		/* LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ZPROM_SalePromotionDetail ZS ON ZS.DocumentID = ISNULL(AG.ContractNumber,BK.BookingNumber)
		LEFT OUTER JOIN ZPROM_PromotionDetail ZD ON ZD.PromotionID = ZS.PromotionID AND ZD.ItemID = ZS.ItemID
		LEFT OUTER JOIN ICON_EntForms_PromotionDescription PM ON PM.ID IN(SELECT * FROM dbo.fn_SplitString(ISNULL(AG.PromotionDetail,BK.PromotionDetail),'','')) AND PM.PromotionID = ISNULL(AG.PromotionID,BK.PromotionID)
WHERE	BK.CancelDate IS NULL AND ISNULL(ISNULL(AG.PromotionDetail,BK.PromotionDetail),'''') <> '''' '
		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (BK.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (BK.UnitNumber = '''+@UnitNumber+''')' */

---ค่าใช้จ่าย
Set @sql = @sql + '
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --BK.ProductID
		,''UnitNumber'' = '''' --BK.UnitNumber
		,''ContractNumber'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber) AS ContractNumber
		,4 AS ID
		,''Text1'' = '''' --ZS.PromotionFeeName AS Text1
		,0 AS Amt
		,0 AS Price1
		,''Price2'' = '''' --CASE PromotionFeeID WHEN ''15'' THEN Amount / 2 ELSE CASE WHEN Charge=''H'' THEN Amount / 2 ELSE Amount END END AS Price2
		,1 AS SaleNo

FROM	[SAL].[Booking] BK' --Need to use table below as well
		/* LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ZPROM_SalePromotionFee ZS ON  ZS.DocumentID = ISNULL(AG.ContractNumber,BK.BookingNumber) 
WHERE	BK.CancelDate IS NULL 
		AND ((PromotionFeeID = ''15'' AND Charge=''N'')
		OR (PromotionFeeID IN (''00'',''01'',''02'',''17'',''2G'',''37'') AND (Charge=''N'' OR Charge=''H''))) '
--AND ZS.Charge IN (''N'',''H'') '
		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (BK.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (BK.UnitNumber = '''+@UnitNumber+''')' */

---FriendGetFriend
Set @sql = @sql + '
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --BK.ProductID
		,''UnitNumber'' = '''' --BK.UnitNumber
		,''ContractNumber'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber) AS ContractNumber
		,5 AS ID
		,''FriendGetFriend'' AS Text1
		,0 AS Amt
		,''Price1'' = '''' --ISNULL(ZP.FGFDiscount,0) AS Price1
		,''Price2'' = '''' --ISNULL(ISNULL(AG.FGFDiscount,BK.FGFDiscount),0) AS Price2
		,1 AS SaleNo

FROM	ICON_EntForms_Booking BK' --Need to use below table as well
		/* LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ---Promotion ใหม่
		(
			SELECT	PromotionID,FGFDiscount
			FROM	ZPROM_Promotion
			WHERE	PromotionType = 1 
		)ZP ON  ZP.PromotionID = ISNULL(AG.PromotionID,BK.PromotionID) 
WHERE	BK.CancelDate IS NULL 
		AND ISNULL(ISNULL(AG.FGFDiscount,BK.FGFDiscount),0) > 0 '
		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (BK.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (BK.UnitNumber = '''+@UnitNumber+''')' */

----/////// หา Promotion ทั้งหมด ของโปรโมชั่นโอน
---ส่วนลด ณ วันโอนจริง
Set @sql = @sql + '
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --AG.ProductID
		,''UnitNumber'' = '''' --AG.UnitNumber 
		,''ContractNumber'' = '''' --AG.ContractNumber
		,6 AS ID
		,''ส่วนลด ณ วันโอน'' AS Text
		,0 AS Amt
		,''Price1'' = '''' --ISNULL(ZP.TransferDiscount,0) AS Price1
		,''Price2'' = '''' --ISNULL(ZT.TransferDiscount,0) AS Price2
		,2 AS SaleNo

FROM	ICON_EntForms_Agreement AG' --Need to use below table as well
		/* LEFT OUTER JOIN ZPROM_TransferPromotion ZT ON AG.ContractNumber = ZT.ContractNumber
		LEFT OUTER JOIN ---Promotion ใหม่
		(
			SELECT	PromotionID,TransferDiscount
			FROM	ZPROM_Promotion
			WHERE	PromotionType = 2
		)ZP ON  ZP.PromotionID = AG.PromotionID

WHERE	ISNULL(IsCancel,0) = 0 AND AG.CancelDate IS NULL AND ISNULL(IsApproved2,0) = 1 AND ISNULL(ZT.TransferDiscount,0) > 0 '
		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (AG.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')' */

---ของแถมของโอน
Set @sql = @sql + '
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --AG.ProductID
		,''UnitNumber'' = '''' --AG.UnitNumber
		,''ContractNumber'' = '''' --AG.ContractNumber
		,7 AS ID
		,''Text1'' = '''' --ZP.ItemID+'' - ''+ZP.DescriptionTH+'' ''+ZP.BrandTH+'' ''+ZP.SpecTH+'' ''+ZP.RemarkTH AS Text1
		,''Amt'' = '''' --ISNULL(ZD.Amount,0) As Amt
		,0 AS Price1
		,''Price2'' = '''' --ISNULL(ZP.PricePerUnit,0)*ISNULL(ZD.Amount,0) AS Price2
		,2 AS SaleNo

FROM	ICON_EntForms_Agreement AG' --Need to use below table as well
		/* LEFT OUTER JOIN ZPROM_TransferPromotion ZT ON AG.ContractNumber = ZT.ContractNumber
		LEFT OUTER JOIN ZPROM_TransferPromotionDetail ZD ON ZD.TransferPromotionID = ZT.TransferPromotionID  AND ISNULL(ZD.IsSelected,0) = 1
		LEFT OUTER JOIN ZPROM_PromotionDetail ZP ON ZP.PromotionID = ZD.PromotionID AND ZP.ItemID = ZD.ItemId 
WHERE	AG.CancelDate IS NULL AND ISNULL(ZT.IsCancel,0) = 0 AND ISNULL(ZT.IsApproved2,0) = 1 AND ISNULL(ZP.ItemID,'''') <> '''' '
		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (AG.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')' */

---ค่าใช้จ่ายโอน
Set @sql = @sql + '
INSERT INTO @TMPPro
SELECT	''ProductID'' = '''' --AG.ProductID
		,''UnitNumber'' = '''' --AG.UnitNumber
		,''ContractNumber'' = '''' --AG.ContractNumber
		,8 AS ID
		,''Text1'' = '''' --ZF.PromotionFeeName AS Text1
		,0 AS Amt
		,0 AS Price1
		,''Text2'' = '''' --CASE PromotionFeeID WHEN ''15'' THEN ISNULL(ZF.Amount,0) / 2 ELSE CASE WHEN Charge=''H'' THEN ISNULL(ZF.Amount,0) / 2 ELSE ISNULL(ZF.Amount,0) END END AS Price2
		,2 AS SaleNo
FROM	ICON_EntForms_Agreement AG' --Need to use below table as well
		/* LEFT OUTER JOIN ZPROM_TransferPromotion ZT ON AG.ContractNumber = ZT.ContractNumber
		LEFT OUTER JOIN ZPROM_TransferPromotionFee ZF ON ZF.TransferPromotionID = ZT.TransferPromotionID
WHERE	AG.CancelDate IS NULL 
		AND ISNULL(ZT.IsCancel,0) = 0 AND ISNULL(ZT.IsApproved2,0) = 1 
		AND ((PromotionFeeID = ''15'' AND Charge=''N'')
			  OR (PromotionFeeID IN (''00'',''01'',''02'',''17'',''2G'',''37'') AND (Charge=''N'' OR Charge=''H''))) '

		IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (AG.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')' */

----Select * From  @TMPPro Order by ProductID,UnitNumber,SaleNo,ID
----////ออกรายงานจริง
Set @sql = @sql + '
SELECT	''ProjectName'' = '''' --PR.ProductID+'' - ''+PR.Project AS ProjectName
		,''TypeOfRealEstate'' = '''' --MA.TypeOfRealEstate
		,''UnitNumber'' = '''' --UN.UnitNumber
		,''CustName'' = '''' --CASE WHEN AG.ContractNumber IS NULL THEN dbo.fn_GenCustBookingAllNoTitle(BK.BookingNumber) ELSE dbo.fn_GenCustAgreementAllNoTitle(AG.ContractNumber) END AS CustName
		,''ContractNumber'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber) AS ContractNumber
		,''Active''  AS Status
		,''ID'' = '''' --ZS.ID
		,''PromotionID'' = '''' --ISNULL(AG.PromotionID,BK.PromotionID) AS PromotionID
		,''ApproveDate'' = '''' --ISNULL(AG.ApproveDate,BK.ApproveDate) AS ApproveDate
		,''PromotionName'' = '''' --CASE WHEN ISNULL(AG.PromotionID,BK.PromotionID) IS NOT NULL AND ZS.Text1 IS NULL THEN ''ไม่ได้เลือก Promotion'' ELSE ZS.Text1 END  AS PromotionName
		,''Amt'' = '''' --ZS.Amt 
		,''Price1'' = '''' --ZS.Price1
		,''Price2'' = '''' --ZS.Price2
		,''SaleNo'' = '''' --ISNULL(ZS.SaleNo,1) AS SaleNo
		,''ContractDate'' = '''' --ISNULL(ISNULL(AG.pContractDate,AG.ContractDate),BK.BookingDate) AS ContractDate 
		,''CompanyNameThai'' = '''' --CO.CompanyNameThai
FROM	[PRJ].[Unit] UN' --Need to use below table as well
		/* LEFT OUTER JOIN [SAL].[Booking] BK ON BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber
		LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN [PRJ].[Project] PR ON PR.ProductID = UN.ProductID
		LEFT OUTER JOIN [MST].[Company] CO ON CO.CompanyID = PR.CompanyID
		LEFT OUTER JOIN [PRJ].[Model] MA ON MA.ProductID = UN.ProductID AND MA.ModelID = UN.ModelID 
		LEFT OUTER JOIN @TmpPro ZS ON  ZS.ProductID = UN.ProductID AND ZS.UnitNumber = UN.UnitNumber 

WHERE	UN.Assettype IN (2,4) 
		AND BK.CancelDate IS NULL 
		' */

/* IF(ISNULL(@CompanyID,'')<>'')Set @sql = @Sql+' AND(CO.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (PR.ProductID = '''+@ProductID+''')'
IF(ISNULL(@UnitNumber,'')<>'' )Set @sql = @sql+' AND (UN.UnitNumber = '''+@UnitNumber+''')'
IF((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	Set @sql = @sql+' AND (ContractDate BETWEEN '''+Convert(nvarchar(10),@DateStart,120)+''' AND '''+Convert(nvarchar(10),@DateEnd,120)+''')'
IF((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) = 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	Set @sql = @sql+' AND (ContractDate >= '''+Convert(nvarchar(10),@DateStart,120)+''')'
IF((YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	Set @sql = @sql+' AND (ContractDate <= '''+Convert(nvarchar(10),@DateEnd,120)+''')'

Set @sql = @sql+' ORDER BY ProjectName,SaleNo,UnitNumber,ID ' */
EXEC(@sql)
--print(@sql)

GO
