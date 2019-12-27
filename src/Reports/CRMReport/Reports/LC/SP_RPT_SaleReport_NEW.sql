SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure

-- exec [SP_RPT_SaleReport]'','10206','20180401','20180408','','w'
--  exec "db_iconcrm_fusion"."dbo"."SP_RPT_SaleReport_NEW";1 '', N'10206', {ts '2017-09-04 00:00:00'}, {ts '2017-09-10 00:00:00'}, N'Administrator','d'

CREATE   PROCEDURE   [dbo].[SP_RPT_SaleReport_NEW]
	@HomeType as nvarchar(50)='',
	@ProductID  as nvarchar(50)='',
	@DateStart as DateTime =null,
	@DateEnd as DateTime =null,
	@UserName  nvarchar(100) = ''
	,@Mode nvarchar(5)=''  -- D=Day    W=Week
As

if(Isnull(@HomeType,'')='ทั้งหมด')set @HomeType=''
if(Isnull(@ProductID,'')='ทั้งหมด')set @ProductID=''
If(Object_ID('tempdb..#TProject')Is not null)Drop table #TProject

-- Load Project 
Select * into #TProject  From(
SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](Isnull(@Username,''))t Where (ISNULL(@ProductID,'')='' or t.ProductID=@ProductID)
Union
SELECT ProductID FROM dbo.ICON_EntForms_Products Where Isnull(RTPExcusive,0)=1 and (Isnull(@Username,'')=''or @Username='Administrator') and (ISNULL(@ProductID,'')='' or ProductID=@ProductID )
)t

Declare @Year int 
set @Year = Year(GetDate())
IF OBJECT_ID('tempdb..#Calendar') IS NOT NULL DROP TABLE #Calendar
 Select * Into #Calendar From DBLINK_SVR_BI.AP_STG.dbo.vw_Calendar_Week With(NoLock) Where Y=@Year

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp
IF OBJECT_ID('tempdb..#Temp_vw_RPTAP2_Walk') IS NOT NULL DROP TABLE #Temp_vw_RPTAP2_Walk
IF OBJECT_ID('tempdb..#temp_minCreate_walk') IS NOT NULL DROP TABLE #temp_minCreate_walk   

IF OBJECT_ID('tempdb..#ICON_EntForms_Booking') IS NOT NULL DROP TABLE #ICON_EntForms_Booking 
Select *,Year(CancelDate)CancelYear Into #ICON_EntForms_Booking From ICON_EntForms_Booking Where BookingDate>='20100101' and (Isnull(@ProductID,'')='' or ProductID=@ProductID) and BookingDate between @DateStart and @DateEnd
Update #ICON_EntForms_Booking set CancelDate=Convert(nvarchar(8),canceldate,112)
IF OBJECT_ID('tempdb..#ICON_EntForms_BookingCancel') IS NOT NULL DROP TABLE #ICON_EntForms_BookingCancel 
Select *,Year(CancelDate)CancelYear Into #ICON_EntForms_BookingCancel From ICON_EntForms_Booking Where BookingDate>='20100101' and (Isnull(@ProductID,'')='' or ProductID=@ProductID) and dbo.fn_ClearTime(CancelDate) between @DateStart and @DateEnd
Update #ICON_EntForms_BookingCancel set CancelDate=Convert(nvarchar(8),canceldate,112)
IF OBJECT_ID('tempdb..#ICON_EntForms_BookingOwner') IS NOT NULL DROP TABLE #ICON_EntForms_BookingOwner
Select * Into #ICON_EntForms_BookingOwner From [ICON_EntForms_BookingOwner] Where BookingNumber In(Select BookingNumber From #ICON_EntForms_Booking)
IF OBJECT_ID('tempdb..#CRM_Opportunities') IS NOT NULL DROP TABLE #CRM_Opportunities 
Select * Into #CRM_Opportunities From CRM_Opportunities Where Isnull(@ProductID,'')='' or ProjectID=@ProductID
Update #CRM_Opportunities set OpportunityDate=convert(nvarchar(8),OpportunityDate,112),CreateDate=convert(nvarchar(8),CreateDate,112)
IF OBJECT_ID('tempdb..#CRM_Leads') IS NOT NULL DROP TABLE #CRM_Leads 
Select * Into #CRM_Leads From CRM_Leads Where Isnull(@ProductID,'')='' or ProjectID=@ProductID
IF OBJECT_ID('tempdb..#vw_RPTAP2_ExV4Booking') IS NOT NULL DROP TABLE #vw_RPTAP2_ExV4Booking  
Select * Into #vw_RPTAP2_ExV4Booking From vw_RPTAP2_ExV4Booking 
if(Isnull(@ProductID,'')<>'')Delete From #vw_RPTAP2_ExV4Booking Where Project<>@ProductID
IF OBJECT_ID('tempdb..#ICON_EntForms_Agreement') IS NOT NULL DROP TABLE #ICON_EntForms_Agreement 
Select ProductID,UnitNumber,SellingPrice,ApproveDate,ContractNumber,CancelDate Into #ICON_EntForms_Agreement From ICON_EntForms_Agreement where ApproveDate>='20100101'and (Isnull(@ProductID,'')='' or ProductID=@ProductID) and ApproveDate between @DateStart and @DateEnd
 IF OBJECT_ID('tempdb..#ICON_EntForms_AgreementTrans') IS NOT NULL DROP TABLE #ICON_EntForms_AgreementTrans
Select ProductID,UnitNumber,SellingPrice,ApproveDate,ContractNumber,CancelDate Into #ICON_EntForms_AgreementTrans From ICON_EntForms_Agreement where ApproveDate>='20100101'and (Isnull(@ProductID,'')='' or ProductID=@ProductID) and CancelDate is null--and ApproveDate between @DateStart and @DateEnd

