SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[AP2SP_PF_AG_001] '10133AA00121'

ALTER PROCEDURE [dbo].[AP2SP_PF_AG_001]
    @ContractNumber  nvarchar(15)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
/* SELECT  AG.ContractNumber
		,'ContractDate' = AG.ContractDate 
		,BK.BookingNumber
		,BK.BookingDate
		, P.ProductID
        , 'ProjectName' = P.Project --ชื่อโครงการ
		, 'NameBooking' = [dbo].[fn_GenCustAgreementAll_AG](AG.ContractNumber)--ISNULL(AO.NamesTitle,'') + ISNULL(AO.FirstName,'') + ' '+ISNULL(AO.LastName,'') 	
        , 'Age' = Year(AG.ContractDate )-Year(AO.BirthDate)
		, 'BCurrentAddress' = ISNULL(AO.CurrentAddress,'-')
        , 'BMoo' = ISNULL(AO.Moo,'-')
		, 'B.Soi' = CASE WHEN AO.Soi = '' THEN '-' ELSE ISNULL(AO.Soi,'-') END
		, 'BRoad' = CASE WHEN AO.Road = '' THEN '-' ELSE ISNULL(AO.Road,'-') END 
        , 'BSubDistrict' = ISNULL(AO.SubDistrict,'-')
        , 'BDistrict' = ISNULL(AO.District,'-')
        , 'BProvince' = ISNULL(AO.Province,'-')
        , 'BPostCode' = ISNULL(AO.PostCode,'-')
        , 'BPhone' = CASE WHEN AO.Phone = '' THEN '-' ELSE ISNULL(AO.Phone,'-') END
		, 'BMobile' = CASE WHEN AO.Mobile = '' THEN '-' ELSE ISNULL(AO.Mobile,'-') END 
	    , 'CompanyName' = [dbo].[fn_GetCompanyNameTH](P.CompanyID,AG.ContractDate) --CP.CompanyNameThai 
		, 'AddressThai' = CASE WHEN CP.AddressThai = '' THEN '-'
                            ELSE ISNULL(CP.AddressThai,'-') END  --ที่อยู่บริษัท

		, 'Road' = CASE WHEN PA.Road = '' THEN '-' ELSE ISNULL(PA.Road,'-') END
		, 'SubDistrict' = ISNULL(PA.SubDistrict,'-')
	    , 'District' = ISNULL(PA.District,'-')
		, 'Province' = ISNULL(PA.Province,'-') --ที่อยู่โครงการ
		, 'TowerName' = Replace(TW.TowerName,'อาคาร','')
        , U.UnitNumber  --ห้องชุดเลขที่
        , 'FloorID' = ISNULl(U.FloorID,'-')
        , 'StandardArea' = ISNULL(U.AreaFromPFB,U.AreaFromRE) --พื้นที่(รวมระเบียง)
		,'IncreasingAreaPrice' = AG.UnitIncreasingAreaPrice
		,'IncreasingAreaPriceText' = [dbo].[fnBHT_BahtText](AG.UnitIncreasingAreaPrice)
--		,'IncreasingAreaPrice' = CASE WHEN ISNULL(U.AreaFromPFB,U.AreaFromRE) = 0 THEN 0 
--								ELSE AG.SellingPrice/(ISNULL(U.AreaFromPFB,U.AreaFromRE)) END --ตาราเมตรละ
--		,'IncreasingAreaPriceText' =  [dbo].[fnBHT_BahtText](CASE WHEN  ISNULL(U.AreaFromPFB,U.AreaFromRE) = 0 THEN 0 
--								 ELSE AG.SellingPrice/(ISNULL(U.AreaFromPFB,U.AreaFromRE)) END)--ตาราเมตรละ
        , 'TotalSellingPrice' = ISNULL(AG.SellingPrice,0)  --รวมเป็นเงินทั้งสิ้น
        , 'TotalSellingPriceText' = dbo.fnBHT_BahtText (ISNULL(AG.SellingPrice,0))  --รวมเป็นเงินทั้งสิ้น
		, 'BookingPaid' = AG.BookingPaid ----เงินจอง
		, 'BookingPaidText' = CASE WHEN ISNULL(AG.BookingPaid,0) = 0 THEN '-' ELSE [dbo].[fnBHT_BahtText](AG.BookingPaid) END ----เงินจอง
		, 'ContractPaid' = ISNULL(AG.ContractAmount,0) ----เงินสัญญา
		, 'DownPaymentPeriod' = ISNULL(AG.DownPaymentPeriod, 0)
		, 'ContractPaidText' = CASE WHEN ISNULL(AG.ContractAmount,0) = 0 THEN '-' ELSE [dbo].[fnBHT_BahtText](AG.ContractAmount) END ----เงินสัญญา
		, 'TotalPaid' = ISNULL(AG.BookingPaid,0)+ISNULL(AG.ContractAmount,0)
		, 'TotalPaidText' = [dbo].[fnBHT_BahtText](ISNULL(AG.BookingPaid,0)+ISNULL(AG.ContractAmount,0))
		, 'TotalNoPay'	= ISNULL(AG.SellingPrice,0)-(ISNULL(AG.BookingPaid,0)+ISNULL(AG.ContractAmount,0))
		, 'TotalNoPayText'	= [dbo].[fnBHT_BahtText](ISNULL(AG.SellingPrice,0)-(ISNULL(AG.BookingPaid,0)+ISNULL(AG.ContractAmount,0)))
		, 'PeriodDate' = CASE WHEN AG.DownPaymentPeriod > 0 THEN AG.PeriodDate ELSE NULL END
		,AG.StartDate

		----ผู้จะซื้อ
        , 'NameAgreement' =  [dbo].[fn_GenCustAgreementAll_AG](AG.ContractNumber)
        --ที่อยู่ผู้ทำสัญญา
        , 'PermanentAddress'= ISNULL(AO.CurrentAddress,'-')
		, 'PermanentMoo'=ISNULL(AO.Moo,'-')
        , 'PermanentVillage'= ISNULL(AO.Village,'-')
		, 'PermanentSoi'=ISNULL(AO.Soi,'-')
        , 'PermanentRoad'= ISNULL(AO.Road,'-')
		, 'PermanentSubDistrict'= ISNULL(AO.SubDistrict,'-')
        , 'PermanentDistrict'= ISNULL(AO.District,'-')
		, 'PermanentProvince'=ISNULL(AO.Province,'-')
        , 'PermanentPostID'= ISNULL(AO.PostCode,'-')
        , 'PermanentPhone'= CASE WHEN AO.Mobile = '' THEN '-' ELSE ISNULL(AO.Mobile,'-') END 
        , 'InsurancePremiumBuilding'= ISNULL(P.SinkingFundRate,0)  --อัตรากองทุนคอนโด 
        , 'PublicFundRate'= ISNULL(P.PublicFundRate,0)  --อัตราค่าส่วนกลาง
        , 'UnitTransferfee'= CASE WHEN P.ProductID='30002' OR P.ProductID='60004' OR P.ProductID='60005' OR P.ProductID='60006'  
						THEN 0 ELSE ISNULL(P.UnitTransferfee,0) END
        , 'UnitTransferfeeText' = dbo.fnBHT_BahtText(P.UnitTransferfee)
		, 'FlagFur' = CASE WHEN ISNULL(AG.FurniturePrice,0)  > 0 THEN 1 ELSE 0 END
        , 'ForType' = DocPP.TypeDesc --CASE @ForType WHEN 1 THEN 'สำหรับบริษัท' WHEN 2 THEN 'สำหรับลูกค้า' ELSE '' END

FROM	[ICON_EntForms_Agreement] AG WITH (NOLOCK)
        LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO WITH (NOLOCK) ON AO.ContractNumber = AG.ContractNumber AND ISNULL(AO.Header,0) = '1' AND ISNULL(AO.IsDelete,0) = 0
		LEFT OUTER JOIN [ICON_EntForms_Booking] BK  WITH (NOLOCK) ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO  WITH (NOLOCK) ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.Header,0) = '1' AND ISNULL(BO.IsDelete,0) = 0
		LEFT OUTER JOIN [ICON_EntForms_Products] P WITH (NOLOCK) ON P.ProductID = BK.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_ProductsAddress] PA	WITH (NOLOCK) ON PA.ProductID = P.ProductID AND PA.AddressFlag IN('0','1')
        LEFT OUTER JOIN [ICON_EntForms_Company] CP WITH (NOLOCK) ON CP.CompanyID = P.CompanyID 
		LEFT OUTER JOIN [ICON_EntForms_Unit] U WITH (NOLOCK) ON U.ProductID = BK.ProductID AND U.UnitNumber = BK.UnitNumber 
        LEFT OUTER JOIN [Users] US WITH (NOLOCK) ON US.UserID = BK.SaleID 
		LEFT OUTER JOIN [ICON_EntForms_Tower] TW WITH (NOLOCK) ON TW.TowerID = U.TowerID AND TW.ProductID = U.ProductID
--		LEFT OUTER JOIN 
--		(
--			SELECT	ReferentID,SUM(ISNULL(Amount,0)) AS Amount
--			FROM	ICON_Payment_paymentDetails
--			WHERE	ReferentID = @ContractNumber AND Paymenttype = '5' 
--			GROUP BY ReferentID
--		)PD ON PD.ReferentID = AG.ContractNumber

		JOIN (
			
			SELECT  'ForType' = 1 , 'TypeDesc' = '( สำหรับบริษัท )' 
			UNION
			SELECT  'ForType' = 2 , 'TypeDesc' = '( สำหรับลูกค้า )' 
					
		) DocPP ON 1=1
		
WHERE AG.ContractNumber = @ContractNumber; */

GO
