SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_LC_005] '','10151','','2015-01-19','2015-01-19','',''
ALTER PROC [dbo].[AP2SP_RP_LC_005]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@SBUID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
    @UserName nvarchar(150),
    @UnitNumber nvarchar(50)


AS
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @sql nvarchar(MAX)
Set @sql= '

SELECT	''CompanyName'' = CO.NameTH
		,''ProjectName'' = P.ProjectNameTH
		,''SBUName'' = '''' --ISNULL(P.SBUID,'''')+''-''+ISNULL(S.SBUName,'''')
		,''TransferDate'' = T.ScheduleTransferDate
        ,''ProductID'' = A.ProjectNo
		,''UnitNumber'' = U.ProjectNo+''-''+U.UnitNo
		,''AddressNumber'' = U.HouseNo
		,''TitledeedNumber'' = CASE WHEN [dbo].[fn_GetProductTypeFromMasterCenter] (P.ProductTypeMasterCenterID) =  ''แนวราบ'' THEN
                                          [dbo].[fnGenUnitTitledeedNumber](U.ID)
                                     WHEN [dbo].[fn_GetProductTypeFromMasterCenter] (P.ProductTypeMasterCenterID) = ''โครงการแนวสูง'' THEN
                                          [dbo].[fnGenProductTitledeedNumber](U.ID) 
                                END
		,''LandNumber'' =  [dbo].[fnGenUnitLandNumber] (U.ID)
		,''ServayArea'' = [dbo].[fnGenUnitLandSurveyArea] (U.ID)
		,''AreaTitledeed'' = ISNULL(U.SaleArea,TD.TitledeedArea)
		,''BankBranch'' = '''' /* CASE WHEN	D.Status = 1 THEN ''CASH''+''/''+''โอนสด''
							ELSE	[dbo].[fn_GetBankShortName](C.BankID)+''/''+[dbo].[fn_GetBranchName](C.BranchID,C.BankID) END */

FROM	[SAL].[Transfer] T 
        LEFT OUTER JOIN [SAL].[Agreement] A ON T.ContractNumber = A.ContractNumber 
        LEFT OUTER JOIN [SAL].[CreditBanking] C ON A.ContractNumber = C.ContractNumber AND C.IsSelected = 1 AND C.IsPass=1 
        --LEFT OUTER JOIN [ICON_EntForms_DocumentCheckList] D ON T.ContractNumber = D.ContractNumber AND D.Status = 1 
        LEFT OUTER JOIN [PRJ].[Unit] U ON A.ProductID = U.ProductID AND A.UnitNumber = U.UnitNumber 
        LEFT OUTER JOIN [PRJ].[TitledeedDetail] TD ON TD.ProjectID = U.ID
        LEFT OUTER JOIN [PRJ].[Project] P ON U.ProductID = P.ProductID 
        LEFT OUTER JOIN [MST].[SBU] S ON S.SBUID = P.SBUID 
        LEFT OUTER JOIN [MST].[Company] CO ON CO.CompanyID = P.CompanyID 
WHERE 1=1 '

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+'and(P.ProjectID = '''+@ProductID+''')'
if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+'and(LTrim(RTrim(A.UnitNo)) = '''+@UnitNumber+''')'
if(Isnull(@SBUID,'')<>'')set @sql=@sql+'and(P.SBUID = '''+@SBUID+''')'
if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (T.ScheduleTransferDate BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'


--PRINT(@sql)
EXEC(@sql)

GO
