SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_PF_AG_003] '10185BA00074'
--[AP2SP_PF_AG_003] '10096BN00918'
CREATE PROCEDURE [dbo].[AP2SP_PF_AG_003]

    @BookingNumber  nvarchar(50)
	
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



SELECT  'ReceivedID' = [dbo].[fn_GenPrintingIDBK](BK.BookingNumber) --เลขที่ใบเสร็จ
        , 'BookingNumber' = BK.BookingNo
        , BK.BookingDate
        , 'ProjectName' = P.ProjectNameTH --ชื่อโครงการ
		, 'NameBooking' = ISNULL(BO.TitleExtTH,'') + ISNULL(BO.FirstNameTH + ' ', '') + ISNULL(BO.LastNameTH, '')  --[dbo].[fn_GenCustBookingAll20090914] (BK.BookingNumber)
        , 'Age' = Year(GetDate())-Year(BO.BirthDate)
		, 'BCurrentAddress' = ISNULL((SELECT HouseNoTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')
        , 'BMoo' = ISNULL((SELECT MooTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')
		, 'B.Soi' = CASE WHEN (SELECT SoiTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT SoiTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END
		, 'BRoad' = CASE WHEN (SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END 
        , 'BSubDistrict' = ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameTH'),'-')
        , 'BDistrict' = ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] ((SELECT DistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameTH'),'-')
        , 'BProvince' = ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] ((SELECT ProvinceID FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')), 'NameTH'),'-')
        , 'BPostCode' = ISNULL((SELECT PostalCode FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-')
        , 'BPhone' = CASE WHEN (SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT.ID,'1')) = '' THEN '-' ELSE ISNULL((SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT.ID,'1')),'-') END
		, 'BMobile' = CASE WHEN (SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT PhoneNumber FROM [dbo].[fn_GetContactPhone] (CT.ID,'0')),'-') END 
		, 'BEmail' =  ISNULL((SELECT Email FROM [dbo].[fn_GetContactEmail] (CT.ID,'0')),'-') 
        , 'NameAgreement' = [dbo].[fn_GenCustBookingToContact] (BK.BookingNo)
	    , 'CompanyID' = P.CompanyID
	    , 'CompanyName' = [dbo].[fn_GetCompanyNameTH](P.CompanyID,BK.BookingDate)  --ผู้ขาย
        , 'CompanyAddress' = [dbo].[fn_GetCompanyFullAddress] (P.CompanyID)
        , 'AddressNo' = CASE WHEN PA.AddressNameTH = '' THEN '-' ELSE ISNULL(PA.AddressNameTH,'-') END
		, 'Soi' = CASE WHEN PA.SoiTH = '' THEN '-' ELSE ISNULL(PA.SoiTH,'-') END 
		, 'Road' = CASE WHEN PA.RoadTH = '' THEN '-' ELSE ISNULL(PA.RoadTH,'-') END 
		, 'SubDistrict' = ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (PA.SubDistrictID,'0')), 'NameTH'),'-')
	    , 'District' = ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (PA.DistrictID,'0')), 'NameTH'),'-')
        , 'Province' = ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (PA.ProvinceID,'0')), 'NameTH'),'-') --ที่อยู่โครงการ
        , 'PostCode' = PA.PostalCode
        , 'UnitNumber' = U.UnitNo  --แปลงเลขที่
        , 'ModelHomeThai' = MM.NameTH
        , 'ModelWidth' = MM.FrontWidth  --แบบหน้ากว้าง
        , 'StandardArea' = ISNULL(BK.SaleArea,0) --พื้นที่
        , 'TotalSellingPrice' = ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคาขาย'),0)   --ราคามาตรฐาน
        , 'SellingPrice' = ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ราคาขายสุทธิ'),0)   --ราคาขาย(หักส่วนลดแล้ว)
        , 'PriceText' = dbo.fnBHT_BahtText(BK.SaleArea)
        , BK.ContractDueDate
        , 'BookingAmount' = ISNULL((SELECT Amount FROM @UPI WHERE Name = N'เงินจอง'),0) --BK.BookingAmount --เงินจองที่จะต้องชำระ
        , 'BookingPaid' = ISNULL(TR.TotalAmount,0) --เงินจองที่ชำระจริง
        , 'BookingPaidText' = dbo.fnBHT_BahtText(TR.TotalAmount)
        , 'ContractAmount' = ISNULL((SELECT Amount FROM @UPI WHERE Name = N'เงินสัญญา'),0) --เงินทำสัญญา
        , 'DownPaymentPerPeriod' = ISNULL((SELECT Installment FROM @UPI WHERE Name = N'เงินดาวน์'),0) --จำนวนงวดดาวน์
        , 'PermanentAddress'= ISNULL((SELECT HouseNoTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'1')),'-'), 'PermanentMoo'=CASE WHEN (SELECT MooTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT MooTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END
        , 'PermanentVillage'= CASE WHEN (SELECT VillageTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT VillageTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END
        , 'PermanentSoi'=CASE WHEN (SELECT SoiTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT SoiTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END
        , 'PermanentRoad'= CASE WHEN (SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')) = '' THEN '-' ELSE ISNULL((SELECT RoadTH FROM [dbo].[fn_GetContactAddress] (CT.ID,'0')),'-') END
        , 'PermanentSubDistrict'= ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] ((SELECT SubDistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'1')), 'NameTH'),'-')
        , 'PermanentDistrict'= ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] ((SELECT DistrictID FROM [dbo].[fn_GetContactAddress] (CT.ID,'1')), 'NameTH'),'-')
        , 'PermanentProvince'= ISNULL([dbo].[fn_GetProvinceDetailFromFieldName] ((SELECT ProvinceID FROM [dbo].[fn_GetContactAddress] (CT.ID,'1')), 'NameTH'),'-')
        , 'PermanentPostID'= ISNULL((SELECT PostalCode FROM [dbo].[fn_GetContactAddress] (CT.ID,'1')),'-')
		, 'PermanentEMail'= ISNULL((SELECT Email FROM [dbo].[fn_GetContactEmail] (CT.ID)),'-')
        , 'IncreasingAreaPrice'= '' --ISNULL(UP.UnitIncreasingAreaPrice,0) --ราคาที่ดินเพิ่ม/ลด
        , 'IncreasingAreaPriceText' = '' -- dbo.fnBHT_BahtText(UP.UnitIncreasingAreaPrice)
        , 'PublicFundRate'= ISNULL(AC.PublicFundRate,0)  --อัตราค่าส่วนกลาง
        --10053 The Centro รามอินทรา ดึงจำนวนเดือนค่าส่วนกลาง มาจากตารางโปรโมชั่นค่าใช้จ่าย
        , 'PublicFundYear' = CASE WHEN BK.ProjectID = '10053' THEN ISNULL(ISNULL(S.Y,P.PublicFundMonths)/12,0) 
									ELSE ISNULL(AC.PublicFundMonths/12,0) END --ชำระล่วงหน้า(เดือน)
        , 'UnitTransferfee'= ISNULL(AC.ChangeNameFee,0)
        , 'UnitTransferfeeText' = dbo.fnBHT_BahtText(AC.ChangeNameFee)
        , 'SaleName' = CASE WHEN BK.SaleUserID > 0 THEN ISNULL(US.FirstName,'') + ' ' + ISNULL(US.LastName,'') --ผู้รับจอง
				--WHEN BK.SaleTraineeID > 0 THEN ISNULL(US3.FirstName,'') + ' ' + ISNULL(US3.LastName,'')
				--ELSE ISNULL(US2.FirstName,'') + ' ' + ISNULL(US2.LastName,'') END
                ELSE '' END
        , 'LocationPrice'= ISNULL(UP.LocationPrice,0)
        , 'DownDate' = dateadd(month,1,BK.ContractDueDate)
        , 'TransferDate' = BK.PromotionTransferDate
        , 'ProductID' = P.ProjectNo --.ProductID
        , 'Tel' = P.PhoneNumber
		, 'CashDiscount'= ISNULL((SELECT Amount FROM @UPI WHERE Name = N'ส่วนลดเงินสด'),0) --ส่วนลดเงินสด
		, 'Condition' = 'หมายเหตุ โอนกรรมสิทธิ์ภายในวันที่ ' +  dbo.FormatDateTime('TH', 'dd MMMM yyyy', BK.PromotionTransferDate)	
		, 'ApproveDate' = ISNULL(R.RDate,ISNULL(AdHoc.Approve3Date,BK.ApproveDate))
		
