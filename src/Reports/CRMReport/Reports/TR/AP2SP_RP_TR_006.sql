SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_TR_006] '''10060''','b01','','','1','','','''''','administrator account','','','','','','','1','','1','SH'

ALTER PROC [dbo].[AP2SP_RP_TR_006]
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

DECLARE @sql nvarchar(max),@sql1 nvarchar(max),@sql2 nvarchar(max),@sql3 nvarchar(max),@sql4 nvarchar(max),@sql5 nvarchar(max)
SET		@sql= '
--select * into #Z_CreditBanking from db_iconcrm_temp.dbo.Z_CreditBanking where ProductID IN ('+@Projects+')

SELECT	''ProductID'' = '''' --UN.ProductID
        ,''Project'' = '''' --PR.Project
        ,''UnitNumber'' = '''' --UN.UnitNumber
        ,''AddressNumber'' = '''' --UN.AddressNumber
		,''CustomerName'' '''' --BK.CustomerName
		,''NewCustomerName'' = '''' --H.NewCustomerName
		,''Telephone'' = '''' --BK.Telephone
        ,''SellingPrice'' = '''' --BK.SellingPrice
        ,''BKArea'' = '''' --BK.BKArea
		,''TFArea'' = '''' --ISNULL(UN.AreaFromPFB,UN.AreaFromRE) AS TFArea	
		,''BookingDate'' = '''' --BK.BookingDate
        ,''ContractDate'' = '''' --BK.ContractDate
        ,''CustStatus'' = '''' 
		,''CheckDocDate'' = '''' --CB.CheckDocDate
        ,''TransferDate'' = '''' --BK.TransferDate
        ,''TransferDateApprove'' = '''' --BK.TransferDateApprove
		,''TransferStatus'' = '''' --CASE	WHEN BK.TransferDateApprove is not null And BK.Approve3 = 1 THEN ''โอนแล้ว'' ELSE ''ยังไม่โอน'' END AS TransferStatus
		,''LoanStatus'' = '''' /* CASE	WHEN cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND BK.TransferDateApprove is not null And BK.Approve3 = 1) Then ''โอนสด''
				WHEN cb.DocStatus = 2 Then ''กู้เอง''
				WHEN cb.DocStatus = 3 Then ''กู้ผ่านโครงการ''
				WHEN cb.DocStatus = 4 Then ''ยังไม่ตัดสินใจ'' 
				WHEN cb.DocStatus = 5 Then ''ลูกค้ารอยกเลิก'' 
				WHEN cb.DocStatus = 6 Then ''ยังไม่ถึงดิวโอน''  
				WHEN cb.DocStatus = 7 Then ''ขายต่อ''  
				ELSE ''ว่าง(ยังไม่คีย์)'' END AS LoneStatus */
		,''BankName'' = '''' --CASE	WHEN cb.DocStatus <> 1 THEN ISNULL(BA.AdBankName,'''')+ISNULL(''/''+BB.BranchName,'''') ELSE '''' END AS BankName
		,''LoanDate'' = '''' --CB.LoanDate
		,''LoanDateAccepted'' = '''' --CB.LoanDateAccepted	
		,''BankStatus'' = '''' /* CASE	WHEN cb.DocStatus IN (2,3) AND CB.Status = 1 THEN ''อนุมัติ''
				WHEN cb.DocStatus IN (2,3) AND CB.Status = 2 THEN ''ไม่อนุมัติ''
				WHEN cb.DocStatus IN (2,3) AND CB.Status = 3 THEN ''รอผล''
				WHEN cb.DocStatus IN (2,3) AND CB.Status = 4 THEN ''ยกเลิกคำขอ''
				ELSE '''' END AS BankStatus */
		,''Loan'' = '''' --CB.Loan
        ,''LoanAccepted'' = '''' --CB.LoanAccepted
        ,''INsuranceAmount'' = '''' --CB.InsuranceAmount
        ,''LoanAcceptedAP'' = '''' --CB.LoanAcceptedAP
		,''IsSelect'' = '''' /* CASE	WHEN CB.IsSelected = 0 THEN ''ไม่เลือกใช้''
								WHEN CB.IsSelected = 1 THEN ''เลือกใช้''
								ELSE ''-'' END AS IsSelect */
		,''Reason'' = '''' --CASE WHEN CB.Status = 1 THEN EC1.Description WHEN CB.Status = 2 THEN EC2.Description WHEN CB.Status = 3 THEN EC3.Description END AS Reason
		,''ReasonNoSelect'' = '''' --EC4.Description AS ReasonNoSelect
		,''Remark'' = '''' --CB.Remark
		,''Remark2'' = '''' --cb.[Note]
		,''CreateByName'' = '''' --ISNULL(U2.FirstName,U.FirstName) AS CreateByName
		,''EditDate'' = '''' --ISNULL(cb.DocEditDate,cb.DocCreateDate) AS EditDate
		,''QuaterNo'' = '''' --QuaterNo
        ,''SalePrice2'' = '''' --SalePrice2

FROM	[PRJ].[Unit] UN' --This is main table. need to use below table as well
		/* LEFT OUTER JOIN 
		( '
SET		@sql1= ' 
			SELECT	BK.ProductID,BK.UnitNumber
					,Case When AG.ContractNumber is not null 
						then (STUFF((SELECT '','' + CASE WHEN ISNULL(NamesTitleExt,'''')=''''THEN  ISNULL(NamesTitle,'''')
															ELSE ISNULL(NamesTitleExt,'''')  END+ISNULL(FirstName,'''')+''  ''+ISNULL(LastName,'''') AS [text()]
										FROM ICON_EntForms_AgreementOwner SUB
										WHERE SUB.ContractNumber = ag.ContractNumber AND ISNULL(IsDelete,0) = 0 
										ORDER BY Header DESC,ContactID Asc
										FOR XML PATH('''') 
										), 1, 1, '''' ))
						else (STUFF((SELECT '','' + CASE WHEN ISNULL(NamesTitleExt,'''')=''''THEN  ISNULL(NamesTitle,'''')
															ELSE ISNULL(NamesTitleExt,'''')  END+ISNULL(FirstName,'''')+''  ''+ISNULL(LastName,'''') AS [text()]
										FROM ICON_EntForms_BookingOwner SUB
										WHERE SUB.BookingNumber = bk.BookingNumber AND ISNULL(IsDelete,0) = 0  AND ISNULL(IsBooking,0) = 1
										ORDER BY Header DESC,ContactID Asc
										FOR XML PATH('''') 
										), 1, 1, '''' )) end CustomerName
					,ISNULL(C.Tel_4,ISNULL(ISNULL(Left(AO.Mobile,15),Left(BO.Mobile,15)),'''')) AS Telephone,BK.SellingPrice,ISNULL(BK.StandardArea,0) AS BKArea
					,BK.BookingDate,ISNULL(AG.pContractDate,AG.ContractDate) AS ContractDate,ISNULL(TF.TransferDate,AG.TransferDate) AS TransferDate
					,TF.TransferDateApprove,TF.Approve3
					,(	Select	top 1 ID 
						From	#Z_CreditBanking cb
						Left Outer JOin [ICON_EntForms_Transfer] TF ON TF.ContractNumber = CB.ContractNumber
						Where	(CB.contractnumber = BK.BookingNumber or CB.contractnumber = AG.ContractNumber) '

						IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql1=@sql1+' And (CB.LoanDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
						IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql1=@sql1+' And (CB.LoanDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

						IF(ISNULL(@BankOnly,'''''')<>'''''')set @sql1=@sql1+'and(CB.BankID = '+@BankOnly+')'

						IF(@LoanStatus = '1')set @sql1=@sql1+' and(cb.DocStatus IN (2,3) AND CB.Status = 3)'
						IF(@LoanStatus = '2')set @sql1=@sql1+' and(cb.DocStatus IN (2,3) AND CB.Status = 1)'
						IF(@LoanStatus = '3')set @sql1=@sql1+' and(cb.DocStatus IN (2,3) AND CB.Status = 2)'

						IF(@LoanStatus1 = '1')set @sql1=@sql1+' and(cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1)) '
						IF(@LoanStatus1 = '2')set @sql1=@sql1+' and (cb.DocStatus = 2) '
						IF(@LoanStatus1 = '3')set @sql1=@sql1+' and (cb.DocStatus = 3) '
						IF(@LoanStatus1 = '4')set @sql1=@sql1+' and (cb.DocStatus = 4) '
						IF(@LoanStatus1 = '5')SET @sql1=@sql1+' AND (cb.DocStatus = 5) '
						IF(@LoanStatus1 = '6')SET @sql1=@sql1+' AND (cb.DocStatus = 6) '
						IF(@LoanStatus1 = '7')SET @sql1=@sql1+' AND (cb.DocStatus = 7) '
						IF(@LoanStatus1 = '8')set @sql1=@sql1+' and(cb.DocStatus IS NULL AND TF.TransferDateApprove is null) '

SET		@sql2=  ' 
						Order By CB.Isselected DESC,Case When CB.Status = 1 Then 7 When CB.Status = 3 Then 8 When CB.Status = 2 Then 9 When CB.Status = 4 Then 10 Else 0 End Asc,CB.LoanDate Asc,Case when cb.DocStatus = 1 Then 1 Else 0 End Desc,CB.ID Asc
					) AS ID
					,(	Select	top 1 cb.ContractNumber 
						From	#Z_CreditBanking cb
						Left Outer JOin [ICON_EntForms_Transfer] TF ON TF.ContractNumber = CB.ContractNumber
						Where	(CB.contractnumber = BK.BookingNumber or CB.contractnumber = AG.ContractNumber) '

						IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql2=@sql2+' And (CB.LoanDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
						IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql2=@sql2+' And (CB.LoanDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

						IF(ISNULL(@BankOnly,'''''')<>'''''')set @sql2=@sql2+'and(CB.BankID = '+@BankOnly+')'

						IF(@LoanStatus = '1')set @sql2=@sql2+' and(cb.DocStatus IN (2,3) AND CB.Status = 3)'
						IF(@LoanStatus = '2')set @sql2=@sql2+' and(cb.DocStatus IN (2,3) AND CB.Status = 1)'
						IF(@LoanStatus = '3')set @sql2=@sql2+' and(cb.DocStatus IN (2,3) AND CB.Status = 2)'

						IF(@LoanStatus1 = '1')set @sql2=@sql2+' and(cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1)) '
						IF(@LoanStatus1 = '2')set @sql2=@sql2+' and(cb.DocStatus = 2) '
						IF(@LoanStatus1 = '3')set @sql2=@sql2+' and(cb.DocStatus = 3) '
						IF(@LoanStatus1 = '4')set @sql2=@sql2+' and(cb.DocStatus = 4) '
						IF(@LoanStatus1 = '5')set @sql2=@sql2+' and(cb.DocStatus = 5) '
						IF(@LoanStatus1 = '6')set @sql2=@sql2+' and(cb.DocStatus = 6) '
						IF(@LoanStatus1 = '7')set @sql2=@sql2+' and(cb.DocStatus = 7) '
						IF(@LoanStatus1 = '8')set @sql2=@sql2+' and(cb.DocStatus IS NULL AND TF.TransferDateApprove is null) '

SET		@sql3=  ' 
						Order By CB.Isselected DESC,Case When CB.Status = 1 Then 7 When CB.Status = 3 Then 8 When CB.Status = 2 Then 9 When CB.Status = 4 Then 10 Else 0 End Asc,CB.LoanDate Asc,Case when cb.DocStatus = 1 Then 1 Else 0 End Desc,CB.ID Asc
					) AS ContractNumber1,BK.CancelDate,BK.BookingNumber,AG.ContractNumber
					,''QuaterNo'' = CAST(YEAR(ISNULL(TF.TransferDate,AG.TransferDate))+543 AS Char(4)) + ''/Q''+ 
						CAST(CASE WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 1 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 4 THEN 1 
						 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 3 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 7 THEN 2 
						 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 7 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 10 THEN 3
						 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 10 THEN 4 END AS Char(1))
					,''SalePrice2'' = ISNULL(ISNULL(AG.SellingPrice,BK.SellingPrice),0) - ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0)
		
			FROM	[ICON_EntForms_Booking] BK
					LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.Header,0) = 1 AND ISNULL(BO.IsDelete,0) = 0 
					LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON AG.BookingNumber = BK.BookingNumber
					LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AO.ContractNumber = AG.ContractNumber AND ISNULL(AO.Header,0) = 1 AND ISNULL(AO.IsDelete,0) = 0 
					LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = AG.ContractNumber 
					LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = BK.ProductID
					LEFT OUTER JOIN [ICON_EntForms_Contacts] C ON C.ContactID = ISNULL(AO.ContactID,BO.ContactID)
			WHERE	1=1 '			

			IF(ISNULL(@Projects,'')<>'' And (@Projects <> '''ทั้งหมด''') AND (ISNULL(@Projects,'') <> ''''''))SET @sql3=@sql3+' AND(BK.ProductID IN ('+@Projects+')) '
			IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql3=@sql3+' and(BK.UnitNumber IN ('+@UnitNumber+'))' 
			IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql3=@sql3+' and(BK.UnitNumber = '''+@UnitNumber+''')'

			IF(@StatusAG = '''ทั้งหมด''')set @sql3=@sql3+' AND (BK.Canceldate IS NULL)'
			IF(@StatusAG = '1')set @sql3=@sql3+' AND (BK.Canceldate IS NULL)'
			IF(@StatusAG = '2')set @sql3=@sql3+' AND (BK.Canceldate IS NOT NULL)'
			IF(@StatusAG = '3')set @sql3=@sql3+' AND (BK.Canceldate IS NULL AND TF.TransferDateApprove IS NULL)' -- ยังไม่โอน
			IF(@StatusAG = '4')set @sql3=@sql3+' AND (BK.Canceldate IS NULL AND TF.TransferDateApprove IS NOT NULL)' -- โอนแล้ว

			IF(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
				SET @sql3=@sql3+' AND (BK.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''') '
			IF(YEAR(@DateStart2) = 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
				SET @sql3=@sql3+' AND (BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''') '

			IF(YEAR(@DateStart3) <> 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
				SET @sql3=@sql3+' AND (TF.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart3,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''') '
			IF(YEAR(@DateStart3) = 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
				SET @sql3=@sql3+' AND (TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''') '

			IF(YEAR(@DateStart4) <> 1800) AND (YEAR(@DateEnd4) <> 7000) AND ISNULL(@DateStart4,'')<>'' AND ISNULL(@DateEnd4,'')<>''
				SET @sql3=@sql3+' AND (ISNULL(TF.TransferDate,AG.TransferDate) Between '''+CONVERT(VARCHAR(50),@DateStart4,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore4,120)+''') '
			IF(YEAR(@DateStart4) = 1800) AND (YEAR(@DateEnd4) <> 7000) AND ISNULL(@DateStart4,'')<>'' AND ISNULL(@DateEnd4,'')<>''
				SET @sql3=@sql3+' AND (ISNULL(TF.TransferDate,AG.TransferDate) <= '''+CONVERT(VARCHAR(50),@DateEndInStore4,120)+''') '
			
			IF(ISNULL(@HomeType,'')<>'') set @sql3=@sql3+' AND (PR.PType = ''' + @HomeType + ''') '
			IF(ISNULL(@ProjectGroup,'')<>'') set @sql3=@sql3+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'
			IF(ISNULL(@ProjectType2,'')<>'') set @sql3=@sql3+' AND (PR.ProjectType = ''' + @ProjectType2 + ''')'
			
			IF(@StatusProject = '1')set @sql3=@sql3+' and(PR.RTPExcusive = 1)'
			IF(@StatusProject = '2')set @sql3=@sql3+' and(PR.RTPExcusive = 2)'
			IF(@StatusProject = '3')set @sql3=@sql3+' and(PR.RTPExcusive = 3)'
			IF(@StatusProject = '4')set @sql3=@sql3+' and(PR.RTPExcusive IN (1,2))'

SET		@sql4=  '
		)AS BK ON UN.ProductID = BK.ProductID AND UN.UnitNumber = BK.UnitNumber
		Left JOin #Z_CreditBanking cb on CB.ContractNumber = BK.ContractNumber1 AND CB.ID = BK.ID
		LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = UN.ProductID
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = CB.BankID
		LEFT OUTER JOIN [ICON_EntForms_BankBranch] BB ON BB.BankID = BA.BankID AND BB.BranchID = CB.BranchID
		LEFT OUTER JOIN [Users] U ON cb.DocCreateBy = U.UserID
		LEFT OUTER JOIN [Users] U2 ON cb.DocEditBy = U2.UserID
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
			SELECT A.HistoryId,A.ReferentID,
				ISNULL([dbo].[fn_GenCustChangeTransferName](A.NewData,A.ReferentID),[dbo].[fn_GenCustChangeBookingName](A.NewData)) AS NewCustomerName
			FROM dbo.ICON_EntForms_UnitHistory A INNER JOIN 
				(
					SELECT ReferentID,MAX(HistoryId) AS HistoryId
					FROM dbo.ICON_EntForms_UnitHistory 
					WHERE OperateType = ''T''
						AND isApprove <> -1
					GROUP BY ReferentID
				) B ON A.ReferentID=B.ReferentID AND A.HistoryId=B.HistoryId
			WHERE A.OperateType = ''T''
				AND A.isApprove <> -1
		)AS H ON BK.ContractNumber1 = H.ReferentID 


WHERE	UN.Assettype IN (2,4) '

IF(ISNULL(@Projects,'')<>'' And (@Projects <> '''ทั้งหมด''') AND (ISNULL(@Projects,'') <> ''''''))SET @sql4=@sql4+' AND(UN.ProductID IN ('+@Projects+')) '

IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql4=@sql4+' And (CB.LoanDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql4=@sql4+' And (CB.LoanDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql4=@sql4+' and(UN.UnitNumber IN ('+@UnitNumber+'))' 
IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql4=@sql4+' and(UN.UnitNumber = '''+@UnitNumber+''')'

IF(@StatusAG = '''ทั้งหมด''')set @sql4=@sql4+' and(BK.Canceldate IS NULL)'
IF(@StatusAG = '1')set @sql4=@sql4+' AND (BK.Canceldate IS NULL)'
IF(@StatusAG = '2')set @sql4=@sql4+' AND (BK.Canceldate IS NOT NULL)'
IF(@StatusAG = '3')set @sql4=@sql4+' AND (BK.Canceldate IS NULL AND BK.TransferDateApprove IS NULL)' -- ยังไม่โอน
IF(@StatusAG = '4')set @sql4=@sql4+' AND (BK.CancelDate IS NULL AND BK.TransferDateApprove IS NOT NULL)' -- โอนแล้ว

IF(ISNULL(@BankOnly,'''''')<>'''''')set @sql4=@sql4+'and(CB.BankID = '+@BankOnly+')'

IF(@LoanStatus = '1')set @sql4=@sql4+' and(cb.DocStatus IN (2,3) AND CB.Status = 3)'
IF(@LoanStatus = '2')set @sql4=@sql4+' and(cb.DocStatus IN (2,3) AND CB.Status = 1)'
IF(@LoanStatus = '3')set @sql4=@sql4+' and(cb.DocStatus IN (2,3) AND CB.Status = 2)'

IF(@LoanStatus1 = '1')set @sql4=@sql4+' and(cb.DocStatus = 1 OR (cb.DocStatus IS NULL AND BK.TransferDateApprove is not null And BK.Approve3 = 1)) '
IF(@LoanStatus1 = '2')set @sql4=@sql4+' and(cb.DocStatus = 2) '
IF(@LoanStatus1 = '3')set @sql4=@sql4+' and(cb.DocStatus = 3) '
IF(@LoanStatus1 = '4')set @sql4=@sql4+' and(cb.DocStatus = 4) '
IF(@LoanStatus1 = '5')set @sql4=@sql4+' and(cb.DocStatus = 5) '
IF(@LoanStatus1 = '6')set @sql4=@sql4+' and(cb.DocStatus = 6) '
IF(@LoanStatus1 = '7')set @sql4=@sql4+' and(cb.DocStatus = 7) '
IF(@LoanStatus1 = '8')set @sql4=@sql4+' and(cb.DocStatus IS NULL AND BK.TransferDateApprove is null) '

IF(ISNULL(@HomeType,'')<>'' and ISNULL(@HomeType,'')<>'ทั้งหมด') set @sql4=@sql4+' AND (PR.PType = ''' + @HomeType + ''') '
IF(ISNULL(@ProjectGroup,'')<>'') set @sql4=@sql4+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'
IF(ISNULL(@ProjectType2,'')<>'') set @sql4=@sql4+' AND (PR.ProjectType = ''' + @ProjectType2 + ''')'

IF(@StatusProject = '1')set @sql4=@sql4+' and(PR.RTPExcusive = 1)'
IF(@StatusProject = '2')set @sql4=@sql4+' and(PR.RTPExcusive = 2)'
IF(@StatusProject = '3')set @sql4=@sql4+' and(PR.RTPExcusive = 3)'
IF(@StatusProject = '4')set @sql4=@sql4+' and(PR.RTPExcusive IN (1,2))' 


set @sql4=@sql4+' ORDER BY UN.ProductID,UN.UnitNumber ASC;'

exec(@sql+@sql1+@sql2+@sql3+@sql4) */

exec(@sql)

return 
-- Backup
SET		@sql= ' 
SELECT	UN.ProductID,PR.Project,UN.UnitNumber,UN.AddressNumber
		,BK.CustomerName
		,''NewCustomerName'' = H.NewCustomerName
		,BK.Telephone,BK.SellingPrice,BK.BKArea
		,ISNULL(UN.AreaFromPFB,UN.AreaFromRE) AS TFArea	
		,BK.BookingDate,BK.ContractDate,'''' AS CustStatus
		,CB.CheckDocDate,BK.TransferDate,BK.TransferDateApprove
		,CASE	WHEN BK.TransferDateApprove is not null And BK.Approve3 = 1 THEN ''โอนแล้ว'' ELSE ''ยังไม่โอน'' END AS TransferStatus
		,CASE	WHEN DC.Status = 1 OR (DC.Status IS NULL AND BK.TransferDateApprove is not null And BK.Approve3 = 1) Then ''โอนสด''
				WHEN DC.Status = 2 Then ''กู้เอง''
				WHEN DC.Status = 3 Then ''กู้ผ่านโครงการ''
				WHEN DC.Status = 4 Then ''ยังไม่ตัดสินใจ'' 
				WHEN DC.Status = 5 Then ''ลูกค้ารอยกเลิก'' 
				WHEN DC.Status = 6 Then ''ยังไม่ถึงดิวโอน''  
				WHEN DC.Status = 7 Then ''ขายต่อ''  
				ELSE ''ว่าง(ยังไม่คีย์)'' END AS LoneStatus
		,CASE	WHEN DC.Status <> 1 THEN ISNULL(BA.AdBankName,'''')+ISNULL(''/''+BB.BranchName,'''') ELSE '''' END AS BankName
		,CB.LoanDate
		,CB.LoanDateAccepted	
		,CASE	WHEN DC.Status IN (2,3) AND CB.Status = 1 THEN ''อนุมัติ''
				WHEN DC.Status IN (2,3) AND CB.Status = 2 THEN ''ไม่อนุมัติ''
				WHEN DC.Status IN (2,3) AND CB.Status = 3 THEN ''รอผล''
				WHEN DC.Status IN (2,3) AND CB.Status = 4 THEN ''ยกเลิกคำขอ''
				ELSE '''' END AS BankStatus
		,CB.Loan,CB.LoanAccepted,CB.InsuranceAmount,CB.LoanAcceptedAP
		,CASE	WHEN CB.IsSelected = 0 THEN ''ไม่เลือกใช้''
								WHEN CB.IsSelected = 1 THEN ''เลือกใช้''
								ELSE ''-'' END AS IsSelect
		,CASE WHEN CB.Status = 1 THEN EC1.Description WHEN CB.Status = 2 THEN EC2.Description WHEN CB.Status = 3 THEN EC3.Description END AS Reason
		,EC4.Description AS ReasonNoSelect
		,CB.Remark
		,''Remark2'' = DC.[Note]
		,ISNULL(U2.FirstName,U.FirstName) AS CreateByName
		,ISNULL(DC.EditDate,DC.CreateDate) AS EditDate
		,QuaterNo, SalePrice2
FROM	[ICON_EntForms_Unit] UN
		LEFT OUTER JOIN 
		( '
SET		@sql1= ' 
			SELECT	BK.ProductID,BK.UnitNumber
					,ISNULL([dbo].[fn_GenCustAgreementAllName](AG.ContractNumber),[dbo].[fn_GenCustBookingAllName](BK.BookingNumber)) AS CustomerName					
					,ISNULL(C.Tel_4,ISNULL(ISNULL(Left(AO.Mobile,15),Left(BO.Mobile,15)),'''')) AS Telephone,BK.SellingPrice,ISNULL(BK.StandardArea,0) AS BKArea
					,BK.BookingDate,ISNULL(AG.pContractDate,AG.ContractDate) AS ContractDate,ISNULL(TF.TransferDate,AG.TransferDate) AS TransferDate
					,TF.TransferDateApprove,TF.Approve3
					,(	Select	top 1 ID 
						From	ICON_EntForms_CreditBanking CB
								Left Outer Join [ICON_EntForms_DocumentCheckList] DC ON CB.ContractNumber = DC.ContractNumber
								Left Outer JOin [ICON_EntForms_Transfer] TF ON TF.ContractNumber = CB.ContractNumber
						Where	CB.contractnumber = CASE WHEN CB.contractnumber like ''%B%'' THEN BK.BookingNumber ELSE AG.ContractNumber END '

						IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql1=@sql1+' And (CB.LoanDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
						IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql1=@sql1+' And (CB.LoanDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

						IF(ISNULL(@BankOnly,'''''')<>'''''')set @sql1=@sql1+'and(CB.BankID = '+@BankOnly+')'

						IF(@LoanStatus = '1')set @sql1=@sql1+' and(DC.Status IN (2,3) AND CB.Status = 3)'
						IF(@LoanStatus = '2')set @sql1=@sql1+' and(DC.Status IN (2,3) AND CB.Status = 1)'
						IF(@LoanStatus = '3')set @sql1=@sql1+' and(DC.Status IN (2,3) AND CB.Status = 2)'

						IF(@LoanStatus1 = '1')set @sql1=@sql1+' and(DC.Status = 1 OR (DC.Status IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1)) '
						IF(@LoanStatus1 = '2')set @sql1=@sql1+' and (DC.Status = 2) '
						IF(@LoanStatus1 = '3')set @sql1=@sql1+' and (DC.Status = 3) '
						IF(@LoanStatus1 = '4')set @sql1=@sql1+' and (DC.Status = 4) '
						IF(@LoanStatus1 = '5')SET @sql1=@sql1+' AND (DC.Status = 5) '
						IF(@LoanStatus1 = '6')SET @sql1=@sql1+' AND (DC.Status = 6) '
						IF(@LoanStatus1 = '7')SET @sql1=@sql1+' AND (DC.Status = 7) '
						IF(@LoanStatus1 = '8')set @sql1=@sql1+' and(DC.Status IS NULL AND TF.TransferDateApprove is null) '

SET		@sql2=  ' 
						Order By CB.Isselected DESC,Case When CB.Status = 1 Then 7 When CB.Status = 3 Then 8 When CB.Status = 2 Then 9 When CB.Status = 4 Then 10 Else 0 End Asc,CB.LoanDate Asc,Case when DC.Status = 1 Then 1 Else 0 End Desc,CB.ID Asc
					) AS ID
					,(	Select	top 1 DC.ContractNumber 
						From	[ICON_EntForms_DocumentCheckList] DC 
								Left Outer Join ICON_EntForms_CreditBanking CB ON CB.ContractNumber = DC.ContractNumber
								Left Outer JOin [ICON_EntForms_Transfer] TF ON TF.ContractNumber = DC.ContractNumber
						Where	DC.contractnumber = CASE WHEN DC.contractnumber like ''%B%'' THEN BK.BookingNumber ELSE AG.ContractNumber END '

						IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql2=@sql2+' And (CB.LoanDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
						IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
						set @sql2=@sql2+' And (CB.LoanDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

						IF(ISNULL(@BankOnly,'''''')<>'''''')set @sql2=@sql2+'and(CB.BankID = '+@BankOnly+')'

						IF(@LoanStatus = '1')set @sql2=@sql2+' and(DC.Status IN (2,3) AND CB.Status = 3)'
						IF(@LoanStatus = '2')set @sql2=@sql2+' and(DC.Status IN (2,3) AND CB.Status = 1)'
						IF(@LoanStatus = '3')set @sql2=@sql2+' and(DC.Status IN (2,3) AND CB.Status = 2)'

						IF(@LoanStatus1 = '1')set @sql2=@sql2+' and(DC.Status = 1 OR (DC.Status IS NULL AND TF.TransferDateApprove is not null And TF.Approve3 = 1)) '
						IF(@LoanStatus1 = '2')set @sql2=@sql2+' and(DC.Status = 2) '
						IF(@LoanStatus1 = '3')set @sql2=@sql2+' and(DC.Status = 3) '
						IF(@LoanStatus1 = '4')set @sql2=@sql2+' and(DC.Status = 4) '
						IF(@LoanStatus1 = '5')set @sql2=@sql2+' and(DC.Status = 5) '
						IF(@LoanStatus1 = '6')set @sql2=@sql2+' and(DC.Status = 6) '
						IF(@LoanStatus1 = '7')set @sql2=@sql2+' and(DC.Status = 7) '
						IF(@LoanStatus1 = '8')set @sql2=@sql2+' and(DC.Status IS NULL AND TF.TransferDateApprove is null) '

SET		@sql3=  ' 
						Order By CB.Isselected DESC,Case When CB.Status = 1 Then 7 When CB.Status = 3 Then 8 When CB.Status = 2 Then 9 When CB.Status = 4 Then 10 Else 0 End Asc,CB.LoanDate Asc,Case when DC.Status = 1 Then 1 Else 0 End Desc,CB.ID Asc
					) AS ContractNumber1,BK.CancelDate,BK.BookingNumber,AG.ContractNumber
					,''QuaterNo'' = CAST(YEAR(ISNULL(TF.TransferDate,AG.TransferDate))+543 AS Char(4)) + ''/Q''+ 
						CAST(CASE WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 1 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 4 THEN 1 
						 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 3 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 7 THEN 2 
						 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 7 AND MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) < 10 THEN 3
						 WHEN MONTH(ISNULL(TF.TransferDate,AG.TransferDate)) >= 10 THEN 4 END AS Char(1))
					,''SalePrice2'' = ISNULL(ISNULL(AG.SellingPrice,BK.SellingPrice),0) - ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0)
		
			FROM	[ICON_EntForms_Booking] BK
					LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BO.BookingNumber = BK.BookingNumber AND ISNULL(BO.Header,0) = 1 AND ISNULL(BO.IsDelete,0) = 0 
					LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON AG.BookingNumber = BK.BookingNumber
					LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AO.ContractNumber = AG.ContractNumber AND ISNULL(AO.Header,0) = 1 AND ISNULL(AO.IsDelete,0) = 0 
					LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = AG.ContractNumber 
					LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = BK.ProductID
					LEFT OUTER JOIN [ICON_EntForms_Contacts] C ON C.ContactID = ISNULL(AO.ContactID,BO.ContactID)
			WHERE	1=1 '

			IF(ISNULL(@Projects,'')<>'' And (@Projects <> '''ทั้งหมด''') AND (ISNULL(@Projects,'') <> ''''''))SET @sql3=@sql3+' AND(BK.ProductID IN ('+@Projects+')) '
			IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql3=@sql3+' and(BK.UnitNumber IN ('+@UnitNumber+'))' 
			IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql3=@sql3+' and(BK.UnitNumber = '''+@UnitNumber+''')'

			IF(@StatusAG = '''ทั้งหมด''')set @sql3=@sql3+' AND (BK.Canceldate IS NULL)'
			IF(@StatusAG = '1')set @sql3=@sql3+' AND (BK.Canceldate IS NULL)'
			IF(@StatusAG = '2')set @sql3=@sql3+' AND (BK.Canceldate IS NOT NULL)'
			IF(@StatusAG = '3')set @sql3=@sql3+' AND (BK.Canceldate IS NULL AND TF.TransferDateApprove IS NULL)' -- ยังไม่โอน
			IF(@StatusAG = '4')set @sql3=@sql3+' AND (BK.Canceldate IS NULL AND TF.TransferDateApprove IS NOT NULL)' -- โอนแล้ว

			IF(YEAR(@DateStart2) <> 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
				SET @sql3=@sql3+' AND (BK.BookingDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''') '
			IF(YEAR(@DateStart2) = 1800) AND (YEAR(@DateEnd2) <> 7000) AND ISNULL(@DateStart2,'')<>'' AND ISNULL(@DateEnd2,'')<>''
				SET @sql3=@sql3+' AND (BK.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''') '

			IF(YEAR(@DateStart3) <> 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
				SET @sql3=@sql3+' AND (TF.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart3,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''') '
			IF(YEAR(@DateStart3) = 1800) AND (YEAR(@DateEnd3) <> 7000) AND ISNULL(@DateStart3,'')<>'' AND ISNULL(@DateEnd3,'')<>''
				SET @sql3=@sql3+' AND (TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore3,120)+''') '

			IF(YEAR(@DateStart4) <> 1800) AND (YEAR(@DateEnd4) <> 7000) AND ISNULL(@DateStart4,'')<>'' AND ISNULL(@DateEnd4,'')<>''
				SET @sql3=@sql3+' AND (ISNULL(TF.TransferDate,AG.TransferDate) Between '''+CONVERT(VARCHAR(50),@DateStart4,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore4,120)+''') '
			IF(YEAR(@DateStart4) = 1800) AND (YEAR(@DateEnd4) <> 7000) AND ISNULL(@DateStart4,'')<>'' AND ISNULL(@DateEnd4,'')<>''
				SET @sql3=@sql3+' AND (ISNULL(TF.TransferDate,AG.TransferDate) <= '''+CONVERT(VARCHAR(50),@DateEndInStore4,120)+''') '

			IF(ISNULL(@HomeType,'')<>'') set @sql=@sql+' AND (PR.PType = ''' + @HomeType + ''') '
			IF(ISNULL(@ProjectGroup,'')<>'') set @sql=@sql+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'
			
			IF(@StatusProject = '1')set @sql3=@sql3+' and(PR.RTPExcusive = 1)'
			IF(@StatusProject = '2')set @sql3=@sql3+' and(PR.RTPExcusive = 2)'
			IF(@StatusProject = '3')set @sql3=@sql3+' and(PR.RTPExcusive = 3)'
			IF(@StatusProject = '4')set @sql3=@sql3+' and(PR.RTPExcusive IN (1,2))'

SET		@sql4=  '
		)AS BK ON UN.ProductID = BK.ProductID AND UN.UnitNumber = BK.UnitNumber
		LEFT OUTER JOIN [ICON_EntForms_CreditBanking] CB ON CB.ContractNumber = BK.ContractNumber1 AND CB.ID = BK.ID
		LEFT OUTER JOIN [ICON_EntForms_DocumentCheckList] DC ON DC.ContractNumber = BK.ContractNumber1
		LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = UN.ProductID
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = CB.BankID
		LEFT OUTER JOIN [ICON_EntForms_BankBranch] BB ON BB.BankID = BA.BankID AND BB.BranchID = CB.BranchID
		LEFT OUTER JOIN [Users] U ON DC.CreateBy = U.UserID
		LEFT OUTER JOIN [Users] U2 ON DC.EditBy = U2.UserID
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
			SELECT A.HistoryId,A.ReferentID,
				ISNULL([dbo].[fn_GenCustChangeTransferName](A.NewData,A.ReferentID),[dbo].[fn_GenCustChangeBookingName](A.NewData)) AS NewCustomerName
			FROM dbo.ICON_EntForms_UnitHistory A INNER JOIN 
				(
					SELECT ReferentID,MAX(HistoryId) AS HistoryId
					FROM dbo.ICON_EntForms_UnitHistory 
					WHERE OperateType = ''T''
						AND isApprove <> -1
					GROUP BY ReferentID
				) B ON A.ReferentID=B.ReferentID AND A.HistoryId=B.HistoryId
			WHERE A.OperateType = ''T''
				AND A.isApprove <> -1
		)AS H ON BK.ContractNumber1 = H.ReferentID 


WHERE	UN.Assettype IN (2,4) '

IF(ISNULL(@Projects,'')<>'' And (@Projects <> '''ทั้งหมด''') AND (ISNULL(@Projects,'') <> ''''''))SET @sql4=@sql4+' AND(UN.ProductID IN ('+@Projects+')) '

IF(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql4=@sql4+' And (CB.LoanDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '
IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
set @sql4=@sql4+' And (CB.LoanDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A >= 1)) set @sql4=@sql4+' and(UN.UnitNumber IN ('+@UnitNumber+'))' 
IF(Isnull(@UnitNumber,'')<>'' And (@UnitNumber <> '''ทั้งหมด''') And (@A <= 0)) set @sql4=@sql4+' and(UN.UnitNumber = '''+@UnitNumber+''')'

IF(@StatusAG = '''ทั้งหมด''')set @sql4=@sql4+' and(BK.Canceldate IS NULL)'
IF(@StatusAG = '1')set @sql4=@sql4+' AND (BK.Canceldate IS NULL)'
IF(@StatusAG = '2')set @sql4=@sql4+' AND (BK.Canceldate IS NOT NULL)'
IF(@StatusAG = '3')set @sql4=@sql4+' AND (BK.Canceldate IS NULL AND BK.TransferDateApprove IS NULL)' -- ยังไม่โอน
IF(@StatusAG = '4')set @sql4=@sql4+' AND (BK.CancelDate IS NULL AND BK.TransferDateApprove IS NOT NULL)' -- โอนแล้ว

IF(ISNULL(@BankOnly,'''''')<>'''''')set @sql4=@sql4+'and(CB.BankID = '+@BankOnly+')'

IF(@LoanStatus = '1')set @sql4=@sql4+' and(DC.Status IN (2,3) AND CB.Status = 3)'
IF(@LoanStatus = '2')set @sql4=@sql4+' and(DC.Status IN (2,3) AND CB.Status = 1)'
IF(@LoanStatus = '3')set @sql4=@sql4+' and(DC.Status IN (2,3) AND CB.Status = 2)'

IF(@LoanStatus1 = '1')set @sql4=@sql4+' and(DC.Status = 1 OR (DC.Status IS NULL AND BK.TransferDateApprove is not null And BK.Approve3 = 1)) '
IF(@LoanStatus1 = '2')set @sql4=@sql4+' and(DC.Status = 2) '
IF(@LoanStatus1 = '3')set @sql4=@sql4+' and(DC.Status = 3) '
IF(@LoanStatus1 = '4')set @sql4=@sql4+' and(DC.Status = 4) '
IF(@LoanStatus1 = '5')set @sql4=@sql4+' and(DC.Status = 5) '
IF(@LoanStatus1 = '6')set @sql4=@sql4+' and(DC.Status = 6) '
IF(@LoanStatus1 = '7')set @sql4=@sql4+' and(DC.Status = 7) '
IF(@LoanStatus1 = '8')set @sql4=@sql4+' and(DC.Status IS NULL AND BK.TransferDateApprove is null) '

IF(ISNULL(@HomeType,'')<>'') set @sql=@sql+' AND (PR.PType = ''' + @HomeType + ''') '
IF(ISNULL(@ProjectGroup,'')<>'') set @sql=@sql+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'

IF(@StatusProject = '1')set @sql4=@sql4+' and(PR.RTPExcusive = 1)'
IF(@StatusProject = '2')set @sql4=@sql4+' and(PR.RTPExcusive = 2)'
IF(@StatusProject = '3')set @sql4=@sql4+' and(PR.RTPExcusive = 3)'
IF(@StatusProject = '4')set @sql4=@sql4+' and(PR.RTPExcusive IN (1,2))'

set @sql4=@sql4+' ORDER BY UN.ProductID,UN.UnitNumber ASC;'



GO
