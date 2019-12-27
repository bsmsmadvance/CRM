SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[AP2SP_PF_FI_002_New_1] '6000220190139','19TFP201061501_1787'

ALTER PROC [dbo].[AP2SP_PF_FI_002_New_1]
    @ReceivedID NVARCHAR(4000) , --เลขที่ใบเสร็จรับเงิน
    @RCReferent NVARCHAR(4000)
AS
    DECLARE @t TABLE
        (
          PaymentType VARCHAR(10) ,
          ReceivedID VARCHAR(50) ,
          RCReferent VARCHAR(50) ,
          Details NVARCHAR(500) ,
          Amount MONEY
        )

    INSERT  INTO @t
            
            SELECT DISTINCT 'PaymentType' = ''
                    , 'ReceivedID' = ''
                    , 'RCReferent' = ''
                    , 'Details' = ''
                    , 'Amount' = ''
            FROM [FIN].[Payment] --This is temp table actual table start from below

            /* SELECT DISTINCT TR.PaymentType ,
                    TR.ReceivedID ,
                    TR.RCReferent ,
                    TR.Details + CASE WHEN T.TransferPaymentType = '3' AND T.RefType='1' THEN ' (รับเงินก่อนทำสัญญา)' ELSE '' END AS Details ,
                    TR.Amount
            FROM    [vw_RPTAP2_New_ReceiptDetail] TR
                    LEFT OUTER JOIN dbo.ICON_Payment_TmpReceipt T ON TR.RCReferent = T.RCReferent
            WHERE   TR.ReceivedID IN (
                    SELECT  *
                    FROM    [dbo].[fn_SplitString](@ReceivedID, ',') )
                    AND TR.RCReferent IN (
                    SELECT  *
                    FROM    [dbo].[fn_SplitString](@RCReferent, ',') ) */


    SELECT  *
    FROM    @t tr
    WHERE   1 = 1
            AND PaymentType NOT IN ( '9P', '9K' )

/* ORDER BY    CASE tr.PaymentType
              WHEN '4' THEN '004'
              WHEN '5' THEN '005'
              WHEN '6' THEN '006'
              WHEN '8' THEN '008'
              WHEN '9T' THEN '008'
              WHEN 'A06' THEN '009'
              WHEN '01' THEN '010'
              WHEN '02' THEN '011'
              WHEN '15' THEN '012'
              WHEN '17' THEN '013'
              WHEN '37' THEN '014'
              WHEN '00' THEN '015'
              WHEN '2G' THEN '016'
              ELSE '1' + tr.PaymentType
            END */


GO
