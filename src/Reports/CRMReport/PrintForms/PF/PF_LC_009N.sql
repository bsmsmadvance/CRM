SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PF_LC_009N]
    @QuotationNo  nvarchar(15)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT 'ProjectName' = P.ProjectNameTH
        , 'UnitName' = U.UnitNo
        , 'HouseModel' = M.NameTH
        , 'HouseNo' = U.HouseNo
        , 'SaleArea' = U.SaleArea
        , 'UsedArea' =  U.UsedArea
        , 'ContractWithin' = [dbo].[fnFormatDateLongTH] (CURRENT_TIMESTAMP)
        , 'InstallmentPeriod' = CASE WHEN MPI.Detail = N'เงินดาวน์' THEN QUPI.Installment ELSE 0 END
        , 'InstallmentAmount' = CASE WHEN MPI.Detail = N'เงินดาวน์' THEN QUPI.InstallmentAmount ELSE 0 END
        , 'SalePrice' = CASE WHEN MPI.Detail = N'ราคาขาย' THEN QUPI.Amount ELSE 0 END
        , 'CashDiscount' = ''
        , 'TotalSalePriice' = CASE WHEN MPI.Detail = N'ราคาขายสุทธิ' THEN QUPI.Amount ELSE 0 END
        , 'DepositAmount' = CASE WHEN MPI.Detail = N'เงินจอง' THEN QUPI.Amount ELSE 0 END
        , 'ContractAmount' = CASE WHEN MPI.Detail = N'เงินสัญญา' THEN QUPI.Amount ELSE 0 END
        , 'TotalInstallmentAmount' = CASE WHEN MPI.Detail = N'เงินดาวน์' THEN QUPI.Amount ELSE 0 END
        , 'TransferDate' = ''
        , 'TotalTransferAmount' = CASE WHEN MPI.Detail = N'ราคาขายสุทธิ' THEN QUPI.Amount ELSE 0 END -
                                    CASE WHEN MPI.Detail = N'เงินจอง' THEN QUPI.Amount ELSE 0 END -
                                    CASE WHEN MPI.Detail = N'เงินสัญญา' THEN QUPI.Amount ELSE 0 END -
                                    CASE WHEN MPI.Detail = N'เงินดาวน์' THEN QUPI.Amount ELSE 0 END
FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [SAL].[QuotationUnitPriceItem] QUPI WITH (NOLOCK) ON QUPI.QuotationUnitPriceID = QUP.ID
    LEFT OUTER JOIN [MST].[MasterPriceItem] MPI WITH (NOLOCK) ON MPI.ID = QUPI.MasterPriceItemID
    LEFT OUTER JOIN [PRJ].[Project] P WITH (NOLOCK) ON P.ID = Q.ProjectID
    LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ID = Q.UnitID
    LEFT OUTER JOIN [PRJ].[Model] M WITH (NOLOCK) ON M.ID = U.ModelID
WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1



GO
