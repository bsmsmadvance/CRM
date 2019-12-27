SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[SP_RPT_EstimatePrice] '40024','','20171231',''
--   exec "[SP_RPT_AR_007_SUM]";1 N'', N'60016', {ts '1800-01-01 00:00:00'}, {ts '2017-11-30 00:00:00'}, N'อริยดา ตึกดี', N'''A10A131'''

CREATE PROC [dbo].[SP_RPT_EstimatePrice]
--DECLARE
	
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@UserName nvarchar(150)

AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @DateEndInStore Datetime
SET @DateStart = ISNULL(@DateStart, '20000101') ;
SET @DateEnd = ISNULL(@DateEnd, '70001231') ;
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd) ;
SET @ProductID = ISNULL(@ProductID, '') ;


select 'productid' = '' --a.productid
    , 'project' = '' --p.project
    , 'unitnumber' = '' --a.unitnumber
    , 'addressnumber' = '' --u.addressnumber
    , 'transferdateapprove' = '' --t.transferdateapprove
    , 'firstname' = '' --tow.firstname
    , 'lastname' = '' --tow.lastname
    , 'netsaleprice' = '' --t.netsaleprice
    , 'allestimateprice' = '' --z.allestimateprice

from [SAL].[Agreement] a WITH(NOLOCK) --This is main table need to use table below as well
      /* left join dbo.ICON_EntForms_Transfer t WITH(NOLOCK) on a.contractnumber=t.contractnumber   
      left join dbo.ICON_EntForms_TransferOwner tow WITH(NOLOCK) on t.transfernumber=tow.transfernumber and isnull(tow.isdelete,0)=0 and tow.id=1
      left join Z_logTransferFeeResult z WITH(NOLOCK) on a.contractnumber=z.contractid
      left join ICON_EntForms_Products p WITH(NOLOCK) on p.productid=a.productid
      left join ICON_EntForms_unit u WITH(NOLOCK) on a.productid=u.productid and a.unitnumber=u.unitnumber
     where (dbo.fn_ClearTime(t.TransferDateApprove) BETWEEN @DateStart and @DateEndInStore)
	   AND (ISNULL(@ProductID,'')='' or a.ProductID=@ProductID)
order by  a.productid,a.unitnumber */


GO
