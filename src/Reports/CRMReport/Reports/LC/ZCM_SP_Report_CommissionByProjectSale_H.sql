SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[ZCM_SP_Report_CommissionByProjectSale_H] '3',NULL,7,2557,'' 
--[ZCM_SP_Report_CommissionByProjectSale_H] '3','10085',1,2558,'' 

ALTER PROCEDURE [dbo].[ZCM_SP_Report_CommissionByProjectSale_H]
	@HomeType nvarchar(20),
	@ProductID nvarchar(20), 
	@Month int,
	@Year int,
	@UserName nvarchar(250),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS


IF (@Year>2500) SET @Year=@Year-543;


--ทำสัญญากับโอนในเดือนเดียวกัน
SELECT  'BUID' = '' --BG.Name
    ,'ProductID' = '' --P.ProjectNo
    ,'ProductName' = '' --P.ProejctNameTH
    ,'UnitNumber' = '' --U.UnitNo
	,'HCSName' = '' --[dbo].[fn_GetHeadOfCS](CS.ProductID,'S') AS HCSName
	,'LCID' = '' --LC.EmployeeID
	,'LCName' = '' --LC.DisplayName
	,'LCHelperID' = '' --LCH.EmployeeID
	,'LCHelperName' = '' --LCH.DisplayName
	,'LCCID' = '' --LCC.EmployeeID
	,'LCCName' = '' --LCC.DisplayName

	,'CustomerName' = '' --ISNULL(AO.FirstNameTH,'') + ' ' + ISNULL(AO.LastNameTH,'')
	
	,'BookingDate' = '' --B.BookingDate
    ,'ContractDate' = '' --A.ContractDate
    ,'ApproveDate' = '' --A.SignAgreementDate AS ApproveDate
    ,'SignContractApproveDate' = '' --A.SignContractApproveDate
		
	,'SellingPrice' = '' --ISNULL(A.SellingPrice,0)-ISNULL(A.TransferDiscount,0)
	
	,'CommissionRatePercentSale' = '' --ISNULL(CS.CommissionRatePercent,0)
	
	,'SaleCommissionSalePaid' = '' --CASE WHEN LC.EmployeeID = LCH.EmployeeID THEN 0 ELSE ISNULL(CS.SaleCommissionSalePaid,0) END
	,'SaleHelperCommissionSalePaid' = '' --CASE WHEN LC.EmployeeID = LCH.EmployeeID THEN 0 ELSE ISNULL(CS.SaleHelperCommissionSalePaid,0) END
	,'TotalSaleCommission' = '' --ISNULL(CS.SaleCommissionSalePaid,0) + ISNULL(CS.SaleHelperCommissionSalePaid,0)
		
	,'LCCCommissionSalePaid' = '' --ISNULL(CS.LCCCommissionSalePaid,0)
	
	,'TotalCommissionPaid' = '' --ISNULL(CS.SaleCommissionSalePaid,0) + ISNULL(CS.SaleHelperCommissionSalePaid,0) + ISNULL(CS.LCCCommissionSalePaid,0)
	
	,'FlagData' = '' --CASE WHEN CS.IsMigrate = 1 THEN 'Comm เก่า' WHEN CS.IsMigrate = 0 THEN 'Comm ใหม่' END
	,'flag' = 1

FROM [SAL].[Booking] --This is temp table actual table start from below
/* dbo.ZComm_CommissionCalSale CS
LEFT OUTER JOIN dbo.ZComm_CommissionCalTransfer CT ON CS.ContractNumber=CT.ContractNumber AND CS.PeriodYear=CT.PeriodYear AND CS.PeriodMonth=CT.PeriodMonth 
LEFT OUTER JOIN [SAL].[Agreement] A ON CS.ContractNumber = A.ContractNumber
LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON A.ID = AO.AgreementID AND ISNULL(AO.IsMainOwner,0) = 1 AND ISNULL(AO.IsDeleted,0) = 0 
LEFT OUTER JOIN [SAL].[Booking] B ON A.BookingID = B.ID
LEFT OUTER JOIN [USR].[User] LC ON CS.SaleID = LC.UserID 
LEFT OUTER JOIN [USR].[User] LCH ON CS.SaleHelperID = LCH.UserID
LEFT OUTER JOIN [USR].[User] LCC ON CS.LCCID = LCC.UserID
LEFT OUTER JOIN [PRJ].[Project] P ON A.ProjectID = P.ProjectID 	
LEFT OUTER JOIN [MST].[BG] BG ON BG.ID = P.BGID
LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID
	
WHERE (ISNULL(@HomeType,'')='' OR P.Ptype = @HomeType)
	AND (ISNULL(@ProductID,'')='' OR CS.ProductID = @ProductID)
	AND CS.PeriodYear = @Year
	AND CS.PeriodMonth = @Month
	AND ISNULL(CT.SaleHelperCommissionTransPaid,0) > 0
	AND CS.ISActive = 1
	AND (ISNULL(@ProjectGroup,'')='' OR P.ProjectGroup = @ProjectGroup) 
	AND (ISNULL(@ProjectType2,'')='' OR P.ProjectType = @ProjectType2) */
	
