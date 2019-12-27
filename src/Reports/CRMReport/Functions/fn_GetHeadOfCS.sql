SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--SELECT [dbo].[fn_GetHeadOfCS]('10091','T')
ALTER FUNCTION [dbo].[fn_GetHeadOfCS](
	@ProductID nvarchar(20),
	@HCSType nvarchar(20)
)
RETURNS nvarchar(max)
BEGIN

	DECLARE @EmployeeID nvarchar(10), @EmployeeName nvarchar(300), @BUID nvarchar(10), @BrandID nvarchar(10);
	SELECT @BUID=PType,@BrandID=BrandID FROM [dbo].[ICON_EntForms_Products] WHERE ProductID=@ProductID;


	SELECT @EmployeeID=ISNULL(EmployeeID,''),@EmployeeName=ISNULL(DisplayName,'')
	FROM dbo.vw_ZPROM_HeadOfCS A
		LEFT OUTER JOIN [dbo].[Users] B ON A.HCSID=B.UserID
	WHERE BUID = @BUID 
		AND BrandID = @BrandID
		AND ProductID = @ProductID
		AND IsActive = 1
		AND HCSType=@HCSType;

	IF @EmployeeID IS NOT NULL
		RETURN @EmployeeID + '|' + @EmployeeName;

	SELECT @EmployeeID=ISNULL(EmployeeID,''),@EmployeeName=ISNULL(DisplayName,'')
	FROM dbo.vw_ZPROM_HeadOfCS A
		LEFT OUTER JOIN [dbo].[Users] B ON A.HCSID=B.UserID
	WHERE BUID = @BUID
		AND BrandID = @BrandID
		AND ProductID = 'ALL' 
		AND IsActive = 1
		AND HCSType=@HCSType;

	IF @EmployeeID IS NOT NULL
		RETURN @EmployeeID + '|' + @EmployeeName;

	SELECT @EmployeeID=ISNULL(EmployeeID,''),@EmployeeName=ISNULL(DisplayName,'')
	FROM dbo.vw_ZPROM_HeadOfCS A
		LEFT OUTER JOIN [dbo].[Users] B ON A.HCSID=B.UserID
	WHERE BUID = @BUID
		AND BrandID = 'ALL' 
		AND ProductID = 'ALL' 
		AND IsActive = 1
		AND HCSType=@HCSType;

	IF @EmployeeID IS NOT NULL
		RETURN @EmployeeID + '|' + @EmployeeName;
		
	
	---------------------------------------------------
	SELECT @EmployeeID=ISNULL(EmployeeID,''),@EmployeeName=ISNULL(DisplayName,'')
	FROM dbo.vw_ZPROM_HeadOfCS A
		LEFT OUTER JOIN [dbo].[Users] B ON A.HCSID=B.UserID
	WHERE BUID = @BUID 
		AND BrandID = @BrandID
		AND ProductID = @ProductID
		AND IsActive = 1
		AND HCSType='S';

	IF @EmployeeID IS NOT NULL
		RETURN @EmployeeID + '|' + @EmployeeName;

	SELECT @EmployeeID=ISNULL(EmployeeID,''),@EmployeeName=ISNULL(DisplayName,'')
	FROM dbo.vw_ZPROM_HeadOfCS A
		LEFT OUTER JOIN [dbo].[Users] B ON A.HCSID=B.UserID
	WHERE BUID = @BUID
		AND BrandID = @BrandID
		AND ProductID = 'ALL' 
		AND IsActive = 1
		AND HCSType='S';

	IF @EmployeeID IS NOT NULL
		RETURN @EmployeeID + '|' + @EmployeeName;

	SELECT @EmployeeID=ISNULL(EmployeeID,''),@EmployeeName=ISNULL(DisplayName,'')
	FROM dbo.vw_ZPROM_HeadOfCS A
		LEFT OUTER JOIN [dbo].[Users] B ON A.HCSID=B.UserID
	WHERE BUID = @BUID
		AND BrandID = 'ALL' 
		AND ProductID = 'ALL' 
		AND IsActive = 1
		AND HCSType='S';

	IF @EmployeeID IS NOT NULL
		RETURN @EmployeeID + '|' + @EmployeeName;
	---------------------------------------------------

	RETURN NULL;

END



GO
