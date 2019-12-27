SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_PF_AG_004]'60015AA00001'
-- [dbo].[AP2SP_PF_AG_004]'60012aa00378'

CREATE PROC [dbo].[PF_MD_002]
	@ProjectNo nvarchar(50)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
/* DECLARE @v Table(PayableAmount money , DueDate Datetime)
INSERT INTO @v
	SELECT	PayableAmount,DueDate
	FROM	ICON_ENTFORMS_AGREEMENTPERIOD  WITH (NOLOCK)
	WHERE	ContractNumber=@ContractNumber
			AND PaymentType IN ('8','82')

DECLARE @t Table (Period nvarchar(10))
INSERT INTO @t
	SELECT	Period
	FROM	ICON_EntForms_AgreementPeriod WITH (NOLOCK)
	WHERE	ContractNumber=@ContractNumber
			AND PaymentType = '6'
			AND Duedate = (SELECT MAX(Duedate)FROM ICON_EntForms_AgreementPeriod WITH (NOLOCK) WHERE ContractNumber=@ContractNumber AND PaymentType = '6')
*/
SELECT TOP(1) 'ContractNumber' = '' --A.ContractNumber,
        ,'ProductID' = P.ProjectNo --P.ProductID,
		,'ProjectName' = P.ProjectNameTH --P.Project,
		,'ContractDate' = '' --ISNULL(A.pContractDate,A.ContractDate),
		,'CompanyNameThai' = '' -- [dbo].[fn_GetCompanyNameTH](P.CompanyID,ISNULL(A.pContractDate,A.ContractDate)), --C.CompanyNameThai,
		,'AddressThai' = C.AddressTH --C.AddressThai,
		,'Company_Soi' = CASE WHEN C.SoiTH = '' THEN '-' ELSE ISNULL(C.SoiTH, '-')END --CASE WHEN C.SoiThai = '' THEN '-' ELSE ISNULL(C.SoiThai,'-')END ,
		,'Company_Road' = ISNULL(C.RoadTH, '-') --ISNULL(C.RoadThai,'-'),
		,'Company_Moo' = '-'
		,'Company_SubDistrict' = ISNULL(SD.NameTH,'-')
		,'Company_District' = ISNULL(D.NameTH,'-')
		,'Company_Province' = ISNULL(PR.NameTH,'-')
		,'Company_Tel' = ISNULL(C.Telephone,'-')
		,'UtilityAttornyName' = '' --ISNULL(P.AttornyName1,'-')
		,'AttoryIssueDate' = '' --ISNULL(DAY(P.AttoryIssueDate),0),
		,'AttoryIssueMonth' = '' --ISNULL([dbo].[MonthName]('TH',MONTH(P.AttoryIssueDate)),'-'),
		,'AttoryIssueYear' = '' --ISNULL((YEAR(P.AttoryIssueDate)+543),0),
		,'Project_Address' = ''-- CASE WHEN PA.AddressNo = '' THEN '-' ELSE ISNULL(PA.AddressNo,'-') END,
		,'Project_Soi' = '' --CASE WHEN PA.Soi = '' THEN '-' ELSE ISNULL(PA.Soi,'-') END,
		,'Project_Road' = '' --CASE WHEN PA.Road = '' THEN '-' ELSE ISNULL(PA.Road,'-') END,
		,'Project_Moo' = '' --CASE WHEN PA.Moo = '' THEN '-' ELSE ISNULL(PA.Moo,'-') END,
		,'Project_SubDistrict' = '' --ISNULL(PA.SubDistrict,'-'),
		,'Project_District' = '' --CASE WHEN Substring(PA.District,1,3)='เขต' THEN substring(PA.District,4,20) ELSE PA.District END,
		,'Project_Province' = '' --ISNULL(PA.Province,'-'),
		,'Project_Tel' = '' --ISNULL(P.Tel,'-'),
		,'CustomerName' = '' --[dbo].[fn_GenCustAgreementAll_AG](@ContractNumber),
		,'Customer_Age' = '' --DATEDIFF(year,AO.BirthDate,ISNULL(A.pContractDate,A.ContractDate)),
		,'Customer_Nationality ' = '' --ISNULL(AO.Nationality,'-'),
		,'Customer_Address' = '' --ISNULL(AO.CurrentAddress,'-'),
		,'Customer_Soi' = '' --ISNULL(AO.Soi,'-'),
		,'Customer_Moo' = '' --ISNULL(AO.Moo,'-'),
		,'Customer_Road' = '' --ISNULL(AO.Road,'-'),
		,'Customer_SubDistrict' = '' --ISNULL(AO.SubDistrict,'-'),
		,'Customer_District' = '' --ISNULL(AO.District,'-'),
		,'Customer_Provnce' = '' --ISNULL(AO.Province,'-'),
		,'Customer_Telephone' = '' --AO.Mobile,
		,'Customer_EMail' =  '' --ISNULL(AO.EMail,'-'),
        ,'Project_TitleDeedNumber' = '' --[dbo].[fnGenProductTitledeedNumber](P.ProductID),
		,'Project_Survey' = '' --[dbo].[fnGenProductLandSurveyArea](P.ProductID),
		,'Project_LandNumber' = '' --[dbo].[fnGenProductLandNumber](P.ProductID),
		,'Project_Area' = '' --P.Area,
		,'LicenceProductNumber' = '' --P.LicenceProductNumber,
		,'LicenceProductDate' = '' --P.LicenceProductIssueDate,
		,'UnitNumber' = U.UnitNo --U.UnitNumber
        ,'FloorID' = U.FloorID
		,'Unit_Area' = '' --B.StandardArea, 
		,'IncreasingAreaPrice' = '' --A.UnitIncreasingAreaPrice,
		,'IncreasingAreaBath' = '' --[dbo].[fnBHT_BahtText](A.UnitIncreasingAreaPrice),
		,'TotalSellingPrice' = '' --A.SellingPrice,
		,'TotalSellingPriceText' = '' --CASE WHEN A.SellingPrice = 0.00 THEN '-' ELSE [dbo].[fnBHT_BahtText](A.SellingPrice)END,
		,'BookingDate' = '' --B.BookingDate,
		,'BookingPaid' = '' --A.BookingPaid,
		,'BookingBath' = '' --CASE WHEN ISNULL(A.BookingPaid,0) = 0.00 THEN '-' ELSE [dbo].[fnBHT_BahtText](A.BookingPaid) END,
		,'ContractAmount' = '' --A.ContractAmount,
		,'ContractAmountText' = '' --CASE WHEN ISNULL(A.ContractAmount,0) = 0 THEN '-' ELSE [dbo].[fnBHT_BahtText](A.ContractAmount)END, 
		,'TotalInContractDate' = '' --ISNULL(A.BookingAmount,0.00)+ISNULL(A.ContractAmount,0.00),
		,'TotalInContractDateText' = '' --[dbo].[fnBHT_BahtText](ISNULL(A.BookingAmount,0.00)+ISNULL(A.ContractAmount,0.00)),	
		,'RemainOfPrice' = '' --A.SellingPrice - (ISNULL(A.BookingAmount,0.00)+ISNULL(A.ContractAmount,0.00)),
		,'RemainOfPriceText' = '' --[dbo].[fnBHT_BahtText](A.SellingPrice - (ISNULL(A.BookingAmount,0.00)+ISNULL(A.ContractAmount,0.00))),
		,'PaymentDown' = '' --[dbo].[fn_GetDownPaymentPeriod](A.ContractNumber),
		,'PaymentDownPerMonth' = '' --[dbo].[fn_GetMoneyOfDownPerMonth](A.ContractNumber),
		,'PaymentDownPerMonthText' = '' --[dbo].[fnBHT_BahtText]([dbo].[fn_GetMoneyOfDownPerMonth](A.ContractNumber)),
		,'Payment_DueDate' = '' --[dbo].[fn_GetDownPaymentPeriodDate](A.ContractNumber),
		,'LastPayment' = '' --(SELECT PayableAmount FROM @v),
		,'LastPaymentText' = '' --[dbo].[fnBHT_BahtText]((SELECT PayableAmount FROM @v)),
		,'TransferDate' = '' --A.TransferDate,
		,'SinkingFundRate' = '' --ISNULL(P.SinkingFundRate,0.00),
		,'PublicFundRate' = '' --ISNULL(P.PublicFundRate,0.00),
		,'BurimasitBank' = '' --ISNULL(P.LoanBank,'-'), 
		,'BurimasitAmount' = '' --ISNULL(P.LoanAmount,0),
		,'BurimasitAmountText' = '' --[dbo].[fnBHT_BahtText](ISNULL(P.LoanAmount,0)),
		,'ParkingUnit' = '' --ISNULL(P.ParkingUnits,0),
		,'LastPeriod' = '' --isnull((SELECT Period FROM @t),0) + Case When Isnull(isApPay,0)=1 Then 1 Else 0 End,
		,'IsBuildComplete' = '' --ISNULL(A.IsBuildComplete,0),
		,'witness1' = '' --P.witness1,
        ,'witness2' = '' --P.witness2,
		,'Parking' = '' --ISNULL(U.Parking,0),
		,'ParkingIncrease' = '' --ISNULL(U.ParkingIncrease,0),
		,'TotalParking' = '' --ISNULL(U.Parking,0)+ISNULL(U.ParkingIncrease,0),
		,'Rai' = '' --ISNULL(P.Rai,'-'),
		,'Ngarn' = '' --ISNULL(P.Ngarn,'-'),
		,'SquareWah' = '' --ISNULL(P.SquareWah,'-'),
		,'Preferunit' = '-' 
		,'Project_SubDistrict2' = '' --ISNULL(PA2.SubDistrict,'-'),
		,'Project_District2' = '' --ISNULL(PA2.District,'-'),
		,'Project_Province2' = '' --ISNULL(PA2.Province,'-'),
		,'Flag_AG' = '' --CASE	WHEN LEN(ISNULL(PA2.SubDistrict,'0')) <= 42 AND LEN([dbo].[fnGenProductTitledeedNumber](P.ProductID)) <= 53  THEN '1'
							--WHEN LEN(ISNULL(PA2.SubDistrict,'0')) > 42 AND LEN([dbo].[fnGenProductTitledeedNumber](P.ProductID)) > 53    THEN '2' 
							--WHEN LEN(ISNULL(PA2.SubDistrict,'0')) <= 42 AND LEN([dbo].[fnGenProductTitledeedNumber](P.ProductID)) > 53   THEN '3' 
							--WHEN LEN(ISNULL(PA2.SubDistrict,'0')) > 42 AND LEN([dbo].[fnGenProductTitledeedNumber](P.ProductID)) <= 53   THEN '4' 
					--END
		,'TicketAmt' = '' --ISNULL(U.Parking,0)+ISNULL(U.ParkingIncrease,0)
		,'TParking' = '' --P.ParkingUnits
		,'ParkingAmt' = '' --ISNULL(U.Parking,0)
		,'ParkingNoAmt' = '' --ISNULL(U.Parking,0)
		,'NoParkingAmt' = '' --ISNULL(U.ParkingIncrease,0) 
		,'ForType' = '( สำหรับลูกค้า )' --DocPP.TypeDesc --CASE @ForType WHEN 1 THEN 'สำหรับบริษัท' WHEN 2 THEN 'สำหรับลูกค้า' ELSE '' END
		,'UnitNumber_Old' = '' --U.UnitNumber_Old
		,'Logo' = '' --P.Logo
		,'FloorPlanImage' = '' --FP.FloorPlanImage
		,'RoomPlanImage' = '' --RP.RoomPlanImage
		,'image_FloorPlanImage' = '' --FP.image_FloorPlanImage
		,'image_RoomPlanImage' = '' --RP.image_RoomPlanImage
		,'IsNotPassEIA' = '' --CAST(CASE WHEN ISNULL(p.Is3step,0)=1 THEN 1 
					--WHEN dbo.fn_ClearTime(A.ContractDate) <= dbo.fn_ClearTime(P.Change2StepDate) THEN 1 
					--ELSE 0 END AS BIT)
		,'DocumentName' = '' --P.DocumentName
					
