SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_RP_FI_024] '','70019','ad1-4','','',''

CREATE PROCEDURE [dbo].[AP2SP_RP_FI_024]
	@CompanyID  nvarchar(50),
    @ProductID  nvarchar(20),
    @UnitNumber nvarchar(20),
	@DateStart  datetime ,
	@DateEnd    datetime ,	
    @UserName   nvarchar(150)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
DECLARE @DateEndInStore Datetime,@A varchar(5)
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
SET @A = (Select CHARINDEX('''',@UnitNumber)) 


Declare @sql nvarchar(max)
Set @sql = '

SELECT	''CompanyID'' = '''' --PR.CompanyID
        ,''CompanyName'' = '''' --C.CompanyNameThai
		,''ProductID'' = '''' --PR.ProductID
        ,''ProjectName'' = '''' --PR.Project
		,''ContractDate'' = '''' --AG.ContractDate
		,''TransferDateApprove'' = '''' --TF.TransferDateApprove
		,''UnitNumber'' = '''' --AG.UnitNumber
		,''CustomerName'' = '''' --AO.FirstName + '' '' + AO.LastName
		,''WBSNumber'' = '''' --REPLACE(REPLACE(U.wbsnumber,''CO-0'',''CO-P''),''-R-'',''-P-'')
		,''FreeMortgage'' = '''' --ISNULL(TP.ValueMortgage,0)
		,''FreeFund'' = '''' --(SELECT Amount FROM ICON_EntForms_TransferFee WHERE TransferNumber=TF.TransferNumber AND Code=''2G'' AND ChangeYNH=''N'')
		,''FreePublic'' = '''' --(SELECT CASE WHEN ChangeYNH=''H'' THEN Amount/2 ELSE Amount END FROM ICON_EntForms_TransferFee WHERE TransferNumber=TF.TransferNumber AND Code IN (''00'',''000'') AND (ChangeYNH=''N'' OR ChangeYNH=''H''))
		,''FreeMeter'' = '''' --(SELECT SUM(Amount) FROM ICON_EntForms_TransferFee WHERE TransferNumber=TF.TransferNumber AND Code IN (''01'',''02'') AND ChangeYNH=''N'')
		,''FGFDiscount'' = '''' --AG.FGFDiscount
		,''FGFName'' = '''' --AG.IntroducerName
		,''Remark'' = NULL
		,''CashBack'' = '''' --ch.CashBack
		,''ContractNumber'' = '''' --ag.ContractNumber
		,''FreeDown''= '''' --ISNULL(D.FreeDownAmount,0)
FROM	[SAL].[Agreement] AG WITH (NOLOCK)'  --This is main table, need to use table below as well
		/* LEFT OUTER JOIN ICON_EntForms_AgreementOwner AO WITH (NOLOCK)  ON AG.ContractNumber = AO.ContractNumber AND ISNULL(AO.Header,0)=1 AND ISNULL(AO.ISDelete,0)=0
		LEFT OUTER JOIN ICON_EntForms_Transfer TF WITH (NOLOCK)  ON AG.ContractNumber = TF.ContractNumber
		LEFT OUTER JOIN ICON_EntForms_Products PR WITH (NOLOCK)  ON PR.ProductID = AG.ProductID 
		LEFT OUTER JOIN ICON_EntForms_Company C WITH (NOLOCK)  ON PR.CompanyID = C.CompanyID
		LEFT OUTER JOIN ICON_EntForms_Unit U WITH (NOLOCK)  ON U.ProductID = AG.ProductID AND U.UnitNumber = AG.UnitNumber 
		LEFT OUTER JOIN ICON_EntForms_TransferPayment TP WITH (NOLOCK)  ON TF.TransferNumber = TP.TransferNumber 
		left join  (
				select DocumentID as ContractNumber,s.Amount*pd.PricePerUnit as CashBack 
				from ZPROM_SalePromotionDetail s WITH (NOLOCK)  
					left join  ZPROM_PromotionDetail pd WITH (NOLOCK)  on pd.PromotionID=s.PromotionID and pd.ItemID=s.ItemID
				where (DescriptionTH like ''%Cash Back%'' or DescriptionTH like ''%CashBack%'')
					and s.DocumentType=''2''
				union
				select p.ContractNumber, t.Amount*pd.PricePerUnit as CashBack 
				from ZPROM_TransferPromotion p WITH (NOLOCK)  
					left join ZPROM_TransferPromotionDetail t WITH (NOLOCK)  on p.TransferPromotionID=t.TransferPromotionID
					left join ZPROM_PromotionDetail pd WITH (NOLOCK)  on pd.PromotionID=t.PromotionID and pd.ItemID=t.ItemID
				where (DescriptionTH like ''%Cash Back%'' or DescriptionTH like ''%CashBack%'')
					and isnull(ISselected,0)=1
					and isnull(p.iscancel,0) = 0
		) ch on ch.ContractNumber = ag.ContractNumber 
		LEFT OUTER JOIN (SELECT DocumentID,DocumentType ,FreeDownAmount
						FROM dbo.CRM_FreeDown WITH (NOLOCK)
						where DocumentType = 2 AND  ISNULL(FreeDownAmount,0) > 0) D on AG.ContractNumber = D.DocumentID '
Set @sql = @sql + '
WHERE AG.CancelDate IS NULL 
	'

IF (Isnull(@CompanyID,'')<>'')set @sql=@sql+' AND (PR.CompanyID = '''+@CompanyID+''')'
IF (Isnull(@ProductID,'')<>'')set @sql=@sql+' AND (PR.ProductID = '''+@ProductID+''')'
IF (Isnull(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A >= 1)) set @sql=@sql+' AND (AG.UnitNumber IN ('+@UnitNumber+'))' 
IF (Isnull(@UnitNumber,'')<>'' AND (@UnitNumber <> '''ทั้งหมด''') AND (@A <= 0)) set @sql=@sql+' AND (AG.UnitNumber = '''+@UnitNumber+''')'

IF (Year(@DateStart) <> 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   set @sql=@sql+' AND (TF.TransferDateApprove Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' AND '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
IF (Year(@DateStart) = 1800) AND (Year(@DateEnd) <> 7000) AND ISNULL(@DateStart,'')<>'' AND ISNULL(@DateEnd,'')<>''
				   set @sql=@sql+' AND (TF.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'

Set @sql = @sql+ ' ORDER BY PR.CompanyID,PR.ProductID, AG.UnitNumber ASC;' */

EXEC(@sql)
Print(@sql)


GO
