SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_010] NULL,'10162','','A06A03','2016-12-21','2016-12-21','0','Administrator Account',NULL
--[AP2SP_RP_LC_010] NULL,'60006','','a28b10','','','1','Administrator Account',NULL
--[AP2SP_RP_LC_010] NULL,'60014','','AB6','','','','Administrator Account',NULL

---NEW ------NEW ------NEW ------NEW ------NEW ---

ALTER PROC  [dbo].[AP2SP_RP_LC_010]
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
	@SBUID		nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,	
    @StatusAG   nvarchar(20),
    @UserName   NVARCHAR(150),
    @Customer   NVARCHAR(50)

AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
DECLARE @DateEndInStore Datetime,@A varchar(5)
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

Declare @sql nvarchar(max)
/* Set @sql = ' 
DECLARE @BookingNumber varchar(30)
DECLARE @ContractNumber nvarchar(4000);
'
DECLARE @ProjectID nvarchar(max)
SET @ProjectID = (SELECT ID FROM PRJ.Project WHERE ProjectNo = @ProducID)

DECLARE @UnitID nvarchar(max)
SET @UnitID = (SELECT ID FROM PRJ.Unit WHERE UnitNo = @UnitNumber)


Set @sql = @sql + ' 
DECLARE @v TABLE(nOrder int IDENTITY, ContractNumber nvarchar(300), BookingNumber NVARCHAR(300), PayableAmount money);

INSERT INTO @v
SELECT	AG.AgreementNo,B.BookingNo, SUM(PayableAmount) 
FROM [SAL].[Agreement] AG WITH (NOLOCK)  
        LEFT OUTER JOIN [SAL].[Booking] B WITH (NOLOCK) ON B.ID = AG.BookingID
		LEFT OUTER JOIN [SAL].[AgreementDownPeriod] AP WITH (NOLOCK)  ON AP.AgreementID = AG.ID
		LEFT OUTER JOIN [PRJ].[Project] PR WITH (NOLOCK)  ON PR.ProjectID = AG.ProjectID 
		LEFT OUTER JOIN [MST].[Company] CO WITH (NOLOCK)  ON PR.CompanyID = CO.CompanyID
WHERE AP.PaymentType NOT IN (''TR2'',''TR3'',''TR4'') '

IF	(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด'''))  
	OR (Year(@DateStart) <> 1800 AND Year(@DateEnd) <> 7000 AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	OR (Isnull(@Customer,'')<>'')
	Begin

		if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' AND (PR.CompanyID = '''+@CompanyID+''')'
		if(Isnull(@ProjectID,'')<>'')set @sql=@sql+' AND (PR.ProjectID = '''+@ProjectID+''')'
		if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A >= 1)) set @sql=@sql+' AND (AG.@UnitID IN ('+@UnitNumber+'))' 
		if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A <= 0)) set @sql=@sql+' AND (AG.@UnitID = '''+@UnitNumber+''')'
		if(Isnull(@Customer,'')<>'')set @sql=@sql+' AND (AG.AgreementNo = '''+@Customer+''')'
		if(@StatusAG = '1')set @sql=@sql+' AND (AG.Canceldate IS NULL)'
		if(@StatusAG = '2')set @sql=@sql+' AND (AG.Canceldate IS NOT NULL)'
		IF(YEAR(@DateStart) <> 1800) AND  (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND  ISNULL(@DateEnd,'')<>''
				SET @sql=@sql+' AND (AG.ContractDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND  '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
	End
Else
	Begin
		set @sql=@sql+' AND  0=1'
	End

SET @sql = @sql + ' GROUP BY AG.AgreementNo,B.BookingNo;'

Set @sql = @sql + ' 
INSERT INTO @v
SELECT  NULL,BK.BookingNumber,SUM(PD.Amount)+T.Amount
	FROM ICON_Payment_PaymentDetails PD WITH (NOLOCK)  
	LEFT OUTER JOIN [SAL].[Booking] BK WITH (NOLOCK)  ON PD.ReferentID =  BK.BookingNo
	LEFT OUTER JOIN [PRJ].[Project] PR WITH (NOLOCK)  ON PR.ProjectID = BK.ProjectID 
	LEFT OUTER JOIN [MST].[Company] CO WITH (NOLOCK)  ON PR.CompanyID = CO.ID
	LEFT OUTER JOIN (
		SELECT PD.ReferentID,SUM(PD.Amount) AS Amount
		FROM ICON_Payment_PaymentDetails PD WITH (NOLOCK)  
			LEFT OUTER JOIN ICON_Payment_TmpReceipt TR WITH (NOLOCK)  ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID 
		WHERE TR.TransferPaymentType = 3
			AND NOT EXISTS(SELECT * FROM dbo.ICON_EntForms_Agreement WITH (NOLOCK)  WHERE BookingNumber=PD.ReferentID)
		GROUP BY PD.ReferentID
	) T ON BK.BookingNumber=T.ReferentID
WHERE PD.PaymentTYpe=''4'' AND NOT EXISTS(SELECT * FROM @v WHERE BookingNumber=BK.BookingNumber) '

IF	(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด'''))  
	OR (Year(@DateStart) <> 1800 AND Year(@DateEnd) <> 7000 AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>'')
	OR (Isnull(@Customer,'')<>'')
	Begin

		if(Isnull(@CompanyID,'')<>'')set @sql=@sql+' AND (PR.CompanyID = '''+@CompanyID+''')'
		if(Isnull(@ProjectID,'')<>'')set @sql=@sql+' AND (PR.ProjectID = '''+@ProjectID+''')'
		if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A >= 1)) set @sql=@sql+' AND (BK.UnitNumber IN ('+@UnitNumber+'))' 
		if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A <= 0)) set @sql=@sql+' AND (BK.UnitNumber = '''+@UnitNumber+''')'
		if(Isnull(@SBUID,'')<>'')set @sql=@sql+' AND (PR.SBUID = '''+@SBUID+''')'
		if(@StatusAG = '1')set @sql=@sql+' AND (BK.Canceldate IS NULL)'
		if(@StatusAG = '2')set @sql=@sql+' AND (BK.Canceldate IS NOT NULL)'
		IF(YEAR(@DateStart) <> 1800) AND  (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND  ISNULL(@DateEnd,'')<>''
						   set @sql=@sql+' AND (BK.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND  '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
	End
Else
	Begin
		set @sql=@sql+' AND  0=1'
	End

SET @sql = @sql + ' GROUP BY BK.BookingNo,T.Amount;'

Set @sql = @sql + ' */

