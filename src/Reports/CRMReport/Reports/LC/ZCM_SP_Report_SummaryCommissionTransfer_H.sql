SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--ZCM_SP_Report_SummaryCommissionTransfer_H '70013','20160101','20160131',''

CREATE PROC [dbo].[ZCM_SP_Report_SummaryCommissionTransfer_H]
	@ProductID nvarchar(20),
	@DateStart datetime,
	@DateEnd datetime,
	@UserName	nvarchar(50)
AS

DECLARE @v TABLE(
	ProductID nvarchar(20),
	UserID int,
	flag nvarchar(250)
);

/* INSERT INTO @v
SELECT ProductID,SaleID,'พนง.ปิดการขาย --> โอนภายในเดือน'
FROM [dbo].[ZComm_CommissionCalTransfer] T
WHERE ProductID = @ProductID
	AND (CONVERT(DateTime,CONVERT(varchar(4),PeriodYear)+'-'+CONVERT(varchar(2),PeriodMonth)+'-01') BETWEEN @DateStart AND @DateEnd)
	AND IsActive=1
	AND SaleID > 0
	AND NOT EXISTS(SELECT * FROM @v WHERE UserID=T.SaleID)

INSERT INTO @v
SELECT ProductID,SaleHelperID,'พนง.ประจำโครงการ --> โอนภายในเดือน'
FROM [dbo].[ZComm_CommissionCalTransfer] T
WHERE ProductID = @ProductID
	AND (CONVERT(DateTime,CONVERT(varchar(4),PeriodYear)+'-'+CONVERT(varchar(2),PeriodMonth)+'-01') BETWEEN @DateStart AND @DateEnd)
	AND IsActive=1
	AND SaleHelperID > 0
	AND NOT EXISTS(SELECT * FROM @v WHERE UserID=T.SaleHelperID)

INSERT INTO @v
SELECT ProductID,SaleID,'พนง.โอน --> ค่าอื่น ค่าหัก'
FROM [dbo].[ZComm_CommissionCalOther] O LEFT OUTER JOIN
	UserRoles UR ON O.SaleID=UR.UserID
WHERE UR.RoleID = '18'
	AND ProductID = @ProductID
	AND (CONVERT(DateTime,CONVERT(varchar(4),PeriodYear)+'-'+CONVERT(varchar(2),PeriodMonth)+'-01') BETWEEN @DateStart AND @DateEnd)
	AND (PaidOther > 0 OR AdjustValue > 0)
	AND SaleID > 0
	AND NOT EXISTS(SELECT * FROM @v WHERE UserID=O.SaleID) */
	
---------------------------------------------------------------------------------------------------------------------------------

SELECT 'ProductID' = '' --U.ProductID AS ProductID,
	,'ProductName' = '' --[dbo].[fn_GetProjectName](U.ProductID) AS ProductName
	,'HCSName' = '' --[dbo].[fn_GetHeadOfCS](U.ProductID,'T') AS HCSName
	,'UserID' = '' --U.UserID
    ,'EmployeeID' = '' --U.EmployeeID
    ,'DisplayName' = '' --U.DisplayName
	,'CommTrans' = '' --ISNULL(T1.CommTrans1,0)+ISNULL(T2.CommTrans2,0)
	,'LCCCommPaid' = '' --ISNULL(LCC2.LCCCommTrans,0)
	,'SumOther' = '' --ISNULL(O.SumOther,0)
	,'SumAdjust' = '' --ISNULL(O.SumAdjust,0)
	,'TotalCommission' = '' /* ISNULL(T1.CommTrans1,0)+ISNULL(T2.CommTrans2,0) + 
			ISNULL(O.SumOther,0) - ISNULL(O.SumAdjust,0) - ISNULL(LCC2.LCCCommTrans,0) */
	,'flag' = '' --flag
			
FROM [USR].[User] --This is temp table actual table start from below
	/* (
		SELECT DISTINCT V.ProductID,V.UserID,US.EmployeeID,US.DisplayName,
			V.flag
		FROM @v V LEFT OUTER JOIN 
			Users US ON V.UserID=US.UserID		
	) U	
	LEFT OUTER JOIN 
	(
		SELECT SaleID,SUM(SaleCommissionTransPaid) AS CommTrans1
		FROM [dbo].[ZComm_CommissionCalTransfer]
		WHERE ProductID = @ProductID
			AND (CONVERT(DateTime,CONVERT(varchar(4),PeriodYear)+'-'+CONVERT(varchar(2),PeriodMonth)+'-01') BETWEEN @DateStart AND @DateEnd)
			AND IsActive=1
		GROUP BY SaleID
	) T1 ON U.UserID=T1.SaleID 
	LEFT OUTER JOIN 
	(
		SELECT SaleHelperID,SUM(SaleHelperCommissionTransPaid) AS CommTrans2
		FROM [dbo].[ZComm_CommissionCalTransfer]
		WHERE ProductID = @ProductID
			AND (CONVERT(DateTime,CONVERT(varchar(4),PeriodYear)+'-'+CONVERT(varchar(2),PeriodMonth)+'-01') BETWEEN @DateStart AND @DateEnd)
			AND IsActive=1
		GROUP BY SaleHelperID
	) T2 ON U.UserID=T2.SaleHelperID 	
	LEFT OUTER JOIN 
	(
		SELECT SaleHelperID,SUM(LCCCommissionTransPaid) AS LCCCommTrans
		FROM [dbo].[ZComm_CommissionCalTransfer]
		WHERE ProductID = @ProductID
			AND (CONVERT(DateTime,CONVERT(varchar(4),PeriodYear)+'-'+CONVERT(varchar(2),PeriodMonth)+'-01') BETWEEN @DateStart AND @DateEnd)
			AND IsActive=1
		GROUP BY SaleHelperID
	) LCC2 ON U.UserID=LCC2.SaleHelperID 
	LEFT OUTER JOIN 
	(
		SELECT SaleID,SUM(PaidOther) AS SumOther,SUM(AdjustValue) AS SumAdjust
		FROM [dbo].[ZComm_CommissionCalOther] O LEFT OUTER JOIN
			UserRoles UR ON O.SaleID=UR.UserID
		WHERE UR.RoleID = '18'
			AND ProductID = @ProductID
			AND (CONVERT(DateTime,CONVERT(varchar(4),PeriodYear)+'-'+CONVERT(varchar(2),PeriodMonth)+'-01') BETWEEN @DateStart AND @DateEnd)
			AND (PaidOther > 0 OR AdjustValue > 0)
		GROUP BY SaleID
	) O ON U.UserID=O.SaleID 
	
WHERE (ISNULL(T1.CommTrans1,0)+ISNULL(T2.CommTrans2,0) + 			
	ISNULL(O.SumOther,0) - ISNULL(O.SumAdjust,0) - ISNULL(LCC2.LCCCommTrans,0)) > 0

ORDER BY U.ProductID,U.EmployeeID; */

GO
