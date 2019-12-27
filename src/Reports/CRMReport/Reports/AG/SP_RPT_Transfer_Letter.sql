SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE   [dbo].[SP_RPT_Transfer_Letter]
	@DateStart datetime  = null,
	@ProductID nvarchar(50)='',
	@UserName nvarchar(150) = ''
	
	
AS
declare @Date varchar(50),@currentdate datetime--,@createDate datetime
set @currentdate =  @DateStart --getdate();
set @Date =  DATENAME(weekday,@DateStart)

-----------------------------------ฉ 1----------------------------------------------------
declare @temp table (productid nvarchar(100),project nvarchar(500),unitnumber nvarchar(100),name nvarchar(500),contractnumber nvarchar(50),TransferDate datetime,ContractAmount decimal(18,2),SentDate datetime, DueDate datetime,FormatLetter nvarchar(50))
insert into @temp

select 'ProductID' = '' --a.ProductID
        , 'Project' = '' --P.Project
        , 'UnitNumber' = '' --a.UnitNumber 
        , 'Name' = '' --(ao.FirstName + ' ' + ao.LastName ) Name 
        , 'contactnumber' = '' --a.contractnumber
        , 'TransferDate' = '' --a.TransferDate                           
 , 'ContractAmount' = '' --a.ContractAmount-isnull((select sum(amount) from ICON_Payment_PaymentDetails b where b.referentid=a.contractnumber and paymenttype='5'),0)                        
,'SentDate' = '' --@DateStart                                              
,'DueDate' = '' --case  when DATENAME(weekday,@currentdate +40) = 'Saturday'   then @currentdate +42       
               --    when DATENAME(weekday,@currentdate +40) = 'Sunday'  then  @currentdate +41
               --    else @currentdate +40 end    
,'ฉ1'                                    
from [SAL].[Agreement] A --This is temp table actual table start from below
/* dbo.ICON_EntForms_Agreement a                                   
left join dbo.ICON_EntForms_AgreementOwner ao on a.ContractNumber = ao.ContractNumber                            
left join dbo.ICON_EntForms_Products p on a.ProductID = p.ProductID                             
where ao.Header = 1  and isnull(IsDelete,0) = 0                                   
and p.PType in (1,2,4) and p.RTPExcusive = 1                               
and a.CancelDate is null         
and  a.Approvedate is not null 
and a.contractnumber not in (select contractnumber from ICON_EntForms_NotificationTranfer n  where timeofNotification in (1,3)  )     
and a.contractnumber not in (select contractnumber from ICON_EntForms_Transfer )
and a.TransferDate  < @currentdate 
and @Date = 'Monday'                          
order by 1,3 */

--------------------------------------ฉ1 ซ้ำ----------------------------------

declare @temp1  table (productid nvarchar(100),project nvarchar(500),unitnumber nvarchar(100),name nvarchar(50),contractnumber nvarchar(500),TransferDate datetime,ContractAmount decimal(18,2),SentDate datetime, DueDate datetime,FormatLetter nvarchar(50))
insert into @temp1
select 'ProductID' = '' --a.ProductID
        , 'Project' = '' --P.Project
        , 'UnitNumber' = '' --a.UnitNumber 
        , 'Name' = '' --(ao.FirstName + ' ' + ao.LastName ) Name
        , 'contractnumber' = '' --a.contractnumber
        , 'TransferDate' = '' --a.TransferDate 
 , 'ContractAmount' = '' --a.ContractAmount-isnull((select sum(amount) from ICON_Payment_PaymentDetails b where b.referentid=a.contractnumber and paymenttype='5'),0)
                          
,'SentDate' =  '' --@DateStart                          
                                
,'DueDate' = '' --case  when @CurrentDate<n.DateInLetter Then n.DateInLetter
				--	when DATENAME(weekday,@currentdate +40) = 'Saturday'   then @currentdate +42       
                  -- when DATENAME(weekday,@currentdate +40) = 'Sunday'  then  @currentdate +41
                  --   else @currentdate +40 end     
