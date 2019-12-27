SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RP_FI_035]
    @Code NVARCHAR(20),
    @CardMachineTypeKey  NVARCHAR(5),	
    @BankAccountID  NVARCHAR(36),
    @CompanyID  NVARCHAR(36),
    @ProjectID  NVARCHAR(36),
    @ProjectStatusKey  NVARCHAR(5),
    @ReceiveBy NVARCHAR(5),
	@ReceiveDateFrom datetime,
    @ReceiveDateTo datetime,
    @CardMachineStatusKey NVARCHAR(5)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

--DECLARE @EDC AS TABLE
--(Code nvarchar(10), CardMachineType nvarchar(100), BankAccount nvarchar(50), CompanyName nvarchar(200), ProjectName nvarchar(100), ProjectStatus nvarchar(100), ReceiveBy nvarchar(100), ReceiveDate nvarchar(50),
--CardMachineStatus nvarchar(100), LastModified NVARCHAR(50), LastModifiedBy nvarchar(100))

DECLARE @sql nvarchar(max)
SET @sql = 'SELECT ''Code'' = EDC.Code
        , ''CardMachineType'' = MCCM.Name
        , ''BankAccount'' = B.Alias + '' '' + BA.BankAccountNo
        , ''CompanyName'' = C.Code + ''-'' + C.NameTH
        , ''ProjectName'' = P.ProjectNo + ''-'' + P.ProjectNameTH
        , ''ProjectStatus'' = MCPS.Name
        , ''ReceiveBy'' = U1.FirstName + '' '' + U1.LastName
        , ''ReceiveDate'' = [dbo].[fnFormatDateLong] (EDC.ReceiveDate)
        , ''CardMachineStatus'' = MCCS.Name
        , ''LastModified'' = [dbo].[fnFormatDateLong] (EDC.Updated)
        , ''LastModifiedBy'' = U2.FirstName + '' '' + U2.LastName
FROM [MST].[EDC] EDC
    LEFT OUTER JOIN [MST].[MasterCenter] MCCM ON MCCM.ID = EDC.CardMachineTypeMasterCenterID AND MCCM.MasterCenterGroupKey = ''CardMachineType''
    LEFT OUTER JOIN [MST].[MasterCenter] MCCS ON MCCS.ID = EDC.CardMachineStatusMasterCenterID AND MCCS.MasterCenterGroupKey = ''CardMachineStatus''
    LEFT OUTER JOIN [MST].[Bank] B ON B.ID = EDC.BankID
    LEFT OUTER JOIN [MST].[BankAccount] BA ON BA.ID = EDC.BankAccountID
    LEFT OUTER JOIN [MST].[Company] C ON C.ID = EDC.CompanyID
    LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = EDC.ProjectID
    LEFT OUTER JOIN [MST].[MasterCenter] MCPS ON MCPS.ID = P.ProjectStatusMasterCenterID
    LEFT OUTER JOIN [USR].[User] U1 ON U1.ID = EDC.ReceiveBy
    LEFT OUTER JOIN [USR].[User] U2 ON U2.ID = EDC.UpdatedByUserID
WHERE 1=1'

IF(@Code <> '' AND @Code IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (EDC.Code LIKE ''%'+@Code+'%'') '
END

IF(@CardMachineTypeKey <> '' AND @CardMachineTypeKey IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (MCCM.[Key] = '''+@CardMachineTypeKey+''') '
END

IF(@BankAccountID <> '' AND @BankAccountID IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (EDC.BankAccountID = '''+@BankAccountID+''') '
END

IF(@CompanyID <> '' AND @CompanyID IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (EDC.CompanyID = '''+@CompanyID+''') '
END

IF(@ProjectID <> '' AND @ProjectID IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (EDC.ProjectID = '''+@ProjectID+''') '
END

IF(@ProjectStatusKey <> '' AND @ProjectStatusKey IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (MCCS.[Key] = '''+@ProjectStatusKey+''') '
END

IF(@ReceiveBy <> '' AND @ReceiveBy IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (EDC.ReceiveBy LIKE ''%'+@ReceiveBy+'%'') '
END

IF(@ReceiveDateFrom <> '' AND @ReceiveDateFrom IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (EDC.ReceiveDate >= '''+CONVERT(VARCHAR(50),@ReceiveDateFrom,120)+''')'
END

IF(@ReceiveDateTo <> '' AND @ReceiveDateTo IS NOT NULL) 
BEGIN
    SET @sql=@sql + ' AND (EDC.ReceiveDate <= '''+CONVERT(VARCHAR(50),@ReceiveDateTo,120)+''')'
END

exec(@sql)

GO
