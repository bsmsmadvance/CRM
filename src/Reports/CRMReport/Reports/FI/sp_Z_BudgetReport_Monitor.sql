SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USE [db_iconcrm_fusion]
--GO
--/****** Object:  StoredProcedure [dbo].[sp_Z_BudgetReport_Monitor]    Script Date: 01/07/2015 09:42:57 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO



--  EXEC [sp_Z_BudgetReport_Monitor] '','','',null,null,null,null,'','','administrator account','','sh'

CREATE PROCEDURE [dbo].[sp_Z_BudgetReport_Monitor]
	@HomeType NVARCHAR(50)
	,@CompanyID NVARCHAR(50)
    ,@ProductID NVARCHAR(19)
	,@DateStart DATETIME
	,@DateEnd DATETIME
	,@DateStart2 DATETIME
	,@DateEnd2 DATETIME	
	,@Status3 NVARCHAR(100) = '' --ทั้งหมด=0/จอง=1/สัญญา=2/โอน=3/Active=4
	,@Status4 NVARCHAR(100) = '' --All=0/Pending=1/Approve=2/Headof=3/CCO=4/MD=5	
	,@UserName  NVARCHAR(150) = ''
	,@ProjectGroup nvarchar(5)
	,@ProjectType2 nvarchar(5)
AS
	if(@DateStart is null) set @DateStart='20000101'
	if(@DateEnd is null) set @DateEnd='21001231'
	if(@DateStart2 is null) set @DateStart2='20000101'
	if(@DateEnd2 is null) set @DateEnd2='21001231'
	/* Set Filter */
		
	DECLARE @BUName NVARCHAR(100) = ''	
	SET @BUName =	(CASE
						WHEN ISNULL(@HomeType,'') = '' THEN N'ทั้งหมด'
						ELSE @HomeType
					END)	
	DECLARE @DocStatus4Filter NVARCHAR(100) = ''	
	SET @DocStatus4Filter = (CASE
							WHEN ISNULL(@Status3,'0') = '1' THEN N'Booking'
							WHEN ISNULL(@Status3,'0') = '2' THEN N'Contract'
							WHEN ISNULL(@Status3,'0') = '3' THEN N'Transfer'
							ELSE ''
						END)	
	DECLARE @DocStatus4Show NVARCHAR(100) = ''	
	SET @DocStatus4Show =	(CASE
							WHEN ISNULL(@Status3,'0') = '1' THEN N'จอง'
							WHEN ISNULL(@Status3,'0') = '2' THEN N'สัญญา'
							WHEN ISNULL(@Status3,'0') = '3' THEN N'โอน'
							ELSE N'ทั้งหมด'
						END)						
	DECLARE @ProcessStatus4Filter NVARCHAR(100) = ''	
	SET @ProcessStatus4Filter = (CASE
							WHEN ISNULL(@Status4,'0') = '1' THEN N'1'
							WHEN ISNULL(@Status4,'0') = '2' THEN N'2'
							WHEN ISNULL(@Status4,'0') = '3' THEN N'3'
							WHEN ISNULL(@Status4,'0') = '4' THEN N'4'
							WHEN ISNULL(@Status4,'0') = '5' THEN N'5'
							ELSE ''
						END)	
	DECLARE @ProcessStatus4Show NVARCHAR(100) = ''	
	SET @ProcessStatus4Show =	(CASE
							WHEN ISNULL(@Status4,'0') = '1' THEN N'Pending'
							WHEN ISNULL(@Status4,'0') = '2' THEN N'Approve'
							WHEN ISNULL(@Status4,'0') = '3' THEN N'Head of'
							WHEN ISNULL(@Status4,'0') = '4' THEN N'CCO'
							WHEN ISNULL(@Status4,'0') = '5' THEN N'MD'
							ELSE N'ทั้งหมด'
						END)	
	DECLARE @CompanyName NVARCHAR(200) = ''		
	SET @CompanyName =	(CASE
							WHEN ISNULL(@CompanyID,'') = '' THEN N'ทั้งหมด'
							ELSE (SELECT TOP(1) NameTH FROM MST.Company WHERE Code = @CompanyID)
						END)						
	DECLARE @ProductName NVARCHAR(200) = ''		
	SET @ProductName = (CASE
							WHEN ISNULL(@ProductID,'') = '' THEN N'ทั้งหมด'
							ELSE (SELECT TOP(1) ProjectNameTH FROM PRJ.Project WHERE ProjectNo = @ProductID)
						END)	

	/* Set Query */
	SELECT a.*,'Impact'= '' FROM --Orginal_ROIminPrice-NetSellingPrice FROM 
		(
			SELECT	
					'BU' = '' --t2.PType AS BU 
					,'ProjectCode' = '' --t2.ProductID AS ProjectCode	
					,'ProjectName' = '' --t2.Project AS ProjectName	
					,'CompanyID' = '' --t2.CompanyID AS CompanyID 
					,'Unit' = '' --t1.UnitNumber AS Unit
					,'Status1' = '' /* (CASE
						WHEN t1.DocumentType LIKE '%Booking%' THEN N'จอง'
						WHEN t1.DocumentType LIKE '%Contract%' THEN N'สัญญา'
						WHEN t1.DocumentType LIKE '%Transfer%' THEN N'โอน'
					END) AS Status1 */
					,'BookingDate' = '' /* (CASE
						WHEN t1.DocumentType LIKE '%Booking%' THEN ISNULL(t4.BookingDate,t3.BookingDate)
						WHEN t1.DocumentType LIKE '%Contract%' THEN ISNULL(t10.BookingDate,t19.BookingDate)
						WHEN t1.DocumentType LIKE '%Transfer%' THEN ISNULL(t12.BookingDate,t20.BookingDate)
					END) AS BookingDate */
					,'TransferDate' = '' --tf.TransferDateApprove AS TransferDate
					,'SellingPrice' = '' /* ISNULL((CASE
						WHEN t1.DocumentType LIKE '%Booking%' THEN Isnull(t4.SellingPrice,t3.SellingPrice)
						WHEN t1.DocumentType LIKE '%Contract%' THEN ISNULL(ISNULL(t5.SellingPrice,t11.SellingPrice),t12.SellingPrice)	
						WHEN t1.DocumentType LIKE '%Transfer%' THEN ISNULL(ISNULL(t5.SellingPrice,t11.SellingPrice),t12.SellingPrice)
					END),0) AS SellingPrice */				
					,'SellingDiscount' = '' /* ISNULL((CASE
						WHEN t1.DocumentType LIKE '%Booking%' THEN Isnull(t4.TransferDiscount,t3.TransferDiscount) 
						WHEN t1.DocumentType LIKE '%Contract%' THEN ISNULL(ISNULL(ISNULL(t5.TransferDiscount,t11.TransferDiscount),t12.TransferDiscount),0) 
						WHEN t1.DocumentType LIKE '%Transfer%' THEN ISNULL(ISNULL(ISNULL(t5.TransferDiscount,t11.TransferDiscount),t12.TransferDiscount),0) --ISNULL(ISNULL(ISNULL(t5.TransferDiscount,Isnull(t6.TransferDiscount,t11.TransferDiscount)),t12.TransferDiscount),0) 	
					END),0) AS SellingDiscount */
					,'SellPromotion' = '' /* ISNULL((CASE
						WHEN t1.DocumentType LIKE '%Booking%' THEN Isnull(t4.TotalBudgetPromotion,t3.TotalBudgetPromotion) 
						WHEN t1.DocumentType LIKE '%Contract%' THEN ISNULL(ISNULL(ISNULL(t5.TotalBudgetPromotion,t11.TotalBudgetPromotion),t12.TotalBudgetPromotion),0) 
						WHEN t1.DocumentType LIKE '%Transfer%' THEN ISNULL(ISNULL(ISNULL(t5.TotalBudgetPromotion,t11.TotalBudgetPromotion),t12.TotalBudgetPromotion),0) 	
					END),0) AS SellPromotion */
					,'TransPromotionDiscount' = '' --ISNULL(t6.TransferDiscount,0) AS TransPromotionDiscount
					,'TransPromotion' = '' --ISNULL(t15.ItemPrice,0) + isnull(t16.PayPrice,0) AS TransPromotion
                    ,'NetSellingPrice' = '' --t1.NetAmount NetSellingPrice
					,'Orginal_ROIminPrice' = '' --convert(decimal(18,2),Isnull((Select Top 1 SuggestionPrice From ICON_EntForms_SuggestionPrice s Where s.unitnumber=t1.UnitNumber and s.ProductID=t1.ProductID /*and Isnull(MinPriceType,0)=2*/ Order by Case When Isnull(MinPriceType,0)=2 Then 1 When Isnull(MinPriceType,0)=1 Then 2 Else 3 End,ImportDate Desc),0))
					,'Impact_Old' = '' --ISNULL(t1.BudgetAmount,0) AS Impact_Old	
					,'Type' = '' /* Case t1.BudgetType When 'Quarterly' Then 'Quarterly'
							When 'AdHoc1' Then 'Adhoc<=5%'
							When 'AdHoc2' Then 'Adhoc>5%'
							When 'BudgetTransfer' Then 'Budget Transfer'
							Else '' End  AS [Type] */
					,'Reason' = '' --t1.Comment AS Reason					
					,'Status2' = '' /* (CASE 
						WHEN t1.[Status] = 'Finish' THEN 'Approve'
						WHEN t1.Approve1Date IS NULL THEN 'LCM'		
						WHEN t1.Approve2Date IS NULL THEN 'Head of'				
						WHEN t1.BudgetType in('AdHoc1') and t1.Approve3Date IS NULL THEN 'CCO'
						WHEN t1.BudgetType in('AdHoc1','AdHoc2') and t1.Approve4Date IS NULL THEN 'MD'
						WHEN t1.[Status] = 'Inprocess' THEN 'Pending'											
					END)  AS Status2 */				
					
					/* For Filter */				
					,'Status1Type' = '' --t1.DocumentType AS Status1Type			
					,'Status2Type' = '' /* (CASE 
						WHEN t1.[Status] = 'Finish' THEN '2'
						WHEN t1.Approve2Date IS NULL THEN '3'
						WHEN t1.BudgetType in('AdHoc1','AdHoc2') and t1.Approve3Date IS NULL THEN '4'
						WHEN t1.BudgetType in('AdHoc2') and t1.Approve4Date IS NULL THEN '5'	
						WHEN t1.[Status] = 'Inprocess' THEN '1'											
					END)  AS Status2Type */
					,'DocStatus' = '' --@DocStatus4Show AS DocStatus
					,'ProcessStatus' = '' --@ProcessStatus4Show AS ProcessStatus	
					,'BUName' = '' --@BUName AS BUName
					,'CompanyName' = '' --@CompanyName AS CompanyName
					,'ProductName' = '' --@ProductName AS ProductName
					,'UserName' = '' --@UserName AS UserName
					,'Status' = '' --t1.[Status]
					,'documentcancel' = '' --t1.documentcancel
					,'ProjectGroup' = '' --t2.ProjectGroup
                    ,'ProjectType' = '' --t2.ProjectType
			--SELECT * 
			FROM [SAL].[Agreement] A  --This is temp table use for temp mapping only. Actual table start from below
                /*Z_BudgetApprove t1
				LEFT OUTER JOIN Icon_Entforms_Products t2 on t2.ProductID = t1.ProductID
				LEFT JOIN Icon_Entforms_Booking t3 on t3.BookingNumber = t1.DocumentNumber AND t3.CancelDate IS NULL	
				LEFT JOIN Z_BudgetBooking t4 on t4.BookingNumber = t1.DocumentNumber AND t4.CancelDate IS NULL		
				LEFT OUTER JOIN Icon_Entforms_Agreement t5 on t5.ContractNumber = t1.DocumentNumber AND t5.CancelDate IS NULL	
				LEFT OUTER JOIN ZPROM_TransferPromotion t6 on t6.TransferPromotionID = t1.DocumentNumber AND ISNULL(t6.IsCancel,0)=0
				----------------------------------------------------------------------------------------------------	
				LEFT OUTER JOIN Icon_Entforms_Booking t10 on t10.BookingNumber = t5.BookingNumber AND t10.CancelDate IS NULL	
				----------------------------------------------------------------------------------------------------	
				LEFT OUTER JOIN Icon_Entforms_Agreement t11 on t11.ContractNumber = t6.ContractNumber AND t11.CancelDate IS NULL	
				LEFT OUTER JOIN Icon_Entforms_Booking t12 on t12.BookingNumber = t11.BookingNumber AND t12.CancelDate IS NULL					
				----------------------------------------------------------------------------------------------------	
				Left Join ICON_EntForms_Transfer tf on tf.ContractNumber =Isnull(t11.ContractNumber,'') or tf.ContractNumber =Isnull(t6.ContractNumber,'')
				----------------------------------------------------------------------------------------------------				
				LEFT OUTER JOIN 
				(	
					SELECT  a.TransferPromotionID,SUM(b.PricePerUnit*a.Amount) AS ItemPrice
					FROM	ZPROM_TransferPromotionDetail a
							LEFT OUTER JOIN ZPROM_PromotionDetail b ON b.PromotionID = a.PromotionID AND b.ItemID = a.ItemID
					WHERE ISNULL(a.IsSelected,0) = 1
					GROUP BY a.TransferPromotionID		
				)t15 ON t15.TransferPromotionID = t6.TransferPromotionID
				LEFT OUTER JOIN 
				(	
					SELECT  TransferPromotionID,SUM(CASE PromotionFeeID WHEN '15' THEN Amount / 2 ELSE Amount END) AS PayPrice
					FROM	ZPROM_TransferPromotionFee 
					WHERE   ((PromotionFeeID = '15' AND Charge='N')
							OR (PromotionFeeID IN ('00','01','02','17','2G','37') AND (Charge='N' OR Charge='H')))
					GROUP BY TransferPromotionID		
				)t16 ON t16.TransferPromotionID = t6.TransferPromotionID 				
				----------------------------------------------------------------------------------------------------
				LEFT OUTER JOIN
				(
					SELECT ProductID,UnitNumber,MAX(ImportDate) AS ImportDate 
					FROM ICON_EntForms_SuggestionPrice 
					WHERE MinPriceType in (1)
					GROUP BY ProductID,UnitNumber	
				)t17 ON t17.ProductID = t1.ProductID AND t17.UnitNumber = t1.UnitNumber			
				LEFT OUTER JOIN
				(
					SELECT ProductID,UnitNumber,MAX(ImportDate) AS ImportDate 
					FROM ICON_EntForms_SuggestionPrice 
					WHERE MinPriceType in (2)
					GROUP BY ProductID,UnitNumber	
				)t17_2 ON t17_2.ProductID = t1.ProductID AND t17_2.UnitNumber = t1.UnitNumber			
				LEFT OUTER JOIN ICON_EntForms_SuggestionPrice t18 ON t18.MinPriceType in (1,2)  
																--AND t18.ImportDate = Isnull(t17_2.ImportDate,t17.ImportDate)
																and t18.ImportDate=Case when t17_2.ImportDate Is not null Then t17_2.ImportDate Else t17.ImportDate End 
																and t18.ProductID=Case when t17_2.ImportDate Is not null Then t17_2.ProductID Else t17.ProductID End 
																and t18.UnitNumber=Case when t17_2.ImportDate Is not null Then t17_2.UnitNumber Else t17.UnitNumber End
				----------------------------------------------------------------------------------------------------	
				LEFT OUTER JOIN Z_BudgetBooking t19 on t19.BookingNumber = t5.BookingNumber AND t19.CancelDate IS NULL	
				LEFT OUTER JOIN Z_BudgetBooking t20 on t20.BookingNumber = t11.BookingNumber AND t20.CancelDate IS NULL			
				----------------------------------------------------------------------------------------------------		
				--Where Isnull(t1.DocumentCancel,0)=0
				Where t1.BudgetType not in('Extra')and recalldate is null and rejectdate is null
				and Isnull(t1.DocumentCancel,0)=0
			*/ ) a 
	
	/* WHERE	1=1
			and (a.BU = (CASE WHEN @HomeType IN ('1','2','3','4','5') THEN @HomeType ELSE a.BU END))
			
			AND (isnull(@ProjectGroup,'') = '' or a.ProjectGroup = @ProjectGroup)
			AND (isnull(@ProjectType2,'') = '' or a.ProjectType = @ProjectType2)
	
			AND (a.CompanyID = (CASE WHEN @CompanyID != '' THEN @CompanyID ELSE a.CompanyID END))
			AND (a.ProjectCode = (CASE WHEN @ProductID != '' THEN @ProductID ELSE a.ProjectCode END))
			----------------------------------------------------------------------------------------------------
			AND (Isnull(a.BookingDate,getdate()) BETWEEN @DateStart and @DateEnd)
			AND (Isnull(a.TransferDate,GetDate()) BETWEEN @DateStart2 and @DateEnd2	) 
			AND (a.Status1Type LIKE '%' + (CASE WHEN @DocStatus4Filter != '' THEN @DocStatus4Filter ELSE a.Status1Type END) + '%')
			and (Isnull(@Status4,'0')='0' or Isnull(@Status4,'0')='' or @Status4=Case When @Status4='1'and Status2Type in('3','4','5','1') Then @Status4 Else Status2Type End)
			
	ORDER BY a.BU ASC,a.ProjectCode ASC,a.Unit ASC
		
	select @Status4 */




GO
