    SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_RP_AR_007] '','','','',NULL,'''''','1'
--  exec "AP2SP_RP_AR_007";1 N'', N'20008', {ts '1800-01-01 00:00:00'}, {ts '2018-09-30 00:00:00'}, N'อริยดา ตึกดี', N'''a20''','0'

ALTER PROC [dbo].[AP2SP_RP_AR_007]
   -- DECLARE
    @CompanyID NVARCHAR(50)
  , @ProductID NVARCHAR(50)
  , @DateStart DATETIME
  , @DateEnd DATETIME
  , @UserName NVARCHAR(150)
  , @UnitNumber NVARCHAR(4000)
  , @PercentPayment NVARCHAR(5)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (ISNULL(@DateStart, '') = '')
        SET @DateStart = '18000101';
    IF (ISNULL(@DateEnd, '') = '')
        SET @DateEnd = '70001231';

    IF (ISNULL(@PercentPayment, '') = '')
        SET @PercentPayment = '0';

    SET @CompanyID = ISNULL(@CompanyID, '');
    SET @ProductID = ISNULL(@ProductID, '');

    SET @UnitNumber = REPLACE(ISNULL(@UnitNumber, ''), '''', '');

    DECLARE @DateEndInStore DATETIME;

    SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd);

    IF (ISNULL(@UnitNumber, '') = 'ทั้งหมด')
        SET @UnitNumber = '';

	/* IF OBJECT_ID('tempdb..#tmpUnitNumber') IS NOT NULL
	DROP TABLE #tmpUnitNumber;
	SELECT Val AS UnitNumber INTO #tmpUnitNumber FROM dbo.fn_SplitString(@UnitNumber, ',');

	IF OBJECT_ID('tempdb..#tmpICON_Payment_PaymentDetails') IS NOT NULL  DROP TABLE  #tmpICON_Payment_PaymentDetails;
	SELECT * INTO  #tmpICON_Payment_PaymentDetails FROM  ICON_Payment_PaymentDetails WITH (NOLOCK)
	WHERE (@ProductID = '' OR ReferentID LIKE @ProductID + '%')

	IF OBJECT_ID('tempdb..#tmpICON_Payment_TmpReceipt') IS NOT NULL  DROP TABLE  #tmpICON_Payment_TmpReceipt;
	SELECT * INTO  #tmpICON_Payment_TmpReceipt FROM  ICON_Payment_TmpReceipt WITH (NOLOCK)
	WHERE (@ProductID = '' OR ProductID = @ProductID)


    PRINT @DateEndInStore;
    PRINT @UnitNumber;

    DECLARE @B TABLE (BookingAmount MONEY, ReferentID NVARCHAR(50));
    INSERT INTO @B
    SELECT SUM(Pay.Amount) AS BookingAmount
      , Pay.ReferentID
    FROM (   SELECT DISTINCT PD.Amount
               , PD.PaymentType
               , PD.Period
               , PD.RCReferent
               , PD.ReferentID
               , PD.TmpReceiptID
             FROM  #tmpICON_Payment_PaymentDetails PD 
             LEFT OUTER JOIN #tmpICON_Payment_TmpReceipt TR  ON PD.RCReferent = TR.RCReferent
                                                                          AND PD.TmpReceiptID = TR.TmpReceiptID
                                                                          AND PD.PaymentType = '4'
             LEFT OUTER JOIN ICON_EntForms_Booking B WITH (NOLOCK) ON PD.ReferentID = B.BookingNumber
             LEFT JOIN Z_BudgetApprove z WITH (NOLOCK) ON PD.ReferentID = z.DocumentNumber
                                                           AND z.Status = 'inprocess'
                                                           AND z.DocumentCancel = 0
             WHERE PD.PaymentType = '4'
                 AND TR.CancelDate IS NULL
                 AND (B.CancelDate IS NULL OR B.CancelDate > @DateEndInStore)
                 AND TR.RDate BETWEEN @DateStart AND @DateEndInStore
                 AND (ISNULL(@ProductID, '') = '' OR @ProductID = TR.ProductID)
	) AS Pay
    GROUP BY Pay.ReferentID;


    DECLARE @AGT TABLE (TotalAmount MONEY, ReferentID NVARCHAR(50));
    INSERT INTO @AGT
    SELECT SUM(Pay.Amount) AS TotalAmount
      , Pay.ReferentID
    FROM (   SELECT PD.Amount
               , PD.PaymentType
               , PD.Period
               , PD.RCReferent
               , PD.ReferentID
             FROM  #tmpICON_Payment_PaymentDetails PD WITH (NOLOCK)
             LEFT OUTER JOIN #tmpICON_Payment_TmpReceipt TR WITH (NOLOCK) ON PD.RCReferent = TR.RCReferent
                                                                          AND PD.TmpReceiptID = TR.TmpReceiptID
             LEFT OUTER JOIN ICON_EntForms_Agreement A WITH (NOLOCK) ON PD.ReferentID = A.ContractNumber
             INNER JOIN ICON_EntForms_PaymentDetail PS WITH (NOLOCK) ON PS.ServiceCode = PD.PaymentType
                                                                         AND ISNULL(PS.Payment, 0) IN ('0', '4', '6')
                                                                         AND PD.PaymentType <> '4'
             WHERE PD.PaymentType NOT LIKE 'TR%'
                 AND PD.PaymentType NOT IN ('A08')
                 AND TR.CancelDate IS NULL 
                 AND (A.CancelDate IS NULL OR A.CancelDate > @DateEndInStore)
                 AND ((TR.Method NOT IN ('5')) OR (TR.Method IN ('5') AND (TR.DepositID IS NOT NULL OR TR.ReconcileDate IS NOT NULL)))
                 AND TR.RDate BETWEEN @DateStart AND @DateEndInStore
                 AND (ISNULL(@ProductID, '') = '' OR @ProductID = TR.ProductID)
	) AS Pay
    GROUP BY Pay.ReferentID;


	 IF OBJECT_ID('tempdb..#tmpTR3_1') IS NOT NULL DROP TABLE #tmpTR3_1
	 SELECT PD.Amount
               , PD.PaymentType
               , PD.Period
               , PD.RCReferent
               , PD.ReferentID
		INTO #tmpTR3_1
             FROM  #tmpICON_Payment_PaymentDetails PD WITH (NOLOCK)
             LEFT OUTER JOIN #tmpICON_Payment_TmpReceipt TR WITH (NOLOCK) ON PD.RCReferent = TR.RCReferent
                                                                          AND PD.TmpReceiptID = TR.TmpReceiptID
                                                                          AND PD.PaymentType IN ('5', '6')
             INNER JOIN ICON_EntForms_Agreement A WITH (NOLOCK) ON PD.ReferentID = A.ContractNumber
             WHERE PD.PaymentType IN ('5', '6')
                 AND TR.CancelDate IS NULL
                 AND ((TR.Method NOT IN ('5')) OR (TR.Method IN ('5') AND (TR.DepositID IS NOT NULL OR TR.ReconcileDate IS NOT NULL))) --AND ISNULL(TR.TransferPaymentType,0)<>'3' --แก้ไขกรณีรับเงินก่อนทำสัญญาแล้วไม่แสดงยอดเงิน 10150/D06
                 AND (A.CancelDate IS NULL OR A.CancelDate > @DateEndInStore)
                 AND TR.RDate BETWEEN @DateStart AND @DateEndInStore
                 AND (ISNULL(@ProductID, '') = '' OR @ProductID = TR.ProductID)
             
	IF OBJECT_ID('tempdb..#tmpTR3_2') IS NOT NULL DROP TABLE #tmpTR3_2
	SELECT PD.Amount
               , PD.PaymentType
               , PD.Period
               , PD.RCReferent
               , PD.ReferentID
			INTO #tmpTR3_2
             FROM  #tmpICON_Payment_PaymentDetails PD WITH (NOLOCK)
             LEFT OUTER JOIN #tmpICON_Payment_TmpReceipt TR WITH (NOLOCK) ON PD.RCReferent = TR.RCReferent
                                                                          AND PD.TmpReceiptID = TR.TmpReceiptID
                                                                          AND PD.PaymentType IN ('5', '6')
             INNER JOIN ICON_EntForms_Booking B WITH (NOLOCK) ON PD.ReferentID = B.BookingNumber
             WHERE PD.PaymentType IN ('5', '6')
                 AND TR.CancelDate IS NULL
                 AND ((TR.Method NOT IN ('5')) OR (TR.Method IN ('5') AND (TR.DepositID IS NOT NULL OR TR.ReconcileDate IS NOT NULL)))
                 AND ISNULL(TR.TransferPaymentType, 0) = '3'
                 AND (B.CancelDate IS NULL OR B.CancelDate > @DateEndInStore)
                 AND TR.RDate BETWEEN @DateStart AND @DateEndInStore
                 AND (ISNULL(@ProductID, '') = '' OR @ProductID = TR.ProductID)
    
    DECLARE @TR3 TABLE (DownAGAmount MONEY, ReferentID NVARCHAR(50));
    INSERT INTO @TR3
    SELECT SUM(Pay.Amount) AS DownAGAmount
      , Pay.ReferentID
    FROM (  SELECT * FROM #tmpTR3_1
             UNION ALL
             SELECT * FROM #tmpTR3_2
    ) AS Pay
    GROUP BY Pay.ReferentID;

	IF OBJECT_ID('tempdb..#tmpTR4_1') IS NOT NULL DROP TABLE #tmpTR4_1
	SELECT PD.Amount
               , PD.PaymentType
               , PD.Period
               , PD.RCReferent
               , PD.ReferentID
		INTO #tmpTR4_1
             FROM  #tmpICON_Payment_PaymentDetails PD WITH (NOLOCK)
             LEFT OUTER JOIN #tmpICON_Payment_TmpReceipt TR WITH (NOLOCK) ON PD.RCReferent = TR.RCReferent
                                                                          AND PD.TmpReceiptID = TR.TmpReceiptID
             LEFT OUTER JOIN ICON_EntForms_Agreement A WITH (NOLOCK) ON PD.ReferentID = A.ContractNumber
             LEFT OUTER JOIN ICON_EntForms_PaymentDetail PS WITH (NOLOCK) ON PS.ServiceCode = PD.PaymentType
             WHERE ((PD.PaymentType = '8' AND ISNULL(PS.Payment, 0) = 0) OR ISNULL(PS.Payment, 0) IN ('4', '6'))
                 AND TR.CancelDate IS NULL
                 AND (A.CancelDate IS NULL OR A.CancelDate > @DateEndInStore)
                 AND TR.RDate BETWEEN @DateStart AND @DateEndInStore
                 AND (ISNULL(@ProductID, '') = '' OR @ProductID = TR.ProductID)
    
	IF OBJECT_ID('tempdb..#tmpTR4_2') IS NOT NULL DROP TABLE #tmpTR4_2
	 SELECT PD.Amount
               , PD.PaymentType
               , PD.Period
               , PD.RCReferent
               , PD.ReferentID
		INTO #tmpTR4_2
             FROM  #tmpICON_Payment_PaymentDetails PD
             LEFT OUTER JOIN #tmpICON_Payment_TmpReceipt TR ON PD.RCReferent = TR.RCReferent
                                                            AND PD.TmpReceiptID = TR.TmpReceiptID
             LEFT OUTER JOIN ICON_EntForms_Booking B ON PD.ReferentID = B.BookingNumber
             LEFT OUTER JOIN ICON_EntForms_PaymentDetail PS ON PS.ServiceCode = PD.PaymentType
             WHERE ((PD.PaymentType = '8' AND ISNULL(PS.Payment, 0) = 0) OR ISNULL(PS.Payment, 0) IN ('4', '6'))
                 AND ISNULL(TR.TransferPaymentType, 0) = '3'
                 AND TR.CancelDate IS NULL
                 AND (B.CancelDate IS NULL OR B.CancelDate > @DateEndInStore)
                 AND TR.RDate BETWEEN @DateStart AND @DateEndInStore
                 AND (ISNULL(@ProductID, '') = '' OR @ProductID = TR.ProductID)
    
    DECLARE @TR4 TABLE (TransferAmount MONEY, ReferentID NVARCHAR(50));
    INSERT INTO @TR4
    SELECT SUM(Pay.Amount) AS TransferAmount
      , Pay.ReferentID
    FROM (    SELECT * FROM #tmpTR4_1
             UNION ALL
             SELECT * FROM #tmpTR4_2
    ) AS Pay
    GROUP BY Pay.ReferentID;

IF OBJECT_ID('tempdb..#tmpTR5_1') IS NOT NULL DROP TABLE #tmpTR5_1
	SELECT PD.Amount
               , PD.PaymentType
               , PD.Period
               , PD.RCReferent
               , PD.ReferentID
		INTO #tmpTR5_1
             FROM  #tmpICON_Payment_PaymentDetails PD 
             LEFT OUTER JOIN #tmpICON_Payment_TmpReceipt TR  ON PD.RCReferent = TR.RCReferent
                                                                          AND PD.TmpReceiptID = TR.TmpReceiptID
             LEFT OUTER JOIN ICON_EntForms_Agreement A WITH (NOLOCK) ON PD.ReferentID = A.ContractNumber
             WHERE (PD.PaymentType = '50' AND TR.CancelDate IS NULL)
                 AND ((TR.Method NOT IN ('5')) OR (TR.Method IN ('5') AND (TR.DepositID IS NOT NULL OR TR.ReconcileDate IS NOT NULL))) --TR.Method Not IN('5')
                 AND (A.CancelDate IS NULL OR A.CancelDate > @DateEndInStore)
                 AND TR.RDate BETWEEN @DateStart AND @DateEndInStore
                 AND (ISNULL(@ProductID, '') = '' OR @ProductID = TR.ProductID)
    
    DECLARE @TR5 TABLE (HomefurniAmt MONEY, ReferentID NVARCHAR(50));
    INSERT INTO @TR5
    SELECT SUM(Pay.Amount) AS HomefurniAmt
      , Pay.ReferentID
    FROM (   SELECT * FROM #tmpTR5_1
    ) AS Pay
    GROUP BY Pay.ReferentID;

    IF (OBJECT_ID('tempdb..#tmpAP1') IS NOT NULL)
        DROP TABLE #tmpAP1;

    SELECT SUM(PayableAmount) AS PayableAmount
      , ContractNumber
    INTO #tmpAP1
    FROM [ICON_EntForms_AgreementPeriod] WITH (NOLOCK)
    WHERE PaymentType NOT LIKE 'TR%'
        AND PaymentType NOT IN ('A08', '72')
    GROUP BY ContractNumber;

    IF (OBJECT_ID('tempdb..#tmpAP2') IS NOT NULL)
        DROP TABLE #tmpAP2;
    SELECT A1.UnitNumber
      , A1.ProductID
      , A1.TotalSellingPrice
    INTO #tmpAP2
    FROM ICON_EntForms_UnitPriceList AS A1 WITH (NOLOCK)
    INNER JOIN (   SELECT ProductID
                     , UnitNumber
                     , MAX(ActiveDate) AS MaxDate
                   FROM [ICON_EntForms_UnitPriceList] WITH (NOLOCK)
                   WHERE ActiveDate <= @DateEndInStore
                   GROUP BY ProductID
                     , UnitNumber) AS A2 ON A1.ProductID = A2.ProductID
                                             AND A1.UnitNumber = A2.UnitNumber
                                             AND A1.ActiveDate = A2.MaxDate
    WHERE 1 = 1
        AND (ISNULL(@ProductID, '') = '' OR @ProductID = A1.ProductID);

    IF (OBJECT_ID('tempdb..#tmpBook') IS NOT NULL)
        DROP TABLE #tmpBook;
    SELECT BK.ProductID
      , BK.UnitNumber
      , BK.BookingNumber
      , BK.SellingPrice
      , BK.TransferDiscount
      , BK.BookingDate
      , BK.CreateDate
      , BO.NamesTitle
      , BO.FirstName
      , BO.LastName
    INTO #tmpBook
    FROM [ICON_EntForms_Booking] BK WITH (NOLOCK)
    LEFT OUTER JOIN [ICON_EntForms_BookingOwner] BO WITH (NOLOCK) ON BO.BookingNumber = BK.BookingNumber
                                                                      AND BO.Header = 1
                                                                      AND ISNULL(BO.IsDelete, 0) = 0
                                                                      AND ISNULL(BO.IsBooking, 0) = 1
    WHERE 1 = 1
        AND (ISNULL(@ProductID, '') = '' OR @ProductID = BK.ProductID)


    IF (OBJECT_ID('tempdb..#tmpAgree') IS NOT NULL)
        DROP TABLE #tmpAgree;
    SELECT AG.ProductID
      , AG.UnitNumber
      , AG.BookingNumber
      , AG.ContractNumber
      , AG.SellingPrice
      , AG.TransferDiscount
      , AG.isApPay
      , SpacialDiscount
      , AO.NamesTitle
      , AO.FirstName
      , AO.LastName
      , NamesTitleExt
    INTO #tmpAgree
    FROM [ICON_EntForms_Agreement] AG WITH (NOLOCK)
    LEFT OUTER JOIN [ICON_EntForms_AgreementOwner] AO WITH (NOLOCK) ON AO.ContractNumber = AG.ContractNumber AND AO.Header = 1 AND ISNULL(AO.IsDelete, 0) = 0 AND (AG.CancelDate IS NULL OR AG.CancelDate > @DateEndInStore)
    WHERE 1 = 1
	AND ag.BookingNumber IN (SELECT BookingNumber  FROM #tmpBook)
        AND (ISNULL(@ProductID, '') = '' OR @ProductID = AG.ProductID);

    IF (OBJECT_ID('tempdb..#tmpTrans') IS NOT NULL)
        DROP TABLE #tmpTrans;
    SELECT TF.ContractNumber
      , TF.TransferNumber
      , TransferDateApprove
      , TF.ExtraDiscount
      , TF.ExtraPayment
      , TF.IncreasingAreaPrice
      , TF.PhusaDiscount
      , LandSize
      , TFO.NamesTitle
      , TFO.FirstName
      , TFO.LastName
      , NamesTitleExt
    INTO #tmpTrans
    FROM [ICON_EntForms_Transfer] TF WITH (NOLOCK)
    LEFT OUTER JOIN [ICON_EntForms_TransferOwner] TFO WITH (NOLOCK) ON TFO.TransferNumber = TF.TransferNumber AND TFO.ID = 1 AND ISNULL(TFO.IsDelete, 0) = 0    
	WHERE 1 = 1
	AND tf.ContractNumber IN (SELECT ContractNumber FROM #tmpAgree); */ 

    IF (OBJECT_ID('tempdb..#tmpResult1') IS NOT NULL)
        DROP TABLE #tmpResult1;

    SELECT DISTINCT 'CompanyName' = '' --CO.CompanyNameThai
      , 'ProJectName' = '' --PR.Project
      , 'ProductID' = '' --UN.ProductID
      , 'UnitNUmber' = '' --UN.UnitNumber
      , 'TransferDate' = '' --TF.TransferDateApprove
      , 'CustomerName' = '' /* CASE
                             WHEN bk.BookingNumber IS NOT NULL
                                 AND ag.ContractNumber IS NULL THEN ISNULL(bk.NamesTitle, '') + ISNULL(bk.FirstName, '') + ' ' + ISNULL(bk.LastName, '')
                             WHEN bk.BookingNumber IS NOT NULL
                                 AND ag.ContractNumber IS NOT NULL THEN ISNULL(ISNULL(ag.NamesTitleExt, ag.NamesTitle), '') + ISNULL(ag.FirstName, '') + ' ' + ISNULL(ag.LastName, '')
                             WHEN bk.BookingNumber IS NOT NULL
                                 AND ag.ContractNumber IS NOT NULL
                                 AND TF.TransferNumber IS NULL THEN ISNULL(ISNULL(ag.NamesTitleExt, ag.NamesTitle), '') + ISNULL(ag.FirstName, '') + ' ' + ISNULL(ag.LastName, '')
                             WHEN bk.BookingNumber IS NOT NULL
                                 AND ag.ContractNumber IS NOT NULL
                                 AND TF.TransferNumber IS NOT NULL THEN ISNULL(ISNULL(TF.NamesTitleExt, TF.NamesTitle), '') + ISNULL(TF.FirstName, '') + ' ' + ISNULL(TF.LastName, '')
                         END */
      , 'TitleDeedNumber' = '' --[dbo].[fnGenUnitTitledeedNumber](UN.ProductID, UN.UnitNumber)
      , 'LandSize' = '' --ISNULL(TF.LandSize, ISNULL(UN.AreaFromPFB, UN.AreaFromRE)) -- ตรวจอีกที StandardArea
      , 'TotalSellingPrice' = '' /* CASE
                                  WHEN bk.BookingNumber IS NOT NULL
                                      AND ag.ContractNumber IS NULL THEN ISNULL(bk.SellingPrice, 0) --ราคาบ้าน
                              ELSE ISNULL(ag.SellingPrice, 0)
                              END */
      , 'PromotionAndOther' = '' /* CASE
                                  WHEN bk.BookingNumber IS NOT NULL
                                      AND ag.ContractNumber IS NULL THEN ISNULL(bk.TransferDiscount, 0) --ต่อเติม/ยกเลิก/ส่วนลด
                                  WHEN bk.BookingNumber IS NOT NULL
                                      AND ag.ContractNumber IS NOT NULL
                                      AND TF.TransferNumber IS NULL THEN ISNULL(ag.TransferDiscount, 0)
                              ELSE CASE
                                       WHEN TF.ExtraDiscount = 0
                                           AND TF.ExtraPayment = 0 THEN CASE
                                                                            WHEN TF.IncreasingAreaPrice = 0 THEN TF.PhusaDiscount
                                                                            WHEN TF.IncreasingAreaPrice > 0 THEN CASE WHEN TF.PhusaDiscount > TF.IncreasingAreaPrice THEN TF.PhusaDiscount - TF.IncreasingAreaPrice ELSE TF.IncreasingAreaPrice - TF.PhusaDiscount END
                                                                            WHEN TF.IncreasingAreaPrice < 0 THEN CASE WHEN TF.PhusaDiscount > TF.IncreasingAreaPrice THEN TF.PhusaDiscount - TF.IncreasingAreaPrice ELSE TF.IncreasingAreaPrice - TF.PhusaDiscount END
                                                                        END
                                       WHEN TF.ExtraDiscount = 0
                                           AND TF.ExtraPayment > 0 THEN CASE
                                                                            WHEN TF.IncreasingAreaPrice = 0 THEN --PhusaDiscount-ExtraPayment
                                                                                CASE WHEN TF.PhusaDiscount > TF.ExtraPayment THEN TF.PhusaDiscount - TF.ExtraPayment ELSE TF.ExtraPayment - TF.PhusaDiscount END
                                                                            WHEN TF.IncreasingAreaPrice > 0 THEN CASE
                                                                                                                     WHEN TF.PhusaDiscount > (TF.IncreasingAreaPrice + TF.ExtraPayment) THEN TF.PhusaDiscount - (TF.IncreasingAreaPrice + TF.ExtraPayment)
                                                                                                                 ELSE (TF.IncreasingAreaPrice + TF.ExtraPayment) - TF.PhusaDiscount
                                                                                                                 END
                                                                            WHEN TF.IncreasingAreaPrice < 0 THEN CASE
                                                                                                                     WHEN (TF.PhusaDiscount - TF.IncreasingAreaPrice) > TF.ExtraPayment THEN (TF.PhusaDiscount - TF.IncreasingAreaPrice) - TF.ExtraPayment
                                                                                                                 ELSE TF.ExtraPayment - (TF.PhusaDiscount - TF.IncreasingAreaPrice)
                                                                                                                 END
                                                                        END
                                       WHEN TF.ExtraDiscount < 0
                                           AND TF.ExtraPayment = 0 THEN CASE
                                                                            WHEN TF.IncreasingAreaPrice = 0 THEN TF.PhusaDiscount - TF.ExtraDiscount
                                                                            WHEN TF.IncreasingAreaPrice > 0 THEN CASE
                                                                                                                     WHEN (TF.PhusaDiscount - TF.ExtraDiscount) > TF.IncreasingAreaPrice THEN (TF.PhusaDiscount - TF.ExtraDiscount) - TF.IncreasingAreaPrice
                                                                                                                 ELSE TF.IncreasingAreaPrice - (TF.PhusaDiscount - TF.ExtraDiscount)
                                                                                                                 END
                                                                            WHEN TF.IncreasingAreaPrice < 0 THEN TF.PhusaDiscount - TF.IncreasingAreaPrice - TF.ExtraDiscount
                                                                        END
                                       WHEN TF.ExtraDiscount < 0
                                           AND TF.ExtraPayment > 0 THEN CASE
                                                                            WHEN TF.IncreasingAreaPrice = 0 THEN CASE
                                                                                                                     WHEN (TF.PhusaDiscount - TF.ExtraDiscount) > TF.ExtraPayment THEN (TF.PhusaDiscount - TF.ExtraDiscount) - TF.ExtraPayment
                                                                                                                 ELSE TF.ExtraPayment - (TF.PhusaDiscount - TF.ExtraDiscount)
                                                                                                                 END
                                                                            WHEN TF.IncreasingAreaPrice > 0 THEN CASE
                                                                                                                     WHEN (TF.ExtraPayment + TF.IncreasingAreaPrice) > (TF.PhusaDiscount - TF.ExtraDiscount) THEN (TF.ExtraPayment + TF.IncreasingAreaPrice) - (TF.PhusaDiscount - TF.ExtraDiscount)
                                                                                                                 ELSE (TF.PhusaDiscount - TF.ExtraDiscount) - (TF.ExtraPayment + TF.IncreasingAreaPrice)
                                                                                                                 END
                                                                            WHEN TF.IncreasingAreaPrice < 0 THEN CASE
                                                                                                                     WHEN (TF.PhusaDiscount - TF.ExtraDiscount - TF.IncreasingAreaPrice) > TF.ExtraPayment THEN (TF.PhusaDiscount - TF.ExtraDiscount - TF.IncreasingAreaPrice) - TF.ExtraPayment
                                                                                                                 ELSE TF.ExtraPayment - (TF.PhusaDiscount - TF.ExtraDiscount - TF.IncreasingAreaPrice)
                                                                                                                 END
                                                                        END
                                   END
                              END */
      , 'NetSalePrice' = '' /* CASE
                             WHEN bk.BookingNumber IS NOT NULL
                                 AND ag.ContractNumber IS NULL THEN ISNULL(bk.SellingPrice, 0) - ISNULL(bk.TransferDiscount, 0)
                             WHEN bk.BookingNumber IS NOT NULL
                                 AND ag.ContractNumber IS NOT NULL
                                 AND TF.TransferNumber IS NULL THEN ISNULL(ag.SellingPrice, 0) - ISNULL(ag.TransferDiscount, 0)
                             WHEN bk.BookingNumber IS NOT NULL
                                 AND ag.ContractNumber IS NOT NULL
                                 AND TF.TransferNumber IS NOT NULL THEN ISNULL(AP1.PayableAmount, 0) - ISNULL(TF.PhusaDiscount, 0) + CASE WHEN ISNULL(TF.ExtraDiscount, 0) < 0 THEN ISNULL(TF.ExtraDiscount, 0)ELSE 0 END + CASE WHEN ISNULL(TF.IncreasingAreaPrice, 0) < 0 THEN ISNULL(TF.IncreasingAreaPrice, 0)ELSE 0 END + CASE WHEN ag.isApPay = 1 THEN ag.SpacialDiscount ELSE 0 END
                         ELSE up.TotalSellingPrice
                         END */
      , 'BookingAmount' = '' --ISNULL(TR1.BookingAmount, 0)
      , 'ContractAmount' = '' --ISNULL(TR3.DownAGAmount, 0)
      , 'TransferAmount' = '' --ISNULL(TR4.TransferAmount, 0) + ISNULL(TR5.HomefurniAmt, 0)
      , 'Total' = '' --ISNULL(TR1.BookingAmount, 0) + ISNULL(TR2.TotalAmount, 0) + ISNULL(TR5.HomefurniAmt, 0)
      , 'TotalTransfer' = '' /* CASE
                              WHEN TF.TransferDateApprove <= CONVERT(VARCHAR(50), @DateEndInStore, 120) THEN ISNULL(TR1.BookingAmount, 0) + ISNULL(TR2.TotalAmount, 0) + ISNULL(TR5.HomefurniAmt, 0)
                          ELSE 0
                          END */
      , 'TotalNotTransfer' = '' /* CASE
                                 WHEN (TF.TransferDateApprove IS NULL OR TF.TransferDateApprove >= CONVERT(VARCHAR(50), @DateEndInStore, 120)) THEN ISNULL(TR1.BookingAmount, 0) + ISNULL(TR2.TotalAmount, 0) + ISNULL(TR5.HomefurniAmt, 0)
                             ELSE 0
                             END */
      , 'PayableAmount' = '' --ISNULL(AP1.PayableAmount, 0) - ISNULL(TF.PhusaDiscount, 0) + CASE WHEN ISNULL(TF.ExtraDiscount, 0) < 0 THEN ISNULL(TF.ExtraDiscount, 0)ELSE 0 END + CASE WHEN ISNULL(TF.IncreasingAreaPrice, 0) < 0 THEN ISNULL(TF.IncreasingAreaPrice, 0)ELSE 0 END
      , 'Producttype' = '' --PR.Producttype
      , 'BookingNumber' = '' --bk.BookingNumber
      , 'BookingDate' = '' --bk.BookingDate
      , 'CreateDate' = '' --bk.CreateDate
      , 'ContractNumebr' = '' --ag.ContractNumber
    INTO #tmpResult1
    FROM [PRJ.Unit] UN WITH (NOLOCK) --This is main table need to use below table as well
    /* LEFT OUTER JOIN #tmpBook bk ON bk.ProductID = UN.ProductID AND UN.UnitNumber = bk.UnitNumber
    LEFT OUTER JOIN #tmpAgree ag ON ag.BookingNumber = bk.BookingNumber
    LEFT OUTER JOIN #tmpTrans TF WITH (NOLOCK) ON TF.ContractNumber = ag.ContractNumber
    LEFT OUTER JOIN [ICON_EntForms_Products] PR WITH (NOLOCK) ON PR.ProductID = UN.ProductID
    LEFT OUTER JOIN [ICON_EntForms_Company] CO WITH (NOLOCK) ON CO.CompanyID = PR.CompanyID
    LEFT OUTER JOIN @B TR1 ON TR1.ReferentID = bk.BookingNumber
    LEFT OUTER JOIN @AGT TR2 ON ISNULL(ag.ContractNumber, bk.BookingNumber) = TR2.ReferentID
    LEFT OUTER JOIN @TR3 TR3 ON ISNULL(ag.ContractNumber, bk.BookingNumber) = TR3.ReferentID
    LEFT OUTER JOIN @TR4 TR4 ON ISNULL(ag.ContractNumber, bk.BookingNumber) = TR4.ReferentID
    LEFT OUTER JOIN @TR5 TR5 ON TR5.ReferentID = ag.ContractNumber
    LEFT OUTER JOIN #tmpAP1 AP1 ON AP1.ContractNumber = ag.ContractNumber
    LEFT OUTER JOIN #tmpAP2 up ON up.UnitNumber = UN.UnitNumber AND up.ProductID = UN.ProductID
    WHERE UN.AssetType IN (2, 4)
        AND (ISNULL(@ProductID, '') = '' OR @ProductID = PR.ProductID)
        AND (ISNULL(@CompanyID, '') = '' OR @CompanyID = CO.CompanyID)
		AND pr.RTPExcusive IN ('1','2')
    ORDER BY UN.ProductID
      , UN.UnitNumber;

    IF (OBJECT_ID('tempdb..#tmpResult2') IS NOT NULL)
        DROP TABLE #tmpResult2;

    IF EXISTS (SELECT TOP 1 * FROM #tmpResult1)
    BEGIN
        SELECT ProductID, UnitNumber, BookingNumber, BookingDate, CreateDate INTO #tmpResult2 FROM #tmpResult1;
    END;

    IF OBJECT_ID('tempdb..#tmpResult') IS NOT NULL
        DROP TABLE #tmpResult;

    SELECT t2.BookingNumber
      , bk.ReferentID
      , BookingDate
      , CreateDate
      , t2.ProductID
      , t2.UnitNumber
    INTO #tmpResult
    FROM #tmpResult2 t2
    LEFT OUTER JOIN @B bk ON bk.ReferentID = t2.BookingNumber;

    IF OBJECT_ID('tempdb..#tmpResult3') IS NOT NULL
        DROP TABLE #tmpResult3;

    SELECT *
      , PercentPayment = ISNULL(CASE WHEN ISNULL(NetSalePrice, 0) = 0 THEN 0 ELSE (TotalNotTransfer / NetSalePrice) * 100 END, 0)
    INTO #tmpResult3
    FROM #tmpResult1 t1
    WHERE t1.BookingNumber = (   SELECT TOP 1 t2.BookingNumber
                                 FROM #tmpResult t2
                                 WHERE t2.ProductID = t1.ProductID
                                     AND t2.UnitNumber = t1.UnitNumber
                                 ORDER BY (CASE WHEN ReferentID IS NOT NULL THEN BookingDate ELSE NULL END) DESC
                                   , (CASE WHEN ReferentID IS NOT NULL THEN CreateDate ELSE NULL END) DESC)
        OR t1.BookingNumber IS NULL;

    SELECT *
      , @DateStart AS DateStart
      , @DateEnd AS DateEnd
    FROM #tmpResult3 a
    WHERE 1 = 1
        AND ((@PercentPayment = '1' AND a.PercentPayment >= 20) OR (@PercentPayment = '0' AND a.PercentPayment >= 0))
        AND (ISNULL(@UnitNumber, '') = '' OR a.UnitNumber IN (SELECT UnitNumber FROM #tmpUnitNumber))
		
    ORDER BY a.ProductID; */

END;

GO
