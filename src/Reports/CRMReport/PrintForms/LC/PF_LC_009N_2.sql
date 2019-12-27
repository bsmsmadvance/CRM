SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PF_LC_009N_2]
    @QuotationNo  nvarchar(16)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT 'MaterialName' = MBPI.MaterialName
FROM [SAL].[Quotation] Q WITH (NOLOCK)
    LEFT OUTER JOIN [SAL].[QuotationUnitPrice] QUP WITH (NOLOCK) ON QUP.QuotationID = Q.ID
    LEFT OUTER JOIN [PRM].[QuotationBookingPromotion] QBP WITH (NOLOCK) ON QBP.QuotationID = Q.ID
    LEFT OUTER JOIN [PRM].[QuotationBookingPromotionItem] QBPI WITH (NOLOCK) ON QBPI.QuotationBookingPromotionID = QBP.ID
    LEFT OUTER JOIN [PRM].[MasterBookingPromotionItem] MBPI WITH (NOLOCK) ON MBPI.ID = QBPI.MasterBookingPromotionItemID
WHERE Q.QuotationNo = @QuotationNo AND QUP.IsActive = 1



GO
