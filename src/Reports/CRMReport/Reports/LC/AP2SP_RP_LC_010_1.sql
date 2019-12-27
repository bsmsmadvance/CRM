SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_010_1] '60008AA01430',''
--[AP2SP_RP_LC_010_1] '40017BN00520',''

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_010_1]
	@ContractNumber  nvarchar(50),
	@UserName   nvarchar(150)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SELECT *
FROM
(


SELECT	'CompanyID' = '' --PR.CompanyID
		,'CompanyName' = '' --(SELECT [dbo].[fn_GetCompanyName] (PR.CompanyID))
		,'ProductID' = '' --PR.ProjectNo
		,'ProjectName' = '' --ISNULL(PR.ProjectNo,'')+'-'+ISNULL(PR.ProjectNameTH,'') 
		,'ContactID' = '' --AO.FromContactID
		,'CustomerName' = '' /* CAST(ISNULL(AO.FromContactID,'') AS nvarchar(10))+'-'
				+CASE WHEN ISNULL((SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (AO.ContactTitleTHMasterCenterID, 'Name')),'') = '' THEN ISNULL((SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (AO.ContactTitleTHMasterCenterID, 'Name')),'') ELSE ISNULL((SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (AO.ContactTitleTHMasterCenterID, 'Name')),'') END
				+ISNULL(AO.FirstNameTH,'')+' '+ISNULL(AO.LastNameTH,'') */
		,'Telephone' = '' --ISNULL((SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone](CT.ID,0)),'')
		,'Email' = '' --ISNULL((SELECT Email FROM [dbo].[fn_GetContactEmail](CT.ID)),'')
		,'Address' =  '' /* CASE WHEN ISNULL(AO.CurrentAddress,'') = '' THEN '' 
					        ELSE ISNULL(AO.CurrentAddress,'')END+' '+ 
					   CASE WHEN ISNULL(AO.Village,'') = '' THEN ''
						    ELSE ISNULL(AO.Village,'')END+' '+ 
					   CASE WHEN ISNULL(AO.Moo,'') = '' THEN '' 
						    ELSE ISNULL('ม.'+AO.Moo,'')END+' '+
					   CASE WHEN ISNULL(AO.Soi,'') = '' THEN '' 
						    ELSE ISNULL('ซ.'+AO.Soi,'')END+' '+ 
					   CASE WHEN ISNULL(AO.Road,'') = '' THEN '' 
						    ELSE ISNULL('ถ.'+AO.Road,'')END+' '+
					   CASE	WHEN ISNULL(AO.SubDistrict,'') = '' THEN ''
							ELSE (CASE	WHEN AO.Province like '%กรุงเทพ%' THEN 'แขวง'+AO.SubDistrict
										ELSE 'ตำบล'+AO.SubDistrict END)END+'  '+
					   CASE	WHEN ISNULL(AO.District,'') = '' THEN ''
							ELSE (CASE	WHEN AO.Province like '%กรุงเทพ%' THEN 'เขต'+AO.District
										ELSE 'อำเภอ'+AO.District END) END+'  '+
					   CASE	WHEN ISNULL(AO.Province,'') = '' THEN ''
							ELSE (CASE  WHEN AO.Province like '%กรุงเทพ%' THEN AO.Province
										ELSE	'จังหวัด'+AO.Province END) +' '+ISNULL(AO.PostCode,'') END	*/	
        ,'Address2' = '' /* CASE WHEN ISNULL(CT.HouseID_4,'') = '' THEN '' 
					        ELSE ISNULL(CT.HouseID_4,'')END+' '+ 
					   CASE WHEN ISNULL(CT.Village_4,'') = '' THEN ''
						    ELSE ISNULL(CT.Village_4,'')END+' '+ 
					   CASE WHEN ISNULL(CT.Moo_4,'') = '' THEN '' 
						    ELSE ISNULL('ม.'+CT.Moo_4,'')END+' '+
					   CASE WHEN ISNULL(CT.Soi_4,'') = '' THEN '' 
						    ELSE ISNULL('ซ.'+CT.Soi_4,'')END+' '+ 
					   CASE WHEN ISNULL(CT.Road_4,'') = '' THEN '' 
						    ELSE ISNULL('ถ.'+CT.Road_4,'')END+' '+
					   CASE	WHEN ISNULL(CT.SubDistrict_4,'') = '' THEN ''
							ELSE (CASE	WHEN CT.Province_4 like '%กรุงเทพ%' THEN 'แขวง'+CT.SubDistrict_4
										ELSE 'ตำบล'+CT.SubDistrict_4 END)END+'  '+
					   CASE	WHEN ISNULL(CT.District_4,'') = '' THEN ''
							ELSE (CASE	WHEN CT.Province_4 like '%กรุงเทพ%' THEN 'เขต'+CT.District_4
										ELSE 'อำเภอ'+CT.District_4 END) END+'  '+
					   CASE	WHEN ISNULL(CT.Province_4,'') = '' THEN ''
							ELSE (CASE  WHEN CT.Province_4 like '%กรุงเทพ%' THEN CT.Province_4
										ELSE	'จังหวัด'+CT.Province_4 END) +' '+ISNULL(CT.PostalCode_4,'') END */
		,'ModelHomeThai' = '' --MM.ModelHomeThai
		,'AgreementArea' = '' --ISNULL(BK.StandardArea,0)
		,'TransferArea' = '' --ISNULL(UN.AreaFromPFB,UN.AreaFromRE)---พี่บิ๊กให้แก้ดึงมาจาก table Unit แทน โอน 2010-03-17
		,'IncreasingArea'= '' --ISNULL(TR.IncreasingArea,AG.IncreasingArea)
		,'ShortName' = '' --ISNULL(MM.TypeofRealEstate,'')+'-'+ISNULL(TY.TypeDescription,'') --ประเภท
		,'UnitNumber' = '' --UN.UnitNumber  --รหัสสินค้า
		,'DownPaymentPeriod' = '' --AG.DownPaymentPeriod
        ,'DownPaymentPerMonth' = '' --AG.DownPaymentPerMonth
		,'UnitIncreasingAreaPrice' = '' --ISNULL(TR.UnitIncreasingAreaPrice,AG.UnitIncreasingAreaPrice)
		,'AddressNumber' = '' --UN.AddressNumber --บ้านเลขที่
		,'BookingNumber' = '' --AG.BookingNumber 
        ,'ContractNumber' = '' --AG.ContractNumber 
		,'TotalSellingPrice' = '' --ISNULL(AG.SellingPrice,AG.TotalSellingPrice)
        ,'ProductType' = '' --PR.ProductType
        ,'ApproveDate' = '' --[dbo].[fn_GenRDateContract_Card](AG.ContractNumber)--เปลี่ยนวันที่ Mark flag เป็นวันที่รับเงินสัญญาแทน คุณยศบอก
        ,'Type' = '' /* CASE WHEN PR.ProductType = 'โครงการแนวราบ' THEN 'สัญญาจะซื้อจะขาย'+TE.TypeDescription
                       WHEN PR.ProductType = 'โครงการแนวสูง' THEN 'สัญญาจะซื้อจะขาย'+TE.TypeDescription END */
        ,'TransferDiscount' = '' --AG.TransferDiscount
        ,'ExtraDiscount' = '' --ISNULL(TR.ExtraDiscount,0)
        ,'CashDiscount' = '' --AG.CashDiscount
		,'TitledeedNumber' = '' --dbo.fnGenUnitTitledeedNumber(UN.ProductID,UN.UnitNumber)
		,'LandNumber' ='' -- dbo.fnGenUnitLandNumber(UN.ProductID,UN.UnitNumber)
		,'LandSurveyArea' = '' --dbo.fnGenUnitLandSurveyArea(UN.ProductID,UN.UnitNumber)
		,'Ravang' = '' --dbo.fnGenUnitRavang(UN.ProductID,UN.UnitNumber)
		,'ConStatus' = '' /* CASE	WHEN AG.Cancel = 1 THEN 'ยึด'
							WHEN AG.Cancel = 2 THEN 'เปลี่ยนห้อง'
							WHEN AG.Cancel = 4 THEN 'เปลี่ยนมือ'
							ELSE 'สัญญา Active' END */
		,'Header' = '' --Header