,'ฉ1ซ้ำ'  

from [SAL].[Agreement] A --This is temp table actual table start from below
/* dbo.ICON_EntForms_Agreement a                                   
left join dbo.ICON_EntForms_AgreementOwner ao on a.ContractNumber = ao.ContractNumber                                
left join dbo.ICON_EntForms_Products p on a.ProductID = p.ProductID    
inner join (select * from ICON_EntForms_NotificationTranfer n  where timeofNotification = 1 and ReasonGroup = 'จ่าหน้าไม่ชัดเจน'   )  n on n.ContractNumber=a.ContractNumber                       
where ao.Header = 1  and isnull(IsDelete,0) = 0                                   
and p.PType in (1,2,4) and p.RTPExcusive = 1                               
and a.CancelDate is null         
and  a.Approvedate is not null     
and a.contractnumber not in (select contractnumber from ICON_EntForms_Transfer )
and a.ContractNumber not in (select ContractNumber from @temp)
and a.ContractNumber not in (select ContractNumber from ICON_EntForms_NotificationTranfer n  where timeofNotification in (3,9))
and a.TransferDate  <= @currentdate 
and @Date = 'Monday'  
order by 1,3 */


-------------------------------------------ยกเลิก-----------------------------------------
declare @temp3  table (productid nvarchar(100),project nvarchar(500),unitnumber nvarchar(100),name nvarchar(500),contractnumber nvarchar(50),TransferDate datetime,ContractAmount decimal(18,2),SentDate datetime, DueDate datetime,FormatLetter nvarchar(50))
insert into @temp3

select Distinct 'ProductID' = '' --a.ProductID
    , 'Project' = '' --P.Project
    , 'UnitNumber' = '' --a.UnitNumber 
    , 'Name' = '' --(ao.FirstName + ' ' + ao.LastName ) Name 
    , 'contractnumber' = '' --a.contractnumber
    , 'TransferDate' = '' --a.TransferDate 
,'ContractAmount' = '' --a.ContractAmount-isnull((select sum(amount) from ICON_Payment_PaymentDetails b where b.referentid=a.contractnumber and paymenttype='5'),0)
,'SentDate' = '' --@DateStart
,'DueDate' = null
,'ยกเลิก'			
from [SAL].[Agreement] A --This is temp table actual table start from below
/* dbo.ICON_EntForms_Agreement a                                   
left join dbo.ICON_EntForms_AgreementOwner ao on a.ContractNumber = ao.ContractNumber                                
left join dbo.ICON_EntForms_NotificationTranfer n on a.ContractNumber = n.ContractNumber  and n.timeofNotification = 1         and n.RespondDate is not null  and Isnull(n.ReasonGroup,'')<>'จ่าหน้าไม่ชัดเจน'
left join dbo.ICON_EntForms_NotificationTranfer n2 on a.ContractNumber = n2.ContractNumber  and n2.timeofNotification = 2    and n2.RespondDate is not null and Isnull(n2.ReasonGroup,'')<>'จ่าหน้าไม่ชัดเจน'
left join dbo.ICON_EntForms_NotificationTranfer n11 on a.ContractNumber = n11.ContractNumber  and n11.timeofNotification = 9         and n11.RespondDate is not null and Isnull(n11.ReasonGroup,'')<>'จ่าหน้าไม่ชัดเจน'
left join dbo.ICON_EntForms_NotificationTranfer n21 on a.ContractNumber = n21.ContractNumber  and n21.timeofNotification = 10    and n21.RespondDate is not null  
left join dbo.ICON_EntForms_NotificationTranfer n22 on a.ContractNumber = n22.ContractNumber  and n22.timeofNotification = 2    and n22.RespondDate is not null 
left join dbo.ICON_EntForms_Products p on a.ProductID = p.ProductID                        
where ao.Header = 1  and isnull(IsDelete,0) = 0                                   
and p.PType in (1,2,4) and p.RTPExcusive = 1                               
and a.CancelDate is null         
and  a.Approvedate is not null 
and a.contractnumber not in (select contractnumber from ICON_EntForms_Transfer )
and a.ContractNumber not in (select ContractNumber from @temp)
and a.ContractNumber not in (select ContractNumber from @temp1)
and a.ContractNumber not in (select ContractNumber from ICON_EntForms_NotificationTranfer where timeofNotification =3  )
and (datediff(d,Isnull(n11.RespondDate,n.RespondDate),Isnull(n11.DateInLetter,n.DateInLetter))+isnull((datediff(d,n2.RespondDate,n2.DateInLetter)),0)+isnull((datediff(d,n21.RespondDate,n21.DateInLetter)),0)>=30 
		or (n22.DateInLetter is not null and n22.DateInLetter>@DateStart and n2.ContractNumber is null and n11.ContractNumber is null)
		or (n.ContractNumber is null and n2.ContractNumber is null and n11.ContractNumber is null)
		)
and a.TransferDate  <= @currentdate 
and @currentdate > Isnull(n22.DateInLetter,n.DateInLetter)
and @Date = 'Monday'  
order by 1,3 */

