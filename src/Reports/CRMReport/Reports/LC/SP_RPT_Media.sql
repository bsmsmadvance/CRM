SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec "db_iconcrm_fusion"."dbo"."SP_RPT_Media";1 N'', {ts '2016-10-31 00:00:00'}, {ts '2016-11-20 00:00:00'}, N'AP001383'

ALTER  PROC [dbo].[SP_RPT_Media]
	@ProductID as nvarchar(50)='',
	@DateStart as DateTime =null,--ActualDate
	@DateEnd as DateTime =null
	,@UserName  nvarchar(100) = ''

AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
If(@DateStart is null)Set @DateStart='18000101'
If(@DateEnd is null)Set @DateEnd='70001231'


Set @ProductID =Isnull(@ProductID,'')

if(object_id('tempdb..#TempQuestionaire')is not null)drop table #TempQuestionaire;
IF OBJECT_ID('tempdb..#TmpOp') IS NOT NULL DROP TABLE #TmpOp
IF OBJECT_ID('tempdb..#TmpLeads') IS NOT NULL DROP TABLE #TmpLeads
if(object_id('tempdb..#TmpBook')is not null)drop table #TmpBook;
if(object_id('tempdb..#Tmp')is not null)drop table #Tmp;

-----------------------------#TmpBook   Load all booking-----------------------------------------------------
/* Print('#TmpBook')
select bo.ContactID,b.BookingNumber,b.BookingDate,b.ContractDueDate,b.ApproveDate,b.ProductID,b.UnitNumber
into #TmpBook
FROM  [ICON_EntForms_Booking] b   
left join [dbo].[ICON_EntForms_BookingOwner] bo on bo.bookingnumber = b.bookingnumber 	
where  bo.header = 1 and isnull(bo.IsDelete,0) = 0 and b.canceldate is null 
and  (ISNULL(@ProductID,'')='' or b.ProductID=@ProductID) 
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล */

--------------------TempQuestionaire--------------------------------------------
/* Print('#TempQuestionaire')
Select Distinct d.ProjectCode,d.ProjectName,a.CustomerID
,'ContactID'=(Select Top 1 DataSourceCustomerID From [DBLINK_SVR_SCV].APSCV.dbo.SCVCustomerMapping t where t.SCVCustomerId=f.SCVCustomerId Order by 1 Desc)
,f.CustFirstName FirstName,f.CustLastName LastName,Convert(DateTime,Convert(nvarchar(8),a.QuestionnaireTakenDateOnDevice,112)) AnswerDate,q.id questionid
,'Question'=Case q.id When 90 Then 'ป้าย' When 91 Then 'หนังสือพิมพ์' When 92 Then 'อินเตอร์เน็ต' When 93 Then 'สื่ออื่นๆ' When 94 Then 'โทรทัศน์' When 95 Then 'นิตยสาร' Else '' End
,c.id AnswerID, c.Answer 
,'Sort'=Case q.id When 90 Then 1 When 91 Then 2 When 92 Then 3 When 93 Then 4 When 94 Then 5 When 95 Then 6 Else '' End
,'IsBook'=0,'ActivityType'=0,'TaskID'=0,'IsLead'=0
Into #TempQuestionaire
From [DBLINK_SVR_QN].APQuestionnaire.dbo.CustomerAnswerHeader a
Left Join [DBLINK_SVR_QN].APQuestionnaire.dbo.CustomerAnswer b on a.id=b.CustomerAnswerHeaderID
Left Join [DBLINK_SVR_QN].APQuestionnaire.dbo.Answer c on b.AnswerID=c.ID
left join [DBLINK_SVR_QN].APQuestionnaire.dbo.Question q on b.questionid = q.id
Left Join [DBLINK_SVR_QN].APQuestionnaire.dbo.Project d on d.ID=a.ProjectID
Left Join [DBLINK_SVR_QN].APQuestionnaire.dbo.Questionnaire e on e.ID=a.QuestionnaireID
Left Join [DBLINK_SVR_SCV].APSCV.dbo.SCVCustomer f on f.SCVCustomerId=a.CustomerId
Where b.QuestionID in(90,91,92,93,94,95)-- Media สื่อ
and e.QuestionnaireTypeID=1 -- F1 Only
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล
and (ISNULL(@ProductID,'')='' or d.ProjectCode=@ProductID) 
order by ContactID */
 
