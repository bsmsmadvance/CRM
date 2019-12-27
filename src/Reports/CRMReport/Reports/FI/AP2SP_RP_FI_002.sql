SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--  [dbo].[AP2SP_RP_FI_002]'','2013-04-29','70002','Administrator Account'
--  [dbo].[AP2SP_RP_FI_002]'','2010-07-01','10074',''
--  [dbo].[AP2SP_RP_FI_002]'','2010-2-03','10022',''
--  [dbo].[AP2SP_RP_FI_002]'','2009-10-06','10019',''
--  [dbo].[AP2SP_RP_FI_002]'','2009-10-31','10049',''

CREATE PROCEDURE [dbo].[AP2SP_RP_FI_002]
    @CompanyID  nvarchar(20),
	@DateStart	datetime, 
	@ProductID	nvarchar(15) = '',
	@UserName	nvarchar(50) = ''

AS

DECLARE @DateEndInStore Datetime
IF(YEAR(@DateStart)  = 7000)  SET @DateEndInStore = GetDate()
IF(YEAR(@DateStart) <> 7000)  SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateStart)

Declare @sql nvarchar(max)
/* Set @sql = '

DECLARE @B Table(BookingAmount money,ReferentID nvarchar(50))
INSERT INTO @B
	SELECT SUM(Pay.Amount) AS BookingAmount,Pay.ReferentID
	FROM 
	(
		SELECT  PD.Amount
			   ,PD.PaymentType
			   ,PD.Period,PD.RCReferent,PD.ReferentID
		FROM   Icon_Payment_PaymentDetails PD
			   LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
			   LEFT OUTER JOIN ICON_EntForms_Booking B ON PD.ReferentID =  B.BookingNumber
		WHERE  PD.PaymentType = ''4'' AND TR.CancelDate IS NULL '
        IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND(TR.ProductID = '''+@ProductID+''') '
	Set @sql = @sql+' ) AS Pay GROUP BY Pay.ReferentID ' */

/* Set @sql = @sql+'
DECLARE @AGT Table(TotalAmount money,ReferentID nvarchar(50))
INSERT INTO @AGT
    SELECT SUM(Pay.Amount) AS TotalAmount,Pay.ReferentID
	FROM 
	(
		SELECT  PD.Amount
			   ,PD.PaymentType
			   ,PD.Period,PD.RCReferent,PD.ReferentID
		FROM  Icon_Payment_PaymentDetails PD
			  LEFT OUTER JOIN Icon_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
			  LEFT OUTER JOIN ICON_EntForms_Agreement A ON PD.ReferentID =  A.ContractNumber
              INNER JOIN Icon_EntForms_PaymentDetail PS ON PS.ServiceCode = PD.PaymentType AND ISNULL(PS.Payment,0) IN (''0'',''4'',''6'')
		WHERE  PD.PaymentType NOT LIKE ''TR%'' AND PD.PaymentType NOT IN(''A08'') AND TR.CancelDate IS NULL '
	    IF(ISNULL(@ProductID,'')<>'')Set @sql = @sql+' AND(TR.ProductID = '''+@ProductID+''') ' 
	 Set @sql = @sql+' ) AS Pay GROUP BY Pay.ReferentID ' */

Set @sql = @sql+ '

