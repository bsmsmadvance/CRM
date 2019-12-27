SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE   [dbo].[SP_RPT_Transfer_WaitLetter]
	@DateStart datetime  = '2015-12-10',
	@ProductID nvarchar(50)='10161',
	@UserName nvarchar(150) = ''
	
	
AS
declare @Date varchar(50),@currentdate datetime--,@createDate datetime
set @currentdate = @DateStart--'2015-12-10';
set @Date =  DATENAME(weekday,@DateStart)

----------------------------รอตอบรับ---------------------------------------


select 'ProductID' = '' --a.ProductID
    , 'Project' = '' --P.Project
    , 'UnitNumber' = '' --a.UnitNumber 
    , 'Name' = '' --(ao.FirstName + ' ' + ao.LastName ) Name 
    , 'contractnumber' = '' --a.contractnumber
    , 'TransferDate' = '' --a.TransferDate          
 , 'ContractAmount' = '' --a.ContractAmount-isnull((select sum(amount) from ICON_Payment_PaymentDetails b where b.referentid=a.contractnumber and paymenttype='5'),0)
 ,'SentDate' = null
 ,'DueDate' = '' --n.DateInLetter
,'FormatLetter'= '' --case timeofNotification when 1 then 'ฉ1' when 2 then 'ฉ2' when 3 then 'ยกเลิก' when 9 then 'ฉ1ซ้ำ' when 10 then 'ฉ2ซ้ำ' end
			
from [SAL].[Agreement] A --This is temp table actual table start from below
/* ICON_EntForms_NotificationTranfer n
left join dbo.ICON_EntForms_Agreement a             on a.ContractNumber = n.ContractNumber                           
left join dbo.ICON_EntForms_AgreementOwner ao on a.ContractNumber = ao.ContractNumber                                                           
left join dbo.ICON_EntForms_Products p on a.ProductID = p.ProductID                             
where ao.Header = 1  --and isnull(IsDelete,0) = 0                                   
and p.PType in (1,2,4) and p.RTPExcusive = 1                               
and a.CancelDate is null         
and  a.Approvedate is not null 
and timeofNotification in (1,2,3,9,10)  
and Backword is null   and RespondDate is  null 
and a.contractnumber not in (select contractnumber from ICON_EntForms_Transfer )
and DATEDIFF(day,n.NotificationDate,@currentdate)>14
and a.TransferDate  < @currentdate 
and @Date = 'Monday'  
and  (isnull(@ProductID,'') = '' or isnull(@ProductID,'') = a.productid)

order by 1,3  */





GO
