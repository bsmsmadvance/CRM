SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[ZCM_SP_Report_OntopPaidOther] '1',NULL,6,2557
--[dbo].[ZCM_SP_Report_OntopPaidOther] NULL,'10122',6,2557

ALTER PROCEDURE [dbo].[ZCM_SP_Report_OntopPaidOther]
	@HomeType nvarchar(20),
	@ProductID nvarchar(20), 
	@Month int,
	@Year int,
	@UserName nvarchar(150) = '',
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS


IF (@Year>2400) SET @Year=@Year-543;

SELECT 'BUID' = '' --BG.Name
    ,'ProductID' = '' --P.ProjectNo
    ,'PeriodMonth' = '' --PeriodMonth
    ,'PeriodYear' = '' --PeriodYear
    ,'SaleID' = '' --SaleID
	,'LCID' = '' --U.EmployeeID AS LCID
	,'LCName' = '' --U.DisplayName AS LCName
	,'PaidOther' = '' --ISNULL(SUM(PaidOther),0) AS PaidOther
	,'PaidAdjust' = '' --ISNULL(SUM(AdjustValue),0) AS PaidAdjust

FROM [SAL].[Booking] --This is temp table actual table start from below
    /* dbo.ZComm_CommissionCalOther A 
	LEFT OUTER JOIN [USR].[User] U ON A.SaleID = U.UserID 
	LEFT OUTER JOIN [PRJ].[Project] P ON A.ProductID = P.ProductID
    LEFT OUTER JOIN [MST].[BG] BG ON BG.ID = P.BGID
WHERE PeriodYear = @Year 
	AND PeriodMonth = @Month 
	AND (ISNULL(@HomeType,'')='' OR P.Ptype = @HomeType)
	AND (ISNULL(@ProductID,'')='' OR A.ProductID = @ProductID)
	AND (ISNULL(@ProjectGroup,'')='' OR P.ProjectGroup = @ProjectGroup) 
	AND (ISNULL(@ProjectType2,'')='' OR P.ProjectType = @ProjectType2) 
	
GROUP BY P.PType,A.ProductID,PeriodMonth,PeriodYear,SaleID,U.EmployeeID,U.DisplayName
ORDER BY P.PType,A.ProductID,PeriodMonth,PeriodYear,EmployeeID */

GO
