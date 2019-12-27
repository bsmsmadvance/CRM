SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--  [AP2SP_RP_FI_006CD] NULL,'10031','RS-06B29',NULL,NULL,'Administrator Account'
--  [AP2SP_RP_FI_006CD] NULL,'10042','RT-F04',NULL,NULL,'Administrator Account'

CREATE PROCEDURE [dbo].[AP2SP_RP_FI_006CD]
	@CompanyID nvarchar(50),
    @ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,
	@DateEnd   datetime,
	@UserName nvarchar(150)
	
AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

Declare @sql nvarchar(max)
/* Set @sql = '
DECLARE @B Table(Pass money,NoPass money,BookingNumber nvarchar(50))
INSERT INTO @B
SELECT SUM(CASE WHEN Pay.IsReconcile = 1 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS B_PASS
	   ,SUM(CASE WHEN Pay.IsReconcile = 0 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS B_NOPASS
	   ,Pay.BookingNumber
FROM (
SELECT CASE WHEN TR.ReconcileDate IS NOT NULL OR TR.Method = ''10'' OR TR.DepositID IS NOT NULL THEN 1 ELSE 0 END AS IsReconcile
	   ,PD.Amount
	   ,PD.PaymentType
	   ,PD.Period,PD.RCReferent,B.BookingNumber
FROM Icon_Payment_PaymentDetails PD
	LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
	LEFT OUTER JOIN ICON_EntForms_Booking B ON PD.ReferentID =  B.BookingNumber
WHERE PD.PaymentType = ''4'' AND B.CancelDate IS NULL AND TR.CancelDate IS NULL AND YEAR(TR.RDate) = 2009 ' 
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND(TR.ProductID = '''+@ProductID+''')' 
IF(ISNULL(@UnitNumber,'')<>'')Set @sql = @sql+' AND(TR.UnitNumber =  '''+@UnitNumber+''')' 
Set @sql = @sql+')AS Pay
GROUP BY Pay.BookingNumber'

Set @sql = @sql+'
DECLARE @T1 Table(Pass money,NoPass money,ContractNumber nvarchar(50))
INSERT INTO @T1
SELECT SUM(CASE WHEN Pay.IsReconcile = 1 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS T_PASS
	   ,SUM(CASE WHEN Pay.IsReconcile = 0 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS T_NOPASS
	   ,Pay.ContractNumber
FROM (
SELECT  CASE WHEN TR.ReconcileDate IS NOT NULL OR TR.Method = ''10'' OR TR.DepositID IS NOT NULL THEN 1 ELSE 0 END AS IsReconcile
	,PD.Amount
	,PD.PaymentType
	,PD.Period,PD.RCReferent,A.ContractNumber
FROM Icon_Payment_PaymentDetails PD
	 LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
	 LEFT OUTER JOIN ICON_EntForms_Agreement A ON PD.ReferentID =  A.ContractNumber
     INNER JOIN Icon_EntForms_PaymentDetail PS ON PS.ServiceCode = PD.PaymentType AND ISNULL(PS.Payment,0) IN (''0'',''4'',''6'')
WHERE PD.PaymentType NOT LIKE ''TR%'' AND PD.PaymentType NOT IN(''A08'')
      AND A.CancelDate IS NULL AND TR.CancelDate IS NULL AND YEAR(TR.RDate) = 2009 '
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND(TR.ProductID = '''+@ProductID+''')' 
IF(ISNULL(@UnitNumber,'')<>'')Set @sql = @sql+' AND(TR.UnitNumber = '''+@UnitNumber+''')' 
Set @sql = @sql+')AS Pay
GROUP BY Pay.ContractNumber'

Set @sql = @sql+'
DECLARE @9P Table(Pass money,NoPass money,ContractNumber nvarchar(50))
INSERT INTO @9P
SELECT DISTINCT SUM(CASE WHEN Pay.IsReconcile = 1 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS T_PASS
	,SUM(CASE WHEN Pay.IsReconcile = 0 THEN ISNULL(Pay.Amount,0) ELSE 0 END) AS T_NOPASS
	,Pay.ContractNumber
FROM (
SELECT DISTINCT CASE WHEN TR.ReconcileDate IS NOT NULL OR TR.Method = ''10'' OR TR.DepositID IS NOT NULL THEN 1 ELSE 0 END AS IsReconcile
	,PD.Amount
	,PD.PaymentType
	,PD.Period,PD.RCReferent,A.ContractNumber
FROM Icon_Payment_PaymentDetails PD
	LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
	LEFT OUTER JOIN ICON_EntForms_Agreement A ON PD.ReferentID =  A.ContractNumber
WHERE PD.PaymentType IN (''9P'') AND A.CancelDate IS NULL AND TR.CancelDate IS NULL AND YEAR(TR.RDate) =  2009 ' 
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND(TR.ProductID = '''+@ProductID+''')' 
IF(ISNULL(@UnitNumber,'')<>'')Set @sql = @sql+' AND(TR.UnitNumber = '''+@UnitNumber+''')' 
Set @sql = @sql+')AS Pay
GROUP BY Pay.ContractNumber' */