SELECT	''CompanyNameThai'' = '''' --CO.CompanyNameThai
		,''ProjectName'' = '''' --ISNULL(PR.ProductID,'''')+''-''+ISNULL(PR.Project,'''')
		,''ProductID'' = '''' --UN.ProductID
		,''ModelType'' = '''' --ISNULL(MM.TypeofRealEstate,'''')+''-''+ISNULL(TY.TypeDescription,'''')
		,''UnitNumber'' = '''' --UN.UnitNumber
        ,''AddressNumber'' = '''' --UN.AddressNumber
		,''HomeType'' = '''' --MM.ModelHomeThai
		,''TitledeedNumber'' = '''' --dbo.fnGenUnitTitledeedNumber(UN.ProductID,UN.UnitNumber)
		,''LandNumber'' = '''' --dbo.fnGenUnitLandNumber(UN.ProductID,UN.UnitNumber)
		,''LandSurveyArea'' = ''''--dbo.fnGenUnitLandSurveyArea(UN.ProductID,UN.UnitNumber)
		,''Ravang'' = '''' --dbo.fnGenUnitRavang(UN.ProductID,UN.UnitNumber)
		,''Area'' = '''' --ISNULL(AreaFromPFB,UN.AreaFromRE)
		,''TextArea'' = '''' --CASE	WHEN PR.Producttype = ''โครงการแนวราบ'' THEN ''ตรว.''
								--ELSE ''ตรม.'' END
		,''UnitStatus'' = '''' /* CASE  WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NULL AND TF.TransferDate IS NULL AND (ISNULL(BK.Cancel, 0) <> 2)  THEN ''จอง''
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NULL AND (ISNULL(AG.Cancel, 0) <> 2) THEN ''ทำสัญญา''
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NULL AND TF.TransferDate IS NULL AND (ISNULL(BK.Cancel, 0) = 2)  THEN ''ย้ายแปลงจอง''
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NULL AND (ISNULL(AG.Cancel, 0) = 2) THEN ''ย้ายแปลงสญ.''
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NOT NULL THEN ''โอนกรรมสิทธิ์''
								WHEN BK.BookingDate IS NULL AND UN.UnitStatus = 0 THEN ''ห้องว่าง''
								WHEN BK.BookingDate IS NULL AND UN.UnitStatus = 1 THEN ''ห้องว่างเคยยกเลิก''
								ELSE ''ห้องว่าง'' END */
		,''OpenSale'' = '''' --CASE	WHEN UN.AssetType IN (2,4) Then ''Y''
								--ELSE ''N'' END
		,''TotalPaid'' = '''' --ISNULL(BKT.BookingAmount,0)+ISNULL(AGT.TotalAmount,0) 
		,''SellingPrice'' = '''' /* CASE	WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NULL AND TF.TransferDate IS NULL THEN BK.SellingPrice
									WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NULL THEN AG.SellingPrice
									WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NOT NULL THEN AG.SellingPrice
									ELSE 0 END */
		,''TotalSellingPrice'' = '''' --AA.TotalSellingPrice
		,''CustName''	= '''' /* CASE	WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NULL AND TF.TransferDate IS NULL THEN [dbo].[fn_GenCustBookingAllNOTitle](ISNULL(BK.BookingNumber,''1'')) 
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NULL THEN [dbo].[fn_GenCustAgreementAllNoTitle](ISNULL(AG.ContractNumber,''1''))
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NOT NULL THEN [dbo].[fn_GenCustTrasferAllNoTitle](ISNULL(TF.TransferNumber,''1''))
								END */

		,''CountUnitStatus'' = '''' /* CASE WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NULL AND TF.TransferDate IS NULL AND (ISNULL(BK.Cancel, 0) <> 2)  THEN 2 
									WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NULL AND (ISNULL(AG.Cancel, 0) <> 2) THEN 3
									WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NULL AND TF.TransferDate IS NULL AND (ISNULL(BK.Cancel, 0) = 2)  THEN 2
									WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NULL AND (ISNULL(AG.Cancel, 0) = 2) THEN 3
									WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NOT NULL THEN 4
									WHEN BK.BookingDate IS NULL AND UN.UnitStatus = 0 THEN 1 
									WHEN BK.BookingDate IS NULL AND UN.UnitStatus = 1 THEN 1
									ELSE 1 END */ '
Set @sql = @sql+'

FROM	[PRJ].[Unit] UN' --This is main table need to include below table as well
		/* LEFT OUTER JOIN [PRJ].[Model] MM ON MM.ModelID = UN.ModelID
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TY ON TY.TypeID = MM.TypeOfRealEstate 
		LEFT OUTER JOIN [PRJ].[Project] PR ON PR.ProductID = UN.ProductID
		LEFT OUTER JOIN [MST].[Company] CO ON CO.CompanyID = PR.CompanyID
		LEFT OUTER JOIN [SAL].[Booking] BK ON (BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber 
														AND (BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
														AND (BK.CancelDate IS NULL OR BK.CancelDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''))
		LEFT OUTER JOIN [SAL].[Agreement] AG ON (AG.BookingNumber = BK.BookingNumber 
														AND (AG.ApproveDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
														AND (AG.CancelDate IS NULL OR AG.CancelDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''))
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON (TF.ContractNumber = AG.ContractNumber
														AND (TF.TransferDateApprove <='''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''))
        LEFT OUTER JOIN @B BKT ON BKT.ReferentID = BK.BookingNumber
        LEFT OUTER JOIN @AGT AGT ON AGT.ReferentID = AG.ContractNumber
		LEFT OUTER JOIN 
		(	
			SELECT	A1.UnitNumber, A1.ProductID, A1.TotalSellingPrice
			FROM    ICON_EntForms_UnitPriceList AS A1 INNER JOIN
				(
				 SELECT     ProductID, UnitNumber, MAX(ActiveDate) AS MaxDate
				 FROM       [ICON_EntForms_UnitPriceList]
				 WHERE      (ActiveDate <='''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
				 GROUP BY ProductID, UnitNumber
				) AS A2 ON A1.ProductID = A2.ProductID AND A1.UnitNumber = A2.UnitNumber AND  A1.ActiveDate = A2.MaxDate
		) AS AA ON AA.UnitNumber = UN.UnitNumber AND AA.ProductID = UN.ProductID
WHERE	1=1 
	'

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(CO.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(UN.ProductID = '''+@ProductID+''')'


set @sql=@sql+'ORDER BY UN.ProductID,UN.UnitNumber ASC; ' */

exec(@sql)
--print(@sql)

GO