IF OBJECT_ID('tempdb..#ICON_EntForms_Transfer') IS NOT NULL DROP TABLE #ICON_EntForms_Transfer 
Select ContractNumber,TransferDateApprove,NetSalePrice Into #ICON_EntForms_Transfer From ICON_EntForms_Transfer where TransferDateApprove>='20100101'and ContractNumber in(Select ContractNumber From #ICON_EntForms_AgreementTrans) and TransferDateApprove between @DateStart and @DateEnd
IF OBJECT_ID('tempdb..#CRM_Activity') IS NOT NULL DROP TABLE #CRM_Activity 
Select * Into #CRM_Activity From CRM_Activity where ReferentID in(Select OpportunityID From #CRM_Opportunities)and ActivityType='3' and Isnull(statusid,0)<>-1
Update #CRM_Activity set CreateDate=convert(nvarchar(8),CreateDate,112)
IF OBJECT_ID('tempdb..#ZTEMP_RPT_SaleReport_Day') IS NOT NULL DROP TABLE #ZTEMP_RPT_SaleReport_Day
Select * Into #ZTEMP_RPT_SaleReport_Day From DBLINK_SVR_CRMREP.db_iconcrm_fusion.dbo.ZTEMP_RPT_SaleReport_Day Where Productid = @ProductID
IF OBJECT_ID('tempdb..#ZTEMP_RPT_SaleReport_Week') IS NOT NULL DROP TABLE #ZTEMP_RPT_SaleReport_Week
Select * Into #ZTEMP_RPT_SaleReport_Week From DBLINK_SVR_CRMREP.db_iconcrm_fusion.dbo.ZTEMP_RPT_SaleReport_Week Where Productid = @ProductID
IF OBJECT_ID('tempdb..#ICON_EntForms_Contacts') IS NOT NULL DROP TABLE #ICON_EntForms_Contacts 
Select * Into #ICON_EntForms_Contacts From ICON_EntForms_Contacts where (ContactID in (select ContactID from #ICON_EntForms_BookingOwner) or ItemId in (select ContactId from #CRM_Opportunities))

if(@HomeType='ทั้งหมด')Set @HomeType=''

--------------------------#Temp_vw_RPTAP2_Walk ---------------------------
Print('#Temp_vw_RPTAP2_Walk '+Convert(nvarchar(50),getdate(),113))
 select *
 into #Temp_vw_RPTAP2_Walk
 From (SELECT  distinct    1 AS WalkAmt, TB.ProjectID AS Project, 
		DATEPART(Week, ISNULL(TB.OpportunityDate, TB.CreateDate)) AS WeekAmt, 
		DATEPART(Mm, ISNULL(TB.OpportunityDate, TB.CreateDate)) AS MonthAmt, 
		dbo.fn_ClearTime(ISNULL(TB.OpportunityDate, TB.CreateDate)) AS CreateDate
		,TA.Contactid,tb.OpportunityID,tb.OpportunityNo
		FROM         #CRM_Opportunities AS TB with(nolock)  
		left JOIN #ICON_EntForms_Contacts  AS TA with(nolock) ON TA.ItemId = TB.ContactId 
		 Where        TB.ProjectID=@ProductID AND ISNULL(tb.OpportunityStatusID,'')<>'D'
		GROUP BY ISNULL(TB.OpportunityDate, TB.CreateDate),TB.ProjectID,TA.Contactid,tb.OpportunityID,tb.OpportunityNo
)t

 where Project=@ProductID

Print('#temp_minCreate_walk '+Convert(nvarchar(50),getdate(),113))
select Project,ContactID,Min(CreateDate)CreateDate
into #temp_minCreate_walk
from #Temp_vw_RPTAP2_Walk v with(nolock) 
group by Project,ContactID


/*  Load ข้อมูลการจอง  */
Print('@TempBook '+Convert(nvarchar(50),getdate(),113))
declare @TempBook table ( Productid nvarchar(50),Unit int, Price money ,Year int , Week int,EffectiveDate datetime )
Insert into @TempBook
select a.Project ProductID ,ISNULL(SUM(UnitAmt),0) AS Unit,ISNULL(Sum(BKPrice),0) AS Price,w.Y Year,w.W Week,'EffectiveDate'=Case When @Mode='D'Then BookingDate Else NULL End
from #vw_RPTAP2_ExV4Booking a with(nolock) 
left join ICON_EntForms_Products p with(nolock) on a.Project = p.ProductID
Inner Join #Calendar w with(nolock) on BookingDate between w.StartDate and w.EndDate
where 1=1 
and (isnull(@HomeType,'') = '' or @HomeType = PType )
and (isnull(@ProductID,'') = '' or @ProductID = a.Project)
group by a.Project,w.Y,w.W,Case When @Mode='D'Then BookingDate Else NULL End


/*  Load ข้อมูลการทำสัญญา  */
Print('@TempAgree '+Convert(nvarchar(50),getdate(),113))
declare @TempAgree table ( Productid nvarchar(50),Unit int, Price money ,Year int , Week int,EffectiveDate datetime )
Insert into @TempAgree
select a.ProductID ProductID ,count(a.ProductID) AS Unit,ISNULL(Sum(SellingPrice),0) AS Price,w.Y Year,w.W Week,'EffectiveDate'=Case When @Mode='D'Then ApproveDate Else NULL End
from #ICON_EntForms_Agreement a with(nolock) 
left join ICON_EntForms_Products p with(nolock) on a.ProductID = p.ProductID
Inner Join #Calendar w with(nolock) on a.ApproveDate between w.StartDate and w.EndDate
where 1=1
and (isnull(@HomeType,'') = '' or @HomeType = PType )
and (isnull(@ProductID,'') = '' or @ProductID = a.ProductID)
group by a.ProductID,w.Y,w.W,Case When @Mode='D'Then ApproveDate Else NULL End


