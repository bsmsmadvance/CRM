SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec "AP2SP_ExecutiveReportV4_ByBU";1 N'', {ts '2015-01-01 00:00:00'}, {ts '2015-06-30 00:00:00'}, N'ทั้งหมด', N'1', N'', N'', N'คุณ Administrator Account'
--[dbo].[AP2SP_ExecutiveReportV4_ByBU] '10191','2017-01-01','2017-01-31','1','1','1','SH','อริยดา ตึกดี'

CREATE  PROCEDURE [dbo].[AP2SP_ExecutiveReportV4_ByBU] 

	@BatchID nvarchar(4000)=null,
	@DateStart datetime,
	@DateEnd   datetime,
	@HomeType nvarchar(20)=null,
	@StatusProject nvarchar(2)=null,
	@ProjectGroup nvarchar(5)=null,
	@ProjectType2 nvarchar(5)=null,
	@UserName nvarchar(150)=''
	
AS

If(ISNULL(@DateStart,'')='')Set @DateStart='18000101'
If(ISNULL(@DateEnd,'')='')Set @DateEnd='70001231'

DECLARE @DateEndInStore Datetime
SET @DateEndInStore = [dbo].[fn_GetMaxDate](@DateEnd)
Set @BatchID =IsNull(@BatchID,'')
Set @StatusProject =IsNull(@StatusProject,'')
Set @HomeType =IsNull(@HomeType,'')
Set @ProjectGroup =IsNull(@ProjectGroup,'')
Set @ProjectType2 =IsNull(@ProjectType2,'')
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
Declare @sql nvarchar(max)
Set @sql = '

SET DATEFIRST 1;
SELECT	''ProjectName'' = '''' --ISNULL(T.Project,'''')+'' - ''+T.ProjectName
        ,''TotalUnit'' = '''' --T.TotalUnit 
		,''UnitNoBK'' = '''' --ISNULL(A.UnitBKN,0)
		,''UnitEmty''  = '''' --ISNULL(A.UnitBKN,0) ---ห้องว่างที่ยังไม่เคยจอง+ห้องว่างจองแล้วยกเลิก
		,''PriceEmty''  = '''' --ISNULL(A.PriceBKN,0)
		,''Producttype'' = '''' --T.Producttype
        ,''StartSale'' = '''' --PR.StartSale
        ,''PType'' = '''' --PR.PType
		,''ProjectGroup'' = '''' --PR.ProjectGroup
		,''UnitBKW'' = '''' --ISNULL(C.UnitBKW,0)
        ,''PriceBKW'' = '''' --ISNULL(C.PriceBKW,0)	--Week จอง
		,''UnitBKM'' = '''' --ISNULL(D.UnitBKM,0)
        ,''PriceBKM'' = '''' --ISNULL(D.PriceBKM,0)	--Month จอง
		,''UnitBKY'' = '''' --ISNULL(E.UnitBKY,0)
        ,''PriceBKY'' = '''' --ISNULL(E.PriceBKY,0)	--Year จอง
		,''UnitBKP'' = '''' --ISNULL(G.UnitBKP,0)
        ,''PriceBKP'' = '''' --ISNULL(G.PriceBKP,0)	--Project จอง

		,''UnitCBKW'' = '''' --ISNULL(H.UnitCBKW,0)
        ,''PriceCBKW'' = '''' --ISNULL(H.PriceCBKW,0)	--Week จองยกเลิก
		,''UnitCBKM'' = '''' --ISNULL(I.UnitCBKM,0)
        ,''PriceCBKM'' = '''' --ISNULL(I.PriceCBKM,0)	--Month จองยกเลิก
		,''UnitCBKY'' = '''' --ISNULL(J.UnitCBKY,0)
        ,''PriceCBKY'' = '''' --ISNULL(J.PriceCBKY,0)	--Year จองยกเลิก
		,''UnitCBKP'' = '''' --ISNULL(K.UnitCBKP,0)
        ,''PriceCBKP'' = '''' --ISNULL(K.PriceCBKP,0)	--Project จองยกเลิก

		,''UnitNetW'' = '''' --ISNULL(C.UnitBKW,0)-ISNULL(H.UnitCBKW,0)
        ,''PriceNetW'' = '''' --ISNULL(C.PriceBKW,0)-ISNULL(H.PriceCBKW,0)	--Week  จองสุทธิ
		,''UnitNetM'' = '''' --ISNULL(D.UnitBKM,0)-ISNULL(I.UnitCBKM,0)
        ,''PriceNetM'' = '''' --ISNULL(D.PriceBKM,0)-ISNULL(I.PriceCBKM,0)	--Month จองสุทธิ
		,''UnitNetY'' = '''' --ISNULL(E.UnitBKY,0)-ISNULL(J.UnitCBKY,0)
        ,''PriceNetY'' = '''' --ISNULL(E.PriceBKY,0)-ISNULL(J.PriceCBKY,0)	--Year  จองสุทธิ
		,''UnitNetP'' = '''' --ISNULL(G.UnitBKP,0)-ISNULL(K.UnitCBKP,0)
        ,''PriceNetP'' = '''' --ISNULL(G.PriceBKP,0)-ISNULL(K.PriceCBKP,0)	--Project จองสุทธิ

		,''UnitAGDP'' = '''' --ISNULL(L.UnitAGDP,0)
        ,''PriceAGDP'' = '''' --ISNULL(L.PriceAGDP,0)	--Project ค้างสัญญา

		,''UnitTFW'' = '''' --ISNULL(M.UnitTFW,0)
        ,''PriceTFW'' = '''' --ISNULL(M.PriceTFW,0) --Week โอน
		,''UnitTFM'' = '''' --ISNULL(N.UnitTFM,0)
        ,''PriceTFM'' = '''' --ISNULL(N.PriceTFM,0) --Month โอน
		,''UnitTFY'' = '''' --ISNULL(O.UnitTFY,0)
        ,''PriceTFY'' = '''' --ISNULL(O.PriceTFY,0) --Year โอน
		,''UnitTFP'' = '''' --ISNULL(P.UnitTFP,0)
        ,''PriceTFP'' = '''' --ISNULL(P.PriceTFP,0) --Project โอน

		,''UnitNTF'' = '''' --ISNULL(R.UnitNTF,0)
        ,''PriceNTF'' = '''' --ISNULL(R.PriceNTF,0)---ค้างโอน

