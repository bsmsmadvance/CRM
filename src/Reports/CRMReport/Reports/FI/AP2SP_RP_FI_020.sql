SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--หนังสือรับรองจำนวนเงินที่ชำระค่าซื้ออสังหาริมทรัพย์
--ตามพระราชกฤษฎีกา ออกตามความในประมวลรัษฎากร
--ว่าด้วยการยกเว้นรัษฎากร (ฉบับที่ ๕๒๘) พ.ศ. ๒๕๕๔
--เงื่อนไขในการออกรายงานคือ ลูกค้าที่โอนตั้งแต่ 21 กันยายน 2554 – 31 ธันวาคม 2555 , มูลค่าสัญญาสุทธิไม่เกิน 5 ล้านบาท

--[AP2SP_RP_FI_020] '','','',NULL,NULL,NULL
--[AP2SP_RP_FI_020] '','10108','','2011-01-01','2011-12-31',NULL
--[AP2SP_RP_FI_020] '','10108','A02,A01','2011-10-01','2011-10-31',NULL
--[AP2SP_RP_FI_020] '','10108','','','2011-12-31',NULL
--[AP2SP_RP_FI_020] '','10097','',NULL,NULL,NULL
--[AP2SP_RP_FI_020] '','10150','c09',NULL,NULL,'Administrator Account'
CREATE PROCEDURE [dbo].[AP2SP_RP_FI_020]
	@CompanyID nvarchar(50) = '',
    @ProductID nvarchar(50) = '',
	@UnitNumber nvarchar(500)= '',
	@DateStart datetime,
	@DateEnd   datetime,
	@UserName nvarchar(150)
AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)


