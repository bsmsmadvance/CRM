SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_PF_AG_002_1Eng] '60022BA00028'
--[AP2SP_PF_AG_002_1Eng] '60005BA00323'
CREATE PROCEDURE [dbo].[AP2SP_PF_AG_002_1Eng]
    @BookingNumber  nvarchar(15)	
AS

DECLARE @PromotionID nvarchar(20);
DECLARE @IsNationalityThai BIT;

SELECT @IsNationalityThai=[dbo].[fn_IsNationalityThaiBookingOwner] (@BookingNumber);

DECLARE @BookingID NVARCHAR(50);
SET @BookingID = (SELECT ID FROM SAL.Booking WHERE BookingNo = @BookingNumber);

SET @PromotionID = (SELECT ID FROM PRM.BookingPromotion WHERE BookingID = @BookingID);

DECLARE @p TABLE(
	nOrder int identity,
	BookingNumber nvarchar(30),
	PromotionDescription nvarchar(500),
	ID nvarchar(30),
	Flag int,
	AmountDiscount money,
	UnitName nvarchar(50));


IF EXISTS(SELECT * FROM ZPROM_Promotion WHERE PromotionID = @PromotionID)
 BEGIN

	INSERT INTO @p
	SELECT BookingNo, 
			'PromotionDescription' = 'Cash Discount', NULL, 1, ISNULL(CashDiscount,0),'Baht'
	FROM [SAL].[Booking]
	WHERE	BookingNo = @BookingNumber	
		AND CashDiscount > 0
		AND BookingDate < '20181001';

	INSERT INTO @p
	SELECT BookingNo, 
			'PromotionDescription' = 'Discount on transfer date', NULL, 1, ISNULL(TransferDiscount,0),'Baht'
	FROM [SAL].[Booking]
	WHERE	BookingNo = @BookingNumber	
		AND TransferDiscount > 0
		AND (BookingDate < '20181001'  OR BookingDate > '20181018');

	
	INSERT INTO @p
	SELECT SPD.DocumentID, 
			'PromotionDescription' = ISNULL(PD.DescriptionENG,'') + 
					CASE WHEN PD.BrandENG IS NULL THEN '' ELSE ' ' END + ISNULL(PD.BrandENG,'') + 
					CASE WHEN PD.SpecENG IS NULL THEN '' ELSE ' ' END + ISNULL(PD.SpecENG,'') + 
					CASE WHEN PD.RemarkENG IS NULL THEN '' ELSE ' ' END + ISNULL(PD.RemarkENG,'') + 
						[dbo].[fn_GetPromotionDescriptionAmount](PD.DescriptionTH,SPD.Amount,PD.UnitNameTH,PD.UnitNameENG,'EN'),
			SPD.ItemID, 3, NULL, NULL
	FROM ZPROM_SalePromotionDetail SPD LEFT OUTER JOIN
		ZPROM_PromotionDetail PD ON SPD.PromotionID = PD.PromotionID AND SPD.ItemID = PD.ItemID
	WHERE DocumentID = @BookingNumber
		AND SPD.DocumentType = 1
		AND PD.IsDisplayPromotion = 1	
		AND (PD.DescriptionENG IS NOT NULL OR PD.DescriptionENG = '')
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 2.5%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%อัตราดอกเบี้ยพิเศษ%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 3%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'));


	INSERT INTO @p
	SELECT DocumentID, 
			'PromotionDescription' = CASE PromotionFeeID 
				WHEN '00' THEN 'Free Common Facility Fee ' + CASE WHEN Charge = 'N' THEN CAST(CAST(Y AS Int) AS nvarchar(20)) ELSE CAST(CAST(Y AS Int) / 2 AS nvarchar(20)) END + ' Months'
				WHEN '000' THEN 'AP help to expense Common Facility Fee in first year ' + CONVERT(varchar(100),P.PublicFundRate_AP,1) +' Baht / sqr.m.'
				WHEN '01' THEN 'Free Electricity Meter Fee'
				WHEN '02' THEN 'Free Water Supply Meter Fee'
				WHEN '15' THEN 'Free Ownership Transfer Fee'				
				--WHEN '17' THEN 'Free Mortgage Registration Fee'
				WHEN '17' THEN  
					CASE WHEN Charge='H' THEN 'Free 50% of mortgage registration fees (mortgage registration fees is 1% of the price stated in the Sale and Purchase Agreement )'
						ELSE 'Free Mortgage Registration Fee' END
				WHEN '2G' THEN 'Free Sinking Fund Fee'
				WHEN '37' THEN 'Free Duty Stamp and Witness Fee' END, NULL, NULL, NULL, NULL 
	FROM [PRM].[BookingPromotionExpense] S
		LEFT OUTER JOIN [SAL].[Booking] B ON S.DocumentID=B.BookingNumber AND S.DocumentType=1
		LEFT OUTER JOIN [PRJ].[Project] P ON B.ProductID=P.ProductID	
	WHERE DocumentID = @BookingNumber	
		AND S.DocumentType = 1 
		AND ((PromotionFeeID = '15' AND Charge='N')
		 OR (PromotionFeeID IN ('00','01','02','17','2G','37','000') AND (Charge='N' OR Charge='H'))); 


	IF @IsNationalityThai=1 AND EXISTS(SELECT *,ISNULL(@IsNationalityThai,0) AS IsNationalityThai FROM @p)
	BEGIN
		DELETE FROM @p
		INSERT INTO @p SELECT @BookingNumber, '-', NULL, NULL, NULL,NULL;
	END

	--Display Result
	SELECT *,ISNULL(@IsNationalityThai,0) AS IsNationalityThai FROM @p;

 END
ELSE
 BEGIN

	SELECT  BK.BookingNo,
			'PromotionDescription' = PM.Detail
			,PM.ID,PM.Flag
			,'AmountDiscount' = CASE WHEN PM.PromotionDescription like '%ส่วนลด ณ.วันโอนกรรมสิทธิ์%'	AND PM.Flag = 2  THEN ISNULL(BK.Transferdiscount,0) 
									 WHEN PM.PromotionDescription like '%ส่วนลด ณ วันโอนกรรมสิทธิ์%'	AND PM.Flag = 2  THEN ISNULL(BK.Transferdiscount,0) 
									 WHEN PM.PromotionDescription like '%ส่วนลด ณ.วันโอน%'	AND PM.Flag = 2  THEN ISNULL(BK.Transferdiscount,0) 
									 WHEN PM.PromotionDescription like '%ส่วนลด ณ วันโอน%'	AND PM.Flag = 2  THEN ISNULL(BK.Transferdiscount,0) 
									 ELSE 0 END      
			,ISNULL(@IsNationalityThai,0) AS IsNationalityThai

	FROM	[SAL].[Booking] BK LEFT OUTER JOIN 
			[ICON_EntForms_PromotionDescription]PM ON PM.ID IN(SELECT * FROM dbo.fn_SplitString(BK.PromotionDetail,','))AND PM.PromotionID = BK.PromotionID AND PM.PromotionDescription NOT LIKE '%ส่วนลดเงินสด%'		
			
	WHERE BK.BookingNo = @BookingNumber 
END
     
GO
