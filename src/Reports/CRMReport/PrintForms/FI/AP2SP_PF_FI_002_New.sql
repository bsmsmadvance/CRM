SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[dbo].[AP2SP_PF_FI_002_New] '6002220180001',''
--[dbo].[AP2SP_PF_FI_002_New] '1004520102476',''
--[dbo].[AP2SP_PF_FI_002_New] '100522009254387',''
--[dbo].[AP2SP_PF_FI_002_New] '00001,1004220090020,1004820090155,1004820090156,1004820090158,1004820090162,1004820090167,1004820090168,1004820090171,1004820090172,1004820090174,1004820090175,1004820090176','882'

CREATE PROC [dbo].[AP2SP_PF_FI_002_New]

	@ReceivedID nvarchar(4000), --เลขที่ใบเสร็จรับเงิน
	@UserID nvarchar(50)

AS

/* DECLARE @TableAddress Table	(	
								ContractNumber varchar(30),ContactID varchar(30),CustomerName varchar(500),CurrentAddress varchar(500),Moo varchar(100)
								,Village varchar(500),Soi varchar(500),Road varchar(500),SubDistrict varchar(500),District varchar(500)
								,Province varchar(500),PostCode varchar(100),ProductID varchar(100),UnitNumber varchar(30),ReceivedID varchar(50),RefType varchar(2)
								,BookingName nvarchar(500),AgreementName nvarchar(500),Tel varchar(100)
							)
INSERT INTO @TableAddress	(
								ContractNumber,ContactID,CustomerName,CurrentAddress,Moo,Village,Soi,Road,SubDistrict
								,District,Province,PostCode,ProductID,UnitNumber,ReceivedID,RefType,BookingName,AgreementName
							)
SELECT	ContractNumber = CS.ContractNumber
		,ContactID = CT.ContactID
		,CustomerName = CASE WHEN CT.Title_2 = '' THEN ISNULL(CT.Title_1,'') ELSE ISNULL(ISNULL(CT.Title_2,CT.Title_1),'')  END+ISNULL(FirstName,'')+' '+ISNULL(LastName,'')
		,CurrentAddress = CT.HouseID_4
		,Moo = CT.Moo_4
		,Village = CT.Village_4
		,Soi = CT.Soi_4
		,Road = CT.Road_4
		,SubDistrict = CT.SubDistrict_4
		,District = CT.District_4
		,Province = CT.Province_4
		,PostCode = CT.PostalCode_4
		,ProductID = CS.ProductID
		,UnitNumber = CS.UnitNumber
		,ReceivedID = CS.ReceivedID
		,RefType = CS.RefType
		,BookingName=dbo.fn_GenCustBookingAllNameRC(CS.ContractNumber)
		,AgreementName=dbo.fn_GenCustAgreementAllNameRC(CS.ContractNumber)
FROM	[ICON_EntForms_Contacts] CT
INNER JOIN
	(
	SELECT		Pay.ReferentID AS ContractNumber
			, CASE	WHEN Pay.RefType = 1 THEN BO.ContactID 
					ELSE AO.ContactID END AS ContactID
			,Pay.ProductID, Pay.UnitNumber,Pay.ReceivedID, Pay.RefType
	FROM         (	SELECT	DISTINCT RC.ReceivedID, TR.RefType, TR.ReferentID, TR.ProductID, TR.UnitNumber
					FROM    dbo.ICON_Payment_Received AS RC LEFT OUTER JOIN
							dbo.ICON_Payment_TmpReceipt AS TR ON RC.RCReferent = TR.RCReferent AND TR.CancelDate IS NULL
					GROUP BY RC.ReceivedID, TR.RefType, TR.ReferentID, TR.ProductID, TR.UnitNumber) AS Pay 
							LEFT OUTER JOIN
							dbo.ICON_EntForms_AgreementOwner AS AO ON AO.ContractNumber = Pay.ReferentID AND AO.Header = 1 AND ISNULL(AO.IsDelete, 0) = 0 LEFT OUTER JOIN
							dbo.ICON_EntForms_BookingOwner AS BO ON BO.BookingNumber = Pay.ReferentID AND BO.Header = 1 AND ISNULL(BO.IsDelete, 0) = 0 
	WHERE Pay.ReceivedID IN (SELECT * FROM [dbo].[fn_SplitString](@ReceivedID,','))
	)CS  ON CS.ContactID = CT.ContactID
ORDER BY CS.ReceivedID


-- ถ้าไม่มีข้อมูลให้ Load จากการรับชำระเงินอื่นๆ
if(Not Exists(Select * From @TableAddress))
Begin
INSERT INTO @TableAddress	(
								ContractNumber,ContactID,CustomerName,CurrentAddress,Moo,Village,Soi,Road,SubDistrict
								,District,Province,PostCode,ProductID,UnitNumber,ReceivedID,RefType,Tel
							)
	SELECT	ContractNumber = ''
			,ContactID = ''
			,CustomerName = DisplayName
			,CurrentAddress = Address
			,Moo = ''
			,Village = ''
			,Soi = ''
			,Road = Road
			,SubDistrict = SubDistrict
			,District = District
			,Province = Province
			,PostCode = PostCode
			,ProductID = ProductID
			,UnitNumber = ''
			,ReceivedID = ReceivedID
			,RefType = ''
			,Tel = Case When Isnull(Mobile,'')<>'' Then Mobile Else Isnull(Phone,'') End
	From vw_ICON_Payment_HousePayment cs
	Where ReceivedID In(SELECT * FROM [dbo].[fn_SplitString](@ReceivedID,','))
End */


