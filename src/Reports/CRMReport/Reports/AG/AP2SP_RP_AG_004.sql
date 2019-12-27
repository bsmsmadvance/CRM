SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--[dbo].[AP2SP_RP_AG_004] '60006','''B07C03'',''''','','','',''
--[dbo].[AP2SP_RP_AG_004] '10082','','','','','Administrator Account',''
CREATE PROCEDURE [dbo].[AP2SP_RP_AG_004]
	@ProductID	nvarchar(15),
	@UnitNumber nvarchar(4000),
	@DateStart	Datetime,
	@DateEnd	Datetime,
	@MailType2 int,
	@UserName	nvarchar(40),  
	@StatusAG varchar(50)=''
AS

DECLARE @DateEndInStore Datetime,@A varchar(5)
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 

Declare @sql nvarchar(max)
Set @sql = '
Select	''ProductID'' = '''' --AG.ProductID
		,''ProjectName'' = '''' --PR.Project
		,''UnitNumber'' = '''' --AG.UnitNumber
        ,''ContractNumber'' = '''' --AG.ContractNumber
		,''CustomerName'' = '''' --ISNULL(AO.NamesTitleExt,ISNULL(AO.NamesTitle,'''')) + ISNULL(AO.FirstName,'''') + '' '' + ISNULL(AO.LastName,'''')
		,''LetterStatus'' = '''' --ISNULL(UH.OperateName,''-'')
		,''LetterName'' = '''' /* CASE	WHEN ND.TimeofNotification = 1 Then ''นัดโอนครั้งที่ 1''
								WHEN ND.TimeofNotification = 2 Then ''นัดโอนครั้งสุดท้าย''
								WHEN ND.TimeofNotification = 3 Then ''แจ้งยกเลิกสัญญา''
								WHEN ND.TimeofNotification = 4 Then ''นัดโอน(ฉบับพิเศษ)''
								WHEN ND.TimeofNotification = 5 Then ''แจ้งเตือน 1''
								WHEN ND.TimeofNotification = 6 Then ''แจ้งเตือน 2''
								WHEN ND.TimeofNotification = 7 Then ''ยกเลิกสัญญา(ฉบับพิเศษ)''
								WHEN ND.TimeofNotification = 8 Then ''นัดโอนครั้งสุดท้าย(ฉบับพิเศษ)''
								WHEN ND.TimeofNotification = 9 Then ''นัดโอนครั้งที่ 1-1''
								WHEN ND.TimeofNotification = 10 Then ''นัดโอนครั้งที่ 2-1''
								ELSE ''-'' END	*/
		,''NotificationDate'' = '''' --ND.NotificationDate
        ,''DateInLetter'' = '''' --ND.DateInLetter
        ,''Backword'' = '''' --ND.Backword
		,''Reason'' = '''' --case when ND.Reason = '''' or ND.Reason = null  then ND.ReasonGroup
					--else ND.Reason end 
		,''RespondDate'' = '''' --ND.RespondDate
		,''AGStatus'' = '''' --Case When AG.CancelDate Is Null Then ''Active'' Else ''Cancel'' End AGStatus
		,''RespondDateDiff'' = '''' --DATEDIFF(day,ND.RespondDate,ND.DateInLetter) 
From	[SAL].[Agreement] A' --This is temp table actual table start from below
/*         [ICON_EntForms_NotificationTranfer] ND 
		LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON ND.ContractNumber = AG.ContractNumber
		LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AG.ContractNumber = AO.ContractNumber And ISNULL(AO.Header,0) = 1 AND  ISNULL(IsDelete,0) = 0
		LEFT OUTER JOIN 
		(
			SELECT	OldProductID,UnitNumber,ReferentID,OperateName
			FROM	[ICON_EntForms_UnitHistory]
			WHERE	OperateType IN (''T'',''A'',''B'') 
					AND OperateDate <= GetDate() AND ISNULL(IsApprove,0) = 0
		) UH ON  UH.ReferentID = ND.ContractNumber
		LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = AG.ProductID
		LEFT OUTER JOIN [ICON_EntForms_Transfer] TF ON TF.ContractNumber = AG.ContractNumber 
Where	1=1 
'

        IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(AG.ProductID = '''+@ProductID+''') '
		if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A >= 1)) set @sql=@sql+' AND (AG.UnitNumber IN ('+@UnitNumber+'))' 
		if(Isnull(@UnitNumber,'')<>'' AND  (@UnitNumber <> '''ทั้งหมด''') AND  (@A <= 0)) set @sql=@sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')'

		IF(ISNULL(@DateStart,'')<>'' AND Year(@DateStart) <> 1800 AND ISNULL(@DateEnd,'') <> '' AND Year(@DateEnd) <> 7000)
		  set @sql=@sql+' AND (ND.NotificationDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''') ' 

		IF(YEAR(@DateStart) = 1800) AND (YEAR(@DateEnd) <> 7000) 
			set @sql=@sql+'AND (ND.NotificationDate <= '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'
		

		IF(@MailType2 = 1)Set @sql = @sql+' AND ND.TimeofNotification = ''1'''
		IF(@MailType2 = 2)Set @sql = @sql+' AND ND.TimeofNotification = ''2'''
		IF(@MailType2 = 3)Set @sql = @sql+' AND ND.TimeofNotification = ''3'''
		IF(@MailType2 = 4)Set @sql = @sql+' AND ND.TimeofNotification = ''4'''
		IF(@MailType2 = 5)Set @sql = @sql+' AND ND.TimeofNotification = ''5'''
		IF(@MailType2 = 6)Set @sql = @sql+' AND ND.TimeofNotification = ''6'''
		IF(@MailType2 = 7)Set @sql = @sql+' AND ND.TimeofNotification = ''7'''

		IF(@StatusAG = '1')set @sql=@sql+' AND (AG.Canceldate IS NULL)'
		IF(@StatusAG = '2')set @sql=@sql+' AND (AG.Canceldate IS NOT NULL)'
		IF(@StatusAG = '3')set @sql=@sql+' AND (TF.TransferDateApprove IS NULL)' -- ยังไม่โอน
		IF(@StatusAG = '4')set @sql=@sql+' AND (AG.CancelDate IS NULL AND TF.TransferDateApprove IS NOT NULL)' -- โอนแล้ว

set @sql=@sql+'ORDER BY AG.ProductID,AG.UnitNumber,ND.NotificationDate ASC;' */

print(@sql)
exec(@sql)




GO
