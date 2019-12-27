SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_TR_012] '','10060','','','','','','','','','','',''
-- [dbo].[AP2SP_RP_TR_012] '','10098','','2012-06-01','2012-06-29','','','','','','1199'
-- [dbo].[AP2SP_RP_TR_012] '','10099','','','','','','','','Administrator Account','2','1','20171128'

CREATE PROCEDURE [dbo].[AP2SP_RP_TR_012]
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
    @UnitNumber varchar(8000),
	@DateStart  datetime ,
	@DateEnd    datetime ,
	@LandStatus nvarchar(10),
	@UnitStatus nvarchar(2),
	@LoanStatus1 nvarchar(2),
	@QCStatus nvarchar(2),		
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
/* IF (Isnull(@currentuserid,'')<>'' AND Isnull(@ProductID,'')='')
begin
	set @sql1=@sql1+' AND (ag.ProductID IN (SELECT ProductID FROM vw_UserinProduct WHERE UserID = '''+@currentuserid+'''))'
	set @sql2=@sql2+' AND (bk.ProductID IN (SELECT ProductID FROM vw_UserinProduct WHERE UserID = '''+@currentuserid+'''))'
End */

Declare @sql nvarchar(max)
Set @sql = /* 'Declare @Tmp Table(ProductID nvarchar(50),M_SAP_CODE nvarchar(50),UNIT_CODE nvarchar(50),M_REQUEST_PHASE_Name nvarchar(50),END_PRODUCT_COMPLETE  nvarchar(5),FINISHDATE datetime,SE_COUNT int,PhaseType nvarchar(20))
Insert into @Tmp
Select ProductID,M_SAP_CODE,UNIT_CODE,M_REQUEST_PHASE_Name,END_PRODUCT_COMPLETE,FINISHDATE,SE_COUNT,PhaseType
From [vw_AP_QC_UNIT_REQUEST_PHASE] */
'
SELECT	''ProductID'' = '''' --PR.ProductID
		,''ProjectName'' = '''' --PR.Project AS ProjectName
		,''UnitNumber'' = '''' --UN.UnitNumber
        ,''AddressNumber'' = '''' --UN.AddressNumber
		,''LandNumber'' = '''' --TD.TitledeedNumber
		,''TitledeedStatus'' = '''' --ISNULL(EX.Description, '''') AS TitledeedStatus
		,''UnitStatus'' = '''' /* CASE WHEN TF.TransferNumber IS NOT NULL AND TF.TransferDateApprove IS NOT NULL THEN ''โอนกรรมสิทธิ์แล้ว'' 
			WHEN TF.TransferNumber IS NOT NULL AND TF.TransferDateApprove IS NULL THEN ''รอโอนกรรมสิทธิ์'' 
			WHEN AG.ContractNumber IS NOT NULL THEN ''สัญญา'' 
			WHEN BK.BookingNumber IS NOT NULL THEN ''จอง'' 
			ELSE ''ห้องว่าง'' END */
		,''LoanStatus'' = '''' /* CASE WHEN DC.Status = 1 THEN ''โอนสด'' 
			WHEN DC.Status = 2 THEN ''กู้เอง'' 
			WHEN DC.Status = 3 THEN ''กู้ผ่านโครงการ'' 
			WHEN DC.Status = 4 THEN ''ไม่ตัดสินใจ'' 
			WHEN DC.Status = 5 THEN ''ลูกค้ารอยกเลิก'' 
			WHEN DC.Status = 6 THEN ''ยังไม่ถึงดิวโอน''
			WHEN DC.Status = 7 THEN ''ขายต่อ''
			WHEN (DC.Status IS NULL AND TF.TransferDateApprove IS NULL) THEN ''ยังไม่มีการขอสินเชื่อ'' END */
		,''TransferDate'' = '''' --ISNULL(TF.TransferDate,WTF.TransferDate) AS TransferDate
		,''AGTransferDate'' = '''' --AG.TransferDate AS AGTransferDate
		,''SAPProductID'' = '''' --PR.SAPProductID
		,''WBSNumber'' = '''' --UN.WBSNumber
		,''BookingNumber'' = '''' --BK.BookingNumber
		,''ContractNumber'' = '''' --AG.ContractNumber		
		,''END_PRODUCT_COMPLETE'' = '''' --ISNULL(NP.END_PRODUCT_COMPLETE,'''') AS END_PRODUCT_COMPLETE
		,''SE_COUNT'' = '''' --ISNULL(NP.SE_COUNT, 0) AS SE_COUNT 
		,''FINISHDATE'' = '''' --NP.FINISHDATE
		,''BankName'' = '''' --CASE WHEN DC.Status <> 1 THEN ISNULL(BA.AdBankName,'''') ELSE '''' END
		,''BranchName'' = '''' --CASE WHEN DC.Status <> 1 THEN REPLACE(ISNULL(BB.BranchName,''''),''สาขา'','''') ELSE '''' END
		,''IsImport'' = '''' --CAST(0 AS BIT)
		,''WaiveQCDate'' = '''' --WQC.WaiveQCDate 
		,''TotalPrice''= '''' ' --round((Isnull(tf.NetSalePrice,0))/1000000,2) '
Set @sql = @sql + '
FROM	[PRJ].[Unit] UN' --This is main table. Need to use below table as well
		/* LEFT OUTER JOIN [ZTF_TransferStatus] WTF ON WTF.UnitNumber=UN.UnitNumber AND WTF.ProductID=UN.ProductID
		LEFT OUTER JOIN ICON_EntForms_TitledeedDetail TD ON TD.UnitNumber=UN.UnitNumber AND TD.ProductID=UN.ProductID
        LEFT OUTER JOIN ICON_EntForms_ExtCod EX ON EX.GType = ''LandStatus'' AND TD.Status=EX.Ref
		LEFT OUTER JOIN ICON_EntForms_Booking BK ON BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber AND BK.CancelDate IS NULL
		LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber
		LEFT OUTER JOIN ICON_EntForms_Transfer TF ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN ICON_EntForms_DocumentCheckList DC ON ((DC.ContractNumber = AG.ContractNumber '+@sql1+') OR (DC.BookingNumber = BK.BookingNumber '+@sql2+'))
		LEFT OUTER JOIN ICON_EntForms_Products PR ON PR.ProductID = UN.ProductID 
		LEFT OUTER JOIN [ICON_EntForms_CreditBanking] CB ON ((AG.ContractNumber = CB.ContractNumber OR BK.BookingNumber = CB.ContractNumber) AND DC.Status <> 1 AND CB.IsSelected = 1) 
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = CB.BankID 
		LEFT OUTER JOIN [ICON_EntForms_BankBranch] BB ON BB.BankID = BA.BankID AND BB.BranchID = CB.BranchID 
		LEFT OUTER JOIN [ZQC_WaiveQC] WQC ON WQC.UnitNumber=UN.UnitNumber AND WQC.ProductID=UN.ProductID  '

Set @sql = @sql + '
		LEFT OUTER JOIN
		(
			SELECT P.M_SAP_CODE,P.UNIT_CODE,P.END_PRODUCT_COMPLETE,P.FINISHDATE,P.SE_COUNT 
			FROM 
				(
					SELECT P.M_SAP_CODE,P.UNIT_CODE,MAX(P.M_REQUEST_PHASE_Name) AS M_REQUEST_PHASE_Name
					FROM @Tmp P 
					WHERE 1 = 1 and FINISHDATE is not null and PhaseType = ''3'''				
						IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' AND (P.ProductID = '''+@ProductID+''') '
Set @sql = @sql + '
					GROUP BY P.M_SAP_CODE,P.UNIT_CODE
				) T LEFT OUTER JOIN @Tmp P ON P.M_SAP_CODE=T.M_SAP_CODE AND P.UNIT_CODE=T.UNIT_CODE AND P.M_REQUEST_PHASE_Name=T.M_REQUEST_PHASE_Name			
	) AS NP ON PR.SAPProductID = NP.M_SAP_CODE AND UN.WBSNumber = NP.UNIT_CODE  '
		
Set @sql = @sql + '
WHERE	UN.AssetType IN (2,4) '

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

IF(@QCStatus = '1')set @sql=@sql+' AND (ISNULL(NP.SE_COUNT, 0) = 0 AND ISNULL(NP.END_PRODUCT_COMPLETE,'''') = '''')  '
IF(@QCStatus = '2')set @sql=@sql+' AND (ISNULL(NP.SE_COUNT, 0) > 0 AND ISNULL(NP.END_PRODUCT_COMPLETE,'''') = '''') '
IF(@QCStatus = '3')set @sql=@sql+' AND (ISNULL(NP.END_PRODUCT_COMPLETE,'''') = ''Y'') '

IF(@WorkTransferStatus = '1')set @sql=@sql+' AND (WTF.TransferStatus = 1) '
IF(@WorkTransferStatus = '1' AND (Year(@WorkTransferDate) <> 1800) AND (ISNULL(@WorkTransferDate,'')<>''))set @sql=@sql+' AND (WTF.WorkTransferDate = '''+CONVERT(VARCHAR(50),@WorkTransferDate,120)+''') '

Set @sql = @sql+ ' ORDER BY PR.ProductID, UN.UnitNumber ASC;' */

EXEC(@sql)
--Print(@sql)











GO
