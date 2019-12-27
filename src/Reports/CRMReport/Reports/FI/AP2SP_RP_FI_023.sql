SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [dbo].[AP2SP_RP_FI_023] '','10045','','','','','5','','','2','1',''
-- [dbo].[AP2SP_RP_FI_023] '','','','2013-08-30','2013-08-30','','','','','2','1',''
-- [dbo].[AP2SP_RP_FI_023] '','','','','','','','','','2','1','2013-08-26'

CREATE PROCEDURE [dbo].[AP2SP_RP_FI_023]
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,
	@LandStatus nvarchar(10),
	@UnitStatus nvarchar(2),
	@LoanStatus1 nvarchar(2),
    @UserName   nvarchar(150),
	@currentuserid nvarchar(20),	
	@WorkTransferStatus nvarchar(10),
	@WorkTransferDate datetime
AS

DECLARE @DateEndInStore Datetime,@A varchar(5)
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

Declare @sql1 nvarchar(max),@sql2 nvarchar(max)
Set @sql1 = ''
Set @sql2 = ''
IF (Isnull(@currentuserid,'')<>'' AND Isnull(@ProductID,'')='')
begin
	set @sql1=@sql1+' AND (ag.ProductID IN (SELECT ProductID FROM vw_UserinProduct WHERE UserID = '''+@currentuserid+'''))'
	set @sql2=@sql2+' AND (bk.ProductID IN (SELECT ProductID FROM vw_UserinProduct WHERE UserID = '''+@currentuserid+'''))'
End

Declare @sql nvarchar(max)
Set @sql = '

SELECT	''CompanyID'' = '''' --PR.CompanyID
        ,''CompanyNameThai'' = '''' --C.CompanyNameThai
		,''ProductID'' = '''' --PR.ProductID
        ,''ProjectName'' = '''' --PR.Project
		,''UnitNumber'' = '''' --UN.UnitNumber
        ,''AddressNumber'' = '''' --UN.AddressNumber
		,''LandNumber'' = '''' --TD.TitledeedNumber
		,''TotalSellingPrice'' = '''' /* CASE WHEN TF.ContractNumber IS NULL THEN ISNULL(ISNULL(AG.SellingPrice,BK.SellingPrice),0) 
			- ISNULL(ISNULL(AG.TransferDiscount,BK.TransferDiscount),0) ELSE  TF.NetSalePrice END */
		,''PaymentedAmount'' = '''' --ISNULL(R.PaymentedAmount,0)
		,''HomePreAmount'' = '''' --HPT.HomePreAmount
		,''FeeAmount'' = '''' --Ceiling(ISNULL(F.FeeAmount,0)) - ISNULL(FPT.FeePreAmount,0)
		,''FreePreAmount'' = '''' --FPT.FeePreAmount
		,''TitledeedStatus'' = '''' --ISNULL(EX.Description, '''')
		,''UnitStatus'' = '''' /* CASE WHEN TF.TransferNumber IS NOT NULL AND TF.TransferDateApprove IS NOT NULL THEN ''โอนกรรมสิทธิ์แล้ว'' 
			WHEN TF.TransferNumber IS NOT NULL AND TF.TransferDateApprove IS NULL THEN ''รอโอนกรรมสิทธิ์'' 
			WHEN AG.ContractNumber IS NOT NULL THEN ''สัญญา'' 
			WHEN BK.BookingNumber IS NOT NULL THEN ''จอง'' 
			ELSE ''ห้องว่าง'' END */
		,''TransferDate'' = '''' --ISNULL(TF.TransferDate,WTF.TransferDate)
		,''LoanStatus'' = '''' /* CASE WHEN DC.Status = 1 THEN ''โอนสด'' 
			WHEN DC.Status = 2 THEN ''กู้เอง'' 
			WHEN DC.Status = 3 THEN ''กู้ผ่านโครงการ'' 
			WHEN DC.Status = 4 THEN ''ไม่ตัดสินใจ'' 
			WHEN DC.Status = 5 THEN ''ลูกค้ารอยกเลิก'' 
			WHEN DC.Status = 6 THEN ''ยังไม่ถึงดิวโอน''
			WHEN DC.Status = 7 THEN ''ต่อขาย''
			WHEN (DC.Status IS NULL AND TF.TransferDateApprove IS NULL) THEN ''ยังไม่มีการขอสินเชื่อ'' END */
		,''BookingNumber'' = '''' --BK.BookingNumber
		,''ContractNumber'' = '''' --AG.ContractNumber		
		,''BankName'' = '''' --CASE WHEN DC.Status <> 1 THEN ISNULL(BA.AdBankName,'''') ELSE '''' END
		,''BranchName'' = '''' --CASE WHEN DC.Status <> 1 THEN REPLACE(ISNULL(BB.BranchName,''''),''สาขา'','''') ELSE '''' END
		,''Ptype'' = '''' --PR.Ptype
        ,''ProjectGroup'' = '''' --PR.ProjectGroup
FROM	[PRJ].[Unit] UN' --This is main table below table need to be use as well
		/* LEFT OUTER JOIN [ZTF_TransferStatus] WTF ON WTF.UnitNumber=UN.UnitNumber AND WTF.ProductID=UN.ProductID 
		LEFT OUTER JOIN ICON_EntForms_TitledeedDetail TD ON TD.UnitNumber=UN.UnitNumber AND TD.ProductID=UN.ProductID
        LEFT OUTER JOIN ICON_EntForms_ExtCod EX ON EX.GType = ''LandStatus'' AND TD.Status=EX.Ref
		LEFT OUTER JOIN ICON_EntForms_Booking BK ON BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber AND BK.CancelDate IS NULL
		LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ICON_EntForms_Transfer TF ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN ICON_EntForms_DocumentCheckList DC ON ((DC.ContractNumber = AG.ContractNumber '+@sql1+') OR (DC.BookingNumber = BK.BookingNumber '+@sql2+'))
		LEFT OUTER JOIN ICON_EntForms_Products PR ON PR.ProductID = UN.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_CreditBanking] CB ON ((AG.ContractNumber = CB.ContractNumber OR BK.BookingNumber = CB.ContractNumber) AND DC.Status <> 1 AND (CB.IsSelected = 1)) 
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = CB.BankID 
		LEFT OUTER JOIN [ICON_EntForms_BankBranch] BB ON BB.BankID = BA.BankID AND BB.BranchID = CB.BranchID 
		LEFT OUTER JOIN [ICON_EntForms_Company] C ON PR.CompanyID = C.CompanyID '

Set @sql = @sql + '
		LEFT OUTER JOIN
		(
			SELECT AP.ContractNumber,SUM(PD.Amount) AS PaymentedAmount
			FROM 
			(	
				SELECT AG1.ContractNumber, CASE WHEN AP1.PaymentType = ''4'' THEN AG1.BookingNumber ELSE AG1.ContractNumber END AS ReferentNumber 
					,AP1.PaymentType,AP1.Period
				FROM ICON_EntForms_AgreementPeriod AS AP1 INNER JOIN
					ICON_EntForms_Agreement AS AG1 ON AG1.ContractNumber = AP1.ContractNumber
				WHERE 1 = 1 '
IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' AND (AG1.ProductID = '''+@ProductID+''') '
Set @sql = @sql + '
				UNION
				SELECT BK.BookingNumber,BK.BookingNumber,''4'',''0''
				FROM ICON_EntForms_Booking BK
				WHERE NOT EXISTS(SELECT * FROM ICON_EntForms_AgreementPeriod AS AP1 INNER JOIN
					ICON_EntForms_Agreement AS AG1 ON AG1.ContractNumber = AP1.ContractNumber WHERE AG1.BookingNumber=BK.BookingNumber)
					'
IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' AND (BK.ProductID = '''+@ProductID+''') '
Set @sql = @sql + '
			) AP LEFT OUTER JOIN 
				ICON_Payment_PaymentDetails AS PD ON PD.ReferentID = AP.ReferentNumber AND PD.PaymentType = AP.PaymentType AND PD.Period = AP.Period LEFT OUTER JOIN 
				ICON_Payment_TmpReceipt TR ON TR.RCReferent = PD.RCReferent AND TR.TmpReceiptID = PD.TmpReceiptID 
			WHERE PD.PaymentType IN (''4'',''5'',''6'',''8'',''A06'') 
				AND TR.CancelDate IS NULL 
			GROUP BY AP.ContractNumber
		) R ON (R.ContractNumber = AG.ContractNumber OR R.ContractNumber = BK.BookingNumber) '
Set @sql = @sql + '
		LEFT OUTER JOIN
		(
			SELECT TR.ReferentID AS ContractNumber,SUM(PD.Amount) AS HomePreAmount
			FROM ICON_Payment_TmpReceipt TR LEFT OUTER JOIN
				ICON_Payment_PaymentDetails PD ON TR.RCReferent=PD.RCReferent AND TR.TmpReceiptID=PD.TmpReceiptID
			WHERE TransferPaymentType = 1
				AND PD.PaymentType IN (''A06'',''8'',''5'',''6'',''4'')
				AND TR.DepositID IS NULL
			GROUP BY TR.ReferentID 
		) HPT ON HPT.ContractNumber = AG.ContractNumber LEFT OUTER JOIN
		(
			SELECT TransferNumber,SUM(CASE WHEN Code = ''15'' AND ChangeYNH = ''H'' THEN ISNULL(Amount,0)/2 ELSE Amount END) AS FeeAmount 
			FROM dbo.ICON_EntForms_TransferFee
			WHERE ChangeYNH IN (''Y'',''H'')
				AND Code IN (''37'',''01'',''02'',''15'',''17'')
			GROUP BY TransferNumber
		) F ON F.TransferNumber = TF.TransferNumber LEFT OUTER JOIN
		(
			SELECT TR.ReferentID AS ContractNumber,SUM(PD.Amount) AS FeePreAmount
			FROM ICON_Payment_TmpReceipt TR LEFT OUTER JOIN
				ICON_Payment_PaymentDetails PD ON TR.RCReferent=PD.RCReferent AND TR.TmpReceiptID=PD.TmpReceiptID
			WHERE TransferPaymentType = 1
				AND PD.PaymentType IN (''37'',''01'',''02'',''15'',''17'')
				AND TR.DepositID IS NULL
			GROUP BY TR.ReferentID 
		) FPT ON FPT.ContractNumber = AG.ContractNumber 
		'

Set @sql = @sql + '
WHERE	UN.AssetType IN (2,4) 
	'
    
IF (Isnull(@currentuserid,'')<>'' AND Isnull(@ProductID,'')='')
	set @sql=@sql+' AND (PR.ProductID IN (SELECT ProductID FROM vw_UserinProduct WHERE UserID = '''+@currentuserid+'''))'

IF (Isnull(@CompanyID,'')<>'')set @sql=@sql+' AND (PR.CompanyID = '''+@CompanyID+''')'
IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' AND (PR.ProductID = '''+@ProductID+''')'
IF (Isnull(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A >= 1)) set @sql=@sql+' AND (UN.UnitNumber IN ('+@UnitNumber+'))' 
IF (Isnull(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A <= 0)) set @sql=@sql+' AND (UN.UnitNumber = '''+@UnitNumber+''')'

IF (Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   set @sql=@sql+' AND (ISNULL(TF.TransferDate,WTF.TransferDate) Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF (Year(@DateStart) = 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   set @sql=@sql+' AND (ISNULL(TF.TransferDate,WTF.TransferDate) <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'

IF(Isnull(@LandStatus,'')<>'') set @sql=@sql+' AND (TD.Status = '''+@LandStatus+''') '

IF(@UnitStatus = '1')set @sql=@sql+' AND (BK.BookingNumber IS NULL) '
IF(@UnitStatus = '2')set @sql=@sql+' AND (BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL) '
IF(@UnitStatus = '3')set @sql=@sql+' AND (AG.ContractNumber IS NOT NULL AND TF.TransferNumber IS NULL) '
IF(@UnitStatus = '4')set @sql=@sql+' AND (TF.TransferNumber IS NOT NULL AND TF.TransferDateApprove IS NULL) '
IF(@UnitStatus = '5')set @sql=@sql+' AND (TF.TransferNumber IS NOT NULL AND TF.TransferDateApprove IS NOT NULL) '

IF(@LoanStatus1 = '1')set @sql=@sql+' AND (DC.Status = 1 OR (DC.Status IS NULL AND TF.TransferDateApprove IS NOT NULL AND TF.Approve3 = 1)) '
IF(@LoanStatus1 = '2')set @sql=@sql+' AND (DC.Status = 2) '
IF(@LoanStatus1 = '3')set @sql=@sql+' AND (DC.Status = 3) '
IF(@LoanStatus1 = '4')set @sql=@sql+' AND (DC.Status = 4) '
IF(@LoanStatus1 = '5')set @sql=@sql+' AND (DC.Status = 5) '
IF(@LoanStatus1 = '6')set @sql=@sql+' AND (DC.Status = 6) '
IF(@LoanStatus1 = '7')set @sql=@sql+' AND (DC.Status = 7) '
IF(@LoanStatus1 = '8')set @sql=@sql+' AND (DC.Status IS NULL AND TF.TransferDateApprove IS NULL) '

IF(@WorkTransferStatus = '1')set @sql=@sql+' AND (WTF.TransferStatus = 1) '
IF(@WorkTransferStatus = '1' AND (Year(@WorkTransferDate) <> 1800) AND (ISNULL(@WorkTransferDate,'')<>''))set @sql=@sql+' AND (WTF.WorkTransferDate = '''+CONVERT(VARCHAR(50),@WorkTransferDate,120)+''') '

Set @sql = @sql+ ' ORDER BY PR.CompanyID,PR.ProductID, UN.UnitNumber ASC;' */

EXEC(@sql)
--Print(@sql)

GO
