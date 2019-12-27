SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--[dbo].[AP2SP_RP_AG_003] '10130','08B23','','','','Administrator Account',''
CREATE PROCEDURE [dbo].[AP2SP_RP_AG_003]

	@ProductID	nvarchar(15),
	@UnitNumber nvarchar(40),
	@DateStart	datetime,
	@DateEnd Datetime,
	@MailType1 int,
	@UserName	nvarchar(40)
	,@StatusAG varchar(50)=''
AS

Declare @sql nvarchar(max)
Set @sql = '
Select	''ProductID'' = '''' --AG.ProductID
		,''ProjectName'' = '''' --PR.Project
		,''UnitNumber'' = '''' --AG.UnitNumber
        ,''ContractNumber'' = '''' --AG.ContractNumber
		,''CustomerName'' = '''' --ISNULL(AO.NamesTitleExt,AO.NamesTitle)+ISNULL(AO.FirstName,'''')+ISNULL(AO.LastName,'''')
		,''LetterStatus'' = '''' --ISNULL(UH.OperateName,''-'')
		,''LetterName'' = '''' /* CASE	WHEN ND.TimeofNotification = 1 Then ''แจ้งรายละเอียดการชำระเงิน''
								WHEN ND.TimeofNotification = 2 Then ''ขอให้ชำระค่างวด''
								WHEN ND.TimeofNotification = 3 Then ''แจ้งยกเลิกสัญญา''
								ELSE ''-'' END*/
		,''NotificationDate'' = '''' --ND.NotificationDate
        ,''Backword'' = '''' --ND.Backword
		,''Reason'' = '''' --ISNULL(ND.Reason,''-'')
		,''AGStatus'' = '''' --Case When ag.CancelDate Is Null Then ''Active'' Else ''Cancel'' End AGStatus
		,''TimeofNotification'' = '''' --ND.TimeofNotification
		,''CancelDate'' = '''' --Isnull(ag.CancelDate,GetDate())


From	[SAL].[Agreement] A ' --This is temp table actual table start below
        /* [ICON_EntForms_NotificationDownPayment] ND 
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
Where	1=1 
'

        IF(ISNULL(@ProductID,'')<>'')set @sql=@sql+' AND(AG.ProductID = '''+@ProductID+''') '
        IF(ISNULL(@UnitNumber,'')<>'')set @sql=@sql+' AND(AG.UnitNumber = '''+@UnitNumber+''') '
		IF(ISNULL(@DateStart,'')<>'' AND Year(@DateStart) <> 1800 AND ISNULL(@DateEnd,'') <> '' AND Year(@DateEnd) <> 7000)
		  set @sql=@sql+' AND(ND.NotificationDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEnd,120)+''') ' 
		IF(@MailType1 = 1)Set @sql = @sql+' AND ND.TimeofNotification = ''1'''
		IF(@MailType1 = 2)Set @sql = @sql+' AND ND.TimeofNotification = ''2'''
		IF(@MailType1 = 3)Set @sql = @sql+' AND ND.TimeofNotification = ''3'''
		if(@StatusAG = '1')set @sql=@sql+' AND (AG.Canceldate IS NULL)'
		if(@StatusAG = '2')set @sql=@sql+' AND (AG.Canceldate IS NOT NULL)'
		
set @sql=@sql+'ORDER BY AG.ProductID,AG.UnitNumber,ND.NotificationDate ASC;' */
exec(@sql)
print(@sql)










GO
