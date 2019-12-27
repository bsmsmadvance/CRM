SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_010_3] '60006AA01243',''
ALTER PROCEDURE [dbo].[AP2SP_RP_LC_010_3]
	@ContractNumber  nvarchar(50)='',
	@UserName   nvarchar(150)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

Declare @sql nvarchar(max)
Set @sql = ' 

DECLARE @BookingNumber varchar(30)
SET	@BookingNumber = '''' '

/* Set @sql = @sql + '
DECLARE @v TABLE(nOrder int IDENTITY, ContractNumber nvarchar(300), PayableAmount money);
DECLARE @ContractNumber nvarchar(4000);

INSERT INTO @v
SELECT	AG.AgreementNo , SUM(PayableAmount) 
FROM [SAL].[Agreement] AG WITH (NOLOCK)  
		LEFT OUTER JOIN [SAL].[AgreementDownPeriod] AP WITH (NOLOCK)  ON AP.AgreementID = AG.ID
		LEFT OUTER JOIN [PRJ].[Project] PR WITH (NOLOCK)  ON PR.ProductID = AG.ProductID 
		LEFT OUTER JOIN [MST].[Company] CO WITH (NOLOCK)  ON PR.CompanyID = CO.CompanyID
WHERE 1=1 
	AND AP.PaymentType NOT IN (''TR2'',''TR3'',''TR4'') '

SET @sql = @sql + ' AND AG.ContractNumber='''+@ContractNumber+''''

SET @sql = @sql + ' GROUP BY AG.ContractNumber;'

Set @sql = @sql + ' */

--This @sql is for temp mapping only actual mapping need to remove line 40 and use line 37
Set @sql = '

DECLARE @DebtorCard TABLE
(
	ReferentNumber varchar(30),ContractNumber varchar(30),Period varchar(50),DueDate Datetime,PayableAmount money
	,RowNo int,PaidBy varchar(5),Amount money,RDate Datetime,PrintingID varchar(30),ReceivedID varchar(30)
	,BankBranch varchar(200),Number varchar(40),DueCheque Datetime,Status varchar(2),ReconcileDate Datetime
	,ProductID varchar(30),UnitNumber varchar(30),CountNo varchar(100),ReceiveDate Datetime
	,PaymentType varchar(100),Method int,Type varchar(5),IsHouse_Payment varchar(5),ChequeAmount money
) '

/* DECLARE @MinOrder int;
SELECT @MinOrder = MIN(nOrder) FROM @v;

WHILE (@MinOrder IS NOT NULL)
  BEGIN
	SET @ContractNumber = (SELECT TOP 1 ContractNumber FROM @v WHERE nOrder = @MinOrder);
	PRINT @ContractNumber

	INSERT INTO @DebtorCard
	EXEC dbo.AP2SP_GetArrangePayment_Card @ContractNumber,'''',''''

	SELECT @MinOrder = MIN(nOrder) FROM @v WHERE nOrder > @MinOrder;
  END  ' */

Set @sql=@sql + '
SELECT	''CompanyName'' = '''' --CO.CompanyNameThai
		,''Project'' = '''' --ISNULL(PR.ProductID,'''')+''-''+ISNULL(PR.Project,'''')
		,''SBUName'' = '''' --SB.SBUName
		,''SBUID'' = '''' --PR.SBUID
		,DC.*
		,''SumAmount'' = '''' --ISNULL(AP1.PayableAmount,0)
		,''TotalPaid'' =  '''' --ISNULL(DS1.TotalPaid,0) 
		,''CashDiscount'' = '''' --ISNULL(AG.CashDiscount,0)
        ,''TransferDiscount'' = '''' --ISNULL(ISNULL(TF.PhusaDiscount,AG.TransferDiscount),0)
		,''TransferPayment'' = '''' --ISNULL(AP1.PayableAmount,0)-ISNULL(DS1.TotalPaid,0)
        ,''ReturnPrice'' = '''' --ISNULL(AG.ReturnPrice,0)
		,''TotalNotIN'' = '''' --ISNULL(DSN.TotalNotIN,0)
		,''TotalPayment'' = '''' --ISNULL(DS1.TotalPaid,0)+ISNULL(DSN.TotalNotIN,0)
        ,''ExtraDiscount'' = '''' --ISNULL(TF.ExtraDiscount,0)
		,''IncreasingAreaPrice'' = '''' --ISNULL(AG.IncreasingAreaPrice,0)
        ,''IncreasingAreaPrice2'' = '''' --ISNULL(TF.IncreasingAreaPrice,0)
		,''IsAPPay'' = '''' --ISNULL(AG.IsAPPay,0)
		,''APPayPeriod''= '''' --Case When ISNULL(AG.IsAPPay,0)>0 Then (Select Max(Period)+1 From [ICON_EntForms_AgreementPeriod]ap Where ap.ContractNumber=ag.ContractNumber) Else 0 End

FROM	@DebtorCard DC ' --This is main table need to use below table as well
		/*LEFT OUTER JOIN [SAL].Agreement AG WITH (NOLOCK)  ON DC.ContractNumber = AG.AgreementID 
		LEFT OUTER JOIN @v AP1 ON AG.ContractNumber = AP1.ContractNumber
		LEFT OUTER JOIN	[SAL].[Transfer] TF WITH (NOLOCK)  ON TF.AgreementID = AG.ID
		LEFT OUTER JOIN [PRJ].[Project] PR WITH (NOLOCK)  ON PR.ID = AG.ProjectID 
		LEFT OUTER JOIN [MST].[Company] CO WITH (NOLOCK)  ON PR.CompanyID = CO.CompanyID
        LEFT OUTER JOIN 
			(
				SELECT	SUM(Amount) AS TotalPaid,ContractNumber
				FROM	@DebtorCard  
				WHERE	PaymentType NOT IN (''TR2'',''TR3'',''TR4'')
                GROUP BY ContractNumber
			) AS DS1 ON DS1.ContractNumber = AG.ContractNumber
        LEFT OUTER JOIN(
				SELECT	SUM(Amount) AS TotalNotIN,ContractNumber
				FROM	@DebtorCard 
                WHERE   ReconcileDate IS NULL AND Method <> ''10''
				GROUP BY ContractNumber
			) AS DSN ON DSN.ContractNumber = AG.ContractNumber 
Where DC.PaymentType In(''15'',''2G'',''2H'',''00'',''01'',''02'',''37'',''17'')
 ORDER BY AG.ProductID,AG.UnitNumber,DC.ContractNumber,DC.DueDate ' */

--แก้ไขปัญหาเรื่องการ report ทั้งโครงการ

--begin
 exec(@sql)
--end

--print(@sql)

GO
