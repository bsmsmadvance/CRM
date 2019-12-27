SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec CRM_SP_GetDataMyWorldToday @KeySearch=N'',@FirstName=N'',@LastName=N'',@TelNo=N''
--,@DateFrom='2016-08-26 00:00:00',@DateTo='2016-08-26 23:59:59',@ProjectID=N'',@LCID=N'',@UserID=N'AP002363',@OverDue=N''


--  exec sp_RPT_MornitorTracking_Followup '40023',null,null,'20180510','20180510','',''

ALTER PROC [dbo].[sp_RPT_MornitorTracking_Followup]

--DECLARE
    @ProductID NVARCHAR(50) 
  , @DateStart DATETIME            --วันที่ต้องทำ(Plan)   ActivityDate
  , @DateEnd DATETIME 
  , @DateStart2 DATETIME              --วันที่ทำจริง
  , @DateEnd2 DATETIME
  , @Username NVARCHAR(100) = ''
  , @LCByProduct NVARCHAR(50) = ''
  , @Lead INT = 1
  , @FirstWalk INT = 1
  , @Revisit INT = 1
  , @FollowUpStatus NVARCHAR(50) = '' -- All,Followup,Completed
AS
BEGIN

    IF (@DateStart IS NULL)
        SET @DateStart = '18000101';

    IF (@DateEnd IS NULL)
        SET @DateEnd = '70001231';

    IF (@DateStart2 IS NULL)
        SET @DateStart2 = '18000101';

    IF (@DateEnd2 IS NULL)
        SET @DateEnd2 = '70001231';

    IF (@FollowUpStatus IS NULL)
        SET @FollowUpStatus = '';

    ---------- Select Project by Permission ---------------

    IF (OBJECT_ID('tempdb..#TProject') IS NOT NULL)
        DROP TABLE #TProject;

    -- Load Project 

    /* SELECT *
    INTO #TProject
    FROM (   SELECT ProductID
             FROM [dbo].[fn_GetProjectAuthorised](ISNULL(@Username, '')) t
             WHERE (ISNULL(@ProductID, '') = '' OR t.ProductID = @ProductID)
             UNION
             SELECT ProjectID
             FROM [PRJ].[Project]
             WHERE ISNULL(RTPExcusive, 0) = 1
                 AND (ISNULL(@Username, '') = '' OR ISNULL(@Username, '') = 'Administrator')
                 AND (ISNULL(@ProductID, '') = '' OR ProductID = @ProductID)) t;

    DECLARE @EmpCode NVARCHAR(50);

    IF (ISNUMERIC(@LCByProduct) = -1)
        SET @LCByProduct = '-1';

    SET @EmpCode = ISNULL((SELECT EmployeeID FROM vw_ActiveUsers WHERE ISNULL(@LCByProduct, '') <> '' AND CONVERT(NVARCHAR(50), UserID) = @LCByProduct), '');
    IF (ISNULL(@Username, '') = 'Administrator')
        SET @EmpCode = '';

    IF OBJECT_ID('tempdb..#tmpLeads') IS NOT NULL
        DROP TABLE #tmpLeads;

    SELECT L.LeadsID
      , L.ContactID
      , L.LeadReference
      , L.FirstName
      , L.LastName
      , L.Mobile
      , L.Telephone
      , L.TelephoneExt
      , L.ProjectID
      , p.Project ProjectName
      , L.LeadsTypeID
      , L.Advertisement
      , L.AdvertisementID
      , L.LeadsStatus
      , L.Remark
      , L.OwnerID
      , L.CreateBy
      , L.CreateDate
      , L.EditBy
      , L.EditDate
      , L.refID
      , L.ContactType
      , p.Producttype
      , 'LeadsTypeName' = (CASE WHEN L.LeadsTypeID = 'W' THEN 'Web' WHEN L.LeadsTypeID = 'C' THEN 'Call' ELSE '' END)
	  , L.CurrentCategoryID
    INTO #tmpLeads
    FROM [CTM].[Lead] L
    LEFT JOIN [PRJ].[Project] p ON L.ProjectID = p.ID
    WHERE 1 = 1 
		AND L.ProjectID IN (SELECT ProductID FROM #TProject)
        AND (L.OwnerID = @EmpCode OR ISNULL(L.OwnerID, '0') = '0' OR @EmpCode = '');

    DECLARE @ToDayDate AS DATE = CONVERT(DATE, GETDATE());

    IF OBJECT_ID('tempdb..#tmpActivity') IS NOT NULL
        DROP TABLE #tmpActivity;

    SELECT *
      , (CASE
            WHEN A.ActualDate IS NULL
                AND CONVERT(DATE, A.ActivityDate) < @ToDayDate THEN 'red'
            WHEN A.ActualDate IS NULL
                AND CONVERT(DATE, A.ActivityDate) = @ToDayDate THEN 'yellow'
            WHEN A.ActualDate IS NULL
                AND CONVERT(DATE, A.ActivityDate) > @ToDayDate THEN 'white'
            WHEN A.ActualDate IS NOT NULL THEN 'green'
        END) AS OverDue
      , 'DateDiff' = (CASE WHEN A.ActualDate IS NOT NULL THEN DATEDIFF(DAY, A.ActualDate, A.ActivityDate)ELSE DATEDIFF(DAY, @ToDayDate, A.ActivityDate) END)
    INTO #tmpActivity
    FROM [dbo].[CRM_Activity] A
    WHERE 1 = 1 
        AND (YEAR(@DateStart) = 1800 OR YEAR(@DateEnd) = 7000 OR dbo.fn_ClearTime(A.ActivityDate) BETWEEN @DateStart AND @DateEnd)
        AND (YEAR(@DateStart2) = 1800 OR YEAR(@DateEnd2) = 7000 OR dbo.fn_ClearTime(A.ActualDate) BETWEEN @DateStart2 AND @DateEnd2)
        AND ((@FollowUpStatus = '' OR @FollowUpStatus = 'All')
                OR (@FollowUpStatus = 'Followup' AND A.ActualDate IS NULL)
                OR (@FollowUpStatus = 'Completed' AND A.ActualDate IS NOT NULL)); */

    IF OBJECT_ID('tempdb..#tmpLeadsActivity') IS NOT NULL
        DROP TABLE #tmpLeadsActivity; 

    SELECT 'Topic' = 'Lead'
      , 'ActivityID' = '' --A.ActivityID
      , 0 AS OpportunityID
      , 'RefID' = '' --CONVERT(NVARCHAR(351), L.LeadsID) AS RefID
      , 'TaskID' = '' --A.TaskID
      , 'TaskText' = '' --TaskLeads.MasterText AS TaskText
      , 'ActivityType' = '' --A.ActivityType
      , 'ActivityTypeText' = '' --ActivityType.MasterText AS ActivityTypeText
      , 'DisplayName' = '' --ISNULL(L.FirstName, '') + ' ' + ISNULL(L.LastName, '') AS DisplayName
      , 'Mobile' = '' --L.Mobile
      , 'ActivityDate' = '' --A.ActivityDate
      , 'ActualDate' = '' --A.ActualDate
      , 'AppointDate' = '' --A.AppointDate
      , 'LeadsTypeID' = '' --L.LeadsTypeID
      , 'LeadsTypeText' = '' --LeadsType.MasterText AS LeadsTypeText
      , 'OwnerID' = '' --L.OwnerID
      , 'OwnerName' = '' --Usr.FirstName + ' ' + Usr.LastName AS OwnerName
      , 'Remark' = '' --A.Remark
      , 'ProjectCode' = '' --L.ProjectID ProjectCode
      , 'ProjectName' = '' --L.ProjectName
      , '' AS PersonalID
      , 'Producttype' = '' --L.Producttype
      , 'OverDue' = '' --A.OverDue
      , 'DateDiff' = '' --A.[DateDiff]
      , 'CategoryName' = '' --ISNULL(CAST(newCat.LeadsSubCategory AS VARCHAR(250)), ISNULL(cat.MasterText, '')) AS CategoryName
      , 'LeadsTypeName' = '' --L.LeadsTypeName
    INTO #tmpLeadsActivity
    /* FROM #tmpActivity A
    INNER JOIN #tmpLeads L ON A.ReferentID = L.LeadsID
	LEFT OUTER JOIN dbo.vw_CRM_LeadsCategory newCat ON newCat.LeadsSubCategoryID = L.CurrentCategoryID
    LEFT JOIN [dbo].[CRM_MasterCenter] ActivityType ON ActivityType.MasterGroup = 'ActivityType' AND ActivityType.MasterValue = A.ActivityType
    LEFT JOIN [dbo].[CRM_MasterCenter] TaskLeads ON TaskLeads.MasterGroup = 'TaskLeads' AND TaskLeads.MasterValue = A.TaskID
    LEFT JOIN [dbo].[CRM_MasterCenter] LeadsType ON LeadsType.MasterGroup = 'LeadsType' AND LeadsType.MasterValue = L.LeadsTypeID
    LEFT JOIN [dbo].[CRM_MasterCenter] cat ON cat.MasterGroup = 'ActivityCategoryLeads' AND cat.MasterValue = A.CategoryID
    LEFT JOIN [dbo].[vw_User] Usr ON L.OwnerID = Usr.EmpCode
    WHERE A.ActivityType = '1'
        AND ISNULL(@Lead, 1) = 1; */

    IF OBJECT_ID('tempdb..#tmpWalkActivity') IS NOT NULL
        DROP TABLE #tmpWalkActivity;

    SELECT 'Topic' = 'First Walk'
      , 'AcitivityID' = ''--A.ActivityID
      , 'OpportunityID' = '' --O.OpportunityID
      , 'RefID' = '' --C.ContactID AS RefID
      , 'TaskID' = '' --A.TaskID
      , 'TaskText' = '' --TaskWalk.MasterText AS TaskText
      , 'ActivityType' = '' --A.ActivityType
      , 'ActivityTypeText' = '' --ActivityType.MasterText AS ActivityTypeText
      , 'DisplayName' = '' --C.DisplayName
      , 'Mobile' = '' --C.Tel_4 AS Mobile
      , 'ActivityDate' = '' --A.ActivityDate
      , 'ActualDate' = '' --A.ActualDate
      , 'AppointDate' = '' --A.AppointDate
      , '' AS LeadsTypeID
      , '' AS LeadsTypeText
      , 'OwnerID' = '' --O.OwnerID
      , 'OwnerName' = '' --Usr.FirstName + ' ' + Usr.LastName AS OwnerName
      , 'Remark' = '' --A.Remark
      , 'ProjectCode' = '' --P.ProductID ProjectCode
      , 'ProjectName' = '' --P.Project ProjectName
      , 'PersonalID' = '' --C.PersonalID
      , 'Producttype' = '' --P.Producttype
      , 'OverDue' = '' --A.OverDue
      , 'DateDiff' = '' --A.[DateDiff]
      , 'CategoryName' = '' --ISNULL(cat.MasterText, '') AS CategoryName
      , '' LeadsTypeName
    INTO #tmpWalkActivity
    /* FROM #tmpActivity A
    LEFT JOIN [CTM].[Opportunities] O ON A.ReferentID = O.OpportunityID AND O.OpportunityStatusID <> 'D'
    LEFT JOIN [PRJ].[Project] P ON O.ProjectID = P.ProductID
    LEFT JOIN [CTM].[Contact] C ON O.ContactID = C.ItemId    
	LEFT JOIN [dbo].[CRM_MasterCenter] ActivityType ON ActivityType.MasterGroup = 'ActivityType' AND ActivityType.MasterValue = A.ActivityType
    LEFT JOIN [dbo].[CRM_MasterCenter] TaskWalk ON TaskWalk.MasterGroup = 'TaskWalk' AND TaskWalk.MasterValue = A.TaskID
    LEFT JOIN [dbo].[CRM_MasterCenter] cat ON cat.MasterGroup = 'ActivityCategoryWalk' AND cat.MasterValue = A.CategoryID
    LEFT JOIN [dbo].[vw_User] Usr ON O.OwnerID = Usr.EmpCode
    WHERE A.ActivityType = '2'
        AND O.ProjectID IN (SELECT ProductID FROM #TProject)
        AND (O.OwnerID = @EmpCode OR @EmpCode = '')
        AND ISNULL(@FirstWalk, 1) = 1; */

    IF OBJECT_ID('tempdb..#tmpRevisitActivity') IS NOT NULL
        DROP TABLE #tmpRevisitActivity;

    SELECT 'Topic' = 'Revisit'
      , 'ActivityID' = '' --A.ActivityID
      , 'OpportunityID' = '' --O.OpportunityID
      , 'RefID' = '' --C.ContactID AS RefID
      , 'TaskID' = '' --A.TaskID
      , 'TaskText' = '' --TaskRevisit.MasterText AS TaskText
      , 'ActivityType' = '' --A.ActivityType
      , 'ActivityTypeText' = '' --ActivityType.MasterText AS ActivityTypeText
      , 'DisplayName' = '' --C.DisplayName
      , 'Mobile' = '' --C.Tel_4 AS Mobile
      , 'ActivityDate' = '' --A.ActivityDate
      , 'ActualDate' = '' --A.ActualDate
      , 'AppointDate' = '' --A.AppointDate
      , '' AS LeadsTypeID
      , '' AS LeadsTypeText
      , 'OwnerID' = '' --O.OwnerID
      , 'OwnerName' = '' --Usr.FirstName + ' ' + Usr.LastName AS OwnerName
      , 'Remark' = '' --A.Remark
      , 'ProjectCode' = '' --P.ProductID ProjectCode
      , 'ProjectName' = '' --P.Project ProjectName
      , 'PersonalID' = '' --C.PersonalID
      , 'Producttype' = '' --P.Producttype
      , 'OverDue' = '' --A.OverDue
      , 'DateDiff' = '' --A.[DateDiff]
      , 'CategoryName' = '' --ISNULL(cat.MasterText, '') AS CategoryName
      , '' LeadsTypeName
    INTO #tmpRevisitActivity
    /* FROM #tmpActivity A
    LEFT JOIN [CTM].[Opportunities] O ON A.ReferentID = O.OpportunityID AND O.OpportunityStatusID <> 'D'
    LEFT JOIN [PRJ].[Project] P ON O.ProjectID = P.ProductID
    LEFT JOIN [CTM].[Contact] C ON O.ContactID = C.ItemId
    LEFT JOIN [dbo].[CRM_MasterCenter] ActivityType ON ActivityType.MasterGroup = 'ActivityType' AND ActivityType.MasterValue = A.ActivityType
    LEFT JOIN [dbo].[CRM_MasterCenter] TaskRevisit ON TaskRevisit.MasterGroup = 'TaskRevisit' AND TaskRevisit.MasterValue = A.TaskID
    LEFT JOIN [dbo].[CRM_MasterCenter] cat ON cat.MasterGroup = 'ActivityCategoryRevisit' AND cat.MasterValue = A.CategoryID
    LEFT JOIN [dbo].[vw_User] Usr ON O.OwnerID = Usr.EmpCode
    WHERE A.ActivityType = '3'
        AND (O.ProjectID IN (SELECT ProductID FROM #TProject))
        AND (O.OwnerID = @EmpCode OR @EmpCode = '')
        AND ISNULL(@Revisit, 1) = 1; */

    SELECT * 
    FROM
	(
		SELECT * FROM #tmpLeadsActivity
		UNION 
		SELECT * FROM #tmpWalkActivity
		UNION 
		SELECT * FROM #tmpRevisitActivity
	) AS tbRst

	/* ORDER BY ProjectCode
		, OwnerID
		, ActivityDate; */
	
END;




GO
