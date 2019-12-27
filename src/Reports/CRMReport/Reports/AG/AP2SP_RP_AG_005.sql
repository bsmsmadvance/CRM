
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_005
	@ProductID nvarchar(30),
	@Year nvarchar(10),
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as Project, '' as Jan_YM, '' as Feb_YM, '' as Mar_YM, '' as App_YM, '' as May_YM, '' as June_YM, '' as July_YM, '' as Aug_YM, '' as Sep_YM, '' as Oct_YM, '' as Nov_YM, '' as Dec_YM, '' as Next,
	'' as JanUnit_YM, '' as FebUnit_YM, '' as MarUnit_YM, '' as AppUnit_YM, '' as MayUnit_YM, '' as JuneUnit_YM, '' as JulyUnit_YM, '' as AugUnit_YM, '' as SepUnit_YM, '' as OctUnit_YM, '' as NovUnit_YM, '' as DecUnit_YM, '' as NextUnit,
	'' as Jan_Due, '' as Feb_Due, '' as Mar_Due, '' as App_Due, '' as May_Due, '' as June_Due, '' as July_Due, '' as Aug_Due, '' as Sep_Due, '' as Oct_Due, '' as Nov_Due, '' as Dec_Due,
	'' as JanUnit_Due, '' as FebUnit_Due, '' as MarUnit_Due, '' as AppUnit_Due, '' as MayUnit_Due, '' as JuneUnit_Due, '' as JulyUnit_Due, '' as AugUnit_Due, '' as SepUnit_Due, '' as OctUnit_Due, '' as NovUnit_Due, '' as DecUnit_Due,
	'' as Jan_PayIN, '' as Feb_PayIN, '' as Mar_PayIN, '' as App_PayIN, '' as May_PayIN, '' as June_PayIN, '' as July_PayIN, '' as Aug_PayIN, '' as Sep_PayIN, '' as Oct_PayIN, '' as Nov_PayIN, '' as Dec_PayIN,
	'' as Jan_CC, '' as Feb_CC, '' as Mar_CC, '' as App_CC, '' as May_CC, '' as June_CC, '' as July_CC, '' as Aug_CC, '' as Sep_CC, '' as Oct_CC, '' as Nov_CC, '' as Dec_CC, '' as JanUnit_CC, '' as FebUnit_CC, '' as MarUnit_CC,
	'' as AppUnit_CC, '' as MayUnit_CC, '' as JuneUnit_CC, '' as JulyUnit_CC, '' as AugUnit_CC, '' as SepUnit_CC, '' as OctUnit_CC, '' as NovUnit_CC, '' as DecUnit_CC from ReportTemplate
END
GO

exec AP2SP_RP_AG_005