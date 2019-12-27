SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetPromotionDescriptionAmount](
	@DescriptionTH nvarchar(500),
	@Amount int,
	@UnitNameTH nvarchar(250),
	@UnitNameENG nvarchar(250),
	@Language nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT CASE WHEN (@DescriptionTH LIKE '%รูดบัตรเครดิต%' AND @DescriptionTH LIKE '%0%%')
							OR (@DescriptionTH LIKE '%คงที่ร้อยละ 2.3%' AND @DescriptionTH LIKE '%3 ปี%')
							OR (@DescriptionTH LIKE '%คงที่ร้อยละ 0%' AND @DescriptionTH LIKE '%2 ปี%')  
							OR (@DescriptionTH LIKE '%ดอกเบี้ย 2%%' AND @DescriptionTH LIKE '%2 ปี %')  
							OR (@DescriptionTH LIKE '%0%%' AND @DescriptionTH LIKE '%6 เดือน%')  
							OR (@DescriptionTH LIKE '%โปรโมชั่นอยู่ฟรี%') 
							OR (@DescriptionTH LIKE '%อยู่ฟรี%') 
							OR (@DescriptionTH LIKE '%ดอกเบี้ยคงที่ 3%' AND @DescriptionTH LIKE '%3 ปี%')
							OR (@DescriptionTH LIKE '%SCB%' AND @DescriptionTH LIKE '%ผ่อนต่ำ%')
							OR (@DescriptionTH LIKE '%SCB%' AND @DescriptionTH LIKE '%ดอกเบี้ย%')
							OR (@DescriptionTH LIKE '%ดอกเบี้ย%' AND @DescriptionTH LIKE '%1 ปี%')
							OR (@DescriptionTH LIKE '%ผ่อนต่ำ%' AND @DescriptionTH LIKE '%1 ปี%')
							OR (@DescriptionTH LIKE '%ผ่อน%' AND @DescriptionTH LIKE '%1.5 ปี%')
							OR (@DescriptionTH LIKE '%2 ปี%' AND @DescriptionTH LIKE '%ดอกเบี้ย%')
							OR (@DescriptionTH LIKE '%ผ่อนถูก%' AND @DescriptionTH LIKE '%3,000%')
							OR (@DescriptionTH LIKE '%ผ่อนเบา%' AND @DescriptionTH LIKE '%SCB%')
							OR (@DescriptionTH LIKE '%3 ปี%' AND @DescriptionTH LIKE '%SCB%')
							OR (@DescriptionTH LIKE '%อัตราดอกเบี้ย%')
							OR (@DescriptionTH LIKE '%Pleno มหัศจรรย์ 1999%')
							OR (@DescriptionTH LIKE '%Pre sale%')
							--OR (@DescriptionTH LIKE '%พิเศษ%' AND @DescriptionTH LIKE '%SCB%')
							--OR (@DescriptionTH LIKE '%อยู่ฟรี%' AND @DescriptionTH LIKE '%SCB%')
					    THEN 
							'' 
						ELSE
							CASE WHEN @Language = 'TH' THEN
								CASE WHEN @Amount > 0 THEN ' จำนวน ' + CAST(@Amount AS varchar(10)) + ' ' + ISNULL(@UnitNameTH,'') ELSE '' END 
								--' จำนวน ' + CAST(@Amount AS varchar(10)) + ' ' + ISNULL(@UnitNameTH,'')
							ELSE
								
						CASE WHEN @Amount > 0 THEN ' ' + CAST(@Amount AS varchar(10)) + ' ' + ISNULL(@UnitNameENG, CASE WHEN @Amount > 1 THEN 'Items' ELSE 'Item' END) ELSE '' END
								--CAST(@Amount AS varchar(10)) + ' ' + ISNULL(@UnitNameENG, CASE WHEN @Amount > 1 THEN 'Items' ELSE 'Item' END) 
							END
						END
		)
END





GO
