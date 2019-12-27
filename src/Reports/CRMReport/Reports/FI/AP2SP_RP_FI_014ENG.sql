SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--หนังสือรับรองการชำระเงิน ENG

--[AP2SP_RP_FI_014ENG] NULL,'60018','a06c20','Administrator Account',NULL,NULL,'''4'',''5'',''6'',''8''','''6''','2017-03-09 00:00:00.000','2017-03-09 00:00:00.000'

CREATE PROC  [dbo].[AP2SP_RP_FI_014ENG]
    @CompanyID  nvarchar(20),
	@ProductID	nvarchar(15) = '',
	@UnitNumber	nvarchar(15) = '',
	@UserName	nvarchar(50) = '',
	@PeriodStart nvarchar(15),
	@PeriodEnd	 nvarchar(15),
    @PaymentType nvarchar(25),
    @PaymentType2 nvarchar(20),
	@DateStart datetime,
	@DateEnd   datetime

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

Declare @sql nvarchar(max)
Set @sql= '

SELECT 
	''PrintDate'' = '''' --dbo.FormatDateTime(''EN'', ''d MMMM yyyy'',GETDATE()) AS PrintDate,
	,''CompanyID'' = '''' --PR.CompanyID,
	,''CompanyName''= '''' --dbo.fn_GetCompanyNameEN(CP.CompanyID,ISNULL(RDate2,RDate1)) 
	,''ProductID'' = '''' --BK.ProductID
    ,''ProjectName'' = '''' --PR.Project AS ProjectName
    ,''UnitNumber'' = '''' --BK.UnitNumber,
	,''ContractNumber'' = '''' -- CASE WHEN AG.MA_RUNNO IS NOT NULL THEN Substring(AG.ContractNumber,8,20)
						    --ELSE ISNULL(AG.ContractNumber,BK.BookingNumber) END,
    ,''ContractNumber2'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber),
    ,''BookingNumber'' = '''' --BK.BookingNumber,
	,''CustomerName'' = '''' --ISNULL([dbo].[fn_GenCustAgreementAll_ContractEng](AG.ContractNumber),[dbo].[fn_GenCustBookingAllName](BK.BookingNumber)),
	,''ContractDate'' = '''' --dbo.FormatDateTime(''EN'', ''d MMMM yyyy'',ISNULL(ContractDate,BookingDate)) AS ContractDate,
	,''UnitNumber'' = '''' --ISNULL(AG.Unitnumber,BK.UnitNumber) AS Unitnumber
     , ''AddressCus1'' = '''' /* CASE WHEN ISNULL(CT.HouseID_4_Eng,'''') = '''' THEN ''''
					           ELSE ISNULL(CT.HouseID_4_Eng,'''') END+ '' '' +
				          CASE WHEN ISNULL(CT.Village_4_Eng,'''') = '''' THEN ''''
						       ELSE ISNULL(CT.Village_4_Eng,'''') END+ '' '' +
                          CASE WHEN ISNULL(CT.Moo_4_Eng,'''') = '''' THEN ''''
                               ELSE CT.Moo_4_Eng  END+'' ''+
						  CASE WHEN ISNULL(CT.Soi_4_Eng,'''') = '''' THEN ''''
							   ELSE  ''Soi ''+CT.Soi_4_Eng  END+ '' '' +
					      CASE WHEN ISNULL(CT.Road_4_Eng,'''') = '''' THEN ''''
							   ELSE  CT.Road_4_Eng +'' Road,'' END +'' ''+
				          CASE WHEN ISNULL(CT.SubDistrict_4_Eng,'''') = '''' THEN ''''
					           ELSE CASE WHEN CT.Country_4 like ''%ไทย%'' THEN SD.SubDistrictNameEng ELSE CT.SubDistrict_4_Eng  END END	*/                      
     , ''AddressCus2'' =  '''' /* CASE WHEN ISNULL(CT.District_4_Eng,'''') = '''' THEN ''''
                                ELSE CASE WHEN CT.Country_4 like ''%ไทย%'' THEN D.DistrictNameEng ELSE CT.District_4_Eng  END END+'', ''+
                           CASE WHEN ISNULL(CT.Province_4_Eng,'''') = '''' THEN ''''
                                ELSE CASE WHEN CT.Country_4 like ''%ไทย%'' THEN P.ProvinceNameEng ELSE CT.Province_4_Eng  END +'' ''+
                           ISNULL(CT.PostalCode_4,'''') END */
	, ''SumAmt'' = '''' --ISNULL(PD1.SumAmt1,0)+ISNULL(PD2.SumAmt2,0)
	, ''SumAmtText'' = '''' --dbo.[Currency_ToWords](ISNULL(PD1.SumAmt1,0)+ISNULL(PD2.SumAmt2,0))

    , ''AddressProduct'' =  ''''  /* CASE WHEN CP.AddressEng = '''' THEN ''''
								   ELSE ISNULL(CP.AddressEng ,'''') END+ '' '' +
							  CASE WHEN CP.BuildingEng = '''' THEN ''''
								   ELSE ISNULL(CP.BuildingEng,'''') END+ '', '' +
							  CASE WHEN CP.SoiEng = '''' THEN ''''
								   ELSE ISNULL(CP.SoiEng,'''')END+'', ''+
							  CASE WHEN ISNULL(CP.RoadEng,'''') = '''' THEN ''''
							       ELSE ISNULL(CP.RoadEng+'' Road.'','''')END+'', ''+
					          CASE WHEN CP.SubDistrictEng = '''' THEN ''''
						           ELSE CP.SubDistrictEng END +'', ''+       
	                          CASE WHEN CP.DistrictEng = '''' THEN ''''
							       ELSE CP.DistrictEng END +'' , ''+
					          CASE WHEN CP.ProvinceEng = '''' THEN ''''
							       ELSE CP.ProvinceEng END */
	,''RDate'' = '''' --ISNULL(RDate2,RDate1)  '
Set @sql= @sql+'
FROM    [SAL].[Booking] BK' --This is main table, below table need to be use as well
		/* LEFT OUTER JOIN [ICON_Entforms_Agreement] AG ON BK.BookingNumber = AG.BookingNumber
		LEFT OUTER JOIN [ICON_Entforms_AgreementOwner] AW ON AW.ContractNumber = AG.ContractNumber AND ISNULL(AW.Header,0) = 1 AND ISNULL(AW.IsDelete,0) = 0
		LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.Header,0) = 1 AND ISNULL(BO.IsDelete,0) = 0  AND ISNULL(BO.IsBooking,0) = 1
		LEFT OUTER JOIN [ICON_Entforms_Products] PR ON PR.ProductID = BK.ProductID
		LEFT OUTER JOIN [ICON_Entforms_Company] CP ON PR.CompanyID = CP.CompanyID
		LEFT OUTER JOIN [ICON_EntForms_Contacts] CT ON CT.ContactID = ISNULL(AW.ContactID,BO.ContactID)
		LEFT OUTER JOIN [ICON_EntForms_Province] P ON P.ProvinceName = CT.Province_4
		LEFT OUTER JOIN [ICON_EntForms_District] D ON D.DistrictName = CT.District_4 AND D.ProvinceID = P.ProvinceID
		LEFT OUTER JOIN [ICON_EntForms_SubDistrict] SD ON SD.SubDistrictName = CT.SubDistrict_4 AND SD.DistrictID = D.DistrictID AND SD.ProvinceID = P.ProvinceID
		LEFT OUTER JOIN
				(
					SELECT PD.ReferentID,SUM(PD.Amount) AS SumAmt1,MAX(TR.RDate) AS RDate1
					FROM [ICON_Payment_TmpReceipt] TR  
						LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
					WHERE PD.PaymentType IN ('+@PaymentType+') '

					
					--เลือกระหว่างวันที่
					IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						SET @sql=@sql+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period Between  '''+@PeriodStart+''' AND '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period >= '''+@PeriodStart+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period <= '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' '

					--เลือกระหว่างวันที่
					IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						SET @sql=@sql+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
						
					SET @sql=@sql+')'

	Set @sql= @sql+'
					Group By PD.ReferentID
				)PD1 ON PD1.ReferentID = AG.ContractNumber
		LEFT OUTER JOIN
				(
					SELECT PD.ReferentID,SUM(PD.Amount) AS SumAmt2,MAX(TR.RDate) AS RDate2
					FROM [ICON_Payment_TmpReceipt] TR  
						LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
					WHERE PD.PaymentType IN ('+@PaymentType+') '
					
					--เลือกระหว่างวันที่
					IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						SET @sql=@sql+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period Between  '''+@PeriodStart+''' AND '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period >= '''+@PeriodStart+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period <= '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql=@sql+' OR (PD.PaymentType = '+@PaymentType2+' '
					
					--เลือกระหว่างวันที่
					IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						SET @sql=@sql+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

					SET @sql=@sql+')'

	Set @sql= @sql+'
					Group By PD.ReferentID
				)PD2 ON PD2.ReferentID = BK.BookingNumber

WHERE (BK.CancelDate IS NULL OR (AG.ContractNumber IS NOT NULL AND AG.CancelDate IS NULL AND BK.CancelDate IS NULL)) 
	'

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(PR.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(BK.ProductID = '''+@ProductID+''')'
if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+' and(BK.UnitNumber = '''+@UnitNumber+''')'

set @sql=@sql+' ORDER BY BK.BookingNumber,AG.ContractNumber ' */
--print( @sql) 

exec( @sql)


GO