FROM	[SAL].[Agreement] AG  WITH (NOLOCK) --This is main table need to use below table as well
		/* LEFT OUTER JOIN [SAL].[AgreementOwner] AO WITH (NOLOCK)  ON AO.AgreementID = AG.ID AND ISNULL(AO.IsDeleted,0) = 0
		LEFT OUTER JOIN [SAL].[Booking] BK WITH (NOLOCK)  ON BK.ID = AG.BookingID 
		LEFT OUTER JOIN [PRJ].[Project] PR WITH (NOLOCK)  ON PR.ID = AG.ProjectID 
		LEFT OUTER JOIN [PRJ].[Unit] UN WITH (NOLOCK)  ON UN.ID = AG.UnitID AND UN.ProjectID = AG.ProjectID 
		LEFT OUTER JOIN [PRJ].[Model] MM WITH (NOLOCK)  ON MM.ProjectID = UN.ProjectID AND MM.ID = UN.ModelID 
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TE WITH (NOLOCK)  ON TE.ID = MM.TypeOfRealEstateID  
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TY WITH (NOLOCK)  ON TY.ID = MM.TypeOfRealEstateID 
		LEFT OUTER JOIN [SAL].[Transfer] TR WITH (NOLOCK)  ON TR.AgreementID = AG.ID
        LEFT OUTER JOIN [CTM].[Contact] CT WITH (NOLOCK)  ON CT.ID = AO.FromContactID

WHERE (AG.AgreementNo = @ContractNumber) */

