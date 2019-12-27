SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





--  [AP2SP_NRP_TR_004_H]NULL,'10054','2009-08-17','2009-09-21','Administrator Account'
CREATE PROC [dbo].[AP2SP_NRP_TR_004_H]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@UserName nvarchar(150)

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)


DECLARE @sql nvarchar(MAX)
Set @sql= '
SELECT ''TransferNumber'' = '''' --T.TransferNumber
    ,''CompanyName'' = '''' --C.CompanyNameThai
	,''ProjectName'' = '''' --ISNULL(P.ProductID,'''')+''-''+ISNULL(P.Project,'''')
	,''UnitNumber'' = '''' --A.UnitNumber
    ,''AddressNumber'' = '''' --U.AddressNumber
	,''TransferDateApprove'' = '''' --T.TransferDateApprove
	,''CustomerName'' = '''' --[dbo].[fn_GenCustTransferAll](T.TransferNumber)
    ,''Total'' = '''' --(T.IncomeTax+T.LocalTax+T.BusinessTax+ISNULL(TP.ValueMortgage,0)+ISNULL(TP.Transferfeetotalamount,0))-T.AllIncomeTax
	,''TaxCO'' = '''' --Isnull(T.AllIncomeTax,0)+Isnull(t.MinistryCash,0)
	,''TaxIncome'' = '''' /* CASE WHEN ISNULL(TP.IncomeTaxTotal,0) = 0 THEN T.IncomeTax
                         ELSE TP.IncomeTaxTotal END */
	,''TransferFee'' = '''' --ISNULL(TP.Transferfeetotalamount,0)
	,''BusinessTax'' = '''' /* CASE WHEN ISNULL(TP.BusinessTaxTotal,0) = 0 THEN T.BusinessTax
                           ELSE TP.BusinessTaxTotal END */
    ,''LocalTax'' = '''' /* CASE WHEN ISNULL(TP.LocalTaxTotal,0) = 0 THEN T.LocalTax 
                        ELSE TP.LocalTaxTotal END */
	,''ValueMortgages'' = '''' --ISNULL(TP.ValueMortgage,0)
	,''Other'' = ''''
	,''Remark'' = '''' --ISNULL(T.Remark,''-'')
	,''WBSNumber'' = '''' --REPLACE(REPLACE(U.wbsnumber,''CO-0'',''CO-P''),''-R-'',''-P-'')
FROM [SAL].[Agreement] A' --This is temp table. Actual table start from below
    /* [ICON_EntForms_Transfer]T LEFT OUTER JOIN
	[ICON_EntForms_Agreement]A ON T.ContractNumber = A.ContractNumber LEFT OUTER JOIN
	[ICON_EntForms_Unit]U ON A.UnitNumber = U.UnitNumber AND A.ProductID = U.ProductID LEFT OUTER JOIN
	[ICON_EntForms_Products]P ON A.ProductID = P.ProductID LEFT OUTER JOIN
	[ICON_EntForms_Company]C ON P.CompanyID = C.CompanyID LEFT OUTER JOIN
	[ICON_EntForms_TransferPayment]TP ON T.TransferNumber = TP.TransferNumber	
WHERE P.ProductType = ''โครงการแนวราบ'' '
if(Isnull(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+'and(P.ProductID = '''+@ProductID+''')'
if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEnd,120)+''')'
if(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) = 7000)  AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
		set @sql=@sql+'AND (T.TransferDateApprove BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'

set @sql=@sql+'ORDER BY A.UnitNumber ASC' */
--PRINT(@sql)
EXEC(@sql)

GO
