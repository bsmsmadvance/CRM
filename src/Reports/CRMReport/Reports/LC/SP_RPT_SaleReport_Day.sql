SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROC [dbo].[SP_RPT_SaleReport_Day]
/* สำหรับออกรายงาน Sale Report แบบรายวัน */
	@HomeType as nvarchar(50)='',
	@ProductID  as nvarchar(50)='',
	@UserName  nvarchar(100) = '',
	@DateStart as DateTime =null,
	@DateEnd as DateTime =null
AS
exec [SP_RPT_SaleReport_NEW]  @HomeType,@ProductID,@DateStart,@DateEnd,@UserName,'D'



GO
