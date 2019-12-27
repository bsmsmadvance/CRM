
--การโอนสิทธิ์ภาษาอังกฤษ

CREATE OR ALTER PROCEDURE AP2SP_PF_LC_002_2Eng
	@ContractNumber nvarchar(20),
    @HistoryID nvarchar(20)
AS
BEGIN
	SELECT '' as CustNameTransfer, '' as OldAge, '' as OldAddress, '' as OldMoo, '' as OldVillage, '' as OldSoi, '' as OldRoad, '' as OldSubdistrict, '' as OldDistrict,
	'' as OldProvince, '' as CustNameTransferNew, '' as NewAge, '' as NewAddress, '' as NewMoo, '' as NewVillage, '' as NewSoi, '' as NewRoad, '' as NewSubdostrict, '' as NewDistrict,
	'' as NewProvince, '' as CompanyID, '' as CompanyName, '' as ProductType, '' as ContractNumber, '' as ContractDate, '' as ProjectName, '' as UnitNumber, '' as Paid,
	'' as PaidText, '' as TransferDate, '' as ReceivedID, '' as EffectDate, '' as SignDate, '' as ProductID, '' as BUID, '' as ForType, '' as TypeDesc, '' as OperateType from ReportTemplate
END
GO

exec AP2SP_PF_LC_002_2Eng