--This @sql is for temp mapping only actual mapping need to remove line 113 and use line 110
Set @sql = '


DECLARE @DebtorCard TABLE
(
	ReferentNumber varchar(30),ContractNumber varchar(30),Period varchar(100),DueDate Datetime,PayableAmount money
	,RowNo int,PaidBy varchar(5),Amount money,RDate Datetime,PrintingID varchar(30),ReceivedID varchar(30)
	,BankBranch varchar(200),Number varchar(40),DueCheque Datetime,Status varchar(2),ReconcileDate Datetime
	,ProductID varchar(30),UnitNumber varchar(30),CountNo varchar(100),ReceiveDate Datetime
	,PaymentType varchar(100),Method int,Type varchar(5),IsHouse_Payment varchar(5),ChequeAmount money
) '

/* DECLARE @MinOrder int;
SELECT @MinOrder = MIN(nOrder) FROM @v;

WHILE (@MinOrder IS NOT NULL)
  BEGIN
	SELECT @ContractNumber=ContractNumber,@BookingNumber=BookingNumber FROM @v WHERE nOrder = @MinOrder;

	INSERT INTO @DebtorCard
	EXEC dbo.AP2SP_GetArrangePayment_Card @ContractNumber,@BookingNumber,'''';

	SELECT @MinOrder = MIN(nOrder) FROM @v WHERE nOrder > @MinOrder;
  END  '

SET @sql=@sql+'
DECLARE @Down Table(DocumentID nvarchar(50),DocumentType int,FreeDownAmount money)
INSERT INTO @Down
SELECT DocumentID,DocumentType ,FreeDownAmount
FROM dbo.CRM_FreeDown WITH (NOLOCK)
where DocumentType = 2 AND  ISNULL(FreeDownAmount,0) > 0' */

Set @sql=@sql + '

