SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_LC_006] null,'10052','2011-01-01','2011-12-31',null,''
-- [dbo].[AP2SP_RP_LC_006] null,'10060','2011-09-01','2015-09-22',null,''
-- [dbo].[AP2SP_RP_LC_006] null,'10101','','2013-04-22',null,''
ALTER PROC [dbo].[AP2SP_RP_LC_006]
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),    
	@DateStart datetime,
    @DateEnd   datetime,
    @LandStatus nvarchar(10),
    @UserName  nvarchar(150)

AS


DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

Declare @sql nvarchar(max)
Set @sql= '

SELECT  ''CompanyID'' = C.Code 
	, ''CompanyName'' = C.NameTH
    , ''LandStatus'' =  (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName](TD.LandStatusMasterCenterID, ''Name''))
	, ''Status'' =  (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName](TD.LandStatusMasterCenterID, ''Name''))
	, ''ProjectName'' = P.ProjectNameTH
	, ''ProjectID'' = P.ProjectNo
    , ''SBUID'' = '''' --P.SBUID
    , ''ShortName'' = M.NameTH --ประเภท
    , ''UnitNumber'' = U.UnitNo
    , ''AddressNumber'' = U.HouseNo
    , ''TitledeedNumber'' = [dbo].[fnGenUnitTitledeedNumber] (U.ID)  --เลขที่โฉนด
    , ''LandNumber'' = [dbo].[fnGenUnitLandNumber] (U.ID)--เลขที่ดิน
    , ''LandSurveyArea'' = [dbo].[fnGenUnitLandSurveyArea] (U.ID) --หน้าสำรวจ
    , ''LandBook'' =  [dbo].[fnGenUnitLandBook] (U.ID) --เลขที่เล่ม
    , ''LandBookPage'' = [dbo].[fnGenUnitLandBookPage] (U.ID) --เลขที่หน้า
	, ''BankBranch'' = '''' --TD.LoanBankName
    , ''StatusDate'' = TD.LandStatusDate
--วันที่ปลอด
	, ''SuppTransfer'' = A.TransferOwnershipDate
	, ''TransferAct'' = T.ActualTransferDate
    , ''Producttype'' = (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName](P.ProductTypeMasterCenterID, ''Name''))
    , ''Area'' = SUM(TD.TitledeedArea)

FROM [PRJ].[Unit] U 
     LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = U.ProjectID
     LEFT OUTER JOIN [MST].[Company] C ON C.ID = P.CompanyID 
	 LEFT OUTER JOIN [PRJ].[TitledeedDetail] TD ON TD.UnitID = U.ID AND TD.ProjectID = U.ProjectID 
	 LEFT OUTER JOIN [PRJ].[Model] M ON M.ProjectID = U.ProjectID AND M.ID = U.ModelID 
	 LEFT OUTER JOIN [SAL].[Agreement] A ON U.ProjectID = A.ProjectID AND U.ID = A.UnitID AND A.CancelDate IS NULL AND A.SignAgreementDate IS NOT NULL 
	 LEFT OUTER JOIN [SAL].[Transfer] T ON T.AgreementID = A.ID 

WHERE (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (U.AssetTypeMasterCenterID, ''Key'')) IN(2,4) '

if(Isnull(@CompanyID,'')<>'') set @sql=@sql+'AND (C.Code = '''+@CompanyID+''') '
if(Isnull(@ProductID,'')<>'')
	set @sql=@sql+'AND (P.ProjectNo = '''+@ProductID+''') '
Else 
	set @sql=@sql+'AND (P.ProjectNo = '''') '

if(Isnull(@LandStatus,'')<>'') set @sql=@sql+'AND ((SELECT [Key] from MST.MasterCenter where MasterCenterGroupKey = ''LandStatus'') WHERE [Key] = '''+@LandStatus+''') '

IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
	set @sql=@sql+'AND ((TD.LandStatusDate BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+'''AND '''+Convert(nvarchar(50),@DateEndInStore,120)+'''))'
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) 
	set @sql=@sql+'AND ((TD.LandStatusDate <= '''+Convert(nvarchar(50),@DateEndInStore,120)+'''))'

set @sql=@sql+'
GROUP BY P.CompanyID,U.ProjectID,M.TypeofRealEstateID,U.UnitNo,TD.LandStatusDate,A.TransferOwnershipDate,T.ActualTransferDate,C.NameTH,P.ProjectNo
ORDER BY U.ProjectID,U.UnitNo;'

--print (@sql)
exec( @sql)


GO