-----------------------------------------ฉ 2--------------------------------------
declare @temp2  table (productid nvarchar(100),project nvarchar(500),unitnumber nvarchar(100),name nvarchar(500),contractnumber nvarchar(50),TransferDate datetime,ContractAmount decimal(18,2),SentDate datetime, DueDate datetime,FormatLetter nvarchar(50))
insert into @temp2

Select Distinct * From(
select 'ProductID' = '' --a.ProductID
        , 'Project' = '' --P.Project
        , 'UnitNumber' = '' --a.UnitNumber 
        , 'Name' = '' --(ao.FirstName + ' ' + ao.LastName ) Name 
        , 'contractnumber' = '' --a.contractnumber
        , 'TransferDate' = '' --a.TransferDate                        
 , 'ContractAmount' = '' --a.ContractAmount-isnull((select sum(amount) from ICON_Payment_PaymentDetails b where b.referentid=a.contractnumber and paymenttype='5'),0)
,'SentDate' =  '' --@DateStart
,'DueDate' = '' --case 
				--when DATENAME(weekday,@currentdate+(datediff(day,  n.DateInLetter, dateadd(month, 1,  n.DateInLetter))  - datediff(day,(case when n.RespondDate is not null then n.RespondDate End) ,n.DateInLetter)+10)) = 'Saturday'
				--	then @currentdate+(datediff(day,  n.DateInLetter, dateadd(month, 1,  n.DateInLetter))  - datediff(day,(case when n.RespondDate is not null then n.RespondDate End) ,n.DateInLetter)+10)+2
				--when DATENAME(weekday,@currentdate+(datediff(day,  n.DateInLetter, dateadd(month, 1,  n.DateInLetter))  - datediff(day,(case when n.RespondDate is not null then n.RespondDate End) ,n.DateInLetter)+10)) = 'Sunday'
				--	then @currentdate+(datediff(day,  n.DateInLetter, dateadd(month, 1,  n.DateInLetter))  - datediff(day,(case when n.RespondDate is not null then n.RespondDate End) ,n.DateInLetter)+10)+1
				--else  @currentdate+(datediff(day,  n.DateInLetter, dateadd(month, 1,  n.DateInLetter))  - datediff(day,(case when n.RespondDate is not null then n.RespondDate End) ,n.DateInLetter)+10) 
				--end 
,'FormatLetter'='ฉ2'
from [SAL].[Agreement] A )t --This is temp table actual table start from below
/* dbo.ICON_EntForms_Agreement a                                   
left join dbo.ICON_EntForms_AgreementOwner ao on a.ContractNumber = ao.ContractNumber                                
left join dbo.ICON_EntForms_NotificationTranfer n on a.ContractNumber = n.ContractNumber                                 
left join dbo.ICON_EntForms_Products p on a.ProductID = p.ProductID                             
where ao.Header = 1  and isnull(IsDelete,0) = 0    and RespondDate is not null
and p.PType in (1,2,4) and p.RTPExcusive = 1                               
and a.CancelDate is null         
and  a.Approvedate is not null 
and timeofNotification in (1,9)     
and timeofNotification =Isnull((select max(timeofNotification) from ICON_EntForms_NotificationTranfer tt where tt.Contractnumber=a.ContractNumber and Isnull(ReasonGroup,'')<>'จ่าหน้าไม่ชัดเจน') ,'0')
and a.contractnumber not in (select contractnumber from ICON_EntForms_Transfer )
and a.ContractNumber not in (select ContractNumber from @temp1)
and a.ContractNumber not in (select ContractNumber from @temp3)
and a.ContractNumber not in (select ContractNumber from ICON_EntForms_NotificationTranfer where timeofNotification in(2,3)    )
and a.TransferDate  <= @currentdate 
and @currentdate > n.DateInLetter 
and @Date = 'Monday'  
)t Where DueDate is not null
order by 1,3 */