From	[PRJ].[Unit] U ' --This is temp table actual table start from below
		/* (--ห้องทั้งหมด	
			Select	PR.Project AS ProjectName,PR.Producttype
					,U.ProductID AS Project,Count(U.UnitNumber)AS TotalUnit
			FROM	ICON_EntForms_Unit U  
					LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = U.ProductID
			WHERE	U.AssetType IN (2,4) '

SET @sql=@sql+ '			GROUP BY	PR.Project,U.ProductID,PR.Producttype
		)T
		LEFT OUTER JOIN     ----ห้องว่าง   
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitBKN,B.Project,
				ISNULL(SUM(B.UnitPrice),0)/1000000 AS PriceBKN
			From	vw_RPTAP2_ExV3Emty B
			GROUP BY B.Project
		)A ON A.Project = T.Project ' 

--=========================จอง==========================================================================
set @sql= @sql+ '
		LEFT OUTER JOIN   ----ห้องจอง Week
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitBKW,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceBKW,B.Project
			From	vw_RPTAP2_ExV4Booking B
			Where	(B.WeekAmt = DATEPART(Week,'''+CONVERT(VARCHAR(50),@DateEnd,120)+''') AND Year(B.BookingDate) = Year('''+CONVERT(VARCHAR(50),@DateEnd,120)+'''))
					AND B.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''			
			GROUP BY B.Project
		)C ON C.Project = T.Project
		LEFT OUTER JOIN  ----ห้องจอง Month
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitBKM,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceBKM,B.Project
			From	vw_RPTAP2_ExV4Booking B
			Where   (B.MonthAmt = DatePart(Mm,'''+CONVERT(VARCHAR(50),@DateEnd,120)+''') AND Year(B.BookingDate) = Year('''+CONVERT(VARCHAR(50),@DateEnd,120)+'''))
					AND B.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY B.Project
		)D ON D.Project = T.Project
		LEFT OUTER JOIN  ----ห้องจอง Year
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitBKY,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceBKY,B.Project
			From	vw_RPTAP2_ExV4Booking B
			Where   Year(B.BookingDate) =  year('''+CONVERT(VARCHAR(50),@DateEnd,120)+''') 
					AND B.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY B.Project
		)E ON E.Project = T.Project
		LEFT OUTER JOIN  ----ห้องจอง Project
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitBKP,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceBKP,B.Project
			From	vw_RPTAP2_ExV4Booking B 
			Where	B.BookingDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY B.Project
		)G ON G.Project = T.Project

--=========================ยกเลิกจอง==========================================================================
		LEFT OUTER JOIN   ----ยกเลิกจอง Week
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitCBKW,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceCBKW,B.Project
			From	vw_RPTAP2_ExV4BookingCancel B
			Where	(B.WeekAmt1 = DATEPART(Week,'''+CONVERT(VARCHAR(50),@DateEnd,120)+''') AND Year(B.CancelDate) = Year('''+CONVERT(VARCHAR(50),@DateEnd,120)+'''))
					AND B.CancelDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''' 			
			GROUP BY B.Project
		)H ON H.Project = T.Project
		LEFT OUTER JOIN  ----ยกเลิกจอง Month
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitCBKM,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceCBKM,B.Project
			From	vw_RPTAP2_ExV4BookingCancel B
			Where   (B.MonthAmt1 = DatePart(Mm,'''+CONVERT(VARCHAR(50),@DateEnd,120)+''') AND Year(B.CancelDate) = year('''+CONVERT(VARCHAR(50),@DateEnd,120)+'''))
					AND B.CancelDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY B.Project
		)I ON I.Project = T.Project
		LEFT OUTER JOIN  ----ยกเลิกจอง Year
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitCBKY,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceCBKY,B.Project
			From	vw_RPTAP2_ExV4BookingCancel B
			Where   Year(B.CancelDate) = year('''+CONVERT(VARCHAR(50),@DateEnd,120)+''') 
					AND B.CancelDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY B.Project
		)J ON J.Project = T.Project
		LEFT OUTER JOIN  ----ยกเลิกจอง Project
		(
			Select	ISNULL(SUM(B.UnitAmt),0) AS UnitCBKP,ISNULL(Sum(B.BKPrice),0)/1000000 AS PriceCBKP,B.Project
			From	vw_RPTAP2_ExV4BookingCancel B
			Where	B.CancelDate <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY B.Project
		)K ON K.Project = T.Project '
set @sql= @sql+ '

--==========================ค้างสัญญา=====================================================================
		LEFT OUTER JOIN
		(
			Select	ISNULL(SUM(UnitAmt),0) AS UnitAGDP,ISNULL(Sum(BKPrice),0)/1000000 AS PriceAGDP,Project
			From	vw_RPTAP2_ExV4NoContract
			Where	BookingDate <=  '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''' 
					And (CancelDateBK IS NULL OR (CancelDateBK IS NOT NULL AND CancelDateBK > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''))
					And (CancelDateAG IS NULL  OR (CancelDateAG IS NOT NULL AND CancelDateAG  > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''')) 
			GROUP BY Project
		)L ON L.Project = T.Project

--==========================โอน========================================================================

		LEFT OUTER JOIN			----Week โอน
		(
			Select	ISNULL(SUM(T.UnitAmt),0) AS UnitTFW,ISNULL(Sum(T.TFPrice),0)/1000000 AS PriceTFW,T.Project
			From	vw_RPTAP2_ExV3Transfer T
			Where	(T.WeekAmt = DATEPART(Week,'''+CONVERT(VARCHAR(50),@DateEnd,120)+''') AND Year(T.TransferDateApprove) = Year('''+CONVERT(VARCHAR(50),@DateEnd,120)+'''))
					AND T.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''			
			GROUP BY T.Project
		)M ON M.Project = T.Project
		LEFT OUTER JOIN			----Month โอน
		(
			Select	ISNULL(SUM(T.UnitAmt),0) AS UnitTFM,ISNULL(Sum(T.TFPrice),0)/1000000 AS PriceTFM,T.Project
			From	vw_RPTAP2_ExV3Transfer T
			Where   (T.MonthAmt = DatePart(Mm,'''+CONVERT(VARCHAR(50),@DateEnd,120)+''') 
					AND Year(T.TransferDateApprove) = year('''+CONVERT(VARCHAR(50),@DateEnd,120)+'''))
					AND T.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY T.Project
		)N ON N.Project = T.Project
		LEFT OUTER JOIN			----Year โอน
		(
			Select	ISNULL(SUM(T.UnitAmt),0) AS UnitTFY,ISNULL(Sum(T.TFPrice),0)/1000000 AS PriceTFY,T.Project
			From	vw_RPTAP2_ExV3Transfer T
			Where   Year(T.TransferDateApprove) = Year('''+CONVERT(VARCHAR(50),@DateEnd,120)+''') 
					AND T.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY T.Project
		)O ON O.Project = T.Project
		LEFT OUTER JOIN			----Project โอน
		(
			Select	ISNULL(SUM(T.UnitAmt),0) AS UnitTFP,ISNULL(Sum(T.TFPrice),0)/1000000 AS PriceTFP,T.Project
			From	vw_RPTAP2_ExV3Transfer T
			Where	T.TransferDateApprove <= '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''
			GROUP BY T.Project
		)P ON P.Project = T.Project
--==========================ค้างโอน=====================================================================

		LEFT OUTER JOIN
		(
			Select	ISNULL(SUM(C.UnitAmt),0) AS UnitNTF
					,ISNULL(Sum(C.TFPrice),0)/1000000 AS PriceNTF
					 ,C.Project
			From	vw_RPTAP2_ExV4NoTransfer C
			Where	BookingDate <=  '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+''' And (CancelDateBK IS NULL OR (CancelDateBK IS NOT NULL AND CancelDateBK > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''))
					And (CancelDateAG IS NULL  OR (CancelDateAG IS NOT NULL AND CancelDateAG  > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''))
					And (TransferDateApprove IS NULL OR (TransferDateApprove IS NOT NULL And TransferDateApprove > '''+CONVERT(VARCHAR(50),@DateEndInStore,120)+'''))
            GROUP BY C.Project
		)R ON R.Project = T.Project   
		LEFT OUTER JOIN [ICON_EntForms_Products] PR ON PR.ProductID = T.Project
		


WHERE	1 = 1 '
		IF(ISNULL(@BatchID,'')<>'')set @sql=@sql+' AND(T.Project IN (SELECT * FROM [dbo].[fn_SplitString]('''+@BatchID+''','','')))'
		
		IF(@StatusProject = '1')set @sql=@sql+' and(PR.RTPExcusive = 1)'
		IF(@StatusProject = '2')set @sql=@sql+' and(PR.RTPExcusive = 2)'
		IF(@StatusProject = '3')set @sql=@sql+' and(PR.RTPExcusive = 3)'
		IF(@StatusProject = '4')set @sql=@sql+' and(PR.RTPExcusive IN (1,2))'
		
		IF(ISNULL(@HomeType,'')<>'' AND ISNULL(@HomeType,'')<>'ทั้งหมด') set @sql=@sql+' and(PR.PType = ''' + @HomeType + ''')'
		
		SET @sql=@sql+ ' AND EXISTS(SELECT ProductID FROM [dbo].[fn_GetProjectAuthorised](''' + @UserName + ''') WHERE ProductID=T.Project) '

		IF(ISNULL(@ProjectGroup,'')<>'') 
			set @sql=@sql+' AND (PR.ProjectGroup = ''' + @ProjectGroup + ''')'
			
		IF(ISNULL(@ProjectType2,'')<>'') 
			set @sql=@sql+' AND (PR.ProjectType = ''' + @ProjectType2 + ''')'

		set @sql=@sql+'Order By PR.PType,PR.ProjectGroup,T.ProjectName,T.Project' */

		exec(@sql)
		--print(@sql)
		--SELECT (@sql)

GO