SELECT	''CompanyName'' = '''' --dbo.fn_GetCompanyNameTH(PR.CompanyID, ISNULL(AG.ContractDate,BK.BookingDate))
		,''Project'' = '''' --ISNULL(PR.ProjectNo,'''')+''-''+ISNULL(PR.ProjectNameTH,'''')
		--,''SBUName'' = '''' --SB.SBUName
		--,''SBUID'' = '''' --PR.SBUID
		,DC.*
		,''SumAmount'' = '''' --ISNULL(AP1.PayableAmount,0)+Case When Isnull(ag.IsAPPay,0)=1 Then Isnull(ag.LastDownAmount,0) Else 0 End
		,''TotalPaid'' =  '''' --ISNULL(DS1.TotalPaid,0) 
		,''CashDiscount'' = '''' --ISNULL(AG.CashDiscount,ISNULL(BK.CashDiscount,0))
        ,''TransferDiscount'' = '''' --ISNULL(TF.PhusaDiscount,ISNULL(AG.TransferDiscount,ISNULL(BK.TransferDiscount, 0)))
		,''TransferPayment'' = '''' --ISNULL(AP1.PayableAmount,0)-ISNULL(DS1.TotalPaid,0)
        ,''ReturnPrice'' = '''' --ISNULL(AG.ReturnPrice,0)
		,''TotalNotIN'' = '''' --ISNULL(DSN.TotalNotIN,0)
		,''TotalPayment'' = '''' --ISNULL(DS1.TotalPaid,0)+ISNULL(DSN.TotalNotIN,0)
        ,''ExtraDiscount'' = '''' --ISNULL(TF.ExtraDiscount,0)
		,''IncreasingAreaPrice'' = '''' --ISNULL(AG.IncreasingAreaPrice,0)
        ,''IncreasingAreaPrice2'' = '''' --ISNULL(TF.IncreasingAreaPrice,0)
		,''IsAPPay'' = '''' --ISNULL(AG.IsAPPay,0)
		,''APPayPeriod'' = '''' --Case When ISNULL(AG.IsAPPay,0)>0 Then (Select Max(Period)+1 From [ICON_EntForms_AgreementPeriod]ap Where ap.ContractNumber=ag.ContractNumber) Else 0 End
		,''FreeDown'' = '''' --ISNULL(D.FreeDownAmount,0)

FROM	@DebtorCard DC' --This is main table need to use below table as well
        /* LEFT OUTER JOIN [SAL].[Booking] BK WITH (NOLOCK)  ON DC.ReferentNumber = BK.BookingNumber AND DC.IsHouse_Payment = 1
        LEFT OUTER JOIN [SAL].[Agreement] AG WITH (NOLOCK)  ON (BK.BookingNumber = AG.BookingNumber OR DC.ContractNumber = AG.ContractNumber AND DC.IsHouse_Payment = 1)
		LEFT OUTER JOIN @v AP1 ON AG.ContractNumber = AP1.ContractNumber OR AP1.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN	[SAL].[Transfer] TF WITH (NOLOCK)  ON TF.ContractNumber = AG.ContractNumber
		LEFT OUTER JOIN [PRJ].[Project] PR WITH (NOLOCK)  ON PR.ProductID = ISNULL(AG.ProductID,BK.ProductID)
        LEFT OUTER JOIN 
		(
			SELECT	SUM(Amount) AS TotalPaid,ContractNumber
			FROM	@DebtorCard  
			WHERE	PaymentType NOT IN (''TR2'',''TR3'',''TR4'',''15'',''2G'',''2H'',''00'',''01'',''02'',''37'',''17'')
            GROUP BY ContractNumber
		) AS DS1 ON DS1.ContractNumber = AG.ContractNumber OR DS1.ContractNumber = BK.BookingNumber
        LEFT OUTER JOIN
		(
			SELECT	SUM(Amount) AS TotalNotIN,ContractNumber
			FROM	@DebtorCard 
            WHERE   ReconcileDate IS NULL AND Method <> ''10'' and PaymentType In(''4'',''5'',''6'',''8'',''A06'')
			GROUP BY ContractNumber
		) AS DSN ON DSN.ContractNumber = AG.ContractNumber OR DS1.ContractNumber = BK.BookingNumber
		 LEFT OUTER JOIN @DOWN D on AG.ContractNumber = D.DocumentID and tf.transferdateapprove is not null

WHERE DC.PaymentType Not In(''15'',''2G'',''2H'',''00'',''01'',''02'',''37'',''17'')
ORDER BY ISNULL(AG.ProductID,BK.ProductID),ISNULL(AG.UnitNumber,BK.UnitNumber),DC.ContractNumber,DC.PaymentType,DC.DueDate,DC.RDate ASC;' */

--แก้ไขปัญหาเรื่องการ report ทั้งโครงการ
--begin
 exec(@sql)
--end

--print(@sql)


--select @sql



GO
