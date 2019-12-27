SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_PrintLabel_Eng] '60015','''A23G24'',''''','','1'
--[dbo].[AP2SP_PrintLabel_Eng] '10062','''20c18'',''15c18''','',''
--[dbo].[AP2SP_PrintLabel_Eng] '‎60015','''A23G24'',''''','Administrator Account','1'

CREATE PROC [dbo].[AP2SP_PrintLabel_Eng]

    @ProductID  nvarchar(20),
    @UnitNumber nvarchar(MAX),
	@UserName   nvarchar(40),
	@PrintLabel	nvarchar(40)
AS

DECLARE @A varchar(5)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

--set @UnitNumber=replace(@UnitNumber,'''','''''')

Declare @sql nvarchar(max)
Set @sql = '
	DECLARE @A TABLE(ProductID nvarchar(40),UnitNumber nvarchar(40),FirstName nvarchar(40),LastName nvarchar(40),Flag_Com int,ADDRESS1 nvarchar(40),ADDRESS2 nvarchar(40),ADDRESS3 nvarchar(40),ADDRESS4 nvarchar(40),ADDRESS5 nvarchar(40),ADDRESS6 nvarchar(40))
	INSERT INTO @A
	SELECT	ProductID,UnitNumber,FirstName,LastName,Flag_Com,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,ADDRESS5,ADDRESS6
	FROM	AP2SP_For_PrintLable
	WHERE	RowNo < '''+@PrintLabel+''' '

Set @sql = @sql+ '

	SELECT  ''ProductID'' = '''' --AG.ProductID
--**************************************************ตรงนี้จะส่งยังไงหนะ*****************************************
		,''UnitNumber'' = '''' --[dbo].[fn_GenUnit_PrintLable](AG.ProductID,AO.ContactID,'''+replace(@UnitNumber,'''','''''')+''')
--*******************************************************************************************
		,''FirstName'' = '''' --ISNULL(AO.NamesTitle,ISNULL(AO.NamesTitleExt,''''))+RTrim(LTrim(AO.FirstName)) AS FirstName
        ,''LastName'' = '''' --AO.LastName
		,Flag_Com = '''' --CASE WHEN CT.HouseID_4 like ''%บริษัท%'' OR CT.Village_4 like ''%บริษัท%'' THEN 1 ELSE 0 END
		,ADDRESS1 =	'''' --CASE WHEN ISNULL(CT.HouseID_4,'''') = '''' OR ISNULL(CT.HouseID_4,'''') = ''-'' THEN '''' ELSE CT.HouseID_4 END+
					--CASE WHEN ISNULL(CT.Moo_4,'''') = '''' OR ISNULL(CT.Moo_4,'''') = ''-'' THEN '''' ELSE ''Moo''+CT.Moo_4 END+''  ''+
					--CASE WHEN ISNULL(CT.Village_4,'''') = '''' OR ISNULL(CT.Village_4,'''') = ''-'' THEN '''' ELSE CT.Village_4 END+''  ''+
					--CASE WHEN ISNULL(CT.Soi_4,'''') = '''' OR ISNULL(CT.Soi_4,'''') = ''-'' THEN '''' ELSE ''Soi''+CT.Soi_4 END+''  ''+ 
					--CASE WHEN ISNULL(CT.Road_4,'''') = '''' OR ISNULL(CT.Road_4,'''') = ''-'' THEN '''' ELSE CT.Road_4+''Road'' END
		,ADDRESS2 = '''' --CASE WHEN CT.Country_4 like ''%ไทย%'' THEN ISNULL(SD.SubDistrictNameEng,'''') ELSE CASE WHEN ISNULL(CT.SubDistrict_4,'''') = '''' OR ISNULL(CT.SubDistrict_4,'''') = ''-'' THEN '' '' ELSE CT.SubDistrict_4 + '',  '' END END + '' '' +
					--CASE WHEN CT.Country_4 like ''%ไทย%'' THEN ISNULL(DT.DistrictNameEng,'''') ELSE ISNULL(CT.District_4,'''')  END+'',''
		,ADDRESS3 = '''' --CASE WHEN CT.Country_4 like ''%ไทย%'' THEN PV.ProvinceNameEng ELSE CT.Province_4  END+''  ''+
					--ISNULL(CT.PostalCode_4,'''')
		,ADDRESS4 =	'''' --CASE WHEN ISNULL(CT.HouseID_4,'''') = '''' OR ISNULL(CT.HouseID_4,'''') = ''-'' THEN '''' ELSE CT.HouseID_4 END+''  ''+
					--CASE WHEN ISNULL(CT.Moo_4,'''') = '''' OR ISNULL(CT.Moo_4,'''') = ''-''  THEN '''' ELSE ''Moo''+CT.Moo_4 END+''  ''+
					--CASE WHEN ISNULL(CT.Village_4,'''') = '''' OR ISNULL(CT.Village_4,'''') = ''-'' THEN '''' ELSE CT.Village_4 END+''  ''+
					--CASE WHEN ISNULL(CT.Soi_4,'''') = '''' OR  ISNULL(CT.Soi_4,'''') = ''-'' THEN '''' ELSE ''Soi''+CT.Soi_4 END 
		,ADDRESS5 = '''' --CASE WHEN ISNULL(CT.Road_4,'''') = '''' OR ISNULL(CT.Road_4,'''') = ''-'' THEN '''' ELSE CT.Road_4+''Road,'' END+'' ''+
					--CASE WHEN CT.Country_4 like ''%ไทย%'' THEN SD.SubDistrictNameEng ELSE CASE WHEN ISNULL(CT.SubDistrict_4,'''') = '''' OR ISNULL(CT.SubDistrict_4,'''') = ''-'' THEN '''' ELSE CT.SubDistrict_4 + '',  '' END END
		,ADDRESS6 = '''' --CASE WHEN CT.Country_4 like ''%ไทย%'' THEN DT.DistrictNameEng ELSE CT.District_4  END+'',  ''+
					--CASE WHEN CT.Country_4 like ''%ไทย%'' THEN PV.ProvinceNameEng ELSE CT.Province_4  END+''  ''+
					--ISNULL(CT.PostalCode_4,'''')
		FROM [SAL].[Agreement] AG 
			 LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON AO.ContractNumber = AG.ContractNumber AND AO.isMainOwner = ''1'' AND ISNULL(AO.IsDeleted,0) = 0
			 LEFT OUTER JOIN [CTM].[Contact] CT ON CT.ContactID = AO.ContactID
			 LEFT OUTER JOIN [MST].[Province] PV ON PV.ProvinceName = CT.Province_4
			 LEFT OUTER JOIN [MST].[District] DT ON DT.DistrictName = CT.District_4 AND DT.ProvinceID = PV.ProvinceID
			 LEFT OUTER JOIN [MST].[SubDistrict] SD ON SD.SubDistrictName = CT.SubDistrict_4 AND SD.DistrictID = DT.DistrictID AND SD.ProvinceID = PV.ProvinceID
		WHERE AG.CancelDate IS NULL 
		'
		if(Isnull(@ProductID,'')<>'')set @sql=@sql+' and(AG.ProductID = '''+@ProductID+''')'
		if(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql=@sql+' and(AG.UnitNumber IN ('+@UnitNumber+'))' 
		if(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql=@sql+' and(AG.UnitNumber = '''+@UnitNumber+''')'

		set @sql=@sql+' Group By AG.ProductID,AO.ContactID,RTrim(LTrim(AO.FirstName)),AO.LastName,CT.HouseID_4,CT.Village_4
						,CT.Moo_4,CT.Soi_4,CT.Road_4,CT.SubDistrict_4,CT.District_4,CT.Province_4,CT.PostalCode_4 
						,CT.Country_4,SD.SubDistrictNameEng,DT.DistrictNameEng,PV.ProvinceNameEng,AO.NamesTitle,AO.NamesTitleExt '
		set @sql=@sql+'
			UNION ALL
			SELECT	ProductID,UnitNumber,FirstName,LastName,Flag_Com,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,ADDRESS5,ADDRESS6
			FROM	@A 
			 '
		set @sql=@sql+' ORDER BY AG.ProductID,UnitNumber '
		exec(@sql)
		Print(@sql)










GO