/*  Load ข้อมูลการโอน  */
Print('@tempTrans '+Convert(nvarchar(50),getdate(),113))
declare @tempTrans table (Productid nvarchar(50),Unit int, Price money ,Year int , Week int,EffectiveDate datetime)
Insert into @tempTrans
select  b.ProductID, Count(*),SUM(a.NetSalePrice),w.Y,w.W, a.TransferDateApprove 
from #ICON_EntForms_AgreementTrans b with(nolock) 
Inner Join #ICON_EntForms_Transfer a with(nolock) on a.ContractNumber=b.ContractNumber and b.Canceldate is null
Inner Join #Calendar w with(nolock) on a.TransferDateApprove between w.StartDate and w.EndDate
where b.ProductID=@ProductID --a.Unit_Status='T'
Group By b.ProductID,w.Y,w.W, a.TransferDateApprove 



/*  Load ข้อมูลการ Walk  */
Print('@tempWalk '+Convert(nvarchar(50),getdate(),113))
If(Object_Id('tempdb..#tempWalk')Is Not Null)Drop Table #tempWalk
select a.Project,ContactID,Min(a.CreateDate)CreateDate,1 Walk ,w.Y [Year],w.W [Week],OpportunityNo
Into #tempWalk
from #Temp_vw_RPTAP2_Walk  a with(nolock) 
left join ICON_EntForms_Products p with(nolock) on a.Project = p.ProductID
Inner Join #Calendar w  with(nolock) On a.CreateDate between w.StartDate and w.EndDate
where  (isnull(@HomeType,'') = '' or @HomeType = PType ) 
and (isnull(@ProductID,'') = '' or @ProductID = a.Project)
group by a.Project,ContactID,w.Y,w.W,OpportunityNo


/*  Load ข้อมูลการ Revisit  */
Print('@tempRevisit '+Convert(nvarchar(50),getdate(),113))
declare @tempRevisit table (Project nvarchar(50),ContactID nvarchar(50), CreateDate datetime, Revisit  int ,[Year] int,[Week] int)
Insert into @tempRevisit
SELECT ta.ProjectID Project, ContactId,dbo.fn_ClearTime(ac.CreateDate) CreateDate ,Row_Number()Over(Partition by ContactID Order by ac.CreateDate asc) Revisit,w.Y,w.W
FROM #CRM_Opportunities ta with(nolock) 
Inner Join #CRM_Activity ac with(nolock) on ac.ReferentID=ta.OpportunityID --and ac.ActivityType='3' and Isnull(ac.statusid,0)<>-1
left join ICON_EntForms_Products p with(nolock) on ta.ProjectID = p.ProductID
Inner Join #Calendar w with(nolock) on dbo.fn_ClearTime(ac.CreateDate) between w.StartDate and w.EndDate
where (isnull(@HomeType,'') = '' or @HomeType = PType )AND ISNULL(ta.OpportunityStatusID,'')<>'D'
and (isnull(@ProductID,'') = '' or @ProductID = ta.ProjectID)
and dbo.fn_ClearTime(ac.ActualDate) between @DateStart and @DateEnd

Print('@tempBookRevisit '+Convert(nvarchar(50),getdate(),113))
declare @tempBookRevisit table (Project nvarchar(50),ContactID nvarchar(50), CreateDate datetime, Revisit  int,[Year] int,[Week] int) 
Insert into @tempBookRevisit
SELECT b.ProductID, c.ContactId,dbo.fn_ClearTime(ac.CreateDate) ,Row_Number()Over(Partition by c.ContactID,unitnumber,b.ProductID Order by ac.CreateDate,b.ProductID,unitnumber asc) Revisit,w.Y,w.W
FROM  #ICON_EntForms_Booking b   with(nolock) 
left join #ICON_EntForms_BookingOwner bo with(nolock) on bo.bookingnumber = b.bookingnumber 
left join #ICON_EntForms_Contacts c with(nolock) on bo.contactid = c.ContactID
left join #CRM_Opportunities ta with(nolock) on (ta.Opportunityno=b.OpID)or ta.Opportunityno=b.OpID2 or ta.Opportunityno=b.OpID3
Inner Join #CRM_Activity ac with(nolock) on ac.ReferentID=ta.OpportunityID
left join ICON_EntForms_Products p with(nolock) on b.ProductID = p.ProductID		AND ISNULL(ta.OpportunityStatusID,'')<>'D'		
Inner Join #Calendar w with(nolock) on dbo.fn_ClearTime(ac.CreateDate) between w.StartDate and w.EndDate
WHERE 
dbo.fn_ClearTime(ac.CreateDate)<>'20130930'
and bo.header = 1 and isnull(bo.IsDelete,0) = 0 and b.canceldate is null 
and (isnull(@HomeType,'') = '' or @HomeType = PType )
and (isnull(@ProductID,'') = '' or @ProductID = b.ProductID)

Print('@tempBookRevisit '+Convert(nvarchar(50),getdate(),113))
Insert into @tempBookRevisit
Select	b.productid,bo.contactid,t.createDate,Row_Number()Over(Partition by bo.ContactID,unitnumber,b.productid Order by t.CreateDate,b.productid,unitnumber asc) Revisit,w.Y,w.W
From	#ICON_EntForms_Booking b   with(nolock) 
left join #ICON_EntForms_BookingOwner bo with(nolock) on bo.bookingnumber = b.bookingnumber 
left join #Temp_vw_RPTAP2_Walk t with(nolock) on b.productid = t.Project and bo.ContactID = t.Contactid
left join ICON_EntForms_Products p with(nolock) on b.productid = p.ProductID
Inner Join #Calendar w with(nolock) on t.createDate between w.StartDate and w.EndDate				
Where t.CreateDate<>(select CreateDate
					from #temp_minCreate_walk v
					where t.Project=v.Project and t.ContactID=v.ContactID
					)
