SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROC [dbo].[SP_RPT_Media_2]
	@ProductID as nvarchar(50)='',
	@DateStart as DateTime =null,--ActualDate
	@DateEnd as DateTime =null

AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
if(object_id('tempdb..#Tmp')is not null)drop table #Tmp;


DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
If(@DateStart is null)Set @DateStart='19000101'
If(@DateEnd is null)Set @DateEnd='70001231'

----------------Insert สื่อCallCenter-----------------------------------------
select distinct 'ProductID' = '' --a.ProductID
,'Project' = '' --a.Project
,'Questionid' = 99 
,'Question' = 'สื่อจาก CallCenter'
,'answerid' = '' --MasterValue as answerid
,'answer' = '' --MasterText as answer
,'CallCenter' = '' --CallCenter = null
into #tmp
/* from CRM_MasterCenter b 
left join [dbo].[ICON_EntForms_Products] a on 1=1
where Mastergroup = 'Advertisement'
and a.RTPExcusive = 1
and  (ISNULL(@ProductID,'')='' or a.ProductID=@ProductID) */


------------Update call center-------------------------------

/* update #tmp
set CallCenter = (select count(l.LeadsID) 
					from CRM_Leads l   
					where isnull(isnull(l.AdvertisementID,l.AdvertisementID2),l.AdvertisementID3) = t.answerid  
					and  l.ProjectID = t.ProductID
					 and   ((dbo.fn_ClearTime(l.LeadDate) BETWEEN Convert(nvarchar(10),@DateStart,120) and Convert(nvarchar(10),@DateEndInStore,120))) )
from #tmp t */

select *
from #tmp
order by 1,5 asc

GO
