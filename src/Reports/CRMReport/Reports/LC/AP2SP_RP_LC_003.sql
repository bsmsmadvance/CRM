SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_003] NULL,'10027','2009-02-01','2009-02-28',NULL,NULL,NULL --48
--[AP2SP_RP_LC_003] NULL,'10027','','2009-06-30','',NULL           --303
--[AP2SP_RP_LC_003] NULL,'10027','2009-02-01','2009-02-28','',NULL --43
--[AP2SP_RP_LC_003] '','10042','','','2009-09-24',NULL --43
--[AP2SP_RP_LC_003] NULL,'10103','20121001','20121231','',NULL,'S3A07',NULL
--[AP2SP_RP_LC_003] NULL,'','2013-04-01','2013-06-30',NULL,'',NULL,'1'
--[AP2SP_RP_LC_003] NULL,'','2017-12-01','2017-12-31',NULL,'Administrator Account',NULL,'1' 

--DROP PROCEDURE [dbo].[AP2SP_RP_LC_003]
--GO

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_003]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart datetime,
	@DateEnd   datetime,
	@DateStart2   datetime,
	@UserName nvarchar(150),
	@UnitNumber	  nvarchar(50),
	@TransferStatus nvarchar(2)
AS

DECLARE @DateEndInStore Datetime,@A varchar(5)
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateStart2)

DECLARE @sql nvarchar(MAX)
DECLARE @sql2 nvarchar(MAX)
DECLARE @sql3 nvarchar(MAX)
DECLARE @UnitID NVARCHAR(MAX)

SET @UnitID = (SELECT ID FROM PRJ.UNIT WHERE UnitNo = @UnitNumber)

DECLARE @UPI Table(Name VARCHAR(50), Amount money, PriceUnitAmount money, PricePerUnitAmount money)
INSERT INTO @UPI(Name, Amount, PriceUnitAmount, PricePerUnitAmount)
	SELECT	'Name' = UPI.Name 
            , 'Amount' = UPI.Amount
            , 'PriceUnitAmount' = UPI.PriceUnitAmount
            , 'PricePerUnitAmount' = UPI.PricePerUnitAmount
	FROM [SAL].[Booking] B WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[UnitPrice] UP WITH (NOLOCK) ON UP.BookingID = B.ID
    LEFT OUTER JOIN [SAL].[UnitPriceItem] UPI WITH (NOLOCK) ON UPI.UnitPriceID = UP.ID
    WHERE UP.IsActive = 1 AND B.UnitID = ''+@UnitID+''

DECLARE @SellingPrice nvarchar(50) 
SET @SellingPrice = (SELECT CAST(ISNULL(Amount,0) AS nvarchar(50)) FROM @UPI WHERE Name = N'ราคาขาย')

