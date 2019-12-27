SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[dbo].[AP2SP_RP_TR_002] '''10060''','','20151001','20151201','1','','','''''','administrator account','','','','','','','1','','1','SH'
--[dbo].[AP2SP_RP_TR_002] '''''','','','','1','','','''''','administrator account','20160101','20161231','','','','','1','','1','SH'

ALTER PROC [dbo].[AP2SP_RP_TR_002]
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
SELECT	''ProductID'' = '''' --BK.ProductID
        ,''Project'' = '''' --PR.Project
		,''UnitNumber'' = '''' --ISNULL(AG.UnitNumber, BK.UnitNumber)
        ,''AddressNumber'' = '''' --UN.AddressNumber
		,''CustomeName'' = '''' --ISNULL([dbo].[fn_GenCustAgreementAllName](AG.ContractNumber), [dbo].[fn_GenCustBookingAllName] (BK.BookingNumber))
		,''NewCustomerName'' = '''' --H.NewCustomerName
		,''Telephone'' = '''' --ISNULL(C.Tel_4,ISNULL(ISNULL(Left(AO.Mobile,15),Left(BO.Mobile,15)),''''))
		,''SellingPrice'' = '''' --ISNULL(AG.SellingPrice,BK.SellingPrice)
		,''AgreementArea'' = '''' --ISNULL(BK.StANDardArea,0)
		,''TransferArea'' = '''' --ISNULL(UN.AreaFromPFB,UN.AreaFromRE)
		,''BookingDate'' = '''' --BK.BookingDate
        ,''ContractDate'' = '''' --ISNULL(AG.pContractDate,AG.ContractDate) AS ContractDate
		,''CheckDocCompleteDate'' = '''' --CB.CheckDocCompleteDate
		,''TransferRealDate'' = '''' --TF.TransferDateApprove
		,''CustomerStatus'' = NULL
		,''TransferDate'' = '''' --ISNULL(TF.TransferDate,AG.TransferDate)
		,''TransferStatus'' = '''' /* CASE	WHEN TF.TransferDateApprove is not null AND TF.Approve3 = 1 THEN ''โอนแล้ว''
									ELSE ''ยังไม่โอน'' END */
		,''LoneStatus'' = '''' /* CASE	WHEN cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove is not null AND TF.Approve3 = 1) Then ''โอนสด''
								WHEN cb.DocStatus = 2 Then ''กู้เอง''
								WHEN cb.DocStatus = 3 Then ''กู้ผ่านโครงการ'' 
								WHEN cb.DocStatus = 4 Then ''ยังไม่ตัดสินใจ'' 
								WHEN cb.DocStatus = 5 Then ''ลูกค้ารอยกเลิก'' 
								WHEN cb.DocStatus = 6 Then ''ยังไม่ถึงดิวโอน'' 
								WHEN cb.DocStatus = 7 Then ''ขายต่อ''  
								ELSE ''ว่าง(ยังไม่คีย์)'' END */
		,''BankName'' = '''' /* CASE WHEN cb.DocStatus <> 1 THEN ISNULL(BA.AdBankName,'''')+ISNULL(''/''+BB.BranchName,'''')
							 ELSE '''' END */
		,''CheckDocDate'' = '''' --CB.CheckDocDate
		,''LoaANDate'' = '''' --CB.LoANDate
		,''LoANDateAccepted '' = '''' --CB.LoANDateAccepted	
		,''LoanStatus'' = '''' /* CASE	WHEN cb.DocStatus IN (2,3) AND CB.Status = 1 THEN ''อนุมัติ''
				WHEN cb.DocStatus IN (2,3) AND CB.Status = 2 THEN ''ไม่อนุมัติ''
				WHEN cb.DocStatus IN (2,3) AND CB.Status = 3 THEN ''รอผล''
				WHEN cb.DocStatus IN (2,3) AND CB.Status = 4 THEN ''ยกเลิกคำขอ''
				ELSE '''' END AS LoanStatus */
		,''Loan'' = '''' --CB.Loan
        ,''LoanAccepted'' = '''' --CB.LoanAccepted
        ,''InsuranceAmount'' = '''' --CB.InsuranceAmount
        ,''LoanAcceptedAP'' = '''' --CB.LoanAcceptedAP
		,''IsSelect'' = '''' /* CASE	WHEN CB.IsSelected = 0 THEN ''ไม่เลือกใช้''
								WHEN CB.IsSelected = 1 THEN ''เลือกใช้''
								ELSE ''-'' END */
		,''Reason'' = '''' --CASE WHEN CB.Status = 1 THEN EC1.Description WHEN CB.Status = 2 THEN EC2.Description WHEN CB.Status = 3 THEN EC3.Description END AS Reason
		,''ReasonNoSelect'' = '''' --EC4.Description AS ReasonNoSelect
		,''Remark'' = '''' --CB.Remark
		,''Remark2'' = '''' --isnull(cb.[Note],'''')
		,''CreateByName'' = '''' --ISNULL(U3.FirstName,U.FirstName) AS CreateByName
		,''EditDate'' = '''' --ISNULL(cb.EditDate,cb.CreateDate) AS EditDate
		,''QuaterNo'' = '''' /* CAST(YEAR(ISNULL(TF.TransferDate,AG.TransferDate))+543 AS Char(4)) + ''/Q''+ 
			CAST(CASE WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 1 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 4 THEN 1 
			 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 3 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 7 THEN 2 
			 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 7 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 10 THEN 3
			 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 10 THEN 4 END AS Char(1)) */
		,''SalePrice2'' = '''' --ISNULL(ISNULL(AG.SellingPrice,BK.SellingPrice),0) - ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0)
		,''BookingStatus'' = '''' --CASE WHEN BK.Canceldate IS NULL THEN ''Active'' WHEN BK.Canceldate IS NOT NULL THEN ''ยกเลิก'' END
		,''SaleName'' = '''' --U2.FirstName 

FROM	[SAL].[Booking] BK' --This is main table. Need to use below table as well 
		/* LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = BK.ProductID
		LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN Z_CreditBanking cb on ((AG.ContractNumber = CB.ContractNumber OR BK.BookingNumber = CB.ContractNumber))
		LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.Header,0) = 1 AND ISNULL(BO.IsDelete,0) = 0 
		LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AO.ContractNumber = AG.ContractNumber AND ISNULL(AO.Header,0) = 1 AND ISNULL(AO.IsDelete,0) = 0 
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = AG.ContractNumber
		LEFT OUTER JOIN [ICON_EntForms_Unit] UN ON UN.ProductID = BK.ProductID AND UN.UnitNumber = BK.UnitNumber
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = CB.BankID
		LEFT OUTER JOIN [ICON_EntForms_BankBranch] BB ON BB.BankID = BA.BankID AND BB.BranchID = CB.BranchID
		LEFT OUTER JOIN [Users] U ON cb.CreateBy = U.UserID
		LEFT OUTER JOIN [Users] U3 ON cb.EditBy = U3.UserID
		LEFT OUTER JOIN [Users] U2 ON ISNULL(AG.SaleID,BK.SaleID) = U2.UserID
		LEFT OUTER JOIN [ICON_EntForms_Contacts] C ON C.ContactID = ISNULL(AO.ContactID,BO.ContactID)
		LEFT OUTER JOIN 
		(
			SELECT	Description,Ref
			FROM 	ICON_EntForms_Extcod
			WHERE	GType = ''ApprovedReason'' AND DeleteStatus IS NULL
		)AS EC1 ON EC1.Ref = CB.Reason
		LEFT OUTER JOIN 
		(
			SELECT	Description,Ref
			FROM 	ICON_EntForms_Extcod
			WHERE	GType = ''NotApproveReason'' AND DeleteStatus IS NULL
		)AS EC2 ON EC2.Ref = CB.Reason
		LEFT OUTER JOIN 
		(
			SELECT	Description,Ref
			FROM 	ICON_EntForms_Extcod
			WHERE	GType = ''PendingApproveReason'' AND DeleteStatus IS NULL
		)AS EC3 ON EC3.Ref = CB.Reason
		LEFT OUTER JOIN 
		(
			SELECT	Description,Ref
			FROM 	ICON_EntForms_Extcod
			WHERE	GType = ''ReasonNotSelect'' AND DeleteStatus IS NULL
		)AS EC4 ON EC4.Ref = CB.ReasonNotSelect
		LEFT OUTER JOIN 
		(
			SELECT A.ReferentID,
				[dbo].[fn_GenCustChangeTransferName](NewData,A.ReferentID) AS NewCustomerName
			FROM dbo.ICON_EntForms_UnitHistory A
				INNER JOIN 
				(
					SELECT ReferentID,MAX(HistoryID) AS HistoryID
					FROM dbo.ICON_EntForms_UnitHistory  
					WHERE OperateType = ''T''
						AND isApprove <> -1
						AND RefType=''2''
					GROUP BY ReferentID
				) B ON A.ReferentID=B.ReferentID AND A.HistoryID=B.HistoryID				
			WHERE OperateType = ''T''
				AND isApprove <> -1
				AND RefType=''2''
		)AS H ON AG.ContractNumber = H.ReferentID

WHERE	1=1 '

IF(ISNULL(@Projects,'')<>'' AND (@Projects <> '''ทั้งหมด''') AND (ISNULL(@Projects,'') <> ''''''))SET @sql=@sql+' AND (BK.ProductID IN ('+@Projects+')) '


IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
SET @sql=@sql+' AND (CB.CheckDocDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
SET @sql=@sql+' AND (CB.CheckDocDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

IF(Isnull(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A >= 1)) SET @sql=@sql+' AND (BK.UnitNumber IN ('+@UnitNumber+'))' 
IF(Isnull(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A <= 0)) SET @sql=@sql+' AND (BK.UnitNumber = '''+@UnitNumber+''')'

IF(@StatusAG = '1')SET @sql=@sql+' AND (BK.Canceldate IS NULL)' -- active
IF(@StatusAG = '2')SET @sql=@sql+' AND (BK.Canceldate IS NOT NULL)' -- ยกเลิก
IF(@StatusAG = '3')set @sql=@sql+' AND (BK.Canceldate IS NULL AND TF.TransferDateApprove IS NULL)' -- ยังไม่โอน
IF(@StatusAG = '4')set @sql=@sql+' AND (BK.CancelDate IS NULL AND TF.TransferDateApprove IS NOT NULL)' -- โอนแล้ว

IF(ISNULL(@BankOnly,'''''')<>'''''')SET @sql=@sql+' AND (CB.BankID = '+@BankOnly+')'

IF(@LoanStatus = '1')SET @sql=@sql+' AND (cb.DocStatus IN (2,3) AND CB.Status = 3)'
IF(@LoanStatus = '2')SET @sql=@sql+' AND (cb.DocStatus IN (2,3) AND CB.Status = 1)'
IF(@LoanStatus = '3')SET @sql=@sql+' AND (cb.DocStatus IN (2,3) AND CB.Status = 2)'

IF(@LoanStatus1 = '1')SET @sql=@sql+' AND (cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove IS NOT NULL AND TF.Approve3 = 1)) '
IF(@LoanStatus1 = '2')SET @sql=@sql+' AND (cb.DocStatus = 2) '
IF(@LoanStatus1 = '3')SET @sql=@sql+' AND (cb.DocStatus = 3) '
IF(@LoanStatus1 = '4')SET @sql=@sql+' AND (cb.DocStatus = 4) '
IF(@LoanStatus1 = '5')SET @sql=@sql+' AND (cb.DocStatus = 5) '
IF(@LoanStatus1 = '6')SET @sql=@sql+' AND (cb.DocStatus = 6) '
IF(@LoanStatus1 = '7')SET @sql=@sql+' AND (cb.DocStatus = 7) '
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

SET @sql=@sql+' ORDER BY BK.ProductID,BK.UnitNumber,CB.LoanDate,TF.TransferDateApprove ASC;' */

exec(@sql)
--Print(@sql) 
--SELECT @sql
--N3A28 






GO
