SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--ZCM_SP_Report_CommissionByProject '1','10060',8,2559,'Administrator Account','1',''
--ZCM_SP_Report_CommissionByProject '2','10171',8,2559,'Administrator Account','2.1','TH'

ALTER PROCEDURE [dbo].[ZCM_SP_Report_CommissionByProject]
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
SELECT 'BUID' = '' --BG.Name
    ,'ProductID' = '' --P.ProjectNo
    ,'ProductName' = '' --P.ProejctNameTH
    ,'UnitNumber' = '' --U.UnitNo
	,'HCSName' = '' --[dbo].[fn_GetHeadOfCS](CS.ProductID,'S') AS HCSName
	,'LCID' = '' --U1.EmployeeID AS LCID
    ,'LCName' = '' --U1.DisplayName AS LCName
	,'LCHelperID' = '' --U2.EmployeeID AS LCHelperID
    ,'LCHelperName' = '' --U2.DisplayName AS LCHelperName
	,'LCCID' = '' --U3.EmployeeID AS LCCID
    ,'LCCName' = '' --U3.DisplayName AS LCCName
	,'CustomerName' = '' --ISNULL(AO.FirstName,'') + ' ' + ISNULL(AO.LastName,'')
	,'BookingDate' = '' --B.BookingDate
    ,'ContractDate' = '' --A.ContractDate
    ,'RDate' = '' --A.ApproveDate AS RDate
    ,'SignContractApproveDate' = '' --A.SignContractApproveDate
    ,'TransferDateApprove' = '' --T.TransferDateApprove	
	,'SellingPrice' = '' --ISNULL(A.SellingPrice,0)-ISNULL(A.TransferDiscount,0)
	,'CommissionRatePercentSale' = '' --ISNULL(CS.CommissionRatePercent,0)
	
	,'CommissionSalePaid' = '' --ISNULL(CS.SaleCommissionSalePaid,0)
	
	,'SaleCommissionTransPaid' = '' --CASE WHEN ISNULL(U1.EmployeeID,U3.EmployeeID) <> ISNULL(U2.EmployeeID,'') AND ISNULL(CT.SaleHelperCommissionTransPaid,0) > 0 
									--	THEN ISNULL(CT.SaleCommissionTransPaid,0) ELSE 0 END
	,'SaleHelperCommissionTransPaid' = '' --CASE WHEN ISNULL(U1.EmployeeID,U3.EmployeeID) <> ISNULL(U2.EmployeeID,'') 
										--THEN ISNULL(CT.SaleHelperCommissionTransPaid,0) ELSE 0 END
	,'TotalTransferCommission' = '' /* CASE WHEN ISNULL(U1.EmployeeID,U3.EmployeeID) = ISNULL(U2.EmployeeID,'') OR ISNULL(CT.SaleHelperCommissionTransPaid,0) = 0 
										THEN ISNULL(CT.SaleCommissionTransPaid,0) + ISNULL(CT.SaleHelperCommissionTransPaid,0) 
									WHEN U3.EmployeeID IS NOT NULL
										THEN ISNULL(CT.SaleHelperCommissionTransPaid,0) ELSE 0 END */

	,'SaleNewLaunchPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') <> ISNULL(U2.EmployeeID,'') AND ISNULL(CS.SaleHelperNewLaunchPaid,0) > 0 
										THEN ISNULL(CS.SaleNewLaunchPaid,0) ELSE 0 END */
	,'SaleHelperNewLaunchPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') <> ISNULL(U2.EmployeeID,'') 
										THEN ISNULL(CS.SaleHelperNewLaunchPaid,0) ELSE 0 END */
	,'TotalNewLaunch' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') = ISNULL(U2.EmployeeID,'') OR ISNULL(CS.SaleHelperNewLaunchPaid,0) = 0 
										THEN ISNULL(CS.SaleNewLaunchPaid,0) + ISNULL(CS.SaleHelperNewLaunchPaid,0) ELSE 0 END */
		
	,'TotalCommissionPaid' = '' /* ISNULL(CS.SaleCommissionSalePaid,0)+ISNULL(CT.SaleCommissionTransPaid,0)
				+ISNULL(CT.SaleHelperCommissionTransPaid,0)
				+ISNULL(CS.SaleNewLaunchPaid,0)+ISNULL(CS.SaleHelperNewLaunchPaid,0)
				+ISNULL(CS.LCCCommissionSalePaid,0) */
				
	,'LCC' = '' --ISNULL(CS.LCCCommissionSalePaid,0)
	,'FlagData' = '' --CASE WHEN CS.IsMigrate = 1 THEN 'Comm เก่า' WHEN CS.IsMigrate = 0 THEN 'Comm ใหม่' END
	,'flag' = 1