and bo.header = 1 and isnull(bo.IsDelete,0) = 0 and b.canceldate is null 
and (isnull(@HomeType,'') = '' or @HomeType = PType )
and (isnull(@ProductID,'') = '' or @ProductID = b.ProductID)

Print('#temp '+Convert(nvarchar(50),getdate(),113))
select Distinct y 
,case when m = 1 then 'Jan' + '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 2 then 'Feb'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 3 then 'Mar'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 4 then 'Apr'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 5 then 'May'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 6 then 'Jun'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 7 then 'Jul'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 8 then 'Aug'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 9 then 'Sep'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 10 then 'Oct'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 11 then 'Nov'+ '_'+substring(convert (nvarchar ,y),3,2)  
		when m = 12 then 'Dec'+ '_'+substring(convert (nvarchar ,y),3,2)  end m 
,t.w
, 'Startdate'=Case When @Mode='W' Then t.Startdate Else d.Date_Value End
, 'EndDate'=Convert(datetime,Convert(nvarchar(8),(Case When (@Mode='W') Then t.EndDate Else d.Date_Value End),112)+' 23:59:59')
,'DateValue'=Case When @Mode='W' Then t.Startdate Else d.Date_Value End
, 'BG' =  (case when Ptype = 1 then 'BG1'
				 when Ptype = 2 then 'BG2'
				 when Ptype = 3 then 'BG3'
				 when Ptype = 4 then 'BG4' end)
,Productid  , Project  
,GrossBook_Unit = null ,GrossBook_Value  = Convert(decimal(18,2),0) 
,CancelUnit =  null ,CancelValue =  Convert(decimal(18,2),0) 
,NetUnit  = null ,NetValue =  Convert(decimal(18,2),0)
,AgreeUnit=  null,AgreeValue = Convert(decimal(18,2),0) 
,TransUnit  = null ,TransValue  = Convert(decimal(18,2),0)
, firstWalk = null
, revisit_1 = null,revisit_2 = null,revisit_3 = null,revisit_More3 = null
,Book_FirstWalk = null, Book_Revisit_1 = null ,Book_Revisit_2 = null ,Book_Revisit_3 = null ,Book_Revisit_More3 = null
,'SumCallcenter'=Convert(int,0),'SumRegister'=Convert(int,0),'SumAppointment'=Convert(int,0)
into #temp
from DBLINK_SVR_BI.AP_STG.dbo.d_cm_date d with(nolock) 
inner join #Calendar t on d.Date_Value between t.startdate and t.EndDate
left join ICON_EntForms_Products p on 1=1
where  p.RTPExcusive = 1 
and (isnull(@HomeType,'') = '' or @HomeType = PType )
and (isnull(@ProductID,'') = '' or @ProductID = p.ProductID)
and d.Year_NO = @Year and d.Date_Value Between @DateStart and @DateEnd

--print @DateStart
--print @DateEnd
--select * from #temp

Print('Update #temp '+Convert(nvarchar(50),getdate(),113))
update #temp
set GrossBook_Unit = isnull((Select	Unit
					From	@TempBook B Where	1=1 
					and  B.productid = P.productid 
					and ( (@Mode='W' and B.Year = @Year and B.Week = p.W) or (@Mode='D' and B.EffectiveDate = p.DateValue) )
					),0)
,GrossBook_Value =	Convert(decimal(18,2),isnull((Select	Price 
					From	@TempBook B Where	1=1 
					and  B.productid = P.productid 
					and ( (@Mode='W' and B.Year = @Year and B.Week = p.W) or (@Mode='D' and B.EffectiveDate = p.DateValue) )
					),0)/1000000)

from #temp p
---------------------------------------Cancel-------------------------------
Update #temp
Set CancelUnit = isnull((SELECT COUNT(UnitNumber) 
						FROM #ICON_EntForms_BookingCancel B 
						Inner Join #Calendar w on dbo.fn_cleartime(CancelDate) between w.StartDate and w.EndDate
						WHERE ((CancelDate IS NOT NULL) 
								AND (ISNULL(Cancel, 0) IN (3, 1) 
								AND ISNULL(ChangeUnit, 0) = 0 OR
							  ISNULL(Cancel, 0) IN (3, 1) 
								AND ISNULL(ChangeUnit, 0) = 1))
						and ( (@Mode='W' and w.Y = @Year and w.W = p.W) or (@Mode='D' and dbo.fn_cleartime(CancelDate) = p.DateValue) )
						and  B.productid = P.productid   ),0)
				  
,CancelValue = Convert(decimal(18,2),isnull((SELECT SUM(B.SellingPrice-Isnull(b.TransferDiscount,0))
						FROM #ICON_EntForms_BookingCancel B 
						Inner Join #Calendar w on dbo.fn_cleartime(CancelDate) between w.StartDate and w.EndDate
						WHERE ((CancelDate IS NOT NULL) 
								AND (ISNULL(Cancel, 0) IN (3, 1) 
								AND ISNULL(ChangeUnit, 0) = 0 OR
							  ISNULL(Cancel, 0) IN (3, 1) 
								AND ISNULL(ChangeUnit, 0) = 1))
						and ( (@Mode='W' and w.Y = @Year and w.W = p.W) or (@Mode='D' and dbo.fn_cleartime(CancelDate) = p.DateValue) )
						and  B.productid = P.productid   ),0)/1000000)

