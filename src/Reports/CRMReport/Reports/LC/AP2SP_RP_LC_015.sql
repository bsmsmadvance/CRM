SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_LC_015] '','10060','','','Administrator Account'

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_015]
    @CompanyID  nvarchar(20),
	@ProductID	nvarchar(15) = '',
	@UnitNumber	nvarchar(15) = '',
    @StatusAG   nvarchar(20),
	@UserName	nvarchar(50) = ''

AS

Declare @sql1 nvarchar(max)
/*Set @sql1 = '

DECLARE @TableAddress Table (	
	ContactID varchar(30),CustomerName varchar(500),CurrentAddress varchar(1000),Moo varchar(100)
	,Village varchar(255),Soi varchar(100),Road varchar(255),SubDistrict varchar(255),District varchar(255)
	,Province varchar(255),PostCode varchar(100),ContractNumber varchar(40),ProductID varchar(10),UnitNumber varchar(20)
	,Custel varchar(100),LastStatus varchar(100)
)
INSERT INTO @TableAddress (
	ContactID,CustomerName,CurrentAddress,Moo,Village,Soi,Road,SubDistrict
	,District,Province,PostCode,ContractNumber,ProductID,UnitNumber,Custel,LastStatus
)

SELECT	DISTINCT 
		ContactID = CT.ContactID
		,CustomerName = ISNULL(CT.FirstName,'''')+'' ''+ISNULL(CT.LastName,'''')
		,CurrentAddress = CT.HouseID_4
		,Moo = CT.Moo_4
		,Village = CT.Village_4
		,Soi = CT.Soi_4
		,Road = CT.Road_4
		,SubDistrict = CT.SubDistrict_4
		,District = CT.District_4
		,Province = CT.Province_4
		,PostCode = CASE WHEN CT.PostalCode_4 = '''' OR CT.PostalCode_4 IS NULL OR CT.PostalCode_4 = ''-'' THEN DS.Postcode ELSE CT.PostalCode_4 END
		,ContractNumber = ISNULL(AG.AgreementNo,BK.BookingNumber)
		,ProductID = UN.ProductID
		,UnitNumber = UN.UnitNumber
		,Custel = ISNULL(AO.Mobile,BO.Mobile)
		,LastStatus = CASE WHEN T.TransferNumber IS NOT NULL THEN ''โอนกรรมสิทธิ์'' 
					WHEN AG.AgreementNo IS NOT NULL AND T.TransferNumber IS NULL THEN ''สัญญา'' 
					WHEN BK.BookingNumber IS NOT NULL AND AG.AgreementNo IS NULL THEN ''จอง'' 
					WHEN BK.BookingNumber IS NULL THEN ''ห้องว่าง'' END
FROM	[PRJ].[Unit] UN 
		LEFT OUTER JOIN [SAL].[Booking] BK ON BK.ProjectID = UN.ProjectID AND BK.UnitID = UN.ID AND BK.CancelDate IS NULL
		LEFT OUTER JOIN [SAL].[BookingOwner] BO ON BO.BookingID = BK.ID AND ISNULL(BO.IsDeleted,0) = 0 AND BO.IsMainHeader = 1 
		LEFT OUTER JOIN [SAL].[Agreement] AG ON AG.BookingID = BK.ID  AND AG.CancelDate IS NULL
		LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON AO.AgreementID = AG.ID AND ISNULL(AO.IsDeleted,0) = 0 AND AO.IsMainHeader = 1
		LEFT OUTER JOIN [SAL].[Transfer] T ON AG.ID = T.AgreementID
		LEFT OUTER JOIN [CTM].[Contact] CT ON CT.ID = CASE WHEN AG.AgreementNo IS NULL THEN BO.FromContactID ELSE AO.FromContactID END 
        LEFT OUTER JOIN 
        (
            SELECT CA.ContactID, CA.HouseNoTH, CA.MooTH, CA.VillageTH, CA.SoiTH, CA.RoadTH, S.NameTH AS SubDistrictName, D.NameTH AS DistrictName, P.NameTH AS ProvinceName
            FROM [CTM].[ContactAddress] CA
            LEFT OUTER JOIN [MST].[MasterCenter] MC ON MC.ID = CA.ContactAddressTypeMasterCenterID
            LEFT OUTER JOIN [MST].[SubDistrict] S ON S.ID = CA.SubDistrictID
            LEFT OUTER JOIN [MST].[District] D ON D.ID = CA.DistrictID
            LEFT OUTER JOIN [MST].[Province] P ON P.ID = CA.ProvinceID
            WHERE MC.[Key] = 0 --ที่อยู่ที่ติดต่อได้
        ) CA ON CA.ContactID = CT.ID
		LEFT OUTER JOIN
		(	
			SELECT DISTINCT DS.ProvinceID,DistrictName,PostCode,ProvinceName
			FROM [MST].[District] DS 
                LEFT OUTER JOIN [MST].[Province] P ON  P.ID = DS.ProvinceID
		)DS ON CT.Province_4 = DS.ProvinceName AND CT.District_4 = DS.DistrictName
WHERE	UN.UnitStatus NOT IN (0,1) 
	AND UN.AssetType IN (2,4) '
		IF(ISNULL(@ProductID,'')<>'')set @sql1=@sql1+'AND (UN.ProductID = '''+@ProductID+''')'
		IF(ISNULL(@UnitNumber,'')<>'')set @sql1=@sql1+'AND (UN.UnitNumber = '''+@UnitNumber+''')'

set @sql1=@sql1+ ' */

