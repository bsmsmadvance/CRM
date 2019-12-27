ALTER PROCEDURE custdb_sample
	--@recordCount VARCHAR(10)
AS
BEGIN
	SELECT ItemId, BUID, FirstName, LastName from ICON_EntForms_Leads
	
	--SELECT ActivityID , ReferentID, Remark from CRM_Activities
END
GO

exec custdb_sample