from #temp p

------------------------------------- Agree------------------------------------------
Update #temp
Set AgreeUnit  = isnull((Select	SUM(Unit )
						From	@TempAgree B Where	1=1 
						and  B.productid = P.productid 
						and ( (@Mode='W' and B.Year = @Year and B.Week = p.W) or (@Mode='D' and B.EffectiveDate = p.DateValue) )
						 ),0)
,AgreeValue  = Convert(decimal(18,2),isnull((Select	SUM(Price )
						From	@TempAgree B Where	1=1 
						and  B.productid = P.productid 
						and ( (@Mode='W' and B.Year = @Year and B.Week = p.W) or (@Mode='D' and B.EffectiveDate = p.DateValue) )
						 ),0)/1000000)

from #temp p
---------------------------------------------Trans---------------------------------------------------------------					
Update #temp
Set TransUnit = Isnull((	select  Sum(a.Unit) from @tempTrans a 	 where a.Productid=p.productid  and a.EffectiveDate<>'20130930' and ( (@Mode='W' and a.Year = @Year and a.Week = p.W) or (@Mode='D' and a.EffectiveDate = p.DateValue) ) ),0)
,TransValue = Convert(decimal(18,2),Isnull((	select  Sum(a.Price) 	from @tempTrans a  where  a.Productid=p.productid  and a.EffectiveDate<>'20130930'and ( (@Mode='W' and a.Year = @Year and a.Week = p.W) or (@Mode='D' and a.EffectiveDate = p.DateValue) )),0)/1000000)

from #temp p
------------------------------------------------------Walk------------------------------------------------
Update #temp
Set firstWalk = isnull((Select	Sum(Walk)  From	#tempWalk w  where w.Project = p.ProductID and ( (@Mode='W' and w.Year = @Year and w.Week = p.W) or (@Mode='D' and w.createdate = p.DateValue) ) ),0)
,revisit_1 = isnull((select count(ContactID) from @tempRevisit r where Revisit  = 1  and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) ) and  r.Project = p.ProductID),0)
,revisit_2 = isnull((select count(ContactID) from @tempRevisit r where Revisit  = 2   and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) )  and  r.Project = p.ProductID),0)
,revisit_3 = isnull((select count(ContactID) from @tempRevisit r where Revisit  = 3   and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) )  and  r.Project = p.ProductID),0)
,revisit_More3 = isnull((select count(ContactID) from @tempRevisit r where Revisit  > 3   and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) )  and  r.Project = p.ProductID),0)
from #temp p

------------------------------------Book Walk--------------------------------------------------------------
If(Object_Id('tempdb..#Book_FirstWalk')Is Not Null)Drop Table #Book_FirstWalk
select Distinct b.UnitNumber,b.Bookingdate,w.CreateDate,c.Y,c.W,cw.Y CWY,cw.W CWW
Into #Book_FirstWalk
FROM  #ICON_EntForms_Booking b   
inner join #tempWalk w on (Isnull(b.OpID,'') = w.OpportunityNo or Isnull(b.OpID2,'') = w.OpportunityNo or Isnull(b.OpID3,'') = w.OpportunityNo)  and b.ProductID = w.Project
Inner Join #Calendar c on dbo.fn_cleartime(b.Bookingdate) between dbo.fn_cleartime(c.StartDate) and dbo.fn_cleartime(c.EndDate)
Inner Join #Calendar cw on dbo.fn_cleartime(w.CreateDate) between dbo.fn_cleartime(cw.StartDate) and dbo.fn_cleartime( cw.EndDate)
Where 1=1  
and  b.Canceldate is Null

If(Object_Id('tempdb..#Book_FirstWalk_Year')Is Not Null)Drop Table #Book_FirstWalk_Year
select count (distinct bo.contactid)CountContactID,year(w.CreateDate)Y,b.ProductID
Into #Book_FirstWalk_Year
FROM  #ICON_EntForms_Booking b   
left join #ICON_EntForms_BookingOwner bo on bo.bookingnumber = b.bookingnumber 		
inner join #tempWalk w on bo.ContactID = w.Contactid  and b.ProductID = w.Project
where  bo.header = 1 and isnull(bo.IsDelete,0) = 0 and b.canceldate is null  
Group by  b.productid,Year(w.CreateDate)
                
Update #temp
Set Book_FirstWalk = isnull((	select count (Distinct UnitNumber)
				FROM  #Book_FirstWalk
				Where 1=1  
				and  ( (@Mode='W' and Y = @Year and W = p.W) or (@Mode='D' and dbo.fn_cleartime(Bookingdate) = dbo.fn_cleartime(p.DateValue)) ) 
				and ( (@Mode='W' and cwY = @Year and cwW = p.W) or (@Mode='D' and dbo.fn_cleartime(CreateDate) = dbo.fn_cleartime(p.DateValue)) ) 
				),0)
from #temp p 

