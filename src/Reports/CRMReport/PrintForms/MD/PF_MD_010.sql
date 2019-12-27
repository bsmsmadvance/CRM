SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--  [PF_MD_010] '10206AA00057'

ALTER PROCEDURE [dbo].[PF_MD_010]
	@AgreementNo nvarchar(30)
AS

DECLARE @BookingID nvarchar(100);
SET @BookingID = (SELECT BookingID FROM SAL.Agreement WHERE AgreementNo = @AgreementNo);

DECLARE @PromotionID nvarchar(30),@BookingDate DATETIME; --, @ContractVersion int;

SELECT @PromotionID = BP.BookingPromotionNo ,@BookingDate=B.BookingDate --,@ContractVersion= [dbo].[fn_GetContractVersion](A.ContractNumber)
FROM [SAL].[Agreement] A WITH(NOLOCK)
	LEFT OUTER JOIN [SAL].[Booking] B ON B.ID = A.BookingID
    LEFT OUTER JOIN [PRM].[BookingPromotion] BP ON BP.BookingID = B.ID
WHERE A.AgreementNo = @AgreementNo;

DECLARE @p TABLE(
	nOrder int identity,
	ContractNumber nvarchar(30),
	PromotionDescription nvarchar(500),
	ID nvarchar(30),
	Flag int,
	AmountDiscount money);

	INSERT INTO @p
	SELECT 'ContractNumber' = BookingID, 
			'PromotionDescription' = 'ส่วนลด ณ วันโอน', 
			NULL AS ID, 
			1 AS Flag, 
			ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (UnitID, N'ส่วนลด ณ วันโอน')),0) AS AmountDiscount
	FROM [SAL].[Agreement]
	WHERE AgreementNo = @AgreementNo	
		AND (SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (UnitID, N'ส่วนลด ณ วันโอน')) > 0
		--AND @ContractVersion = 1

	/* INSERT INTO @p
	SELECT 'ContractNumber' = AgreementNo, 
			'PromotionDescription' = 'ส่วนลดพิเศษ(AP จ่ายงวดสุดท้าย)', NULL, 1, ISNULL(SpacialDiscount,0)
	FROM [SAL].[Agreement]
	WHERE AgreementNo = @ContractNumber	
		AND SpacialDiscount > 0
		AND @ContractVersion = 1
    
	INSERT INTO @p
	SELECT SPD.DocumentID, 
			'PromotionDescription' = ISNULL(PD.DescriptionTH,'') + 
					CASE WHEN PD.BrandTH IS NULL THEN '' ELSE ' ' END + ISNULL(PD.BrandTH,'') + 
					CASE WHEN PD.SpecTH IS NULL THEN '' ELSE ' ' END + ISNULL(PD.SpecTH,'') + 
					CASE WHEN PD.RemarkTH IS NULL THEN '' ELSE ' ' END + ISNULL(PD.RemarkTH,'') + 
					[dbo].[fn_GetPromotionDescriptionAmount](PD.DescriptionTH,SPD.Amount,PD.UnitNameTH,PD.UnitNameENG,'TH'),
			SPD.ItemID, 3, NULL
	FROM [PRM].[BookingPromotion] BP WITH(NOLOCK) 
        LEFT OUTER JOIN [PRM].[BookingPromotionItem] BPI WITH(NOLOCK) ON BP.ID = BPI.BookingPromotionID --AND SPD.ItemID = PD.ItemID
        LEFT OUTER JOIN [PRM].[BookingPromotionFreeItem] BPFI WITH (NOLOCK) ON BP.ID = BPFI.BookingPromotionID
    WHERE BP.BookingID = @BookingID
		AND (SELECT [Key] FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'BookingPromotionStage' AND ID = BP.BookingPromotionStageMasterCenterID) = 2  --SPD.DocumentType = 2
		AND PD.IsDisplayPromotion = 1
		AND (PD.DescriptionTH IS NOT NULL OR PD.DescriptionTH = '')
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 2.5%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%อัตราดอกเบี้ยพิเศษ%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 3%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'));

	INSERT INTO @p
	SELECT DocumentID, 
			'PromotionDescription' = 
            CASE PromotionFeeID 
				WHEN '00' THEN 'ฟรีค่าส่วนกลาง ' + CASE WHEN Charge = 'N' THEN CAST(CAST(Y AS Int) AS nvarchar(20))
											ELSE CAST(CAST(Y AS Int) / 2 AS nvarchar(20)) END + ' เดือน'
				WHEN '000' THEN 'AP ช่วยจ่ายค่าส่วนกลาง ปีแรก ' + CONVERT(varchar(100),P.PublicFundRate_AP,1) +' บาท / ตร.ม.'
				WHEN '01' THEN 'ฟรีค่ามิเตอร์ไฟฟ้า'
				WHEN '02' THEN 'ฟรีค่ามิเตอร์น้ำ'
				WHEN '15' THEN 'ฟรีค่าธรรมเนียมการโอน'
				WHEN '17' THEN  
					CASE WHEN Charge='H' THEN 'ฟรีค่าจดจำนอง ครึ่งนึง (ค่าจดจำนองร้อยละ 1 ของราคาที่ระบุในสัญญาจะซื้อจะขาย)'
						ELSE 'ฟรีค่าจดจำนอง (จากราคาหน้าสัญญาจะซื้อจะขาย)' END	
				WHEN '2G' THEN 'ฟรีค่ากองทุน'
				WHEN '37' THEN 'ฟรีค่าอากรแสตมป์ และค่าพยาน' END, NULL, NULL, NULL
	FROM ZPROM_SalePromotionFee S WITH(NOLOCK)
		LEFT OUTER JOIN SAL.Agreement A WITH(NOLOCK) ON S.DocumentID=A.ContractNumber AND S.DocumentType=2
		LEFT OUTER JOIN PRJ.Project P WITH(NOLOCK) ON A.ProductID=P.ProductID	
	WHERE DocumentID = @ContractNumber	
		AND S.DocumentType = 2 
		AND ((PromotionFeeID = '15' AND Charge='N')
		 OR (PromotionFeeID IN ('00','01','02','17','2G','37','000') AND (Charge='N' OR Charge='H'))); 
    */

    -- For BookingPromotionItem
    INSERT INTO @p
    SELECT BP.BookingID
            , 'PromotionDescription' = MBPI.NameTH + ' ' + BPI.Quantity
            , NULL
            , NULL
            , NULL
    FROM [PRM].[BookingPromotion] BP WITH (NOLOCK)
    LEFT OUTER JOIN [PRM].[BookingPromotionItem] BPI WITH (NOLOCK) ON BPI.BookingPromotionID = BP.ID
    LEFT OUTER JOIN [PRM].[MasterBookingPromotionItem] MBPI WITH (NOLOCK) ON MBPI.ID = BPI.MasterBookingPromotionItemID
    WHERE BP.BookingID = @BookingID
        AND (SELECT [Key] FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'BookingPromotionStage' AND ID = BP.BookingPromotionStageMasterCenterID) = 2 --สัญญา

    -- For BookingPromotionFreeItem
    INSERT INTO @p
    SELECT BP.BookingID
            , 'PromotionDescription' = MBPFI.NameTH + ' ' + BPFI.Quantity
            , NULL
            , NULL
            , NULL
    FROM [PRM].[BookingPromotion] BP WITH (NOLOCK)
    LEFT OUTER JOIN [PRM].[BookingPromotionFreeItem] BPFI WITH (NOLOCK) ON BPFI.BookingPromotionID = BP.ID
    LEFT OUTER JOIN [PRM].[MasterBookingPromotionFreeItem] MBPFI WITH (NOLOCK) ON MBPFI.ID = BPFI.MasterBookingPromotionFreeItemID
    WHERE BP.BookingID = @BookingID
        AND (SELECT [Key] FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'BookingPromotionStage' AND ID = BP.BookingPromotionStageMasterCenterID) = 2 --สัญญา

    -- For BookingPromotionExpense
    INSERT INTO @p
    SELECT BP.BookingID
            , 'PromotionDescription' = MPI.Detail
            , NULL
            , NULL
            , BPE.SellerAmount
    FROM [PRM].[BookingPromotion] BP WITH (NOLOCK)
    LEFT OUTER JOIN [PRM].[BookingPromotionExpense] BPE WITH (NOLOCK) ON BPE.BookingPromotionID = BP.ID
    LEFT OUTER JOIN MST.MasterPriceItem MPI on MPI.ID = BPE.MasterPriceItemID
    WHERE BP.BookingID = @BookingID
        AND (SELECT [Key] FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'ExpenseReponsibleBy' AND ID = BPE.ExpenseReponsibleByMasterCenterID) = 2 --สัญญา
        AND (SELECT [Key] FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'BookingPromotionStage' AND ID = BP.BookingPromotionStageMasterCenterID) = 0 --ผู้ขาย


