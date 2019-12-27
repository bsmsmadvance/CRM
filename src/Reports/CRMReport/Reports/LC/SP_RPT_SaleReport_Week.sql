SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [dbo].[SP_RPT_SaleReport_Week] 'ทั้งหมด','10194','',2560,10,10
CREATE  PROC [dbo].[SP_RPT_SaleReport_Week]
/* สำหรับออกรายงาน Sale Report แบบรายสัปดาห์ */
    @HomeType AS NVARCHAR(50) = '' ,
    @ProductID AS NVARCHAR(50) = '' ,
    @UserName NVARCHAR(100) = '' ,
    @Year INT = 0 ,
    @WeekStart AS INT = 0 ,
    @WeekEnd AS INT = 0
AS
    IF ( @Year > 2400 ) SET @Year = @Year - 543;

    DECLARE @StartDate DATETIME ,
        @EndDate DATETIME
    SELECT  @StartDate = StartDate
    FROM    [DBLINK_SVR_BI].AP_STG.dbo.vw_Calendar_Week
    WHERE   Y = @Year
            AND W = @WeekStart
    SELECT  @EndDate = EndDate
    FROM    [DBLINK_SVR_BI].AP_STG.dbo.vw_Calendar_Week
    WHERE   Y = @Year
            AND W = @WeekEnd

    EXEC [SP_RPT_SaleReport_NEW] @HomeType, @ProductID, @StartDate, @EndDate,
        @UserName, 'W'



GO
