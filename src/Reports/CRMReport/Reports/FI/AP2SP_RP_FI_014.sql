SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--หนังสือรับรองการชำระเงิน

--[AP2SP_RP_FI_014] '','40012','A05B15','Administrator Account','','','''4'',''5'',''6'',''8'',''TR2'',''A06''','6','2017-03-09 00:00:00.000','2017-03-09 00:00:00.000'
--exec "AP2SP_RP_FI_014_1";1 N'', N'10060', N'', N'', N''''','''','''','''',''''', N'''''', N'จินตนา อินทะเสย์', {ts '1800-01-01 00:00:00'}, {ts '7000-12-31 00:00:00'}

CREATE PROC  [dbo].[AP2SP_RP_FI_014]
    @CompanyID  nvarchar(20),
	@ProductID	nvarchar(15) = '',
	@UnitNumber	nvarchar(15) = '',
	@UserName	nvarchar(150) = '',
	@PeriodStart nvarchar(15)='',
	@PeriodEnd	 nvarchar(15)='',
    @PaymentType nvarchar(250)='',
    @PaymentType2 nvarchar(200)='',
	@DateStart datetime,
	@DateEnd   datetime
AS

If(@DateStart is null)Set @DateStart='19000101'
If(@DateEnd is null)Set @DateEnd='70001231'
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

if(isnull(@PaymentType2,'')='') set @PaymentType2=@PaymentType

Declare @sql nvarchar(max)
Declare @sql1 nvarchar(max)
Declare @sql2 nvarchar(max)
Set @sql= '

SELECT ''CompanyID'' = '''' --PR.CompanyID,
	,''CompanyNameThai''= '''' --dbo.fn_GetCompanyNameTH(CP.CompanyID,ISNULL(RDate2,RDate1)),
	,''ProductID'' = '''' --BK.ProductID
    ,''Project'' = '''' --PR.Project
    ,''UnitNumber'' = '''' --BK.UnitNumber,
	,''ContractNumber'' = '''' --CASE WHEN AG.MA_RUNNO IS NOT NULL THEN Substring(AG.ContractNumber,8,20)
						    --ELSE ISNULL(AG.ContractNumber,BK.BookingNumber) END,
    ,''ContractNumber2'' = '''' --ISNULL(AG.ContractNumber,BK.BookingNumber),
    ,''BookingNumber'' = '''' --BK.BookingNumber,
	,''CustomerName'' = '''' --ISNULL([dbo].[fn_GenCustAgreementAll_Contract](AG.ContractNumber),
	,''BookingNumber'' = '''' --[dbo].[fn_GenCustBookingAllName](BK.BookingNumber)),
	,''ContractDate'' = '''' --ISNULL(ContractDate,BookingDate) AS ContractDate,
	,''UnitNumber'' = '''' --ISNULL(AG.Unitnumber,BK.UnitNumber) AS Unitnumber,
    ,''AddressCus1'' =  '''' /* CASE WHEN ISNULL(CT.HouseID_4,'''') = '''' THEN ''''
					           ELSE ISNULL(CT.HouseID_4,'''') END+ '' '' +
				          CASE WHEN ISNULL(CT.Village_4,'''') = '''' THEN ''''
						       ELSE ISNULL(CT.Village_4,'''') END+ '' '' +
                          CASE WHEN ISNULL(CT.Moo_4,'''') = '''' THEN ''''
                               ELSE ''หมู่''+CT.Moo_4  END+'' ''+
						  CASE WHEN ISNULL(CT.Soi_4,'''') = '''' THEN ''''
							   ELSE  ''ซ.''+CT.Soi_4  END+ '' '' +
					      CASE WHEN ISNULL(CT.Road_4,'''') = '''' THEN ''''
							   ELSE  ''ถ.''+CT.Road_4 END+'' ''+
				          CASE WHEN ISNULL(CT.SubDistrict_4,'''') = '''' THEN ''''
					           ELSE (CASE WHEN CT.Province_4 like ''%กรุงเทพ%'' THEN
									          ''แขวง''+CT.SubDistrict_4
								         ELSE ''ตำบล''+CT.SubDistrict_4 END) END */	                      
     ,''AddressCus2'' =   '''' /* CASE WHEN ISNULL(CT.District_4,'''') = '''' THEN ''''
                                ELSE (CASE WHEN CT.Province_4 like ''%กรุงเทพ%''THEN
                                                ''เขต''+CT.District_4
                                           ELSE ''อำเภอ''+CT.District_4 END) END+'' ''+
                           CASE WHEN ISNULL(CT.Province_4,'''') = '''' THEN ''''
                                ELSE (CASE WHEN CT.Province_4 like ''%กรุงเทพ%'' THEN
                                           CT.Province_4
                                           ELSE ''จังหวัด''+CT.Province_4 END) +'' ''+
                           ISNULL(CT.PostalCode_4,'''') END */
	, ''SumAmt'' = '''' --ISNULL(PD1.SumAmt1,0)+ISNULL(PD2.SumAmt2,0)
	, ''SumAmtText'' = '''' --dbo.fnBHT_BahtText(ISNULL(PD1.SumAmt1,0)+ISNULL(PD2.SumAmt2,0))

    , ''AddressProduct'' =   '''' /* CASE WHEN CP.AddressThai = '''' THEN ''''
								   ELSE ISNULL(CP.AddressThai ,'''') END+ '' '' +
							  CASE WHEN CP.BuildingThai = '''' THEN ''''
								   ELSE ISNULL(CP.BuildingThai,'''') END+ '' '' +
							  CASE WHEN CP.SoiThai = '''' THEN ''''
								   ELSE ISNULL(CP.SoiThai,'''')END+'' ''+
							  CASE WHEN ISNULL(CP.RoadThai,'''') = '''' THEN ''''
							       ELSE ISNULL(''ถนน''+CP.RoadThai,'''')END+'' ''+
					          CASE WHEN CP.SubDistrictThai = '''' THEN ''''
						           ELSE (CASE WHEN CP.ProvinceThai like ''%กรุงเทพ%'' THEN
										           ''แขวง''+CP.SubDistrictThai
									          ELSE ''ตำบล''+CP.SubDistrictThai END) END+'' ''+       
	                          CASE WHEN CP.DistrictThai = '''' THEN ''''
							       ELSE (CASE WHEN CP.ProvinceThai like ''%กรุงเทพ%'' THEN
											       ''เขต''+CP.DistrictThai
									          ELSE ''อำเภอ''+CP.DistrictThai END) END+''  ''+
					          CASE WHEN CP.ProvinceThai = '''' THEN ''''
							       ELSE (CASE WHEN CP.ProvinceThai like ''%กรุงเทพ%'' THEN
											       CP.ProvinceThai
									          ELSE ''จังหวัด''+CP.ProvinceThai END) END */ 
	,''RDate'' = '''' --ISNULL(RDate2,RDate1) '

Set @sql1= '
FROM    [SAL].[Booking] BK' --This is main table need to use below table as well
		/* LEFT OUTER JOIN [ICON_Entforms_Agreement] AG ON BK.BookingNumber = AG.BookingNumber
		LEFT OUTER JOIN [ICON_Entforms_AgreementOwner] AW ON AW.ContractNumber = AG.ContractNumber AND ISNULL(AW.Header,0) = 1 AND ISNULL(AW.IsDelete,0) = 0
		LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.Header,0) = 1 AND ISNULL(BO.IsDelete,0) = 0  AND ISNULL(BO.IsBooking,0) = 1
		LEFT OUTER JOIN [ICON_Entforms_Products] PR ON PR.ProductID = BK.ProductID
		LEFT OUTER JOIN [ICON_Entforms_Company] CP ON PR.CompanyID = CP.CompanyID
		LEFT OUTER JOIN [ICON_EntForms_Contacts] CT ON CT.ContactID = ISNULL(AW.ContactID,BO.ContactID)
		LEFT OUTER JOIN
				(
					SELECT PD.ReferentID,SUM(PD.Amount) AS SumAmt1,MAX(TR.RDate) AS RDate1
					FROM [ICON_Payment_TmpReceipt] TR  
						LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
					WHERE PD.PaymentType IN ('+@PaymentType+') '

					--เลือกระหว่างวันที่
					IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						SET @sql1=@sql1+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period Between  '''+@PeriodStart+''' AND '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period >= '''+@PeriodStart+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period <= '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' '

					--เลือกระหว่างวันที่
					IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						SET @sql1=@sql1+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

					SET @sql1=@sql1+')' 


	Set @sql1=@sql1+ '
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
						SET @sql1=@sql1+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period Between  '''+@PeriodStart+''' AND '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')<>'') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period >= '''+@PeriodStart+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')<>'')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' AND  (PD.Period <= '''+@PeriodEnd+''') '
					if (ISNULL(@PaymentType2,'')<>'')AND(ISNULL(@PeriodStart,'')='') AND (ISNULL(@PeriodEnd,'')='')
					   set @sql1=@sql1+' OR (PD.PaymentType = '+@PaymentType2+' '

					
					--เลือกระหว่างวันที่
					IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						SET @sql1=@sql1+' AND (TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

					SET @sql1=@sql1+')'

	Set  @sql2='
					Group By PD.ReferentID
				)PD2 ON PD2.ReferentID = BK.BookingNumber

WHERE (BK.CancelDate IS NULL OR (AG.ContractNumber IS NOT NULL AND AG.CancelDate IS NULL AND BK.CancelDate IS NULL)) 
	'
if(Isnull(@CompanyID,'')<>'')set @sql2=@sql2+' and(PR.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql2=@sql2+' and(BK.ProductID = '''+@ProductID+''')'
if(Isnull(@UnitNumber,'')<>'')set @sql2=@sql2+' and(BK.UnitNumber = '''+@UnitNumber+''')'

set @sql2=@sql2+' ORDER BY BK.BookingNumber,AG.ContractNumber ' */

--exec(@sql+@sql1+@sql2)
--SELECT(@sql+@sql1+@sql2)

exec(@sql+@sql1) -- This is for temp mapping only. Actual mapping need to uncomment line 178 and 179


GO
