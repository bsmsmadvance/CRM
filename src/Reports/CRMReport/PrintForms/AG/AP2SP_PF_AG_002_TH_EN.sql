SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [dbo].[AP2SP_PF_AG_002_TH_EN] '60002BA00011'
ALTER PROCEDURE [dbo].[AP2SP_PF_AG_002_TH_EN]
    @BookingNumber  nvarchar(15)	
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @UPI Table(Name VARCHAR(20), Amount money, Installment nvarchar(2))
INSERT INTO @UPI(Name, Amount, Installment)
	SELECT	'Name' = UPI.Name 
            , 'Amount' = UPI.Amount
            , 'Installment' = UPI.Installment
	FROM [SAL].[Booking] BK  WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[UnitPrice] UP WITH (NOLOCK) ON UP.BookingID = BK.ID
    LEFT OUTER JOIN [SAL].[UnitPriceItem] UPI WITH (NOLOCK) ON UPI.UnitPriceID = UP.ID  
    WHERE BK.BookingNo = @BookingNumber AND UP.IsActive = 1


SELECT  BK.BookingNo
		, BK.BookingDate
		, 'ProjectID' = P.ProjectNo --ProjectID
        , 'ProjectName' = P.ProjectNameTH --ชื่อโครงการ
        , 'ProjectNameEN' = P.ProjectNameEN
		, 'NameBooking' = CASE WHEN ISNULL(BO.TitleExtTH,'')='' OR ISNULL(BO.TitleExtTH,'')='0' THEN ISNULL(BO.TitleExtTH,'') ELSE ISNULL(BO.TitleExtTH,'') END + ISNULL(BO.FirstNameTH,'') + ' '+ISNULL(BO.LastNameTH,'') 		
		, 'NameBookingEN' = CASE WHEN ISNULL(CT.TitleExtEN,'')='' OR ISNULL(CT.TitleExtEN,'')='0' THEN ISNULL(CT.TitleExtEN,'') ELSE ISNULL(CT.TitleExtEN,'') END + ISNULL(CT.FirstNameEN,'') + ' '+ISNULL(CT.LastNameEN,'') 	
        , 'Age' = Year(GetDate())-Year(BO.BirthDate)
		, 'Nationality' = CASE WHEN BO.NationalMasterCenterID = '' THEN '-' ELSE ISNULL((SELECT Name FROM MST.MasterCenter where ID = BO.NationalMasterCenterID),'-') END
		, 'NationalityEN' = CASE WHEN CT.NationalMasterCenterID = '' THEN '-' ELSE ISNULL((SELECT Name FROM MST.MasterCenter where ID = CT.NationalMasterCenterID),'-') END
		, 'PersonalID' = CT.CitizenIdentityNo
		, 'BCurrentAddress' =  ISNULL((SELECT HouseNoTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')
        , 'BMoo' = ISNULL((SELECT MooTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')
		, 'BSoi' = CASE WHEN (SELECT MooTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT MooTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END
		, 'BRoad' = CASE WHEN (SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END 
        , 'BSubDistrict' = ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameTH'),'-')
        , 'BDistrict' = ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] ((SELECT DistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameTH'),'-')
        , 'BProvince' = ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] ((SELECT ProvinceID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameTH'),'-')
        , 'BPostCode' = ISNULL((SELECT PostalCode FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')

		, 'BCurrentAddressEN' = ISNULL((SELECT HouseNoEN FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')
        , 'BMooEN' = CASE WHEN (SELECT MooEN FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' Then '-' Else ISNULL((SELECT MooEN FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END
		, 'BSoiEN' = CASE WHEN (SELECT SoiEN FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT SoiEN FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END 
		, 'BRoadEN' = CASE WHEN (SELECT RoadEN FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT RoadEN FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END 
        , 'BSubDistrictEN' = ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameEN'),'-')
        , 'BDistrictEN' = ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] ((SELECT DistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameEN'),'-')
        , 'BProvinceEN' = ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] ((SELECT ProvinceID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameEN'),'-')
        , 'BPostCodeEN' = ISNULL((SELECT PostalCode FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')

        , 'BPhone' = '' --CASE WHEN BO.Phone = '' THEN '-' ELSE ISNULL(BO.Phone,'-') END
		, 'BMobile' = '' --CASE WHEN BO.Mobile = '' THEN '-' ELSE ISNULL(BO.Mobile,'-') END 
		, 'BEmail' = '' --ISNULL(BO.Email,'-')
        , 'BWechat' = CASE WHEN ISNULL(CT.WeChatID,'-') = '-' OR ISNULL(CT.WeChatID,'-') = '' THEN '-' ELSE ISNULL(CT.WeChatID,'-') END + '/' 
					+ CASE WHEN ISNULL(CT.WhatsAppID,'-') = '-' OR ISNULL(CT.WhatsAppID,'-') = '' THEN '-' ELSE ISNULL(CT.WhatsAppID,'-') END
		, 'BLineID' = CASE WHEN ISNULL(CT.LineID,'-') = '-' OR ISNULL(CT.LineID,'-') = '' THEN '-' ELSE ISNULL(CT.LineID,'-') END

		, 'NameAgreement' = [dbo].[fn_GenCustBookingToContact] (BK.BookingNo)
		, 'NameAgreementEN' = [dbo].[fn_GenCustBookingToContactEN] (BK.BookingNo)

	    , 'CompanyID' = CP.Code --P.CompanyID
	    , 'CompanyName' = [dbo].[fn_GetCompanyNameTH](P.CompanyID,BK.BookingDate)
	    , 'CompanyNameEN' = [dbo].[fn_GetCompanyNameEN](P.CompanyID,BK.BookingDate)
       
	      --ที่อยู่บริษัท
        , 'Address1' = CASE WHEN CP.AddressTH = '' THEN ''
                            ELSE ISNULL(CP.AddressTH,'') END+ '   ' +
                       CASE WHEN CP.BuildingTH = '' THEN ''
                            ELSE ISNULL(+CP.BuildingTH,'')END+' '+
                       --CASE WHEN CP.SoiThai = '' THEN ''
                       --     ELSE ISNULL('ซอย'+CP.SoiThai,'')END+' '+
                       CASE WHEN CP.RoadTH = '' THEN ''
                            ELSE ISNULL('ถนน'+CP.RoadTH,'')END
        , 'Address2' = CASE WHEN CP.SubDistrictID = '' THEN ''
                            ELSE (CASE WHEN [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameTH')  like '%กรุงเทพ%' THEN
                                            'แขวง'+ [dbo].[fn_GetDistrictDetailFromFieldName] ( CP.DistrictID, 'NameTH')
                                       ELSE 'ตำบล'+ [dbo].[fn_GetDistrictDetailFromFieldName] ( CP.DistrictID, 'NameTH') END) END +' '+
                       CASE WHEN CP.DistrictID = '' THEN ''
                             ELSE (CASE WHEN [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameTH') like '%กรุงเทพ%' THEN
                                             'เขต'+ [dbo].[fn_GetDistrictDetailFromFieldName] ( CP.DistrictID, 'NameTH')
                                        ELSE 'อำเภอ'+ [dbo].[fn_GetDistrictDetailFromFieldName] ( CP.DistrictID, 'NameTH') END) END+'  '+
                       CASE WHEN CP.ProvinceID = '' THEN ''
                            ELSE (CASE WHEN [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameTH') like '%กรุงเทพ%' THEN
                                            [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameTH')
                                       ELSE 'จังหวัด'+[dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameTH') END) END +'  '+
                       ISNULL(CP.PostalCode,'')

		          --ที่อยู่บริษัท
        , 'Address1EN' = CASE WHEN CP.AddressEN = '' THEN ''
                            ELSE ISNULL(CP.AddressEN,'') END+ '   ' +
                       CASE WHEN CP.BuildingEN = '' THEN ''
                            ELSE ISNULL(CP.BuildingEN,'')END+' '+
                       CASE WHEN CP.SoiEN = '' THEN ''
                            ELSE ISNULL(CP.SoiEN,'')END+' '+
                       CASE WHEN CP.RoadEN = '' THEN ''
                            ELSE ISNULL(CP.RoadEN+'Road','')END
        , 'Address2EN' = CASE WHEN CP.SubDistrictID = '' THEN ''
                            ELSE (CASE WHEN [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameEN') like '%Bangkok%' THEN
                                            [dbo].[fn_GetSubDistrictDetailFromFieldName] ( CP.SubDistrictID, 'NameEN')
                                       ELSE [dbo].[fn_GetSubDistrictDetailFromFieldName] ( CP.SubDistrictID, 'NameEN') END) END +', '+
                       CASE WHEN CP.DistrictID = '' THEN ''
                             ELSE (CASE WHEN [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameEN') like '%Bangkok%' THEN
                                             [dbo].[fn_GetDistrictDetailFromFieldName] ( CP.DistrictID, 'NameEN')
                                        ELSE [dbo].[fn_GetDistrictDetailFromFieldName] ( CP.DistrictID, 'NameEN') END) END+',  '+
                       CASE WHEN CP.ProvinceID = '' THEN ''
                            ELSE (CASE WHEN [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameEN') like '%Bangkok%' THEN
                                            [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameEN')
                                       ELSE [dbo].[fn_GetProvinceDetailFromFieldName] ( CP.ProvinceID, 'NameEN') END) END +'  '+
                       ISNULL(CP.PostalCode,'')

		--ที่อยู่โครงการ
        , 'Soi' = CASE WHEN PA.SoiTH = '' THEN '-' ELSE ISNULL(PA.SoiTH,'-') END
		, 'Road' = CASE WHEN PA.RoadTH = '' THEN '-' ELSE ISNULL(PA.RoadTH,'-') END
		, 'SubDistrict' = CASE WHEN PA.SubDistrictID = '' THEN '-' ELSE ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] (PA.SubDistrictID, 'NameTH'),'-') END
	    , 'District' = CASE WHEN PA.DistrictID = '' THEN '-' ELSE ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] (PA.DistrictID, 'NameTH'),'-') END
		, 'Province' = CASE WHEN PA.ProvinceID = '' THEN '-' ELSE ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] (PA.ProvinceID, 'NameTH'),'-') END
		, 'ProjectWechat' = CASE WHEN ISNULL(P.WeChatID,'-') = '-' OR ISNULL(P.WeChatID,'-') = '' THEN '-' ELSE ISNULL(P.WeChatID,'-') END + '/' 
					+ CASE WHEN ISNULL(P.WhatsAppID,'-') = '-' OR ISNULL(P.WhatsAppID,'-') = '' THEN '-' ELSE ISNULL(P.WhatsAppID,'-') END


		, 'SoiEN' = CASE WHEN PA.SoiEN = '' THEN '-' ELSE ISNULL(PA.SoiEN,'-') END
		, 'RoadEN' = CASE WHEN PA.RoadEN = '' THEN '-' ELSE ISNULL(PA.RoadEN,'-') END
		, 'SubDistrictEN' = CASE WHEN PA.SubDistrictID = '' THEN '-' ELSE ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] (PA.SubDistrictID, 'NameEN'),'-') END
	    , 'DistrictEN' = CASE WHEN PA.DistrictID = '' THEN '-' ELSE ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] (PA.DistrictID, 'NameEN'),'-') END
		, 'ProvinceEN' = CASE WHEN PA.ProvinceID = '' THEN '-' ELSE ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] (PA.ProvinceID, 'NameEN'),'-') END

        , 'UnitNumber' = U.UnitNo  --ห้องชุดเลขที่
        , 'FloorID' = CASE WHEN U.FloorID = '' THEN '-' ELSE ISNULL((SELECT NameTH FROM PRJ.[Floor] where ID = U.FloorID),'-') END
        , 'StandardArea' = ISNULL(BK.SaleArea,0) --พื้นที่(รวมระเบียง)
		 ,'StandardAreaText' = [dbo].[fnTH_NumberText](BK.SaleArea)
		 ,'StandardAreaTextEN' = [dbo].[NumberToWords](BK.SaleArea)
        , 'TotalSellingPrice' = ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคาขาย'),0)   --ราคามาตรฐาน
        , 'TotalSellingPriceText' =  dbo.fnBHT_BahtText(ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคาขาย'),0) )  --ราคามาตรฐาน
        , 'TotalSellingPriceTextEN' = [dbo].[Currency_ToWords](ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคาขาย'),0) )  --ราคามาตรฐาน
        , 'SellingPrice' = ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคราขายสุทธิ'),0)   --ราคาขาย(หักส่วนลดแล้ว)
        , 'PriceText' = dbo.fnBHT_BahtText(ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคราขายสุทธิ'),0))
        , 'PriceTextEN' = [dbo].[Currency_ToWords](ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคราขายสุทธิ'),0))
        , BK.ContractDueDate
		,'PricePerSqm' = CEILING(ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคราขายสุทธิ'),0)/ISNULL(BK.SaleArea,1))
		,'PricePerSqmText' = [dbo].fnBHT_BahtText(CEILING(ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคราขายสุทธิ'),0) /ISNULL(BK.SaleArea,1)))
		,'PricePerSqmTextEN' = [dbo].[Currency_ToWords](CEILING(ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคราขายสุทธิ'),0)/ISNULL(BK.SaleArea,1)))

        --ที่อยู่ผู้ทำสัญญา
        , 'PermanentAddress'= ISNULL((SELECT HouseNoTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-')
		, 'PermanentMoo'=CASE WHEN (SELECT MooTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT HouseNoTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-') END
        , 'PermanentVillage'= CASE WHEN (SELECT VillageTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT VillageTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-') END
		, 'PermanentSoi'= CASE WHEN (SELECT SoiTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT SoiTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-') END --,LEN(CASE WHEN CT2.Road_3 = '' THEN '-' ELSE ISNULL(CT2.Road_3,'-') END)
        , 'PermanentRoad'= CASE WHEN (SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-') END
		, 'PermanentSubDistrict'= ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameTH'),'-')
        , 'PermanentDistrict'= ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] ((SELECT DistrictID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameTH'),'-')
		, 'PermanentProvince'= ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] ((SELECT ProvinceID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameTH'),'-')
        , 'PermanentPostID'= ISNULL((SELECT PostalCode FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-')
		, 'PermanentPhone' = CASE WHEN (SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT2.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT2.ID,'1')),'-') END
		, 'PermanentMobile' = CASE WHEN (SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT2.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT2.ID,'0')),'-') END 
		, 'PermanentEmail' = CASE WHEN (SELECT Email FROM [dbo].[fn_GetContactEmail] (CT2.ID)) = '' THEN '-' ELSE ISNULL((SELECT Email FROM [dbo].[fn_GetContactEmail] (CT2.ID)),'-') END 
        , 'PermanentWechat' = CASE WHEN ISNULL(CT2.WeChatID,'-') = '-' OR ISNULL(CT2.WeChatID,'-') = '' THEN '-' ELSE ISNULL(CT2.WeChatID,'-') END + '/' 
					+ CASE WHEN ISNULL(CT2.WhatsAppID,'-') = '-' OR ISNULL(CT2.WhatsAppID,'-') = '' THEN '-' ELSE ISNULL(CT2.WhatsAppID,'-') END
		, 'PermanentLineID' = CASE WHEN ISNULL(CT2.LineID,'-') = '-' OR ISNULL(CT2.LineID,'-') = '' THEN '-' ELSE ISNULL(CT2.LineID,'-') END

		, 'PermanentAddressEN'= ISNULL((SELECT HouseNoEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-')
		, 'PermanentMooEN'=CASE WHEN (SELECT MooEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT MooEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-') END
        , 'PermanentVillageEN'= CASE WHEN (SELECT VillageEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')) = '' THEN '-' ELSE  ISNULL((SELECT VillageEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-') END
		, 'PermanentSoiEN'= ISNULL((SELECT SoiEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-')
        , 'PermanentRoadEN'= CASE WHEN (SELECT RoadEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT RoadEN FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-') END
		, 'PermanentSubDistrictEN'= CASE WHEN ISNULL((SELECT CountryID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'ประเทศไทย') like '%ไทย%' THEN 
                                    ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameEN'),'-') ELSE 
                                    ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameEN'),'-') END
        , 'PermanentDistrictEN'= CASE WHEN ISNULL((SELECT CountryID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'ประเทศไทย') like '%ไทย%' THEN 
                                    ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] ((SELECT DistrictID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameEN'),'-') ELSE 
                                    ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] ((SELECT DistrictID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameEN'),'-') END
		, 'PermanentProvinceEN'=CASE WHEN ISNULL((SELECT CountryID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'ประเทศไทย') like '%ไทย%' THEN 
                                    ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] ((SELECT ProvinceID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameEN'),'-') ELSE 
                                    ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] ((SELECT ProvinceID FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')), 'NameEN'),'-') END
        , 'PermanentPostIDEN'= ISNULL((SELECT PostalCode FROM [dbo].[fn_GetContactAddress] (CT2.ID,'1')),'-')

        , 'InsurancePremiumBuilding'= ISNULL(AC.CondoFundRate,0)  --อัตรากองทุนคอนโด
        , 'InsurancePremiumBuildingText' = dbo.fnBHT_BahtText(AC.CondoFundRate)
        , 'InsurancePremiumBuildingTextEN' = dbo.[Currency_ToWords](AC.CondoFundRate)
        , 'PublicFundRate'= ISNULL(AC.PublicFundRate,0)  --อัตราค่าส่วนกลาง
        , 'PublicFundRateText' = dbo.fnBHT_BahtText(AC.PublicFundRate)
        , 'PublicFundRateTextEN' = dbo.[Currency_ToWords](AC.PublicFundRate)
        , 'UnitTransferfee'= ISNULL(AC.ChangeNameFee,0)
        , 'UnitTransferfeeText' = dbo.fnBHT_BahtText(AC.ChangeNameFee)
        , 'UnitTransferfeeTextEN' = dbo.[Currency_ToWords](AC.ChangeNameFee)

        , 'SaleName' = CASE WHEN BK.SaleUserID IS NOT NULL THEN ISNULL(US.FirstName,'') + ' ' + ISNULL(US.LastName,'') --ผู้รับจอง
				--WHEN BK.SaleTraineeID > 0 THEN ISNULL(US3.FirstName,'') + ' ' + ISNULL(US3.LastName,'')
				ELSE ISNULL(US2.FirstName,'') + ' ' + ISNULL(US2.LastName,'') END

        , 'PromotionDetail' = '' --BK.PromotionDetail
		, 'CashDiscount'= ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0) --ส่วนลดเงินสด
		, 'CashDiscountText'= CASE WHEN ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0)=0 THEN '-' ELSE dbo.fnBHT_BahtText(ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0)) END --ส่วนลดเงินสด
		, 'CashDiscountTextEN'= CASE WHEN ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0)=0 THEN '-' ELSE dbo.[Currency_ToWords](ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0)) END --ส่วนลดเงินสด
		, 'TransferDiscount'= ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลด ณ​ วันโอน'),0) --ส่วนลด ณ วันโอน
        , 'TransferDiscountText' = CASE WHEN ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลด ณ​ วันโอน'),0)=0 THEN '-' ELSE dbo.fnBHT_BahtText(ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0)) END
        , 'TransferDiscountTextEN' = CASE WHEN ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลด ณ​ วันโอน'),0)=0 THEN '-' ELSE dbo.[Currency_ToWords](ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0)) END
		
        --, 'Condition' = '' --'หมายเหตุ โอนกรรมสิทธิ์ภายในวันที่ ' +  dbo.FormatDateTime('TH', 'dd MMMM yyyy', BK.PromotionTransferDate)
		--, 'ConditionEN' = '' --'Remarks:  Ownership transfer within ' +  dbo.FormatDateTime('EN', 'dd MMMM yyyy', BK.PromotionTransferDate)
        , 'ReceivedID' = '' --[dbo].[fn_GenPrintingIDBK](BK.BookingNumber)
		, 'Parking' = U.NumberOfParkingUnFix
		, 'Is3Step' = CAST(0 AS Bit) 
		, 'ApproveDate' = '' --ISNULL(R.RDate,ISNULL(AdHoc.Approve3Date,BK.ApproveDate))
		, 'PromotionItemAmt' = '' --CASE WHEN ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0)>0 THEN 1 ELSE 0 END +
							--CASE WHEN ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลด ณ​ วันโอน'),0)>0 THEN 1 ELSE 0 END +
							--CASE WHEN SpecialDiscount>0 THEN 1 ELSE 0 END +
							--(SELECT COUNT(*) 
							--	FROM ZPROM_SalePromotionDetail SPD  WITH (NOLOCK)
							--		LEFT OUTER JOIN ZPROM_PromotionDetail PD WITH (NOLOCK) ON SPD.PromotionID = PD.PromotionID AND SPD.ItemID = PD.ItemID
							--	WHERE SPD.DocumentID=BK.BookingNumber 
							--			AND SPD.DocumentType = 1
							--			AND PD.IsDisplayPromotion = 1	
							--			AND (PD.DescriptionENG IS NOT NULL OR PD.DescriptionENG = '')
							--			AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 2.5%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))
							--			AND NOT ((ISNULL(PD.DescriptionTH,'') LIKE '%ดอกเบี้ย 3%%') AND (ISNULL(PD.DescriptionTH,'') LIKE '%3 ปี%'))) + 
							--(SELECT COUNT(*)
							--	FROM ZPROM_SalePromotionFee S WITH (NOLOCK)
							--		LEFT OUTER JOIN ICON_EntForms_Booking B WITH (NOLOCK) ON S.DocumentID=B.BookingNumber AND S.DocumentType=1
							--		LEFT OUTER JOIN ICON_EntForms_Products P WITH (NOLOCK) ON B.ProductID=P.ProductID	
							--	WHERE DocumentID = BK.BookingNumber 
							--		AND S.DocumentType = 1 
							--		AND ((PromotionFeeID = '15' AND Charge='N')
							--		 OR (PromotionFeeID IN ('00','01','02','17','2G','37','000') AND (Charge='N' OR Charge='H'))))
        ,ISNULL(U.IsForeignUnit,0) AS IsForeignUnit
		,CAST(CASE WHEN P.ProjectNo = '60021' OR P.ProjectNo = '60022' OR P.ProjectNo = '40052' OR P.ProjectNo = '60023' THEN 1 ELSE 0 END AS  BIT) AS IsForeignQuataProject 		
		--,ISNULL(CT.IsNationalityThai,0) AS IsNationalityThai
FROM	[SAL].[Booking] BK  WITH (NOLOCK)
		LEFT OUTER JOIN [SAL].[BookingOwner] BO WITH (NOLOCK) ON BO.BookingID = BK.ID AND ISNULL(BO.IsMainOwner,0) = '1' AND ISNULL(BO.IsDeleted,0) = 0
		LEFT OUTER JOIN [PRJ].[Project] P WITH (NOLOCK) ON P.ID = BK.ProjectID 
        LEFT OUTER JOIN [PRJ].[AgreementConfig] AC WITH (NOLOCK) ON AC.ProjectID = P.ID 
		LEFT OUTER JOIN [PRJ].[Address] PA	WITH (NOLOCK) ON PA.ProjectID = P.ID AND PA.ProjectAddressTypeMasterCenterID IN('0','1')
        LEFT OUTER JOIN [MST].[Company] CP WITH (NOLOCK) ON CP.ID = P.CompanyID 
		LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ProjectID = BK.ProjectID AND U.ID = BK.UnitID 
        LEFT OUTER JOIN [USR].[User] US WITH (NOLOCK) ON US.ID = BK.SaleUserID 
        LEFT OUTER JOIN [USR].[User] US2 WITH (NOLOCK) ON US2.ID = BK.ProjectSaleUserID 
        --LEFT OUTER JOIN [USR].[User] US3 WITH (NOLOCK) ON US3.ID = BK.SaleTraineeID 
		LEFT OUTER JOIN [CTM].[Contact] CT WITH (NOLOCK) ON CT.ID = BO.FromContactID
		LEFT OUTER JOIN [SAL].[Agreement] A WITH (NOLOCK) ON A.BookingID = BK.ID
        LEFT OUTER JOIN [SAL].[AgreementOwner] AO WITH (NOLOCK) ON AO.AgreementID = A.ID
        LEFT OUTER JOIN [CTM].[Contact] CT2 WITH (NOLOCK) ON CT2.ID = AO.FromContactID
		/* LEFT OUTER JOIN (
			SELECT PD.ReferentID,MAX(RDate) AS RDate
			FROM dbo.ICON_Payment_PaymentDetails PD WITH (NOLOCK)LEFT OUTER JOIN 
				dbo.ICON_Payment_TmpReceipt R WITH (NOLOCK) ON PD.RCReferent=R.RCReferent AND PD.TmpReceiptID=R.TmpReceiptID
			WHERE PD.PaymentType = '4'
				AND R.CancelDate IS NULL
			GROUP BY PD.ReferentID
		)R ON BK.BookingNo=R.ReferentID
		LEFT OUTER JOIN (
			SELECT [DocumentNumber],[ProductID],[UnitNumber],[Approve3Date]
			FROM [db_iconcrm_fusion].[dbo].[Z_BudgetApprove] WITH (NOLOCK)
			WHERE DocumentType IN ('Booking','BookingChangeUnit')
				AND BudgetType IN ('AdHoc1','AdHoc2')
				AND [Status] = 'Finish'
		)AdHoc ON BK.BookingNo=AdHoc.[DocumentNumber] */

WHERE BK.BookingNo = @BookingNumber;

GO