FROM [SAL].[Booking] B --This is temp table actual table start from below
/* dbo.ZComm_CommissionCalSale CS
LEFT OUTER JOIN dbo.ZComm_CommissionCalTransfer CT ON CS.ContractNumber=CT.ContractNumber AND CS.PeriodYear=CT.PeriodYear AND CS.PeriodMonth=CT.PeriodMonth AND CT.ISActive = 1 
LEFT OUTER JOIN [SAL].[Agreement] A ON CS.ContractNumber = A.ContractNumber
LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON A.ID = AO.AgreementID AND ISNULL(AO.IsMainOwner,0) = 1 AND ISNULL(AO.IsDeleted,0) = 0 
LEFT OUTER JOIN [SAL].[Booking] B ON A.BookingID = B.ID 
LEFT OUTER JOIN [SAL].[Transfer] T ON CS.ContractNumber=T.ContractNumber 
LEFT OUTER JOIN [USR].[User] U1 ON CS.SaleID = U1.UserID 
LEFT OUTER JOIN [USR].[User] U2 ON CS.SaleHelperID = U2.UserID
LEFT OUTER JOIN [USR].[User] U3 ON CS.LCCID = U3.UserID
LEFT OUTER JOIN [PRJ].[Project] P ON A.ProjectID = P.ProjectID 	
LEFT OUTER JOIN [MST].[BG] BG ON BG.ID = P.BGID
LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID
	
WHERE (ISNULL(@HomeType,'')='' OR P.Ptype = @HomeType)
	AND (ISNULL(@ProductID,'')='' OR CS.ProductID = @ProductID)
	AND CS.PeriodYear = @Year
	AND CS.PeriodMonth = @Month
	AND (CT.ContractNumber IS NOT NULL OR CT.LCCID > 0)
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
	,'LCID' = '' --U1.EmployeeID AS LCID
    ,'LCName' = '' --U1.DisplayName AS LCName
	,'LCHelperID' = '' --U2.EmployeeID AS LCHelperID
    ,'LCHelperName' = '' --U2.DisplayName AS LCHelperName
	,'LCCID' = '' --U3.EmployeeID AS LCCID
    ,'LCCName' = '' --U3.DisplayName AS LCCName
	,'CustomerName' = '' --ISNULL(AO.FirstName,'') + ' ' + ISNULL(AO.LastName,'')
	,'BookingDate' = '' --B.BookingDate
    ,'ContractDate' = '' --A.ContractDate
    ,'RDate' = '' --A.ApproveDate AS RDate
    ,'SignContractApproveDate' = '' --A.SignContractApproveDate
    ,'TransferDateApprove' = '' --T.TransferDateApprove
	,'SellingPrice' = '' --ISNULL(A.SellingPrice,0)-ISNULL(A.TransferDiscount,0)
	
	,'CommissionRatePercentSale' = '' --ISNULL(CS.CommissionRatePercent,0)	
	
	,'CommissionSalePaid' = '' --ISNULL(CS.SaleCommissionSalePaid,0)
	
	,'SaleCommissionTransPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,U3.EmployeeID) <> ISNULL(U2.EmployeeID,'') AND ISNULL(CS.SaleHelperCommissionTransPaid,0) > 0 
			THEN ISNULL(CS.SaleCommissionTransPaid,0) ELSE 0 END */
	,'SaleHelperCommissionTransPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,U3.EmployeeID) <> ISNULL(U2.EmployeeID,'') 
			THEN ISNULL(CS.SaleHelperCommissionTransPaid,0) ELSE 0 END */
	,'TotalTransferCommission' = '' /* CASE WHEN ISNULL(U1.EmployeeID,U3.EmployeeID) = ISNULL(U2.EmployeeID,'') OR ISNULL(CS.SaleHelperCommissionTransPaid,0) = 0 
			THEN ISNULL(CS.SaleCommissionTransPaid,0)+ISNULL(CS.SaleHelperCommissionTransPaid,0) 
		 WHEN U3.EmployeeID IS NOT NULL
			THEN ISNULL(CS.SaleHelperCommissionTransPaid,0) ELSE 0 END */
		
	,'SaleNewLaunchPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') <> ISNULL(U2.EmployeeID,'') AND ISNULL(CS.SaleHelperNewLaunchPaid,0) > 0 
			THEN ISNULL(CS.SaleNewLaunchPaid,0) ELSE 0 END */
	,'SaleHelperNewLaunchPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') <> ISNULL(U2.EmployeeID,'') 
			THEN ISNULL(CS.SaleHelperNewLaunchPaid,0) ELSE 0 END */
	,'TotalNewLaunch' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') = ISNULL(U2.EmployeeID,'') OR ISNULL(CS.SaleHelperNewLaunchPaid,0) = 0 
			THEN ISNULL(CS.SaleNewLaunchPaid,0) + ISNULL(CS.SaleHelperNewLaunchPaid,0) ELSE 0 END */
	
	,'TotalCommissionPaid' = '' /* ISNULL(CS.SaleCommissionSalePaid,0)
		+ISNULL(CS.SaleNewLaunchPaid,0)+ISNULL(CS.SaleHelperNewLaunchPaid,0)
		+ISNULL(CS.LCCCommissionSalePaid,0) */
			 
	,'LCC' = '' --ISNULL(CS.LCCCommissionSalePaid,0)
	,'FlagData' = '' --CASE WHEN CS.IsMigrate = 1 THEN 'Comm เก่า' WHEN CS.IsMigrate = 0 THEN 'Comm ใหม่' END
	,'flag' = 2
	
