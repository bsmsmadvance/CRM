SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Stored Procedure

/*
exec "SP_RPT_FollowCustomer";1 
N'10096', {ts '1800-01-01 00:00:00'}, {ts '7000-12-31 00:00:00'}
, {ts '1800-01-01 00:00:00'}, {ts '7000-12-31 00:00:00'}, {ts '2018-02-05 00:00:00'}, {ts '2018-02-11 00:00:00'}, N''
, N'ศรันยา คลังวิเชียร', 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, N'8b8b8f5b-a968-4172-a3c3-99c85c29415e', 1
*/
/* 

exec "SP_RPT_FollowCustomer";1 N'10118', {ts '2018-02-12 00:00:00'}, {ts '2018-02-18 00:00:00'}, {ts '1800-01-01 00:00:00'}, {ts '7000-12-31 00:00:00'}, {ts '1800-01-01 00:00:00'}, {ts '7000-12-31 00:00:00'}, N'', N'ภัคญาริณญ์ สรณ์ปัญจพล', 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, N'ee94849a-2980-4825-b660-aedd6980bc7dd3', 1 

*/
 --exec [SP_RPT_FollowCustomer]'10077','20180303','20180303','','',1,1,1,1,1,1,1,1,1,1,1,1
 --exec [SP_RPT_FollowCustomer_Test]'60010','20180127','20180127','','',1,1,1,1,1,1,1,1,1,1,1,1
 --exec [SP_RPT_FollowCustomer_Test]'10192','20180401','20180630','','',0,1,1,1,1,1,1,1,1,1,1,1
 

CREATE PROC [dbo].[SP_RPT_FollowCustomer]
	@ProductID nvarchar(50),
	@DateStart2 Datetime,-- actual date วันที่ทำจริง
	@DateEnd2 Datetime,
	@LCByProduct nvarchar(50)=''
	,@Username nvarchar(100)=''
	,@Disqualify int = 0  
	,@Lead int=1
	,@FirstWalk int=1
	,@Revisit int=1
	,@TaskLead1 int=1
	,@TaskLead2 int=1
	,@TaskLead3 int=1
	,@TaskLead4 int=1
	,@TaskWalk1 int=1
	,@TaskWalk2 int=1
	,@TaskWalk3 int=1
	,@TaskWalk4 int=1
	,@TaskWalk5 int=1
	,@TaskWalk6 int=1
	,@TaskRevisit1 int=1
	,@TaskRevisit2 int=1
	,@TaskRevisit3 int=1
	,@TaskRevisit4 int=1
	,@SessionID nvarchar(50)=''
	,@TaskWalkEnd INT=1
AS
--Return ;
/*
IssuedStatus = Web,Call,First walk  :  Web,Call=>From Lead    First walk=>From Opp not have lead

*/
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
Set @Lead = Isnull(@Lead,0)
Set @FirstWalk = Isnull(@FirstWalk,0)
Set @Revisit = Isnull(@Revisit,0)
Set @TaskLead1 = Isnull(@TaskLead1,0)
Set @TaskLead2 = Isnull(@TaskLead2,0)
Set @TaskLead3 = Isnull(@TaskLead3,0)
Set @TaskLead4 = Isnull(@TaskLead4,0)
Set @TaskWalk1 = Isnull(@TaskWalk1,0)
Set @TaskWalk2 = Isnull(@TaskWalk2,0)
Set @TaskWalk3 = Isnull(@TaskWalk3,0)
Set @TaskWalk4 = Isnull(@TaskWalk4,0)
Set @TaskWalk5 = Isnull(@TaskWalk5,0)
Set @TaskWalk6 = Isnull(@TaskWalk6,0)
Set @TaskWalkEnd = Isnull(@TaskWalkEnd,1)
Set @TaskRevisit1 = Isnull(@TaskRevisit1,0)
Set @TaskRevisit2 = Isnull(@TaskRevisit2,0)
Set @TaskRevisit3 = Isnull(@TaskRevisit3,0)
Set @TaskRevisit4 = Isnull(@TaskRevisit4,0)
Set @Disqualify = Isnull(@Disqualify,0)


---------- Select Project by Permission ---------------
--If(Isnull(@Username,'')='')Set @Username='พลอยไพลิน ธนกิจวรบูลย์'
If(Object_ID('tempdb..#TProject')Is not null)Drop table #TProject

-- Load Project 
Select * into #TProject  From(
SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](Isnull(@Username,''))t Where (ISNULL(@ProductID,'')='' or t.ProductID=@ProductID)
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล
Union
SELECT ProductID FROM dbo.ICON_EntForms_Products Where Isnull(RTPExcusive,0)=1 and (Isnull(@Username,'')=''or @Username='Administrator') and (ISNULL(@ProductID,'')='' or ProductID=@ProductID )
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล
)t

Declare @EmpCode nvarchar(50)
if(ISNUMERIC(@LCByProduct)=-1)Set @LCByProduct='-1'
Set @EmpCode = Isnull((Select EmployeeID From dbo.vw_ActiveUsers With(NoLock) Where Isnull(@LCByProduct,'')<>'' and Convert(nvarchar(50),UserID)=@LCByProduct),'')

