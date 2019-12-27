SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- [dbo].[AP2SP_RP_AR_011] NULL,'10025','2009-01-01','2009-12-31','2009-01-01','2009-12-31','Administrator Account'
CREATE PROCEDURE [dbo].[AP2SP_RP_AR_011]
	@CompanyID nvarchar(50),
    @ProductID nvarchar(20),
	@DateStart datetime,
	@DateEnd  datetime,
    @DateStart2 datetime,
	@DateEnd2  datetime,
	@UserName nvarchar(150)
AS
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)

Declare @sql nvarchar(max)
Set @sql= '


SELECT ''CompanyName'' = '''' --C.CompanyNameThai,
	   ,''NotificationDate'' = '''' --NF.NotificationDate,
	   ,''ProductID'' = '''' --P.ProductID,
	   ,''ProjectName'' = '''' --ISNULL(P.ProductID,'''')+''-''+ISNULL(P.Project,''''),
	   ,''TypeOfRealEstate'' = '''' --M.TypeOfRealEstate,
	   ,''UnitNumber'' = '''' --U.UnitNumber,
	   ,''Customer'' = '''' --CASE WHEN T.TransferNumber IS NULL THEN Convert(nvarchar(40),B.ContactID)+''-''+ISNULL(B.NamesTitle,'''')+ISNULL(B.FirstName,'''')+'' ''+ISNULL(B.LastName,'''')
						   --ELSE Convert(nvarchar(40),TW.ContactID)+''-''+ISNULL(TW.NamesTitle,'''')+ISNULL(TW.FirstName,'''')+'' ''+ISNULL(TW.LastName,'''') END,
	   ,''AddressNumer'' = '''' --U.AddressNumber,
	   ,''LandPortionNumber'' = '''' --[dbo].[fnGenUnitRavang](U.ProductID,U.UnitNumber),
	   ,''TitledeedNumber'' = '''' --[dbo].[fnGenUnitTitledeedNumber](U.ProductID,U.UnitNumber),
	   ,''LandNumber'' = '''' --[dbo].[fnGenUnitLandNumber](U.ProductID,U.UnitNumber),
	   ,''LandSurveyArea'' = '''' --[dbo].[fnGenUnitLandSurveyArea](U.ProductID,U.UnitNumber),
	   ,''Transfer_Area'' = '''' --ISNULL(T.LandSize,0),
	   ,''Contract_Area'' = '''' --Convert(Decimal(12,2),ISNULL(BK.StandardArea,0)),
	   ,''Difference_Area'' = '''' --ISNULL(T.IncreasingArea,0) ,
	   ,''PricePerUnit'' = '''' --ISNULL(T.UnitIncreasingAreaPrice,0),
	   ,''PayAmount'' = '''' --ISNULL(T.IncreasingAreaPrice,0),
	   ,''Contract_Price'' = '''' --ISNULL(A.SellingPrice,0),
	   ,''Transfert_Price'' = '''' --ISNULL(T.NetSalePrice,0)
  
FROM   [SAL].[Agreement] A ' -- This is temp table need to start from below
       /* [ICON_EntForms_NotificationTranfer] NF LEFT OUTER JOIN
       [SAL].[Agreement] A ON NF.ContractNumber = A.ContractNumber LEFT OUTER JOIN
	   [SAL].[Booking] BK ON A.BookingNumber = BK.BookingNumber LEFT OUTER JOIN
       [SAL].[AgreementOwner] B ON B.ContractNumber = A.ContractNumber AND B.Header = 1 AND ISNULL(B.IsDelete,0) = 0 LEFT OUTER JOIN
	   [ICON_EntForms_Transfer] T ON A.ContractNumber = T.ContractNumber LEFT OUTER JOIN
	   [ICON_EntForms_TransferOwner]TW ON T.TransferNumber = TW.TransferNumber LEFT OUTER JOIN
	   [PRJ].[Unit] U ON U.UnitNumber = A.UnitNumber AND U.ProductID = A.ProductID LEFT OUTER JOIN
       [PRJ].[Model] M ON M.ProductID = U.ProductID AND M.ModelID = U.ModelID LEFT OUTER JOIN
       [PRJ].[Project] P ON P.ProductID = M.ProductID LEFT OUTER JOIN
	   [MST].[Company] C ON C.CompanyID = P.CompanyID 

WHERE 1=1
	'

IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''')'
IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(P.ProductID = '''+@ProductID+''')'
IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
       set @sql=@sql+'and(NF.DateInLetter Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
       set @sql=@sql+'and(NF.NotificationDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'

set @sql=@sql+'ORDER BY P.ProductID,U.UnitNumber,Customer' */
--print (@sql)

GO