FROM	[PRJ].[Project] P WITH (NOLOCK)
        LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ProjectID = P.ID
        LEFT OUTER JOIN [MST].[Company] C WITH (NOLOCK) ON C.ID = P.CompanyID
        LEFT OUTER JOIN [MST].[SubDistrict] SD WITH (NOLOCK) ON SD.ID = C.SubDistrictID
        LEFT OUTER JOIN [MST].[District] D WITH (NOLOCK) ON D.ID = C.DistrictID
        LEFT OUTER JOIN [MST].[Province] PR WITH (NOLOCK) ON P.ID = C.ProvinceID
        --[ICON_EntForms_Agreement]A WITH (NOLOCK) LEFT OUTER JOIN
		--[ICON_EntForms_AgreementOwner]AO WITH (NOLOCK) ON A.ContractNumber = AO.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete,0) = 0 LEFT OUTER JOIN 
		--[ICON_EntForms_Booking]B WITH (NOLOCK) ON A.BookingNumber = B.BookingNumber LEFT OUTER JOIN
		--[ICON_EntForms_Unit]U WITH (NOLOCK) ON A.UnitNumber = U.UnitNumber AND A.ProductID = U.ProductID LEFT OUTER JOIN
		--[ICON_EntForms_ManageModel]M WITH (NOLOCK) ON U.ProductID = M.ProductID AND U.ModelID = M.ModelID LEFT OUTER JOIN
		--[ICON_EntForms_Products]P WITH (NOLOCK) ON A.ProductID = P.ProductID LEFT OUTER JOIN
		--[ICON_EntForms_ProductsAddress]PA WITH (NOLOCK) ON A.ProductID = PA.ProductID AND PA.AddressFlag IN(0,1) LEFT OUTER JOIN
		--[ICON_EntForms_ProductsAddress]PA2 WITH (NOLOCK) ON A.ProductID = PA2.ProductID AND PA2.AddressFlag IN(0,2) LEFT OUTER JOIN
		--[ICON_EntForms_Company]C WITH (NOLOCK) ON P.CompanyID = C.CompanyID LEFT OUTER JOIN
		--[ZPreBooking_FloorPlan]FP WITH (NOLOCK) ON U.ProductID = FP.ProjectID AND U.[FloorPlanImage]=FP.ImageFileName LEFT OUTER JOIN
		--[ZPreBooking_RoomPlan]RP WITH (NOLOCK) ON U.ProductID = RP.ProjectID AND U.[RoomPlanImage]=RP.ImageFileName
		
		--JOIN (
			
		--	SELECT  'ForType' = 1 , 'TypeDesc' = N'( สำหรับบริษัท )' 
		--	UNION
		--	SELECT  'ForType' = 2 , 'TypeDesc' = N'( สำหรับลูกค้า )' 
					
		--) DocPP ON 1=1
		
		
WHERE P.ProjectNo = @ProjectNo	--A.ContractNumber = @ContractNumber;



GO
