
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_002
--DECLARE
    @CompanyID NVARCHAR(20) 
  , @ProductID NVARCHAR(15) 
  , @UnitNumber NVARCHAR(15) 
  , @StatusAG NVARCHAR(20) 
  , @DateStart DATETIME 
  , @DateEnd DATETIME 
  , @UserName NVARCHAR(50) 
  , @HomeType NVARCHAR(20) 
  , @StatusProject NVARCHAR(2) 
  , @ProjectGroup NVARCHAR(5) 
  , @ProjectType2 NVARCHAR(5) 
AS
BEGIN
	SELECT '' as Sample from ReportTemplate
END
GO

exec AP2SP_RP_AG_002