--------------------------------------#TmpOp-------------------------------------
/* Print('#TmpOp')
select  TA.OpportunityID,TA.ProjectID ,c.ContactID,ta.OpportunityNo,ta.OwnerID,dbo.fn_ClearTime(tA.OpportunityDate)OpportunityDate
,'IsBook'=0,'ActivityType'=0,'TaskID'=0
into #TmpOp
from CRM_Opportunities TA 
Left Join ICON_EntForms_Contacts c on ta.ContactID=c.ItemID
where (ISNULL(@ProductID,'')='' or ta.ProjectID=@ProductID)
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล
and   ((YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) = 7000) or (dbo.fn_ClearTime(tA.OpportunityDate) BETWEEN Convert(nvarchar(10),@DateStart,120) and Convert(nvarchar(10),@DateEndInStore,120)))

Update #TmpOp
Set IsBook=Case When Exists(Select * From #TmpBook b Where b.ContactID=t.ContactID)Then 1 Else 0 End
From #TmpOp t

Update #TmpOp
Set ActivityType=Isnull((Select Max(ActivityType)From CRM_Activity acc Where acc.ReferentID=t.OpportunityID and acc.ActivityType in(2,3)),0)
From #TmpOp t

Update #TmpOp
Set TaskID=Isnull((Select Max(TaskID)From CRM_Activity acc Where acc.ReferentID=t.OpportunityID and acc.ActivityType=t.ActivityType),0)
From #TmpOp t */

-----------------------------------#TmpLeads Lead to Opp only------------------------------------------------
/* Print('#TmpLeads')
select  l.LeadsID,l.LeadDate,l.ContactID,l.ProjectID ProjectCode,l.LeadsTypeID,l.Remark as RemarkLeads,l.OwnerID,l.FirstName
,l.LastName,l.Mobile,l.HouseID,l.Moo,l.Village,l.Soi,l.Road,l.District,l.SubDistrict,l.Province,l.PostalCode,l.AdvertisementID,l.AdvertisementID2,l.AdvertisementID3
,ta.OpportunityID
,Question=Case when l.ContactType in('REGISTER','RESALE','PRIVILEGE') Then 'Register' Else l.ContactType End
,'Answer'='','IsBook'=0,'ActivityType'=0,'TaskID'=0
into #TmpLeads
from  CRM_Leads l 
Left Join ICON_EntForms_Contacts c on c.ContactID=l.ContactID
Inner Join CRM_Opportunities TA on c.ItemID=ta.ContactID and l.ProjectID=@ProductID
where (ISNULL(@ProductID,'')='' or l.ProjectID=@ProductID)
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล
and   ((YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) = 7000) or (dbo.fn_ClearTime(tA.OpportunityDate) BETWEEN Convert(nvarchar(10),@DateStart,120) and Convert(nvarchar(10),@DateEndInStore,120)))
and l.ContactType in('REGISTER','RESALE','PRIVILEGE','Call Center','APPOINTMENT')

Update #TmpLeads
Set IsBook=Case When Exists(Select * From #TmpBook b Where b.ContactID=t.ContactID)Then 1 Else 0 End
From #TmpLeads t

Update #TmpLeads
Set ActivityType=Isnull((Select Max(ActivityType)From CRM_Activity acc Where acc.ReferentID=t.OpportunityID and acc.ActivityType in(2,3)),0)
From #TmpLeads t

Update #TmpLeads
Set TaskID=Isnull((Select Max(TaskID)From CRM_Activity acc Where acc.ReferentID=t.OpportunityID and acc.ActivityType=t.ActivityType),0)
From #TmpLeads t */


----------------- Update #TempQuestionaire ----------------
/* Update #TempQuestionaire
Set IsBook=Case When Exists(Select * From #TmpBook b Where b.ContactID=t.ContactID)Then 1 Else 0 End
From #TempQuestionaire t

Update #TempQuestionaire
Set ActivityType=Isnull((Select Max(ActivityType)From #TmpOp op Where op.ContactID=t.ContactID),0)
From #TempQuestionaire t

Update #TempQuestionaire
Set TaskID=Isnull((Select Max(TaskID)From #TmpOp op Where op.ContactID=t.ContactID),0)
From #TempQuestionaire t

Update #TempQuestionaire
Set IsLead=Case When Exists(Select * From #TmpLeads b Where b.ContactID=t.ContactID)Then 1 Else 0 End
From #TempQuestionaire t */

---------------------- Create Temp table for result -------------------------------------------------
Print('#Tmp')
select distinct 'ProjectCode' = '' --ProjectCode
,'ProjectName' = '' --ProjectName
,'QuestionID' = '' --QuestionID
,'Question' = '' --Question
,'AnswerID' = '' --AnswerID
,'Answer' = '' --Answer
,'Sort' = '' --Sort
,walk_1 = 0 
,Revisit_1 = 0
,Revisit_2 = 0
,Revisit_3 = 0
,[Revisit_>3] = 0
,Walk_LCCall = 0
,BookWalk_1 = 0 
,BookRevisit_1 = 0
,BookRevisit_2 = 0
,BookRevisit_3 = 0
,[BookRevisit_>3] = 0
,Book_LCCall = 0
,Sum_walk_1 = 0 
,Sum_Revisit_1 = 0
,Sum_Revisit_2 = 0
,Sum_Revisit_3 = 0
,[Sum_Revisit_>3] = 0
,Sum_Walk_LCCall = 0
,Sum_BookWalk_1 = 0 
,Sum_BookRevisit_1 = 0
,Sum_BookRevisit_2 = 0
,Sum_BookRevisit_3 = 0
,[Sum_BookRevisit_>3] = 0
,Sum_Book_LCCall = 0
Into #Tmp
--from  #TempQuestionaire t

