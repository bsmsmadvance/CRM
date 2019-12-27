SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_PF_AG_005_1_2]'10104AA00001'
CREATE PROC [dbo].[PF_MD_001_1]
	@ProjectNo nvarchar(50)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT 'PayableAmount' = '',
	'PayableAmountText' = '', --[dbo].[fnBHT_BahtText](PayableAmount),
	'DueDate' = '' --[dbo].[ForMatDateTime]('TH','dd mmmm yyyy',DueDate)
FROM PRJ.Project --ICON_EntForms_AgreementPeriod  WITH (NOLOCK)
WHERE ProjectNo=@ProjectNo
--	AND PaymentType = '8'



GO
