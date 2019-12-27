SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[dbo].[AP2SP_RP_AG_010] '','''10116''','','1',NULL,'2013-06-30',''
--[dbo].[AP2SP_RP_AG_010] '',N'''40009'',''40016''','','3',NULL,'20151203','Administrator Account'

ALTER PROCEDURE [dbo].[AP2SP_RP_AG_010]
    @CompanyID  nvarchar(20),
	--@ProductID	nvarchar(15) = '',	
	@Projects	nvarchar(4000),
	@UnitNumber	nvarchar(15) ,
	@StatusAG3   nvarchar(20),
	@DateStart datetime = NULL,
	@DateEnd Datetime = null,
	@UserName	nvarchar(50) 

AS

If(@DateStart is null)Set @DateStart='19000101'
If(@DateEnd is null)Set @DateEnd='70001231'

IF @DateEnd = NULL OR @DateEnd = '' OR @DateEnd = '7000-12-31' 
	SET @DateEnd = GETDATE();

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)


Declare @sql nvarchar(max)
Set @sql = '

SELECT	''ProductID'' = '''' --AG.ProductID
        ,''UnitNumber'' = '''' --AG.UnitNumber
		,''ContractNumber'' = '''' --AG.ContractNumber
		,''ContactID'' = '''' --AO.ContactID
		,''CustomerName'' = '''' --ISNULL(AO.FirstName,'''')+''  ''+ISNULL(AO.LastName,'''')
		,''Price'' = '''' --AG.SellingPrice
		,''BookingDate'' = '''' --BK.BookingDate
		,''ContractDueDate'' = '''' --AG.ContractDate
		,''RDate'' = '''' --CASE WHEN (AG.ApproveDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') THEN AG.ApproveDate END
		,''ContractAmount'' = '''' --ISNULL(AG.ContractAmount, 0)
		,''Status'' = '''' /* CASE	WHEN (BK.BookingNumber IS NULL AND AG.ContractNumber IS NULL) THEN ''ว่าง''
				WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL AND BK.CancelDate IS NULL THEN ''Active''
				WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NOT NULL AND BK.CancelDate IS NULL  THEN ''Active''
				WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL AND BK.CancelDate IS NOT NULL THEN ''ยกเลิก''
				WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NOT NULL AND BK.CancelDate IS NOT NULL  THEN ''ยกเลิก''
		END AS Status */
		,''CompanyID'' = '''' --CO.CompanyID
        ,''CompanyNameThai'' = '''' --CO.CompanyNameThai
		,''Project'' = '''' --PR.Project
		,''Cancel'' = '''' --AG.CancelDate AS Cancel

		,''OverDue1'' = '''' --CASE WHEN (AG.ApproveDate IS NULL OR (AG.ApproveDate IS NOT NULL AND AG.ApproveDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''')) 
							--AND (DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') >= 1 AND DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') <= 30) THEN 1 ELSE 0 END
		,''OverDue2'' = '''' --CASE WHEN (AG.ApproveDate IS NULL OR (AG.ApproveDate IS NOT NULL AND AG.ApproveDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''')) 
							--AND (DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') >= 31 AND DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') <= 60) THEN 1 ELSE 0 END
		,''OverDue3'' = '''' --CASE WHEN (AG.ApproveDate IS NULL OR (AG.ApproveDate IS NOT NULL AND AG.ApproveDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''')) 
							--AND (DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') >= 61 AND DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') <= 90) THEN 1 ELSE 0 END
		,''OverDue4'' = '''' --CASE WHEN (AG.ApproveDate IS NULL OR (AG.ApproveDate IS NOT NULL AND AG.ApproveDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''')) 
							--AND (DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') >= 91) THEN 1 ELSE 0 END	
		,''OverDueDate'' = '''' --isnull( CASE WHEN (AG.ApproveDate IS NULL OR (AG.ApproveDate IS NOT NULL AND AG.ApproveDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''')) AND
					--DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') >= 1 THEN DATEDIFF(day, AG.ContractDate, '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ ''') END,0)
		,''ApproveDate'' = '''' --AG.ApproveDate
FROM	[SAL].[Agreement] AG' --This is main table need to use below table as well
		/* LEFT OUTER JOIN ICON_EntForms_AgreementOwner AO ON AO.ContractNumber = AG.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete,0) = 0
		LEFT OUTER JOIN ICON_EntForms_Booking BK ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = AG.ProductID
		LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PR.CompanyID

WHERE	1=1 '

IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND (PR.CompanyID = '''+@CompanyID+''')'
--IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND (AG.ProductID = '''+@ProductID+''')'

IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> ''''''))
	SET @sql=@sql+' AND (AG.ProductID IN ('+dbo.fn_AutoSQ(@Projects)+')) '

IF(ISNULL(@UnitNumber,'')<>'')set @sql=@sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')'


IF(ISNULL(@StatusAG3,'0)') = '1')
	set @sql=@sql+' AND (AG.CancelDate IS NULL) '
IF(ISNULL(@StatusAG3,'0') = '2')
	set @sql=@sql+' AND (AG.CancelDate IS NOT NULL) '
IF(ISNULL(@StatusAG3,'0') = '3')
	set @sql=@sql+' AND ((AG.CancelDate IS NULL) AND (AG.ApproveDate IS NULL OR (AG.ApproveDate > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+ '''))) '
IF(ISNULL(@StatusAG3,'0') = '4')
	set @sql=@sql+' AND ((AG.CancelDate IS NULL) AND (AG.ApproveDate IS NOT NULL)) '


IF (YEAR(@DateEnd) <> 7000) 
SET @sql=@sql+'AND ((AG.ContractDate < '''+Convert(nvarchar(50),dbo.fn_ClearTime(@DateEnd),120)+''')) '		


SET @sql=@sql+'ORDER BY AG.ProductID,AG.UnitNumber ASC;' */



exec(@sql)
Print(@sql)


GO
