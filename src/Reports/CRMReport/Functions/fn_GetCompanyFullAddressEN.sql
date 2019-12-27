SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetCompanyFullAddressEN]
	(@CompanyID nvarchar(15))
	RETURNS varchar(max)
AS
BEGIN
	RETURN 
    (SELECT CASE WHEN AddressEN = '' THEN ''
                 ELSE ISNULL(AddressEN,'')END +' '+
            CASE WHEN BuildingEN = '' THEN ''
                 ELSE ISNULL(BuildingEN,'')END +' '+
			CASE WHEN RoadEN = '' THEN '' 
                 ELSE ISNULL('Road'+''+ RoadEN,'')END+' '+
			ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] (CP.SubDistrictID, 'NameEN'),'')+' '+ ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] (CP.DistrictID, 'NameEN'),'')+' '+ 
			ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] (CP.DistrictID, 'NameEN'),'')+' '+ ISNULL(CP.PostalCode,'')
			 
	FROM	[MST].[Company] CP	
	WHERE ID = @CompanyID)
END

GO