SELECT	'CompanyID' = CP.Code
        ,'AuthoritarianByCompany' = ISNULL((SELECT AttorneyNameTH1 FROM PRJ.AgreementConfig WHERE ProjectID = P.ID), '-') --P.AttornyName1
		,'CompanyName' = [dbo].[fn_GetCompanyNameTH] (P.CompanyID,A.ContractDate)
		,'AddressThai' = ISNULL(CP.AddressTH, '-')
        ,'BuildingThai' = ISNULL(CP.BuildingTH, '-')
		,'SoiThai' = ISNULL(CP.SoiTH,'-')
		,'RoadThai' = ISNULL(CP.RoadTH, '-')
        ,'SubDistrictThai' = ISNULL((SELECT NameTH FROM MST.SubDistrict WHERE ID = CP.SubDistrictID), '-')
        ,'DistrictThai' = ISNULL((SELECT NameTH FROM MST.District WHERE ID = CP.DistrictID), '-')
        ,'ProvinceThai' = ISNULL((SELECT NameTH FROM MST.Province WHERE ID = CP.ProvinceID), '-')
		,'CustomerName' =  [dbo].[fn_GenCustAgreementAll_AG](@AgreementNo) 
		,'Age' = datediff(year,AO.BirthDate,CURRENT_TIMESTAMP)
		,'CusCurrentAddress' = ISNULL(AOA.HouseNoTH, '-')
		,'CusMoo' = ISNULL(AOA.MooTH,'-')
		,'CusVillage' = ISNULL(AOA.VillageTH,'-')
		,'CusSoi' = ISNULL(AOA.SoiTH,'-')
		,'CusRoad' = ISNULL(AOA.RoadTH,'-')
		,'CusSubDistrict' = ISNULL((SELECT NameTH FROM MST.SubDistrict WHERE ID = AOA.SubDistrictID), '-')
		,'CusDistrict' = ISNULL((SELECT NameTH FROM MST.District WHERE ID = AOA.DistrictID), '-')
		,'CusProvince' = ISNULL((SELECT NameTH FROM MST.Province WHERE ID = AOA.ProvinceID), '-')
		,'ContractNumber' = A.AgreementNo --สัญญาหลัก
		,'ContractDate' = CASE WHEN A.ContractDate IS NULL THEN '' ELSE [dbo].[fnFormatDateLongTH] (A.ContractDate) END
		,'ProductID' = P.ProjectNo
		,'ProjectName' = P.ProjectNameTH --P.Project,
		,'UnitNumber' = U.UnitNo
		,'Condition' = CASE WHEN BP.TransferDateBefore IS NULL OR BP.TransferDateBefore = '' THEN '-' 
							WHEN P.ProjectNo = '60010' THEN '-' 
							ELSE 'โอนกรรมสิทธิ์ภายในวันที่ ' +  dbo.FormatDateTime('TH', 'dd MMMM yyyy', BP.TransferDateBefore) END
		,'PromotionTransferDate' = BP.TransferDateBefore
		,'PromotionDescription' = D.PromotionDescription
		,'Amount' = D.AmountDiscount
        ,'ProductType' = CASE	WHEN (SELECT Name FROM MST.MasterCenter WHERE ID = P.ProductTypeMasterCenterID)  = 'แนวสูง' THEN 'ห้องชุด'
								WHEN (SELECT Name FROM MST.MasterCenter WHERE ID = P.ProductTypeMasterCenterID) = 'แนวราบ' THEN 'ที่ดินพร้อมสิ่งปลูกสร้าง' END
		,'witness1' = ISNULL((SELECT WitnessTH1 FROM PRJ.AgreementConfig WHERE ProjectID = P.ID), '-')
        ,'witness2' = ISNULL((SELECT WitnessTH2 FROM PRJ.AgreementConfig WHERE ProjectID = P.ID), '-')
		,'Flag' = D.Flag
		,'PromotionID' = '' --PromotionID
		,LEN( [dbo].[fn_GenCustAgreementAll_AG](@AgreementNo))