-----------------------------------------ฉ 2 => ฉ1,ฉ1ซ้ำ เป็นจ่าหน้าไม่ชัดเจน--------------------------------------
insert into @temp2
Select Distinct * From(
select 'ProductID' = '' --a.ProductID
    , 'Project' = '' --P.Project
    , 'UnitNumber' = '' --a.UnitNumber 
    , 'Name' = '' --(ao.FirstName + ' ' + ao.LastName ) Name 
    , 'contractnumber' = '' --a.contractnumber
    , 'TransferDate' = '' --a.TransferDate                        
 , 'ContractAmount' = '' --a.ContractAmount-isnull((select sum(amount) from ICON_Payment_PaymentDetails b where b.referentid=a.contractnumber and paymenttype='5'),0)
,'SentDate' = '' --@DateStart
,'DueDate' = '' --case  when DATENAME(weekday,@currentdate +40) = 'Saturday'   then @currentdate +42       
               --    when DATENAME(weekday,@currentdate +40) = 'Sunday'  then  @currentdate +41
               --    else @currentdate +40 end   
,'FormatLetter'='ฉ2'
from [SAL].[Agreement] A)t --This is temp table actual table start from below
/* dbo.ICON_EntForms_Agreement a                                   
left join dbo.ICON_EntForms_AgreementOwner ao on a.ContractNumber = ao.ContractNumber                                
left join dbo.ICON_EntForms_NotificationTranfer n on a.ContractNumber = n.ContractNumber                                 
left join dbo.ICON_EntForms_Products p on a.ProductID = p.ProductID                             
where ao.Header = 1  and isnull(IsDelete,0) = 0    and RespondDate is not null
and p.PType in (1,2,4) and p.RTPExcusive = 1                               
and a.CancelDate is null         
and  a.Approvedate is not null 
and timeofNotification in (1,9)     
and '0' =Isnull((select max(timeofNotification) from ICON_EntForms_NotificationTranfer tt where tt.Contractnumber=a.ContractNumber and Isnull(ReasonGroup,'')<>'จ่าหน้าไม่ชัดเจน') ,'0')
and a.contractnumber not in (select contractnumber from ICON_EntForms_Transfer )
and a.ContractNumber not in (select ContractNumber from @temp1)
and a.ContractNumber not in (select ContractNumber from @temp3)
and a.ContractNumber not in (select ContractNumber from ICON_EntForms_NotificationTranfer where timeofNotification in(2,3)    )
and a.TransferDate  <= @currentdate 
and @currentdate > n.DateInLetter 
and @Date = 'Monday'  
)t Where DueDate is not null
order by 1,3  */

------------------------------------ ฉ2 ซ้ำ
declare @temp2_2 table (productid nvarchar(100),project nvarchar(500),unitnumber nvarchar(100),name nvarchar(500),contractnumber nvarchar(50),TransferDate datetime,ContractAmount decimal(18,2),SentDate datetime, DueDate datetime,FormatLetter nvarchar(50))
insert into @temp2_2

