SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from dbo.fn_SplitString('1,2,3,4',',')


CREATE function [dbo].[fn_SplitString](
 @String nvarchar (max),
 @Delimiter nvarchar (10)
 )
returns @ValueTable table ([Val] nvarchar(4000))
begin
 declare @NextString nvarchar(4000)
 declare @Pos int
 declare @NextPos int
 declare @CommaCheck nvarchar(1)
 
 --Initialize
 set @NextString = ''
 set @CommaCheck = right(@String,1) 
 
 --Check for trailing Comma, if not exists, INSERT
 --if (@CommaCheck <> @Delimiter )
 set @String = @String + @Delimiter
 
 --Get position of first Comma
 set @Pos = charindex(@Delimiter,@String)
 set @NextPos = 1
 
 --Loop while there is still a comma in the String of levels
 while (@pos <>  0)  
 begin
  set @NextString = substring(@String,1,@Pos - 1)
 
  insert into @ValueTable ( [Val]) Values (@NextString)
 
  set @String = substring(@String,@pos +1,len(@String))
  
  set @NextPos = @Pos
  set @pos  = charindex(@Delimiter,@String)
 end
 
 return
end
GO
