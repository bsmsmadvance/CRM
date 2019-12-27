SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--AP2SP_RP_LC_027 NULL,'''10060'',''10049''',NULL,NULL,'administrator account'

ALTER PROC [dbo].[AP2SP_RP_LC_027]
	@CompanyID nvarchar(50),
	@Projects	nvarchar(4000),
	@DateStart datetime,
	@DateEnd datetime,
    @UserName nvarchar(150)
AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore  = [dbo].[fn_GetMaxDate] (@DateEnd)
DECLARE @DateStartInStore Datetime

IF (YEAR(@DateStart) = 1800 ) SET @DateStartInStore = NULL
IF (YEAR(@DateEnd) = 7000 )	  SET @DateEndInStore = NULL

Declare @sql nvarchar(max)
Set @sql= '

SELECT ''GroupName'' = '''' --R.GroupName
    ,''ReasonDescription'' = '''' --R.ReasonDescription
	,''BBU1'' = '''' --ISNULL(BBU1.BBU1,0) 
	,''BBU2'' = '''' --ISNULL(BBU2.BBU2,0) 
	,''BBU3'' = '''' --ISNULL(BBU3.BBU3,0) 
	,''BBU4'' = '''' --ISNULL(BBU4.BBU4,0)
	,''ABU1'' = '''' --ISNULL(ABU1.ABU1,0)
	,''ABU2'' = '''' --ISNULL(ABU2.ABU2,0)
	,''ABU3'' = '''' --ISNULL(ABU3.ABU3,0)
	,''ABU4'' = '''' --ISNULL(ABU4.ABU4,0)

FROM  [SAL].[Booking]' --This is temp table actual table start from below
/* (
	SELECT GroupName,ReasonDescription,ReasonID
	FROM ICON_EntForms_Reason
	WHERE (CollectionID IN (''888'')) 
		AND ISNULL(DeleteFlag,0) = 0
) R LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS BBU1
	FROM [ICON_EntForms_UnitHistory] UH 
        LEFT OUTER JOIN [SAL].[Booking] B  ON B.BookingNumber = UH.ReferentID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = B.ProjectID	
        LEFT OUTER JOIN	[ICON_EntForms_Reason] R ON R.ReasonID = B.CancelReason
	WHERE UH.OperateType = ''BV'' AND UH.OperateName = ''ยกเลิกจอง'' AND UH.IsApprove = 1 AND P.PType = ''1'' '

		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) BBU1 ON R.ReasonID = BBU1.ReasonID LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS BBU2
	FROM [ICON_EntForms_UnitHistory] UH 
        LEFT OUTER JOIN [SAL].[Booking] B  ON B.BookingNumber = UH.ReferentID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = B.ProjectID	
        LEFT OUTER JOIN	[ICON_EntForms_Reason] R ON R.ReasonID = B.CancelReason
	WHERE UH.OperateType = ''BV'' AND UH.OperateName = ''ยกเลิกจอง'' AND UH.IsApprove = 1 AND P.PType = ''2'' '

		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) BBU2 ON R.ReasonID = BBU2.ReasonID LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS BBU3
	FROM [ICON_EntForms_UnitHistory] UH LEFT OUTER JOIN
		[SAL].[Booking] B  ON B.BookingNumber = UH.ReferentID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = B.ProjectID	
        LEFT OUTER JOIN	[ICON_EntForms_Reason] R ON R.ReasonID = B.CancelReason
	WHERE UH.OperateType = ''BV'' AND UH.OperateName = ''ยกเลิกจอง'' AND UH.IsApprove = 1 AND P.PType = ''3'' '

		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) BBU3 ON R.ReasonID = BBU3.ReasonID LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS BBU4
	FROM [ICON_EntForms_UnitHistory] UH 
        LEFT OUTER JOIN [SAL.Booking] B  ON B.BookingNumber = UH.ReferentID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ProductID = B.ProductID
        LEFT OUTER JOIN	[ICON_EntForms_Reason] R ON R.ReasonID = B.CancelReason
	WHERE UH.OperateType = ''BV'' AND UH.OperateName = ''ยกเลิกจอง'' AND UH.IsApprove = 1 AND P.PType = ''4'' '

		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) BBU4 ON R.ReasonID = BBU4.ReasonID LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS ABU1
	FROM [ICON_EntForms_UnitHistory] UH 
         LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = UH.ReferentID 
         LEFT OUTER JOIN [PRJ].[Projec] P ON P.ID = A.ProjectID	
         LEFT OUTER JOIN [ICON_EntForms_Reason] R ON R.ReasonID = A.CancelReason	
	WHERE UH.OperateType = ''V'' AND UH.OperateName = ''ยกเลิกสัญญา'' AND UH.IsApprove = 1 AND P.PType = ''1'' '

		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) ABU1 ON R.ReasonID = ABU1.ReasonID LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS ABU2
	FROM [ICON_EntForms_UnitHistory] UH 
        LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = UH.ReferentID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = A.ProjectID	
        LEFT OUTER JOIN	[ICON_EntForms_Reason] R ON R.ReasonID = A.CancelReason	
	WHERE UH.OperateType = ''V'' AND UH.OperateName = ''ยกเลิกสัญญา'' AND UH.IsApprove = 1 AND P.PType = ''2'' '
	
		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) ABU2 ON R.ReasonID = ABU2.ReasonID LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS ABU3
	FROM [ICON_EntForms_UnitHistory] UH 
        LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = UH.ReferentID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = A.ProjectID	
        LEFT OUTER JOIN	[ICON_EntForms_Reason] R ON R.ReasonID = A.CancelReason	
	WHERE UH.OperateType = ''V'' AND UH.OperateName = ''ยกเลิกสัญญา'' AND UH.IsApprove = 1 AND P.PType = ''3'' '

		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) ABU3 ON R.ReasonID = ABU3.ReasonID LEFT OUTER JOIN
(
	SELECT R.ReasonID,COUNT(*) AS ABU4
	FROM [ICON_EntForms_UnitHistory] UH 
        LEFT OUTER JOIN [SAL].[Agreement] A ON A.ContractNumber = UH.ReferentID 
        LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = A.ProjectID	
        LEFT OUTER JOIN	[ICON_EntForms_Reason] R ON R.ReasonID = A.CancelReason	
	WHERE UH.OperateType = ''V'' AND UH.OperateName = ''ยกเลิกสัญญา'' AND UH.IsApprove = 1 AND P.PType = ''4'' '

		if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '

		IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (@Projects <> '''''')) SET @sql=@sql+' AND (P.ProductID IN ('+@Projects+')) '

		if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
			set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

set @sql=@sql+'
	GROUP BY R.ReasonID
) ABU4 ON R.ReasonID = ABU4.ReasonID' */

--print (@sql)
exec( @sql)





GO