FROM	[SAL].[Agreement] A  WITH(NOLOCK) 
        LEFT OUTER JOIN [SAL].[AgreementOwner] AO WITH(NOLOCK) ON AO.AgreementID = A.ID AND AO.IsMainOwner = 1 AND ISNULL(AO.IsDeleted,0) = 0
        LEFT OUTER JOIN [SAL].[AgreementOwnerAddress] AOA WITH (NOLOCK) ON AOA.AgreementOwnerID = AO.ID AND AOA.ContactAddressTypeMasterCenterID = (SELECT ID FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'ContactAddressType' AND [Key] = 0)
		LEFT OUTER JOIN [PRJ].[Unit] U WITH(NOLOCK) ON U.ID = A.UnitID AND U.ProjectID = A.ProjectID 
        LEFT OUTER JOIN [PRJ].[Project] P WITH(NOLOCK) ON P.ID = U.ProjectID 
        LEFT OUTER JOIN [MST].[Company] CP WITH(NOLOCK) ON CP.ID = P.CompanyID
        LEFT OUTER JOIN [PRM].[BookingPromotion] BP WITH(NOLOCK) ON BP.BookingID = A.BookingID
        LEFT OUTER JOIN @p D ON A.BookingID = D.ContractNumber 

WHERE	A.AgreementNo = @AgreementNo;


GO