select 'ProductID' = '' --a.ProductID
    , 'Project' = '' --P.Project
    , 'UnitNumber' = '' --a.UnitNumber 
    , 'Name' = '' --(ao.FirstName + ' ' + ao.LastName ) Name 
    , 'contractnumber' = '' --a.contractnumber
    , 'TransferDate' = '' --a.TransferDate 
 , 'ContractAmount' = '' --a.ContractAmount-isnull((select sum(amount) from ICON_Payment_PaymentDetails b where b.referentid=a.contractnumber and paymenttype='5'),0)
                          
,'SentDate' =  '' --@DateStart                          
                                
,'DueDate' = '' --case  when DATENAME(weekday,40-DateDiff(d,Isnull(n9.RespondDate,n1.RespondDate),isnull(n9.DateInLetter,n1.DateInLetter))) = 'Saturday'   then @CurrentDate+(40-DateDiff(d,Isnull(n9.RespondDate,n1.RespondDate),isnull(n9.DateInLetter,n1.DateInLetter)) +2       )
                --   when DATENAME(weekday,40-DateDiff(d,Isnull(n9.RespondDate,n1.RespondDate),isnull(n9.DateInLetter,n1.DateInLetter))) = 'Sunday'  then  @CurrentDate+(40-DateDiff(d,Isnull(n9.RespondDate,n1.RespondDate),isnull(n9.DateInLetter,n1.DateInLetter)) +1)
                --     else @CurrentDate+(40-DateDiff(d,Isnull(n9.RespondDate,n1.RespondDate),isnull(n9.DateInLetter,n1.DateInLetter))) end  
					 
,'ฉ2ซ้ำ'  

from [SAL].[Agreement] A --This is temp table actual table start from below
/* dbo.ICON_EntForms_Agreement a                                   
left join dbo.ICON_EntForms_AgreementOwner ao on a.ContractNumber = ao.ContractNumber                                
left join dbo.ICON_EntForms_Products p on a.ProductID = p.ProductID    
inner join (select * from ICON_EntForms_NotificationTranfer n  where timeofNotification = 2 and RespondDate Is Not null and n.DateInLetter<@currentdate  )  n on n.ContractNumber=a.ContractNumber 
left join dbo.ICON_EntForms_NotificationTranfer n1 on a.ContractNumber = n1.ContractNumber  and n1.timeofNotification = 1         and n.RespondDate is not null
left join dbo.ICON_EntForms_NotificationTranfer n9 on a.ContractNumber = n9.ContractNumber  and n9.timeofNotification = 9         and n9.RespondDate is not null                      
where ao.Header = 1  and isnull(IsDelete,0) = 0                                   
and p.PType in (1,2,4) and p.RTPExcusive = 1                               
and a.CancelDate is null         
and  a.Approvedate is not null    
and a.contractnumber not in (select contractnumber from ICON_EntForms_Transfer )
and a.ContractNumber not in (select ContractNumber from @temp)
and a.ContractNumber not in (select ContractNumber from @temp1)
and a.ContractNumber not in (select ContractNumber from @temp3)
and a.ContractNumber not in (select ContractNumber from @temp2)
and a.ContractNumber not in (select ContractNumber from ICON_EntForms_NotificationTranfer where timeofNotification in(10,3)    )
and a.TransferDate  <= @currentdate 
and @Date = 'Monday'  
order by 1,3  */

 
 /* select * from(
 select *
 from @temp
 Union All
  select *
 from @temp1
 Union All
  select *
 from @temp2
 Union All
  select *
 from @temp3
  Union All
  select *
 from @temp2_2)t
 where   (isnull(@ProductID,'') = '' or isnull(@ProductID,'') = productid)
 and 	 ((@DateStart is null and @DateStart is null) or CONVERT(datetime,SentDate,103) between CONVERT(datetime,@DateStart,103) and CONVERT(datetime,@DateStart,103))

 order by 1,4 */





GO
