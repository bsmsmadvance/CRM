SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec AP2SP_RP_LC_022 '10042','','2','','19000101','70000101','19000101','70000101','19000101','70000101','Administrator Account'

CREATE Proc [dbo].[AP2SP_RP_LC_022]
  @ProductID  nvarchar(20)=''
, @UnitNumber nvarchar(2000)=''
, @WaterStatus nvarchar(20)=''
, @ElectricStatus nvarchar(20)=''
, @DateStart datetime ='19000101'   -- TransferDateStart
, @DateEnd   datetime ='70000101'  -- TransferDateEnd
, @DateStart2 datetime ='19000101'  -- WaterNumberSaveDateStart
, @DateEnd2   datetime ='70000101'  -- WaterNumberSaveDateEnd
, @DateStart3 datetime ='19000101'  -- ElectricNumberSaveDateStart
, @DateEnd3   datetime ='70000101'  -- ElectricNumberSaveDateEnd
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

if(Year(@DateStart)>1900 and Year(Isnull(@DateEnd,'70000101'))=7000) Set @DateEnd = @DateStart
if(Year(@DateStart2)>1900 and Year(Isnull(@DateEnd2,'70000101'))=7000) Set @DateEnd2 = @DateStart2
if(Year(@DateStart3)>1900 and Year(Isnull(@DateEnd3,'70000101'))=7000) Set @DateEnd3 = @DateStart3

if(@WaterStatus='1')Set @WaterStatus='ยังไม่ได้ติดตั้ง'
else if(@WaterStatus='2')Set @WaterStatus='ในนามบริษัท'
else if(@WaterStatus='3')Set @WaterStatus='โอนให้ลูกค้า'
else Set @WaterStatus=''

if(@ElectricStatus='1')Set @WaterStatus='ยังไม่ได้ติดตั้ง'
else if(@ElectricStatus='2')Set @WaterStatus='ในนามบริษัท'
else if(@ElectricStatus='3')Set @WaterStatus='โอนให้ลูกค้า'
else Set @ElectricStatus=''

Select 'CompanyNameThai' = '' --c.CompanyNameThai
    ,'ProductID' = '' --u.ProductID
    ,'Project' = '' --p.Project
    ,'UnitNumber' = '' --u.UnitNumber
    ,'AddressNumber' = '' --u.AddressNumber
	,'ProjectAddress' = '' /* Isnull((Select Top 1 RTrim(LTrim(
				 Case When Isnull(pa.AddressNO,'')<>'' and Isnull(pa.AddressNO,'')<>'-'Then pa.AddressNO+' ' Else ' 'End
				+Case When Isnull(pa.Moo,'')<>''and Isnull(pa.Moo,'')<>'-'Then 'ม.'+pa.Moo+' ' Else ' ' End
				+Case When Isnull(pa.Soi,'')<>''and Isnull(pa.Soi,'')<>'-'Then 'ซอย '+pa.Soi+' ' Else ' ' End
				+Case When Isnull(pa.Road,'')<>''and Isnull(pa.Road,'')<>'-'Then 'ถนน '+pa.Road+' ' Else ' ' End
				+Case When Isnull(pa.SubDistrict,'')<>''Then 'แขวง'+pa.SubDistrict+' ' Else ' ' End
				+Case When Isnull(pa.District,'')<>''Then 'เขต'+pa.District+' ' Else ' ' End
				+Case When Isnull(pa.Province,'')<>''Then 'จังหวัด '+pa.Province+' ' Else ' ' End
				+Case When Isnull(pa.PostCode,'')<>''Then pa.PostCode+'' Else '' End))
				From ICON_EntForms_ProductsAddress pa
				Where pa.ProductID=p.ProductID and pa.AddressFlag=3
				Order by [NO]),'') ProjectAddress */
	,'WaterMeterNumber' = '' --um.WaterMeterNumber
    ,'ElectricMeterNumber' = '' --um.ElectricMeterNumber
	,'WaterMeterTransferDate' = '' --um.WaterMeterTransferDate
    ,'ElectricMeterTransferDate' = '' --um.ElectricMeterTransferDate
	,'TransferDateApprove' = '' --t.TransferDateApprove
	,'BU' = '' --p.PType BU
	,'BUOwner' = '' --(Select Top 1  FirstName+' '+Lastname From vw_UserinProduct v Where v.ProductID=u.ProductID and v.UserID In(select UserID from userroles where roleid=36)order by 1)BUOwner
	,'WaterMeterNumberSaveDate' = '' --um.WaterMeterNumberSaveDate
	,'ElectricMeterNumberSaveDate' = '' --um.ElectricMeterNumberSaveDate
	,'WaterBranchCode' = '' --p.WaterBranchCode
    ,'WaterStationCode' = '' --p.WaterStationCode
	,'ElectricBranchCode' = '' --p.ElectricBranchCode
    ,'ElectricStationCode' = '' --p.ElectricStationCode
	,'CustomerName' = '' --LTrim(Isnull(ao.NamesTitle,'')+Isnull(ao.FirstName,'')+' '+Isnull(ao.LastName,'')) CustomerName
	,'WaterMeterStatus' = '' --Case When Isnull(WaterMeterNumber,'')=''Then 'ยังไม่ได้ติดตั้ง' When Isnull(WaterMeterStatus,0)=0 Then 'ในนามบริษัท' Else 'โอนให้ลูกค้า' End WaterMeterStatus
	,'ElectricMeterStatus' = '' --Case When Isnull(ElectricMeterNumber,'')=''Then 'ยังไม่ได้ติดตั้ง' When Isnull(ElectricMeterStatus,0)=0 Then 'ในนามบริษัท' Else 'โอนให้ลูกค้า' End ElectricMeterStatus

