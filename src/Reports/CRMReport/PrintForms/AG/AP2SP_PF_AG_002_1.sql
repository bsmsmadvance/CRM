SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[AP2SP_PF_AG_002_1] '10206BA00058'
CREATE PROCEDURE [dbo].[AP2SP_PF_AG_002_1]
    @BookingNumber  nvarchar(15)
	
AS

DECLARE @p TABLE(
	nOrder int identity,
	BookingNumber nvarchar(30),
	PromotionDescription nvarchar(500),
	ID nvarchar(30),
	Flag int,
	AmountDiscount money,
	UnitName nvarchar(50));

DECLARE @BookingID NVARCHAR(50);
SET @BookingID = (SELECT ID FROM SAL.Booking WHERE BookingNo = @BookingNumber);

DECLARE @PromotionID nvarchar(20);
SET @PromotionID = (SELECT ID FROM PRM.BookingPromotion WHERE BookingID = @BookingID);

IF EXISTS(SELECT * FROM [PRM].[Promotion] WHERE PromotionID = @PromotionID)
	OR (EXISTS(SELECT * FROM ZPROM_SalePromotionFee WHERE DocumentID = @BookingNumber AND DocumentType = 1))
 BEGIN

	INSERT INTO @p
	SELECT BookingNo, 
			'PromotionDescription' = 'ส่วนลดเงินสด', NULL, 1, ISNULL(CashDiscount,0),'บาท'
	FROM [SAL].[Booking]
	WHERE	BookingNo = @BookingNumber	
		AND CashDiscount > 0
		AND BookingDate < '20181001';

	INSERT INTO @p
	SELECT BookingNo, 
			'PromotionDescription' = 'ส่วนลด ณ วันโอน', NULL, 1, ISNULL(TransferDiscount,0),'บาท'
	FROM [SAL].[Booking]
	WHERE	BookingNo = @BookingNumber	
		AND TransferDiscount > 0
		AND (BookingDate < '20181001' OR BookingDate > '20181018');

	INSERT INTO @p
	SELECT SPD.DocumentID, 
			'PromotionDescription' = ISNULL(PD.DescriptionTH,'') + ' ' 
				+ ISNULL(PD.BrandTH,'') + ' ' 
				+ ISNULL(PD.SpecTH,'') + ' ' 
				+ ISNULL(PD.RemarkTH,'') 
				+ [dbo].[fn_GetPromotionDescriptionAmount](PD.DescriptionTH,SPD.Amount,PD.UnitNameTH,PD.UnitNameENG,'TH'),
				
				--CASE WHEN (PD.DescriptionTH LIKE '%รูดบัตรเครดิต%' AND PD.DescriptionTH LIKE '%0%%')
				--			OR (PD.DescriptionTH LIKE '%คงที่ร้อยละ 2.3%' AND PD.DescriptionTH LIKE '%3 ปี%')
				--			OR (PD.DescriptionTH LIKE '%คงที่ร้อยละ 0%' AND PD.DescriptionTH LIKE '%2 ปี%')  
				--			OR (PD.DescriptionTH LIKE '%ดอกเบี้ย 2%%' AND PD.DescriptionTH LIKE '%2 ปี %') 
				--			OR (PD.DescriptionTH LIKE '%0%%' AND PD.DescriptionTH LIKE '%6 เดือน%')   
				--			OR (PD.DescriptionTH LIKE '%โปรโมชั่นอยู่ฟรี%')
				--			OR (PD.DescriptionTH LIKE '%อยู่ฟรี%') 
				--			OR (PD.DescriptionTH LIKE '%ดอกเบี้ยคงที่ 3%' AND PD.DescriptionTH LIKE '%3 ปี%')
				--			OR (PD.DescriptionTH LIKE '%SCB%' AND PD.DescriptionTH LIKE '%ผ่อนต่ำ%')
				--			OR (PD.DescriptionTH LIKE '%SCB%' AND PD.DescriptionTH LIKE '%ดอกเบี้ย%')
				--			OR (PD.DescriptionTH LIKE '%ดอกเบี้ย%' AND PD.DescriptionTH LIKE '%1 ปี%')
				--			OR (PD.DescriptionTH LIKE '%ผ่อนต่ำ%' AND PD.DescriptionTH LIKE '%1 ปี%')
				--			OR (PD.DescriptionTH LIKE '%ผ่อน%' AND PD.DescriptionTH LIKE '%1.5 ปี%')
				--			OR (PD.DescriptionTH LIKE '%2 ปี%' AND PD.DescriptionTH LIKE '%ดอกเบี้ย%')
				--			OR (PD.DescriptionTH LIKE '%ผ่อนถูก%' AND PD.DescriptionTH LIKE '%3,000%')
				--			OR (PD.DescriptionTH LIKE '%ผ่อนเบา%' AND PD.DescriptionTH LIKE '%SCB%')
				--			OR (PD.DescriptionTH LIKE '%3 ปี%' AND PD.DescriptionTH LIKE '%SCB%')
				--	THEN '' 
				--	ELSE ' จำนวน ' + CAST(SPD.Amount AS varchar(10)) + ' ' + ISNULL(PD.UnitNameTH,'') END,
			PD.ItemID, 3, 0, NULL
	FROM [PRM].[BookingPromotion] SPD LEFT OUTER JOIN
		[PRM].[BookingPromotionItem] PD ON SPD.ID = PD.BookingPromotionID AND SPD.ItemID = PD.ItemID
	WHERE	DocumentID = @BookingNumber
		AND SPD.DocumentType = 1
		AND PD.IsDisplayPromotion = 1			
		AND (PD.DescriptionTH IS NOT NULL OR PD.DescriptionTH = '')
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 2.5%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%อัตราดอกเบี้ยพิเศษ%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 3%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'));

	INSERT INTO @p
	SELECT DocumentID, 
			'PromotionDescription' = CASE PromotionFeeID 
				WHEN '00' THEN 'ฟรีค่าส่วนกลาง ' + CASE WHEN Charge = 'N' THEN CAST(Y AS nvarchar(20)) ELSE CAST(Y / 2 AS nvarchar(20)) END + ' เดือน'
				WHEN '000' THEN 'AP ช่วยจ่ายค่าส่วนกลาง ปีแรก ' + CONVERT(varchar(100),P.PublicFundRate_AP,1) +' บาท / ตร.ม.'
				WHEN '01' THEN 'ฟรีค่ามิเตอร์ไฟฟ้า'
				WHEN '02' THEN 'ฟรีค่ามิเตอร์น้ำ'
				WHEN '15' THEN 'ฟรีค่าธรรมเนียมการโอน'
				WHEN '17' THEN  
					CASE WHEN Charge='H' THEN 'ฟรีค่าจดจำนอง ครึ่งนึง (ค่าจดจำนองร้อยละ 1 ของราคาที่ระบุในสัญญาจะซื้อจะขาย)'
						ELSE 'ฟรีค่าจดจำนอง (จากราคาหน้าสัญญาจะซื้อจะขาย)' END					
				--WHEN '17' THEN 'ฟรีค่าจดจำนอง (จากราคาหน้าสัญญาจะซื้อจะขาย)'
				--WHEN '17' THEN 'ฟรีค่าจดจำนอง เท่ากับ 1% ของราคาหน้าสัญญาจะซื้อจะขาย'
				WHEN '2G' THEN 'ฟรีค่ากองทุน'
				WHEN '37' THEN 'ฟรีค่าอากรแสตมป์ และค่าพยาน' END, PromotionFeeID, NULL, 0, NULL
	FROM [PRM].[BookingPromotionExpense] S
		LEFT OUTER JOIN [SAL].[Booking] B ON S.DocumentID=B.BookingNumber AND S.DocumentType=1
		LEFT OUTER JOIN [PRJ].[Project] P ON B.ProductID=P.ProductID	
	WHERE	DocumentID = @BookingNumber	
		AND S.DocumentType = 1 
		AND ((PromotionFeeID = '15' AND Charge='N')
		 OR (PromotionFeeID IN ('00','01','02','17','2G','37','000') AND (Charge='N' OR Charge='H')));

	--Display Result
	SELECT * FROM @p;
 END
