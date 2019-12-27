
CREATE OR ALTER PROCEDURE AP2SP_RP_LC_029
--DECLARE 
	@CompanyID NVARCHAR(50)
  , @ProductID NVARCHAR(20)
  , @UnitNumber NVARCHAR(500)
  , @DateStart DATETIME
  , @DateEnd DATETIME
  , @DateStart2 DATETIME
  , @DateEnd2 DATETIME
  , @Status NVARCHAR(20)
  , @HomeType NVARCHAR(20)
  , @UserName NVARCHAR(150)
  , @ProjectGroup NVARCHAR(5)
  , @ProjectType2 NVARCHAR(5)
AS
BEGIN
	SELECT '' as RefType, '' as ReferenceID, '' as CompanyID, '' as CompanyName, '' as ProductID, '' as ProjectName, '' as TypeOfRealEstate, '' as UnitNumber, 
	'' as BookingNumber, '' as ContractNumber, '' as CustomerName,
	'' as Type, '' as OperateDate, '' as BookingDate, '' as ContractDate, '' as ApproveBy, '' as TotalSellingPrice, '' as Area, '' as SaleName, '' as OperateBy, 
	'' as ReceiveAmount, '' as ReceiveMoney, '' as ReturnMoney, '' as CancelType,
	'' as HoldCancelCreateDate, '' as HoldCancelStatus, '' as HoldCancelStatusDate, '' as HoldCancelBy from ReportTemplate
END
GO

exec AP2SP_RP_LC_029