SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--  [dbo].[AP2SP_RP_FI_002_1]'','2010-07-01','10079','Administrator Account'
--  [dbo].[AP2SP_RP_FI_002_1]'','2010-2-03','10022',''
--  [dbo].[AP2SP_RP_FI_002_1]'','2009-10-06','10019',''
--  [dbo].[AP2SP_RP_FI_002_1]'','2009-10-31','10049',''

ALTER PROCEDURE [dbo].[AP2SP_RP_FI_002_1]
    @CompanyID  nvarchar(20),
	@DateStart	datetime, 
	@ProductID	nvarchar(15) = '',
	@UserName	nvarchar(50) = ''

AS

DECLARE @DateEndInStore Datetime
IF(YEAR(@DateStart)  = 7000)  SET @DateEndInStore = GetDate()
IF(YEAR(@DateStart) <> 7000)  SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateStart)

Declare @sql nvarchar(max)
Set @sql = '
SELECT	''ProductID'' = '''' --UN.ProductID
		,''ModelType'' = '''' --ISNULL(MM.TypeofRealEstate,'''')+''-''+ISNULL(TY.TypeDescription,'''')
		,''UnitNumber'' = '''' --Count(UN.UnitNumber) AS UnitNumber
		,''Area'' = '''' --ISNULL(SUM(ISNULL(AreaFromPFB,UN.AreaFromRE)),0)
		,''SellingPrice'' = '''' /* SUM(CASE	WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NULL AND TF.TransferDate IS NULL THEN BK.SellingPrice
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NULL THEN AG.SellingPrice
								WHEN BK.BookingDate IS NOT NULL AND AG.ContractDate IS NOT NULL AND TF.TransferDate IS NOT NULL THEN AG.SellingPrice
								ELSE 0 END) */
FROM	[PRJ].[Unit] UN' --This is main table need to include table below as well
		/* LEFT OUTER JOIN [PRJ].[Model] MM ON MM.ModelID = UN.ModelID
		LEFT OUTER JOIN [MST].[TypeOfRealEstate] TY ON TY.TypeID = MM.TypeOfRealEstate 
		LEFT OUTER JOIN [SAL].[Booking] BK ON BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber 
														AND (BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
														AND (BK.CancelDate IS NULL OR BK.CancelDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
		LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.BookingNumber = BK.BookingNumber 
														AND (AG.ApproveDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
														AND (AG.CancelDate IS NULL OR AG.CancelDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = AG.ContractNumber
														AND (TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')

WHERE	1=1 
	' 

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(CO.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(UN.ProductID = '''+@ProductID+''')'

set @sql=@sql+' GROUP BY UN.ProductID,MM.TypeofRealEstate,TY.TypeDescription ' */

exec(@sql)



GO
