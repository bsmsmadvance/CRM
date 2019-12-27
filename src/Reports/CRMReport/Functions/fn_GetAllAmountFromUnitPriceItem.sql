SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	Funcion: fn_GetAllAmountFromUnitPriceItem (ใช้สำหรับดูค่าเงินของ Unit นั้นๆ)
*/

ALTER FUNCTION [dbo].[fn_GetAllAmountFromUnitPriceItem]
(
	@UnitID NVARCHAR(100)
)
RETURNS @result TABLE (Name VARCHAR(50), Amount money, Installment NVARCHAR(10), PriceUnitAmount money, PricePerUnitAmount money)
AS

BEGIN

	DECLARE @UPI Table(Name VARCHAR(50), Amount money, Installment NVARCHAR(10), PriceUnitAmount money, PricePerUnitAmount money)
    INSERT INTO @UPI(Name, Amount, Installment, PriceUnitAmount, PricePerUnitAmount)
	SELECT	'Name' = UPI.Name 
            , 'Amount' = UPI.Amount
            , 'Installment' = UPI.Installment
            , 'PriceUnitAmount' = UPI.PriceUnitAmount
            , 'PricePerUnitAmount' = UPI.PricePerUnitAmount
	FROM [SAL].[Booking] B WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[UnitPrice] UP WITH (NOLOCK) ON UP.BookingID = B.ID
    LEFT OUTER JOIN [SAL].[UnitPriceItem] UPI WITH (NOLOCK) ON UPI.UnitPriceID = UP.ID
    WHERE UP.IsActive = 1 AND B.UnitID = ''+@UnitID+''

    INSERT INTO @result
    select * from @UPI

    RETURN
END






GO
