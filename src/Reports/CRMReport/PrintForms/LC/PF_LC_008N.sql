SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PF_LC_008N]
    @QuotationNo  nvarchar(16)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @QUPI Table(Name VARCHAR(20), Amount money, Installment nvarchar(2), InstallmentAmount money)
INSERT INTO @QUPI(Name, Amount, Installment, InstallmentAmount)
	SELECT	'Name' = QUPI.Name 
            , 'Amount' = QUPI.Amount
            , 'Installment' = QUPI.Installment
            , 'InstallmentAmount' = QUPI.InstallmentAmount
	FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [SAL].[QuotationUnitPriceItem] QUPI WITH (NOLOCK) ON QUPI.QuotationUnitPriceID = QUP.ID
    WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1


SELECT 'ProjectName' = P.ProjectNameTH
        , 'UnitName' = U.UnitNo
        , 'HouseModel' = M.NameTH
        , 'HouseNo' = U.HouseNo
        , 'SaleArea' = U.SaleArea
        , 'UsedArea' =  U.UsedArea
        , 'ContractWithin' = [dbo].[fnFormatDateLongTH] (CURRENT_TIMESTAMP + 7)
        , 'InstallmentPeriod' = ISNULL((SELECT Installment FROM @QUPI WHERE Name = N'เงินดาวน์'),0) 
        , 'InstallmentAmount' = ISNULL((SELECT InstallmentAmount FROM @QUPI WHERE Name = N'เงินดาวน์'),0) 
        , 'SalePrice' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ราคาขาย'),0)
        , 'CashDiscount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ส่วนลดเงินสด '),0)
        , 'TotalSalePrice' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ราคาขายสุทธิ '),0) 
        , 'DepositAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินจอง'),0)
        , 'ContractAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินสัญญา'),0) 
        , 'TotalInstallmentAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินดาวน์') * (SELECT InstallmentAmount FROM @QUPI WHERE Name = N'เงินดาวน์'),0) 
        , 'TransferDate' = ISNULL([dbo].[fnFormatDateLongTH] (Q.TransferOwnershipDate) , null)
        , 'TotalTransferAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ราคาขายสุทธิ '),0) -
                                    ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินจอง'),0) -
                                    ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินสัญญา'),0) -
                                    ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินดาวน์') * (SELECT InstallmentAmount FROM @QUPI WHERE Name = N'เงินดาวน์'),0)
FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [PRJ].[Project] P WITH (NOLOCK) ON P.ID = Q.ProjectID
    LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ID = Q.UnitID
    LEFT OUTER JOIN [PRJ].[Model] M WITH (NOLOCK) ON M.ID = U.ModelID
WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1

GO
