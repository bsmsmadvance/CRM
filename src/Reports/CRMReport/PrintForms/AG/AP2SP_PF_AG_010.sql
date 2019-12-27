SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--  [AP2SP_PF_AG_010] '60004AA01152'

CREATE PROCEDURE [dbo].[AP2SP_PF_AG_010]
	@ContractNumber nvarchar(20)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
/* DECLARE @PromotionID nvarchar(20);

SELECT @PromotionID = PromotionID 
FROM [ICON_EntForms_Agreement]
WHERE ContractNumber = @ContractNumber;

DECLARE @p TABLE(
	nOrder int identity,
	ContractNumber nvarchar(30),
	PromotionDescription nvarchar(500),
	ID nvarchar(30),
	Flag int,
	AmountDiscount money);

IF EXISTS(SELECT * FROM ZPROM_Promotion WITH (NOLOCK) WHERE PromotionID = @PromotionID) 
	OR (EXISTS(SELECT * FROM ZPROM_SalePromotionFee WITH (NOLOCK)WHERE DocumentID = @ContractNumber AND DocumentType = 2)) --@ProjectID <> '10082' AND 
 BEGIN

	INSERT INTO @p
	SELECT ContractNumber, 
			'PromotionDescription' = 'ส่วนลด ณ วันโอน', NULL, 1, ISNULL(TransferDiscount,0)
	FROM [ICON_EntForms_Agreement] WITH (NOLOCK)
	WHERE ContractNumber = @ContractNumber	
		AND TransferDiscount > 0;


	INSERT INTO @p
	SELECT ContractNumber, 
			'PromotionDescription' = 'ส่วนลดพิเศษ(AP จ่ายงวดสุดท้าย)', NULL, 1, ISNULL(SpacialDiscount,0)
	FROM [ICON_EntForms_Agreement] WITH (NOLOCK)
	WHERE ContractNumber = @ContractNumber	
		AND SpacialDiscount > 0;

	INSERT INTO @p
	SELECT SPD.DocumentID, 
			'PromotionDescription' = ISNULL(PD.DescriptionTH,'') + 
					CASE WHEN PD.BrandTH IS NULL THEN '' ELSE ' ' END + ISNULL(PD.BrandTH,'') + 
					CASE WHEN PD.SpecTH IS NULL THEN '' ELSE ' ' END + ISNULL(PD.SpecTH,'') + 
					CASE WHEN PD.RemarkTH IS NULL THEN '' ELSE ' ' END + ISNULL(PD.RemarkTH,'') + 
					[dbo].[fn_GetPromotionDescriptionAmount](PD.DescriptionTH,SPD.Amount,PD.UnitNameTH,PD.UnitNameENG,'TH'),
			SPD.ItemID, 3, NULL
	FROM ZPROM_SalePromotionDetail SPD WITH (NOLOCK) LEFT OUTER JOIN
		ZPROM_PromotionDetail PD WITH (NOLOCK) ON SPD.PromotionID = PD.PromotionID AND SPD.ItemID = PD.ItemID
	WHERE DocumentID = @ContractNumber
		AND SPD.DocumentType = 2
		AND PD.IsDisplayPromotion = 1
		AND (PD.DescriptionTH IS NOT NULL OR PD.DescriptionTH = '')
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 2.5%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%อัตราดอกเบี้ยพิเศษ%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
		AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 3%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'));

	INSERT INTO @p
	SELECT DocumentID, 
			'PromotionDescription' = CASE PromotionFeeID 
				WHEN '00' THEN 'ฟรีค่าส่วนกลาง ' + CASE WHEN Charge = 'N' THEN CAST(CAST(Y AS Int) AS nvarchar(20))
											ELSE CAST(CAST(Y AS Int) / 2 AS nvarchar(20)) END + ' เดือน'
				WHEN '000' THEN 'AP ช่วยจ่ายค่าส่วนกลาง ปีแรก ' + CONVERT(varchar(100),P.PublicFundRate_AP,1) +' บาท / ตร.ม.'
				WHEN '01' THEN 'ฟรีค่ามิเตอร์ไฟฟ้า'
				WHEN '02' THEN 'ฟรีค่ามิเตอร์น้ำ'
				WHEN '15' THEN 'ฟรีค่าธรรมเนียมการโอน'
				WHEN '17' THEN  
					CASE WHEN Charge='H' THEN 'ฟรีค่าจดจำนอง (จากวงเงินกู้)'
						ELSE 'ฟรีค่าจดจำนอง (จากราคาหน้าสัญญาจะซื้อจะขาย)' END				
				WHEN '2G' THEN 'ฟรีค่ากองทุน'
				WHEN '37' THEN 'ฟรีค่าอากรแสตมป์ และค่าพยาน' END, NULL, NULL, NULL
	FROM ZPROM_SalePromotionFee S WITH (NOLOCK)
		LEFT OUTER JOIN ICON_EntForms_Agreement A WITH (NOLOCK) ON S.DocumentID=A.ContractNumber AND S.DocumentType=2
		LEFT OUTER JOIN ICON_EntForms_Products P WITH (NOLOCK) ON A.ProductID=P.ProductID	
	WHERE DocumentID = @ContractNumber	
		AND S.DocumentType = 2 
		AND ((PromotionFeeID = '15' AND Charge='N')
		 OR (PromotionFeeID IN ('00','01','02','17','2G','37','000') AND (Charge='N' OR Charge='H'))); 

 END
ELSE
 BEGIN

	INSERT INTO @p
	SELECT  A.ContractNumber,PM.PromotionDescription
			,PM.ID,PM.Flag
			,'AmountDiscount' = CASE WHEN PM.PromotionDescription like '%ส่วนลด ณ.วันโอนกรรมสิทธิ์%'	AND PM.Flag = 2 THEN A.TransferDisCount 
						WHEN PM.PromotionDescription like '%ส่วนลด ณ วันโอนกรรมสิทธิ์%'	AND PM.Flag = 2   THEN A.TransferDisCount
						WHEN PM.PromotionDescription like '%ส่วนลดณ.วันโอน%'	 AND PM.Flag = 2   THEN A.TransferDisCount
						WHEN PM.PromotionDescription like '%ส่วนลดณ วันโอน%'	 AND PM.Flag = 2   THEN A.TransferDisCount
						WHEN PM.PromotionDescription like '%ส่วนลด ณ วันโอน%'	 AND PM.Flag = 2   THEN A.TransferDisCount
						ELSE 0 END	   
	FROM	[ICON_EntForms_Agreement] A WITH (NOLOCK)  LEFT OUTER JOIN
			[ICON_EntForms_PromotionDescription] PM WITH (NOLOCK) ON PM.ID IN(SELECT * FROM dbo.fn_SplitString(A.PromotionDetail,',')) AND PM.PromotionID = A.PromotionID AND PM.PromotionDescription NOT LIKE '%ส่วนลดเงินสด%' AND PM.PromotionDescription NOT LIKE '%ส่วนลดหน้าสัญญา%'			
	WHERE	A.ContractNumber = @ContractNumber

 END


SELECT	P.CompanyID,'AuthoritarianByCompany' = P.AttornyName1,
		'CompanyName' = [dbo].[fn_GetCompanyNameTH] (P.CompanyID,A.ContractDate),
		CP.AddressThai,CP.BuildingThai,
		'SoiThai' = ISNULL(CP.SoiThai,'-'),
		CP.RoadThai,CP.SubDistrictThai,CP.DistrictThai,CP.ProvinceThai,
		'CustomerName' =  [dbo].[fn_GenCustAgreementAll_AG](@ContractNumber),
		'Age' = datediff(year,AO.BirthDate,A.ContractDate),
		'CusCurrentAddress' = AO.CurrentAddress,
		'CusMoo' = ISNULL(AO.Moo,'-'),
		'CusVillage' = ISNULL(AO.Village,'-'),
		'CusSoi' = ISNULL(AO.Soi,'-'),
		'CusRoad' = ISNULL(AO.Road,'-'),
		'CusSubDistrict' = ISNULL(AO.SubDistrict,'-'),
		'CusDistrict' = ISNULL(AO.District,'-'),
		'CusProvince' = ISNULL(AO.Province,'-'),
		A.ContractNumber, --สัญญาหลัก
		A.ContractDate,
		P.ProductID,
		'ProjectName' = P.Project,
		U.UnitNumber,
		'Condition' = CASE WHEN A.PromotionTransferDate IS NULL OR A.PromotionTransferDate = '' THEN '-' 
							WHEN A.ProductID = '60010' THEN '-' 
							ELSE 'โอนกรรมสิทธิ์ภายในวันที่ ' +  dbo.FormatDateTime('TH', 'dd MMMM yyyy', A.PromotionTransferDate) END,
		A.PromotionTransferDate,
		'PromotionDescription' = D.PromotionDescription,
		'Amount' = D.AmountDiscount,
		'ProductType' = CASE	WHEN P.ProductType = 'โครงการแนวสูง' THEN 'ห้องชุด'
								WHEN P.ProductType = 'โครงการแนวราบ' THEN 'ที่ดินพร้อมสิ่งปลูกสร้าง' END,
		P.witness1,P.witness2,
		D.Flag,
		PromotionID,
		LEN( [dbo].[fn_GenCustAgreementAll_AG](@ContractNumber))
FROM	[ICON_EntForms_Agreement] A  WITH (NOLOCK) LEFT OUTER JOIN
		[ICON_EntForms_AgreementOwner] AO WITH (NOLOCK) ON AO.ContractNumber = A.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete,0) = 0 LEFT OUTER JOIN
		[ICON_EntForms_Unit] U WITH (NOLOCK) ON U.UnitNumber = A.UnitNumber AND U.ProductID = A.ProductID LEFT OUTER JOIN
		[ICON_EntForms_Products] P WITH (NOLOCK) ON P.ProductID = U.ProductID LEFT OUTER JOIN
		[ICON_EntForms_Company] CP WITH (NOLOCK) ON CP.CompanyID = P.CompanyID  LEFT OUTER JOIN
		@p D ON A.ContractNumber = D.ContractNumber 
		
WHERE	A.ContractNumber = @ContractNumber; */


GO
