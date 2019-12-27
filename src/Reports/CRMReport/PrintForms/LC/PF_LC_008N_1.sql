SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PF_LC_008N_1]
    @QuotationNo  nvarchar(16)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @QUPI Table(Name VARCHAR(20), Amount money, Installment nvarchar(2), InstallmentAmount money, PriceUnitAmount nvarchar(10))
INSERT INTO @QUPI(Name, Amount, Installment, InstallmentAmount, PriceUnitAmount)
	SELECT	'Name' = QUPI.Name 
            , 'Amount' = QUPI.Amount
            , 'Installment' = QUPI.Installment
            , 'InstallmentAmount' = QUPI.InstallmentAmount
            , 'PriceUnitAmount' = QUPI.PriceUnitAmount
	FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [SAL].[QuotationUnitPriceItem] QUPI WITH (NOLOCK) ON QUPI.QuotationUnitPriceID = QUP.ID
    WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1


SELECT 'MortgageFee' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าจดจำนอง'),0)
        , 'TransfeeFee' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าธรรมเนียมการโอน'),0) 
        , 'DocumentFeeCharge' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าดำเนินการเอกสาร'),0)
        , 'WaterMeterExpense' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'มิเตอร์น้ำ'),0) 
        , 'ElectricMeterExpense' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'มิเตอร์ไฟ'),0)
        , 'CommonFee' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าส่วนกลาง'),0) 
        , 'TotalExpenseOnTransfer' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าจดจำนอง'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าธรรมเนียมการโอน'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าดำเนินการเอกสาร'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'มิเตอร์น้ำ'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'มิเตอร์ไฟ'),0) +
                                        ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ค่าส่วนกลาง'),0)
        , 'WaterMeterSize' = CASE WHEN U.WaterMeterPriceID != NULL THEN [dbo].[fn_GetWaterMeterSizeFromUnit] (U.WaterMeterPriceID)
                                ELSE [dbo].[fn_GetWaterMeterSizeFromModel] (U.ModelID) END
        , 'ElectricMeterSize' = CASE WHEN U.ElectricMeterPriceID != NULL THEN [dbo].[fn_GetElectricMeterSizeFromUnit] (U.ElectricMeterPriceID)
                                ELSE [dbo].[fn_GetElectricMeterSizeFromModel] (U.ModelID) END
        , 'CommonFeePerWaa' = ISNULL(AC.PublicFundRate, 0)
        , 'CommonFeeDuration' = [dbo].[fn_GetYearAndMonth] (ISNULL((SELECT PriceUnitAmount FROM @QUPI WHERE Name = N'ค่าส่วนกลาง'),0)) 
FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [PRJ].[AgreementConfig] AC WITH (NOLOCK) ON AC.ProjectID = Q.ProjectID
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ID = Q.UnitID
WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1



GO
