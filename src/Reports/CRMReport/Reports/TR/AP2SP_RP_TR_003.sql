SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_TR_003] '','10039','','','''002''','','07b19'
-- [dbo].[AP2SP_RP_TR_003] '','10025','','','''''','Administrator Account'

CREATE PROC [dbo].[AP2SP_RP_TR_003]

	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart datetime,
	@DateEnd datetime,
	@BankCheque nvarchar(20),
	@UserName nvarchar(150),
	@UnitNumber nvarchar(MAX)=''

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @A varchar(5)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

DECLARE @sql nvarchar(max)
SET		@sql= '
SELECT	''UnitNumber'' = '''' --AG.UnitNumber
		,''TitledeedNumber'' = '''' --[dbo].[fnGenUnitTitledeedNumber](AG.ProductID,AG.UnitNumber)
		,''AdBankName'' = '''' --BA.AdBankName
        ,''ChecqueOrder'' = '''' --TC.ChequeOrder
        ,''Bank'' = '''' --TC.Bank
        ,''Branch'' = '''' --TC.Branch
        ,''BankNumber'' = '''' --TC.BankNumber
        ,''Amount'' = '''' --TC.Amount
        ,''DueDate'' = '''' --TC.DueDate
		,''CompanyName'' = '''' --ISNULL(CO.CompanyNameThai,PR.BankProjectAP)
		,''PayIn'' = '''' -- CASE WHEN CAST(TC.CompanyID AS varchar(2)) = ''0'' Then ''นิติบุคคล''
						 --ELSE ''AP'' END
		,''Project'' = '''' --PR.Project
        ,''BankName'' = '''' --BA.BankName
        ,''Status'' = '''' --DC.Status
        ,''CompanyNameThai'' = '''' --CO2.CompanyNameThai

FROM	[SAL].[Booking] B' --This is temp table actual table start from below
        /* [ICON_EntForms_TransferCheque] TC
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.TransferNumber = TC.TransferNumber
		LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = TC.Bank
		LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = AG.ProductID
		LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = CAST(TC.CompanyID AS varchar(2))
		LEFT OUTER JOIN [ICON_EntForms_Company] CO2 ON CO2.CompanyID = PR.CompanyID
		LEFT OUTER JOIN [ICON_EntForms_DocumentCheckList] DC ON AG.ContractNumber = DC.ContractNumber 
WHERE	BA.BankID IS NOT NULL '

IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(PR.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(PR.ProductID = '''+@ProductID+''')'

IF(ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A >= 1)) set @sql=@sql+' AND (AG.UnitNumber IN ('+@UnitNumber+'))' 
IF(ISNULL(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A <= 0)) set @sql=@sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')'

IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql=@sql+' And(TF.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql=@sql+' And(TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

IF(ISNULL(@BankCheque,'''''')<>'''''')set @sql=@sql+'and(TC.Bank = '+@BankCheque+')'

set @sql=@sql+' ORDER BY AG.ProductID,AG.UnitNumber,TC.ID;' */

exec(@sql)
GO
