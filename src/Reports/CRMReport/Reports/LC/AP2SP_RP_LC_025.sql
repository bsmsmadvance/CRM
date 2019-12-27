SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_LC_025] '60012','18000101','70000101','18000101','70000101','1','Administrator Account',''
--[dbo].[AP2SP_RP_LC_025] '60012,10060','18000101','70000101','18000101','70000101','1','Administrator Account',''
--exec "AP2SP_RP_LC_025";1 N'''40009'',''40016''', {ts '1800-01-01 00:00:00'}, {ts '7000-12-31 00:00:00'}, {ts '1800-01-01 00:00:00'}, {ts '7000-12-31 00:00:00'}, N'1', N'อริยดา ตึกดี', N'0'

CREATE PROC [dbo].[AP2SP_RP_LC_025]
	@ProductID nvarchar(50)='',
	--@Projects	nvarchar(4000),
	@DateStart datetime='',-- วันที่ทำสัญญา
	@DateEnd   datetime='',-- วันที่ทำสัญญา
	@DateStart2 datetime='',--กำหนดทำสัญญา
	@DateEnd2   datetime='',
	@StatusAG nvarchar(100),
	@Username nvarchar(150),
	@StatusDoc nvarchar(150)
AS

Set @StatusAG = Isnull(@StatusAG,'')
if(Isnull(@DateStart,'')='') Set @DateStart='18000101'
if(Isnull(@DateEnd,'')='') Set @DateEnd='70000101'
if(Isnull(@DateStart2,'')='') Set @DateStart2='18000101'
if(Isnull(@DateEnd2,'')='') Set @DateEnd2='70000101'

