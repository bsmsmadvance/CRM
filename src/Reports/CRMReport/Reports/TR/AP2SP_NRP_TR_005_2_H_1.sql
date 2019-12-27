SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- [dbo].[AP2SP_NRP_TR_005_2_H_1] '','10020','','','Administrator Account'
CREATE PROC [dbo].[AP2SP_NRP_TR_005_2_H_1]

	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@UserName nvarchar(150)
AS
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @sql nvarchar(MAX)
SET @sql = '

SELECT	''TransferNumber'' = '''' --TC.TransferNumber
        ,''UnitNumber'' = '''' --AG.UnitNumber
        ,''Number'' = '''' --TR.Number
		,''Amount'' = '''' --TC.Amount
        ,''CompanyID'' = '''' --TC.CompanyID
        ,''CompanyNameThai'' = '''' --CO.CompanyNameThai

FROM	[SAL].[Booking] B' --This is temp table. Actual table start from below
        /* ICON_EntForms_TransferCheque TC
		LEFT OUTER JOIN ICON_EntForms_Transfer TF ON TF.TransferNumber = TC.TransferNumber
		LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN ICON_EntForms_Company CO ON CO.CompanyID = TC.CompanyID
		INNER JOIN ICON_Payment_TmpReceipt TR ON	TR.BankID = TC.Bank AND TR.BranchName = TC.Branch AND TR.DueDate = TC.DueDate
														AND TR.Number = TC.BankNumber
													AND TR.ReferentID = AG.ContractNumber AND TR.CancelDate IS NULL
		LEFT OUTER JOIN ICON_EntForms_Products PR ON PR.ProductID = AG.ProductID
WHERE	TC.ChequeOrder = 1 And TC.CompanyID <> ''0'' And PR.Producttype = ''โครงการแนวราบ''' 

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' and(PR.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(PR.ProductID = '''+@ProductID+''')'
if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (TF.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (TF.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) = 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (TF.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
		
set @sql=@sql+'ORDER BY TF.TransferDateApprove,AG.UnitNumber ASC' */
EXEC(@sql)

GO
