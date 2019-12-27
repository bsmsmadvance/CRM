SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RP_Master_001]
    @ProjectNo  NVARCHAR(10),
    @ProjectNameTH  NVARCHAR(100),	
    @BrandID  NVARCHAR(36),
    @CompanyID  NVARCHAR(36),
    @ProductTypeKey NVARCHAR(5),
	@ProjectStatusKey NVARCHAR(20),
    @isActive BIT
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @sql nvarchar(max)

SET @sql = 'SELECT  P.ProjectNo
        , P.ProjectNameTH
        , P.ProjectNameEN
        , ''BrandName'' = B.Name
        , ''CompanyName'' = C.NameTH
        , ''ProjectType'' = MCPT.Name
        , ''ProjectStatus'' = MCPS.Name
        , ''UnitVacant'' = [dbo].[fn_GetUnitVacantCountFromProjectID] (P.ID)
        , ''UnitSold'' = [dbo].[fn_GetUnitSoldCountFromProjectID] (P.ID)
        , ''UnitTransfer'' = [dbo].[fn_GetUnitTransferCountFromProjectID] (P.ID)
FROM	[PRJ].[Project] P  WITH (NOLOCK)
		LEFT OUTER JOIN [MST].[Brand] B WITH (NOLOCK) ON B.ID = P.BrandID
		LEFT OUTER JOIN [MST].[Company] C WITH (NOLOCK) ON C.ID = P.CompanyID
		LEFT OUTER JOIN [MST].[MasterCenter] MCPT WITH (NOLOCK) ON MCPT.ID = P.ProductTypeMasterCenterID 
		LEFT OUTER JOIN [MST].[MasterCenter] MCPS WITH (NOLOCK) ON MCPS.ID = P.ProjectStatusMasterCenterID
        --LEFT OUTER JOIN @UnitCount UC ON UC.ProjectID = P.ID
WHERE 1=1'


IF(ISNULL(@ProjectStatusKey,'')<>'')
BEGIN
    set @sql=@sql+' AND (MCPS.[Key] IN '+@ProjectStatusKey+')'
END

IF(@isActive = 1) 
BEGIN
    SET @sql=@sql + ' AND (MCPS.[Key] = 0 OR MCPS.[Key] = 1 OR MCPS.[Key] = 2 OR MCPS.[Key] IS NULL)'
END
ELSE IF(@isActive = 0)
BEGIN 
    SET @sql=@sql + ' AND (MCPS.[Key] = 3)'
END
ELSE IF(@isActive IS NULL)
BEGIN 
    SET @sql=@sql + ' AND (MCPS.[Key] = 0 OR MCPS.[Key] = 1 OR MCPS.[Key] = 2 OR MCPS.[Key] = 3 OR MCPS.[Key] IS NULL)'
END

IF(ISNULL(@ProjectNo,'')<>'')
BEGIN
    set @sql=@sql+' AND (P.ProjectNo = '''+@ProjectNo+''') '
END

IF(ISNULL(@ProjectNameTH,'')<>'')
BEGIN
    set @sql=@sql+' AND (P.ProjectNameTH LIKE '''+'%'+@ProjectNameTH+'%'+''')'
END

IF(ISNULL(@BrandID,'')<>'')
BEGIN
    set @sql=@sql+' AND (B.ID = '''+@BrandID+''')'
END

IF(ISNULL(@CompanyID,'')<>'')
BEGIN
    set @sql=@sql+' AND (C.ID = '''+@CompanyID+''')'
END

IF(ISNULL(@ProductTypeKey,'')<>'')
BEGIN
    set @sql=@sql+' AND (MCPT.[Key] = '''+@ProductTypeKey+''')'
END

exec(@sql)

GO