From [PRJ].[Unit] u --This is main table need to use below table as well 
	/* Left Join ICON_EntForms_UnitMeter um on u.ProductID=um.ProductID and u.UnitNumber=um.UnitNumber
	Left Join ICON_EntForms_Products p on u.ProductID=p.ProductID
	Left Join ICON_EntForms_Agreement a on a.ProductID=u.ProductID and a.UnitNumber=u.UnitNumber and a.CancelDate Is Null
	Left Join ICON_EntForms_AgreementOwner ao on a.ContractNumber=ao.ContractNumber and Isnull(ao.IsDelete,0)=0 and Isnull(ao.Header,0)=1
	Left Join ICON_EntForms_Transfer t on t.ContractNumber=a.Contractnumber
	Left Join ICON_EntForms_Company c on c.CompanyID=p.CompanyID
Where 1=1 
	and Isnull(um.WaterMeterNumber,'')<>''
	and (@ProductID='' or u.ProductID=@ProductID)
	and (isnull(@UnitNumber,'')='' or @UnitNumber='ทั้งหมด' or u.UnitNumber IN(Select * From dbo.fn_SplitString (@UnitNumber,',')))
	and (@WaterStatus='' or Case When Isnull(WaterMeterNumber,'')=''Then 'ยังไม่ได้ติดตั้ง' When Isnull(WaterMeterStatus,0)=0 Then 'ในนามบริษัท' Else 'โอนให้ลูกค้า' End =@WaterStatus)
	and (@ElectricStatus='' or Case When Isnull(ElectricMeterNumber,'')=''Then 'ยังไม่ได้ติดตั้ง' When Isnull(ElectricMeterStatus,0)=0 Then 'ในนามบริษัท' Else 'โอนให้ลูกค้า' End =@ElectricStatus)
	and (Year(@DateStart)=1800 or t.TransferDateApprove Between @DateStart and @DateEnd)
	and (Year(@DateStart2)=1800 or um.WaterMeterNumberSaveDate Between @DateStart2 and @DateEnd2)
	and (Year(@DateStart3)=1800 or um.ElectricMeterNumberSaveDate Between @DateStart3 and @DateEnd3)
Order by u.ProductID,p.Project,u.UnitNumber */



GO