--This is @sql1 for temp mapping only need to remove this for actual mapping remove line 71 and use line 68
set @sql1 = '
SELECT	''CompanyNameThai'' = '''' --CO.CompanyNameThai
        ,''Project'' = '''' --PR.Project
		,''ContactID'' = '''' --AD.ContactID
		,''CustomerName'' =	'''' --AD.CustomerName
	    ,''Address1''  =	'''' /* CASE	WHEN AD.CurrentAddress = '''' THEN ''''
									ELSE ISNULL(AD.CurrentAddress,'''')  END+'' ''+
							CASE	WHEN AD.Moo = '''' THEN ''''
									ELSE ISNULL(''หมู่''+AD.Moo,'''')END+'' ''+
							CASE	WHEN AD.Village = '''' THEN ''''
									ELSE ISNULL(AD.Village,'''') END+'' ''+
							CASE	WHEN AD.Soi = '''' THEN ''''
									ELSE ISNULL(''ซอย''+AD.Soi,'''')END+'' ''+
							CASE	WHEN AD.Road = '''' THEN ''''
									ELSE ISNULL(''ถนน''+AD.Road,'''')END+'' ''+
							CASE	WHEN AD.SubDistrict = '''' THEN ''''
									ELSE (CASE	WHEN AD.Province like ''%กรุงเทพ%'' THEN ISNULL(''แขวง''+AD.SubDistrict,'''')
												ELSE ISNULL(''ตำบล''+AD.SubDistrict,'''') END) END+'' ''+ 
							CASE	WHEN AD.District = '''' THEN ''''
									ELSE	(CASE	WHEN AD.Province like ''%กรุงเทพ%'' THEN ISNULL(''เขต''+AD.District,'''')
													ELSE ISNULL(''อำเภอ''+AD.District,'''') END) END+'' ''+
							CASE	WHEN AD.Province = '''' THEN ''''
									ELSE (CASE  WHEN AD.Province like ''%กรุงเทพ%'' THEN ISNULL(AD.Province,'''')
												ELSE ISNULL(''จังหวัด''+AD.Province,'''') END)+'' ''+
									ISNULL(AD.PostCode,'''') END */
		,''CusTel'' = '''' --ISNULL(AD.CusTel,'''')
		,''ProductID'' = '''' --UN.ProductID
        ,''TypeOfRealEstate'' = '''' --MM.TypeOfRealEstate
        ,''UnitNumber'' = '''' --UN.UnitNumber
        ,''LastStatus'' = '''' --LastStatus

FROM	[PRJ].[Unit] UN' --This is actual table need to use below table as well
		/* LEFT OUTER JOIN @TableAddress AD ON  AD.ProductID = UN.ProductID AND AD.UnitNumber = UN.UnitNo 
		LEFT OUTER JOIN [PRJ].[Model] MM ON MM.ID = UN.ModelID AND MM.ProjectID = UN.ProjectID
		LEFT OUTER JOIN [PRJ].[Project] PR ON PR.ID = UN.ProjectID
		LEFT OUTER JOIN [MST].[Company] CO ON CO.ID = PR.CompanyID
WHERE	UN.UnitStatus NOT IN (0,1) 
	AND (SELECT [dbo].[fn_GetMasterCenterDetailFromFieldName] (UN.AssetTypeMasterCenterID, 'Key')) IN (2,4) 
	AND AD.ContactID IS NOT NULL '
	
	
	IF(ISNULL(@CompanyID,'')<>'')set @sql1=@sql1+' AND (PR.CompanyID = '''+@CompanyID+''')'
	IF(ISNULL(@ProductID,'')<>'')set @sql1=@sql1+' AND (UN.ProductID = '''+@ProductID+''')'
	IF(ISNULL(@UnitNumber,'')<>'')set @sql1=@sql1+' AND (UN.UnitNumber = '''+@UnitNumber+''')'
	
	set @sql1=@sql1+' ORDER BY UN.UnitNumber ' */
	
--	print (@sql1)
	exec(@sql1)



GO
