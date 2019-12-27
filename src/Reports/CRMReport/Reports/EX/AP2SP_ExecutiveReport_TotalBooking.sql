SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- EXEC AP2SP_ExecutiveReport_TotalBooking '10049','','','',''
-- exec "AP2SP_ExecutiveReport_TotalBooking";1 '''''', '20180101', '20181115', '', N'อริยดา ตึกดี'

ALTER Proc [dbo].[AP2SP_ExecutiveReport_TotalBooking]
@Projects varchar(500)=''
,@DateStart DateTime
,@DateEnd DateTime
,@StatusProject varchar(50)=''
,@UserName nvarchar(150)=''
,@Trans varchar(5)=''
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
BEGIN

	Set @Projects=Replace(@Projects,'''','')
	If (@StatusProject='4' or @StatusProject is null) Set @StatusProject='1,2'
	If (@DateStart Is Null)Set @DateStart='18000101'
	If (@DateEnd Is Null OR Year(@DateEnd)=1900)Set @DateEnd='70000101'


	/* If(Object_Id('tempdb..#vw_RPTAP2_ExV4Booking_ExecutiveReport_Subtract')Is Not Null)Drop Table #vw_RPTAP2_ExV4Booking_ExecutiveReport_Subtract
	SELECT *
	INTO #vw_RPTAP2_ExV4Booking_ExecutiveReport_Subtract 
	FROM dbo.vw_RPTAP2_ExV4Booking_ExecutiveReport_Subtract With(NoLock)
	Where 1=1 
		AND (Isnull(@Projects,'')='' Or ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))

	If(Object_Id('tempdb..#Temp_Subtract')Is Not Null)Drop Table #Temp_Subtract
	SELECT ProductID
	,'SubtractGrossBookingAmount'=Sum(SubtractGrossBookingAmount) ,'SubtractGrossBookingUnit'=Sum(SubtractGrossBookingUnit)
	INTO #Temp_Subtract 
	FROM #vw_RPTAP2_ExV4Booking_ExecutiveReport_Subtract With(NoLock)
	Where 1=1 
		AND (Isnull(@Projects,'')='' Or ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
		and BookingDate between @DateStart and @DateEnd
	Group by ProductID

	If(Object_Id('tempdb..#Temp_Subtract_Cancel')Is Not Null)Drop Table #Temp_Subtract_Cancel
	SELECT ProductID
	,'SubtractCancelAmount'=Sum(SubtractCancelAmount) ,'SubtractCancelUnit'=Sum(SubtractCancelUnit)
	INTO #Temp_Subtract_Cancel 
	FROM #vw_RPTAP2_ExV4Booking_ExecutiveReport_Subtract With(NoLock)
	Where 1=1 
		AND (Isnull(@Projects,'')='' Or ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
		and Convert(nvarchar(8),BookingCancelDate,112) between @DateStart and @DateEnd
		and CurrentStatus='ยกเลิก'
	Group by ProductID 

	
    IF (ISNULL(@Trans, '') = '') SET @Trans = '0'

	Declare @Booking Table (UnitAmt Int,BKPrice Money,Project nvarchar(100),BookingDate DateTime)
	Insert Into @Booking (UnitAmt,BKPrice,Project,BookingDate)
	Select	ISNULL(SUM(B.UnitAmt),0) AS TotalUnit,ISNULL(Sum(B.BKPrice),0)  AS TotalPrice,B.Project ProductID,b.BookingDate
	From	[vw_RPTAP2_ExV4Booking_ExecutiveReport] B With(NoLock)
	Where	1=1 
	AND (Isnull(@Projects,'')='' Or B.Project In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
	GROUP BY B.Project,b.BookingDate


	Declare @BookingCancel Table (UnitAmt Int,BKPrice Money,Project nvarchar(100),CancelDate DateTime)
	Insert Into @BookingCancel (UnitAmt,BKPrice,Project,CancelDate)
	Select	ISNULL(SUM(B.UnitAmt),0) AS TotalUnit,ISNULL(Sum(B.BKPrice),0) AS TotalPrice,B.Project ProductID,CancelDate
	From	[vw_RPTAP2_ExV4BookingCancel] B With(NoLock)
	Where	1=1 
		AND (Isnull(@Projects,'')='' Or B.Project In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
	GROUP BY B.Project,CancelDate */

	/*หา Last Minprice ROI แต่ละ Unit*/
	/* If(Object_Id('tempdb..#TMin')Is Not Null)Drop Table #TMin

	Select ProductID, UnitNumber, MAX(ImportDate) AS MaxDate,IsNull(MinPriceType,1)MinPriceType
	Into #TMin
	FROM dbo.ICON_EntForms_SuggestionPrice With(NoLock)
	WHERE (ImportDate <= @DateEnd) And IsNull(MinPriceType,1)=2
	GROUP BY ProductID, UnitNumber,IsNull(MinPriceType,1)
	Order By ProductID, UnitNumber

	Insert Into #TMin
	Select ProductID, UnitNumber, MAX(ImportDate) AS MaxDate,IsNull(MinPriceType,1)MinPriceType
	FROM dbo.ICON_EntForms_SuggestionPrice a With(NoLock)
	WHERE (ImportDate <= @DateEnd) And IsNull(MinPriceType,1)=1 And Not Exists(Select * From #TMin t Where t.ProductID=a.ProductID And t.UnitNumber=a.UnitNumber)
	GROUP BY ProductID, UnitNumber,IsNull(MinPriceType,1)
	Order By ProductID, UnitNumber

	Declare @Empty Table(TotalPrice money,TotalUnit Int,ProductID varchar(50))
	Insert Into @Empty (ProductID,TotalUnit,TotalPrice)
	SELECT UN.ProductID AS Project, COUNT(UN.UnitNumber) AS UnitAmt, ISNULL(SUM(P.TotalSellingPrice), 0)/1000000 AS UnitPrice
	FROM dbo.ICON_EntForms_Unit AS  UN With(NoLock) LEFT OUTER JOIN
	(
		SELECT A.UnitNumber, A.ProductID, A.ImportDate, A.SuggestionPrice TotalSellingPrice
		FROM dbo.ICON_EntForms_SuggestionPrice AS A With(NoLock) INNER JOIN
			(
				Select Distinct ProductID, UnitNumber,MaxDate From #TMin
			) AS B ON A.ProductID = B.ProductID AND A.UnitNumber = B.UnitNumber AND B.MaxDate = A.ImportDate
		WHERE 1=1
			AND (Isnull(@Projects,'')='' Or A.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
	) AS P ON P.ProductID = UN.ProductID AND P.UnitNumber = UN.UnitNumber
	WHERE (UN.AssetType IN (2, 4)) 
		AND (Isnull(@Projects,'')='' Or UN.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
		AND Not Exists (Select * From ICON_EntForms_Booking b With(NoLock) Where BookingDate<=@DateEnd and (CancelDate Is Null Or Convert(NVarchar(8),CancelDate,112)>@DateEnd )and b.ProductID=UN.ProductID and UN.UnitNumber=b.UnitNumber)
	GROUP BY UN.ProductID

	Declare @Total Table(TotalPrice money,ProductID nvarchar(50))
	Insert Into @Total
	Select Isnull(b.TotalPrice,0)-Isnull(bc.TotalPrice,0)
	,b.ProductID
	From 
	(
		Select	ISNULL(SUM(B.UnitAmt),0) AS TotalUnit,Round(ISNULL(Sum(B.BKPrice),0)/1000000,2) AS TotalPrice,B.Project ProductID
		From	@Booking B
		Where	1=1 AND B.BookingDate Between '18000101' and @DateEnd 
		GROUP BY B.Project
	)B Left Join(
		Select	ISNULL(SUM(BC.UnitAmt),0) AS TotalUnit,Round(ISNULL(Sum(BC.BKPrice),0)/1000000,2) AS TotalPrice,BC.Project ProductID
		From	@BookingCancel BC
		Where	1=1	AND BC.CancelDate Between '18000101' and @DateEnd 
		GROUP BY BC.Project
	)BC on b.ProductID=bc.ProductID */

If(Object_ID('tempdb..#Temp')Is not null) DROP table #Temp

	Select 'ProductID' = '' --p.ProductID
    ,'Project' = '' --p.Project
	,'BU' = '' --p.PType BU
	,'StartSale' = '' --p.StartSale
	,'TotalUnit' = '' --Isnull((Select	Count(U.UnitNumber)FROM	ICON_EntForms_Unit U  WHERE	U.AssetType IN (2,4) and u.ProductID=p.ProductID ),0)
	,'TotalPrice' = '' --Round(Isnull((Select TotalPrice From @Total t Where t.ProductID=p.ProductID),0),2)+Round((Isnull((Select	ISNULL((v.TotalPrice),0) AS TotalPrice From	@Empty v Where v.ProductID=p.ProductID),0)),2)
	,'EmptyUnit' = '' --Isnull((Select	ISNULL((v.TotalUnit),0) From	@Empty v Where v.ProductID=p.ProductID),0)
	,'EmptyPrice' = '' --Round(Isnull((Select	ISNULL((v.TotalPrice),0)  From	@Empty v Where v.ProductID=p.ProductID),0),2)
	,'BookingTotalPrice' = '' --Round(Isnull(b.TotalPrice,0)/1000000,2)
    ,'BookingTotalUnit' = '' --Isnull(b.TotalUnit,0)
	,'CancelTotalPrice' = '' --Round(Isnull(bc.TotalPrice,0)/1000000*-1,2)
    ,'CancelTotalUnit' = '' --Isnull(bc.TotalUnit,0)*-1
	,'ContractPrice'= '' --convert(decimal(18,2),0)--ROUND(Isnull(a.TotalPrice,0)/1000000,2)
	,'TransferDiscount'= '' --convert(decimal(18,2),0)--ROUND(Isnull(tp.TotalPrice,0)/1000000*-1,2)
	,'AreaPrice'= '' --convert(decimal(18,2),0)-- ROUND(Isnull(t.AreaPrice,0)/1000000,2)
	,'ExtraDiscount'= '' --convert(decimal(18,2),0)--ROUND(Isnull(t.ExtraDiscount,0),2)
	,'TransferTotalPrice' = '' --Round((Isnull(t.TotalPrice,0)),2)
    ,'TransferTotalUnit' = '' --Isnull(t.TotalUnit,0)
	,'PTDBookingTotalPrice' = '' --Round(Isnull(bb.TotalPrice,0),2)
    ,'PTDBookingTotalUnit' = '' --Isnull(bb.TotalUnit,0)
	,'PTDTransferTotalPrice' = '' --Round(Isnull(tt.TotalPrice,0),2)
    ,'PTDTransferTotalUnit' = '' --Isnull(tt.TotalUnit,0)
	,'ProjectGroup' = '' --p.ProjectGroup
	INTO #Temp
	From [PRJ].[Project] p --This is acutal table need to use table below as well
	/* Left Join
	(
		Select	ISNULL(SUM(B.UnitAmt),0)- Isnull((Select (SubtractGrossBookingUnit) From #Temp_Subtract t with(nolock) Where t.ProductID=b.Project),0) AS TotalUnit
		,ISNULL(Sum(B.BKPrice),0)- Isnull((Select (SubtractGrossBookingAmount) From #Temp_Subtract t with(nolock)Where t.ProductID=b.Project),0) AS TotalPrice
		,B.Project ProductID
		From	@Booking B
		Where	1=1 AND B.BookingDate Between @DateStart and @DateEnd 
		GROUP BY B.Project
	)B ON p.ProductID=b.ProductID Left Join 
	(
		Select	'TotalUnit'=ISNULL(SUM(BC.UnitAmt),0)- Isnull((Select (SubtractCancelUnit) From #Temp_Subtract_Cancel t with(nolock) Where t.ProductID=bc.Project),0)
		,'TotalPrice'=ISNULL(Sum(BC.BKPrice),0)- Isnull((Select (SubtractCancelAmount) From #Temp_Subtract_Cancel t with(nolock) Where t.ProductID=bc.Project),0)
		,BC.Project ProductID
		From	@BookingCancel BC
		Where	1=1	AND BC.CancelDate Between @DateStart and @DateEnd 
			and (Isnull(@Projects,'')='' Or BC.Project In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
		GROUP BY BC.Project
	)BC ON p.ProductID=bc.ProductID Left Join
	(
		Select Round(Sum(Isnull(t.NetSalePrice,0)-isnull(c.FreedownAmount,0))/1000000,2)TotalPrice,Count(t.ContractNumber)TotalUnit,Sum(t.IncreasingAreaPrice)AreaPrice,round(Sum(t.ExtraPayment)/1000000,2)+round(Sum(t.ExtraDiscount)/1000000,2) ExtraDiscount,a.ProductID
		From ICON_EntForms_Transfer t with(nolock)
		left join ICON_EntForms_Agreement a with(nolock)on t.ContractNumber = a.ContractNumber
		Left Join CRM_Freedown c with(nolock)on c.DocumentID=t.ContractNumber and c.DocumentType=2
		Where 1=1 and t.TransferDateApprove Is Not Null AND t.TransferDateApprove Between @DateStart and @DateEnd and Isnull(t.Approve3,0)=1 
			and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
		Group By a.ProductID
	)t ON p.ProductID=t.ProductID Left Join
	(
		Select Isnull(b.TotalPrice,0)+Isnull(a.TotalPrice,0)-Isnull(tp.TotalPrice,0)+Isnull(t.ExtraDiscount,0)+isnull(t.AreaPrice,0) TotalPrice,isnull(b.TotalUnit,0) TotalUnit,p.ProductID
		From ICON_EntForms_Products p Left Join
		(
			Select Count(unitnumber) AS TotalUnit,Round(ISNULL(Sum(IsNull(B.TotalSellingPrice, 0)-isnull(c.FreedownAmount,0)+ IsNull(B.FurniturePrice, 0) - IsNull(B.TransferDiscount, 0)- IsNull(B.CashDiscount, 0)),0)/1000000,2) AS TotalPrice,B.ProductID ProductID
			From ICON_EntForms_Booking b with(nolock)
			Left Join CRM_Freedown c with(nolock)on c.DocumentID=b.BookingNumber and c.DocumentType=1
			Where 1=1
			and  Convert(NVarchar(8),B.BookingDate,112) Between '18000101' and @DateEnd 
			And (canceldate Is Null or
				b.BookingNumber Not In(Select t.BookingNumber From ICON_EntForms_Booking t Where t.ProductID=b.ProductID and Convert(NVarchar(8),t.canceldate,112) Between '18000101' and @DateEnd))
			and b.BookingNumber not in(Select t.BookingNumber From CRM_Freedown_Cancel t where t.Canceltype='ยกเลิกพิเศษ' and t.CancelDate<=@DateEnd)
			GROUP BY B.ProductID
		)b on p.ProductID=b.ProductID Left Join
		(	
			Select Round(Sum(a.TotalSellingPrice-Isnull(c.FreedownAmount,0)-ISNULL(a.CashDiscount,0)-Isnull(a.TransferDiscount,0)+Isnull(a.FurniturePrice,0)-ISNULL(a.LastDownAmount,0)-(b.TotalSellingPrice-Isnull(d.FreedownAmount,0)-ISNULL(b.CashDiscount,0)-Isnull(b.TransferDiscount,0)+IsNull(b.FurniturePrice,0)))/1000000,2) TotalPrice,Count(a.ContractNumber)TotalUnit,a.ProductID
			From ICON_EntForms_Agreement a with(nolock)Left Join 
				ICON_EntForms_Booking b with(nolock)on a.BookingNumber=b.BookingNumber
			Left Join CRM_Freedown c with(nolock)on c.DocumentID=a.ContractNumber and c.DocumentType=2
			Left Join CRM_Freedown d with(nolock)on d.DocumentID=a.BookingNumber and d.DocumentType=1
			Where 1=1 AND a.ApproveDate Between '18000101' and @DateEnd and (a.CancelDate Is Null OR a.CancelDate>@DateEnd)
				and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
				and a.BookingNumber not in(Select t.BookingNumber From CRM_Freedown_Cancel t with(nolock)where t.Canceltype='ยกเลิกพิเศษ'and t.CancelDate<=@DateEnd)
			Group By a.ProductID
		)a on p.ProductID=a.ProductID Left Join
		(
			Select Round(Sum(t.phusadiscount-Isnull(a.TransferDiscount,0)-ISNULL(a.LastDownAmount,0))/1000000,2)TotalPrice,Count(t.ContractNumber)TotalUnit,a.ProductID
			From ICON_EntForms_Transfer t with(nolock)Left Join 
			ICON_EntForms_Agreement a with(nolock)on t.ContractNumber = a.ContractNumber and (a.CancelDate Is Null OR a.CancelDate>@DateEnd)
			Where 1=1 and t.TransferDateApprove Is Not Null AND t.TransferDateApprove Between '18000101' and @DateEnd and Isnull(t.Approve3,0)=1 
			and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
			Group By a.ProductID
		)tp ON p.ProductID=tp.ProductID Left Join
		(
			Select Sum(Isnull(t.NetSalePrice,0)-Isnull(c.FreedownAmount,0))TotalPrice,Count(t.ContractNumber)TotalUnit,round(Sum(t.IncreasingAreaPrice)/1000000,2)AreaPrice,round(Sum(t.ExtraPayment)/1000000,2)+round(Sum(t.ExtraDiscount)/1000000,2) ExtraDiscount,a.ProductID
			From ICON_EntForms_Transfer t with(nolock)
			left join ICON_EntForms_Agreement a with(nolock)on t.ContractNumber = a.ContractNumber
			Left Join CRM_Freedown c with(nolock)on c.DocumentID=a.ContractNumber and c.DocumentType=2
			Where 1=1 and t.TransferDateApprove Is Not Null AND t.TransferDateApprove Between '18000101' and @DateEnd and Isnull(t.Approve3,0)=1 
			and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
			Group By a.ProductID
		)t ON p.ProductID=t.ProductID
		Where (Isnull(@Projects,'')='' Or p.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
			and (Isnull(@StatusProject,'')='' Or p.RTPExcusive In(SELECT * FROM [dbo].[fn_SplitString](@StatusProject,',')))
	)bb ON p.ProductID=bb.ProductID Left Join
	(
		Select round(Sum(Isnull(t.NetSalePrice,0)-Isnull(c.FreedownAmount,0))/1000000,2)TotalPrice,round(Sum(t.IncreasingAreaPrice)/1000000,2)AreaPrice,round(Sum(t.ExtraPayment)/1000000,2)+round(Sum(t.ExtraDiscount)/1000000,2) ExtraDiscount,Count(t.ContractNumber)TotalUnit,a.ProductID
		From ICON_EntForms_Transfer t with(nolock)
		left join ICON_EntForms_Agreement a with(nolock)on t.ContractNumber = a.ContractNumber
		Left Join CRM_Freedown c with(nolock)on c.DocumentID=a.ContractNumber and c.DocumentType=2
		Where 1=1 and t.TransferDateApprove Is Not Null AND t.TransferDateApprove Between '18000101' and @DateEnd and Isnull(t.Approve3,0)=1 
		and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
		Group By a.ProductID
	)tt ON p.ProductID=tt.ProductID
	Where 1=1
		AND (p.ProductID IN (SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](@UserName))or isnull(@UserName,'')='' or isnull(@UserName,'')='Administrator')
		and (Isnull(@Projects,'')='' Or p.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
		and (Isnull(@StatusProject,'')='' Or p.RTPExcusive In(SELECT * FROM [dbo].[fn_SplitString](@StatusProject,',')))
	Order by 
	p.ProjectGroup
	,p.PType
	,p.Project
    ,p.ProductID */
	
	UPDATE #Temp
	SET ContractPrice = '' /* ISNULL((Select Round(ISNULL(Sum((a.TotalSellingPrice-ISNULL(a.CashDiscount,0)-Isnull(a.TransferDiscount,0)+IsNull(a.FurniturePrice,0)-ISNULL(a.LastDownAmount,0)-Isnull(contractFreedown.FreeDownAmount,0))-((b.TotalSellingPrice-ISNULL(b.CashDiscount,0)-Isnull(b.TransferDiscount,0)+IsNull(b.FurniturePrice,0) -Isnull(bookFreedown.FreeDownAmount,0)))),0)/1000000,2)
						From ICON_EntForms_Agreement a Left Join 
						ICON_EntForms_Booking b on a.BookingNumber=b.BookingNumber
						LEFT JOIN dbo.ICON_EntForms_Transfer t ON a.ContractNumber = t.ContractNumber

						left join CRM_FreeDown contractFreedown on a.ContractNumber=contractFreedown.DocumentID
						left join CRM_FreeDown bookFreedown on a.bookingnumber=bookFreedown.DocumentID
						Where 1=1 
						AND a.ProductID = aa.ProductID
						AND a.ApproveDate Between @DateStart and @DateEnd and (a.CancelDate Is Null OR a.CancelDate>@DateEnd)
						and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
						AND ((@Trans = '1' AND t.TransferDateApprove IS NOT NULL ) OR (@Trans = '0' AND t.TransferDateApprove IS  NULL) OR (ISNULL(@Trans, '') = '2' ))						
						Group By a.ProductID),0) */
	,TransferDiscount = '' /* ISNULL((Select Round(ISNULL(Sum(t.phusadiscount-Isnull(a.TransferDiscount,0)-ISNULL(a.LastDownAmount,0)),0)/1000000,2)
						From ICON_EntForms_Transfer t Left Join 
						ICON_EntForms_Agreement a on t.ContractNumber = a.ContractNumber and (a.CancelDate Is Null OR a.CancelDate>@DateEnd)
						Where 1=1 and t.TransferDateApprove Is Not Null AND t.TransferDateApprove Between @DateStart and @DateEnd and Isnull(t.Approve3,0)=1 
							and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
							And IsNull(t.PhusaDiscount,0)>0
							AND a.ProductID = aa.ProductID
							AND ((@Trans = '1' AND t.TransferDateApprove IS NOT NULL ) OR (@Trans = '0' AND t.TransferDateApprove IS  NULL) OR (ISNULL(@Trans, '') = '2' ))	
						Group By a.ProductID),0) */
	,AreaPrice = '' /* isnull((Select Round(ISNULL(Sum(t.IncreasingAreaPrice),0)/1000000,2)
				From ICON_EntForms_Transfer t
				left join ICON_EntForms_Agreement a on t.ContractNumber = a.ContractNumber
				Where 1=1 and t.TransferDateApprove Is Not Null AND t.TransferDateApprove Between @DateStart and @DateEnd --and Isnull(t.Approve3,0)=1 
					and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
					AND a.ProductID = aa.ProductID
					AND ((@Trans = '1' AND t.TransferDateApprove IS NOT NULL and Isnull(t.Approve3,0)=1  ) OR (@Trans = '0' AND t.TransferDateApprove IS  NULL) OR (ISNULL(@Trans, '') = '2' ))	
				Group By a.ProductID),0) */
	,ExtraDiscount = '' /* ISNULL((Select round(Sum(t.ExtraPayment)/1000000,2)+round(Sum(t.ExtraDiscount)/1000000,2) 
				From ICON_EntForms_Transfer t
				left join ICON_EntForms_Agreement a on t.ContractNumber = a.ContractNumber
				Where 1=1 and t.TransferDateApprove Is Not Null AND t.TransferDateApprove Between @DateStart and @DateEnd --and Isnull(t.Approve3,0)=1 
					and (Isnull(@Projects,'')='' Or a.ProductID In(SELECT * FROM [dbo].[fn_SplitString](@Projects,',')))
					AND a.ProductID = aa.ProductID
					AND ((@Trans = '1' AND t.TransferDateApprove IS NOT NULL and Isnull(t.Approve3,0)=1  ) OR (@Trans = '0' AND t.TransferDateApprove IS  NULL) OR (ISNULL(@Trans, '') = '2' ))	
				Group By a.ProductID),0) */
	FROM #Temp aa

	SELECT * FROM #temp ORDER BY 
	ProjectGroup
	,BU
	,Project
    ,ProductID

	if(object_id('tempdb..##temp_New')is not null)drop table ##temp_New
	Select 'RecType'='New',* into ##temp_New From #temp ORDER BY ProjectGroup,BU,Project,ProductID
	

End
---------------------------------------------



GO
