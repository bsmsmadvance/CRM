SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[AP2SP_RP_AG_006] ','60005','08A4','ทั้งหมด','2016-01-01 00:00:00.000','2017-03-21 00:00:00.000','อริยดา ดึกดี',',',','

CREATE PROCEDURE [dbo].[AP2SP_RP_AG_006]
--DECLARE
    @CompanyID NVARCHAR(20) 
  , @ProductID NVARCHAR(15) 
  , @UnitNumber NVARCHAR(15) 
  , @StatusAG NVARCHAR(20) 
  , @DateStart DATETIME 
  , @DateEnd DATETIME 
  , @UserName NVARCHAR(50) 
  , @HomeType NVARCHAR(20) 
  , @StatusProject NVARCHAR(2) 
  , @ProjectGroup NVARCHAR(5) 
  , @ProjectType2 NVARCHAR(5) 
AS
BEGIN

    DECLARE @DateEndInStore DATETIME;
    SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd);

    IF (OBJECT_ID('tempdb..#tmpUnitNumber') IS NOT NULL)
        DROP TABLE #tmpUnitNumber;

    SELECT 'ProductID' = '' --UN.ProductID
      , 'UnitNumber' = '' --UN.UnitNumber
      , 'ContractNumber' = '' --ISNULL(AG.ContractNumber, BK.BookingNumber) AS ContractNumber
      , 'ContactID' = '' --(CASE WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL THEN BO.ContactID ELSE AO.ContactID END) AS ContactID
      , 'CustomerName' = '' /* (CASE
            WHEN BK.BookingNumber IS NOT NULL
                AND AG.ContractNumber IS NULL THEN ISNULL(BO.FirstName, '') + '  ' + ISNULL(BO.LastName, '')
        ELSE ISNULL(AO.FirstName, '') + '  ' + ISNULL(AO.LastName, '')
        END) AS CustomerName */
      , 'Mobile' = '' --CASE WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL THEN ISNULL(BO.Mobile, '')ELSE ISNULL(AO.Mobile, '')END AS Mobile
      , 'Price' = '' --CASE WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL THEN BK.TotalSellingPrice ELSE AG.TotalSellingPrice END AS Price
      , 'TransferDiscount' = '' --ISNULL(AG.TransferDiscount, BK.TransferDiscount) AS TransferDiscount
      , 'CashDiscount' = '' --ISNULL(AG.CashDiscount, BK.CashDiscount) AS CashDiscount
      , 'SpecialDiscount' = '' --ISNULL(BK.SpecialDiscount, 0) AS SpecialDiscount
      , 'ItemPrice' = '' --ISNULL(ZD.ItemPrice, 0) AS ItemPrice
      , 'PayPrice' = '' --ISNULL(ZF.PayPrice, 0) AS PayPrice
      , 'FGFDiscount' = '' --ISNULL(AG.FGFDiscount, 0) AS FGFDiscount
      , 'TFItemPrice' = '' --ISNULL(ZP.ItemPrice, 0) AS TFItemPrice
      , 'TFPayPrice' = '' --ISNULL(TF.PayPrice, 0) AS TFPayPrice
      , 'BookingDate' = '' --BK.BookingDate
      , 'ContractDueDate' = '' --AG.ContractDate
      , 'RDate' = '' --AG.ApproveDate
      , 'TransferContact' = '' --AG.TransferDate AS TransferContact
      , 'TransferDateApprove' = '' --T.TransferDateApprove
      , 'Status' = '' /*(CASE
            WHEN (BK.BookingNumber IS NULL AND AG.ContractNumber IS NULL) THEN 'ว่าง'
            WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL AND BK.CancelDate IS NULL THEN 'Active'
            WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NOT NULL AND BK.CancelDate IS NULL THEN 'Active'
            WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL AND BK.CancelDate IS NOT NULL THEN 'ยกเลิก'
            WHEN BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NOT NULL AND BK.CancelDate IS NOT NULL THEN 'ยกเลิก'
        END) AS Status */
      , 'CompanyID' = '' --CO.CompanyID
      , 'CompanyNameThai' = '' --CO.CompanyNameThai
      , 'Project' = '' --PR.Project
      , 'Cancel' = '' --ISNULL(BK.CancelDate, AG.CancelDate) AS Cancel
      , 'LC' = '' --(SELECT DisplayName FROM Users u WHERE u.UserID = ISNULL(ISNULL(T.SaleID, AG.SaleID), BK.SaleID))
    FROM [PRJ].[Unit] UN --This is main table, more table below
    /* LEFT OUTER JOIN ICON_EntForms_Booking BK ON BK.ProductID = UN.ProductID AND BK.UnitNumber = UN.UnitNumber
    LEFT OUTER JOIN ICON_EntForms_BookingOwner BO ON BO.BookingNumber = BK.BookingNumber AND BO.Header = 1 AND ISNULL(BO.IsDelete, 0) = 0
    LEFT OUTER JOIN ICON_EntForms_Agreement AG ON AG.BookingNumber = BK.BookingNumber
    LEFT OUTER JOIN ICON_EntForms_AgreementOwner AO ON AO.ContractNumber = AG.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete, 0) = 0
    LEFT OUTER JOIN ICON_EntForms_Transfer T ON T.ContractNumber = AG.ContractNumber
    LEFT OUTER JOIN ZPROM_TransferPromotion TP ON TP.ContractNumber = AG.ContractNumber AND ISNULL(TP.IsCancel, 0) = 0
    LEFT OUTER JOIN (   SELECT ZS.DocumentID
                          , SUM(ZD.PricePerUnit * ZS.Amount) AS ItemPrice
                        FROM ZPROM_SalePromotionDetail ZS
                        LEFT OUTER JOIN ZPROM_PromotionDetail ZD ON ZD.PromotionID = ZS.PromotionID AND ZD.ItemID = ZS.ItemID
                        GROUP BY ZS.DocumentID) ZD ON ZD.DocumentID = CASE WHEN AG.ContractNumber IS NULL THEN BK.BookingNumber ELSE AG.ContractNumber END
    LEFT OUTER JOIN (   SELECT DocumentID
                          , SUM(CASE PromotionFeeID WHEN '15' THEN Amount / 2 ELSE CASE WHEN Charge = 'H' THEN Amount / 2 ELSE Amount END END) AS PayPrice
                        FROM ZPROM_SalePromotionFee
                        WHERE ((PromotionFeeID = '15' AND Charge = 'N') OR (PromotionFeeID IN ('00', '01', '02', '17', '2G', '37', '000') AND (Charge = 'N' OR Charge = 'H')))
                        GROUP BY DocumentID) ZF ON ZF.DocumentID = CASE WHEN AG.ContractNumber IS NULL THEN BK.BookingNumber ELSE AG.ContractNumber END
    LEFT OUTER JOIN (   SELECT ZP.TransferPromotionID
                          , SUM(ZD.PricePerUnit * ZP.Amount) AS ItemPrice
                        FROM ZPROM_TransferPromotionDetail ZP
                        LEFT OUTER JOIN ZPROM_PromotionDetail ZD ON ZD.PromotionID = ZP.PromotionID AND ZD.ItemID = ZP.ItemID
                        WHERE ISNULL(ZP.IsSelected, 0) = 1
                        GROUP BY ZP.TransferPromotionID) ZP ON ZP.TransferPromotionID = TP.TransferPromotionID
    LEFT OUTER JOIN (   SELECT TransferPromotionID
                          , SUM(CASE PromotionFeeID WHEN '15' THEN Amount / 2 ELSE Amount END) AS PayPrice
                        FROM ZPROM_TransferPromotionFee
                        WHERE ((PromotionFeeID = '15' AND Charge = 'N') OR (PromotionFeeID IN ('00', '01', '02', '17', '2G', '37') AND (Charge = 'N' OR Charge = 'H')))
                        GROUP BY TransferPromotionID) TF ON TF.TransferPromotionID = TP.TransferPromotionID
    LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = UN.ProductID
    LEFT OUTER JOIN [ICON_EntForms_Company] CO ON CO.CompanyID = PR.CompanyID
    WHERE 1 = 1
        AND UN.AssetType IN (2, 4)
        AND (PR.CompanyID = @CompanyID OR ISNULL(@CompanyID, '') = '')
        AND (UN.ProductID = @ProductID OR ISNULL(@ProductID, '') = '')
        AND (UN.UnitNumber = @UnitNumber OR ISNULL(@UnitNumber, '') = '')
        AND (   ISNULL(@StatusAG, 0) <> '1'
                OR (BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL AND BK.CancelDate IS NULL)
                OR (BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NOT NULL AND BK.CancelDate IS NULL))
        AND (   ISNULL(@StatusAG, 0) <> '2'
                OR ((BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NULL AND BK.CancelDate IS NOT NULL)
                       OR (BK.BookingNumber IS NOT NULL AND AG.ContractNumber IS NOT NULL AND BK.CancelDate IS NOT NULL)))
        AND (   ((YEAR(@DateStart) = 1800 OR ISNULL(@DateStart, '') = '') AND (YEAR(@DateEnd) = 7000 OR ISNULL(@DateEnd, '') = ''))
                OR ((BK.BookingDate BETWEEN CONVERT(NVARCHAR(50), @DateStart, 120) AND CONVERT(NVARCHAR(50), @DateEndInStore, 120)) OR BK.BookingDate IS NULL))
        AND (   ( (YEAR(@DateStart) <> 1800 OR ISNULL(@DateStart, '') = '') AND (YEAR(@DateEnd) = 7000) OR ISNULL(@DateEnd, '') = '')
                OR ((BK.BookingDate <= '' + CONVERT(NVARCHAR(50), @DateEndInStore, 120) + '') OR BK.BookingDate IS NULL))
        AND (PR.PType = @HomeType OR ISNULL(@HomeType, '') = '' OR ISNULL(@HomeType, '') = 'ทั้งหมด')
        AND (PR.RTPExcusive = @StatusProject OR @StatusProject = '4' OR ISNULL(@StatusProject, '') = '')
        AND (PR.RTPExcusive IN (1, 2) OR @StatusProject <> '4' OR ISNULL(@StatusProject, '') = '')
        AND (PR.ProjectGroup = @ProjectGroup OR ISNULL(@ProjectGroup, '') = '')
        AND (PR.ProjectType = @ProjectType2 OR ISNULL(@ProjectType2, '') = '') */

    UNION ALL
    SELECT 'ProductID' = '' --PR.ProductID
      , 'UnitNumber' = '' --UnitNumber
      , 'ContractNumber' = NULL
      , 'ContactID' = NULL
      , 'CustomerName' = NULL
      , 'Mobile' = NULL
      , 'Price' = NULL
      , 'TransferDiscount' = NULL
      , 'CashDiscount' = NULL
      , 'SpecialDiscount' = NULL
      , 'ItemPrice' = NULL
      , 'PayPrice' = NULL
      , 'FGFDiscount' = NULL
      , 'TFItemPrice' = NULL
      , 'TFPayPrice' = NULL
      , 'BookingDate' = NULL
      , 'ContractDueDate' = NULL
      , 'RDate' = NULL
      , 'TransferContact' = NULL
      , 'TransferDateApprove' = NULL
      , 'Status' = 'ว่าง'
      , 'CompanyID' = NULL
      , 'CompanyNameThai' = NULL
      , 'Project' = '' --PR.Project
      , 'Cancel' = NULL
      , 'LC' = NULL
    FROM [PRJ].[Unit] UN
    LEFT OUTER JOIN [PRJ].[Project] PR ON PR.ID = UN.ProjectID
    WHERE 1 = 1
        --AND UN.UnitStatus = 1
        --AND @StatusAG = 'ทั้งหมด'
        --AND UN.AssetType IN (2, 4)
        --AND (UN.ProductID = @ProductID OR ISNULL(@ProductID, '') = '')
        --AND (UN.UnitNumber = @UnitNumber OR ISNULL(@UnitNumber, '') = '')
        --AND (PR.PType = @HomeType OR ISNULL(@HomeType, '') = '' OR ISNULL(@HomeType, '') = 'ทั้งหมด')
        --AND (PR.RTPExcusive = @StatusProject OR @StatusProject = '4' OR ISNULL(@StatusProject, '') = '')
        --AND (PR.RTPExcusive IN (1, 2) OR @StatusProject <> '4' OR ISNULL(@StatusProject, '') = '')
        --AND (PR.ProjectGroup = @ProjectGroup OR ISNULL(@ProjectGroup, '') = '')
        --AND (PR.ProjectType = @ProjectType2 OR ISNULL(@ProjectType2, '') = '')
    --ORDER BY UN.ProjectID
    --  , UN.UnitNo
    --  , Status;

END;

GO
