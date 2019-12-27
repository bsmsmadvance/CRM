
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_016
	@HomeType nvarchar(20),
	@DateStart datetime,
	@DateEnd datetime,
	@Quarter int, 
	@QuarterYear int,
	@UserName nvarchar(250),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as BU, '' as BUName, '' as Unit_SCB, '' as Unit_KBANK, '' as Unit_BAY, '' as Unit_BBL, '' as Unit_KTB, '' as Unit_GHB, '' as Unit_TBANK, 
	'' as Unit_UOBT, '' as Unit_GSB, '' as Unit_TMB, '' as Unit_CIMB, '' as Unit_OTHER from ReportTemplate
END
GO

exec AP2SP_RP_TR_016