SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_PF_AG_005_1_2]'10104AA00001'
CREATE PROC [dbo].[AP2SP_PF_AG_001_1]
	@ContractNumber nvarchar(50)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
/* SELECT PayableAmount,
	'PayableAmountText' = [dbo].[fnBHT_BahtText](PayableAmount),
	'DueDate' = [dbo].[ForMatDateTime]('TH','dd mmmm yyyy',DueDate)
FROM ICON_EntForms_AgreementPeriod  WITH (NOLOCK)
WHERE Contractnumber=@ContractNumber
	AND PaymentType = '8' */


GO
