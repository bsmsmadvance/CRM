
CREATE OR ALTER PROCEDURE AP2SP_RP_LC_021
  @ProductID  nvarchar(20)=''
, @UnitNumber nvarchar(2000)=''
, @WaterStatus nvarchar(20)=''
, @ElectricStatus nvarchar(20)=''
, @DateStart datetime ='19000101'   -- TransferDateStart
, @DateEnd   datetime ='70000101'  -- TransferDateEnd
, @DateStart2 datetime ='19000101'  -- WaterTransferDateStart
, @DateEnd2   datetime ='70000101'  -- WaterTransferDateEnd
, @DateStart3 datetime ='19000101'  -- ElectricTransferDateStart
, @DateEnd3   datetime ='70000101'  -- ElectricTransferDateEnd
, @UserName nvarchar(150)=''
AS
BEGIN
	SELECT '' as ProductID, '' as Project, '' as UnitNumber, '' as AddressNumber, '' as WaterMeterNumber, '' as ElectricMeterNumber, '' as DocumentCompleteDate, 
	'' as DocumentUncomplete, '' as DocumentUncompleteDate, '' as WaterMeterTransferDate, '' as ElectricMeterTransferDate,
	'' as TransferDateApprove, '' as BU, '' as BUOwner, '' as WaterMeterTransferDate_Save, '' as ElectricMeterTransferDate_Save, '' as WaterMeterNumberSaveDate, 
	'' as ElectricMeterNumberSaveDate from ReportTemplate
END
GO

exec AP2SP_RP_LC_021