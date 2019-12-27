SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- [dbo].[AP2SP_RP_TR_002_1] '''10060''','','20151001','20151201','1','','','''''','administrator account','','','','','','','1','','1','SH'

CREATE PROC [dbo].[AP2SP_RP_TR_002_1]
	@Projects	nvarchar(4000),
	@UnitNumber nvarchar(MAX),
	@DateStart datetime,
	@DateEnd datetime,
    @StatusAG   nvarchar(20),
	@LoanStatus nvarchar(2),
	@LoanStatus1 nvarchar(2),
	@BankOnly nvarchar(20),
	@UserName nvarchar(150),
	@DateStart2 datetime,
	@DateEnd2   datetime,
	@DateStart3 datetime,
	@DateEnd3   datetime,
	@DateStart4 datetime,
	@DateEnd4   datetime,
	@HomeType nvarchar(20),
	@StatusProject nvarchar(2),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)

AS
SET TRANSACTION ISOLATION LEVEL Read uncommitted;
DECLARE @DateEndInStore Datetime, @DateEndInStore2 Datetime, @DateEndInStore3 Datetime, @DateEndInStore4 Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)
SET @DateEndInStore3 = [dbo].[fn_GetMaxDate](@DateEnd3)
SET @DateEndInStore4 = [dbo].[fn_GetMaxDate](@DateEnd4)


DECLARE @A varchar(5)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

DECLARE @sql nvarchar(max)
SET		@sql= '
SELECT	''SumName'' = '''' --SumName
		,''Approve'' = '''' --SUM(Approve) AS Approve
		,''NoApp'' = '''' --SUM(NoApp) AS NoApp
		,''TransApp'' = '''' --SUM(TransApp) AS TransApp
		,''NoKey'' = '''' --SUM(NoKey) AS NoKey
		,''WaitApp'' = '''' --SUM(WaitApp) AS WaitApp
		,''BankNo'' = '''' --BankNO

FROM [SAL].[Booking]' --This is temp table actual table start from below
/* (
SELECT	PR.Project,BK.UnitNumber
		,ISNULL(CASE WHEN  cb.DocStatus <> 1 THEN RTrim(LTrim(BA.AdBankName)) ELSE CASE	WHEN cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1) Then ''โอนสด''
																						WHEN cb.DocStatus = 2 Then ''กู้เองไม่ระบุธนาคาร''
																						WHEN cb.DocStatus = 3 Then ''กู้ผ่านโครงการไม่ระบุธนาคาร'' 
																						WHEN cb.DocStatus = 4 Then ''ยังไม่ตัดสินใจ'' 																						
																						WHEN cb.DocStatus = 5 THEN ''ลูกค้ารอยกเลิก'' 
																						WHEN cb.DocStatus = 6 THEN ''ยังไม่ถึงดิวโอน''
																						WHEN cb.DocStatus = 7 THEN ''ต่อขาย''
																						ELSE ''ว่าง(ยังไม่คีย์)'' END END
				,CASE	WHEN cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1) Then ''โอนสด''
						WHEN cb.DocStatus = 2 Then ''กู้เองไม่ระบุธนาคาร''
						WHEN cb.DocStatus = 3 Then ''กู้ผ่านโครงการไม่ระบุธนาคาร'' 
						WHEN cb.DocStatus = 4 Then ''ยังไม่ตัดสินใจ'' 																						
						WHEN cb.DocStatus = 5 THEN ''ลูกค้ารอยกเลิก'' 
						WHEN cb.DocStatus = 6 THEN ''ยังไม่ถึงดิวโอน''
						WHEN cb.DocStatus = 7 THEN ''ต่อขาย'' 
						ELSE ''ว่าง(ยังไม่คีย์)'' END) AS SumName
		,CASE	WHEN cb.DocStatus IN (2,3) AND CB.Status = 1 THEN 1 ELSE 0 END AS Approve
		,CASE	WHEN cb.DocStatus IN (2,3) AND CB.Status = 2 THEN 1 ELSE 0 END AS NoApp
		,CASE	WHEN cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1) THEN 1 ELSE 0 END AS TransApp
		,CASE	WHEN cb.DocStatus IS NULL AND TF.TransferDateApprove IS NULL THEN 1 ELSE 0 END AS NoKey
		,CASE	WHEN cb.DocStatus IN (2,3) AND CB.Status = 3 THEN 1 ELSE 0 END AS WaitApp
		,CASE	WHEN cb.DocStatus <> 1 AND BA.AdBankName IS NOT NULL THEN 1
				WHEN cb.DocStatus <> 1 AND BA.AdBankName IS NULL THEN 2
				WHEN cb.DocStatus = 1 THEN 3 
				ELSE 4 END AS BankNO
		,cb.DocStatus

FROM  [ICON_EntForms_Booking] BK 
		LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON BK.BookingNumber = AG.BookingNumber
		LEFT OUTER JOIN Z_CreditBanking cb on ((AG.ContractNumber = CB.ContractNumber OR BK.BookingNumber = CB.ContractNumber) AND cb.DocStatus <> 1)
		LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.Header,0) = 1 AND ISNULL(BO.IsDelete,0) = 0 		
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = AG.ContractNumber
		LEFT OUTER JOIN [ICON_EntForms_Unit] UN ON UN.ProductID = BK.ProductID AND UN.UnitNumber = BK.UnitNumber
		LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = BK.ProductID
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = CB.BankID
		LEFT OUTER JOIN [ICON_EntForms_BankBranch] BB ON BB.BankID = BA.BankID AND BB.BranchID = CB.BranchID
WHERE	1=1  '

IF(ISNULL(@Projects,'')<>'' And (@Projects <> '''ทั้งหมด''') AND (ISNULL(@Projects,'') <> ''''''))SET @sql=@sql+' AND (PR.ProductID IN ('+@Projects+')) '

IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql=@sql+' And (CB.CheckDocDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql=@sql+' And (CB.CheckDocDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql=@sql+' and (BK.UnitNumber IN ('+@UnitNumber+'))' 
IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql=@sql+' and (BK.UnitNumber = '''+@UnitNumber+''')'

IF(@StatusAG = '1')set @sql=@sql+' and (BK.Canceldate IS NULL)'
IF(@StatusAG = '2')set @sql=@sql+' and (BK.Canceldate IS NOT NULL)'
IF(@StatusAG = '3')set @sql=@sql+' AND (TF.TransferDateApprove IS NULL)' -- ยังไม่โอน
IF(@StatusAG = '4')set @sql=@sql+' AND (AG.CancelDate IS NULL AND TF.TransferDateApprove IS NOT NULL)' -- โอนแล้ว

IF(ISNULL(@BankOnly,'''''')<>'''''')set @sql=@sql+'and (CB.BankID = '+@BankOnly+')'

IF(@LoanStatus = '1')set @sql=@sql+' and (cb.DocStatus IN (2,3) AND CB.Status = 3)'
IF(@LoanStatus = '2')set @sql=@sql+' and (cb.DocStatus IN (2,3) AND CB.Status = 1)'
IF(@LoanStatus = '3')set @sql=@sql+' and (cb.DocStatus IN (2,3) AND CB.Status = 2)'

IF(@LoanStatus1 = '1')set @sql=@sql+' and (cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1)) '
IF(@LoanStatus1 = '2')set @sql=@sql+' and (cb.DocStatus = 2) '
IF(@LoanStatus1 = '3')set @sql=@sql+' and (cb.DocStatus = 3) '
IF(@LoanStatus1 = '4')SET @sql=@sql+' AND (cb.DocStatus = 4) '
IF(@LoanStatus1 = '5')set @sql=@sql+' AND (cb.DocStatus = 5) '
IF(@LoanStatus1 = '6')set @sql=@sql+' AND (cb.DocStatus = 6) '
IF(@LoanStatus1 = '7')set @sql=@sql+' AND (cb.DocStatus = 7) '
IF(@LoanStatus1 = '8')SET @sql=@sql+' AND (cb.DocStatus IS NULL AND TF.TransferDateApprove IS NULL) '

IF(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
	SET @sql=@sql+' AND (BK.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''') '
