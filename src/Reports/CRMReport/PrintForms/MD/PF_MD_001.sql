SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[AP2SP_PF_AG_001] '10133AA00121'

ALTER PROCEDURE [dbo].[PF_MD_001]
    @AgreementNo  nvarchar(30)
AS

SELECT  'ContractNumber' = AG.AgreementNo
		,'ContractDate' = AG.ContractDate 
        ,'ContractDay' = day(AG.ContractDate)
        ,'ContractMonth' = CASE WHEN AG.ContractDate IS NULL THEN NULL ELSE  [dbo].[fnFormatMonthLongTH] (AG.ContractDate) END
        ,'ContractYear' = year(AG.ContractDate) + 543
		,'BookingNumber' = BK.BookingNo
		,'BookingDate' = BK.BookingDate
        ,'BookingDay' = day(BK.BookingDate)
        ,'BookingMonth' = CASE WHEN BK.BookingDate IS NULL THEN NULL ELSE  [dbo].[fnFormatMonthLongTH] (BK.BookingDate) END
        ,'BookingYear' = year(BK.BookingDate) + 543
		, 'ProductID'  = P.ProjectNo 
        , 'ProjectName' = P.ProjectNameTH --ชื่อโครงการ
		, 'NameBooking' = [dbo].[fn_GenCustAgreementAll_AG](AG.AgreementNo)--ISNULL(AO.NamesTitle,'') + ISNULL(AO.FirstName,'') + ' '+ISNULL(AO.LastName,'') 	
        , 'Age' = Year(CURRENT_TIMESTAMP)-Year(AO.BirthDate)
		, 'BCurrentAddress' = ISNULL(AOA.HouseNoTH,'-')
        , 'BMoo' = ISNULL(AOA.HouseNoEN,'-')
		, 'B.Soi' = CASE WHEN AOA.SoiTH = '' THEN '-' ELSE ISNULL(AOA.SoiTH,'-') END
		, 'BRoad' = CASE WHEN AOA.RoadTH = '' THEN '-' ELSE ISNULL(AOA.RoadTH,'-') END 
        , 'BSubDistrict' = CASE WHEN AOA.SubDistrictID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.SubDistrict WHERE ID = AOA.SubDistrictID) END
        , 'BDistrict' = CASE WHEN AOA.DistrictID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.District WHERE ID = AOA.DistrictID) END
        , 'BProvince' = CASE WHEN AOA.ProvinceID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.Province WHERE ID = AOA.ProvinceID) END
        , 'BPostCode' = ISNULL(AOA.PostalCode,'-')
        , 'BPhone' = '' --CASE WHEN AO.Phone = '' THEN '-' ELSE ISNULL(AO.Phone,'-') END
		, 'BMobile' = ISNULL(AOP.PhoneNumber,'-')
	    , 'CompanyName' = CP.NameTH
		, 'AddressThai' = CASE WHEN CP.AddressTH = '' THEN '-'
                            ELSE ISNULL(CP.AddressTH,'-') END  --ที่อยู่บริษัท

		, 'Road' = CASE WHEN PA.RoadTH = '' THEN '-' ELSE ISNULL(PA.RoadTH,'-') END
		, 'SubDistrict' = CASE WHEN PA.SubDistrictID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.SubDistrict WHERE ID = PA.SubDistrictID) END
	    , 'District' = CASE WHEN PA.DistrictID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.District WHERE ID = PA.DistrictID) END
		, 'Province' = CASE WHEN PA.ProvinceID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.Province WHERE ID = PA.ProvinceID) END --ที่อยู่โครงการ
		, 'TowerName' = Replace(TW.TowerNoTH,'อาคาร','')
        , 'UnitNumber' = U.UnitNo  --ห้องชุดเลขที่
        , 'FloorID' = CASE WHEN U.FloorID IS NULL THEN '-' ELSE (SELECT NameTH FROM PRJ.Floor WHERE ID = U.FloorID) END
        , 'StandardArea' = ISNULL(U.SaleArea, (SELECT TitledeedArea FROM PRJ.TitledeedDetail WHERE UnitID = U.ID)) --พื้นที่(รวมระเบียง)
		,'IncreasingAreaPrice' = AG.OffsetAreaPrice
		,'IncreasingAreaPriceText' = [dbo].[fnBHT_BahtText](AG.OffsetAreaPrice)
        , 'TotalSellingPrice' = ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID,N'ราคาขายสุทธิ')),0)  --รวมเป็นเงินทั้งสิ้น
        , 'TotalSellingPriceText' = dbo.fnBHT_BahtText (ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID,N'ราคาขายสุทธิ')),0))  --รวมเป็นเงินทั้งสิ้น
		, 'BookingPaid' = ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินจอง')),0) ----เงินจอง
		, 'BookingPaidText' = CASE WHEN ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินจอง')),0) = 0 THEN '-' ELSE [dbo].[fnBHT_BahtText]((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินจอง'))) END ----เงินจอง
		, 'ContractPaid' = ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินสัญญา')),0) ----เงินสัญญา
		, 'DownPaymentPeriod' = ISNULL((SELECT Installment FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินดาวน์')),0)
		, 'ContractPaidText' = CASE WHEN ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินสัญญา')),0) = 0 THEN '-' ELSE [dbo].[fnBHT_BahtText]((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินสัญญา'))) END ----เงินสัญญา
		, 'TotalPaid' = ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินจอง')),0)+ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินสัญญา')),0)
		, 'TotalPaidText' = [dbo].[fnBHT_BahtText](ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินจอง')),0)+ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินสัญญา')),0))
		, 'TotalNoPay'	= ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'ราคาขายสุทธิ')),0)-(ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินจอง')),0)+ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินสัญญา')),0))
		, 'TotalNoPayText'	= [dbo].[fnBHT_BahtText](ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'ราคาขายสุทธิ')),0)-(ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินจอง')),0)+ISNULL((SELECT Amount FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินสัญญา')),0)))
		, 'PeriodDate' = CASE WHEN (SELECT Installment FROM [dbo].[fn_GetAmountFromUnitPriceItemByName] (U.ID, N'เงินดาวน์')) > 0 THEN '-' ELSE NULL END
		, 'StartDate' = '' --AG.StartDate

		----ผู้จะซื้อ
        , 'NameAgreement' = [dbo].[fn_GenCustAgreementAll_AG](AG.AgreementNo)
        --ที่อยู่ผู้ทำสัญญา
        , 'PermanentAddress'= ISNULL(AOA.HouseNoTH,'-')
		, 'PermanentMoo'= ISNULL(AOA.MooTH,'-')
        , 'PermanentVillage'= ISNULL(AOA.VillageTH,'-')
		, 'PermanentSoi'= ISNULL(AOA.SoiTH,'-')
        , 'PermanentRoad'= ISNULL(AOA.RoadTH,'-')
		, 'PermanentSubDistrict'= CASE WHEN AOA.SubDistrictID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.SubDistrict WHERE ID = AOA.SubDistrictID) END
        , 'PermanentDistrict'= CASE WHEN AOA.DistrictID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.District WHERE ID = AOA.DistrictID) END
		, 'PermanentProvince'= CASE WHEN AOA.ProvinceID IS NULL THEN '-' ELSE (SELECT NameTH FROM MST.Province WHERE ID = AOA.ProvinceID) END
        , 'PermanentPostID'= ISNULL(AOA.PostalCode,'-')
        , 'PermanentPhone'= ISNULL(AOP.PhoneNumber, '-')
        , 'InsurancePremiumBuilding'= ISNULL((SELECT CondoFundRate FROM PRJ.AgreementConfig WHERE ProjectID = P.ID),0)  --อัตรากองทุนคอนโด 
        , 'PublicFundRate'= ISNULL((SELECT PublicFundRate FROM PRJ.AgreementConfig WHERE ProjectID = P.ID),0)  --อัตราค่าส่วนกลาง
        , 'UnitTransferfee'= ISNULL((SELECT ChangeNameFee FROM PRJ.AgreementConfig WHERE ProjectID = P.ID),0) 
        , 'UnitTransferfeeText' = ISNULL(dbo.fnBHT_BahtText((SELECT ChangeNameFee FROM PRJ.AgreementConfig WHERE ProjectID = P.ID)),0)
		, 'FlagFur' = '' --CASE WHEN ISNULL(AG.FurniturePrice,0)  > 0 THEN 1 ELSE 0 END
        , 'ForType' = DocPP.TypeDesc --CASE @ForType WHEN 1 THEN 'สำหรับบริษัท' WHEN 2 THEN 'สำหรับลูกค้า' ELSE '' END

