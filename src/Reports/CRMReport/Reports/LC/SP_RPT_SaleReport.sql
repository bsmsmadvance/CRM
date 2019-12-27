
CREATE OR ALTER PROCEDURE SP_RPT_SaleReport
	@HomeType as nvarchar(50)='',
	@ProductID  as nvarchar(50)='',
	@DateStart as DateTime =null,
	@DateEnd as DateTime =null,
	@UserName  nvarchar(100) = ''
	,@Mode nvarchar(5)=''  -- D=Day    W=Week
AS
BEGIN
	SELECT '' as y, '' as m, '' as w, '' as StartDate, '' as EndDate, '' as DateValue, '' as BG, '' as ProductID, '' as Project, '' as GrossBook_Unit, '' as GrossBook_Value,
	'' as CancelUnit, '' as CancelValue, '' as NetUnit, '' as NetValue, '' as AgreeUnit, '' as AgreeValue, '' as FirstWalk, '' as Revisit_1, '' as Revisit_2, '' as Revisit_3,
	'' as Revisit_More3, '' as Book_FirstWalk, '' as Book_Revisit_1, '' as Book_Revisit_2, '' as Book_Revisit_3, '' as Book_Revisit_More3, '' as SumCallCenter, '' as SumRegister,
	'' as SumAppointment, '' as SumFacebook, '' as GrossBook_Unit_2012, '' as GrossBook_Unit_2013, '' as GrossBook_Unit_2014, '' as GrossBook_Unit_2015,
	'' as GrossBook_Value_2012,'' as GrossBook_Value_2013,'' as GrossBook_Value_2014,'' as GrossBook_Value_2015, '' as CancelUnit_2012, '' as CancelUnit_2013, '' as CancelUnit_2014,
	'' as CancelUnit_2015, '' as CancelValue_2012, '' as CancelValue_2013, '' as CancelValue_2014, '' as CancelValue_2015, '' as NetUnit_2012, '' as NetUnit_2013,
	'' as NetUnit_2014, '' as NetUnit_2015, '' as NetValue_2012, '' as NetValue_2013, '' as NetValue_2014, '' as NetValue_2015, '' as AgreeUnit_2012, '' as AgreeUnit_2013,
	'' as AgreeUnit_2014, '' as AgreeUnit_2015, '' as AgreeValue_2012, '' as AgreeValue_2013, '' as AgreeValue_2014, '' as AgreeValue_2015, '' as TransUnit_2012,
	'' as TransUnit_2013, '' as TransUnit_2014, '' as TransUnit_2015, '' as TransValue_2012, '' as TransValue_2013, '' as TransValue_2014, '' as TransValue_2015,
	'' as FirstWalk_2012, '' as FirstWalk_2013, '' as FirstWalk_2014, '' as FirstWalk_2015, '' as Revisit_1_2012, '' as Revisit_1_2013, '' as Revisit_1_2014, '' as Revisit_1_2015,
	'' as Revisit_2_2012, '' as Revisit_2_2013, '' as Revisit_2_2014, '' as Revisit_2_2015, '' as Revisit_3_2012, '' as Revisit_3_2013, '' as Revisit_3_2014, '' as Revisit_3_2015,
	'' as Revisit_More3_2012, '' as Revisit_More3_2013, '' as Revisit_More3_2014, '' as Revisit_More3_2015, '' as Book_FirstWalk_2012, '' as Book_FirstWalk_2013, '' as Book_FirstWalk_2014,
	'' as Book_FirstWalk_2015, '' as Book_Revisit_1_2012, '' as Book_Revisit_1_2013, '' as Book_Revisit_1_2014, '' as Book_Revisit_1_2015, '' as Book_Revisit_2_2012,
	'' as Book_Revisit_2_2013, '' as Book_Revisit_2_2014, '' as Book_Revisit_2_2015, '' as Book_Revisit_3_2012, '' as Book_Revisit_3_2013, '' as Book_Revisit_3_2014,
	'' as Book_Revisit_3_2015, '' as Book_Revisit_More3_2012, '' as Book_Revisit_More3_2013, '' as Book_Revisit_More3_2014, '' as Book_Revisit_More3_2015,
	'' as SumCallCenter_2012, '' as SumCallCenter_2013, '' as SumCallCenter_2014, '' as SumCallCenter_2015, '' as SumRegister_2012, '' as SumRegister_2013, '' as SumRegister_2014,
	'' as SumRegister_2015, '' as SumAppointment_2012, '' as SumAppointment_2013, '' as SumAppointment_2014, '' as SumAppointment_2015, '' as SumFaceBook_2012, '' as SumFaceBook_2013,
	'' as SumFaceBook_2014, '' as SumFaceBook_2015, '' as Year1, '' as Year2, '' as Year3, '' as Year4, '' as CreateDate from ReportTemplate
END
GO

exec SP_RPT_SaleReport