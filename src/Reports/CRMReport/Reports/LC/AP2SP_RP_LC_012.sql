SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[AP2SP_RP_LC_012] NULL,'40017','2018-06-01 00:00:00','2018-07-01 00:00:00','','','Administrator Account',NULL,NULL,NULL,NULL

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_012]
	@CompanyID nvarchar(50),
    @ProductID  nvarchar(20),
	@DateStart datetime,
	@DateEnd datetime,
	@Status nvarchar(20),
	@Reason nvarchar(20)='',
    @UserName nvarchar(150), 
	@UnitNumber nvarchar(500),
	@HomeType nvarchar(20),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)

AS


DECLARE @DateEndInStore Datetime,@A nvarchar(5)
SET @DateEndInStore  = [dbo].[fn_GetMaxDate] (@DateEnd)
DECLARE @DateStartInStore Datetime

SET @A = (Select CHARINDEX('''',@UnitNumber)) 
IF (YEAR(@DateStart) = 1800 ) SET @DateStartInStore = NULL
IF (YEAR(@DateEnd) = 7000 )	  SET @DateEndInStore = NULL

Declare @sql nvarchar(max)
Set @sql= '

SELECT	''RefType'' = '''' --UH.RefType
        ,''ReferenceID'' = '''' --UH.ReferentID
        ,''CompanyID'' = '''' --P.CompanyID 
		, ''CompanyName'' = '''' --C.CompanyNameThai
		, ''ProductID'' = '''' --P.ProductID 
		, ''ProjectName'' = '''' --ISNULL(P.ProductID,'''')+''-''+ISNULL(P.Project,'''')
		, ''TypeOfRealEstate'' = '''' --M.TypeOfRealEstate --ประเภท
		, ''UnitNumber'' = '''' --U.UnitNumber  --รหัสสินค้า
		, ''BookingNumber'' = '''' --CASE WHEN UH.RefType = ''1'' THEN B.BookingNumber WHEN UH.RefType = ''2'' THEN A.BookingNumber END
		, ''ContractNumber'' = '''' --A.ContractNumber --เลขที่สัญญาหลัก
		, ''CustomerName'' = '''' /* CASE WHEN UH.RefType = ''1'' THEN [dbo].[fn_GenCustBookingAllNoTitle](B.BookingNumber)
								WHEN UH.RefType = ''2'' THEN [dbo].[fn_GenCustAgreementAllNoTitle](A.ContractNumber) END */
		, ''Type'' = '''' /* CASE WHEN UH.RefType = ''1'' THEN ''จอง''
                       WHEN UH.RefType = ''2'' THEN ''สญ'' END */
		, ''OperateDate''  = '''' --UH.ApproveDate
		, ''BookingDate'' = '''' /* CASE WHEN UH.RefType = ''1'' THEN B.BookingDate
                              WHEN UH.RefType = ''2'' THEN BK.BookingDate END */
		, ''ContractDate'' = '''' --A.ContractDate
		, ''ApproveBy'' =  '''' --US.FirstName
		, ''TotalSellingPrice'' = '''' /* CASE WHEN UH.RefType = ''1'' THEN B.SellingPrice
                                    WHEN UH.RefType = ''2'' THEN A.SellingPrice END */
		, ''TransferDiscount'' = '''' --ISNULL(A.TransferDiscount,B.TransferDiscount)
		, ''Area'' = '''' --ISNULL(U.AreaFromPFB,U.AreaFromRE)   --M.StandardBuiltInSQM --พื้นที่ใช้สอย
		, ''SaleName'' = 	'''' /* CASE WHEN UH.RefType = ''1'' THEN US3.FirstName
						     ELSE US4.FirstName END */
		, ''OperateBy'' = '''' --US2.FirstName
		, ''ReceiveAmount'' =  '''' --ISNULL(DC1.ReceiveAmount1,0)+ISNULL(DC2.ReceiveAmount2,0) 
		, ''ReceiveMoney'' = '''' --ISNULL(UH.Paid,0)
		, ''ReturnMoney'' = '''' --ISNULL(UH.SuspenseAmount,0)
		, ''Reason'' = '''' /* CASE	WHEN UH.RefType = ''1'' THEN ISNULL(R2.ReasonDescription,''-'')
							WHEN UH.RefType = ''2'' THEN ISNULL(R1.ReasonDescription,''-'') END */
		, ''ReasonText'' = '''' /* ISNULL((Select ReasonDescription From [Icon_EntForms_Reason] 
                                   Where ReasonID = ''' + Isnull(@Reason,'') + '''),''ทั้งหมด'') */
		, ''CancelType'' = '''' /* CASE	WHEN UH.RefType = ''1'' THEN ISNULL(B.CancelType,''-'')
								WHEN UH.RefType = ''2'' THEN ISNULL(A.CancelType,''-'') END */
		, ''Remark'' = '''' --CASE WHEN ISNULL(UH.Remark,'''') = '''' Then ''-'' ELSE UH.Remark END
		, ''GroupName'' = '''' /* CASE	WHEN UH.RefType = ''1'' THEN ISNULL(R2.GroupName,''-'')
							WHEN UH.RefType = ''2'' THEN ISNULL(R1.GroupName,''-'') END */ 

FROM  [SAL].[Booking] ' --This is temp table, actual table start from below
      /* [ICON_EntForms_UnitHistory] UH 
      LEFT OUTER JOIN [SAL].[Agreement] A  ON A.ID = UH.ReferentID 
      LEFT OUTER JOIN [SAL].[Booking] B  ON B.BookingNumber = UH.ReferentID
      LEFT OUTER JOIN [SAL].[Booking] BK  ON BK.ID = A.BookingID
	  LEFT OUTER JOIN [PRJ].[Project] P ON P.ID = ISNULL(A.ProjectID,B.ProjectID) 
	  LEFT OUTER JOIN [MST].[Company] C ON C.ID = P.CompanyID   
	  LEFT OUTER JOIN [PRJ].[Unit] U ON (U.ID = ISNULL(A.UnitID,B.UnitID) AND U.ProjectID = ISNULL(A.ProjectID,B.ProjectID)) 
	  LEFT OUTER JOIN [PRJ].[Model] M ON M.ProjectID = U.ProjectID AND M.ID = U.ModelID 
      LEFT OUTER JOIN [USR].[User] US ON US.UserID = UH.ApproveBy
      LEFT OUTER JOIN [USR].[User] US2 ON US2.UserID = UH.OperateBy 
      LEFT OUTER JOIN [USR].[User] US3 ON US3.UserID = B.SaleID 
      LEFT OUTER JOIN [USR].[User] US4 ON US4.UserID = A.SaleID
      LEFT OUTER JOIN [Icon_EntForms_Reason] R1 ON R1.ReasonID = A.CancelReason 
      LEFT OUTER JOIN [Icon_EntForms_Reason] R2 ON R2.ReasonID = B.CancelReason '
Set @sql= @sql+ '
	  LEFT OUTER JOIN
		(
			Select ''ReceiveAmount1'' = SUM(Amount),ReferentID
			FROM	ICON_Payment_PaymentDetails
			Where	PaymentType IN (''4'')
			Group BY ReferentID
		) DC1 ON DC1.ReferentID = BK.BookingNumber
	  LEFT OUTER JOIN
		(
			Select ''ReceiveAmount2'' = SUM(Amount),ReferentID
			FROM	ICON_Payment_PaymentDetails
			Where	PaymentType IN (''4'',''5'',''6'',''8'')
			Group BY ReferentID
		) DC2 ON DC2.ReferentID = UH.ReferentID '
Set @sql= @sql+ '
WHERE UH.OperateType IN (''V'',''BV'') AND UH.OperateName IN (''ยกเลิกสัญญา'',''ยกเลิกจอง'') AND UH.IsApprove = 1 '

if(ISNULL(@CompanyID,'')<>'')set @sql=@sql+'and(P.CompanyID = '''+@CompanyID+''') '
if(ISNULL(@ProductID,'')<>'')set @sql=@sql+'and(P.ProductID = '''+@ProductID+''') '
if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A >= 1)) set @sql=@sql+' AND (U.UnitNumber IN ('+@UnitNumber+'))' 
if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A <= 0)) set @sql=@sql+' AND (U.UnitNumber = '''+@UnitNumber+''')'

if(ISNULL(@Status,'')<>'' AND @Status <> 'ทั้งหมด')set @sql=@sql+'and(UH.RefType='''+@Status+''')'
if(ISNULL(@Reason,'')<>'')set @sql=@sql+'and((A.CancelReason= '''+@Reason+''') OR (B.CancelReason = '''+@Reason+''')) '
if(YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
   set @sql=@sql+'and(UH.ApproveDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') '

IF(ISNULL(@HomeType,'')<>'' AND @HomeType<>'ทั้งหมด') set @sql=@sql+' AND (P.PType = ''' + @HomeType + ''')'
IF(ISNULL(@ProjectGroup,'')<>'') set @sql=@sql+' AND (P.ProjectGroup = ''' + @ProjectGroup + ''')'
IF(ISNULL(@ProjectType2,'')<>'') set @sql=@sql+' AND (P.ProjectType = ''' + @ProjectType2 + ''')'

set @sql=@sql+'Order By P.ProductID,M.TypeOfRealEstate,UH.UnitNumber,UH.ReferentID; ' */

exec( @sql)


GO
