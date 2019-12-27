SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER FUNCTION [dbo].[fn_GetCompanyFullAddress]
	(@CompanyID nvarchar(15))
	RETURNS varchar(max)
AS
BEGIN
	RETURN 
    (SELECT CASE WHEN AddressTH = '' THEN ''
                 ELSE ISNULL(AddressTH,'')END +' '+
            CASE WHEN BuildingTH = '' THEN ''
                 ELSE ISNULL(BuildingTH,'')END +' '+
			CASE WHEN RoadTH = '' THEN '' 
                 ELSE ISNULL('ถนน'+''+ RoadTH,'')END+' '+
			ISNULL('แขวง'+ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] (CP.SubDistrictID, 'NameTH'),'-'),'')+' '+ ISNULL('เขต'+ISNULL([dbo].[fn_GetDistrictDetailFromFieldName] (CP.DistrictID, 'NameTH'),'-'),'')+' '+ 
			ISNULL([dbo].[fn_GetSubDistrictDetailFromFieldName] (CP.DistrictID, 'NameTH'),'')+' '+ ISNULL(CP.PostalCode,'')
			 
	FROM	[MST].[Company] CP	
	WHERE ID = @CompanyID)
END

GO