If(ISNULL(@DateStart2,'')='')Set @DateStart2='18000101'
If(ISNULL(@DateEnd2,'')='')Set @DateEnd2='70001231'
DECLARE @DateEnd2InStore Datetime
SET @DateEnd2InStore = [dbo].[fn_GetMaxDate](@DateEnd2)

if(object_id('tempdb..#temp')is not null)drop table #temp;
if(object_id('tempdb..#TmpQN')is not null)drop table #TmpQN;
if(object_id('tempdb..#TmpOp')is not null)drop table #TmpOp;
if(object_id('tempdb..#TmpLeads')is not null)drop table #TmpLeads;
if(object_id('tempdb..#Temp')is not null)drop table #Temp;
if(object_id('tempdb..#TmpCurrentOp')is not null)drop table #TmpCurrentOp;
if(object_id('tempdb..#TmpCurrentAcct_Lead')is not null)drop table #TmpCurrentAcct_Lead;  
if(object_id('tempdb..#TmpCurrentAcct_Opp')is not null)drop table #TmpCurrentAcct_Opp;  
if(object_id('tempdb..#TmpCurrentLead')is not null)drop table #TmpCurrentLead;
if(object_id('tempdb..#CRM_Opportunities')is not null)drop table #CRM_Opportunities;
if(object_id('tempdb..#CRM_OpportunitiesDetail')is not null)drop table #CRM_OpportunitiesDetail;
if(object_id('tempdb..#CRM_Activity')is not null)drop table #CRM_Activity;
if(object_id('tempdb..#CRM_Leads')is not null)drop table #CRM_Leads;
if(object_id('tempdb..#CRM_LeadsActivity')is not null)drop table #CRM_LeadsActivity;
If(Object_ID('tempdb..#Project')is not null)Drop table #Project
If(Object_ID('tempdb..#CustomerAnswerHeader')is not null)Drop table #CustomerAnswerHeader
If(Object_ID('tempdb..#CustomerAnswer')is not null)Drop table #CustomerAnswer
If(Object_ID('tempdb..#Answer')is not null)Drop table #Answer
If(Object_ID('tempdb..#Questionnaire')is not null)Drop table #Questionnaire
If(Object_ID('tempdb..#vw_CRM_LeadsCategory')is not null)Drop table #vw_CRM_LeadsCategory

Select * 
INTO #CRM_Opportunities 
FROM CRM_Opportunities O With(NoLock) 
WHERE ProjectID in(@ProductID)

