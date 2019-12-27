SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_FI_005] '20171006','20171006','','','40017',NULL,'1','Administrator Account'

ALTER PROCEDURE [dbo].[AP2SP_RP_FI_005]
	@DateStart	datetime,
	@DateEnd	datetime,
    @DateStart2	datetime,
	@DateEnd2	datetime,
	@ProductID	nvarchar(15),
    @CompanyID  nvarchar(20),
    @StatusAG   nvarchar(10)='',
	@UserName	nvarchar(50) 
	,@UnitStatus nvarchar(50) ='0'
AS
if(Isnull(@UnitStatus,'')='')Set @UnitStatus='0'
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)


Declare @sql nvarchar(max)
Set @sql = '

SELECT	''CompanyNameThai'' = '''' --CO.CompanyNameThai
        ,''ProductID'' = '''' --BK.ProductID
		,''ProjectName'' = '''' --ISNULL(PD.ProductID,'''')+'' ''+ISNULL(PD.Project,'''')
		,''UnitNumber'' = '''' --BK.UnitNumber
		,''UnitStatus'' = '''' /* CASE	WHEN  UH.OperateType = ''T'' THEN ''เปลี่ยนมือ''
								WHEN  ISNULL(BK.Cancel,0) = 1  THEN ''ยกเลิกจอง''
								WHEN  ISNULL(BK.Cancel,0) = 2  THEN ''ยกเลิกจากย้ายแปลง''
								WHEN  ISNULL(BK.Cancel,0) = 3  THEN ''ยกเลิกจากสัญญา''
								WHEN  ISNULL(BK.Cancel,0) = 4  THEN ''ยกเลิกเปลี่ยนชื่อ''
								ELSE ''จอง'' END */

		,''ContactID'' = '''' --ISNULL(BO.ContactID,'''')
		,''CustName'' = '''' --ISNULL(BO.FirstName,'''')+'' ''+ISNULL(BO.LastName,'''')
		,''BookingDate'' = '''' --BK.BookingDate
        ,''CreateDate'' = '''' --BK.CreateDate
		,''BookingNumber'' = '''' --BK.BookingNumber
		,''SellingPrice'' = '''' --BK.SellingPrice
		,''BookingPaid'' = '''' --[dbo].[fn_GenAmount_Booking](BK.BookingNumber)
		,''RecordBy'' = '''' --US.FirstName
		,''SaleName'' = '''' --ISNULL(US3.FirstName,US2.FirstName)
		,''DiscountAmount''= '''' --TotalSellingPrice-Isnull(BK.CashDiscount,0)-Isnull(BK.TransferDiscount,0)
		,''CashDiscount'' = '''' --BK.CashDiscount
        ,''TransferDiscount'' = '''' --BK.TransferDiscount
		,''PromotionPrice''= '''' --Isnull(bk.PromotionPrice,0)
		,''NetPrice''= '''' --TotalSellingPrice
FROM	[SAL].[Booking] BK' --This is main table need to include table below as well
		/* LEFT OUTER JOIN [SAL].[BookingOwner] BO ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.IsDelete,0) = 0 AND BO.Header = 1
		LEFT OUTER JOIN [USERS] US ON US.USERID = BK.ModifyBy
		LEFT OUTER JOIN [USERS] US2 ON US2.USERID = BK.SaleID
		LEFT OUTER JOIN [USERS] US3 ON US3.USERID = BK.SaleTraineeID
		LEFT OUTER JOIN 
		(
			SELECT UH.OperateType,UH.ReferentID
			FROM [ICON_Entforms_UnitHistory] UH INNER JOIN
			(
			SELECT  ReferentID,MAX(OperateDate) AS MAXDate From [ICON_Entforms_UnitHistory] 
			WHERE	OperateType = ''T'' AND isApprove = 1 
			Group BY ReferentID
			)UH1 ON UH.ReferentID = UH1.ReferentID AND UH.OperateDate = UH1.MAXDate
		)UH ON UH.ReferentID = BK.BookingNumber  
		LEFT OUTER JOIN [ICON_Entforms_Products] PD ON PD.ProductID = BK.ProductID
		LEFT OUTER JOIN [ICON_Entforms_Company] CO ON CO.CompanyID = PD.CompanyID

WHERE	1=1
	'

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(PD.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(BK.ProductID = '''+@ProductID+''')'
if(@StatusAG = '1')set @sql=@sql+' and(BK.CancelDate IS NULL)'
if(@StatusAG = '2')set @sql=@sql+' and(BK.CancelDate IS NOT NULL)'

if(Year(@DateStart) <> 1800 AND Year(@DateEnd) <> 7000 AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
set @sql=@sql+'and(BK.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
if (YEAR(@DateStart2) <> 1800 And YEAR(@DateEnd2) <> 7000 AND ISNULL(@DateStart2,'') <> '' AND ISNULL(@DateEnd2,'') <> '')
set @sql=@sql+' and(BK.CreateDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'

if(Year(@DateStart) = 1800 AND Year(@DateEnd) <> 7000 AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
set @sql=@sql+'and(BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
if(Year(@DateStart2) = 1800 AND Year(@DateEnd2) <> 7000 AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>'')
set @sql=@sql+'and(BK.CreateDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'

if(@UnitStatus <> '0')set @sql=@sql+' and('+@UnitStatus+'=(CASE	WHEN  UH.OperateType = ''T'' THEN ''5''
								WHEN  ISNULL(BK.Cancel,0) = 1  THEN ''1''
								WHEN  ISNULL(BK.Cancel,0) = 2  THEN ''2''
								WHEN  ISNULL(BK.Cancel,0) = 3  THEN ''3''
								WHEN  ISNULL(BK.Cancel,0) = 4  THEN ''4''
								ELSE ''0'' END))'

set @sql=@sql+' ORDER BY BK.ProductID,BK.BookingDate,BK.UnitNumber' */

exec( @sql)
print(@sql)


GO