UNION


SELECT	'CompanyID' = '' --PR.CompanyID
		,'CompanyName' = '' --[dbo].[fn_GetCompanyName] (PR.CompanyID)
		,'ProductID' = '' --PR.ProductID
		,'ProjectName' = '' --ISNULL(PR.ProductID,'')+'-'+ISNULL(PR.Project,'') 
		,'ContactID' = '' --BO.ContactID
		,'CustomerName' = '' /* CAST(ISNULL(BO.ContactID,'') AS nvarchar(10))+'-'
				+CASE WHEN ISNULL(BO.NamesTitleExt,'') = '' THEN ISNULL(BO.NamesTitle,'') ELSE ISNULL(BO.NamesTitleExt,'') END
				+ISNULL(BO.FirstName,'')+' '+ISNULL(BO.LastName,'') */
		,'Telephone' = '' --ISNULL(CT.Tel_4,'')
		,'Email' = '' --ct.EMail
		,'Address2' =  '' /* CASE WHEN ISNULL(BO.CurrentAddress,'') = '' THEN '' 
					        ELSE ISNULL(BO.CurrentAddress,'')END+' '+ 
					   CASE WHEN ISNULL(BO.Village,'') = '' THEN  ''
						    ELSE ISNULL(BO.Village,'')END+' '+ 
					   CASE WHEN ISNULL(BO.Moo,'') = '' THEN '' 
						    ELSE ISNULL('ม.'+BO.Moo,'')END+' '+
					   CASE WHEN ISNULL(BO.Soi,'') = '' THEN '' 
						    ELSE ISNULL('ซ.'+BO.Soi,'')END+' '+ 
					   CASE WHEN ISNULL(BO.Road,'') = '' THEN '' 
						    ELSE ISNULL('ถ.'+BO.Road,'')END+' '+
					   CASE	WHEN ISNULL(BO.SubDistrict,'') = '' THEN ''
							ELSE (CASE	WHEN BO.Province like '%กรุงเทพ%' THEN 'แขวง'+BO.SubDistrict
										ELSE 'ตำบล'+BO.SubDistrict END)END+'  '+
					   CASE	WHEN ISNULL(BO.District,'') = '' THEN ''
							ELSE (CASE	WHEN BO.Province like '%กรุงเทพ%' THEN 'เขต'+BO.District
										ELSE 'อำเภอ'+BO.District END) END+'  '+
					   CASE	WHEN ISNULL(BO.Province,'') = '' THEN ''
							ELSE (CASE  WHEN BO.Province like '%กรุงเทพ%' THEN BO.Province
										ELSE	'จังหวัด'+BO.Province END) +' '+ISNULL(BO.PostCode,'') END	*/	
        ,'Address' = '' /* CASE WHEN ISNULL(CT.HouseID_3,'') = '' THEN '' 
					        ELSE ISNULL(CT.HouseID_3,'')END+' '+ 
					   CASE WHEN ISNULL(CT.Village_3,'') = '' THEN ''
						    ELSE ISNULL(CT.Village_3,'')END+' '+ 
					   CASE WHEN ISNULL(CT.Moo_3,'') = '' THEN '' 
						    ELSE ISNULL('ม.'+CT.Moo_3,'')END+' '+
					   CASE WHEN ISNULL(CT.Soi_3,'') = '' THEN '' 
						    ELSE ISNULL('ซ.'+CT.Soi_3,'')END+' '+ 
					   CASE WHEN ISNULL(CT.Road_3,'') = '' THEN '' 
						    ELSE ISNULL('ถ.'+CT.Road_3,'')END+' '+
					   CASE	WHEN ISNULL(CT.SubDistrict_3,'') = '' THEN ''
							ELSE (CASE	WHEN CT.Province_3 like '%กรุงเทพ%' THEN 'แขวง'+CT.SubDistrict_3
										ELSE 'ตำบล'+CT.SubDistrict_3 END)END+'  '+
					   CASE	WHEN ISNULL(CT.District_3,'') = '' THEN ''
							ELSE (CASE	WHEN CT.Province_3 like '%กรุงเทพ%' THEN 'เขต'+CT.District_3
										ELSE 'อำเภอ'+CT.District_3 END) END+'  '+
					   CASE	WHEN ISNULL(CT.Province_3,'') = '' THEN ''
							ELSE (CASE  WHEN CT.Province_3 like '%กรุงเทพ%' THEN CT.Province_3
										ELSE	'จังหวัด'+CT.Province_3 END) +' '+ISNULL(CT.PostalCode_3,'') END */
		,'ModelHomeThai' = '' --MM.ModelHomeThai
		,'AgreementArea' = '' --ISNULL(BK.StandardArea,0)
		,'TransferArea' = '' --ISNULL(UN.AreaFromPFB,UN.AreaFromRE)---พี่บิ๊กให้แก้ดึงมาจาก table Unit แทน โอน 2010-03-17
		,'IncreasingArea'= 0 
		,'ShortName' = '' --ISNULL(MM.TypeofRealEstate,'')+'-'+ISNULL(TY.TypeDescription,'') --ประเภท
		,'UnitNumber' = '' --UN.UnitNumber  --รหัสสินค้า
		,0
        ,0 
		,'UnitIncreasingAreaPrice' = 0
		,'AddressNumber' = '' --UN.AddressNumber --บ้านเลขที่
		,'BookingNumber' = '' --BK.BookingNumber 
        , NULL
		,'TotalSellingPrice' = '' --ISNULL(BK.SellingPrice,BK.TotalSellingPrice)
        ,'ProductType' = '' --PR.ProductType
        ,'ApproveDate' = '' --[dbo].[fn_GenRDateContract_Card](BK.BookingNumber)--เปลี่ยนวันที่ Mark flag เป็นวันที่รับเงินสัญญาแทน คุณยศบอก
        ,'Type' = '' --CASE WHEN PR.ProductType = 'โครงการแนวราบ' THEN 'สัญญาจะซื้อจะขาย'+TE.TypeDescription
                       --WHEN PR.ProductType = 'โครงการแนวสูง' THEN 'สัญญาจะซื้อจะขาย'+TE.TypeDescription END
        ,'TransferDiscount' = '' --BK.TransferDiscount
        ,'ExtraDiscount' = 0
        ,'CashDiscount' = '' --BK.CashDiscount
		,'TitledeedNumber' = '' --dbo.fnGenUnitTitledeedNumber(UN.ProductID,UN.UnitNumber)
		,'LandNumber' = '' --dbo.fnGenUnitLandNumber(UN.ProductID,UN.UnitNumber)
		,'LandSurveyArea' = '' --dbo.fnGenUnitLandSurveyArea(UN.ProductID,UN.UnitNumber)
		,'Ravang' = '' --dbo.fnGenUnitRavang(UN.ProductID,UN.UnitNumber)
		,'ConStatus' = '' /* CASE	WHEN BK.Cancel = 1 THEN 'ยึด'
							WHEN BK.Cancel = 2 THEN 'เปลี่ยนห้อง'
							WHEN BK.Cancel = 4 THEN 'เปลี่ยนมือ'
							ELSE 'จอง Active' END */
		,'Header' = '' --Header
