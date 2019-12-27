SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec AP2SP_RP_TR_019 '10143','',null,null,''

CREATE PROC [dbo].[AP2SP_RP_TR_019]
	@ProductID  nvarchar(20),
    @UnitNumber nvarchar(20),
	@DateStart datetime ,
	@DateEnd datetime,			
    @UserName nvarchar(150)
AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
If(@DateStart is null)Set @DateStart='19000101'
If(@DateEnd is null)Set @DateEnd='70001231'

Set @ProductID =Isnull(@ProductID,'')
Set @UnitNumber =Isnull(@UnitNumber,'')

select 'ProductID' = '' --b.ProductID
        ,'Project' = '' --d.Project
        ,'UnitNumber' = '' --b.UnitNumber
        ,'AddressNumber' = '' --c.AddressNumber
        ,'TransferDateApprove' = '' --TransferDateApprove
        ,'RemainingTotalAmount' = '' --p.RemainingTotalAmount
        ,'Customer' = '' --ta.FirstName+' '+ta.LastName

from [SAL].[Booking] B --This is temp table actual table start from below
/* [dbo].[ICON_EntForms_Transfer] a
left join [dbo].[ICON_EntForms_TransferPayment] p on a.TransferNumber = p.TransferNumber  and p.MarginTotal = 1 and p.RemainingTotal in (1,2)
left join [dbo].[ICON_EntForms_TransferOwner] ta on a.TransferNumber = ta.TransferNumber and  ta.id = 1 
left join [ICON_EntForms_Agreement] b on a.ContractNumber = b.ContractNumber and b.CancelDate is null
left join [dbo].[ICON_EntForms_Unit] c on b.ProductID = c.ProductID and  b.UnitNumber = c.UnitNumber
left join [dbo].[ICON_EntForms_Products] d on c.ProductID = d.ProductID
where (ISNULL(@ProductID,'')='' or b.ProductID=@ProductID) 
and (ISNULL(@UnitNumber,'')='' or b.UnitNumber=@UnitNumber) 
and ((YEAR(@DateStart) = 1900) AND (YEAR(@DateEnd) = 7000) or (dbo.fn_ClearTime(TransferDateApprove) BETWEEN Convert(nvarchar(10),@DateStart,120) and Convert(nvarchar(10),@DateEnd,120)))
and d.RTPExcusive = 1

order by 1,2,3,5 */


GO
