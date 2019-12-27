SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- [dbo].[AP2SP_RP_FI_006] Null,'60001',Null,'2012-09-26','2012-09-26',Null,Null,'Administrator Account'
CREATE PROCEDURE [dbo].[AP2SP_RP_FI_006]
	@CompanyID  nvarchar(20),
 	@ProductID	nvarchar(15) = '',
    @SBUID	    nvarchar(20),
	@DateStart	datetime,
	@DateEnd	datetime,
    @DateStart2	datetime,
	@DateEnd2	datetime,
	@UserName	nvarchar(50) = ''
AS


DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
DECLARE @DateEndInStore2 Datetime
SET @DateEndInStore2 = [dbo].[fn_GetMaxDate](@DateEnd2)


Declare @sql nvarchar(max)
Set @sql = '

SELECT	''CompanyNameThai'' = '''' --CO.CompanyNameThai
        ,''RDate'' = '''' --TR.RDate
        ,''ProductID'' = '''' --TR.ProductID
		,''ShortName'' = '''' --	CASE	WHEN TR.RefType = ''1'' THEN MM1.TypeOfRealEstate
								--	ELSE MM2.TypeOfRealEstate END
		,''UnitNumber'' = '''' --TR.UnitNumber
		,''Detail'' = '''' /* CASE WHEN PD.PaymentType = ''4'' THEN ''จอง''
						   WHEN PD.PaymentType = ''5'' THEN ''สัญญา''
						   WHEN PD.PaymentType = ''8'' THEN ''โอน''
						   WHEN PD.PaymentType = ''6'' THEN	RIGHT(''00''+CONVERT(nvarchar(5), PD.Period),3)
						   ELSE CONVERT(nvarchar(20),PD.Period) END */
		,''DepositDate'' = '''' --DE.DepositDate
		,''Bank'' = '''' --ISNULL(BA.AdBankName,'''')+ISNULL(''/''+TR.BranchName,'''')
		,''CreditBankID'' = '''' --PP.CreditBankID
        ,''CreditType'' = '''' --PP.CreditType
        ,''Number'' = '''' --TR.Number
		,''CustName'' =	'''' --CASE WHEN TR.RefType = ''1'' THEN CAST(ISNULL(BO.ContactID,'''')AS nvarchar(20))+''-''+ISNULL(BO.FirstName,'''')+'' ''+ISNULL(BO.LastName,'''')
						--ELSE CAST(ISNULL(AO.ContactID,'''')AS nvarchar(20))+''-''+ISNULL(AO.FirstName,'''')+'' ''+ISNULL(AO.LastName,'''') END
		,''Amount'' = '''' --TR.Amount
		,''Total'' = '''' --ISNULL(TR.Amount,0)
		,''Fee'' = '''' --ISNULL(TR.ChargeAmount,0)
		,''VatFee'' = '''' --ISNULL(TR.ChargeAmount*0.07,0)
		,''Net'' = '''' --ISNULL(TR.Amount,0)-ISNULL(TR.ChargeAmount,0)-ISNULL(TR.ChargeAmount*0.07,0)
		,''Tax Fee'' = '''' --ISNULL(TR.ChargeAmount*0.03,0)
		,''%Fee'' = '''' --ISNULL(TR.Charge,0)
		,''ProjectName'' = '''' --ISNULL(PR.ProductID,'''')+''-''+ISNULL(PR.Project,'''')

FROM	[SAL].[Booking] B' --This is temp table actual table start from below
        /* [ICON_Payment_TmpReceipt] TR
		LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
		LEFT OUTER JOIN [ICON_Payment_Payment] PP ON PP.RCReferent = TR.RCReferent AND TR.Number = PP.Number
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = TR.BankID
		LEFT OUTER JOIN [ICON_EntForms_Booking] BK ON BK.BookingNumber = TR.ReferentID
		LEFT OUTER JOIN [ICON_EntForms_Unit] UN1 ON UN1.ProductID = BK.ProductID AND UN1.UnitNumber = BK.UnitNumber
		LEFT OUTER JOIN [ICON_EntForms_ManageModel] MM1 ON MM1.ProductID = UN1.ProductID AND MM1.ModelID = UN1.ModelID
		LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO ON BK.BookingNumber = BO.BookingNumber AND ISNULL(BO.Header,0) = 1 AND ISNULL(BO.IsDelete,0) = 0
		LEFT OUTER JOIN [ICON_EntForms_Agreement] AG ON AG.ContractNumber = TR.ReferentID
		LEFT OUTER JOIN [ICON_EntForms_Unit] UN2 ON UN2.ProductID = AG.ProductID AND UN2.UnitNumber = AG.UnitNumber
		LEFT OUTER JOIN [ICON_EntForms_ManageModel] MM2 ON MM2.ProductID = UN2.ProductID AND MM2.ModelID = UN2.ModelID
		LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO ON AG.ContractNumber = AO.ContractNumber AND ISNULL(AO.Header,0) = 1 AND ISNULL(AO.IsDelete,0) = 0
		LEFT OUTER JOIN [ICON_Payment_Deposit] DE ON DE.DepositID = TR.DepositID AND TR.DepositID IS NOT NULL
        LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = CASE WHEN TR.RefType = ''1'' THEN BK.ProductID ELSE AG.ProductID END 
		LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PR.CompanyID


WHERE	(TR.Method = 3)	
	AND (TR.DepositID IS NOT NULL)
	'
		if(Isnull(@CompanyID,'')<>'') set @sql=@sql+' AND (CO.CompanyID = '''+@CompanyID+''')'
		if(Isnull(@ProductID,'')<>'') set @sql=@sql+' AND (TR.ProductID = '''+@ProductID+''')'
		if(Isnull(@SBUID,'')<>'')	  set @sql=@sql+' AND (PR.SBUID = '''+@SBUID+''')'

		if (ISNULL(@DateStart,'') <> '' AND ISNULL(@DateEnd,'') <> '') AND (YEAR(@DateStart) > 1800 And YEAR(@DateEnd) < 7000)
			set @sql=@sql+' and(TR.RDate Between '''+CONVERT(VARCHAR(50),@DateStart,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')'
		
		if (ISNULL(@DateStart2,'') <> '' AND ISNULL(@DateEnd2,'') <> '') AND (YEAR(@DateStart2) > 1800 And YEAR(@DateEnd2) < 7000)
			set @sql=@sql+' and(DE.DepositDate Between '''+CONVERT(VARCHAR(50),@DateStart2,120)+''' And '''+CONVERT(VARCHAR(50),@DateEndInStore2,120)+''')'
		
set @sql=@sql+'ORDER BY TR.RDate,TR.ProductID,ShortName,TR.UnitNumber' */

exec(@sql)

GO