---------------------------- Revisit ------------------------------
Update #temp
Set Book_Revisit_1 = isnull((select count(ContactId)	from @tempBookRevisit r where Revisit = 1 and r.project = p.productid and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) )  ),0)
,Book_Revisit_2 = isnull((select count(ContactId)	from @tempBookRevisit r where Revisit = 2 and r.project = p.productid and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) )  ),0)
,Book_Revisit_3 = isnull((select count(ContactId)	from @tempBookRevisit r where Revisit = 3 and r.project = p.productid and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) )  ),0)
,Book_Revisit_More3 = isnull((select count(ContactId)	from @tempBookRevisit r where Revisit > 3 and r.project = p.productid and ( (@Mode='W' and r.Year = @Year and r.Week = p.W) or (@Mode='D' and r.createdate = p.DateValue) )  ),0)
,SumCallcenter=Isnull((Select Count(*) From #CRM_Leads l Where l.ProjectID=p.ProductID and dbo.fn_ClearTime(l.LeadDate) Between p.StartDate and p.EndDate and Isnull(l.ContactType,'')in('Call Center') and callback = 1  ),0)
,SumRegister=Isnull((Select Count(*) From #CRM_Leads l Where l.ProjectID=p.ProductID and dbo.fn_ClearTime(l.LeadDate) Between p.StartDate and p.EndDate and Isnull(l.ContactType,'')in('REGISTER','RESALE','PRIVILEGE')),0)
,SumAppointment=Isnull((Select Count(*) From #CRM_Leads l Where l.ProjectID=p.ProductID and dbo.fn_ClearTime(l.LeadDate) Between p.StartDate and p.EndDate and Isnull(l.ContactType,'')in('APPOINTMENT')),0)
from #temp p 


/* Update NetUnit,NetValue*/
Print('Update #temp 2 '+Convert(nvarchar(50),getdate(),113))
Update #temp
Set NetUnit=GrossBook_Unit-CancelUnit
,NetValue=GrossBook_Value-CancelValue
Print('UpdatePreviusYear '+Convert(nvarchar(50),getdate(),113))

IF OBJECT_ID('tempdb..#tempPrevious') IS NOT NULL DROP TABLE #tempPrevious
select *
,'GrossBook_Unit_2012'= Convert(int,0)
,'GrossBook_Unit_2013'= Convert(int,0)
,'GrossBook_Unit_2014'= Convert(int,0)
,'GrossBook_Unit_2015'= Convert(int,0)
,'GrossBook_Value_2012'= Convert(decimal(18,2),0)
,'GrossBook_Value_2013'= Convert(decimal(18,2),0)
,'GrossBook_Value_2014'= Convert(decimal(18,2),0)
,'GrossBook_Value_2015'= Convert(decimal(18,2),0)
,'CancelUnit_2012' = Convert(int,0)
,'CancelUnit_2013' = Convert(int,0)
,'CancelUnit_2014' = Convert(int,0)
,'CancelUnit_2015' = Convert(int,0)
,'CancelValue_2012' = Convert(decimal(18,2),0)
,'CancelValue_2013' = Convert(decimal(18,2),0)
,'CancelValue_2014' = Convert(decimal(18,2),0)
,'CancelValue_2015' = Convert(decimal(18,2),0)
,'NetUnit_2012'= Convert(int,0)
,'NetUnit_2013'= Convert(int,0)
,'NetUnit_2014'= Convert(int,0)
,'NetUnit_2015'= Convert(int,0)
,'NetValue_2012'= Convert(decimal(18,2),0)
,'NetValue_2013'= Convert(decimal(18,2),0)
,'NetValue_2014'= Convert(decimal(18,2),0)
,'NetValue_2015'= Convert(decimal(18,2),0)
,'AgreeUnit_2012'= Convert(int,0)
,'AgreeUnit_2013'= Convert(int,0)
,'AgreeUnit_2014'= Convert(int,0)
,'AgreeUnit_2015'= Convert(int,0)
,'AgreeValue_2012'= Convert(decimal(18,2),0)
,'AgreeValue_2013'= Convert(decimal(18,2),0)
,'AgreeValue_2014'= Convert(decimal(18,2),0)
,'AgreeValue_2015'= Convert(decimal(18,2),0)
,'TransUnit_2012'= Convert(int,0)
,'TransUnit_2013'= Convert(int,0)
,'TransUnit_2014'= Convert(int,0)
,'TransUnit_2015'= Convert(int,0)
,'TransValue_2012'= Convert(decimal(18,2),0)
,'TransValue_2013'= Convert(decimal(18,2),0)
,'TransValue_2014'= Convert(decimal(18,2),0)
,'TransValue_2015'= Convert(decimal(18,2),0)
,'firstWalk_2012'= Convert(int,0)
,'firstWalk_2013'= Convert(int,0)
,'firstWalk_2014'= Convert(int,0)
,'firstWalk_2015'= Convert(int,0)
,'revisit_1_2012'= Convert(int,0)
,'revisit_1_2013'= Convert(int,0)
,'revisit_1_2014'= Convert(int,0)
,'revisit_1_2015'= Convert(int,0)
,'revisit_2_2012'= Convert(int,0)
,'revisit_2_2013'= Convert(int,0)
,'revisit_2_2014'= Convert(int,0)
,'revisit_2_2015'= Convert(int,0)
,'revisit_3_2012'= Convert(int,0)
,'revisit_3_2013'= Convert(int,0)
,'revisit_3_2014'= Convert(int,0)
,'revisit_3_2015'= Convert(int,0)
,'revisit_More3_2012'= Convert(int,0)
,'revisit_More3_2013'= Convert(int,0)
,'revisit_More3_2014'= Convert(int,0)
,'revisit_More3_2015'= Convert(int,0)
,'Book_FirstWalk_2012'= Convert(int,0)
,'Book_FirstWalk_2013'= Convert(int,0)
,'Book_FirstWalk_2014'= Convert(int,0)
,'Book_FirstWalk_2015'= Convert(int,0)
,'Book_Revisit_1_2012'= Convert(int,0)
,'Book_Revisit_1_2013'= Convert(int,0)
,'Book_Revisit_1_2014'= Convert(int,0)
,'Book_Revisit_1_2015'= Convert(int,0)
,'Book_Revisit_2_2012'= Convert(int,0)
,'Book_Revisit_2_2013'= Convert(int,0)
,'Book_Revisit_2_2014'= Convert(int,0)
,'Book_Revisit_2_2015'= Convert(int,0)
,'Book_Revisit_3_2012'= Convert(int,0)
,'Book_Revisit_3_2013'= Convert(int,0)
,'Book_Revisit_3_2014'= Convert(int,0)
,'Book_Revisit_3_2015'= Convert(int,0)
,'Book_Revisit_More3_2012'= Convert(int,0)
,'Book_Revisit_More3_2013'= Convert(int,0)
,'Book_Revisit_More3_2014'= Convert(int,0)
,'Book_Revisit_More3_2015'= Convert(int,0)
,'SumCallcenter_2012' = Convert(int,0)
,'SumCallcenter_2013' = Convert(int,0)
,'SumCallcenter_2014' = Convert(int,0)
,'SumCallcenter_2015' = Convert(int,0)
,'SumRegister_2012' = Convert(int,0)
,'SumRegister_2013' = Convert(int,0)
,'SumRegister_2014' = Convert(int,0)
,'SumRegister_2015' = Convert(int,0)
,'SumAppointment_2012' = Convert(int,0)
,'SumAppointment_2013' = Convert(int,0)
,'SumAppointment_2014' = Convert(int,0)
,'SumAppointment_2015' = Convert(int,0)
,'Year1'=Convert(int,0)
,'Year2'=Convert(int,0)
,'Year3'=Convert(int,0)
,'Year4'=Convert(int,0)
,'CreateDate' =Convert(datetime,0)
into #tempPrevious
from #temp
where 1=1
and (isnull(@ProductID,'') = '' or @ProductID = ProductID)
Order by BG,ProductID,StartDate

Print('Update#tempPrevious'+Convert(nvarchar(50),getdate(),113))

update #tempPrevious
set GrossBook_Unit_2012 = (case when @Mode = 'W' then (select GrossBook_Unit_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Unit_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
, GrossBook_Unit_2013 = (case when @Mode = 'W' then (select GrossBook_Unit_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Unit_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
, GrossBook_Unit_2014 = (case when @Mode = 'W' then (select GrossBook_Unit_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Unit_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
, GrossBook_Unit_2015 = (case when @Mode = 'W' then (select GrossBook_Unit_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Unit_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,GrossBook_Value_2012 = (case when @Mode = 'W' then (select GrossBook_Value_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Value_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,GrossBook_Value_2013 = (case when @Mode = 'W' then (select GrossBook_Value_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Value_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,GrossBook_Value_2014 = (case when @Mode = 'W' then (select GrossBook_Value_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Value_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,GrossBook_Value_2015 = (case when @Mode = 'W' then (select GrossBook_Value_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select GrossBook_Value_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,CancelUnit_2012 = (case when @Mode = 'W' then (select CancelUnit_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select CancelUnit_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,CancelUnit_2013 = (case when @Mode = 'W' then (select CancelUnit_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select CancelUnit_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,CancelUnit_2014 = (case when @Mode = 'W' then (select CancelUnit_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select CancelUnit_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,CancelUnit_2015 = (case when @Mode = 'W' then (select CancelUnit_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select CancelUnit_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetUnit_2012 = (case when @Mode = 'W' then (select NetUnit_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetUnit_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetUnit_2013 = (case when @Mode = 'W' then (select NetUnit_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetUnit_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetUnit_2014 = (case when @Mode = 'W' then (select NetUnit_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetUnit_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetUnit_2015 = (case when @Mode = 'W' then (select NetUnit_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetUnit_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetValue_2012 = (case when @Mode = 'W' then (select NetValue_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetValue_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetValue_2013 = (case when @Mode = 'W' then (select NetValue_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetValue_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetValue_2014 = (case when @Mode = 'W' then (select NetValue_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetValue_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,NetValue_2015 = (case when @Mode = 'W' then (select NetValue_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select NetValue_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeUnit_2012 = (case when @Mode = 'W' then (select AgreeUnit_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeUnit_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeUnit_2013 = (case when @Mode = 'W' then (select AgreeUnit_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeUnit_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeUnit_2014 = (case when @Mode = 'W' then (select AgreeUnit_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeUnit_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeUnit_2015 = (case when @Mode = 'W' then (select AgreeUnit_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeUnit_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeValue_2012 = (case when @Mode = 'W' then (select AgreeValue_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeValue_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeValue_2013 = (case when @Mode = 'W' then (select AgreeValue_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeValue_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeValue_2014 = (case when @Mode = 'W' then (select AgreeValue_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeValue_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,AgreeValue_2015 = (case when @Mode = 'W' then (select AgreeValue_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select AgreeValue_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransUnit_2012 = (case when @Mode = 'W' then (select TransUnit_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransUnit_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransUnit_2013 = (case when @Mode = 'W' then (select TransUnit_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransUnit_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransUnit_2014 = (case when @Mode = 'W' then (select TransUnit_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransUnit_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransUnit_2015 = (case when @Mode = 'W' then (select TransUnit_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransUnit_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransValue_2012 = (case when @Mode = 'W' then (select TransValue_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransValue_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransValue_2013 = (case when @Mode = 'W' then (select TransValue_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransValue_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransValue_2014 = (case when @Mode = 'W' then (select TransValue_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransValue_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,TransValue_2015 = (case when @Mode = 'W' then (select TransValue_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select TransValue_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,firstWalk_2012 =  (case when @Mode = 'W' then (select firstWalk_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select firstWalk_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,firstWalk_2013 =  (case when @Mode = 'W' then (select firstWalk_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select firstWalk_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,firstWalk_2014 =  (case when @Mode = 'W' then (select firstWalk_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select firstWalk_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,firstWalk_2015 =  (case when @Mode = 'W' then (select firstWalk_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select firstWalk_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_1_2012 =   (case when @Mode = 'W' then (select revisit_1_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_1_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_1_2013 =   (case when @Mode = 'W' then (select revisit_1_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_1_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_1_2014 =  (case when @Mode = 'W' then (select revisit_1_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_1_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_1_2015 =   (case when @Mode = 'W' then (select revisit_1_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_1_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_2_2012 =   (case when @Mode = 'W' then (select revisit_2_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_2_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_2_2013 =   (case when @Mode = 'W' then (select revisit_2_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_2_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_2_2014 =   (case when @Mode = 'W' then (select revisit_2_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_2_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_2_2015 =   (case when @Mode = 'W' then (select revisit_2_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_2_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_3_2012 =   (case when @Mode = 'W' then (select revisit_3_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_3_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_3_2013 =   (case when @Mode = 'W' then (select revisit_3_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_3_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_3_2014 =   (case when @Mode = 'W' then (select revisit_3_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_3_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_3_2015 =   (case when @Mode = 'W' then (select revisit_3_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_3_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_More3_2012 =   (case when @Mode = 'W' then (select revisit_More3_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_More3_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_More3_2013 =   (case when @Mode = 'W' then (select revisit_More3_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_More3_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_More3_2014 =   (case when @Mode = 'W' then (select revisit_More3_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_More3_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,revisit_More3_2015 =   (case when @Mode = 'W' then (select revisit_More3_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select revisit_More3_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_FirstWalk_2012 =   (case when @Mode = 'W' then (select Book_FirstWalk_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_FirstWalk_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_FirstWalk_2013 =   (case when @Mode = 'W' then (select Book_FirstWalk_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_FirstWalk_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_FirstWalk_2014 =   (case when @Mode = 'W' then (select Book_FirstWalk_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_FirstWalk_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_FirstWalk_2015 =   (case when @Mode = 'W' then (select Book_FirstWalk_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_FirstWalk_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_1_2012 =   (case when @Mode = 'W' then (select Book_Revisit_1_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_1_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_1_2013 =   (case when @Mode = 'W' then (select Book_Revisit_1_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_1_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_1_2014 =    (case when @Mode = 'W' then (select Book_Revisit_1_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_1_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_1_2015 =  (case when @Mode = 'W' then (select Book_Revisit_1_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_1_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_2_2012 =  (case when @Mode = 'W' then (select Book_Revisit_2_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_2_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_2_2013 =  (case when @Mode = 'W' then (select Book_Revisit_2_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_2_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_2_2014 =  (case when @Mode = 'W' then (select Book_Revisit_2_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_2_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_2_2015 =  (case when @Mode = 'W' then (select Book_Revisit_2_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_2_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_3_2012 =  (case when @Mode = 'W' then (select Book_Revisit_3_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_3_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_3_2013 =  (case when @Mode = 'W' then (select Book_Revisit_3_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_3_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_3_2014 =  (case when @Mode = 'W' then (select Book_Revisit_3_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_3_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_3_2015 =  (case when @Mode = 'W' then (select Book_Revisit_3_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_3_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_More3_2012 =  (case when @Mode = 'W' then (select Book_Revisit_More3_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_More3_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_More3_2013  = (case when @Mode = 'W' then (select Book_Revisit_More3_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_More3_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_More3_2014  = (case when @Mode = 'W' then (select Book_Revisit_More3_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_More3_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Book_Revisit_More3_2015 =  (case when @Mode = 'W' then (select Book_Revisit_More3_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Book_Revisit_More3_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumCallcenter_2012 =  (case when @Mode = 'W' then (select SumCallcenter_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumCallcenter_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumCallcenter_2013 =  (case when @Mode = 'W' then (select SumCallcenter_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumCallcenter_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumCallcenter_2014 =  (case when @Mode = 'W' then (select SumCallcenter_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumCallcenter_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumCallcenter_2015 =  (case when @Mode = 'W' then (select SumCallcenter_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumCallcenter_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumRegister_2012 =  (case when @Mode = 'W' then (select SumRegister_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumRegister_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumRegister_2013 =  (case when @Mode = 'W' then (select SumRegister_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumRegister_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumRegister_2014 =  (case when @Mode = 'W' then (select SumRegister_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumRegister_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumRegister_2015 =  (case when @Mode = 'W' then (select SumRegister_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumRegister_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumAppointment_2012 =  (case when @Mode = 'W' then (select SumAppointment_2012 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumAppointment_2012 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumAppointment_2013 =  (case when @Mode = 'W' then (select SumAppointment_2013 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumAppointment_2013 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumAppointment_2014 =  (case when @Mode = 'W' then (select SumAppointment_2014 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumAppointment_2014 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,SumAppointment_2015 =  (case when @Mode = 'W' then (select SumAppointment_2015 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select SumAppointment_2015 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Year1 = (case when @Mode = 'W' then (select Year1 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Year1 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Year2 = (case when @Mode = 'W' then (select Year2 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Year2 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Year3 = (case when @Mode = 'W' then (select Year3 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Year3 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,Year4 = (case when @Mode = 'W' then (select Year4 from #ZTEMP_RPT_SaleReport_Week a where t.ProductID = a.ProductID and t.y = a.y and t.w = a.w)
						else (select Year4 from #ZTEMP_RPT_SaleReport_Day a where t.ProductID = a.ProductID and t.y = a.y and t.DateValue = a.DateValue) end )
,CreateDate = getdate()
from #tempPrevious t 

select * from #tempPrevious
order by DateValue
--select * from @tempBookRevisit
--select * from #tempWalk --where OpportunityNo in('a1aa5380-6d56-47b4-88e7-172551f7b910','2c563221-6fd9-48e8-b5f6-63aa575a2d32')
--select * from @tempRevisit


GO
