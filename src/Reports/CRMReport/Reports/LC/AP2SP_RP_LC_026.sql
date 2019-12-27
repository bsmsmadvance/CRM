SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[AP2SP_RP_LC_026]
	@ProductID nvarchar(50)='',
	@DateStart datetime='',-- วันที่ทำสัญญา
	@DateEnd   datetime='',-- วันที่ทำสัญญา
	@Username nvarchar(150)=''
AS
if(Isnull(@DateStart,'')='') Set @DateStart='18000101'
if(Isnull(@DateEnd,'')='') Set @DateEnd='70000101'

/* DECLARE @UPI Table(Name VARCHAR(20), Amount money)
INSERT INTO @UPI(Name, Amount)
	SELECT	'Name' = QUPI.Name 
            , 'Amount' = QUPI.Amount
	FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [SAL].[QuotationUnitPriceItem] QUPI WITH (NOLOCK) ON QUPI.QuotationUnitPriceID = QUP.ID
    WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1 */

DECLARE @ProjectID VARCHAR(50)
SET @ProjectID = (SELECT ID FROM PRJ.Project WHERE ProjectNo = @ProductID)

Select 'ProductID' = p.ProjectNo
,'Project' = p.ProjectNameTH
,'CompanyNameThai' = Isnull(Replace(c.NameTH,'บริษัท',''),'')
,'Tel' = '' --Isnull(p.Tel,'')Tel
,'UnitNumber' = a.UnitID
,'AddressNumber' = Isnull(unit.HouseNo,'')
,'ContractNumber' = a.AgreementNo
,'ContractDate' = a.ContractDate
,'ContactID' = ao.ContactNo
,'CustomerName' = Isnull((SELECT [dbo].[fn_GetNamesTitle] (ao.ContactTitleTHMasterCenterID) ),'')+Isnull(ao.FirstNameTH,'')+' '+Isnull(ao.LastNameTH,'')
,'CustomerTel' = '' --Isnull(tw.Mobile,'')CustomerTel
,'NetSalePrice' = '' --t.NetSalePrice
,'TransferNumber' = Isnull(t.TransferNo,'')
,'TransferDate' = t.ScheduleTransferDate
,'TransferDateApprove' = t.ActualTransferDate
,'DocumentAssign' = '' --Isnull(Convert(int,t.DocumentAssign),-1)DocumentAssign
,'CreateByName' = '' --Isnull(u.DisplayName,'')CreateByName
,'CreateDate' = '' --d.CreateDate
,'Remark' = '' --Isnull(d.Remark,'')Remark
,'FullRemark' = ''  --Case When Isnull(d.IsReject,0)=1 Then '<b style="color:red;">ยกเลิก</b> ' Else '' End + Isnull(u.DisplayName,'')+' '+Convert(nvarchar(50),d.CreateDate,103)+' '+Convert(nvarchar(50),d.CreateDate,108)+' <br/> : '+Isnull(d.Remark,'')
,'SendDocumentByName' = ''  --Case When Isnull(u1.DisplayName,'')<>'' Then Isnull(u1.DisplayName,'') Else Isnull(u2.DisplayName,'') End SendDocumentByName
,'SendDocumentDate' = ''  --Case When t.DocumentAssign Is Not null Then t.SendDocumentDate Else Null End SendDocumentDate
,'CustomerAssignByName' = ''  --Isnull(u2.DisplayName,'')CustomerAssignByName
,'CustomerAssignDate' = ''  --t.CustomerAssignDate
,'RowNO' = ''  --row_number()over(partition by a.ContractNumber order by a.ContractNumber,d.CreateDate) RowNO

From [SAL].[Agreement] a 
Left Join [SAL].[AgreementOwner] ao on a.ID=ao.AgreementID and Isnull(ao.IsDeleted,0)=0 and Isnull(ao.IsMainOwner,0)=1
Inner Join [SAL].[Transfer] t on t.AgreementID=a.ID
left join [SAL].[TransferOwner] tw on t.ID = tw.TransferID
Left Join [SAL].[TransferDocument] d on t.ID =d.TransferID
Left Join [PRJ].[Project] p on p.ID=a.ProjectID
Left Join [MST].[Company] c on p.CompanyID=c.ID
--Left Join [USR].[User] u on d.CreateBy = Convert(varchar(50),u.UserID)
--Left Join [USR].[User] u1 on t.SendDocumentBy = Convert(varchar(50),u1.UserID) and t.DocumentAssign Is Not null
--Left Join [USR].[User] u2 on t.CustomerAssignBy = Convert(varchar(50),u2.UserID)
Left Join [PRJ].[Unit] unit on unit.ID=a.UnitID and unit.ProjectID=a.ProjectID
Where 1=1 --a.CancelDate Is Null --1=1 just for testing need to remove this
and (Isnull(@ProductID,'')='' or a.ProjectID=@ProjectID)
and t.ActualTransferDate Between @DateStart and @DateEnd 
and t.ActualTransferDate is not null
Order by a.ProjectID,a.UnitID,d.Created


GO