SELECT 'CompanyName' = '' --dbo.fn_GetCompanyNameTH(P.CompanyID, TF.TransferDateApprove)  --ผู้ขาย
	   , 'TaxID' = '' --CM.TaxID
	   , 'AddressCompany' =  '' /* CASE WHEN CM.AddressThai = '' THEN ''
						          ELSE ISNULL(CM.AddressThai,'') END+ ' ' +
					         CASE WHEN CM.BuildingThai = '' THEN ''
							      ELSE ISNULL(CM.BuildingThai,'') END+ ' ' +
							 CASE WHEN CM.SoiThai = '' THEN ''
								  ELSE ISNULL('ซ.'+CM.SoiThai,'')END+ ' ' +
						     CASE WHEN CM.RoadThai = '' THEN ''
								  ELSE ISNULL('ถ.'+CM.RoadThai,'')END+' '+
					         CASE WHEN CM.SubDistrictThai = '' THEN ''
						          ELSE (CASE WHEN CM.ProvinceThai like '%กรุงเทพ%' THEN
										          'แขวง'+CM.SubDistrictThai
									         ELSE 'ตำบล'+CM.SubDistrictThai END) END+' '+       
	                         CASE WHEN CM.DistrictThai = '' THEN ''
							      ELSE (CASE WHEN CM.ProvinceThai like '%กรุงเทพ%' THEN
											      'เขต'+CM.DistrictThai
									         ELSE 'อำเภอ'+CM.DistrictThai END) END+'  '+
					         CASE WHEN CM.ProvinceThai = '' THEN ''
							      ELSE (CASE WHEN CM.ProvinceThai like '%กรุงเทพ%' THEN
											      CM.ProvinceThai
									         ELSE 'จังหวัด'+CM.ProvinceThai END) +' ' + ISNULL(CM.PostCode,'') END */
        , 'Telephone' = '' --CM.Telephone
		, 'TransferNumber' = '' --N.TransferNumber
		, 'TransferName' = '' --[dbo].[fn_GenCustTransferAllName] (TN.TransferNumber)
	    , 'PersonCardID' = '' --[dbo].[fn_GetPersonCardIDAll](TN.TransferNumber)
		, 'CurrentAddress' = '' --[dbo].[fn_GetAddNoAll](TF.TransferNumber)
	    , 'Moo' = '' --[dbo].[fn_GetMooALL](TF.TransferNumber)
		, 'Village' = '' --[dbo].[fn_GetVillageAll](TF.TransferNumber)
		, 'Soi' = '' --[dbo].[fn_GetSoiAll] (TF.TransferNumber)
		, 'Road' = '' --[dbo].[fn_GetRoadAll] (TF.TransferNumber)
		, 'SubDistrict' = '' --[dbo].[fn_GetSubdistrictAll] (TF.TransferNumber)
		, 'District' = '' --[dbo].[fn_GetDistrictAll] (TF.TransferNumber)
		, 'Province_Code' = '' --[dbo].[fn_GetProvince_CodeAll] (TF.TransferNumber)
        , 'TelephoneCus' = '' --ISNULL(TN.Phone,'') + '  '+ ISNULL(TN.Mobile,'')
		, 'TransferCapacity' = '' --[dbo].[fn_GenCustTransferCapacity](TN.TransferNumber)
        , 'TypeHome' = '' --TE.TypeDescription
        , 'ContractNumber' = '' --CASE WHEN A.MA_RUNNO IS NOT NULL THEN Substring(A.ContractNumber,8,20)
                                  --ElSE A.ContractNumber END
		, 'BookingDate' = '' --B.BookingDate
        , 'ContractDate' = '' --A.ContractDate
        , 'TitledeedNumber' = '' --[dbo].[fnGenUnitTitledeedNumber](A.ProductID,A.UnitNumber)  --เลขที่โฉนด
        , 'LandNumber' = '' --[dbo].[fnGenUnitLandNumber] (A.ProductID,A.UnitNumber)--เลขที่ดิน
        , 'LandSurveyArea' = '' --[dbo].[fnGenUnitLandSurveyArea]( A.ProductID,A.UnitNumber) --หน้าสำรวจ
        , 'LandSubDistrict' = '' --ISNULL(PA2.SubDistrict,ISNULL(PA.SubDistrict,'-'))
        , 'LandDistrict' = '' --ISNULL(PA2.District,ISNULL(PA.District,'-'))
        , 'LandProvince' = '' --ISNULL(PA2.Province,ISNULL(PA.Province,'-'))
        , 'Project' = '' --P.Project 
        , 'TitledeedNumberTower' = '' --[dbo].[fnGenProductTitledeedNumber](A.ProductID)--เลขที่โฉนด
        , 'AddressNumber' = '' --U.AddressNumber --ห้องชุดเลขที่
        , 'FloorID' = '' --U.FloorID--ชั้นที่
        , 'TowerName' = '' --TW.TowerName--อาคารเลขที่
        , 'TowerTitledeedName' = '' --P.Project   --ชื่ออาคารชุด
        , 'TowerTitledeedNumber' = '' --TW.TowerTitledeedNumber --ทะเบียนอาคารชุดเลขที่

	    , 'AddressProduct' = '' /* CASE WHEN PA3.Soi = '' THEN ''
								    ELSE ISNULL('ซ.'+PA3.Soi,'')END+ '  ' + 
                               CASE WHEN PA3.Road = '' THEN ''
							        ELSE ISNULL('ถ.'+PA3.Road,'')END+' '+
					           CASE WHEN PA3.SubDistrict = '' THEN ''
						            ELSE (CASE WHEN PA3.Province like '%กรุงเทพ%' THEN
										           'แขวง'+PA3.SubDistrict
									          ELSE 'ตำบล'+PA3.SubDistrict END) END+' '+       
	                          CASE WHEN PA3.District = '' THEN ''
							       ELSE (CASE WHEN PA3.Province like '%กรุงเทพ%' THEN
											       'เขต'+PA3.District
									          ELSE 'อำเภอ'+PA3.District END) END+'  '+
					          CASE WHEN PA3.Province = '' THEN ''
							       ELSE (CASE WHEN PA3.Province like '%กรุงเทพ%' THEN
											       PA3.Province
									          ELSE 'จังหวัด'+PA3.Province END) END */
		
         , 'TransferDate' = '' --ISNULL(TF.TransferDateApprove,TF.TransferDate)
	     , 'NetSalePrice' = '' --TF.NetSalePrice
         , 'NetSalePriceText' = '' --dbo.fnBHT_BahtText(ISNULL(TF.NetSalePrice,0))
         , 'AddressCus1' =  '' /* CASE WHEN CT.HouseID_4 = '' THEN ''
						           ELSE ISNULL(CT.HouseID_4,'') END+ ' ' +
					          CASE WHEN CT.Village_4 = '' THEN ''
							       ELSE ISNULL(CT.Village_4,'') END+ ' ' +
                              CASE WHEN CT.Moo_4 = '' THEN ''
                                   ELSE ISNULL('หมู่'+CT.Moo_4,'')END+' '+
							  CASE WHEN CT.Soi_4 = '' THEN ''
								   ELSE ISNULL('ซ.'+CT.Soi_4,'')END+ ' ' +
						      CASE WHEN CT.Road_4 = '' THEN ''
								   ELSE ISNULL('ถ.'+CT.Road_4,'')END+' '+
					          CASE WHEN CT.SubDistrict_4 = '' THEN ''
						           ELSE (CASE WHEN CT.Province_4 like '%กรุงเทพ%' THEN
										          'แขวง'+CT.SubDistrict_4
									         ELSE 'ตำบล'+CT.SubDistrict_4 END) END */	                         
               
         , 'AddressCus2' =  '' /* CASE WHEN CT.District_4 = '' OR CT.District_4 IS NULL THEN ''
                                    ELSE (CASE WHEN CT.Province_4 like '%กรุงเทพ%' THEN
                                                    'เขต'+CT.District_4
                                               ELSE 'อำเภอ'+CT.District_4 END) END+' '+
                               CASE WHEN CT.Province_4 = '' THEN ''
                                    ELSE (CASE WHEN CT.Province_4 like '%กรุงเทพ%' THEN
                                               CT.Province_4
                                               ELSE 'จังหวัด'+CT.Province_4 END) +' '+
                               ISNULL(CT.PostalCode_4,'') END */
		, 'ProductId' = '' --A.ProductId
		, 'UnitNumber' = '' --A.UnitNumber