FROM	[SAL].[Agreement] AG WITH (NOLOCK)
        LEFT OUTER JOIN [SAL].[AgreementOwner] AO WITH (NOLOCK) ON AO.AgreementID = AG.ID AND AO.IsMainOwner = 1 AND AO.IsDeleted = 0		
        LEFT OUTER JOIN [SAL].[AgreementOwnerAddress] AOA WITH (NOLOCK) ON AOA.AgreementOwnerID = AO.ID AND AOA.ContactAddressTypeMasterCenterID = (SELECT ID FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'ContactAddressType' AND [Key] = 0)
		LEFT OUTER JOIN [SAL].[AgreementOwnerPhone] AOP WITH (NOLOCK) ON AOP.AgreementOwnerID = AO.ID AND AOP.PhoneTypeMasterCenterID = (SELECT ID FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'PhoneType' AND [Key] = 0)
		LEFT OUTER JOIN [SAL].[Booking] BK  WITH (NOLOCK) ON AG.BookingID = BK.ID
		LEFT OUTER JOIN [SAL].[BookingOwner] BO  WITH (NOLOCK) ON BO.BookingID = BK.ID AND ISNULL(BO.IsMainOwner,0) = '1' AND ISNULL(BO.IsDeleted,0) = 0
		LEFT OUTER JOIN [PRJ].[Project] P WITH (NOLOCK) ON P.ID = BK.ProjectID 
		LEFT OUTER JOIN [PRJ].[Address] PA	WITH (NOLOCK) ON PA.ProjectID = P.ID AND PA.ProjectAddressTypeMasterCenterID IN (SELECT ID FROM MST.MasterCenter WHERE MasterCenterGroupKey = 'ProjectAddressType' AND [Key] = 1)
        LEFT OUTER JOIN [MST].[Company] CP WITH (NOLOCK) ON CP.ID = P.CompanyID 
		LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ProjectID = BK.ProjectID AND U.ID = BK.UnitID 
        LEFT OUTER JOIN [USR].[User] US WITH (NOLOCK) ON US.ID = BK.SaleUserID 
		LEFT OUTER JOIN [PRJ].[Tower] TW WITH (NOLOCK) ON TW.ID = U.TowerID AND TW.ProjectID = U.ProjectID

		JOIN (
	
			SELECT  'ForType' = 1 , 'TypeDesc' = '( สำหรับบริษัท )' 
			UNION
			SELECT  'ForType' = 2 , 'TypeDesc' = '( สำหรับลูกค้า )' 
					
		) DocPP ON 1=1
		
WHERE AG.AgreementNo = @AgreementNo; 




GO