IF(YEAR(@DateStart2) = 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
	SET @sql=@sql+' AND (BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''') '

IF(YEAR(@DateStart3) <> 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
	SET @sql=@sql+' AND (TF.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart3,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''') '
IF(YEAR(@DateStart3) = 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
	SET @sql=@sql+' AND (TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''') '

IF(YEAR(@DateStart4) <> 1800) AND (YEAR(@DateEnd4) <> 7000) AND ISNULL(@DateStart4,'')<>'' AND ISNULL(@DateEnd4,'')<>''
	SET @sql=@sql+' AND (ISNULL(TF.TransferDate,AG.TransferDate) Between '''+CONVERT(VARCHAR(50),@DateStart4,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore4,120)+''') '
IF(YEAR(@DateStart4) = 1800) AND (YEAR(@DateEnd4) <> 7000) AND ISNULL(@DateStart4,'')<>'' AND ISNULL(@DateEnd4,'')<>''
	SET @sql=@sql+' AND (ISNULL(TF.TransferDate,AG.TransferDate) <= '''+CONVERT(VARCHAR(50),@DateEndInStore4,120)+''') '

IF(ISNULL(@HomeType,'')<>'' and ISNULL(@HomeType,'')<>'ทั้งหมด') set @sql=@sql+' AND (PR.PType = ''' + @HomeType + ''') '
IF(ISNULL(@ProjectGroup,'')<>'') set @sql=@sql+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'
IF(ISNULL(@ProjectType2,'')<>'') set @sql=@sql+' AND (PR.ProjectType = ''' + @ProjectType2 + ''')'

IF(@StatusProject = '1')set @sql=@sql+' and(PR.RTPExcusive = 1)'
IF(@StatusProject = '2')set @sql=@sql+' and(PR.RTPExcusive = 2)'
IF(@StatusProject = '3')set @sql=@sql+' and(PR.RTPExcusive = 3)'
IF(@StatusProject = '4')set @sql=@sql+' and(PR.RTPExcusive IN (1,2))'

set @sql=@sql+') AS A '
set @sql=@sql+' GROUP BY SumName,BankNO '
set @sql=@sql+' ORDER BY BankNO ' */


exec(@sql)
Print(@sql)

GO