FROM [SAL].[Booking] B --This is temp table actual table start from below
/* dbo.ZComm_CommissionCalSale CS
LEFT OUTER JOIN [SAL].[Agreement] A ON CS.ContractNumber = A.ContractNumber
LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON A.ID = AO.AgreementID AND ISNULL(AO.IsMainOwner,0) = 1 AND ISNULL(AO.ISDeleted,0) = 0 
LEFT OUTER JOIN [SAL].[Booking] B ON A.BookingID = B.ID 
LEFT OUTER JOIN [SAL].[Transfer] T ON CS.ContractNumber=T.ContractNumber 
LEFT OUTER JOIN [USR].[User] U1 ON CS.SaleID = U1.UserID 
LEFT OUTER JOIN [USR].[User] U2 ON CS.SaleHelperID = U2.UserID
LEFT OUTER JOIN [USR].[User] U3 ON CS.LCCID = U3.UserID
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
	AND (ISNULL(@ProjectType2,'')='' OR P.ProjectType = @ProjectType2) */
	
UNION

--ทำสัญญามาแล้วแต่มาโอนในเดือนนี้
SELECT 'BUID' = '' --BG.Name
    ,'ProductID' = '' --P.ProjectNo
    ,'ProductName' = '' --P.ProejctNameTH
    ,'UnitNumber' = '' --U.UnitNo
	,'HCSName' = '' --[dbo].[fn_GetHeadOfCS](CT.ProductID,'T') AS HCSName
	,'LCID' = '' --U1.EmployeeID AS LCID
    ,'LCName' = '' --U1.DisplayName AS LCName
	,'LCHelperID' = '' --U2.EmployeeID AS LCHelperID
    ,'LCHelperName' = '' --U2.DisplayName AS LCHelperName
	,'LCCID' = '' --U3.EmployeeID AS LCCID
    ,'LCCName' = '' --U3.DisplayName AS LCCName
	,'CustomerName' = '' --ISNULL(AO.FirstName,'') + ' ' + ISNULL(AO.LastName,'')
	,'BookingDate' = '' --B.BookingDate
    ,'ContractDate' = '' --A.ContractDate
    ,'RDate' = '' --A.ApproveDate AS RDate
    ,'SignContractApproveDate' = '' --A.SignContractApproveDate
    ,'TransferDateApprove' = '' --T.TransferDateApprove	
	,'SellingPrice' = '' --ISNULL(A.SellingPrice,0)-ISNULL(A.TransferDiscount,0)
	
	,'CommissionRatePercentSale' = '' --ISNULL(CT.CommissionRatePercent,0)
	
	,'CommissionSalePaid' = 0
	,'SaleCommissionTransPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') <> ISNULL(U2.EmployeeID,'') AND ISNULL(CT.SaleHelperCommissionTransPaid,0) > 0 
			THEN ISNULL(CT.SaleCommissionTransPaid,0) ELSE 0 END */
	,'SaleHelperCommissionTransPaid' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') <> ISNULL(U2.EmployeeID,'') 
			THEN ISNULL(CT.SaleHelperCommissionTransPaid,0) ELSE 0 END */
	,'TotalTransferCommission' = '' /* CASE WHEN ISNULL(U1.EmployeeID,'') = ISNULL(U2.EmployeeID,'') OR ISNULL(CT.SaleHelperCommissionTransPaid,0) > 0 
			THEN ISNULL(CT.SaleCommissionTransPaid,0) + ISNULL(CT.SaleHelperCommissionTransPaid,0) ELSE 0 END */
	,'SaleNewLaunchPaid' = 0
    ,'SaleHelperNewLaunchPaid' = 0
    ,'TotalNewLaunch' = 0
	
	,'TotalCommissionPaid' = '' /* ISNULL(CT.SaleCommissionTransPaid,0)+ISNULL(CT.SaleHelperCommissionTransPaid,0)
		+ISNULL(CT.LCCCommissionTransPaid,0) */
	
	,'LCC' = '' --ISNULL(CT.LCCCommissionTransPaid,0)
	,'FlagData' = '' --CASE WHEN CT.IsMigrate = 1 THEN 'Comm เก่า' WHEN CT.IsMigrate = 0 THEN 'Comm ใหม่' END
	,'flag' = 3