UNION

--ทำสัญญาแต่ยังไม่โอน
SELECT 'BUID' = '' --BG.Name
    ,'ProductID' = '' --P.ProjectNo
    ,'ProductName' = '' --P.ProejctNameTH
    ,'UnitNumber' = '' --U.UnitNo
	,'HCSName' = '' --[dbo].[fn_GetHeadOfCS](CS.ProductID,'S') AS HCSName
	,'LCID' = '' --LC.EmployeeID
	,'LCName' = '' --LC.DisplayName
	,'LCHelperID' = '' --LCH.EmployeeID
	,'LCHelperName' = '' --LCH.DisplayName
	,'LCCID' = '' --LCC.EmployeeID
	,'LCCName' = '' --LCC.DisplayName
	
	,'CustomerName' = '' --ISNULL(AO.FirstNameTH,'') + ' ' + ISNULL(AO.LastNameTH,'')
	
	,'BookingDate' = '' --B.BookingDate
    ,'ContractDate' = '' --A.ContractDate
    ,'ApproveDate' = '' --A.SignAgreementDate
    ,'SignContactApproveDate' = '' --A.SignContractApproveDate
	
	,'SellingPrice' = '' --ISNULL(A.SellingPrice,0)-ISNULL(A.TransferDiscount,0)
	
	,'CommisionRatePercentSale' = ''--ISNULL(CS.CommissionRatePercent,0)
	
	,'SaleCommissionSalePaid' = '' --CASE WHEN LC.EmployeeID = LCH.EmployeeID THEN 0 ELSE ISNULL(CS.SaleCommissionSalePaid,0) END
	,'SaleHelperCommissionSalePaid' = '' --CASE WHEN LC.EmployeeID = LCH.EmployeeID THEN 0 ELSE ISNULL(CS.SaleHelperCommissionSalePaid,0) END
	,'TotalSaleCommission' = '' --ISNULL(CS.SaleCommissionSalePaid,0) + ISNULL(CS.SaleHelperCommissionSalePaid,0)
	 
	,'LCCCommissionSalePaid' = '' --ISNULL(CS.LCCCommissionSalePaid,0)
	
	,'TotalCommissionPaid' = '' --ISNULL(CS.SaleCommissionSalePaid,0) + ISNULL(CS.SaleHelperCommissionSalePaid,0) + ISNULL(CS.LCCCommissionSalePaid,0)
	
	,'FlagData' = '' --CASE WHEN CS.IsMigrate = 1 THEN 'Comm เก่า' WHEN CS.IsMigrate = 0 THEN 'Comm ใหม่' END
	,2
	
FROM  [SAL].[Booking] --This is temp table actual table start from below
/* dbo.ZComm_CommissionCalSale CS
LEFT OUTER JOIN [SAL].[Agreeemnt] A ON CS.ContractNumber = A.ContractNumber
LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON A.ID = AO.AgreementID AND ISNULL(AO.IsMainOwner,0) = 1 AND ISNULL(AO.IsDeleted,0) = 0 
LEFT OUTER JOIN [SAL].[Booking] B ON A.BookingID = B.ID 
LEFT OUTER JOIN [SAL].[Transfer] T ON CS.ContractNumber = T.ContractNumber 
LEFT OUTER JOIN [USR].[User] LC ON CS.SaleID = LC.UserID 
LEFT OUTER JOIN [USR].[User] LCH ON CS.SaleHelperID = LCH.UserID
LEFT OUTER JOIN [USR].[User] LCC ON CS.LCCID = LCC.UserID
LEFT OUTER JOIN [PRJ].[Project] P ON A.ProjectID = P.ProjectID 	
LEFT OUTER JOIN [MST].[BG] BG ON BG.ID = P.BGID
LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID

WHERE (ISNULL(@HomeType,'')='' OR P.Ptype = @HomeType)
	AND (ISNULL(@ProductID,'')='' OR CS.ProductID = @ProductID)
	AND CS.PeriodYear = @Year
	AND CS.PeriodMonth = @Month
	AND NOT EXISTS(SELECT * FROM  dbo.ZComm_CommissionCalTransfer WHERE ContractNumber=CS.ContractNumber AND ((PeriodMonth<=@Month AND PeriodYear=@Year) OR PeriodYear<@Year))
	AND CS.ISActive = 1
	AND (ISNULL(@ProjectGroup,'')='' OR P.ProjectGroup = @ProjectGroup) 
	AND (ISNULL(@ProjectType2,'')='' OR P.ProjectType = @ProjectType2) 
	
ORDER BY BUID,ProductID,UnitNumber ASC; */



GO