------------------------Insert Call Center to temp ----------------------------------------
/* Insert Into #Tmp
select distinct ProductID  ,Project
,Questionid = 777,Question = 'Call Center'
,AnswerID=0,Answer='',Sort=10
,walk_1 = 0 ,Revisit_1 = 0,Revisit_2 = 0,Revisit_3 = 0,[Revisit_>3] = 0,Walk_LCCall = 0,BookWalk_1 = 0 ,BookRevisit_1 = 0,BookRevisit_2 = 0
,BookRevisit_3 = 0,[BookRevisit_>3] = 0,Book_LCCall = 0
,Sum_walk_1 = 0 ,Sum_Revisit_1 = 0,Sum_Revisit_2 = 0,Sum_Revisit_3 = 0,[Sum_Revisit_>3] = 0,Sum_Walk_LCCall = 0
,Sum_BookWalk_1 = 0 ,Sum_BookRevisit_1 = 0,Sum_BookRevisit_2 = 0,Sum_BookRevisit_3 = 0,[Sum_BookRevisit_>3] = 0,Sum_Book_LCCall = 0
from ICON_EntForms_Products a
Where (Isnull(@ProductID,'')='' or a.ProductID=@ProductID)
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล */

------------------------Insert Web Register to temp ----------------------------------------
/* Insert Into #Tmp
select distinct ProductID  ,Project
,Questionid = 888,Question = 'Web Register'
,AnswerID=0,Answer='',Sort=11
,walk_1 = 0 ,Revisit_1 = 0,Revisit_2 = 0,Revisit_3 = 0,[Revisit_>3] = 0,Walk_LCCall = 0,BookWalk_1 = 0 ,BookRevisit_1 = 0,BookRevisit_2 = 0
,BookRevisit_3 = 0,[BookRevisit_>3] = 0,Book_LCCall = 0
,Sum_walk_1 = 0 ,Sum_Revisit_1 = 0,Sum_Revisit_2 = 0,Sum_Revisit_3 = 0,[Sum_Revisit_>3] = 0,Sum_Walk_LCCall = 0
,Sum_BookWalk_1 = 0 ,Sum_BookRevisit_1 = 0,Sum_BookRevisit_2 = 0,Sum_BookRevisit_3 = 0,[Sum_BookRevisit_>3] = 0,Sum_Book_LCCall = 0
from ICON_EntForms_Products a
Where (Isnull(@ProductID,'')='' or a.ProductID=@ProductID)
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล */

------------------------Insert Appointment to temp ----------------------------------------
/* Insert Into #Tmp
select distinct ProductID  ,Project
,Questionid = 999,Question = 'Appointment'
,AnswerID=0,Answer='',Sort=12
,walk_1 = 0 ,Revisit_1 = 0,Revisit_2 = 0,Revisit_3 = 0,[Revisit_>3] = 0,Walk_LCCall = 0,BookWalk_1 = 0 ,BookRevisit_1 = 0,BookRevisit_2 = 0
,BookRevisit_3 = 0,[BookRevisit_>3] = 0,Book_LCCall = 0
,Sum_walk_1 = 0 ,Sum_Revisit_1 = 0,Sum_Revisit_2 = 0,Sum_Revisit_3 = 0,[Sum_Revisit_>3] = 0,Sum_Walk_LCCall = 0
,Sum_BookWalk_1 = 0 ,Sum_BookRevisit_1 = 0,Sum_BookRevisit_2 = 0,Sum_BookRevisit_3 = 0,[Sum_BookRevisit_>3] = 0,Sum_Book_LCCall = 0
from ICON_EntForms_Products a
Where (Isnull(@ProductID,'')='' or a.ProductID=@ProductID)
And ISNULL(@ProductID,'')<>'' -- ถ้าไม่ระบุโครงการจะไม่แสดงข้อมูล */