Set @sql2= '
SELECT ''UnitNumber'' = U.UnitNo
       , ''AddressNumber'' =  U.HouseNo
	   , ''CustomerName'' =  [dbo].[fn_GenCustAgreementAllNoTitle](A.AgreementNo)
       , ''ContractNumber'' =  A.AgreementNo --สัญญาหลัก
       , ''ModelHomeEng'' =  ISNULL(M.NameEN,M.NameTH)
       , ''TitledeedNumber'' =  [dbo].[fnGenUnitTitledeedNumber](U.ID)  --เลขที่โฉนด
       , ''ContractDate'' =  A.ContractDate
	   , ''AreaSale''=  B.SaleArea	--พื้นที่วันทำสัญญา 
	   , ''TransferDate'' =  A.TransferOwnershipDate  --วันโอน
	   , ''TotalSellingPrice'' = '''' 
       , ''StandardArea'' =  ISNULL(TD.TitledeedArea,U.SaleArea)  --พื้นที่วันโอน
	   , ''TransferDateApprove'' =  T.ActualTransferDate  --วันโอนจริง
	   , ''NetSalePrice'' =  '''' /* ISNULL(AP1.PayableAmount,0)-ISNULL(T.PhusaDiscount,0)+ case when isnull(isappay,0)=1 then Isnull(a.SpacialDiscount,0)else 0 end
                            + CASE WHEN ISNULL(T.ExtraDiscount,0) < 0 THEN ISNULL(T.ExtraDiscount,0) ELSE 0 END
                            + CASE WHEN ISNULL(T.IncreasingAreaPrice,0) < 0 THEN ISNULL(T.IncreasingAreaPrice,0) ELSE 0 END */ --มูลค่าสัญญา2(ราคาบ้านในวันโอน)
	   , ''PayAll'' =  '''' --ISNULL(DC.PayAll,0.00)+ISNULL(DC1.BPayAll,0.00) --ชำระ
	   , ''Remain'' =  '''' /* ISNULL(AP1.PayableAmount,0)-ISNULL(T.PhusaDiscount,0)+ case when isnull(isappay,0)=1 then Isnull(a.SpacialDiscount,0)else 0 end
                      +CASE WHEN ISNULL(T.ExtraDiscount,0) < 0 THEN ISNULL(T.ExtraDiscount,0) ELSE 0 END
                      + CASE WHEN ISNULL(T.IncreasingAreaPrice,0) < 0 THEN ISNULL(T.IncreasingAreaPrice,0) ELSE 0 END
                      -(ISNULL(DC.PayAll,0.00)+ISNULL(DC1.BPayAll,0.00)) */ --คงเหลือ	
	   , ''PricePerArea'' =  '''' --ISNULL(T.UnitIncreasingAreaPrice,0)   --ราคาต่อหน่วย(ส่วนเพิ่มพื้นที่)
 	   , ''PriceDifference'' =  '''' --ISNULL(T.IncreasingArea,0)  --ราคาส่วนต่างพื้นที่
	   , ''DayDifference'' =  '''' --ISNULL(datediff(dw,A.TransferDate,T.TransferDateApprove),0) 
	   , ''CompanyID'' =  P.CompanyID
	   , ''CompanyName'' =  CO.NameTH
       , ''ProductID'' =  P.ProjectNo
	   , ''ProjectName'' =  ISNULL(P.ProjectNameTH,'''')
       , ''ProductType'' =  (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName](P.ProductTypeMasterCenterID, ''Name''))
	   , ''TypeOfRealEstate'' =  TE.Name
	   , ''RealName'' =  ISNULL(TE.Name,'''')
	   , ''PType'' =  ISNULL((SELECT NAME FROM MST.BG WHERE ID = P.BGID), '''')
	   , ''ProjectGroup'' = P.[Group]

FROM	[SAL].[Transfer] T  
        LEFT OUTER JOIN [SAL].[Agreement] A ON T.AgreementID = A.ID 
        LEFT OUTER JOIN [SAL].[Booking] B ON B.ID = A.BookingID 
        LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID AND U.ProjectID = A.ProjectID 
        LEFT OUTER JOIN [PRJ].[Model] M ON M.ProjectID = U.ProjectID AND M.ID = U.ModelID 
        LEFT OUTER JOIN [MST].[TypeOfRealEstate] TE ON TE.ID = M.TypeOfRealEstateID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = A.ProjectID 
        LEFT OUTER JOIN [MST].[Company] CO ON CO.ID = P.CompanyID
        LEFT OUTER JOIN [PRJ].[TitledeedDetail] TD ON TD.UnitID = U.ID'
        /* LEFT OUTER JOIN
		(
			SELECT SUM(PD.Amount) AS PayAll,PD.ReferentID
			FROM  Icon_Payment_PaymentDetails PD
				  LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
				  LEFT OUTER JOIN ICON_EntForms_Agreement A ON PD.ReferentID =  A.ContractNumber
                  INNER JOIN Icon_EntForms_PaymentDetail PS ON PS.ServiceCode = PD.PaymentType AND ISNULL(PS.Payment,0) IN (''0'',''4'',''6'')
			WHERE  PD.PaymentType NOT LIKE ''TR%'' AND PD.PaymentType NOT IN(''A08'') AND TR.CancelDate IS NULL
			GROUP BY PD.ReferentID
		) DC ON DC.ReferentID = T.ContractNumber
		LEFT OUTER JOIN
		(
			SELECT SUM(Amount) AS BPayAll,ReferentID
			FROM [ICON_Payment_PaymentDetails]
			WHERE PaymentType IN (''4'')
			GROUP BY ReferentID
		) DC1 ON DC1.ReferentID = B.BookingNumber
        LEFT OUTER JOIN
        (
			SELECT	SUM(PayableAmount) AS PayableAmount,ContractNumber
			FROM	[ICON_EntForms_AgreementPeriod]
			WHERE	PaymentType NOT LIKE ''TR%''
			GROUP BY ContractNumber
		) AP1 ON AP1.ContractNumber = A.ContractNumber 
		' */

SET @sql3 = '
WHERE 1=1 
	'
if(Isnull(@CompanyID,'')<>'')set @sql3=@sql3+'and (C.Code = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql3=@sql3+'and (P.ProjectNo = '''+@ProductID+''')'
if(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql3=@sql3+' and(U.UnitNo IN ('+@UnitNumber+'))' 
if(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql3=@sql3+' and(U.UnitNo = '''+@UnitNumber+''')'

if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql3=@sql3+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql3=@sql3+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart2) <> 1800) AND ISNULL(@DateStart2,'')<>''
		set @sql3=@sql3+'AND (A.TransferDate BETWEEN '''+Convert(nvarchar(50),@DateStart2,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore2,120)+''')'

IF(@TransferStatus = '1')set @sql3=@sql3+' and(T.TransferDateApprove IS NOT NULL)'
IF(@TransferStatus = '2')set @sql3=@sql3+' and(T.TransferDateApprove IS NULL)'


set @sql3=@sql3+' ORDER BY P.ProjectNo,U.UnitNo'

--Print( @sql + @sql2)
exec(@sql2 + @sql3)



GO