FROM	[SAL].[Booking] BK  WITH (NOLOCK) --This is main table need to use below table as well
		/* LEFT OUTER JOIN [SAL].[BookingOwner] BO WITH (NOLOCK)  ON BO.BookingID = BK.ID AND ISNULL(BO.IsDeleted,0) = 0 AND ISNULL(BO.IsMainOwner,0) = 1
		LEFT OUTER JOIN [PRJ].[Project] PR WITH (NOLOCK)  ON PR.ID = BK.ProjectID 
		LEFT OUTER JOIN [PRJ].[Unit] UN WITH (NOLOCK)  ON UN.ID = BK.UnitID AND UN.ProjectID = BK.ProjectID 
		LEFT OUTER JOIN [PRJ].[Model] MM WITH (NOLOCK)  ON MM.ProjectID = UN.ProjectID AND MM.ID = UN.ModelID 
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TE WITH (NOLOCK)  ON TE.ID = MM.TypeOfRealEstateID  
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TY WITH (NOLOCK)  ON TY.ID = MM.TypeOfRealEstateID 
        LEFT OUTER JOIN [CTM].[Contact] CT WITH (NOLOCK)  ON CT.ID = BO.FromContactID

WHERE (BK.BookingNo = @ContractNumber) */

) T

--ORDER BY Header DESC,ContactID ASC
      


GO