----------------------------------Update From Question-----------------------------------
/* update #Tmp
set walk_1 =  Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=2)  ,0)

,Revisit_1 =  Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID=1)  ,0)

,Revisit_2 =  Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID=2)  ,0)

,Revisit_3 =  Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID=3)  ,0)

,[Revisit_>3] =  Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID>3)  ,0)

,Walk_LCCall = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.IsLead=1)  ,0)

,BookWalk_1 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=2 )  ,0)

,BookRevisit_1 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID=1)  ,0)

,BookRevisit_2 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID=2)  ,0)

,BookRevisit_3 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID=3)  ,0)

,[BookRevisit_>3] = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID>3)  ,0)

,Book_LCCall = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.IsLead=1)  ,0)

,Sum_walk_1 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				Where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=2)  ,0) 
,Sum_Revisit_1 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID=1)  ,0)
,Sum_Revisit_2 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID=2)  ,0)
,Sum_Revisit_3 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID=3)  ,0)
,[Sum_Revisit_>3] = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID>3)  ,0)
,Sum_Walk_LCCall = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.IsLead=1)  ,0)
,Sum_BookWalk_1 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				Where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=2)  ,0) 
 
,Sum_BookRevisit_1 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID=1)  ,0)
,Sum_BookRevisit_2 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID=2)  ,0)
,Sum_BookRevisit_3 = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID=3)  ,0)
,[Sum_BookRevisit_>3] = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID>3)  ,0)
,Sum_Book_LCCall = Isnull( (select count( distinct a.contactid)  
				from #TempQuestionaire a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.IsLead=1)  ,0)

from #Tmp t
where questionid <777 */

-------------------Update Lead------------------------------------
/* update #Tmp
set walk_1 =  Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=2)  ,0)

,Revisit_1 =  Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID=1)  ,0)

,Revisit_2 =  Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID=2)  ,0)

,Revisit_3 =  Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID=3)  ,0)

,[Revisit_>3] =  Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=0 and a.ActivityType=3  and a.TaskID>3)  ,0)

,Walk_LCCall = 0

,BookWalk_1 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=2 )  ,0)

,BookRevisit_1 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID=1)  ,0)

,BookRevisit_2 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID=2)  ,0)

,BookRevisit_3 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID=3)  ,0)

,[BookRevisit_>3] = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.Question = t.Question and  a.Answer = t.Answer and a.ProjectCode = t.ProjectCode
				and a.IsBook=1 and a.ActivityType=3 and a.TaskID>3)  ,0)

,Book_LCCall = 0

,Sum_walk_1 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				Where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=2)  ,0) 
,Sum_Revisit_1 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID=1)  ,0)
,Sum_Revisit_2 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID=2)  ,0)
,Sum_Revisit_3 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID=3)  ,0)
,[Sum_Revisit_>3] = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=0 and a.ActivityType=3  and a.TaskID>3)  ,0)
,Sum_Walk_LCCall = 0
,Sum_BookWalk_1 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				Where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=2)  ,0) 
 
,Sum_BookRevisit_1 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID=1)  ,0)
,Sum_BookRevisit_2 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID=2)  ,0)
,Sum_BookRevisit_3 = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID=3)  ,0)
,[Sum_BookRevisit_>3] = Isnull( (select count( distinct a.contactid)  
				from #TmpLeads a  
				where a.ProjectCode = t.ProjectCode and a.IsBook=1 and a.ActivityType=3  and a.TaskID>3)  ,0)
,Sum_Book_LCCall = 0
from #Tmp t
where questionid >=777 */


select *
from #Tmp
--order by ProjectCode,Sort,Case When AnswerID=-99 Then 1 Else 0 End,Answer


/* Select distinct a.ProjectCode,a.ProjectName,a.contactid,a.FirstName,a.LastName	
,'Type' = case when a.ActivityType = 2  and a.IsBook=0 then '1stWalk'	
	 when  a.ActivityType=3  and a.TaskID=1  and a.IsBook=0 then 'Revisit1'
	 when  a.ActivityType=3  and a.TaskID=2  and a.IsBook=0 then 'Revisit2'
	 when  a.ActivityType=3  and a.TaskID=3 and a.IsBook=0 then 'Revisit3'
	 when  a.ActivityType=3  and a.TaskID>3  and a.IsBook=0 then 'Revisit>3' 
	 when  a.ActivityType=2  and a.IsBook=1 then 'Book_1stWalk'
	 when  a.ActivityType=3  and a.TaskID=1 and a.IsBook=1 then 'BookRevisit_1'
	 when  a.ActivityType=3  and a.TaskID=2 and a.IsBook=1 then 'BookRevisit_2'
	 when  a.ActivityType=3  and a.TaskID=3  and a.IsBook=1 then 'BookRevisit_3' 
	 when  a.ActivityType=3  and a.TaskID>3  and a.IsBook=1 then 'BookRevisit_>3' 
	 end 
,OpportunityDate 	
 From  #TempQuestionaire  a	
inner join  #TmpOp  b on a.ProjectCode = b.ProjectID  and a.contactid = b.contactid	
left join     #TmpLeads c on a.ProjectCode = c.ProjectCode and  a.contactid = c.contactid	
order by 1	*/

GO
