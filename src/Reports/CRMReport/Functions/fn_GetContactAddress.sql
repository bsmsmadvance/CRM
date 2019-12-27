SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--SELECT ProductID, AccountID, Method, RAmount FROM [dbo].[fn_GenTotalAmtRV] ('RV1802F0012')

CREATE FUNCTION [dbo].[fn_GetContactAddress]
(
	@ContactID NVARCHAR(40),
    @MasterCenterGroupKey NVARCHAR(5)
)
RETURNS @ContactAddress TABLE (HouseNoTH nvarchar(50),MooTH nvarchar(50),VillageTH nvarchar(50),SoiTH nvarchar(50),RoadTH nvarchar(50),ForeignDistrict nvarchar(50)
                                ,ForeignSubDistrict nvarchar(50),HouseNoEN nvarchar(50),MooEN nvarchar(50),VillageEN nvarchar(50),SoiEN nvarchar(50),RoadEN nvarchar(50)
                                ,ForeignProvince nvarchar(50),PostalCode nvarchar(50),SubDistrictID nvarchar(50),DistrictID nvarchar(50),ProvinceID nvarchar(50),CountryID nvarchar(50))

AS
BEGIN

    INSERT INTO @ContactAddress 
    SELECT CA.HouseNoTH
            , CA.MooTH
            , CA.VillageTH
            , CA.SoiTH
            , CA.RoadTH
            , CA.ForeignDistrict
            , CA.ForeignSubDistrict
            , CA.HouseNoEN
            , CA.MooEN
            , CA.VillageEN
            , CA.SoiEN
            , CA.RoadEN
            , CA.ForeignProvince
            , CA.PostalCode
            , CA.SubDistrictID
            , CA.DistrictID
            , CA.ProvinceID
            , CA.CountryID
    FROM [CTM].[ContactAddress] CA
    LEFT OUTER JOIN [MST].[MasterCenter] MC on MC.ID = CA.ContactAddressTypeMasterCenterID
    WHERE CA.ContactID = @ContactID AND MC.[Key] = @MasterCenterGroupKey AND MC.MasterCenterGroupKey = 'ContactAddressType'

RETURN
END

GO
