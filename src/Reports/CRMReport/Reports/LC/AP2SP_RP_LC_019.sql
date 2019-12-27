SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec [AP2SP_RP_LC_019] '','','0','0','20160101','20160331',NULL,NULL,NULL,NULL,''
--exec [AP2SP_RP_LC_019] '70012','','0','0','20160101','20160331',NULL,NULL,NULL,NULL,''

CREATE Proc [dbo].[AP2SP_RP_LC_019]
  @ProductID  nvarchar(20)=''
, @UnitNumber nvarchar(2000)=''
, @WaterStatus nvarchar(20)=''
, @ElectricStatus nvarchar(20)=''
, @DateStart datetime ='18000101'   -- TransferDateStart
, @DateEnd   datetime ='70001231'  -- TransferDateEnd
, @DateStart2 datetime ='18000101'  -- WaterTransferDateStart
, @DateEnd2   datetime ='70001231'  -- WaterTransferDateEnd
, @DateStart3 datetime ='18000101'  -- ElectricTransferDateStart
, @DateEnd3   datetime ='70001231'  -- ElectricTransferDateEnd
, @UserName nvarchar(150)=''
as

/* Update icon_entforms_unitmeter Set WaterMeterTransferDate_Save=ElectricMeterTransferDate_Save
where 1=1 
and ElectricMeterTransferDate_Save is not null and WaterMeterTransferDate_Save is null
and ElectricMeterTransferDate is not null and WaterMeterTransferDate is not null

Update icon_entforms_unitmeter Set ElectricMeterTransferDate_Save = WaterMeterTransferDate_Save
where 1=1 
and ElectricMeterTransferDate_Save is  null and WaterMeterTransferDate_Save is not null
and ElectricMeterTransferDate is not null and WaterMeterTransferDate is not null */

Set @UnitNumber=Replace(@UnitNumber,'''','')

if(@DateStart is null or @DateStart = '') Set @DateStart='18000101'
if(@DateEnd is null or @DateEnd = '') Set @DateEnd='70001231'
if(@DateStart2 is null or @DateStart2 = '') Set @DateStart2='18000101'
if(@DateEnd2 is null or @DateEnd2 = '') Set @DateEnd2='70001231'
if(@DateStart3 is null or @DateStart3 = '') Set @DateStart3='18000101'
if(@DateEnd3 is null or @DateEnd3 = '') Set @DateEnd3='70001231'

if(Year(@DateStart)>1800 and Year(Isnull(@DateEnd,''))=7000) Set @DateEnd = @DateStart
if(Year(@DateStart2)>1800 and Year(Isnull(@DateEnd2,''))=7000) Set @DateEnd2 = @DateStart2
if(Year(@DateStart3)>1800 and Year(Isnull(@DateEnd3,''))=7000) Set @DateEnd3 = @DateStart3


Select 'ProducID' = '' --u.ProductID
    ,'Project' = '' --p.Project
    ,'UnitNumber' = '' --u.UnitNumber
    ,'AddressNumber' = '' --u.AddressNumber
	,'WaterMeterNumber' = '' --um.WaterMeterNumber
    ,'ElectricMeterNumber' = '' --um.ElectricMeterNumber
    ,'DocumentCompleteDate' = '' --um.DocumentCompleteDate
	,'WaterMeterTransferDate' = '' --um.WaterMeterTransferDate
    ,'ElectricMeterTransferDate' = '' --um.ElectricMeterTransferDate
	,'TransferDateApprove' = '' --t.TransferDateApprove
	,'BU' = '' --p.PType BU
	,'WaterMeterTransferDay' = '' --Isnull(DateDiff(Day,t.TransferDateApprove,um.WaterMeterTransferDate),0)WaterMeterTransferDay
	,'ElectricMeterTransferDay' = '' --Isnull(DateDiff(Day,t.TransferDateApprove,um.ElectricMeterTransferDate),0)ElectricMeterTransferDay
	,'BUOwner' = '' --(Select Top 1  FirstName+' '+Lastname From vw_UserinProduct v Where v.ProductID=u.ProductID)BUOwner
	,'WaterMeterOwner' = '' /* Case When Isnull(WaterMeterNumber,'')=''Then 'ยังไม่ได้ติดตั้ง' 
			When Isnull(WaterMeterStatus,0)=0 Then c.CompanyNameThai 
			Else LTrim(Isnull(ao.NamesTitle,'')+Isnull(ao.FirstName,'')+' '+Isnull(ao.LastName,'')) End WaterMeterOwner */
	,'ElectricMeterOwner' = '' /* Case When Isnull(ElectricMeterNumber,'')=''Then 'ยังไม่ได้ติดตั้ง' 
			When Isnull(ElectricMeterStatus,0)=0 Then c.CompanyNameThai 
			Else LTrim(Isnull(ao.NamesTitle,'')+Isnull(ao.FirstName,'')+' '+Isnull(ao.LastName,'')) End ElectricMeterOwner */

From [PRJ].[Unit] u --This is main table. Need to use table below as well. 
	/* Left Join ICON_EntForms_UnitMeter um on u.ProductID=um.ProductID and u.UnitNumber=um.UnitNumber
	Left Join ICON_EntForms_Products p on u.ProductID=p.ProductID
	Left Join ICON_EntForms_Agreement a on a.ProductID=u.ProductID and a.UnitNumber=u.UnitNumber and a.CancelDate Is Null
	Left Join ICON_EntForms_AgreementOwner ao on a.ContractNumber=ao.ContractNumber and Isnull(ao.IsDelete,0)=0 and Isnull(ao.Header,0)=1
	Left Join ICON_EntForms_Transfer t on t.ContractNumber=a.Contractnumber
	Left Join ICON_EntForms_Company c on c.CompanyID=p.CompanyID
Where 1=1
	and (@ProductID='' or u.ProductID=@ProductID)
	and (isnull(@UnitNumber,'')='' or @UnitNumber='ทั้งหมด' or u.UnitNumber IN(Select * From dbo.fn_SplitString (@UnitNumber,','))or u.AddressNumber IN(Select * From dbo.fn_SplitString (@UnitNumber,',')))
	and (Isnull(@WaterStatus,'0')='0' or (Case When Isnull(WaterMeterNumber,'')=''Then '1' 
			When Isnull(WaterMeterStatus,'0')='0' Then '2' 
			Else '3' End=@WaterStatus))
	and (Isnull(@ElectricStatus,'0')='0' or (Case When Isnull(ElectricMeterNumber,'')=''Then '1' 
			When Isnull(ElectricMeterStatus,'0')='0' Then '2' 
			Else '3' End=@ElectricStatus))
	and (Year(@DateStart)=1800 or t.TransferDateApprove Between @DateStart and @DateEnd)
	and (Year(@DateStart2)=1800 or um.WaterMeterTransferDate Between @DateStart2 and @DateEnd2)
	and (Year(@DateStart3)=1800 or um.ElectricMeterTransferDate Between @DateStart3 and @DateEnd3)
Order by u.ProductID,p.Project,u.UnitNumber */


GO
