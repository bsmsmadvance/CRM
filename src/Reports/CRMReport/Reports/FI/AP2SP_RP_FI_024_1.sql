SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--[dbo].[AP2SP_RP_FI_024_1] '','10136','','20170628','20170628',''
--[dbo].[AP2SP_RP_FI_024_1] '','10161','BI2','','',''

CREATE PROCEDURE [dbo].[AP2SP_RP_FI_024_1]
	@CompanyID  NVARCHAR(50),
    @ProductID  NVARCHAR(20),
    @UnitNumber NVARCHAR(20),
	@DateStart  DATETIME ,
	@DateEnd    DATETIME ,	
    @UserName   NVARCHAR(150)
AS

DECLARE @DateEndInStore Datetime,@A varchar(5)
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

Declare @sql nvarchar(max)
SET @sql = '

SELECT	''CompanyID'' = '''' --PR.CompanyID
		,''CompanyName'' = '''' --C.CompanyNameThai
		,''ProductID'' = '''' --PR.ProductID
		,''ProjectName'' = '''' --PR.Project
		,''UnitNumber'' = '''' --U.UnitNumber
		,''WBSNumber'' = '''' --REPLACE(REPLACE(U.wbsnumber,''CO-0'',''CO-P''),''-R-'',''-P-'')
		,''DescriptionName'' = '''' --R.SHORT_TEXT
		,''PRNO'' = '''' /* CASE WHEN ISNULL(R.sap_prno,'''') = ''Manual PR'' THEN R.itm_StsMsg
						WHEN ISNULL(R.sap_prno,'''') = '''' THEN R.sap_msg 
						ELSE R.sap_prno END --sap_msg itm_StsMsg */
		,''Amount'' = '''' --R.C_AMT_BAPI * R.QUANTITY, R.C_AMT_BAPI,R.QUANTITY
		,''ContractNumber'' = '''' --AG.ContractNumber

FROM	[SAL].[Agreement] AG' --This is main table, need to use table below as well
		/* LEFT OUTER JOIN ICON_EntForms_Transfer TF ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN ICON_EntForms_Products PR ON PR.ProductID = AG.ProductID 
		LEFT OUTER JOIN ICON_EntForms_Company C ON PR.CompanyID = C.CompanyID
		LEFT OUTER JOIN ICON_EntForms_Unit U ON U.ProductID = AG.ProductID AND U.UnitNumber = AG.UnitNumber 
		LEFT OUTER JOIN [AP_CRMPRO].[dbo].[vw_ProRec_ReceiveItem] R ON AG.ContractNumber = R.DocumentID '
		

SET @sql = @sql + '
WHERE AG.CancelDate IS NULL 
	AND (R.crm_itemid IN (''FGF'',''00'',''01'',''02'',''15'',''17'',''2G'',''000'',''FD'') 
			or (textb01 like ''%Cash Back%'' or textb01 like ''%CashBack%''))
'

IF (ISNULL(@CompanyID,'')<>'')SET @sql=@sql+' AND (PR.CompanyID = '''+@CompanyID+''')'
IF (ISNULL(@ProductID,'')<>'')SET @sql=@sql+' AND (PR.ProductID = '''+@ProductID+''')'
IF (ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A >= 1)) SET @sql=@sql+' AND (AG.UnitNumber IN ('+@UnitNumber+'))' 
IF (ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A <= 0)) SET @sql=@sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')'

IF (YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   SET @sql=@sql+' AND (TF.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF (YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   SET @sql=@sql+' AND (TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'

SET @sql = @sql+ ' 
ORDER BY AG.ProductID, AG.UnitNumber ASC;' */

EXEC(@sql)
--Print(@sql)

GO