FROM  [SAL].[Booking] B --This is temp table actual table start from below
    /* [ICON_EntForms_Transfer] TF 
      LEFT OUTER JOIN [ICON_EntForms_Agreement] A ON A.ContractNumber = TF.ContractNumber 
      LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AO.ContractNumber = A.ContractNumber AND AO.Header = 1 
      LEFT OUTER JOIN [ICON_EntForms_TransferOwner] TN ON TN.TransferNumber = TF.TransferNumber AND TN.ID = 1
	  LEFT OUTER JOIN [ICON_EntForms_Booking] B ON A.BookingNumber = B.BookingNumber 
      LEFT OUTER JOIN [ICON_EntForms_Contacts] CT ON CT.ContactID = TN.ContactID
      LEFT OUTER JOIN [ICON_EntForms_Unit] U ON U.UnitNumber = A.UnitNumber AND U.ProductID = A.ProductID 
	  LEFT OUTER JOIN [ICON_EntForms_Products] P ON P.ProductID = U.ProductID  AND P.ProductID = A.ProductID 
      LEFT OUTER JOIN [ICON_EntForms_ManageModel] MM ON MM.ProductID =U.ProductID AND MM.ModelID = U.ModelID 
	  LEFT OUTER JOIN [ICON_EntForms_TypeOfRealEstate] TE ON TE.TypeID = MM.TypeOfRealEstate 
	  LEFT OUTER JOIN [ICON_EntForms_Company] CM ON CM.CompanyID = P.CompanyID  
      LEFT OUTER JOIN [ICON_EntForms_ProductsAddress] PA ON PA.ProductID = P.ProductID AND PA.AddressFlag IN ('0','2')
	  LEFT OUTER JOIN [ICON_EntForms_ProductsAddress] PA3 ON PA3.ProductID = P.ProductID AND PA3.AddressFlag IN ('0','1')
	  LEFT OUTER JOIN [ICON_EntForms_Tower] TW ON TW.ProductID = U.ProductID AND TW.TowerID = U.TowerID 
	  LEFT OUTER JOIN 
	  (
			SELECT TOP 1 *
			FROM [ICON_EntForms_ProductsAddress] 
			WHERE (ProductID = @ProductID)
				AND (AddressFlag = '3')
			ORDER BY [No] DESC
	  ) PA2 ON PA.ProductID = P.ProductID

WHERE 1=1 
	AND (TF.NetSalePrice <= 3000000) 
	AND (dbo.fn_ClearTime(TF.TransferDateApprove) BETWEEN '2015-10-13' AND '2016-12-31') 
	
AND (@CompanyID= '' OR @CompanyID IS NULL OR P.CompanyID = @CompanyID)
AND (@ProductID= '' OR @ProductID IS NULL OR P.ProductID = @ProductID)
AND (@UnitNumber= '' OR @UnitNumber IS NULL OR U.UnitNumber IN (SELECT Val FROM dbo.[fn_SplitString](@UnitNumber,',')))
AND ((@DateStart IS NULL AND @DateEndInStore IS NULL) OR (@DateStart = '' AND @DateEnd = '') OR TF.TransferDateApprove BETWEEN @DateStart AND @DateEndInStore)


ORDER BY TF.TransferDateApprove,P.ProductID,U.UnitNumber; */



GO
