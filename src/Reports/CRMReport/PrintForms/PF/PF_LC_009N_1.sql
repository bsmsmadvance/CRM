SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PF_LC_009N_1]
    @QuotationNo  nvarchar(15)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT 'MortgageFee' = CASE WHEN MPI.Detail = N'ค่าจดจำนอง' THEN QUPI.Amount ELSE 0 END
        , 'TransfeeFee' = CASE WHEN MPI.Detail = N'ค่าธรรมเนียมการโอน' THEN QPE.BuyerAmount ELSE 0 END
        , 'DocumentFeeCharge' = CASE WHEN MPI.Detail = N'ค่าดำเนินการเอกสาร' THEN QUPI.Amount ELSE 0 END 
        , 'WaterMeterExpense' = CASE WHEN MPI.Detail = N'มิเตอร์น้ำ' THEN QUPI.Amount ELSE 0 END 
        , 'ElectricMeterExpense' = CASE WHEN MPI.Detail = N'มิเตอร์ไฟ' THEN QUPI.Amount ELSE 0 END 
        , 'CommonFee' = CASE WHEN MPI.Detail = N'ค่าส่วนกลาง' THEN QUPI.Amount ELSE 0 END 
        , 'TotalExpenseOnTransfer' = CASE WHEN MPI.Detail = N'ค่าจดจำนอง' THEN QUPI.Amount ELSE 0 END +
                                        CASE WHEN MPI.Detail = N'ค่าธรรมเนียมการโอน' THEN QUPI.Amount ELSE 0 END +
                                        CASE WHEN MPI.Detail = N'ค่าดำเนินการเอกสาร' THEN QUPI.Amount ELSE 0 END +
                                        CASE WHEN MPI.Detail = N'มิเตอร์น้ำ' THEN QUPI.Amount ELSE 0 END +
                                        CASE WHEN MPI.Detail = N'มิเตอร์ไฟ' THEN QUPI.Amount ELSE 0 END +
                                        CASE WHEN MPI.Detail = N'ค่าส่วนกลาง' THEN QUPI.Amount ELSE 0 END 
FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [SAL].[QuotationUnitPriceItem] QUPI WITH (NOLOCK) ON QUPI.QuotationUnitPriceID = QUP.ID
    LEFT OUTER JOIN [MST].[MasterPriceItem] MPI WITH (NOLOCK) ON MPI.ID = QUPI.MasterPriceItemID
    LEFT OUTER JOIN [PRM].[QuotationPromotionExpense] QPE WITH (NOLOCK) ON QPE.MasterPriceItemID = MPI.ID
WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1

GO
