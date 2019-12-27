SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_007] NULL,'10080' ,'2012-11-12','2012-11-12','','C04','ทั้งหมด','0'
--[AP2SP_RP_LC_007] NULL,NULL,'2010-01-01','2010-03-19',NULL,NULL,'ทั้งหมด','0'
--[AP2SP_RP_LC_007] 'F',NULL,'2012-03-27','2012-03-31',NULL,NULL,'ทั้งหมด','4'

ALTER PROCEDURE [dbo].[AP2SP_RP_LC_007]
     @CompanyID  nvarchar(20)     
    , @ProductID  nvarchar(20)
	, @DateStart datetime
	, @DateEnd   datetime
    , @UserName nvarchar(150)
    , @UnitNumber nvarchar(50)
    , @StatusTransfer nvarchar(20)
	, @Change2 nvarchar(20)

AS

DECLARE @DateEndInStore Datetime
SET @DateEndInStore  = [dbo].[fn_GetMaxDate](@DateEnd)

DECLARE @sql nvarchar(MAX)
Set @sql= '
SELECT  ''CompanyName'' = '''' --C.NameTH
        , ''ProductID'' = '''' --P.ProjectNo 
		, ''ProjectName'' ='''' -- P.ProjectNameTH
        , ''OperateDate'' = '''' --UH.OperateDate
        , ''Type'' = '''' /* CASE UH.OperateType	WHEN ''A'' THEN ''เพิ่มชื่อ''
											WHEN ''B'' THEN ''สละชื่อ''
											WHEN ''T'' THEN ''โอนสิทธิ์'' 
											WHEN ''BN''THEN ''ย้ายแปลง'' END */
		, ''OperateType'' = '''' --UH.OperateType 
-- ข้อมูลเก่า
		, ''BookingOld'' =  '''' --UH.ReferentID 
		, ''UnitNumberOld'' = '''' --UH.UnitNumber
        , ''TypeOld'' =   '''' --M1.TypeOfRealEstate  
        , ''BookingDateOld'' = '''' --B1.BookingDate             
        , ''CustNameOld'' =  '''' /* CASE UH.OperateType	WHEN ''BN'' THEN [dbo].[fn_GenCustBookingAll] (UH.ReferentID) 
													ELSE [dbo].[fn_GenCustBBefore_A] (UH.ReferentID,UH.OldData) END */
        , ''PriceOld'' = '''' --B1.SellingPrice

-- ข้อมูลใหม่  
		, ''BookingNew'' =  '''' /* CASE WHEN UH.OperateType = ''BN'' THEN B3.BookingNumber 
                                 ELSE UH.ReferentID END */
        , ''UnitNumberNew'' = '''' /* CASE UH.OperateType	WHEN ''BN'' THEN B3.UnitNumber 
													ELSE  UH.UnitNumber END */
        , ''TypeNew'' = '''' /* CASE UH.OperateType WHEN ''BN'' THEN M3.TypeOfRealEstate
                                            ELSE M1.TypeOfRealEstate  END */
        , ''BookingDateNew'' = '''' /* CASE UH.OperateType	WHEN ''BN''THEN B3.BookingDate 
													ELSE B1.BookingDate END */             
        , ''CustNameNew'' =  '''' /* CASE UH.OperateType WHEN ''BN'' THEN [dbo].[fn_GenCustBookingAll] (B3.BookingNumber) 
                                                 ELSE [dbo].[fn_GenCustBBefore_A] (UH.ReferentID,UH.NewData) END */

        , ''PriceNew'' = '''' /* CASE UH.OperateType WHEN ''BN'' THEN B3.SellingPrice
                                             ELSE B1.SellingPrice END */
        , ''OperateBy'' = '''' --ISNULL(US.FirstName,UH.CreateName)
		, ''ReceiveMoney'' = '''' --ISNULL(UH.TotalPaidAmount,0)
		, ''ReturnMoney'' = '''' --ISNULL(UH.SuspenseAmount,0)

FROM  [SAL].[Booking]' --This is temp table actual table start from below
    /* [ICON_EntForms_UnitHistory] UH 

---JOIN ข้อมูลก่อนเปลี่ยน
      LEFT OUTER JOIN [SAL].[Boooking] B1 ON B1.BookingNumber = UH.ReferentID  AND UH.OperateType in (''A'',''B'',''T'',''BN'') AND UH.IsApprove <> -1 
	  LEFT OUTER JOIN [PRJ].[Unit] U1 ON U1.ProjectID = UH.OldProductID AND U1.UnitNumber = UH.UnitNumber 	
	  LEFT OUTER JOIN [PRJ].[Model] M1 ON M1.ProjectID = U1.ProjectID AND M1.ID = U1.ModelID	

---JOIN ข้อมูลหลังเปลี่ยน
      LEFT OUTER JOIN [SAL].[Booking] B3 ON B3.BookingReferent = UH.ReferentID AND UH.OperateType = ''BN'' AND UH.IsApprove <> -1
	  LEFT OUTER JOIN [PRJ].[Unit] U3 ON U3.ProjectID = B3.ProjectID AND U3.ID = B3.UnitID 	
	  LEFT OUTER JOIN [PRJ].[Model] M3 ON M3.ProjectID = U3.ProjectID AND M3.ID = U3.ModelID	

---ใช้ร่วมกัน
      LEFT OUTER JOIN [SAL].[Booking] P ON P.ProjectID = UH.OldProductID 
	  LEFT OUTER JOIN [PRJ].[Company] C ON C.ID = P.CompanyID 	
      LEFT OUTER JOIN [Users] US ON US.UserID = UH.OperateBy

WHERE (UH.RefType = 1 AND UH.OperateType in (''A'',''B'',''T'',''BN'')) 
	AND UH.IsApprove <> -1 
	AND UH.IsApprove <> 0 '
	
if(Isnull(@CompanyID,'')<>'')set @sql=@sql+'and(C.ID = '''+@CompanyID+''')'
if(Isnull(@ProductID,'')<>'')set @sql=@sql+'and(UH.OldProductID = '''+@ProductID+''')'
if(Isnull(@UnitNumber,'')<>'')set @sql=@sql+'and(UH.UnitNumber = '''+@UnitNumber+''')'
if((YEAR(@DateStart) <> 1800) AND (YEAR(@DateEnd) <> 7000) AND (ISNULL(@DateStart,'')<>'') AND (ISNULL(@DateEnd,'')<>'') )
		set @sql=@sql+'AND (UH.OperateDate BETWEEN '''+Convert(nvarchar(50),@DateStart,120)+''' AND '''+Convert(nvarchar(50),@DateEndInStore,120)+''')'

if(@statustransfer = '1')set @sql=@sql+'and(UH.IsApprove = 1) '
if(@statustransfer = '2')set @sql=@sql+'and(ISNULL(UH.IsApprove,0) = 0) '

if(@Change2 = '0')set @sql=@sql+'and(UH.OperateType in (''A'',''B'',''T'',''BN'')) '
if(@Change2 = '1')set @sql=@sql+'and(UH.OperateType = ''B'') '
if(@Change2 = '2')set @sql=@sql+'and(UH.OperateType = ''T'') '
if(@Change2 = '3')set @sql=@sql+'and(UH.OperateType = ''A'') '
if(@Change2 = '4')set @sql=@sql+'and(UH.OperateType = ''BN'') '


SET @sql=@sql+'ORDER BY UH.OperateDate,UH.OldProductID,UH.UnitNumber' */


--PRINT(@sql)
EXEC(@sql)


GO
