SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- [dbo].[AP2SP_PF_TR_007_1] '10171','n04'
ALTER PROC [dbo].[AP2SP_PF_TR_007_1]
	@ProductID nvarchar(50)=''
	,@UnitNumber nvarchar(50)=''
	,@UserName nvarchar(50)=''
AS
Declare @TransferNumber nvarchar(50)
Set @TransferNumber=''
/* If(Isnull(@TransferNumber,'')='')
Begin
	Set @TransferNumber = ((Select TransferNumber From ICON_EntForms_Transfer t 
							Left Join ICON_EntForms_Agreement a On a.ContractNumber=t.ContractNumber
							Where a.CancelDate Is Null and a.ProductID=@ProductID and a.UnitNumber=@UnitNumber))
End */
Select *
From(
SELECT	'TransferNumber' = '' --TF.TransferNumber		
		,'BankName' = '' --RTrim(LTrim(Replace(BA.BankName,'ธนาคาร',''))) AS BankName
		,'AdBankName' = '' --BA.AdBankName
        ,'Branch' = '' --TC.Branch
        ,'BankNumber' = '' --TC.BankNumber
        ,'DueDate' = '' --TC.DueDate
        ,'Amount' = '' --Isnull(TC.Amount,0)Amount
        ,'FlagPayIn' = '' --CAST(TC.CompanyID AS varchar(2))AS FlagPayIn
        ,'TransferDateApprove' = '' --TF.TransferDateApprove
FROM	[SAL].[Agreement] --This is temp table. Actual table start from below
        /* [ICON_EntForms_Transfer] TF
		LEFT OUTER JOIN [ICON_EntForms_TransferCheque] TC ON TC.TransferNumber = TF.TransferNumber
		Left Join ICON_Payment_TmpReceipt tr On tc.BankNumber=tr.Number and tc.Bank=tr.BankID and tc.Amount=tr.Amount and CancelDate Is Null
		LEFT OUTER JOIN [ICON_EntForms_Bank] BA ON BA.BankID = TC.Bank
WHERE	TF.TransferNumber = @TransferNumber
and Isnull(TC.Amount,0)>0
and (tc.ChequeOrder=1 or Isnull(tc.TransferPaymentType,2)=2 or Not Exists(Select * From ICON_Payment_PaymentDetails pd Where pd.TmpReceiptID=tr.TmpReceiptID and pd.PaymentType In('6','8','A06'))
)*/

Union
Select @TransferNumber TransferNumber,''BankName,''AdBankName,''Branch,''BankNumber,null DueDate
,0 Amount,'Z'FlagPayIn,null TransferDateApprove
From [PRJ].[Unit] u
WHERE	UnitNo=@UnitNumber and ProjectID=@ProductID 
--and Not Exists(Select * From [ICON_EntForms_TransferCheque] TC Where TC.TransferNumber = Isnull(@TransferNumber,'') and TC.CompanyID='Z')
)t
Where amount>0
Order by FlagPayIn desc



GO
