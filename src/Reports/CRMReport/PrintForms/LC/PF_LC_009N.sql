SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PF_LC_009N]
    @QuotationNo  nvarchar(16)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @QUPI Table(Name VARCHAR(50), Amount money, Installment nvarchar(10), InstallmentAmount money, SpecialInstallments nvarchar(50))
INSERT INTO @QUPI(Name, Amount, Installment, InstallmentAmount, SpecialInstallments)
	SELECT	'Name' = QUPI.Name 
            , 'Amount' = QUPI.Amount
            , 'Installment' = QUPI.Installment
            , 'InstallmentAmount' = QUPI.InstallmentAmount
            , 'SpecialInstallments' = QUPI.SpecialInstallments
	FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [SAL].[QuotationUnitPriceItem] QUPI WITH (NOLOCK) ON QUPI.QuotationUnitPriceID = QUP.ID
    WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1


SELECT 'ProjectName' = P.ProjectNameTH
        , 'UnitName' = U.UnitNo
        , 'TowerName' = T.TowerCode
        , 'FloorName' = F.NameTH
        , 'SaleArea' = U.SaleArea
        , 'ModelName' = M.NameTH
        , 'TypeOfRealEstate' = RE.Name
        , 'ProjectNo' = P.ProjectNo
        , 'FloorPlanName' = ISNULL(U.FloorPlanFileName, '')
        , 'FloorPlan' = ISNULL('C:\\crm\\' + P.ProjectNo + '\\' + @QuotationNo + '\\' +  U.FloorPlanFileName, '')
        , 'RoomPlanName' = ISNULL(U.RoomPlanFileName, '')
        , 'RoomPlan' = ISNULL('C:\\crm\\' + P.ProjectNo + '\\' + @QuotationNo + '\\' +  U.RoomPlanFileName, '')
        , 'TotalInstallmentPeriod' = CONVERT(int,ISNULL((SELECT Installment FROM @QUPI WHERE Name = N'เงินดาวน์'),0))
        , 'InstallmentPeriod' = ISNULL([dbo].[fn_GetNormalInstallmentCount] (ISNULL((SELECT Installment FROM @QUPI WHERE Name = N'เงินดาวน์'),ISNULL([dbo].[fn_GetSpecialInstallmentCount] (ISNULL((SELECT SpecialInstallments FROM @QUPI WHERE Name = N'เงินดาวน์'),0)), 0)),0),0)
        , 'SpecialInstallmentPeriod' = ISNULL([dbo].[fn_GetSpecialInstallmentCount] (ISNULL((SELECT SpecialInstallments FROM @QUPI WHERE Name = N'เงินดาวน์'),NULL)), 0)
        , 'InstallmentAmount' = ISNULL((SELECT InstallmentAmount FROM @QUPI WHERE Name = N'เงินดาวน์'),0) 
        , 'SpecialInstallmentAmount' = ISNULL([dbo].[fn_GetSpecialInstallmentAmount] (ISNULL((SELECT SpecialInstallments FROM @QUPI WHERE Name = N'เงินดาวน์'),0)), 0)
        , 'SalePrice' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ราคาขาย'),0)
        , 'CashDiscount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ส่วนลดเงินสด '),0)
        , 'TotalSalePrice' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ราคาขายสุทธิ '),0) 
        , 'PricePerSquareMeter' = ''
        , 'DepositAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินจอง'),0)
        , 'ContractAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินสัญญา'),0) 
        , 'TotalInstallmentAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินดาวน์') * (SELECT InstallmentAmount FROM @QUPI WHERE Name = N'เงินดาวน์'),0) 
        , 'TotalTransferAmount' = ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'ราคาขายสุทธิ '),0) -
                                    ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินจอง'),0) -
                                    ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินสัญญา'),0) -
                                    ISNULL((SELECT Amount FROM @QUPI WHERE Name = N'เงินดาวน์') * (SELECT InstallmentAmount FROM @QUPI WHERE Name = N'เงินดาวน์'),0)
FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [PRJ].[Project] P WITH (NOLOCK) ON P.ID = Q.ProjectID
    LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ID = Q.UnitID
    LEFT OUTER JOIN [PRJ].[Tower] T WITH (NOLOCK) ON T.ID = U.TowerID
    LEFT OUTER JOIN [PRJ].[Floor] F WITH (NOLOCK) ON F.ID = U.FloorID
    LEFT OUTER JOIN [PRJ].[Model] M WITH (NOLOCK) ON M.ID = U.ModelID
    LEFT OUTER JOIN [MST].[TypeOfRealEstate] RE WITH (NOLOCK) ON RE.ID = M.TypeOfRealEstateID
WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1






GO
