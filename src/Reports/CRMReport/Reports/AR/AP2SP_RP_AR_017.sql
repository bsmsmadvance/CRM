SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_AR_017] 'Q','1800-01-01 00:00:00','7000-12-31 00:00:00','','''ทั้งหมด'''

CREATE PROC [dbo].[AP2SP_RP_AR_017]
	@CompanyID nvarchar(40), 
	@DateStart datetime,
	@DateEnd	datetime,
	@BatchID nvarchar(40),
	@JV  nvarchar(Max)

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)

Declare @sql nvarchar(max)
Set @sql= '

SELECT	''ReferentID'' = '''' --JV.ReferentID
        , ''CustName'' = '''' --JV.CustName
        , ''OldproductID'' = '''' --JV.OldproductID
        , ''UnitNumber'' = '''' --JV.UnitNumber
        , ''OperateName'' = '''' --JV.OperateName
		, ''OperateDate'' = '''' --JV.OperateDate
        , ''Paid'' = '''' --JV.Paid
        , ''SuspenseAmount'' = '''' --JV.SuspenseAmount
        , ''TotalPaidAmount'' = '''' --JV.TotalPaidAmount
        , ''BookingDate'' = '''' --JV.BookingDate
		, ''BatchID'' = '''' --HG.BatchID
        , ''CancelDate'' = '''' --HG.CancelDate
        , ''FirstName'' = '''' --UE.FirstName
        , ''CRMCompanyID'' = '''' --HG.CRMCompanyID
		, ''CompanyName'' = '''' --CO.CompanyID +'' - ''+CO.CompanyNameThai AS CompanyName
        , ''OperateType'' = '''' --JV.OperateType
FROM	[MST].[Company] C' --This is temp table actual table start from below
/* [ICON_PostToSap_Header] HG
LEFT OUTER JOIN 
(

	SELECT	UH.ReferentID
			,CASE WHEN UH.OperateType = ''BV'' THEN BO.Firstname+ '' '' +BO.Lastname ELSE AO.Firstname+ '' '' +AO.Lastname END as CustName
			,UH.OldproductID,UH.UnitNumber,UH.OperateName,UH.BatchID,UH.ApproveDate OperateDate,UH.OperateType
			,UH.Paid,UH.SuspenseAmount,UH.TotalPaidAmount
			,CASE WHEN UH.OperateType = ''BV'' THEN BK.BookingDate ELSE BK1.BookingDate END AS BookingDate

	FROM	ICON_EntForms_UnitHistory UH 
			LEFT OUTER JOIN (SELECT BookingNumber,firstname,lastname FROM ICON_EntForms_BookingOwner WHERE header = ''1'' AND ISNULL(IsDelete,0) = 0) BO ON UH.ReferentID = BO.BookingNumber AND UH.OperateType = ''BV''
			LEFT OUTER JOIN (SELECT Contractnumber,firstname,lastname FROM ICON_EntForms_AgreementOwner WHERE header = ''1'' AND ISNULL(IsDelete,0) = 0) AO ON UH.ReferentID = AO.ContractNumber AND UH.OperateType = ''V''
			LEFT OUTER JOIN ICON_EntFOrms_Booking BK ON BK.BookingNumber = UH.ReferentID AND UH.OperateType = ''BV''
			LEFT OUTER JOIN ICON_EntFOrms_Agreement AG ON AG.ContractNumber = UH.ReferentID AND UH.OperateType = ''V''
			LEFT OUTER JOIN ICON_EntFOrms_Booking BK1 ON BK1.BookingNumber = AG.BookingNumber 

	WHERE	UH.BatchID IS NOT NULL AND UH.OperateType IN (''BV'',''V'')

)AS JV ON JV.BatchID = HG.BatchID

LEFT OUTER JOIN USERS UE ON UE.UserID = HG.CancelBy
LEFT OUTER JOIN ICON_EntForms_Company CO ON CO.CompanyID = HG.CRMCompanyID
Where HG.CRMPostCode = ''JV'' AND HG.PostID IN (6,8) AND JV.OperateType  IN (''BV'',''V'')'


		IF(ISNULL(@CompanyID,'')<>'')set @sql=@sql+' AND(CO.CompanyID IN (SELECT * FROM [dbo].[fn_SplitString]('''+@CompanyID+''','',''))) '
		IF(Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
                   set @sql=@sql+'and(HG.OperateDate Between '''+CONVERT(VARCHAR(10),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(20),@DateEndInStore,120)+''')'
		IF(ISNULL(@BatchID,'')<>'')set @sql=@sql+' AND(HG.BatchID IN (SELECT * FROM [dbo].[fn_SplitString]('''+@BatchID+''','','')))'
		IF(Isnull(@JV,'')<>'' And (@JV <> '''ทั้งหมด''')) set @sql=@sql+' and(HG.BatchID IN ('+@JV+'))' 

		set @sql=@sql+' Order By HG.BatchID,JV.Operatedate Desc ' */

		exec(@sql)
		print(@sql)

GO