Select * 
INTO #CRM_Activity 
FROM CRM_Activity  a With(NoLock) 
WHERE ActivityType in(2,3) 
AND a.StatusID<>-1 
AND a.ReferentID in(Select OpportunityID From #CRM_Opportunities);

Select *
Into #CRM_OpportunitiesDetail
From CRM_OpportunitiesDetail
WHERE OpportunityID in(Select OpportunityID From #CRM_Opportunities);

Select * 
INTO #CRM_Leads 
FROM CRM_Leads O With(NoLock) 
WHERE ProjectID in(@ProductID)

Select * 
INTO #CRM_LeadsActivity 
FROM CRM_Activity  a With(NoLock) 
WHERE ActivityType in(1) 
AND a.StatusID<>-1 
AND a.ReferentID in(Select LeadsID From #CRM_Leads);

select *
into #vw_CRM_LeadsCategory
from vw_CRM_LeadsCategory a
where  a.LeadsSubCategoryID in (select CurrentCategoryID from #CRM_Leads)

--------------------#TmpOp-----------------------------
select  TA.OpportunityID,TA.ProjectID ,a.ReferentID,ta.ContactID,ta.OpportunityNo,ta.OwnerID, a.ActivityDate,a.CreateDate,a.ActualDate,ActivityType,a.TaskID,a.StatusID
,'ProductCompare'=convert(nvarchar(1000),'')
,'UnitNumber' =  Isnull(ISNULL(TA.UnitNumber1,'')+CASE WHEN ISNULL(TA.UnitNumber2,'')='' THEN ''ELSE' , '+ISNULL(TA.UnitNumber2,'')END,'')
,'CustomerFeature' =  (select top 1 remark from #CRM_OpportunitiesDetail D  With(NoLock)where  d.OpportunityID = TA.OpportunityID and d.CategoryID = 1)
,'Probability' = CASE WHEN TA.OpportunityLevelID = 'H' THEN 'High' 
					WHEN TA.OpportunityLevelID = 'M' THEN 'Medium'
					WHEN TA.OpportunityLevelID = 'L' THEN 'Low'  END
,a.remark
,'SourceType'=Case Isnull(l.LeadsTypeID,'')when 'W'Then'Web' When'C'Then'Call' When 'F' Then 'FaceBook'Else 'First walk'End
,'SourceDate'=Case Isnull(l.LeadsTypeID,'')when 'W'Then l.LeadDate When'C'Then l.LeadDate Else ta.OpportunityDate End
,ta.OpportunityDate
,'OppDetail2'=convert(nvarchar(1000),'')
,'OppDetail3'=convert(nvarchar(1000),'')
,'OppDetail4'=convert(nvarchar(1000),'')
,'OppDetail6'=convert(nvarchar(1000),'')
,'OppDetail7'=convert(nvarchar(1000),'')
,'OppDetail8'=convert(nvarchar(1000),'')
,'OppDetail9'=convert(nvarchar(1000),'')
,'PreviousStatus'=convert(nvarchar(1000),'')
,a.ActivityID
into #TmpOp
from #CRM_Opportunities TA WITH (NOLOCK)
left join #CRM_Activity a WITH (NOLOCK)on TA.OpportunityID = a.ReferentID and a.ActivityType in(2,3) and a.StatusID<>-1
Left Join ICON_EntForms_Contacts con WITH (NOLOCK)on (con.ItemID=ta.ContactID)
Left Join #CRM_Leads l WITH (NOLOCK)on l.ContactID=con.ContactID and l.ContactID is not null and  ta.ProjectID = l.ProjectID
where 1=1 
and (Isnull(@ProductID,'')='' or ta.ProjectID=@ProductID)
and ((dbo.fn_ClearTime(isnull(a.ActualDate,'18000101')) BETWEEN @DateStart2 and @DateEnd2InStore)
or (dbo.fn_ClearTime(isnull(ta.OpportunityDate,'18000101')) BETWEEN @DateStart2 and @DateEnd2InStore)
or (dbo.fn_ClearTime(isnull(l.LeadDate,'18000101'))  BETWEEN @DateStart2 and @DateEnd2InStore))
and ( 
	 (@TaskWalk1=1 and @FirstWalk=1 and ActivityType='2' and TaskID=1)
	or (@TaskWalk2=1 and @FirstWalk=1 and ActivityType='2' and TaskID=2)
	or (@TaskWalk3=1 and @FirstWalk=1 and ActivityType='2' and TaskID=3)
	or (@TaskWalk4=1 and @FirstWalk=1 and ActivityType='2' and TaskID=4)
	or (@TaskWalk5=1 and @FirstWalk=1 and ActivityType='2' and TaskID=5)
	or (@TaskWalk6=1 and @FirstWalk=1 and ActivityType='2' and TaskID=6)
	or (@TaskWalkEnd=1 and @FirstWalk=1 and ActivityType='2' and TaskID=7)

	or (@TaskRevisit1=1 and @Revisit=1 and ActivityType='3' and TaskID=1)
	or (@TaskRevisit2=1 and @Revisit=1 and ActivityType='3' and TaskID=2)
	or (@TaskRevisit3=1 and @Revisit=1 and ActivityType='3' and TaskID=3)
	or (@TaskRevisit4=1 and @Revisit=1 and ActivityType='3' and TaskID=4)
	)
Order by ta.ProjectID,ta.OpportunityDate

Update #TmpOp
Set PreviousStatus=Left(Isnull((Select Top 1 Remark From #CRM_Activity tt  With(NoLock)Where TA.OpportunityID = tt.ReferentID and tt.ActivityType=ta.ActivityType and tt.StatusID<>-1 and tt.ActivityID<ta.ActivityID Order by tt.ActivityDate Desc),''),1000)
From #TmpOp ta

-- Update PreviousStatus for first activity
Update #TmpOp
Set PreviousStatus=Left(Isnull((Select Top 1 Remark From #CRM_Activity tt  With(NoLock)Where t.ActivityID = tt.ActivityID),''),1000)
From #TmpOp t
Where ActivityID In(Select min(ActivityID)ActivityID	
					From #CRM_Activity a  With(NoLock)
					Where a.ReferentID=t.OpportunityID and a.ActivityType=t.ActivityType
					Group by ReferentID,ActivityType)

Update #TmpOp
Set UnitNumber=Isnull(OppDetail2,''),OppDetail2=''
Where Isnull(UnitNumber,'')=''


-------------------------------#TmpLeads-----------------------------
select  l.LeadsID,dbo.fn_ClearTime(l.LeadDate) LeadDate,l.ContactID,l.ProjectID,l.LeadsTypeID,l.Remark as RemarkLeads,l.OwnerID,l.FirstName
,TaskID,StatusID
,l.LastName,l.Mobile,l.HouseID,l.Moo,l.Village,l.Soi,l.Road,l.District,l.SubDistrict,l.Province,l.PostalCode
,a.ActivityID,a.ReferentID,a.ActivityType,a.ActivityDate,a.ActualDate,a.Remark as RemarkAct,a.CreateDate ,a.EditDate 
,'SourceType'=Case Isnull(l.LeadsTypeID,'')when 'W'Then'Web' When'C'Then'Call' When 'F' Then 'FaceBook'Else ''End
,'SourceDate'=dbo.fn_ClearTime(l.LeadDate)
,'PreviousStatus'=Isnull((Select Top 1 Remark From #CRM_Activity tt  With(NoLock)Where a.ReferentID = tt.ReferentID and tt.ActivityType=a.ActivityType and tt.StatusID<>-1 and tt.ActivityID<a.ActivityID Order by tt.ActivityDate Desc),'')
,b.LeadsSubCategory
into #TmpLeads
from  #CRM_Leads l WITH (NOLOCK)
left join #CRM_LeadsActivity a WITH (NOLOCK)on l.LeadsID = a.ReferentID and a.ActivityType=1 and a.StatusID<>-1
left join #vw_CRM_LeadsCategory b on b.LeadsSubCategoryID = l.CurrentCategoryID 
where 1=1 
AND Isnull(l.ContactID,'')='' -- เฉพาะรายการ Lead ที่ยังไม่เป็น Opp
and (l.ProjectID in(@ProductID))
and (dbo.fn_ClearTime(isnull(l.LeadDate,'18000101')) BETWEEN @DateStart2 and @DateEnd2InStore)
and  (dbo.fn_ClearTime(isnull(a.ActualDate,'18000101')) BETWEEN @DateStart2 and @DateEnd2InStore)-------- จะต้องมีวันที่ทำจริงเท่านั้น 
and ( 
	   (@TaskLead1=1 and @Lead=1 and ActivityType='1' and TaskID=1)
	or (@TaskLead2=1 and @Lead=1 and ActivityType='1' and TaskID=2)
	or (@TaskLead3=1 and @Lead=1 and ActivityType='1' and TaskID=3)
	or (@TaskLead4=1 and @Lead=1 and ActivityType='1' and TaskID=4)
	)
Order by l.ProjectID,l.LeadDate

Update #TmpLeads
Set PreviousStatus=Left(Isnull((Select Top 1 Remark From #CRM_LeadsActivity tt  With(NoLock)Where l.LeadsID = tt.ReferentID and tt.ActivityType=l.ActivityType and tt.StatusID<>-1 and tt.ActivityID<l.ActivityID Order by tt.ActivityDate Desc),''),1000)
From #TmpLeads l

-- Update PreviousStatus for first activity
Update #TmpLeads
Set PreviousStatus=Left(Isnull((Select Top 1 Remark From #CRM_LeadsActivity tt  With(NoLock)Where t.ActivityID = tt.ActivityID),''),1000)
From #TmpLeads t
Where ActivityID In(Select min(ActivityID)ActivityID	
					From #CRM_LeadsActivity a  With(NoLock)
					Where a.ReferentID=t.LeadsID and a.ActivityType=t.ActivityType
					Group by ReferentID,ActivityType)

--------------------#TmpCurrentAcct_Opp-----------------------------
select Max(ActualDate)LastActivityDate,ReferentID,max(ActivityType )ActivityType-----------เนื่องจากuserมีการคีย์ย้อนหลัง
Into #TmpCurrentAcct_Opp
from #CRM_Activity l WITH (NOLOCK)
Where 1=1
AND EXISTS(Select OpportunityID From #TmpOp WHERE OpportunityID=l.ReferentID) 
AND ActivityType in(2,3) 
AND StatusID<>-1
Group by ReferentID

--------------------#TmpCurrentAcct_Lead-----------------------------
select Max(CreateDate)LastActivityDate,ReferentID,max(ActivityType )ActivityType
Into #TmpCurrentAcct_Lead
from #CRM_LeadsActivity l WITH (NOLOCK)
Where 1=1
AND exists(Select LeadsID From #TmpLeads WHERE LeadsID=l.ReferentID)
AND ActivityType =1
AND StatusID<>-1
Group by ReferentID

-------------------------------------------------------------------
Select TA.OpportunityID,TA.ProjectID,LastActivityDate,OpportunityDate,acc.Remark,StatusID,a.ActivityType,TaskID
Into #TmpCurrentOp
From #CRM_Opportunities ta WITH (NOLOCK)
left join #TmpCurrentAcct_Opp a WITH (NOLOCK)on TA.OpportunityID = a.ReferentID
left join #CRM_Activity acc WITH (NOLOCK)on TA.OpportunityID = acc.ReferentID and [dbo].[fn_ClearTime](acc.CreateDate)=a.LastActivityDate and acc.ActivityType in(2,3) -- walk , revisit
Where 1=1 
AND acc.StatusID<>-1

Select l.LeadsID,l.ProjectID,LastActivityDate,LeadDate,acc.Remark,StatusID,a.ActivityType,TaskID,acc.ActivityID,LeadsStatus
Into #TmpCurrentLead
From #CRM_Leads l  With(NoLock)
left join #TmpCurrentAcct_Lead a on l.LeadsID = a.ReferentID
left join #CRM_LeadsActivity acc  With(NoLock)on l.LeadsID = acc.ReferentID and acc.CreateDate=a.LastActivityDate and acc.ActivityType in(1)
Where 1=1 
AND acc.StatusID<>-1

SELECT distinct 'RecType'=Convert(nvarchar(10),'OPP'),
tp.SourceDate IssueDate
,tp.SourceType IssuedStatus 
,'FirstName' = Convert(nvarchar(500),ISNULL(TZ.FirstName,'-'))
,'SurName' = Convert(nvarchar(500),ISNULL(TZ.LastName,'-'))
,'Contact_Tel' = Convert(nvarchar(500),isnull(TZ.Tel_4,''))
,'Address' = Convert(nvarchar(500),(CASE WHEN ISNULL(TZ.SubDistrict_4,'')='' THEN '' 
					WHEN TZ.Province_4 like '%กรุงเทพ%' THEN 'แขวง'+ISNULL(TZ.SubDistrict_4,'')
					ELSE'ต.'+ISNULL(TZ.SubDistrict_4,'') END+' '
			+CASE WHEN ISNULL(TZ.District_4,'')='' THEN '' 
					WHEN TZ.Province_4 like '%กรุงเทพ%' THEN 'เขต'+ISNULL(TZ.District_4,'')
					ELSE'อ.'+ISNULL(TZ.District_4,'') END+' '
			+CASE WHEN ISNULL(TZ.Province_4,'')='' THEN '' 
					WHEN TZ.Province_4 like '%กรุงเทพ%' THEN ISNULL(TZ.Province_4,'')
					ELSE'จ.'+ISNULL(TZ.Province_4,'') END))
,'Address_SubDistrict'=Convert(nvarchar(500),(CASE WHEN ISNULL(TZ.SubDistrict_4,'')='' THEN '' 
					WHEN TZ.Province_4 like '%กรุงเทพ%' THEN 'แขวง'+ISNULL(TZ.SubDistrict_4,'')
					ELSE'ต.'+ISNULL(TZ.SubDistrict_4,'') END))
,'Address_District'=Convert(nvarchar(500),(CASE WHEN ISNULL(TZ.District_4,'')='' THEN '' 
					WHEN TZ.Province_4 like '%กรุงเทพ%' THEN 'เขต'+ISNULL(TZ.District_4,'')
					ELSE'อ.'+ISNULL(TZ.District_4,'') END))
,'Address_Province'=Convert(nvarchar(500),(CASE WHEN ISNULL(TZ.Province_4,'')='' THEN '' 
					WHEN TZ.Province_4 like '%กรุงเทพ%' THEN ISNULL(TZ.Province_4,'')
					ELSE'จ.'+ISNULL(TZ.Province_4,'') END))
,'Address_Job' =  Convert(NVarchar(1000),Left(CASE WHEN ISNULL(TZ.Road_2,'')='' THEN '' 
					ELSE'ถ.'+ISNULL(TZ.Road_2,'') END+' '
			+CASE WHEN ISNULL(TZ.District_2,'')='' THEN '' 
					WHEN TZ.Province_2 like '%กรุงเทพ%' THEN 'เขต'+ISNULL(TZ.District_2,'')
					ELSE'อ.'+ISNULL(TZ.District_2,'') END+' '
			+CASE WHEN ISNULL(TZ.Province_2,'')='' THEN '' 
					WHEN TZ.Province_2 like '%กรุงเทพ%' THEN ISNULL(TZ.Province_2,'')
					ELSE'จ.'+ISNULL(TZ.Province_2,'') End,1000))
,'Address_Job_SubDistrict' =  Convert(NVarchar(500),Left(CASE WHEN ISNULL(TZ.SubDistrict_2,'')='' THEN '' 
					WHEN TZ.Province_2 like '%กรุงเทพ%' THEN 'เขต'+ISNULL(TZ.SubDistrict_2,'')
					ELSE'อ.'+ISNULL(TZ.SubDistrict_2,'') End,500))
,'Address_Job_District' =  Convert(NVarchar(500),Left(CASE WHEN ISNULL(TZ.District_2,'')='' THEN '' 
					WHEN TZ.Province_2 like '%กรุงเทพ%' THEN 'เขต'+ISNULL(TZ.District_2,'')
					ELSE'อ.'+ISNULL(TZ.District_2,'') End,500))
,'Address_Job_Province' =  Convert(NVarchar(500),Left(CASE WHEN ISNULL(TZ.Province_2,'')='' THEN '' 
					WHEN TZ.Province_2 like '%กรุงเทพ%' THEN ISNULL(TZ.Province_2,'')
					ELSE'จ.'+ISNULL(TZ.Province_2,'') End,500))
,'Presentation'=Convert(NVarchar(500),'')
,'OwnerID'=Convert(NVarchar(50),tp.OwnerID)
,'LC' = Convert(NVarchar(500),US.DisplayName)
,p.ProductID, P.Project
,'Budget'=Convert(NVarchar(500),''),'Income_1'=Convert(NVarchar(500),''),'Income_2'=Convert(NVarchar(500),''),'ProductCompare'=Convert(NVarchar(500),'')
,'UnitNumber'=convert(nvarchar(50),tp.UnitNumber )
,'CustomerFeature'=Convert(NVarchar(500),'')
,'Probability'=Convert(nvarchar(50),tp.Probability)
,convert(nvarchar(1000),PreviousStatus) PreviousStatus 
,convert(datetime,null) DateUpdate ,
convert(nvarchar(1000),null) CurrentMess ,
convert(nvarchar(1000),null) CurrentStatus,
convert(nvarchar(50),null) [Status]
,'OpportunityID'=Convert(nvarchar(50),Tp.OpportunityID)
,'ContactID'=Convert(nvarchar(50),TZ.ContactID)
,convert(nvarchar(50),'') ActivityType
,tp.TaskID
,tp.OpportunityDate
,tp.ActualDate
,'OppDetail2' =  Left(Isnull(OppDetail2,''),1000)
,'OppDetail3' =  Left(Isnull(OppDetail3,''),1000)
,'OppDetail4' =  Left(Isnull(OppDetail4,''),1000)
,'OppDetail6' =  Left(Isnull(OppDetail6,''),1000)
,'OppDetail7' =  Left(Isnull(OppDetail7,''),1000)
,'OppDetail8' =  Left(Isnull(OppDetail8,''),1000)
,'OppDetail9' =  Left(Isnull(OppDetail9,''),1000)
,ActivityType ActivityTypeID
,tp.ActivityID,tp.ActivityDate
,'CurrentActivityType'=Convert(nvarchar(50),'')
,'TaskName'=Convert(nvarchar(100),'')
,'LeadsSubCategory' =Convert(nvarchar(100),'')
into #temp
FROM #TmpOp tp 
	Inner Join[ICON_EntForms_Contacts]TZ WITH (NOLOCK)on tp.ContactId = TZ.ItemId
	LEFT OUTER JOIN[ICON_EntForms_Products]P WITH (NOLOCK)ON tp.[ProjectID] = P.ProductID 
	LEFT OUTER JOIN[Users]US WITH (NOLOCK)ON tp.OwnerID = US.EmployeeID 
WHERE 1=1  

---------------------------------Insert #tmp----------------------------
Insert Into #temp
SELECT  distinct  'RecType'='Leads',
	l.SourceDate IssueDate
	,l.SourceType IssuedStatus 
	,'FirstName' = ISNULL(l.FirstName,'-')
	,'SurName' = ISNULL(l.LastName,'-')
	,'Contact_Tel' = l.Mobile
	,'Address' =  isnull(l.HouseID+' '+l.Moo+' '+l.Village+' '+l.Soi+' '+l.Road+' '+l.District+' '+l.SubDistrict+' '+l.Province+' '+l.PostalCode,'')
	,'Address_SubDistrict'=l.SubDistrict
	,'Address_District'=l.District
	,'Address_Province'=l.Province
	,'Address_Job' = ''
	,'Address_Job_SubDistrict'=''
	,'Address_Job_District'=''
	,'Address_Job_Province'=''
	,convert(nvarchar(50),'') Presentation
	,l.OwnerID
	,'LC' = US.DisplayName,p.ProductID, P.Project
	,convert(nvarchar(50),'') Budget 
	,convert(nvarchar(50),'') Income_1 
	,convert(nvarchar(50),'')
	,convert(nvarchar(max),'')ProductCompare
	,convert(nvarchar(50),'')UnitNumber 
	,convert(nvarchar(max),'')CustomerFeature 
	,convert(nvarchar(50),'')Probability 
	,PreviousStatus 
	,convert(datetime,null) DateUpdate ,
	convert(nvarchar(max),'') CurrentMess ,
	convert(nvarchar(50),'') CurrentStatus,
	convert(nvarchar(10),'') [Status]
,l.LeadsID,l.ContactID
,convert(nvarchar(50),'') CurrentType
,l.TaskID
,'OpportunityDate'=null
,l.ActualDate
,'OppDetail2' =  ''
,'OppDetail3' =  ''
,'OppDetail4' =  ''
,'OppDetail6' =  ''
,'OppDetail7' =  ''
,'OppDetail8' =  ''
,'OppDetail9' =  ''
,ActivityType ActivityTypeID
,l.ActivityID,l.ActivityDate
,'CurrentActivityType'=Convert(nvarchar(50),'')
,'TaskName'=Convert(nvarchar(100),'')
,LeadsSubCategory
from  #TmpLeads l
LEFT  JOIN [ICON_EntForms_Products]P  With(NoLock)ON l.ProjectID = P.ProductID 
left join [Users]US  With(NoLock)ON l.OwnerID = US.EmployeeID 
WHERE 1=1  


-------------------------update opp -------------------------	
update #temp 
set DateUpdate		=(select top 1 a.ActualDate
						from     #TmpOp tp
						left join (select max(ActualDate)ActualDate, OpportunityID,ReferentID,ProjectID from  #TmpOp  group by  OpportunityID,ProjectID,ReferentID ) a 
						 on a.OpportunityID = tp.OpportunityID and a.ActualDate = tp.ActualDate and a.ProjectID = tp.ProjectID
						 where  a.OpportunityID = t.OpportunityID and a.ProjectID = t.ProductID ) 

,CurrentMess=Left(isnull((Select Top 1 Remark From #TmpCurrentOp tt Where tt.OpportunityID=t.OpportunityID),''),1000)
,CurrentStatus=Left(isnull((Select Top 1 Isnull(mWalk.MasterText ,mRevisit.MasterText )
				From #TmpCurrentOp tt 
				Left Join CRM_MasterCenter mWalk  With(NoLock)on mWalk.MasterGroup='TaskWalk' and mWalk.MasterValue=tt.TaskID and tt.ActivityType=2
				Left Join CRM_MasterCenter mRevisit  With(NoLock)on mRevisit.MasterGroup='TaskRevisit' and mRevisit.MasterValue=tt.TaskID and tt.ActivityType=3
				Where tt.OpportunityID=t.OpportunityID),''),1000)
,[Status]=(Select Top 1 case when tt.StatusID = 1 then 'Complete'
						 when tt.StatusID = 0 then 'follow up'
						 when tt.StatusID = -1 then 'ไม่เอาแล้ว'
						 end  From #TmpCurrentOp tt Where tt.OpportunityID=t.OpportunityID)
,ActivityType=(Select Top 1 
						 Case when tt.ActivityType = 2 then 'First walk'
						 when tt.ActivityType = 3 then 'Revisit'
						 end  From #TmpOp tt Where tt.ActivityID=t.ActivityID)
from #temp t 
where RecType = 'Opp'

-------------------------update Leads -------------------------	
update #temp 
set DateUpdate		=(select top 1 a.ActualDate
					from #TmpLeads tp
						LEFT join (select max(ActualDate)ActualDate, LeadsID,ReferentID,ProjectID from  #TmpLeads  group by  LeadsID,ProjectID,ReferentID ) a 
						 on a.LeadsID = tp.LeadsID and a.ActualDate = tp.ActualDate and a.ProjectID = tp.ProjectID
						 where  a.ReferentID = t.OpportunityID and a.ProjectID = t.ProductID ) 
,CurrentMess=Left(isnull((Select Top 1 Remark From #TmpCurrentLead tt Where tt.LeadsID=t.OpportunityID),''),1000)
,CurrentStatus=(Select Top 1 Isnull(mLead.MasterText ,'' )
				From #TmpCurrentLead tt 
				Left Join CRM_MasterCenter mLead  With(NoLock)on mLead.MasterGroup='TaskLeads' and mLead.MasterValue=tt.TaskID and tt.ActivityType=1
				Where tt.LeadsID=t.OpportunityID)
,[Status]=(Select Top 1 case when tt.LeadsStatus = 2 then 'Disqualify'
						when tt.StatusID = 1 then 'Complete'
						 when tt.StatusID = 0 then 'Follow up'
						 when tt.StatusID = -1 then 'ไม่เอาแล้ว'					 
						 end  From #TmpCurrentLead tt Where tt.LeadsID=t.OpportunityID)
,ActivityType='Lead'
from #temp t 
where RecType = 'Leads'

Update #temp Set Address_Job='' Where Replace(Address_Job,' ','')='อ.-'
Update #temp Set Address_Job_District='' Where Replace(Address_Job_District,' ','')='อ.-'
Update #temp Set Address='' Where Replace(Address_Job,' ','')='อ.-'
Update #temp Set Address_District='' Where Replace(Address_District,' ','')='อ.-'
Update #temp set CurrentActivityType=Isnull(Case When ActivityTypeID=1 Then 'Lead' When ActivityTypeID=2 Then 'First Walk'When ActivityTypeID=3 Then 'Revisit'End,'')
Update #temp set TaskName=Isnull(m.MasterText,'')
from  #temp t
Left JOin CRM_MasterCenter m on m.MasterGroup=Case When t.ActivityTypeID=1 Then 'TaskLeads'When t.ActivityTypeID=2 Then 'TaskWalk'When t.ActivityTypeID=3 Then 'TaskRevisit'End  and m.MasterValue=Convert(nvarchar(50),t.TaskID)

/* Load QN Data*/
If(Object_ID('tempdb..#ICON_EntForms_Contacts')is not null)Drop table #ICON_EntForms_Contacts
SELECT ItemId,ContactID
INTO #ICON_EntForms_Contacts
FROM ICON_EntForms_Contacts a
WHERE (EXISTS
(
    SELECT ContactID
    FROM #temp
    WHERE ContactID IS NOT NULL AND ContactID = a.ContactID
)
      );

Begin
	If(Object_ID('tempdb..#Project')is not null)Drop table #Project
	If(Object_ID('tempdb..#CustomerAnswerHeader')is not null)Drop table #CustomerAnswerHeader
	If(Object_ID('tempdb..#CustomerAnswer')is not null)Drop table #CustomerAnswer
	If(Object_ID('tempdb..#Answer')is not null)Drop table #Answer
	If(Object_ID('tempdb..#Questionnaire')is not null)Drop table #Questionnaire
	Select * 
	INTO #Project 
	FROM [DBLINK_SVR_QN].APQuestionnaire.dbo.Project p With(NoLock) 
	WHERE p.ProjectCode=@ProductID

	Select ID,CustomerID,QuestionnaireID,QuestionnaireTakenDateOnDevice,ProjectID 
	INTO #CustomerAnswerHeader 
	FROM [DBLINK_SVR_QN].APQuestionnaire.dbo.CustomerAnswerHeader c WITH (NOLOCK) 
	WHERE (exists(Select ID From #Project WHERE ID=c.ProjectID)) --ProjectID in(Select ID From #Project) 
		AND (exists(Select ID From [DBLINK_SVR_QN].APQuestionnaire.dbo.Questionnaire Where QuestionnaireTypeID=1 AND ID=c.QuestionnaireID));

	Select ID,AnswerID,QuestionID,CustomerAnswerHeaderID,AnswerText 
	INTO #CustomerAnswer 
	FROM [DBLINK_SVR_QN].APQuestionnaire.dbo.CustomerAnswer c WITH (NOLOCK) 
	WHERE QuestionID in(89,90,91,92,93,94,95,97,11,128,279,287,294,72,73,327,364,363,365,381,392,396,399,400,401,402,403,404,398) 
		AND EXISTS(Select ID From #CustomerAnswerHeader WHERE id=c.CustomerAnswerHeaderID);

	Select ID,Answer 
	INTO #Answer 
	FROM [DBLINK_SVR_QN].APQuestionnaire.dbo.Answer a WITH (NOLOCK) 
	WHERE exists(Select AnswerID From #CustomerAnswer WHERE AnswerID=a.ID)
End

Select Distinct d.ProjectCode,d.ProjectName,a.CustomerID
,'ContactID'=Convert(NVarchar(50),a.CustomerID)
,Convert(DateTime,Convert(nvarchar(8),a.QuestionnaireTakenDateOnDevice,112)) AnswerDate
,'Answer'= (Case When  isnull(b.AnswerText,'') <> '' then b.AnswerText Else c.Answer end)
,'QuestionType'=Case when QuestionID in(89,90,91,92,93,94,95,97 ,392) THEN 'Media' 
     WHEN QuestionID in(11,294,381,398)Then 'Budget' 
     WHEN QuestionID in(72,365 )Then 'Income' 
     WHEN QuestionID in(73,364 ,396)Then 'IncomeFamily'   
	 When QuestionID in(399)Then 'ลูกค้ามากี่คน'
	 When QuestionID in(400)Then 'CustomerFeature'--บุคลิกที่โดดเด่น(จุดเด่น/จุดด้อย)
	 When QuestionID in(128,279,327) Then 'เหตุผลที่แวะมาดูAP'
	 When QuestionID in(401)Then 'ProductCompare'--คู่แข่งที่เปรียบเทียบ
	 When QuestionID in(402)Then 'คนที่ตัดสินใจ'
	 When QuestionID in(403)Then 'ลักษณะสินค้าที่สนใจ'
	 When QuestionID in(363)Then 'ข้อโต้แย้ง'
	 When QuestionID in(404)Then 'ลูกค้าจะซื้อโครงการเราหรือไม่'
	 When QuestionID in(287)Then 'ความคิดเห็นพนักงาน/อื่นๆ' 
	 End 
into #TmpQN
From #CustomerAnswerHeader as a WITH (NOLOCK)
Left Join #CustomerAnswer b WITH (NOLOCK)on a.id=b.CustomerAnswerHeaderID 
Left Join #Answer c WITH (NOLOCK)on b.AnswerID=c.ID 
Left Join #Project d WITH (NOLOCK)on d.ID=a.ProjectID 
Where QuestionID in(89,90,91,92,93,94,95,97,11,128,279,287,294,72,73,327,364,363,365,381,392,396,399,400,401,402,403,404,398) -- Media สื่อ,Budget,Income
and d.ProjectCode=@ProductID

Update #temp
Set Presentation=Left(Isnull((SELECT  distinct   
							STUFF((    SELECT ',' + SUB.Answer AS [text()]
										FROM #TmpQN SUB
										WHERE QuestionType='Media' and 
										SUB.CustomerID = t.CustomerID and SUB.ProjectCode = t.ProjectCode
										FOR XML PATH('') 
										), 1, 1, '' )
							AS [Sub Categories]
							FROM  #TmpQN t
							Where QuestionType='Media' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),''),500)
,Budget= Left(Isnull((SELECT  distinct   
							STUFF((    SELECT ',' + SUB.Answer AS [text()]
										FROM #TmpQN SUB
										WHERE QuestionType='Budget' And SUB.Answer Is Not Null and 
										SUB.CustomerID = t.CustomerID and SUB.ProjectCode = t.ProjectCode
										FOR XML PATH('') 
										), 1, 1, '' )
							AS [Sub Categories]
				FROM  #TmpQN t
				Where QuestionType='Budget' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),''),500)
,Income_1=Left(Isnull((SELECT  distinct   
							STUFF((    SELECT ',' + SUB.Answer AS [text()]
										FROM #TmpQN SUB
										WHERE QuestionType='Income' and 
										SUB.CustomerID = t.CustomerID and SUB.ProjectCode = t.ProjectCode
										FOR XML PATH('') 
										), 1, 1, '' )
							AS [Sub Categories]
				FROM  #TmpQN t
				Where QuestionType='Income' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),''),500)
,Income_2=Left(Isnull((SELECT  distinct   
							STUFF((    SELECT ',' + SUB.Answer AS [text()]
										FROM #TmpQN SUB
										WHERE QuestionType='IncomeFamily' and
										SUB.CustomerID = t.CustomerID and SUB.ProjectCode = t.ProjectCode
										FOR XML PATH('') 
										), 1, 1, '' )
							AS [Sub Categories]
				FROM  #TmpQN t
				Where QuestionType='IncomeFamily' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),''),500)
,ProductCompare=Left(Isnull(temp.ProductCompare,'')+' '+ IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='ProductCompare' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
,CustomerFeature=Left(Isnull(temp.CustomerFeature,'')+' '+IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='CustomerFeature' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
, OppDetail8 =  Left(IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='ลูกค้ามากี่คน' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
, OppDetail3 =  Left(IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='เหตุผลที่แวะมาดูAP' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
, OppDetail4 =  Left(IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='คนที่ตัดสินใจ' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
, OppDetail2 =  Left(IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='ลักษณะสินค้าที่สนใจ' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
, OppDetail7 =  Left(IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='ข้อโต้แย้ง' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
, OppDetail6 =  Left(IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='ลูกค้าจะซื้อโครงการเราหรือไม่' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)
, OppDetail9 =  Left(IsNull((Select Top 1 t.Answer From #TmpQN t Where t.QuestionType='ความคิดเห็นพนักงาน/อื่นๆ' and t.ProjectCode=temp.ProductID and t.ContactID=temp.ContactID),'') ,500)

From #temp temp

select *,
@DateStart2 AS DateStart2,
@DateEnd2 AS DateEnd2
from #temp 
where ((@Disqualify=0  )
						or (@Disqualify=1  and status <> 'Disqualify')
						or (@Disqualify=2  and status = 'Disqualify'))
and  (Isnull(@EmpCode,'')='' or OwnerID=@EmpCode)


GO
