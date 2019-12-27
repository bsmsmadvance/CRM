SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[fn_GetSpecialInstallmentCount](
	@SpecialInstallment varchar(50)
)RETURNS int
BEGIN

    DECLARE @result int;
    SET @result = 0;
    IF (@SpecialInstallment > 0) 
    BEGIN
        SET @SpecialInstallment=ltrim(rtrim(@SpecialInstallment))
    
        while charindex(',',@SpecialInstallment)>0
        Begin
            set @SpecialInstallment=replace(@SpecialInstallment,',',' ')
        end
        
        SET @result = len(@SpecialInstallment)-len(replace(@SpecialInstallment,' ',''))+1;

    END

    RETURN @result;
END



GO
