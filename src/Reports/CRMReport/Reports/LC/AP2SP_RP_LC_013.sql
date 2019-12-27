SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_013] NULL,'10060',NULL,'2014-01-29','','','Administrator Account','0'

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_013]
     @CompanyID  nvarchar(20)     
    , @ProductID  nvarchar(20)
	, @DateStart datetime
	, @DateEnd   datetime
    , @UserName nvarchar(150)
    , @UnitNumber nvarchar(50)
    , @statustransfer nvarchar(20)
	, @Change2 nvarchar(20)

AS
DECLARE @DateEndInStore Datetime
SET @DateEndInStore  = [dbo].[fn_GetMaxDate](@DateEnd)
DECLARE @sql nvarchar(MAX)
Set @sql= '
SELECT  ''CompanyName'' = '''' --C.CompanyNameThai
        ,''ProductID'' = '''' --P.ProductID 
        , ''ProjectName'' = '''' --P.Project
        ,''OperateDate'' = '''' --UH.OperateDate
        ,''Type'' = ''''  /* CASE UH.OperateType WHEN ''A'' THEN ''เพิ่มชื่อ''
									    WHEN ''B'' THEN ''สละชื่อ''
									    WHEN ''T'' THEN ''โอนสิทธิ์''
									    WHEN ''N'' THEN ''ย้ายแปลง'' END */
		,''OperateType'' = '''' --UH.OperateType 
-- ข้อมูลเก่า

		,''AgreementOld'' = '''' --UH.ReferentID
		,''UnitNumberOld'' = '''' --UH.UnitNumber
        ,''TypeOld'' = '''' --M1.TypeOfRealEstate
        ,''AgreementDateOld'' =  '''' --A1.ContractDate                  
        ,''CustNameOld'' =  '''' /* CASE UH.OperateType WHEN ''N'' THEN [dbo].[fn_GenCustAgreementAll] (UH.ReferentID) 
												WHEN ''T''  THEN [dbo].[fn_GenCustChangeTransfer] (UH.ReferentID,UH.OldData)
												WHEN ''B''  THEN [dbo].[fn_GenCustChangeTransfer] (UH.ReferentID,UH.OldData)
												WHEN ''A''  THEN [dbo].[fn_GenCustChangeTransfer] (UH.ReferentID,UH.OldData) END */
        ,''PriceOld'' =  '''' --A1.SellingPrice 
-- ข้อมูลใหม่  
		,''AgreementNew'' =  '''' --CASE UH.OperateType	WHEN ''N'' THEN A3.ContractNumber  ELSE UH.ReferentID END 
        ,''UnitNumberNew'' = '''' --CASE UH.OperateType	WHEN ''N'' THEN A3.UnitNumber ELSE A1.UnitNumber END
        ,''TypeNew'' = '''' /* CASE UH.OperateType	WHEN ''N'' THEN M3.TypeOfRealEstate
											ELSE M1.TypeOfRealEstate END */
       ,''AgreementDateNew'' = '''' /* CASE UH.OperateType  WHEN ''N'' THEN A3.ContractDate
													ELSE A1.ContractDate END */                 
        ,''CustNameNew'' =  '''' /* CASE UH.OperateType WHEN ''N'' THEN [dbo].[fn_GenCustAgreementAll](A3.ContractNumber) 
												WHEN ''T'' THEN [dbo].[fn_GenCustChangeTransfer](UH.ReferentID,UH.NewData)
												WHEN ''A'' THEN [dbo].[fn_GenCustChangeTransfer](UH.ReferentID,UH.NewData)
												WHEN ''B'' THEN [dbo].[fn_GenCustChangeTransfer](UH.ReferentID,UH.NewData) END */

        ,''PriceNew'' = '''' /* CASE UH.OperateType		WHEN ''N'' THEN A3.SellingPrice
												ELSE A1.SellingPrice END */
        ,''OperateBy'' = '''' --US.FirstName 
		, ''ReceiveMoney'' =  '''' --ISNULL(UH.TotalPaidAmount,0)
		, ''ReturnMoney'' = '''' --ISNULL(UH.SuspenseAmount,0)

 
FROM  [SAL].[Booking] ' --This is temp table, actual table start from below
    /* [ICON_EntForms_UnitHistory] UH 
---JOIN ข้อมูลก่อนเปลี่ยน
      LEFT OUTER JOIN [SAL].[Agreemnt] A1 ON A1.ContractNumber = UH.ReferentID  AND UH.OperateType in (''A'',''B'',''T'',''N'') AND UH.IsApprove <> -1 
	  LEFT OUTER JOIN [PRJ].[Unit] U1 ON U1.ProjectID = A1.ProjectID AND U1.ID = A1.UnitID 
	  LEFT OUTER JOIN [PRJ].[Model] M1 ON M1.ProjectID = U1.ProjectID AND M1.ID = U1.ModelID	
---JOIN ข้อมูลหลังเปลี่ยน
      LEFT OUTER JOIN [SAL].[Agreement] A3 ON A3.ContractReferent = UH.ReferentID AND UH.OperateType = ''N'' AND UH.IsApprove <> -1 
	  LEFT OUTER JOIN [PRJ].[Unit] U3 ON U3.ProjectID = A3.ProjectID AND U3.ID = A3.UnitID 	
	  LEFT OUTER JOIN [PRJ].[Model] M3 ON M3.ProjectID = U3.ProjectID AND M3.ID = U3.ModelID	
---ใช้ร่วมกัน
      LEFT OUTER JOIN [PRJ].[Project] P ON P.ProductID = UH.OldProductID 
	  LEFT OUTER JOIN [MST].[Company] C ON C.ID = P.CompanyID 
      LEFT OUTER JOIN [USR].[User] US ON US.UserID = UH.OperateBy

WHERE (UH.RefType = 2  AND UH.IsApprove <> -1) '

if(Isnull(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+'and(UH.OldProductID = '''+@ProductID+''')'
if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+'and(UH.UnitNumber = '''+@UnitNumber+''')'
if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (UH.OperateDate BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'

if((YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (UH.OperateDate <= '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'

if(@statustransfer = '1')set @sql=@sql+'and(UH.IsApprove = 1) '
if(@statustransfer = '2')set @sql=@sql+'and(ISNULL(UH.IsApprove,0) = 0) '

if(@Change2 = '0')set @sql=@sql+'and(UH.OperateType in (''A'',''B'',''T'',''BN'')) '
if(@Change2 = '1')set @sql=@sql+'and(UH.OperateType = ''B'') '
if(@Change2 = '2')set @sql=@sql+'and(UH.OperateType = ''T'') '
if(@Change2 = '3')set @sql=@sql+'and(UH.OperateType = ''A'') '
if(@Change2 = '4')set @sql=@sql+'and(UH.OperateType = ''BN'') '


SET @sql=@sql+'ORDER BY UH.OperateDate,UH.OldProductID,UH.UnitNumber' */

EXEC(@sql)


GO