--SET @sql = @sql+' --For actual mapping need to use this one below one is for mapping only!!!
Set @sql = '
SELECT ''TransferNumber'' = '''' --TN.TransferNumber
       ,''CompanyName'' = '''' --CM.CompanyNameThai  --ผู้ขาย
	   , ''TaxID'' = '''' --CM.TaxID
	   , ''AddressCompany'' = '''' /* CASE WHEN CM.AddressThai = '''' THEN ''''
						           ELSE ISNULL(CM.AddressThai,'''') END+ '' '' +
					          CASE WHEN CM.BuildingThai = '''' THEN ''''
							       ELSE ISNULL(CM.BuildingThai,'''') END+ '' '' +
							  CASE WHEN CM.SoiThai = '''' THEN ''''
								   ELSE ISNULL(''ซ.''+CM.SoiThai,'''')END+ '' '' +
						      CASE WHEN CM.RoadThai = '''' THEN ''''
								   ELSE ISNULL(''ถ.''+CM.RoadThai,'''')END+'' ''+
					          CASE WHEN CM.SubDistrictThai = '''' THEN ''''
						           ELSE (CASE WHEN CM.ProvinceThai like ''%กรุงเทพ%'' THEN
										           ''แขวง''+CM.SubDistrictThai
									          ELSE ''ตำบล''+CM.SubDistrictThai END) END+'' ''+       
	                          CASE WHEN CM.DistrictThai = '''' THEN ''''
							       ELSE (CASE WHEN CM.ProvinceThai like ''%กรุงเทพ%'' THEN
											       ''เขต''+CM.DistrictThai
									          ELSE ''อำเภอ''+CM.DistrictThai END) END+''  ''+
					          CASE WHEN CM.ProvinceThai ='''' THEN ''''
							       ELSE (CASE WHEN CM.ProvinceThai like ''%กรุงเทพ%'' THEN
											       CM.ProvinceThai
									          ELSE ''จังหวัด''+CM.ProvinceThai END) +'' ''+
							                       ISNULL(CM.PostCode,'''') END */
        , ''Telephone'' = '''' --CM.Telephone
		, ''TransferName'' = '''' --[dbo].[fn_GenCustTransferAllName](TF.TransferNumber)
	    , ''PersonCardID'' = '''' --[dbo].[fn_GetPersonCardIDAll](TN.TransferNumber)
		, ''CurrentAddress'' = '''' --[dbo].[fn_GetAddNoAll](TF.TransferNumber)
	    , ''Moo'' = '''' --[dbo].[fn_GetMooALL](TF.TransferNumber)
        , ''Village'' = '''' --[dbo].[fn_GetVillageAll](TF.TransferNumber)
        , ''Soi'' = '''' --[dbo].[fn_GetSoiAll] (TF.TransferNumber)
        , ''Road'' = '''' --[dbo].[fn_GetRoadAll] (TF.TransferNumber)
        , ''SubDistrict'' = '''' --[dbo].[fn_GetSubdistrictAll] (TF.TransferNumber)
        , ''District'' = '''' --[dbo].[fn_GetDistrictAll] (TF.TransferNumber)
        , ''Province_Code'' = '''' --[dbo].[fn_GetProvince_CodeAll] (TF.TransferNumber)
        , ''TelephoneCus'' = '''' --ISNULL(TN.Phone,'''') + ''  ''+ ISNULL(TN.Mobile,'''')
        , ''TypeHome'' = '''' --TE.TypeDescription
        , ''ContractNumber'' = '''' --CASE WHEN A.MA_RUNNO IS NOT NULL THEN Substring(A.ContractNumber,8,20)
                                    --ElSE A.ContractNumber END
        , ''ContractDate'' = '''' --A.ContractDate
        , ''TitledeedNumber'' = '''' --[dbo].[fnGenProductTitledeedNumber](A.ProductID)--เลขที่โฉนด
        , ''AddressNumber'' = '''' --U.AddressNumber --ห้องชุดเลขที่
        , ''FloorID'' = '''' --U.FloorID--ชั้นที่
        , ''TowerName'' = '''' --TW.TowerName--อาคารเลขที่
        , ''LandSubDistrict'' = '''' --ISNULL(PA.SubDistrict,''-'')
        , ''LandDistrict'' = '''' --ISNULL(PA.District,''-'')
        , ''LandProvince'' = '''' --ISNULL(PA.Province,''-'')
        , ''TowerTitledeedName''= '''' --P.Project    --ชื่ออาคารชุด
        , ''TowerTitledeedNumber'' = '''' --TW.TowerTitledeedNumber --ทะเบียนอาคารชุดเลขที่
	    , ''AddressProduct'' =  '''' /* CASE WHEN PA.Road = '''' THEN ''''
							       ELSE ISNULL(''ถ.''+PA.Road,'''')END+'' ''+
					          CASE WHEN PA.SubDistrict = '''' THEN ''''
						           ELSE (CASE WHEN PA.Province like ''%กรุงเทพ%'' THEN
										           ''แขวง''+PA.SubDistrict
									          ELSE ''ตำบล''+PA.SubDistrict END) END+'' ''+       
	                          CASE WHEN PA.District = '''' THEN ''''
							       ELSE (CASE WHEN PA.Province like ''%กรุงเทพ%'' THEN
											       ''เขต''+PA.District
									          ELSE ''อำเภอ''+PA.District END) END+''  ''+
					          CASE WHEN PA.Province = '''' THEN ''''
							       ELSE (CASE WHEN PA.Province like ''%กรุงเทพ%'' THEN
											       PA.Province
									          ELSE ''จังหวัด''+PA.Province END) END */ '
Set @sql = @sql+'
         , ''TransferDate''= '''' --TF.TransferDateApprove
	     , ''NetSalePrice'' = '''' --TF.NetSalePrice
         , ''NetSalePriceText'' = '''' --dbo.fnBHT_BahtText(ISNULL(TB.pass,0)+ISNULL(TA.pass,0))
         , ''AddressCus1'' =  '''' /* CASE WHEN CT.HouseID_4 = '''' THEN ''''
						           ELSE ISNULL(CT.HouseID_4,'''') END+ '' '' +
					          CASE WHEN CT.Village_4 = '''' THEN ''''
							       ELSE ISNULL(CT.Village_4,'''') END+ '' '' +
                              CASE WHEN CT.Moo_4 = '''' THEN ''''
                                   ELSE ISNULL(''หมู่''+ISNULL(CT.Moo_4,''''),'''')END+'' ''+
							  CASE WHEN CT.Soi_4 = '''' THEN ''''
								   ELSE ISNULL(''ซ.''+ISNULL(CT.Soi_4,''''),'''')END+ '' '' +
						      CASE WHEN CT.Road_4 = '''' THEN ''''
								   ELSE ISNULL(''ถ.''+ISNULL(CT.Road_4,''''),'''')END+'' ''+
					          CASE WHEN CT.SubDistrict_4 = '''' THEN ''''
						           ELSE (CASE WHEN CT.Province_4 like ''%กรุงเทพ%'' THEN
										          ''แขวง''+ISNULL(CT.SubDistrict_4,'''')
									         ELSE ''ตำบล''+ISNULL(CT.SubDistrict_4,'''') END) END */                        
         , ''AddressCus2'' =   '''' /* CASE WHEN CT.District_4 = '''' OR CT.District_4 IS NULL THEN ''''
                                    ELSE (CASE WHEN CT.Province_4 like ''%กรุงเทพ%''THEN
                                                    ''เขต''+ISNULL(CT.District_4,'''')
                                               ELSE ''อำเภอ''+ISNULL(CT.District_4,'''') END) END+'' ''+
                               CASE WHEN CT.Province_4 = '''' THEN ''''
                                    ELSE (CASE WHEN CT.Province_4 like ''%กรุงเทพ%'' THEN
                                               ISNULL(CT.Province_4,'''')
                                               ELSE ''จังหวัด''+ISNULL(CT.Province_4,'''') END) +'' ''+
                               ISNULL(CT.PostalCode_4,'''') END */
		, ''UnitNumber'' = '''' --A.UnitNumber
		,''PayAll'' = '''' --ISNULL(TB.pass,0)+ISNULL(TA.pass,0)
FROM  [SAL].[Booking] B' --This is temp table acutal table start from below
    /* [ICON_EntForms_Transfer] TF 
      LEFT OUTER JOIN [ICON_EntForms_Agreement] A ON A.ContractNumber = TF.ContractNumber 
      LEFT OUTER JOIN [ICON_EntForms_TransferOwner] TN ON TN.TransferNumber = TF.TransferNumber AND TN.ID = 1
      LEFT OUTER JOIN [ICON_EntForms_Contacts] CT ON CT.ContactID = TN.ContactID
      LEFT OUTER JOIN [ICON_EntForms_Unit] U ON U.UnitNumber = A.UnitNumber AND U.ProductID = A.ProductID 
	  LEFT OUTER JOIN [ICON_EntForms_Products] P ON P.ProductID = U.ProductID  AND P.ProductID = A.ProductID 
      LEFT OUTER JOIN [ICON_EntForms_ManageModel] MM ON MM.ProductID =U.ProductID AND MM.ModelID = U.ModelID 
	  LEFT OUTER JOIN [ICON_EntForms_TypeOfRealEstate] TE ON TE.TypeID = MM.TypeOfRealEstate 
	  LEFT OUTER JOIN [ICON_EntForms_Tower] TW ON TW.ProductID = U.ProductID AND TW.TowerID = U.TowerID 
	  LEFT OUTER JOIN [ICON_EntForms_Company] CM ON CM.CompanyID = P.CompanyID  
      LEFT OUTER JOIN [ICON_EntForms_ProductsAddress] PA ON PA.ProductID = P.ProductID AND PA.AddressFlag IN(''0'',''1'')
	  LEFT OUTER JOIN @B TB ON A.BookingNumber = TB.BookingNumber 
	  LEFT OUTER JOIN @T1 TA ON A.ContractNumber = TA.ContractNumber
      LEFT OUTER JOIN @9P TP ON A.ContractNumber = TP.ContractNumber

WHERE 1=1 
	AND P.ProductType = ''โครงการแนวสูง''
	'

IF(ISNULL(@CompanyID,'')<>'') Set @sql = @sql+' AND (P.CompanyID = '''+@CompanyID+''')' 
IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND (P.ProductID = '''+@ProductID+''')' 
IF(ISNULL(@UnitNumber,'')<>'')Set @sql = @sql+' AND (U.UnitNumber = '''+@UnitNumber+''')' 
IF(ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'' AND YEAR(@DateStart)<>1800 AND YEAR(@DateEnd)<>7000)

Set @sql = @sql+' AND (TF.TransferDateApprove BETWEEN '''+Convert(nvarchar(10),@DateStart,120)+''' AND '''+Convert(nvarchar(10),@DateEndInStore,120)+''')'
Set @sql = @sql+' ORDER BY TF.TransferDateApprove,P.ProductID,U.UnitNumber ' */

--PRINT(@sql)
EXEC(@sql)



























GO