FROM	[SAL].[Booking] BK WITH (NOLOCK)
		LEFT OUTER JOIN [SAL].[BookingOwner] BO WITH (NOLOCK) ON BO.BookingID = BK.ID AND BO.Header = '1'  AND ISNULL(BO.IsDelete,0) = 0 AND ISNULL(BO.IsBooking,0) = 1
		LEFT OUTER JOIN [CTM].[Contact] CT WITH (NOLOCK) ON CT.ID = BO.FromContactID
		LEFT OUTER JOIN [PRJ].[Project] P WITH (NOLOCK) ON P.ID = BK.ProjectID 
		LEFT OUTER JOIN [PRJ].[AgreementConfig] AC WITH (NOLOCK) ON AC.ProjectID = P.ID 
		LEFT OUTER JOIN [PRJ].[Address] PA	WITH (NOLOCK) ON PA.ProjectID = P.ID AND PA.AddressFlag IN('0','1') 
		LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ProjectID = BK.ProjectID AND U.ID = BK.UnitID 
        LEFT OUTER JOIN [PRJ].[Model] MM WITH (NOLOCK) ON MM.ProjectID = U.ProjectID AND MM.ID = U.ModelID  
		LEFT OUTER JOIN [SAL].[BookingOwner] BO2 WITH (NOLOCK) ON BO2.BookingID = BK.ID AND ISNULL(BO2.IsDelete,0) = 0 And ISNULL(BO2.IsContract,0) = 1 And ISNULL(BO2.IsContractHeader,0) = 1
		LEFT OUTER JOIN [CTM].[Contact] CT2 WITH (NOLOCK) ON CT2.ID = BO2.FromContactID
		LEFT OUTER JOIN 
		(	
			SELECT	A1.UnitNumber,A1.ProductID,A1.ContractAmount,A1.IncreasingAreaPrice,A1.DownPaymentPeriod,A1.LocationPrice,A1.UnitIncreasingAreaPrice
			FROM    [ICON_EntForms_UnitPriceList] AS A1 WITH (NOLOCK) INNER JOIN
				(
				 SELECT  ProductID, UnitNumber, MAX(ActiveDate) AS MaxDate
				 FROM    [ICON_EntForms_UnitPriceList] WITH (NOLOCK)
				 WHERE   (ActiveDate <= GetDate())
				 GROUP BY  ProductID, UnitNumber
				) AS A2 ON A1.ProductID = A2.ProductID AND A1.UnitNumber = A2.UnitNumber AND  A1.ActiveDate = A2.MaxDate
		) AS UP ON UP.UnitNumber = U.UnitNumber AND UP.ProductID = U.ProductID 
		LEFT OUTER JOIN
		(
			SELECT	SUM(ISNULL(Amount,0)) AS TotalAmount,ReferentID
			FROM	[ICON_Payment_TmpReceipt] WITH (NOLOCK)
			WHERE	ReferentID = @BookingNumber
					AND CancelDate IS NULL
			GROUP BY ReferentID
		) TR ON TR.ReferentID = BK.BookingNumber 
        LEFT OUTER JOIN [USR].[User] US WITH (NOLOCK) ON US.ID = BK.SaleUserID
        --LEFT OUTER JOIN [Users] US2 WITH (NOLOCK) ON US2.UserID = BK.SaleHelper 
        --LEFT OUTER JOIN [Users] US3 WITH (NOLOCK) ON US3.UserID = BK.SaleTraineeID 
		LEFT OUTER JOIN [ZPROM_SalePromotionFee] S WITH (NOLOCK) ON S.DocumentID = BK.BookingNumber AND S.DocumentType = 1 AND S.PromotionFeeID = '00'
		LEFT OUTER JOIN (
			SELECT PD.ReferentID,MAX(RDate) AS RDate 
			FROM dbo.ICON_Payment_PaymentDetails PD WITH (NOLOCK) LEFT OUTER JOIN 
				dbo.ICON_Payment_TmpReceipt R WITH (NOLOCK) ON PD.RCReferent=R.RCReferent AND PD.TmpReceiptID=R.TmpReceiptID
			WHERE PD.PaymentType = '4'
				AND R.CancelDate IS NULL
			GROUP BY PD.ReferentID
		)R ON BK.BookingNumber=R.ReferentID
		LEFT OUTER JOIN (
			SELECT [DocumentNumber],[ProductID],[UnitNumber],[Approve3Date]
			FROM [db_iconcrm_fusion].[dbo].[Z_BudgetApprove] WITH (NOLOCK)
			WHERE DocumentType IN ('Booking','BookingChangeUnit')
				AND BudgetType IN ('AdHoc1','AdHoc2')
				AND [Status] = 'Finish'
		)AdHoc ON BK.BookingNo=AdHoc.[DocumentNumber]
		
WHERE BK.BookingNo = @BookingNumber;

GO
