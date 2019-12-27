SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PF_LC_009N_1]
    @QuotationNo  nvarchar(16)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @QUPI Table(Name VARCHAR(50), Amount money, Installment nvarchar(20), InstallmentAmount money, PriceUnitAmount nvarchar(10), PricePerUnitAmount money)
INSERT INTO @QUPI(Name, Amount, Installment, InstallmentAmount, PriceUnitAmount, PricePerUnitAmount)
	SELECT	'Name' = QUPI.Name 
            , 'Amount' = QUPI.Amount
            , 'Installment' = QUPI.Installment
            , 'InstallmentAmount' = QUPI.InstallmentAmount
            , 'PriceUnitAmount' = QUPI.PriceUnitAmount
            , 'PricePerUnitAmount' = QUPI.PricePerUnitAmount
	FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [SAL].[QuotationUnitPriceItem] QUPI WITH (NOLOCK) ON QUPI.QuotationUnitPriceID = QUP.ID
    WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1


SELECT 'MortgageFee' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าจดจำนอง'),0)
        , 'TransfeeFee' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าธรรมเนียมการโอน'),0) 
        , 'DocumentFeeCharge' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าดำเนินการเอกสาร'),0)
        , 'ElectricMeterExpense' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'มิเตอร์ไฟ'),0)
        , 'CommonFee' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าส่วนกลาง'),0) 
        , 'TotalExpenseOnTransfer' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าจดจำนอง'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าธรรมเนียมการโอน'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าดำเนินการเอกสาร'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'มิเตอร์ไฟ'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าส่วนกลาง'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่ากองทุนแรกเข้าเรียกเก็บครั้งเดียว'),0) 
        , 'ElectricMeterSize' = CASE WHEN U.ElectricMeterPriceID != NULL THEN [dbo].[fn_GetElectricMeterSizeFromUnit] (U.ElectricMeterPriceID)
                                ELSE [dbo].[fn_GetElectricMeterSizeFromModel] (U.ModelID) END
        , 'CommonFeePerWaa' = ISNULL(AC.PublicFundRate, 0)
        , 'CommonFeeDuration' = [dbo].[fn_GetYearAndMonth] (ISNULL((SELECT PriceUnitAmount FROM @QUPI WHERE Name = N'ค่าส่วนกลาง'),0)) 
        , 'FirstTimeFundPerSquareMeter' = ISNULL((SELECT PricePerUnitAmount FROM @QUPI WHERE Name = N'ค่ากองทุนแรกเข้าเรียกเก็บครั้งเดียว'),0) 
        , 'FirstTimeFundAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่ากองทุนแรกเข้าเรียกเก็บครั้งเดียว'),0) 
FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [PRJ].[AgreementConfig] AC WITH (NOLOCK) ON AC.ProjectID = Q.ProjectID
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ID = Q.UnitID
WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1





GO