FROM [SAL].[Booking] B --This is temp table actual table start from below
/* dbo.ZComm_CommissionCalTransfer CT
LEFT OUTER JOIN [SAL].[Agreement] A ON CT.ContractNumber = A.ContractNumber
LEFT OUTER JOIN [SAL].[AgreementOwner] AO ON A.ID = AO.ContractNumber AND ISNULL(AO.Header,0) = 1 AND ISNULL(AO.ISDelete,0) = 0 
LEFT OUTER JOIN [SAL].[Booking] B ON A.BookingID = B.ID 
LEFT OUTER JOIN [SAL].[Transfer] T ON CT.ContractNumber = T.ContractNumber 
LEFT OUTER JOIN [USR].[Users] U1 ON CT.SaleID = U1.UserID 
LEFT OUTER JOIN [USR].[Users] U2 ON CT.SaleHelperID = U2.UserID
LEFT OUTER JOIN [USR].[Users] U3 ON CT.LCCID = U3.UserID
LEFT OUTER JOIN [PRJ].[Project] P ON A.ProjectID = P.ProjectID 
LEFT OUTER JOIN [MST].[BG] BG ON BG.ID = P.BGID
LEFT OUTER JOIN [PRJ].[Unit] U ON U.ID = A.UnitID	

WHERE (ISNULL(@HomeType,'')='' OR P.Ptype = @HomeType)
	AND (ISNULL(@ProductID,'')='' OR CT.ProductID = @ProductID)
	AND CT.PeriodYear = @Year
	AND CT.PeriodMonth = @Month
	AND NOT EXISTS(SELECT * FROM  dbo.ZComm_CommissionCalSale WHERE ContractNumber=CT.ContractNumber AND PeriodYear=CT.PeriodYear AND PeriodMonth=CT.PeriodMonth)
	AND CT.ISActive = 1
	AND (ISNULL(@ProjectGroup,'')='' OR P.ProjectGroup = @ProjectGroup) 
	AND (ISNULL(@ProjectType2,'')='' OR P.ProjectType = @ProjectType2) 
	
ORDER BY BUID,ProductID,UnitNumber ASC; */



GO
