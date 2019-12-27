SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--Select 'A' = [dbo].[fn_GenRDateContract_Card]('10039AA00278')
CREATE FUNCTION [dbo].[fn_GenRDateContract_Card]
(
	@ContractNumber nvarchar(50)
)
RETURNS datetime
AS
BEGIN

	DECLARE @result datetime;
	
	DECLARE @v TABLE(RDate datetime);

	INSERT INTO @v

		SELECT	TR.RDate		
		FROM	[ICON_Payment_TmpReceipt] TR 
				LEFT OUTER JOIN [ICON_Payment_PaymentDetails] PD ON PD.RCReferent = TR.RCReferent AND PD.TmpReceiptID = TR.TmpReceiptID
		WHERE	TR.ReferentID =  @ContractNumber  AND TR.CancelDate IS NULL AND PD.PaymentType = '5'
		ORDER BY TR.RDate
						
	-- ดึงเอารายการแรกมาเป็นค่าเริ่มต้นข้อความก่อน
	SET @result = (SELECT TOP 1 RDate FROM @v ORDER BY RDate ASC);	

	RETURN @result;

END
































GO
