
--

CREATE OR ALTER PROCEDURE AP2SP_PF_LC_005
	@TransferNumber nvarchar(50)
AS
BEGIN
	SELECT '' as CustomerName, '' as CustomerTel, '' as ProjectName, '' as HomeNumber, '' as RoomNumber, '' as TitleDeedNumber, '' as TransferDate, '' as LCName,
	'' as LC_Tel_No, '' as LC_Fax_No, '' as TransferEmp, '' as TransferEmp_Tel_No, '' as PriceOfContract, '' as AreaOwnerShip, '' as Pay_Down_Contract_Booking,
	'' as Act_Pay_Down_Contract_Booking, '' as TransferMoney, '' as Promotion, '' as MoneyOfCreditBank, '' as PercentOfLoan, '' as ExtraDiscount, '' as PayMaterial,
	'' as Fire, '' as TransferFee from ReportTemplate
END
GO

exec AP2SP_PF_LC_005