ELSE
 BEGIN

	SELECT  BK.BookingNumber,PM.PromotionDescription
			,PM.ID,PM.Flag
			,'AmountDiscount' = CASE WHEN PM.PromotionDescription like '%ส่วนลด ณ.วันโอนกรรมสิทธิ์%'	AND  PM.Flag = 2  THEN ISNULL(BK.Transferdiscount,0) 
									 WHEN PM.PromotionDescription like '%ส่วนลด ณ วันโอนกรรมสิทธิ์%'	AND  PM.Flag = 2  THEN ISNULL(BK.Transferdiscount,0) 
									 WHEN PM.PromotionDescription like '%ส่วนลด ณ.วันโอน%'	AND  PM.Flag = 2 THEN ISNULL(BK.Transferdiscount,0) 
									 WHEN PM.PromotionDescription like '%ส่วนลด ณ วันโอน%'	AND  PM.Flag = 2 THEN ISNULL(BK.Transferdiscount,0) 
									 WHEN PM.PromotionDescription like '%กรุงศรีโบนัส%' THEN 0
									 ELSE 0 END    
	FROM	[SAL].[Booking] BK LEFT OUTER JOIN 
			[ICON_EntForms_PromotionDescription]PM ON PM.ID IN(SELECT * FROM dbo.fn_SplitString(BK.PromotionDetail,','))AND PM.PromotionID = BK.PromotionID AND (PM.PromotionDescription NOT LIKE '%ส่วนลดเงินสด%')		
	WHERE	BK.BookingNumber = @BookingNumber;

 END

GO
