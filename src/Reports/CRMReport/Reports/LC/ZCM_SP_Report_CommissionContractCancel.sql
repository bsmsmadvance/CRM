SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[ZCM_SP_Report_CommissionContractCancel] '3',NULL,6,2557,'' 
--[ZCM_SP_Report_CommissionContractCancel] NULL,'70016',6,2557,'' 

ALTER PROCEDURE [dbo].[ZCM_SP_Report_CommissionContractCancel]
	@HomeType nvarchar(20),
	@ProductID nvarchar(20), 
	@Month int,
	@Year int,
	@UserName nvarchar(250),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS


IF (@Year>2500) SET @Year=@Year-543;


SELECT 'BUID' = '' --BG.Name
    ,'ProductID' = '' --P.ProjectNo
    ,'ProductName' = '' --P.ProjectNameTH
    ,'HCSName' = '' --[dbo].[fn_GetHeadOfCS](P.ProductID,'S') AS HCSName
	,'LCID' = '' --U1.EmployeeID AS LCID
    ,'LCName' = '' --U1.DisplayName AS LCName
	,'LCHelperID' = '' --U2.EmployeeID AS LCHelperID
    ,'LCHelperName' = '' --U2.DisplayName AS LCHelperName
    ,'UnitNumber' = '' --U.UnitNo
	,'CustomerName' = '' --ISNULL(AO.FirstName,'') + ' ' + ISNULL(AO.LastName,'')
	,'BookingDate' = '' --B.BookingDate
    ,'ContractDate' = '' --A.ContractDate
    ,'RDate' = '' --A.ApproveDate AS RDate
    ,'SignContractApproveDate' = '' --A.SignContractApproveDate
    ,'CancelDate' = '' --A.CancelDate,
	,'SellingPrice' = '' --ISNULL(A.SellingPrice,0)-ISNULL(A.TransferDiscount,0)

FROM  [SAL].[Agreement] A --This is actual table need to use below table as well 
/* LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON A.ID = AO.AgreementID AND ISNULL(AO.IsMainOwner,0) = 1 AND ISNULL(AO.IsDeleted,0) = 0 
LEFT OUTER JOIN [SAL].[Booking] B ON A.BookingID = B.ID 
LEFT OUTER JOIN [USR].[User] U1 ON A.SaleID = U1.UserID 
LEFT OUTER JOIN [USR].[User] U2 ON A.SaleHelper = U2.UserID
LEFT OUTER JOIN [PRJ].[Project] P ON A.ProjectID = P.ProjectID 	
LEFT OUTER JOIN [MST].[BG] BG ON BG.ID = P.BGID
LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID
	
WHERE (ISNULL(@HomeType,'')='' OR P.Ptype = @HomeType)
	AND (ISNULL(@ProductID,'')='' OR A.ProductID = @ProductID)
	AND YEAR(A.CancelDate) = @Year
	AND MONTH(A.CancelDate) = @Month
	AND A.Cancel = 1
	AND (ISNULL(@ProjectGroup,'')='' OR P.ProjectGroup = @ProjectGroup) 
	AND (ISNULL(@ProjectType2,'')='' OR P.ProjectType = @ProjectType2) 
	
ORDER BY BUID,ProductID,UnitNumber ASC; */


GO