select *
from(
		Select 'ProductID' = '' --b.ProductID
            ,'Project' = '' --p.Project
            ,'CompanyNameThai' = '' --Isnull(Replace(c.CompanyNameThai,'บริษัท',''),'') as CompanyNameThai
            ,'Tel' = '' --Isnull(p.Tel,'') as Tel
			,'UnitNumber' = '' --b.UnitNumber
			,'ContractNumber' = '' --Isnull(a.ContractNumber,b.bookingnumber) as ContractNumber
			,'BookingDate' = '' --b.BookingDate
			,'ContractDate' = '' --a.ContractDate
			,'pContractDate' = '' --a.pContractDate
			,'ContactID' = '' --Isnull(ao.ContactID,bo.ContactID) as ContactID
			,'CustomerName' = '' /* Case When a.ContractNumber is not null then Isnull(ao.NamesTitle,'')+Isnull(ao.FirstName,'')+' '+Isnull(ao.LastName,'')
				Else Isnull(bo.NamesTitle,'')+Isnull(bo.FirstName,'')+' '+Isnull(bo.LastName,'') End as CustomerName */
			,'SellingPrice' = '' --Isnull(a.SellingPrice,b.SellingPrice) as SellingPrice
			,'ContractPayment' = '' /* Case When Isnull(a.ContractAmount,0)>0 Then(
				Select Min(RDate) From ICON_Payment_TmpReceipt t
				Left Join ICON_Payment_PaymentDetails p on t.TmpReceiptID=p.TmpReceiptID
				Where t.ReferentID=a.ContractNumber)
				Else a.ApproveDate End as ContractPayment */
			,'ApproveDate' = '' --a.ApproveDate
			,'SignContractByName' = '' --Isnull(u2.FirstName,'') as SignContractByName
			,'SignContractDate' = '' --a.SignContractDate
			,'SignContractApproveByName' = '' --Isnull(u1.FirstName,'') as SignContractApproveByName
			,'SignContractApproveDate' = '' --a.SignContractApproveDate
			,'SignContractRejectByName' = '' --Isnull(u3.FirstName,'') as SignContractRejectByName
			,'SignContractRejectDate' = '' --a.SignContractRejectDate
			,'SignContractApproveByName2' = '' --Isnull(u4.FirstName,'') as SignContractApproveByName2
			,'SignContractApproveDate2' = '' --a.SignContractApproveDate2
			,'SignContractRejectByName2' = '' --Isnull(u5.FirstName,'') as SignContractRejectByName2
			,'SignContractRejectDate2' = '' --a.SignContractRejectDate2
			,'Remark' = '' --Convert(nvarchar(2000),Isnull(s.Remark,'')) as Remark
			,'CreateByName' = '' --Isnull(u.FirstName,'') as CreateByname
			,'CreateDate' = '' --s.CreateDate 
			,'TransferNumber' = '' --Isnull(t.TransferNumber,'') as TransferNumber
			,'TransferDate' = '' --a.TransferDate
			,'TransferDateApprove' = '' --t.TransferDateApprove
			,'Status' = '' --Case When a.CancelDate Is Null Then 'Active' Else 'ยกเลิก' End as [Status]
			,'RowNO' = '' --row_number()over(partition by b.BookingNumber order by a.ContractNumber) as RowNO			
			,'isValueCondo' = '' /* CAST(CASE WHEN ISNULL(p.isValueCondo,0)=1 AND dbo.fn_ClearTime(B.BookingDate) <= dbo.fn_ClearTime(P.Change2StepDate) THEN 1 
							ELSE 0 END AS BIT) */
			,'Is3step' = '' /* CAST(CASE WHEN ISNULL(p.isValueCondo,0)=1 AND P.Change2StepDate is not null THEN 1 
							ELSE 0 END AS BIT) */
			,'IsBooking' = '' --CASE WHEN row_number()over(partition by b.BookingNumber order by a.ContractNumber)=1 and b.CancelDate is null and a.ContractNumber is null then 1 else 0 end
			,'IsContract' = '' --CASE WHEN row_number()over(partition by b.BookingNumber order by a.ContractNumber)=1 and b.CancelDate is null and a.ContractNumber is not null then 1 else 0 end   
			,'isCondo' = '' --CAST(CASE WHEN ISNULL(p.Producttype,'')='โครงการแนวสูง' THEN 1 ELSE 0 END AS BIT)

		From [SAL].[Booking] b ) a --This is main table, need to use below table as well
			/* Left Join ICON_EntForms_BookingOwner bo  on b.BookingNumber=bo.BookingNumber and Isnull(bo.IsDelete,0)=0 and Isnull(bo.Header,0)=1
			Left Join ICON_EntForms_Agreement a on b.BookingNumber=a.BookingNumber
			Left Join ICON_EntForms_AgreementOwner ao on a.ContractNumber=ao.ContractNumber and Isnull(ao.IsDelete,0)=0 and Isnull(ao.Header,0)=1
			Left Join ICON_EntForms_AgreementSign s on a.ContractNumber=s.ContractNumber
			Left Join Users u on s.CreateBy = Convert(varchar(50),u.UserID)
			Left Join Users u1 on a.SignContractApproveBy = Convert(varchar(50),u1.UserID)
			Left Join Users u2 on a.SignContractBy = Convert(varchar(50),u2.UserID)
			Left Join Users u3 on a.SignContractRejectBy = Convert(varchar(50),u3.UserID)
			Left Join Users u4 on a.SignContractApproveBy2 = Convert(varchar(50),u4.UserID)
			Left Join Users u5 on a.SignContractRejectBy2 = Convert(varchar(50),u5.UserID)
			Left Join ICON_EntForms_Transfer t on t.ContractNumber=a.ContractNumber
			Left Join ICON_EntForms_Products p on p.ProductID=b.ProductID
			Left Join ICON_EntForms_Company c on p.CompanyID=c.CompanyID
		Where 1=1
			and ((@StatusAG='')or(@StatusAG='ทั้งหมด')
				or(@StatusAG='1' and b.CancelDate Is Null)
				or(@StatusAG='2' and b.CancelDate Is Not Null)
				or(@StatusAG='3' and t.TransferNumber Is Null)
				or(@StatusAG='4' and t.TransferNumber Is Not Null))
			AND (Isnull(@ProductID,'')='' or b.ProductID=@ProductID)
) a

Where 1=1
	and (Isnull(a.ContractPayment,'18000101') Between @DateStart and @DateEnd)
	and (Isnull(a.ContractDate,'18000101') Between @DateStart2 and @DateEnd2)
	
	and ((isnull(@StatusDoc,'')='')or(@StatusDoc='ทั้งหมด')or(@StatusDoc='0')			
		or(@StatusDoc='1' and a.ApproveDate is not null and a.isValueCondo = 1 and a.SignContractApproveDate Is Not Null)
		or(@StatusDoc='2' and a.ApproveDate is not null and a.isValueCondo = 1 and a.SignContractApproveDate Is Null)
		or(@StatusDoc='3' and a.ApproveDate is not null and a.isValueCondo = 1 and a.SignContractApproveDate2 Is Not Null)
		or(@StatusDoc='4' and a.ApproveDate is not null and a.isValueCondo = 1 and a.SignContractApproveDate2 Is Null)
		or(@StatusDoc='3' and a.ApproveDate is not null and a.isValueCondo = 0 and a.SignContractApproveDate Is Not Null)
		or(@StatusDoc='4' and a.ApproveDate is not null and a.isValueCondo = 0 and a.SignContractApproveDate Is Null))
		
Order by a.ProductID,a.UnitNumber,a.Createdate asc; */


GO
