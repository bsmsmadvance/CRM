SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ใช้ในการหาข้้อมูลของ MasterCenter
CREATE FUNCTION [dbo].[fn_GetMasterCenterDetailFromFieldName]
(
	@MasterCenterID nvarchar(50),
	@FieldName nvarchar(50)
)
RETURNS nvarchar(100)
AS
BEGIN

	DECLARE @result NVARCHAR(100);
	DECLARE @MasterCenter TABLE(MCName NVARCHAR(50), [Key] NVARCHAR(50), IsActive nvarchar(10), MasterCenterGroupKey NVARCHAR(50));

	INSERT INTO @MasterCenter
		SELECT	MCName = Name
                , [Key] = [Key]
                , [IsActive] = IsActive
                , MasterCenterGroupKey = MasterCenterGroupKey
		FROM	MST.MasterCenter
		WHERE	ID = @MasterCenterID 

	SET @result = CASE 
                    WHEN @FieldName = 'Name' THEN  (SELECT MCName FROM @MasterCenter)
                    WHEN @FieldName = 'Key' THEN  (SELECT [Key] FROM @MasterCenter)
                    WHEN @FieldName = 'IsActive' THEN  (SELECT [IsActive] FROM @MasterCenter)
                    WHEN @FieldName = 'MasterCenterGroupKey' THEN  (SELECT [MasterCenterGroupKey] FROM @MasterCenter)
                  END

	RETURN @result;

END














GO
