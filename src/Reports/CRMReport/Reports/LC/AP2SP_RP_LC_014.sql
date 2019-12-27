SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_014] NULL,'10056','2009-05-01','2009-11-01','','','Administrator Account',NULL

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_014]
      @CompanyID  nvarchar(20)    
    , @ProductID  nvarchar(20)
	, @DateStart datetime
	, @DateEnd   datetime
    , @DateStart2 datetime
	, @DateEnd2   datetime
    , @UserName nvarchar(150)
    , @UnitNumber nvarchar(50)

AS

DECLARE @DateEndInStore Datetime,@A varchar(5),@DateEndInStore2 Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)

Declare @sql nvarchar(max)
Set @sql= '

SELECT  ''CompanyID'' = '''' --PR.CompanyID
        , ''CompanyName'' = '''' --CO.CompanyNameThai
        , ''ProductID'' = '''' --PR.ProductID
		, ''ReferenceID'' = '''' --UH.ReferentID
        , ''ProjectName'' = '''' --ISNULL(PR.ProductID,'''')+''-''+ISNULL(PR.Project,'''')
        , ''OperateDate'' = '''' --UH.OperateDate
		, ''OperateBy'' = '''' --US.FirstName 
        , ''ModelType'' = '''' --MM1.TypeOfRealEstate
-- ข้อมูลเก่า
        , ''CustNameOld'' =  '''' --ISNULL(CONVERT(nvarchar(50),AO1.ContactID),'''')+''-''+ISNULL(AO1.FirstName,'''')+'' ''+ISNULL(AO1.LastName,'''')
		, ''UnitNumberOld'' = '''' --AG1.UnitNumber 
		, ''ContractOld'' =  '''' --UH.ReferentID
        , ''PriceOld'' ='''' -- AG1.SellingPrice
		, ''TotalPaidAmountOld'' = '''' --ISNULL(DC.TotalPaidAmountOld,0)+ISNULL(DC1.TotalPaidAmountOld1,0)
-- ข้อมูลใหม่      
        ,''CustNameNew'' =  '''' --ISNULL(CONVERT(nvarchar(50),AO2.ContactID),'''')+''-''+ISNULL(AO2.FirstName,'''')+'' ''+ISNULL(AO2.LastName,'''')
	    ,''ProductNew'' =  '''' --UH.NewProductID                                            
        ,''TypeNew''= '''' --MM2.TypeOfRealEstate                                        
		,''UnitNumberNew'' = '''' --UH.NewData 
		,''ContractNew'' = '''' --AG2.ContractNumber                                          
        ,''PriceNew'' =  '''' --AG2.SellingPrice 
        ,''TotalPaidAmount'' = '''' --ISNULL(DC.TotalPaidAmountOld,0)+ISNULL(DC1.TotalPaidAmountOld1,0)
		,''Paid'' = '''' --ISNULL(UH.Paid,0.00)
		,''ReceivePaid'' = 0.00
  
FROM	[SAL].[Booking] B' --This is temp table, actual table start from below
        /* [ICON_EntForms_UnitHistory] UH 
-- ข้อมูลเก่า
		LEFT OUTER JOIN [SAL].[Agreement] AG1 ON AG1.ContractNumber = UH.ReferentID AND UH.OperateType = ''N'' AND UH.IsApprove = 1  
		LEFT OUTER JOIN [SAL].[AgreementOwner] AO1 ON AO1.AgreementID = AG1.ID AND ISNULL(AO1.IsDeleted,0) = 0 AND ISNULL(AO1.IsMainOwner,0) = 1
		LEFT OUTER JOIN [PRJ].[Unit] UN1 ON UN1.ProductID = UH.OldProductID AND UN1.UnitNumber = UH.UnitNumber
		LEFT OUTER JOIN [PRJ].[Model] MM1 ON MM1.ID = UN1.ModelID AND MM1.ProjectID = UN1.ProjectID
		LEFT OUTER JOIN [PRJ].[Project] PR ON PR.ProjectID = AG1.ProjectID 
		LEFT OUTER JOIN [MST].[Company] CO ON CO.ID = PR.CompanyID
		LEFT OUTER JOIN [USR].[User] US ON US.UserID = UH.OperateBy
		LEFT OUTER JOIN
		(
			SELECT	SUM(Amount) AS TotalPaidAmountOld1,ReferentID
			FROM	[ICON_Payment_PaymentDetails]
			WHERE	PaymentType = ''4''
			GROUP BY ReferentID
		)DC1 ON DC1.ReferentID = AG1.BookingNumber
		LEFT OUTER JOIN
		(
			SELECT	SUM(Amount) AS TotalPaidAmountOld,ReferentID
			FROM	[ICON_Payment_PaymentDetails]
			WHERE	PaymentType IN (''5'',''6'',''8'')
			GROUP BY ReferentID
		)DC ON DC.ReferentID = UH.ReferentID

-- ข้อมูลใหม่  
		LEFT OUTER JOIN [SAL].[Agreement] AG2 ON AG2.ContractReferent = UH.ReferentID AND UH.OperateType = ''N'' AND UH.IsApprove = 1  
		LEFT OUTER JOIN [SAL].[AgreementOwner] AO2 ON AO2.AgreementID = AG2.ID AND ISNULL(AO2.IsDelete,0) = 0 AND ISNULL(AO2.Header,0) = 1
		LEFT OUTER JOIN [PRJ].[Unit] UN2 ON UN2.ProjectID = UH.NewProductID AND UN2.UnitNumber = UH.NewData
		LEFT OUTER JOIN [PRJ].[Model] MM2 ON MM2.ID = UN2.ModelID AND MM2.ProjectId = UN2.ProjectID

WHERE  UH.OperateType = ''N'' AND UH.IsApprove = 1 '
	
if(Isnull(@CompanyID,'')<>'')set @sql=@sql+'and(PR.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+'and(UH.OldProductID = '''+@ProductID+''')'
if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+'and(AG1.UnitNumber  = '''+@UnitNumber+''')'

if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>''))
						set @sql=@sql+'AND (UH.OperateDate BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'

if((YEAR(@DateStart2) <>1800) AND (YEAR(@DateEnd2) <> 7000) AND (ISNULL(@DateStart2,'')<>'') AND (ISNULL(@DateEnd2,'')<>''))
						set @sql=@sql+'AND (AG1.ApproveDate BETWEEN '''+Convert(nvarchar(50),@DateStart2,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore2,120)+''')'
--Print( @sql) */
exec( @sql)

GO