---ข้อมูลลูกค้า---
SELECT	'CompanyNameThai' = '' --dbo.fn_GetCompanyNameTH(CO.CompanyID,Pay.RDate),--CO.CompanyNameThai,
		,'CompanyNameEng' = '' --dbo.fn_GetCompanyNameEN(CO.CompanyID,Pay.RDate),--CO.CompanyNameEng,
		,'AddressThai' = '' /* (CO.AddressThai 
				+ Case When Isnull(CO.BuildingThai,'')=''Then ''
				Else ' '+Isnull(CO.BuildingThai,'') End 
				+ Case When Isnull(CO.RoadThai,'')=''Then ''
				Else ' ถ.'+Isnull(CO.RoadThai,'') End 
				+ Case When Isnull(CO.SubDistrictThai,'')=''Then ''
				Else ' แขวง'+Isnull(CO.SubDistrictThai,'') End 
				+ Case When Isnull(CO.DistrictThai,'')=''Then ''
				Else ' เขต'+Isnull(CO.DistrictThai,'') End 
				+ Case When Isnull(CO.ProvinceThai,'')=''Then ''
				Else ' จ.'+Isnull(CO.ProvinceThai,'') End 
				+ Case When Isnull(CO.PostCode,'')=''Then ''
				Else ' '+Isnull(CO.PostCode,'') End 
				)AddressThai */
		,'AddressEng' = '' /* (CO.AddressEng 
				+ Case When Isnull(CO.BuildingEng,'')=''Then ''
				Else ' '+Isnull(CO.BuildingEng,'') End 
				+ Case When Isnull(CO.RoadEng,'')=''Then ''
				Else ' '+Isnull(CO.RoadEng,'')+' Rd.' End 
				+ Case When Isnull(CO.SubDistrictEng,'')=''Then ''
				Else ' ,'+Isnull(CO.SubDistrictEng,'') End 
				+ Case When Isnull(CO.DistrictEng,'')=''Then ''
				Else ' ,'+Isnull(CO.DistrictEng,'') End 
				+ Case When Isnull(CO.ProvinceEng,'')=''Then ''
				Else ' ,'+Isnull(CO.ProvinceEng,'') End 
				+ Case When Isnull(CO.PostCode,'')=''Then ''
				Else ' '+Isnull(CO.PostCode,'') End 
				)AddressEng */
		,'Tel' = '' /* (Case When Isnull(CO.Telephone,'')=''Then ''
			Else 'Tel.'+Isnull(CO.Telephone,'') End 
			+ Case When Isnull(CO.Fax,'')=''Then ''
			Else ' Fax.'+Isnull(CO.Fax,'') End 
			+ Case When Isnull(CO.TaxID,'')=''Then ''
			Else ' Tax ID. '+Isnull(TaxID,'') End)Tel */
		,'CompanyID' = '' --CO.CompanyID
		,'PersonPayForTax' = '' --CO.TaxID
		,'ReceivedID' = '' --Pay.ReceivedID
        ,'ReceiveDate' = '' --Pay.ReceiveDate
        ,'PrintingID' = '' --Pay.PrintingID
        ,'RCReferent' = '' --Pay.RCReferent
		,'ProductID' = '' --PR.Project
		,'UnitNumber' = '' --Pay.UnitNumber
		,'RDate' = '' --Pay.RDate
		,'AmouontInReceived' = '' --Isnull((select sum(Amount)From [vw_RPTAP2_New_ReceiptDetail] v where v.ReceivedID=Pay.ReceivedID and v.RCReferent=Pay.RCReferent and v.PaymentType<>'9P'),0)--Pay.RAmount
		,'AmountText' = '' --[dbo].[fnBHT_BahtText](Isnull((select sum(Amount)From [vw_RPTAP2_New_ReceiptDetail] v where v.ReceivedID=Pay.ReceivedID and v.RCReferent=Pay.RCReferent and v.PaymentType<>'9P'),0))--(Pay.RAmount)	
		,'AmountTextEng' = '' --[dbo].Currency_ToWords(Isnull((select sum(Amount)From [vw_RPTAP2_New_ReceiptDetail] v where v.ReceivedID=Pay.ReceivedID and v.RCReferent=Pay.RCReferent and v.PaymentType<>'9P'),0))--(Pay.RAmount)	
		,'CustomerName'= '' /* CASE   WHEN Pay.RefType = 1 and AD.AgreementName Is Not Null THEN LEFT(AD.AgreementName,69) 
								WHEN AD.BookingName Is Not Null THEN LEFT(AD.BookingName,69)
								ELSE AD1.CustomerName END */
	    ,'Address1'  =	''	/* CASE	WHEN Isnull(ISNULL(AD.CurrentAddress,AD1.CurrentAddress),'') = '' THEN ''
									ELSE Isnull(ISNULL(AD.CurrentAddress,AD1.CurrentAddress),'')  END+' '+
							CASE	WHEN Isnull(Isnull(AD.Moo,AD1.Moo),'') = '' THEN ''
									ELSE Isnull(ISNULL('ม.'+AD.Moo,AD1.Moo),'')END+' '+
							CASE	WHEN Isnull(Isnull(AD.Village,AD1.Village),'') = '' THEN ''
									ELSE Isnull(ISNULL(AD.Village,AD1.Village),'') END+' '+
							CASE	WHEN Isnull(Isnull(AD.Soi,AD1.Soi),'') = '' THEN ''
									ELSE Isnull(ISNULL('ซ.'+AD.Soi,AD1.Soi),'')END+' '+
							CASE	WHEN Isnull(Isnull(AD.Road,AD1.Road),'') = '' THEN ''
									ELSE Isnull(ISNULL('ถ.'+AD.Road,AD1.Road),'')END */
		
	   ,'Address2' =	''	/* CASE	WHEN Isnull(Isnull(AD.SubDistrict,AD1.SubDistrict),'') = '' THEN ''
									ELSE (CASE	WHEN Isnull(Isnull(AD.Province,AD1.Province),'') like '%กรุงเทพ%' THEN Isnull(ISNULL('แขวง'+AD.SubDistrict,AD1.SubDistrict),'')
												ELSE Isnull(ISNULL('ตำบล'+AD.SubDistrict,AD1.SubDistrict),'') END)END+' '+
							CASE	WHEN Isnull(Isnull(AD.District,AD1.District),'') = '' THEN ''
									ELSE	(CASE	WHEN Isnull(Isnull(AD.Province,AD1.Province),'') like '%กรุงเทพ%' THEN Isnull(ISNULL('เขต'+AD.District,AD1.District),'')
													ELSE Isnull(ISNULL('อำเภอ'+AD.District,AD1.District),'') END) END */
													
	   ,'Address3' =	''	/* CASE	WHEN Isnull(Isnull(AD.Province,AD1.Province),'') = '' THEN ''
									ELSE (CASE  WHEN Isnull(Isnull(AD.Province,AD1.Province),'') like '%กรุงเทพ%' THEN Isnull(ISNULL(AD.Province,AD1.Province),'')
												ELSE Isnull(ISNULL('จังหวัด'+AD.Province,AD1.Province),'') END)+' '+Isnull(ISNULL(AD.PostCode,AD1.PostCode),'') END */
					
		,'CusTel' =	'' --Case When Isnull(Pay.Mobile,'')<>''Then Pay.Mobile Else Isnull(AD1.Tel,'') End
		


FROM	[FIN].[ReceiptTempHeader] RTH
        /* (
			SELECT		Pay.ReferentID AS ContractNumber
						,CASE	WHEN Pay.RefType = 1 THEN BO.ContactID 
								ELSE AO.ContactID END AS ContactID
						,CASE	WHEN Pay.RefType = 1 THEN ISNULL(BO.Mobile,'') 
								ELSE ISNULL(AO.Mobile,'') END AS Mobile
						,Pay.ProductID, Pay.UnitNumber,Pay.ReceivedID,Pay.RefType,Pay.ReceiveDate,Pay.PrintingID,Pay.RDate
						,Pay.Amount AS RAmount,Pay.RCReferent
			FROM        (	SELECT DISTINCT RC.ReceivedID, TR.RefType, TR.ReferentID, TR.ProductID
									,'RDate' = CASE WHEN TR.Method = '6' THEN RC.ReceiveDate ELSE TR.RDate END 
									,TR.UnitNumber,RC.ReceiveDate,RC.PrintingID,RC.Amount,RC.RCReferent
							FROM	dbo.ICON_Payment_Received AS RC LEFT OUTER JOIN
									dbo.ICON_Payment_TmpReceipt AS TR ON RC.RCReferent = TR.RCReferent AND TR.CancelDate IS NULL
							GROUP BY RC.ReceivedID,TR.RefType,TR.ReferentID,TR.ProductID,TR.UnitNumber,RC.ReceiveDate,RC.PrintingID,RC.Amount,RC.RCReferent,TR.RDate,TR.Method
						 ) AS Pay 
						 LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AS AO ON AO.ContractNumber = Pay.ReferentID AND AO.Header = 1 AND ISNULL(AO.IsDelete, 0) = 0 
						 LEFT OUTER JOIN [ICON_EntForms_BookingOwner] AS BO ON BO.BookingNumber = Pay.ReferentID AND BO.Header = 1 AND ISNULL(BO.IsDelete, 0) = 0 
			WHERE		Pay.ReceivedID IN (SELECT * FROM [dbo].[fn_SplitString](@ReceivedID,','))

		)Pay 
		LEFT OUTER JOIN [ICON_EntForms_Products]PR ON PR.ProductID = Pay.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_Company] CO ON PR.CompanyID = CO.CompanyID 
		LEFT OUTER JOIN @TableAddress AD ON AD.ContactID = Pay.ContactID AND Pay.ReceivedID = AD.ReceivedID AND Pay.RefType = AD.RefType
		LEFT OUTER JOIN @TableAddress AD1 ON Pay.ReceivedID = AD1.ReceivedID AND Pay.RefType = AD1.RefType

WHERE	 Pay.ReceivedID  IN (SELECT * FROM [dbo].[fn_SplitString](@ReceivedID,',')) */


GO
